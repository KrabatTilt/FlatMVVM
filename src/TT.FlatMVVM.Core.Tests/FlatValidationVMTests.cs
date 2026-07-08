using System.Collections;
using NUnit.Framework;

namespace TT.FlatMVVM.Core.Tests;

[TestFixture]
public class FlatValidationVMTests
{
    private static readonly string[] InvalidErrors = ["invalid"];
    private static readonly string[] FirstSecondErrors = ["first", "second"];
    private static readonly string[] EntityErrors = ["entity"];

    [Test]
    public void Initially_HasErrorsIsFalse()
    {
        var vm = new TestValidationVM();

        Assert.That(vm.HasErrors, Is.False);
    }

    [Test]
    public void SetProperty_AddsValidationErrorSynchronously()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };

        vm.Value = "bad";

        Assert.That(vm.HasErrors, Is.True);
        Assert.That(GetErrors(vm, nameof(TestValidationVM.Value)), Is.EqualTo(InvalidErrors));
    }

    [Test]
    public void SetProperty_RaisesErrorsChangedForChangedProperty()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };
        var changedProperties = new List<string?>();
        vm.ErrorsChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "bad";

        Assert.That(changedProperties, Is.EqualTo(new[] { nameof(TestValidationVM.Value) }));
    }

    [Test]
    public void SetProperty_RaisesHasErrorsPropertyChangedWhenErrorStateChangesToTrue()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "bad";

        Assert.That(changedProperties, Does.Contain(nameof(TestValidationVM.HasErrors)));
    }

    [Test]
    public void SetProperty_ClearsErrorsWhenRuleReturnsNoErrors()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };
        vm.Value = "bad";
        vm.Rule = _ => Array.Empty<string>();

        vm.Value = "good";

        Assert.That(vm.HasErrors, Is.False);
        Assert.That(GetErrors(vm, nameof(TestValidationVM.Value)), Is.Empty);
    }

    [Test]
    public void SetProperty_RaisesHasErrorsPropertyChangedWhenErrorStateChangesToFalse()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };
        vm.Value = "bad";
        vm.Rule = _ => Array.Empty<string>();
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "good";

        Assert.That(changedProperties, Does.Contain(nameof(TestValidationVM.HasErrors)));
    }

    [Test]
    public void SetProperty_SameValidationErrorsDoNotRaiseDuplicateErrorsChanged()
    {
        var vm = new TestValidationVM { Rule = _ => new[] { "invalid" } };
        vm.Value = "first";
        var changedProperties = new List<string?>();
        vm.ErrorsChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "second";

        Assert.That(changedProperties, Is.Empty);
    }

    [Test]
    public void GetErrors_ReturnsExpectedPropertyErrors()
    {
        var vm = new TestValidationVM();
        vm.SetValueErrors("first", "second");

        Assert.That(GetErrors(vm, nameof(TestValidationVM.Value)), Is.EqualTo(FirstSecondErrors));
    }

    [Test]
    public void GetErrors_NullAndEmptyReturnEntityLevelErrorsOnly()
    {
        var vm = new TestValidationVM();
        vm.SetValueErrors("first");

        Assert.That(GetErrors(vm, null), Is.Empty);
        Assert.That(GetErrors(vm, string.Empty), Is.Empty);

        vm.SetEntityErrors("entity");

        Assert.That(GetErrors(vm, null), Is.EqualTo(EntityErrors));
        Assert.That(GetErrors(vm, string.Empty), Is.EqualTo(EntityErrors));
    }

    [Test]
    public void SetProperty_BadValidationRuleExceptionIsNotSwallowed()
    {
        var vm = new TestValidationVM { Rule = _ => throw new InvalidOperationException("bad rule") };

        var exception = Assert.Throws<InvalidOperationException>(() => vm.Value = "bad");

        Assert.That(exception?.Message, Is.EqualTo("bad rule"));
        Assert.That(vm.HasErrors, Is.False);
    }

    [Test]
    public void SetProperty_WithValidationRaisesLinkedPropertyNotifications()
    {
        var vm = new TestValidationVM();
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.LinkedValue = "changed";

        Assert.That(changedProperties, Is.EqualTo(new[]
        {
            nameof(TestValidationVM.LinkedValue),
            nameof(TestValidationVM.Linked1),
            nameof(TestValidationVM.Linked2)
        }));
    }

    private static string[] GetErrors(TestValidationVM vm, string? propertyName)
    {
        return vm.GetErrors(propertyName).Cast<string>().ToArray();
    }

    private sealed class TestValidationVM : FlatValidationVM
    {
        private string? _value;
        private string? _linkedValue;

        public Func<string?, IEnumerable<string>> Rule { get; set; } = _ => Array.Empty<string>();

        public string? Value
        {
            get => _value;
            set => SetProperty(ref _value, value, Rule);
        }

        public string? Linked1 => LinkedValue;

        public string? Linked2 => LinkedValue;

        public string? LinkedValue
        {
            get => _linkedValue;
            set => SetProperty(ref _linkedValue, value, new[] { nameof(Linked1), nameof(Linked2) }, Rule);
        }

        public void SetValueErrors(params string[] errors)
        {
            SetErrors(nameof(Value), errors);
        }

        public void SetEntityErrors(params string[] errors)
        {
            SetErrors(string.Empty, errors);
        }
    }
}



using System.Collections;
using System.ComponentModel;
using NUnit.Framework;

namespace TT.FlatMVVM.Core.Tests;

[TestFixture]
public class FlatVMTests
{
    [Test]
    public void SetProperty_ChangesValueAndRaisesPropertyChanged()
    {
        var vm = new TestVM();
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "changed";

        Assert.That(vm.Value, Is.EqualTo("changed"));
        Assert.That(changedProperties, Is.EqualTo(new[] { nameof(TestVM.Value) }));
    }

    [Test]
    public void SetProperty_DoesNotRaiseWhenValueIsEqual()
    {
        var vm = new TestVM { Value = "same" };
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.Value = "same";

        Assert.That(changedProperties, Is.Empty);
    }

    [Test]
    public void SetProperty_RaisesLinkedProperties()
    {
        var vm = new TestVM();
        var changedProperties = new List<string?>();
        vm.PropertyChanged += (_, args) => changedProperties.Add(args.PropertyName);

        vm.LinkedValue = "changed";

        Assert.That(changedProperties, Is.EqualTo(new[]
        {
            nameof(TestVM.LinkedValue),
            nameof(TestVM.Linked1),
            nameof(TestVM.Linked2)
        }));
    }

    private sealed class TestVM : FlatVM
    {
        private string? _value;
        private string? _linkedValue;

        public string? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public string? Linked1 => LinkedValue;

        public string? Linked2 => LinkedValue;

        public string? LinkedValue
        {
            get => _linkedValue;
            set => SetProperty(ref _linkedValue, value, new[] { nameof(Linked1), nameof(Linked2) });
        }
    }
}

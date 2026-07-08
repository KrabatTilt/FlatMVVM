using NUnit.Framework;

namespace TT.FlatMVVM.Core.Tests;

[TestFixture]
public class DelegateCommandTests
{
    [Test]
    public void ParameterlessCommand_ExecutesAndCanExecuteWorks()
    {
        var executed = false;
        var canExecute = false;
        var command = new DelegateCommand(
            new LocalEventStrategy(),
            () => executed = true,
            () => canExecute);

        Assert.That(command.CanExecute(null), Is.False);

        canExecute = true;
        Assert.That(command.CanExecute(new object()), Is.True);

        command.Execute(null);

        Assert.That(executed, Is.True);
    }

    [Test]
    public void StringCommand_AcceptsNullAndNonNullValues()
    {
        var received = new List<string?>();
        var command = new DelegateCommand<string>(
            new LocalEventStrategy(),
            value => received.Add(value),
            value => value is null || value.Length > 0);

        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);

        Assert.That(command.CanExecute("hello"), Is.True);
        command.Execute("hello");

        Assert.That(received, Is.EqualTo(new string?[] { null, "hello" }));
    }

    [Test]
    public void IntCommand_RejectsNullInCanExecuteAndExecuteThrowsClearly()
    {
        var command = new DelegateCommand<int>(new LocalEventStrategy(), _ => { });

        Assert.That(command.CanExecute(null), Is.False);

        var exception = Assert.Throws<ArgumentException>(() => command.Execute(null));
        Assert.That(exception?.ParamName, Is.EqualTo("parameter"));
    }

    [Test]
    public void NullableIntCommand_AcceptsNull()
    {
        int? received = 42;
        var command = new DelegateCommand<int?>(new LocalEventStrategy(), value => received = value);

        Assert.That(command.CanExecute(null), Is.True);
        command.Execute(null);

        Assert.That(received, Is.Null);
    }

    [Test]
    public void WrongParameterType_MakesCanExecuteFalse()
    {
        var command = new DelegateCommand<int>(new LocalEventStrategy(), _ => { });

        Assert.That(command.CanExecute("wrong"), Is.False);
    }

    [Test]
    public void WrongParameterType_MakesExecuteThrow()
    {
        var command = new DelegateCommand<int>(new LocalEventStrategy(), _ => { });

        var exception = Assert.Throws<ArgumentException>(() => command.Execute("wrong"));
        Assert.That(exception?.ParamName, Is.EqualTo("parameter"));
    }

    [Test]
    public void TwoParameterCommand_AcceptsValidTuple()
    {
        string? receivedText = null;
        var receivedNumber = 0;
        var command = new DelegateCommand<string, int>(
            new LocalEventStrategy(),
            (text, number) =>
            {
                receivedText = text;
                receivedNumber = number;
            },
            (text, number) => text == "ready" && number == 7);

        Assert.That(command.CanExecute(("ready", 7)), Is.True);
        command.Execute(("ready", 7));

        Assert.That(receivedText, Is.EqualTo("ready"));
        Assert.That(receivedNumber, Is.EqualTo(7));
    }

    [Test]
    public void TwoParameterCommand_HandlesNullableTupleItemsCorrectly()
    {
        string? receivedText = "initial";
        int? receivedNumber = 42;
        var command = new DelegateCommand<string, int?>(
            new LocalEventStrategy(),
            (text, number) =>
            {
                receivedText = text;
                receivedNumber = number;
            },
            (text, number) => text is null && number is null);

        Assert.That(command.CanExecute(((string?)null, (int?)null)), Is.True);
        command.Execute(((string?)null, (int?)null));

        Assert.That(receivedText, Is.Null);
        Assert.That(receivedNumber, Is.Null);
    }

    [Test]
    public void TwoParameterCommand_RejectsWrongTupleItemTypes()
    {
        var command = new DelegateCommand<string, int>(new LocalEventStrategy(), (_, _) => { });

        Assert.That(command.CanExecute(("ready", "wrong")), Is.False);

        var exception = Assert.Throws<ArgumentException>(() => command.Execute(("ready", "wrong")));
        Assert.That(exception?.ParamName, Is.EqualTo("parameter"));
    }

    [Test]
    public void RaiseCanExecuteChanged_WorksWithLocalEventStrategy()
    {
        var command = new DelegateCommand(new LocalEventStrategy(), () => { });
        object? receivedSender = null;
        var raised = false;
        command.CanExecuteChanged += (sender, _) =>
        {
            raised = true;
            receivedSender = sender;
        };

        command.RaiseCanExecuteChanged();

        Assert.That(raised, Is.True);
        Assert.That(receivedSender, Is.SameAs(command));
    }
}


using System.Windows.Input;
using NUnit.Framework;

namespace TT.FlatMVVM.Core.Tests;

[TestFixture]
public class CommandFactoryTests
{
    [Test]
    public void Create_UsesLocalEventStrategyAndCreatesUsableCommand()
    {
        var executed = false;
        var canExecute = false;
        var factory = new CommandFactory(new LocalEventStrategy());

        var command = factory.Create(
            () => executed = true,
            () => canExecute);

        Assert.That(command.CanExecute(null), Is.False);

        canExecute = true;

        Assert.That(command.CanExecute(null), Is.True);

        command.Execute(null);

        Assert.That(executed, Is.True);
    }

    [Test]
    public void Create_GenericCommandCreatesUsableCommand()
    {
        string? received = null;
        var factory = new CommandFactory(new LocalEventStrategy());

        ICommand command = factory.Create<string>(value => received = value);

        Assert.That(command.CanExecute("hello"), Is.True);

        command.Execute("hello");

        Assert.That(received, Is.EqualTo("hello"));
    }

    [Test]
    public void CreatedCommand_RaiseCanExecuteChangedRaisesEvent()
    {
        var factory = new CommandFactory(new LocalEventStrategy());
        var command = (CommandBase)factory.Create(() => { });
        var raised = false;
        object? receivedSender = null;

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

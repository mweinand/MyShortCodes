using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShortCodes.Phone.Infrastructure.Messaging;

namespace MyShortCodes.Phone.Tests.Infrastructure.Messaging
{
    [TestClass]
    public class CommandBusTests
    {
        [TestMethod]
        public void can_add_command_handler_and_use_it()
        {
            var commandBus = new CommandBus();
            var command = new TestCommand();
            var handler = new TestCommandHandler();

            commandBus.RegisterHandler(handler);

            commandBus.PublishCommand(command);

            Assert.IsTrue(command.WasHandled);
        }

        [TestMethod]
        public void can_add_action_handler_and_use_it()
        {
            var commandBus = new CommandBus();
            var command = new TestCommand();

            commandBus.RegisterHandler<TestCommand>(x =>
                                                        {
                                                            x.WasHandled = true;
                                                        });

            commandBus.PublishCommand(command);

            Assert.IsTrue(command.WasHandled);

        }
    }

    public class TestCommand : ICommand
    {
        public bool WasHandled { get; set; }
    }

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Handle(TestCommand command)
        {
            command.WasHandled = true;
        }
    }
}
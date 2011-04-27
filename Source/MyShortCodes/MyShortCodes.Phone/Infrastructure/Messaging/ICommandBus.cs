using System;
using System.Collections.Generic;
using System.Threading;

namespace MyShortCodes.Phone.Infrastructure.Messaging
{
    public interface ICommandBus
    {
        void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
        void RegisterHandler<TCommand>(Action<TCommand> action);

        void PublishCommand<TCommand>(TCommand command) where TCommand : ICommand;
    }

    public class CommandBus : ICommandBus
    {
        private readonly Dictionary<Type, List<object>> _store;

        public CommandBus()
        {
            _store = new Dictionary<Type, List<object>>();
        }

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            var commandType = typeof (TCommand);
            if(!_store.ContainsKey(commandType))
            {
                _store.Add(commandType, new List<object>());
            }
            _store[commandType].Add(handler);
        }

        public void RegisterHandler<TCommand>(Action<TCommand> action)
        {
            var commandType = typeof(TCommand);
            if (!_store.ContainsKey(commandType))
            {
                _store.Add(commandType, new List<object>());
            }
            _store[commandType].Add(action);
        }

        public void PublishCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = typeof(TCommand);
            if (!_store.ContainsKey(commandType))
            {
                return;
            }

            var actions = _store[commandType];

            foreach(var action in actions)
            {
                var actionType = action.GetType();
                if (typeof(ICommandHandler<TCommand>).IsAssignableFrom(actionType))
                {
                    var commandHandler = action as ICommandHandler<TCommand>;
                    if(commandHandler == null)
                    {
                        continue;
                    }
                    ThreadPool.QueueUserWorkItem((s) =>
                    {
                        commandHandler.Handle(command);
                    });
                }
                else if (actionType == typeof(Action<TCommand>))
                {
                    var actionHandler = action as Action<TCommand>;
                    if(actionHandler == null)
                    {
                        continue;
                    }
                    ThreadPool.QueueUserWorkItem((s) =>
                    {
                        actionHandler(command);
                    });
                }
            }
        }
    }
}
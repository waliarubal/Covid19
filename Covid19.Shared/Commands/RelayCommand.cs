using Covid19.Shared.Base;
using System;
using System.Threading.Tasks;

namespace Covid19.Shared.Commands
{
    public class RelayCommand : CommandBase
    {
        readonly Action _action;

        public RelayCommand(Action action, bool isEnabled = true, bool isAsynchronous = false) : base(isEnabled, isAsynchronous)
        {
            _action = action;
        }

        public async override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = true;

            if (IsAsynchronous)
                await Task.Run(_action);
            else
                _action.Invoke();

            IsExecuting = false;
        }
    }

    public class RelayCommand<P> : CommandBase
    {
        readonly Action<P> _action;

        public RelayCommand(Action<P> action, bool isEnabled = true, bool isAsynchronous = false) : base(isEnabled, isAsynchronous)
        {
            _action = action;
        }

        public async override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            IsExecuting = true;

            P argument;
            try
            {
                argument = (P)parameter;
            }
            catch (InvalidCastException)
            {
                argument = default;
            }

            if (IsAsynchronous)
                await Task.Run(() => _action.Invoke(argument));
            else
                _action.Invoke((P)parameter);

            IsExecuting = false;
        }
    }
}

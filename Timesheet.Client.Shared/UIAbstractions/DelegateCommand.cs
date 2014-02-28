using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Timesheet.Client.Shared.UIAbstractions
{
    public class DelegateCommand<T>: DelegateCommand
    {
        public DelegateCommand(Func<T, bool> canExecute, Func<T, Task> onExecute)
            : base(o => canExecute((T) o), o => onExecute((T) o))
        {
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Func<object, Task> _onExecute;
        private bool _executing;

        public DelegateCommand(Func<bool> canExecute, Func<Task> onExecute)
        {
            _canExecute = o => canExecute();
            _onExecute = o => onExecute();
        }

        public DelegateCommand(Func<object, bool> canExecute, Func<object, Task> onExecute)
        {
            _canExecute = canExecute;
            _onExecute = onExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !_executing && _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        private async Task ExecuteAsync(object parameter)
        {
            ExecutingChanged(true);
            try
            {
                await _onExecute(parameter);
            }
            finally
            {
                ExecutingChanged(false);
            }
        }

        private void ExecutingChanged(bool isExecuting)
        {
            _executing = isExecuting;
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}
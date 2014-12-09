using System;
using System.Windows.Input;

namespace App
{
    public sealed class RelayCommand : ICommand
    {
        private readonly Func<object, bool> _canExecute;

        private readonly Action<object> _action;

        public RelayCommand(Action<object> action, Func<object, bool> canExecute= null)
        {
            if(action == null) throw new ArgumentException("action can't be null");

            _canExecute = canExecute ?? (o=> true);
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            var canExecute = _canExecute(parameter);
            return canExecute;
        }

        public void Execute(object parameter)
        {
            if(CanExecute(parameter))
                _action(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
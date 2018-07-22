using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordStore.Common
{
    public class Command<T> : ICommand
    {
        private bool _canExecute { get; set; }
        private Action<T> _action { get; set; }

        public Command(Action<T> action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute;

        public void Execute(object parameter)
        {
            _action((T)parameter);
        }
    }
}

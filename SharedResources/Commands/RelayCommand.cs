using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
#pragma warning disable CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
    public class RelayCommand(Action methodToExecute) : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action methodToExecute = methodToExecute;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            methodToExecute?.Invoke();
        }
    }

    public class RelayCommand<T>(Action<T> methodToExectue) : ICommand where T : struct
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is not null)
            {
                methodToExectue?.Invoke((T)parameter);
            }
        }
    }
#pragma warning restore CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin
{
    public class RelayCommand(Action methodToExecute) : ICommand
    {
#pragma warning disable CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'RelayCommand.CanExecuteChanged' is never used
        private readonly Action methodToExecute = methodToExecute;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            methodToExecute?.Invoke();
        }
    }
}

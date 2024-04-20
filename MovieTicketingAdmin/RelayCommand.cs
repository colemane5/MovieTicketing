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
        public event EventHandler? CanExecuteChanged;
        private readonly Action methodToExecute = methodToExecute;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            methodToExecute?.Invoke();
        }
    }
}

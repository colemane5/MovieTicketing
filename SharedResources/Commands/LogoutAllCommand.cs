using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class LogoutAllCommand() : ICommand
    {
        private SqlUserRepository _userRepository = new();

#pragma warning disable CS0067 // The event 'LogoutCommand.CanExecuteChanged' is never used
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'LogoutCommand.CanExecuteChanged' is never used

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _userRepository.LogoutAll();
        }
    }
}

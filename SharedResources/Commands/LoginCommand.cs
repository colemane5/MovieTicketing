using SharedResources.Models;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SharedResources.SqlInterfaces;

namespace SharedResources.Commands
{
    public class LoginCommand(Func<ViewModelBase> getAdminViewModelStart, Func<ViewModelBase> getClientViewModelStart,
                              Action<string> loginFailShowMessage, NavigationService navigationService) : ICommand
    {
        private SqlUserRepository _userRepository = new();
        private readonly Func<ViewModelBase> _getClientViewModelStart = getClientViewModelStart;
        private readonly Func<ViewModelBase> _getAdminViewModelStart = getAdminViewModelStart;
        private readonly NavigationService _navigationService = navigationService;
        private readonly Action<string> _loginFailShowMessage = loginFailShowMessage;

#pragma warning disable CS0067 // The event 'LoginCommand.CanExecuteChanged' is never used
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'LoginCommand.CanExecuteChanged' is never used

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is string email)
            {                
                User? userN = _userRepository.LoginUser(email);
                if (userN is User user)
                {
                    Func<ViewModelBase> getViewModelStart = user.IsAdmin ? _getAdminViewModelStart : _getClientViewModelStart;
                    _navigationService.CurrentUser = user;
                    _navigationService.ChangeViewModel(getViewModelStart());
                }
                else _loginFailShowMessage(email);
            }
        }
    }
}

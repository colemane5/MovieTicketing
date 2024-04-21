using SharedResources.Models;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class LoginCommand(Func<ViewModelBase> getAdminViewModelStart, Func<ViewModelBase> getClientViewModelStart,
                              NavigationService navigationService) : ICommand
    {
        private readonly Func<ViewModelBase> _getClientViewModelStart = getClientViewModelStart;
        private readonly Func<ViewModelBase> _getAdminViewModelStart = getAdminViewModelStart;
        private readonly NavigationService _navigationService = navigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is string email)
            {
                User user = new User(101, email.Split('@')[0], email, email.Split('@')[1] == "admin.com"); // replace with database call
                Func<ViewModelBase> getViewModelStart = user.IsAdmin ? _getAdminViewModelStart : _getClientViewModelStart;
                _navigationService.CurrentUser = user;
                _navigationService.ChangeViewModel(getViewModelStart()); 
            }
        }
    }
}

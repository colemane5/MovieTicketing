using MovieTicketingAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin
{
    public class NavigateCommand<T>(NavigationService navigationService) : ICommand where T : ViewModelBase
    {
        private readonly NavigationService _navigationService = navigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _navigationService.ChangeViewModel<T>();
        }
    }
}

using MovieTicketingAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MovieTicketingAdmin
{
    public class LoginCommand(List<ViewModelBase> viewModels, NavigationService navigationService) : ICommand
    {
        private readonly List<ViewModelBase> _viewModels = viewModels;
        private readonly NavigationService _navigationService = navigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            foreach (ViewModelBase viewModel in _viewModels)
            {
                _navigationService.AddViewModel(viewModel);
            }
        }
    }
}

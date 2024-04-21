using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class LogoutCommand(Func<ViewModelBase?> getStartViewModel, Func<NavigationService?> getNavigationService) : ICommand
    {
        private readonly Func<ViewModelBase?> _getStartViewModel = getStartViewModel;
        private readonly Func<NavigationService?> _getNavigationService = getNavigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (_getNavigationService() is NavigationService navigationService
             && _getStartViewModel() is ViewModelBase startViewModel)
            {
                navigationService.ClearViewModels();
                navigationService.ReturnToStart();
            }
        }
    }
}

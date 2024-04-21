using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class NavigateCommand<TViewModel>(Func<NavigationService?> getNavigationService) : ICommand where TViewModel : ViewModelBase, new()
    {
        private readonly Func<NavigationService?> _getNavigationService = getNavigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            _getNavigationService()?.ChangeViewModel<TViewModel>();
        }
    }
}

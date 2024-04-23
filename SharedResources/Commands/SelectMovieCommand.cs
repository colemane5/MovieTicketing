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
    public class SelectMovieCommand<TViewModel>(Func<NavigationService?> getNavigationService) 
        : ICommand where TViewModel: ViewModelBase, new()
    {
        private readonly Func<NavigationService?> _getNavigationService = getNavigationService;

#pragma warning disable CS0067 // The event 'SelectMovieCommand.CanExecuteChanged' is never used
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'SelectMovieCommand.CanExecuteChanged' is never used

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is not Movie movie) return;
            if (_getNavigationService() is not NavigationService navigationService) return;

            navigationService.ChangeViewModel<TViewModel>();
            if (navigationService.CurrentViewModel is IMovieSelectable movieSelectable)
            {
                movieSelectable.SelectMovie(movie);
            }
        }
    }
}

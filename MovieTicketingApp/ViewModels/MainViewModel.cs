using SharedResources;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase? CurrentViewModel => _navigationService?.CurrentViewModel;

        public MainViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

using SharedResources.Commands;
using SharedResources.Models;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class NavigationService
    {
        public event EventHandler? CurrentViewModelChanged;

        public ViewModelBase _currentViewModel = new();
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke(this, new EventArgs());
            }
        }

        public Func<ViewModelBase> StartViewModel { get; set; } = () => new();

        public User? CurrentUser { get; set; }

        public void ChangeViewModel<TViewModel>() where TViewModel : ViewModelBase, new() => ChangeViewModel(new TViewModel());

        public void ChangeViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
            CurrentViewModel.RegisterNavigationService(this);
            CurrentViewModel.RegisterUser(CurrentUser);
        }

        public void ReturnToStart()
        {
            if (StartViewModel is not null)
            {
                CurrentViewModel = StartViewModel();
                CurrentUser = null;
            }
        }
    }
}

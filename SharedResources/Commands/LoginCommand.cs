using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.Commands
{
    public class LoginCommand(IEnumerable<ViewModelBase> adminViewModels, IEnumerable<ViewModelBase> clientViewModels,
                              NavigationService navigationService) : ICommand
    {
        private readonly IEnumerable<ViewModelBase> _clientViewModels = clientViewModels;
        private readonly IEnumerable<ViewModelBase> _adminViewModels = adminViewModels;
        private readonly NavigationService _navigationService = navigationService;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            IEnumerable<ViewModelBase> viewModels = true /* Replace with userType == admin */ ? _adminViewModels : _clientViewModels;
            ViewModelBase startViewModel = viewModels.First();
            foreach (ViewModelBase viewModel in viewModels)
            {
                _navigationService.AddViewModel(viewModel);
                viewModel.RegisterNavigationService(_navigationService);
            }
            _navigationService.ChangeViewModel(startViewModel);
        }
    }
}

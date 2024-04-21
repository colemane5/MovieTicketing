using MovieTicketingAdmin.ViewModels;
using MovieTicketingApp.ViewModels;
using MovieTicketingApp.Views;
using MovieTicketingClient.ViewModels;
using SharedResources;
using SharedResources.Commands;
using SharedResources.Models;
using SharedResources.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace MovieTicketingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel? _mainViewModel { get; set; }
        private NavigationService? _navigationService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationService = new();
            MainWindow = new MainWindow();

            _mainViewModel = new(_navigationService);
            MainWindow.DataContext = _mainViewModel;

            ICommand loginCommand = new LoginCommand(() => new AdminHomeViewModel(), () => new MovieSelectionViewModel(), _navigationService);
            Func<LoginViewModel> loginViewModel = () => new(loginCommand);
            _navigationService.ChangeViewModel(loginViewModel());
            _navigationService.StartViewModel = loginViewModel;

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

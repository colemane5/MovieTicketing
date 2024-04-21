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

            List<ViewModelBase> adminViewModels = 
            [
                new AdminHomeViewModel(new User(101, "AdminUser", "person@admin.com")),
                new AdminStatsViewModel(),
                new AdminModifyTablesViewModel(),
                new AdminAddEditRowViewModel()
            ];
            List<ViewModelBase> clientViewModels =
            [
                new MovieSelectionViewModel(),
                new ShowtimeSelectionViewModel()
            ];

            ICommand loginCommand = new LoginCommand(adminViewModels, clientViewModels, _navigationService);
            LoginViewModel loginViewModel = new(loginCommand);
            _navigationService.AddViewModel(loginViewModel);
            _navigationService.ChangeViewModel<LoginViewModel>();
            _navigationService.StartViewModel = loginViewModel;

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

﻿using MovieTicketingAdmin.ViewModels;
using MovieTicketingApp.ViewModels;
using MovieTicketingApp.Views;
using SharedResources;
using SharedResources.Models;
using System.Configuration;
using System.Data;
using System.Windows;

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

            AdminHomeViewModel adminHome = new(new User(101, "AdminUser", "person@admin.com"));
            adminHome.RegisterNavigationService(_navigationService);
            _navigationService.AddViewModel(adminHome);
            _navigationService.AddViewModel(new AdminStatsViewModel());
            _navigationService.ChangeViewModel<AdminHomeViewModel>();

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

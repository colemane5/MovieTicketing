using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin.ViewModels
{
    public class AdminHomeViewModel : ViewModelBase
    {
        public User CurrentUser { get; }

        public ICommand NavigateStatsCommand { get; }
        public ICommand NavigateModifyTablesCommand { get; }

        public AdminHomeViewModel(NavigationService navigationService, User user)
        {
            NavigateStatsCommand = navigationService.CreateNavigationCommand<AdminStatsViewModel>();
            NavigateModifyTablesCommand = navigationService.CreateNavigationCommand<AdminModifyTablesViewModel>();
            CurrentUser = user;
        }
    }
}

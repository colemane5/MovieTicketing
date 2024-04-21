using SharedResources;
using SharedResources.Models;
using SharedResources.ViewModels;
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
        public string Name => CurrentUser.Name;

        public ICommand NavigateStatsCommand { get; }
        public ICommand NavigateModifyTablesCommand { get; }
        public ICommand LogoutCommand { get; }

        public AdminHomeViewModel(User user)
        {
            NavigateStatsCommand = Navigation<AdminStatsViewModel>();
            NavigateModifyTablesCommand = Navigation<AdminModifyTablesViewModel>();
            LogoutCommand = Logout();
            CurrentUser = user;
        }
    }
}

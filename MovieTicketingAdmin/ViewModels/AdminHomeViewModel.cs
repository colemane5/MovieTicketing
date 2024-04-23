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
        public string? Name => _user?.Name;
        public string? UserEmail => _user?.Email;

        public ICommand NavigateStatsCommand { get; }
        public ICommand NavigateModifyTablesCommand { get; }
        public ICommand LogoutCommand { get; }

        public AdminHomeViewModel()
        {
            NavigateStatsCommand = Navigation<AdminStatsViewModel>();
            NavigateModifyTablesCommand = Navigation<AdminModifyTablesViewModel>();
            LogoutCommand = Logout();
        }
    }
}

using SharedResources;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin.ViewModels
{
    public class AdminStatsViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }

        public AdminStatsViewModel() : base()
        {
            NavigateHomeCommand = Navigation<AdminHomeViewModel>();
        }
    }
}

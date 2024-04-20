using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingAdmin.ViewModels
{
    public class AdminStatsViewModel : ViewModelBase
    {
        public TopTheatersPanelViewModel TopTheatersPanelViewModel { get; }
        public TopGenresPanelViewModel TopGenresPanelViewModel { get; }

        public AdminStatsViewModel(TopTheatersPanelViewModel topTheatersPanelViewModel, TopGenresPanelViewModel topGenresPanelViewModel)
        {
            TopTheatersPanelViewModel = topTheatersPanelViewModel;
            TopGenresPanelViewModel = topGenresPanelViewModel;
        }
    }
}

using SharedResources.Results;
using SharedResources.ViewModels;
using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin.ViewModels
{
    public class TopGenresPanelViewModel : DateRangeViewModel
    {
        private readonly SqlAggregateQueryRepo _aggregateQueryRepo = new();
        private ObservableCollection<GenreRanksResult> _topGenres = [];
        public ObservableCollection<GenreRanksResult> TopGenres
        {
            get => _topGenres;
            private set
            {
                _topGenres = value;
                OnPropertyChanged(nameof(TopGenres));
            }
        }

        public TopGenresPanelViewModel() : base()
        {
            RefreshData();
        }

        public override void RefreshData()
        {
            TopGenres = new ObservableCollection<GenreRanksResult>(
                _aggregateQueryRepo.GetGenreRanks(From, To));
        }
    }
}

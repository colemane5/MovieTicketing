using SharedResources.Results;
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
            TopGenres =
            [
                new GenreRanksResult(1, "Action", 826771),
                new GenreRanksResult(2, "Drama", 733480),
                new GenreRanksResult(3, "Romance", 600132),
                new GenreRanksResult(4, "Science Fiction", 598293)
            ];
        }
    }
}

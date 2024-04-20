using SharedResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketingAdmin.ViewModels
{
    public class TopGenresPanelViewModel : ViewModelBase
    {
        public ObservableCollection<GenreRanksResult> TopGenres { get; private set; } = new();

        public TopGenresPanelViewModel()
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

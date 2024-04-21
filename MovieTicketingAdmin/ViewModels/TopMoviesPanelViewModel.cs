using SharedResources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin.ViewModels
{
    public class TopMoviesPanelViewModel : ViewModelBase
    {
        private ObservableCollection<TopMoviesResult> _topMovies = [];
        public ObservableCollection<TopMoviesResult> TopMovies
        {
            get => _topMovies;
            set
            {
                _topMovies = value;
                OnPropertyChanged(nameof(TopMovies));
            }
        }

        private ObservableCollection<TopMoviesResult> _topMoviesPage = null!;
        public ObservableCollection<TopMoviesResult> TopMoviesPage
        {
            get => _topMoviesPage;
            set
            {
                _topMoviesPage = value;
                OnPropertyChanged(nameof(TopMoviesPage));
            }
        }

        private int _page;
        public int Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));

                TopMoviesPage = new(TopMovies.Skip((_page - 1) * 4).Take(4));
            }
        }

        private string _pageValue = null!;
        public string PageValue
        {
            get => _pageValue;
            set
            {
                _pageValue = value;
                OnPropertyChanged(nameof(PageValue));
            }
        }

        public ICommand GoCommand { get; }

        public TopMoviesPanelViewModel()
        {
            TopMovies = 
            [
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
                new TopMoviesResult("Dune: Part Two", 163764, 2236, 73.24f),
            ];
            Page = 1;
            PageValue = "1";

            GoCommand = new RelayCommand(() => {
                if (int.TryParse(PageValue, out int pageValue))
                {
                    Page = pageValue;
                }
            });
        }
    }
}

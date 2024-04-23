using SharedResources.ViewModels;
using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using System.Windows.Input;
using SharedResources.Commands;
using System.Collections.ObjectModel;

namespace MovieTicketingClient.ViewModels
{
    public class ShowtimeSelectionViewModel : RefreshableViewModel, IMovieSelectable
    {
        //to use for data binding once viewmodel is developed
        private readonly SqlTheaterRepository theaterRepository = new();

        public Movie SelectedMovie { get; private set; }

        public string MovieTitle => SelectedMovie.Title;

        public List<Theater> AvailableTheaters { get; private set; }

        private Theater _selectedTheater;
        public Theater SelectedTheater
        {
            get => _selectedTheater;
            set
            {
                _selectedTheater = value;
                OnPropertyChanged(nameof(SelectedTheater));
                RefreshData();
            }
        }

        private ObservableCollection<Showtime> _availableShowtimes = [];
        public ObservableCollection<Showtime> AvailableShowtimes
        {
            get => _availableShowtimes;
            set
            {
                _availableShowtimes = value;
                OnPropertyChanged(nameof(AvailableShowtimes));
            }
        }

        private Showtime _selectedShowtime;
        public Showtime SelectedShowtime
        {
            get => _selectedShowtime;
            set
            {
                _selectedShowtime = value;
                OnPropertyChanged(nameof(SelectedShowtime));
            }
        }

        public ICommand ReserveShowtimeCommand { get; }
        public ICommand BackCommand { get; }

        public ShowtimeSelectionViewModel()
        {
            ReserveShowtimeCommand = new RelayCommand(ReserveSelectedShowtime);
            BackCommand = Navigation<MovieSelectionViewModel>();

            AvailableTheaters = theaterRepository.RetrieveTheaters(SelectedMovie.Id).ToList();
            AvailableShowtimes.CollectionChanged += (s, e) => OnPropertyChanged(nameof(AvailableShowtimes));
        }

        private void ReserveSelectedShowtime()
        {
            if (_user is User user)
            {
                //bool success = theaterRepository.GetTicket(user.Id, SelectedMovie.Id, SelectedShowtime.Price, SelectedShowtime.SeatsAvailable);
                /*if (success)*/ AvailableShowtimes.Remove(SelectedShowtime);
            }
        }

        public void SelectMovie(Movie movie)
        {
            SelectedMovie = movie;
            AvailableTheaters = theaterRepository.RetrieveTheaters(SelectedMovie.Id).ToList();
            OnPropertyChanged(nameof(AvailableTheaters));
        }

        public override void RefreshData()
        {
            AvailableShowtimes = new(theaterRepository.FindShowtimes(SelectedMovie.Id, SelectedTheater.Id));
        }
    }
}

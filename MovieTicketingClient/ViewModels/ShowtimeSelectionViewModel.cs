using SharedResources.ViewModels;
using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using System.Windows.Input;

namespace MovieTicketingClient.ViewModels
{
    public class ShowtimeSelectionViewModel : ViewModelBase
    {
        //to use for data binding once viewmodel is developed
        private readonly SqlTheaterRepository theaterRepository = new();

        public Movie SelectedMovie { get; }

        public string MovieTitle => SelectedMovie.Title;

        public List<Theater> AvailableTheaters { get; }

        private Theater _selectedTheater;
        public Theater SelectedTheater
        {
            get => _selectedTheater;
            set
            {
                _selectedTheater = value;
                OnPropertyChanged(nameof(SelectedTheater));
            }
        }

        private List<MovieShowtime> _availableShowtimes;
        public List<MovieShowtime> AvailableShowtimes
        {
            get => _availableShowtimes;
            set
            {
                _availableShowtimes = value;
                OnPropertyChanged(nameof(AvailableShowtimes));
            }
        }

        private MovieShowtime _selectedShowtime;
        public MovieShowtime SelectedShowtime
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
        }

        private void ReserveSelectedShowtime()
        {
            theaterRepository.GetTicket(_user.Id, SelectedMovie.Id, SelectedShowtime.Price, SelectedShowtime.SeatsAvailable);
        }
    }
}

using SharedResources.Commands;
using SharedResources.Results;
using SharedResources.ViewModels;
using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieTicketingAdmin.ViewModels
{
    public class TopTheatersPanelViewModel : ViewModelBase
    {
        private readonly SqlAggregateQueryRepo _aggregateQueryRepo = new();
        public int _month;
        public int Month
        {
            get => _month;
            set
            {
                string oldName = SelectedTheater.Name;

                _month = value;
                OnPropertyChanged(nameof(MonthName));

                TheaterSales = new(FullTheaterSales.Where(
                    s => s.Month == _month
                ));
                OnPropertyChanged(nameof(TheaterSales));

                NextActive = Month < 12;
                PreviousActive = Month > 1;
                OnPropertyChanged(nameof(NextActive));
                OnPropertyChanged(nameof(PreviousActive));

                SelectedTheater = TheaterSales.Where(s => s.Name == oldName).SingleOrDefault();
            }
        }
        public string MonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);

        private List<TopTheatersResult> FullTheaterSales { get; set; }
        public ObservableCollection<TopTheatersResult> TheaterSales { get; private set; } = new();

        public TopTheatersResult _selectedTheater;
        public TopTheatersResult SelectedTheater 
        {
            get => _selectedTheater;
            set
            {
                _selectedTheater = value;
                OnPropertyChanged(nameof(TheaterName));
                OnPropertyChanged(nameof(IndvTheaterSales));
                OnPropertyChanged(nameof(TheaterAddress));
            }
        }

        public string TheaterName => SelectedTheater.Name;
        public decimal IndvTheaterSales => SelectedTheater.Sales;
        public string TheaterAddress => SelectedTheater.Address;

        public bool NextActive { get; private set; }
        public bool PreviousActive { get; private set; }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }

        public TopTheatersPanelViewModel()
        {
            // Replace with call to database.
            FullTheaterSales = _aggregateQueryRepo.GetTopTheaters();

            NextCommand = new RelayCommand(() => { if (Month < 12) Month++; });
            PreviousCommand = new RelayCommand(() => { if (Month > 1) Month--; });

            Month = 1;
        }
    }
}

using SharedResources;
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
        public string IndvTheaterSales => SelectedTheater.Sales.ToString();
        public string TheaterAddress => SelectedTheater.Address;

        public bool NextActive { get; private set; }
        public bool PreviousActive { get; private set; }

        public ICommand NextCommand { get; }
        public ICommand PreviousCommand { get; }

        public TopTheatersPanelViewModel()
        {
            // Replace with call to database.
            FullTheaterSales =
            [
                new TopTheatersResult(1, 1, "AMC10", "Some Address", 12493582.72M),
                new TopTheatersResult(1, 2, "AMC15", "Some Address", 10672855.92M),
                new TopTheatersResult(1, 3, "AMC20", "Some Address", 35960287.68M),
                new TopTheatersResult(1, 4, "AMC30", "Some Address", 13495832.58M),
                new TopTheatersResult(2, 3, "AMC20", "Some Address", 359602.68M),
                new TopTheatersResult(2, 4, "AMC30", "Some Address", 134958.58M),
                new TopTheatersResult(2, 1, "AMC10", "Some Address", 124935.72M),
                new TopTheatersResult(2, 2, "AMC15", "Some Address", 106728.92M),
            ];

            NextCommand = new RelayCommand(() => { if (Month < 12) Month++; });
            PreviousCommand = new RelayCommand(() => { if (Month > 1) Month--; });

            Month = 1;
        }
    }
}

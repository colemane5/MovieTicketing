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
    public class HourlySalesPanelViewModel : ViewModelBase
    {
		private HourlySalesResult _selectedHour;
		public HourlySalesResult SelectedHour
		{
			get => _selectedHour;
			set
			{
				_selectedHour = value;
				OnPropertyChanged(nameof(SelectedHour));
				OnPropertyChanged(nameof(Sales));
				OnPropertyChanged(nameof(UniqueTitles));
				OnPropertyChanged(nameof(UniqueTheaters));
			}
		}

		public decimal Sales => SelectedHour.TicketSales;
		public decimal UniqueTitles => SelectedHour.MovieCount;
		public decimal UniqueTheaters => SelectedHour.TheaterCount;

		private ObservableCollection<HourlySalesResult> _hourlySales = [];
		public ObservableCollection<HourlySalesResult> HourlySales
		{
			get => _hourlySales;
			set
			{
				_hourlySales = value;
				OnPropertyChanged(nameof(HourlySales));
			}
		}

		public decimal MaxSale => HourlySales.Max(s => s.TicketSales);

		public ICommand SelectHourCommand { get; }

        public HourlySalesPanelViewModel()
        {
			HourlySales =
			[
				new HourlySalesResult(10, 134, 667, 11943854.12M),
                new HourlySalesResult(11, 345, 708, 18549235.12M),
                new HourlySalesResult(12, 212, 234, 73453422.12M),
                new HourlySalesResult(13, 856, 113, 54323342.23M),
                new HourlySalesResult(14, 363, 353, 34754234.12M),
                new HourlySalesResult(15, 737, 113, 56345234.25M),
                new HourlySalesResult(16, 226, 789, 67958204.36M),
                new HourlySalesResult(17, 671, 324, 43640929.69M),
                new HourlySalesResult(18, 831, 463, 70293812.96M),
                new HourlySalesResult(19, 363, 353, 34754234.12M),
                new HourlySalesResult(20, 737, 113, 56345234.25M),
                new HourlySalesResult(21, 134, 667, 11943854.12M),
            ];

			SelectHourCommand = new RelayCommand<HourlySalesResult>((s) => { SelectedHour = s; });
        }
    }
}

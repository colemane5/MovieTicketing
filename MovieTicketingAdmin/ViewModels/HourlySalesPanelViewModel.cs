using SharedResources.Commands;
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
    public class HourlySalesPanelViewModel : DateRangeViewModel
    {
		private readonly SqlAggregateQueryRepo _aggregateQueryRepo = new();
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

        public HourlySalesPanelViewModel() : base()
        {
			RefreshData();
			SelectHourCommand = new RelayCommand<HourlySalesResult>((s) => { SelectedHour = s; });
        }

		public override void RefreshData()
		{
            // Refresh data
            HourlySales = new ObservableCollection<HourlySalesResult>(_aggregateQueryRepo.GetSalesPerHourOfTheDay(
				new DateTimeOffset(2024, 4, 1, 12, 00, 00, DateTimeOffset.Now.Offset),
                new DateTimeOffset(2024, 4, 1, 23, 00, 00, DateTimeOffset.Now.Offset)));
        }
    }
}

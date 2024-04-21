using SharedResources.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.ViewModels
{
    public abstract class DateRangeViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        protected DateRangeViewModel()
        {
            RefreshCommand = new RelayCommand(RefreshData);
        }

        public abstract void RefreshData();
    }
}

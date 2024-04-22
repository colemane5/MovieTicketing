using SharedResources.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedResources.ViewModels
{
    public abstract class RefreshableViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; }

        protected RefreshableViewModel()
        {
            RefreshCommand = new RelayCommand(RefreshData);
        }

        public abstract void RefreshData();
    }
}

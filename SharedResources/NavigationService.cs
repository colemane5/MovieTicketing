using SharedResources.Commands;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class NavigationService
    {
        public event EventHandler? CurrentViewModelChanged;

        public ViewModelBase _currentViewModel = new();
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke(this, new EventArgs());
            }
        }

        private List<ViewModelBase> _storedViewModels = [];

        public void ChangeViewModel<T>()
        {
            CurrentViewModel = _storedViewModels.Where(s => s.GetType() == typeof(T)).SingleOrDefault() ?? new ViewModelBase();
        }

        public void AddViewModel(ViewModelBase viewModel)
        {
            _storedViewModels.Add(viewModel);
        }
    }
}

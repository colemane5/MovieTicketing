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

        private ViewModelBase? _startViewModel;
        public ViewModelBase? StartViewModel
        {
            get => _startViewModel;
            set
            {
                if (value is not null)
                {
                    _startViewModel = value;
                    _storedViewModels.Add(_startViewModel); 
                }
            }
        }

        private List<ViewModelBase> _storedViewModels = [];

        public void ChangeViewModel<T>()
        {
            CurrentViewModel = _storedViewModels.Where(s => s.GetType() == typeof(T)).SingleOrDefault() ?? new ViewModelBase();
        }

        public void ChangeViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = _storedViewModels.Where(vm => vm == viewModel).SingleOrDefault() ?? new ViewModelBase();
        }

        public void ReturnToStart()
        {
            if (StartViewModel is not null)
            {
                CurrentViewModel = StartViewModel; 
            }
        }

        public void AddViewModel(ViewModelBase viewModel)
        {
            _storedViewModels.Add(viewModel);
        }

        public void ClearViewModels()
        {
            _storedViewModels.Clear();
            if (StartViewModel is not null) 
                _storedViewModels.Add(StartViewModel);
        }
    }
}

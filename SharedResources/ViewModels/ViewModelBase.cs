using SharedResources.Commands;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected NavigationService? _navigationService;

        protected User? _user;

        /// <summary>
        /// Raises the PropertyChanged event for the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property to update.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RegisterNavigationService(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void RegisterUser(User? user)
        {
            _user = user;
        }

        protected NavigateCommand<T> Navigation<T>() where T : ViewModelBase, new()
        {
            return new NavigateCommand<T>(() => _navigationService);
        }

        protected LogoutCommand Logout()
        {
            return new LogoutCommand(() => _navigationService);
        }
    }
}

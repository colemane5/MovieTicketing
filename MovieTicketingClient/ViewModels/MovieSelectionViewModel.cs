using SharedResources.Models;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MovieTicketingClient.ViewModels
{
    public class MovieSelectionViewModel : ViewModelBase
    {
        private List<Movie> movies { get; set; }
        private List<string> genres { get; set; }
        private List<Actor> actors { get; set; }
        private List<Director> directors { get; set; }

        public ICommand LogoutCommand { get; }

        public MovieSelectionViewModel() : base() 
        {
            LogoutCommand = Logout();
        }
    }
}

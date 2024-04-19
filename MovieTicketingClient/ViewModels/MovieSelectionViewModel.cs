using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MovieTicketingClient.ViewModels
{
    internal class MovieSelectionViewModel
    {
        public List<Movie> movies { get; }


        public MovieSelectionViewModel() { }
    }
}

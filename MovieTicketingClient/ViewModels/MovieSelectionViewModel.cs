﻿using SharedResources.Models;
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
        private List<Movie> movies { get; set; }
        private List<string> genres { get; set; }
        private List<Actor> actors { get; set; }
        private List<Director> directors { get; set; }

        public MovieSelectionViewModel() { }
    }
}
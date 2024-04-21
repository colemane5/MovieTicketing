﻿using MovieTicketingAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieTicketingAdmin.Views
{
    /// <summary>
    /// Interaction logic for AdminStatsView.xaml
    /// </summary>
    public partial class AdminStatsView : UserControl
    {
        public AdminStatsView()
        {
            InitializeComponent();
            HourlySalesPanelView.DataContext = new HourlySalesPanelViewModel();
            TopGenresPanelView.DataContext = new TopGenresPanelViewModel();
            TopMoviesPanelView.DataContext = new TopMoviesPanelViewModel();
            TopTheatersPanelView.DataContext = new TopTheatersPanelViewModel();
            HourlySalesPanelView.BuildGraph();
        }
    }
}
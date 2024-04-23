using MovieTicketingClient.ViewModels;
using SharedResources.Models;
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

namespace MovieTicketingClient.Views
{
    /// <summary>
    /// Interaction logic for MovieSelectionView.xaml
    /// </summary>
    public partial class MovieSelectionView : UserControl
    {
        public MovieSelectionView()
        {
            InitializeComponent();
        }

        private void MovieSelected(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is not MovieSelectionViewModel vm) return;
            if (sender is not ListViewItem listViewItem) return;
            if (listViewItem.DataContext is not Movie movie) return;

            vm.SelectMovieCommand.Execute(movie); 
        }
    }
}

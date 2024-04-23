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
    /// Interaction logic for ShowtimeSelectionView.xaml
    /// </summary>
    public partial class ShowtimeSelectionView : UserControl
    {
        public ShowtimeSelectionView()
        {
            InitializeComponent();

            CollectionView showtimeCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(ShowtimesListView.ItemsSource);
            PropertyGroupDescription propertyGroup = new("StartDay");
            showtimeCollectionView.GroupDescriptions.Add(propertyGroup);
        }
    }
}

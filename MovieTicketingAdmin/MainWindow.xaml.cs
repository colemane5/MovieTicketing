using MovieTicketingAdmin.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieTicketingAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AdminStatsViewModel AdminStatsViewModel { get; private set; }
        public MainWindow()
        {
            TopTheatersPanelViewModel topTheaters = new();
            TopGenresPanelViewModel topGenres = new();
            AdminStatsViewModel = new AdminStatsViewModel(topTheaters, topGenres);
            InitializeComponent();
        }
    }
}
using MovieTicketingAdmin.ViewModels;
using SharedResources.Results;
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
    /// Interaction logic for HourlySalesPanel.xaml
    /// </summary>
    public partial class HourlySalesPanel : UserControl
    {
        private static readonly int _baseGraphHeight = 200;

        public HourlySalesPanel()
        {
            InitializeComponent();
        }

        public void BuildGraph()
        {
            if (DataContext is HourlySalesPanelViewModel vm)
            {
                HourGraph.Children.Clear();
                HourGraph.ColumnDefinitions.Clear();
                for (int i = 0; i < vm.HourlySales.Count; i++)
                {
                    HourGraph.ColumnDefinitions.Add(new ColumnDefinition());

                    HourlySalesResult hour = vm.HourlySales[i];
                    DockPanel container = new();

                    TextBlock hourLabel = new()
                    {
                        Text = ToAM_PM(hour.HourOfDay),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    DockPanel.SetDock(hourLabel, Dock.Bottom);
                    container.Children.Add(hourLabel);

                    Button buttonContainer = new();
                    ControlTemplate buttonInternal = new();
                    FrameworkElementFactory frameworkElement = new(typeof(Rectangle));
                    frameworkElement.SetValue(Shape.StretchProperty, Stretch.UniformToFill);
                    frameworkElement.SetValue(Shape.FillProperty, new SolidColorBrush(Colors.Gray));
                    frameworkElement.SetValue(HeightProperty, (double)(hour.TicketSales / vm.MaxSale) * _baseGraphHeight);
                    frameworkElement.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
                    frameworkElement.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                    buttonInternal.VisualTree = frameworkElement;
                    buttonContainer.Template = buttonInternal;
                    buttonContainer.Command = vm.SelectHourCommand;
                    buttonContainer.CommandParameter = hour;

                    DockPanel.SetDock(buttonContainer, Dock.Bottom);
                    container.Children.Add(buttonContainer);

                    Grid.SetColumn(container, i);
                    HourGraph.Children.Add(container);
                }
            }
        }

        private string ToAM_PM(int hour) => hour switch
        {
            (< 12) => $"{hour}AM",
            (12) => $"12PM",
            (< 24) => $"{hour - 12}PM",
            (24) => $"12AM",
            _ => "N/A"
        };

        private void HourGraph_SourceUpdated(object sender, DataTransferEventArgs e) => BuildGraph();
    }
}

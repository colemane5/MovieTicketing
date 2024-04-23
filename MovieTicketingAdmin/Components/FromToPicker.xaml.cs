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

namespace MovieTicketingAdmin.Components
{
    /// <summary>
    /// Interaction logic for FromToPicker.xaml
    /// </summary>
    public partial class FromToPicker : UserControl
    {
        public DateTime From
        {
            get => (DateTime)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public DateTime To
        {
            get => (DateTime)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }
        public ICommand DateChangedCommand
        {
            get => (ICommand)GetValue(DateChangedCommandProperty);
            set => SetValue(DateChangedCommandProperty, value);
        }

        private bool _active = false;

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            nameof(From), typeof(DateTime), typeof(FromToPicker), new PropertyMetadata(new DateTime(2024, 1, 1))
        );
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            nameof(To), typeof(DateTime), typeof(FromToPicker), new PropertyMetadata(DateTime.Now)
        );
        public static readonly DependencyProperty DateChangedCommandProperty = DependencyProperty.Register(
            nameof(DateChangedCommand), typeof(ICommand), typeof(FromToPicker), new PropertyMetadata(null)
        );

        public FromToPicker()
        {
            InitializeComponent();
        }

        private void HandleFromChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_active)
            {
                DateChangedCommand?.Execute(null); 
            }
        }

        private void HandleToChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_active)
            {
                DateChangedCommand?.Execute(null); 
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            _active = true;
        }
    }
}

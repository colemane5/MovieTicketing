﻿using MovieTicketingAdmin.ViewModels;
using SharedResources.Models;
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
        public User User { get; set; } = new(1, "AdminUser", "person@admin.com");
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
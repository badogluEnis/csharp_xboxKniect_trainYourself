﻿using System;
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

namespace Trainyourself.Pages
{
    /// <summary>
    /// Interaction logic for HauptmenuPage.xaml
    /// </summary>
    public partial class HauptmenuPage : Page
    {
        public HauptmenuPage()
        {
            InitializeComponent();
        }

        private void situp_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SigninPage());
        }

        private void Buttonpushups_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SigninPage());
        }

        private void Profilbutton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
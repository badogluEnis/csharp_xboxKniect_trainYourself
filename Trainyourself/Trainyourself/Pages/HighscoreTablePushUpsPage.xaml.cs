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
using DataAccess;
using Model;

namespace Trainyourself.Pages
{
    /// <summary>
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Page
    {
        public Highscore()
        {
            InitializeComponent();
            filllabels();
        }
        public void filllabels()
        {
            using (TrainContext context = new TrainContext())
            {
                UserRepository userRepository = new UserRepository(context);

                erstername.Content = "1. " + userRepository.Top4ListPushUps()[0].Name;
                ersterscore.Text = Convert.ToString(userRepository.Top4ListPushUps()[0].RecordPushups);

                zweitername.Content = "2. " + userRepository.Top4ListPushUps()[1].Name;
                zweiterscore.Text = Convert.ToString(userRepository.Top4ListPushUps()[1].RecordPushups);

                drittertername.Content = "3. " + userRepository.Top4ListPushUps()[2].Name;
                dritterscore.Text = Convert.ToString(userRepository.Top4ListPushUps()[2].RecordPushups);

                viertername.Content = "4. " + userRepository.Top4ListPushUps()[3].Name;
                vierterscore.Text = Convert.ToString(userRepository.Top4ListPushUps()[3].RecordPushups);
            }

        }

        private void Backbutton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }
    }
}

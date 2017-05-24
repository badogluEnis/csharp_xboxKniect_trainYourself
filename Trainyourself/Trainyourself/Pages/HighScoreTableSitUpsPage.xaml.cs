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
    /// Interaction logic for HighScoreTableSitUpsPage.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Page" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class HighScoreTableSitUpsPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreTableSitUpsPage"/> class.
        /// </summary>
        public HighScoreTableSitUpsPage()
        {
            InitializeComponent();
            Filllabels();
        }
        /// <summary>
        /// Filllabelses this instance.
        /// </summary>
        public void Filllabels()
        {
            using (TrainContext context = new TrainContext())
            {
                UserRepository userRepository = new UserRepository(context);

                erstername.Content = "1. " + userRepository.Top4ListSitUps()[0].Name;
                ersterscore.Text = Convert.ToString(userRepository.Top4ListSitUps()[0].RecordSitups);

                zweitername.Content = "2. " + userRepository.Top4ListSitUps()[1].Name;
                zweiterscore.Text = Convert.ToString(userRepository.Top4ListSitUps()[1].RecordSitups);

                drittertername.Content = "3. " + userRepository.Top4ListSitUps()[2].Name;
                dritterscore.Text = Convert.ToString(userRepository.Top4ListSitUps()[2].RecordSitups);

                viertername.Content = "4. " + userRepository.Top4ListSitUps()[3].Name;
                vierterscore.Text = Convert.ToString(userRepository.Top4ListSitUps()[3].RecordSitups);
            }

        }

        /// <summary>
        /// Handles the OnClick event of the Backbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Backbutton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }
    }
}
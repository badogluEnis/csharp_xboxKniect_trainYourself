﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using DataAccess;
using KinectConnection;
using Microsoft.Kinect;
using Model;

namespace Trainyourself.Pages
{
    /// <summary>
    /// Logic for LiveviewPushups.xaml. Load the Camera to the Image. Works with the Coordinates from the Kinecktsensor. Count for each Pushup. Database Update.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Page" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class LiveviewPushups
    {
        /// <summary>
        /// The shoulder left x
        /// </summary>
        public float ShoulderLeftX;
        /// <summary>
        /// The shoulder left y
        /// </summary>
        public float ShoulderLeftY;
        /// <summary>
        /// The shoulder right x
        /// </summary>
        public float ShoulderRightX;
        /// <summary>
        /// The shoulder right y
        /// </summary>
        public float ShoulderRightY;
        /// <summary>
        /// The shoulder right z
        /// </summary>
        public float ShoulderRightZ;
        /// <summary>
        /// The shoulder left z
        /// </summary>
        public float ShoulderLeftZ;
        /// <summary>
        /// The criteriumdown
        /// </summary>
        private const float CRITERIUMDOWN = 0.24f;
        /// <summary>
        /// The criteriumup
        /// </summary>
        private const float CRITERIUMUP = 0.18f;
        /// <summary>
        /// The cal
        /// </summary>
        Calibration cal = new Calibration();
        /// <summary>
        /// The counter
        /// </summary>
        public int Counter;
        /// <summary>
        /// The war unten
        /// </summary>
        public bool WarUnten;
        /// <summary>
        /// The kinect provider
        /// </summary>
        private KinectProvider _kinectProvider = new KinectProvider();

        /// <summary>
        /// Initializes a new instance of the <see cref="LiveviewPushups" /> class.
        /// </summary>
        public LiveviewPushups()
        {
            InitializeComponent();
            Image.Source = _kinectProvider._colorBitmap;
            _kinectProvider.PositionChanged += SkeletonChanged;
            SetHighscore();
        }

        /// <summary>
        /// Gets the Coordinates from the Kinecktsensor. Calibrats.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="skeleton">The skeleton.</param>
        private void SkeletonChanged(object sender, Skeleton skeleton)
        {
            ShoulderLeftX = skeleton.Joints[JointType.ShoulderLeft].Position.X;
            ShoulderLeftY = skeleton.Joints[JointType.ShoulderLeft].Position.Y;
            ShoulderLeftZ = skeleton.Joints[JointType.ShoulderLeft].Position.Z;
            ShoulderRightX = skeleton.Joints[JointType.ShoulderRight].Position.X;
            ShoulderRightY = skeleton.Joints[JointType.ShoulderRight].Position.Y;
            ShoulderRightZ = skeleton.Joints[JointType.ShoulderRight].Position.Z;


            if (cal.IsCalibrated)
            {
                CalLabel.Visibility = Visibility.Hidden;
                CheckCount(skeleton);
                CheckUp(skeleton);
            }
            else
            {
                CalLabel.Content = "CALIBRATING";
                CalLabel.Foreground = new SolidColorBrush(Colors.Orange);
                CalLabel.Background = new SolidColorBrush(Colors.Black);
                cal.Calibrate(skeleton);

            }
        }



        /// <summary>
        /// Handles the OnClick event of the QuitButton control Navigate to Hauptmenupage and creates a new score in the Database.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void QuitButton_OnClick(object sender, RoutedEventArgs e)
        {
            _kinectProvider.Dispose();
            if (NavigationService != null) NavigationService.Navigate(new HauptmenuPage());
            Score score = new Score();
            using (TrainContext context = new TrainContext())
            {
                UserRepository userRepository = new UserRepository(context);
                score.UserID = userRepository.GetById(Int32.Parse(ConfigurationManager.AppSettings["LoggedUserId"])).Id;
                score.Date = DateTime.Now.Date;
                score.Exercise_Id = 1;
                score.Score1 = Counter;
                context.Scores.Add(score);
                context.SaveChanges();
            }


        }

        /// <summary>
        /// Checks if the User made a Unit so it can count up.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public void CheckCount(Skeleton skeleton)
        {
            if (ShoulderRightY < skeleton.Joints[JointType.HandRight].Position.Y + CRITERIUMDOWN &&
                ShoulderLeftY < skeleton.Joints[JointType.HandLeft].Position.Y + CRITERIUMDOWN &&
                !WarUnten &&
                skeleton.Joints[JointType.HandRight].Position.Y < ShoulderRightY &&
                skeleton.Joints[JointType.HandLeft].Position.Y < ShoulderLeftY)
            {
                Counter = Counter + 1;
                Currentrun.Content = $"Current Run: {Counter}";
                using (TrainContext context = new TrainContext())
                {
                    UserRepository userRepository = new UserRepository(context);
                    User user = userRepository.GetById(Int32.Parse(ConfigurationManager.AppSettings["LoggedUserId"]));
                    if (user.RecordPushups == null)
                    {
                        user.RecordPushups = Counter;
                        Record.Content = $"Record: {user.RecordPushups}";
                    }
                    if (Counter > user.RecordPushups)
                    {
                        user.RecordPushups = Counter;
                        Record.Content = $"Record: {user.RecordPushups}";
                    }

                    context.Users.Attach(user);
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
                WarUnten = true;
                Debug.WriteLine("DOWN");
            }
        }

        /// <summary>
        /// Checks up.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        public void CheckUp(Skeleton skeleton)
        {
            if (ShoulderRightY > cal.ShoulderHandDistanceRight - CRITERIUMUP && ShoulderLeftY > cal.ShoulderHandDistanceLeft - CRITERIUMUP)
            {
                WarUnten = false;
                Debug.WriteLine("UP");
            }

        }

        /// <summary>
        /// Sets the highscore.
        /// </summary>
        public void SetHighscore()
        {
            using (TrainContext context = new TrainContext())
            {
                UserRepository userRepository = new UserRepository(context);
                User user = userRepository.GetById(Int32.Parse(ConfigurationManager.AppSettings["LoggedUserId"]));
                Record.Content = $"Record: {user.RecordPushups}";
            }
        }
    }
}

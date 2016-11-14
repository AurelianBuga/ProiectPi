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
using System.Timers;
using System.ComponentModel;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for TimerPage.xaml
    /// </summary>
    public partial class TimerPage : Page , INotifyPropertyChanged
    {
        private Timer aTimer;
        private int hours;
        private int minutes;
        private int seconds;

        

        public  int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public int Seconds
        {
            get { return seconds; }
            set
            {
                seconds = value;
                OnPropertyChanged("Seconds");
            }
        }

        private void SetTimer()
        {
            // interval 1 sec
            aTimer = new Timer(1000);
            // Hook up the Elapsed event for the timer.
            aTimer.Start();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(Seconds == 0)
            {
                if(Minutes == 0)
                {
                    if(Hours == 0)
                    {
                        //END ---> ALARM
                        aTimer.Stop();
                        MessageBox.Show("ALERT");
                    }
                    else
                    {
                        Hours--;
                        Seconds = 59;
                    }
                }
                else
                {
                    Minutes--;
                    Seconds = 59;
                }
            }
            else
            {
                Seconds--;
            }
        }

        public TimerPage(int hours , int minutes , int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            
            InitializeComponent();
            SetTimer();
        }

        #region 
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(property));

            }
        }

        #endregion

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            pauseButton.Visibility = Visibility.Hidden;
            resumeButton.Visibility = Visibility.Visible;
        }

        private void resumeButton_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Start();
            resumeButton.Visibility = Visibility.Hidden;
            pauseButton.Visibility = Visibility.Visible;
        }
    }
}

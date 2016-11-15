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
using System.Timers;
using System.ComponentModel;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for Rule2020TimerPage.xaml
    /// </summary>
    public partial class Rule2020TimerPage : Page , INotifyPropertyChanged
    {
        private Frame mainFrame;
        private Timer aTimer;
        private int minutes;
        private int seconds;

        public Rule2020TimerPage(ref Frame mainFrame)
        {
            this.minutes = 20;
            this.seconds = 0;
            this.mainFrame = mainFrame;

            InitializeComponent();
            SetTimer();
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
            aTimer = new Timer(1);
            // Hook up the Elapsed event for the timer.
            aTimer.Start();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (Seconds == 0)
            {
                if (Minutes == 0)
                {
                        //END ---> ALARM
                        aTimer.Stop();
                    MessageBox.Show("ALERT"); // mesaj + timer de 20 de sec
                    textBlock2.Margin = new Thickness((double)64 , (double)141 , (double)0 , (double)-55);
                    textBlock3.Margin = new Thickness(221, 141, 0, -55);
                    textBlock4.Margin = new Thickness(267, 141, 0, -55);
                    pauseButton.Visibility = Visibility.Hidden;
                    resumeButton.Visibility = Visibility.Hidden;
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

        private void ClassicModeButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new TimerPage(0, 20, 0 , ref mainFrame));
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
    }
}

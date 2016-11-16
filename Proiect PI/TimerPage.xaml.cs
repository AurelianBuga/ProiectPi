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
using System.Windows.Threading;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for TimerPage.xaml
    /// </summary>
    public partial class TimerPage : Page , INotifyPropertyChanged
    {
        private DispatcherTimer timer;
        private int hours;
        private int minutes;
        private int seconds;
        private Frame mainFrame;

        

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
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(Seconds == 0)
            {
                if(Minutes == 0)
                {
                    if(Hours == 0)
                    {
                        //END ---> ALARM
                        timer.Stop();
                        System.Media.SystemSounds.Asterisk.Play();
                        MessageBox.Show("ALERT");
                        resumeButton.Visibility = Visibility.Visible;
                        pauseButton.Visibility = Visibility.Hidden;
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

        public TimerPage(int hours , int minutes , int seconds , ref Frame mainFrame)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            this.mainFrame = mainFrame;
            
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
            timer.Stop();
            pauseButton.Visibility = Visibility.Hidden;
            resumeButton.Visibility = Visibility.Visible;
        }

        private void resumeButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            resumeButton.Visibility = Visibility.Hidden;
            pauseButton.Visibility = Visibility.Visible;
        }

        private void Rule2020Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Rule2020TimerPage(ref mainFrame));
        }

        private void EditTimer(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new TimerSetPage(ref mainFrame));
        }
    }
}

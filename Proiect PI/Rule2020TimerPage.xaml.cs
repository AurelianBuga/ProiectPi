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
    /// Interaction logic for Rule2020TimerPage.xaml
    /// </summary>
    public partial class Rule2020TimerPage : Page , INotifyPropertyChanged
    {
        private Frame mainFrame;
        private DispatcherTimer timer;
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
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Seconds == 0)
            {
                if (Minutes == 0)
                {
                    //END ---> ALARM
                    timer.Stop();
                    //System.Media.SystemSounds.Asterisk.Play();
                    System.Media.SystemSounds.Beep.Play();
                    mainFrame.Navigate(new MessageCountDown2020Rule(ref mainFrame));
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

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
    /// Interaction logic for MessageCountDown2020Rule.xaml
    /// </summary>
    public partial class MessageCountDown2020Rule : Page, INotifyPropertyChanged
    {
        private int seconds;
        private Frame mainFrame;
        private DispatcherTimer timer;

        public MessageCountDown2020Rule(ref Frame mainFrame)
        {
            this.seconds = 20;
            InitializeComponent();
            SetTimer();
            this.mainFrame = mainFrame;
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
                timer.Stop();
                System.Media.SystemSounds.Asterisk.Play();
                mainFrame.Navigate(new Rule2020TimerPage(ref mainFrame));
                
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

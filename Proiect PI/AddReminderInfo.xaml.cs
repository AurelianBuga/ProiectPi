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
using System.Windows.Shapes;
using UserManager;
using DataManager;
using Components;
using MySql.Data.Types;
using System.ComponentModel;
using System.Globalization;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for AddReminderInfo.xaml
    /// </summary>
    public partial class AddReminderInfo : Window , INotifyPropertyChanged
    {
        private int hour;
        private int minute;
        private string AMPM;
        private DateTime dateDMY;
        private Frame currentFrame;


        public static  bool isOpn {get; set; }


        public AddReminderInfo(ref Frame currentFrame)
        {
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            this.currentFrame = currentFrame;
            AMPM = DateTime.Now.ToString("tt", CultureInfo.InvariantCulture);

            InitializeComponent();
            DataContext = this;

            isOpn = true;
        }

        public int CurrentHour
        {
            get
            {
                if (hour > 12)
                {
                    return hour - 12;
                }
                else
                {
                    return hour;
                }

            }
            set
            {
                if (value > 0)
                {
                    hour = value;
                }

                OnPropertyChanged("CurrentHour");
            }
        }

        public int CurrentMinute
        {
            get
            {
                if (minute >= 60)
                {
                    minute = minute - 60;
                }

                return minute;
            }
            set
            {
                if (value >= 0)
                {
                    minute = value;
                }

                OnPropertyChanged("CurrentMinute");
            }
        }

        public string AMPMPr
        {
            get { return AMPM; }
            set
            {
                AMPM = value;
                OnPropertyChanged("AMPMPr");
            }
        }


        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private MySqlDateTime GetFullDateMySqlFormat()
        {
            MySqlDateTime fullDate = new MySqlDateTime();
            fullDate.Second = 0;
            fullDate.Minute = this.minute;
            fullDate.Hour = this.hour;
            fullDate.Day = this.dateDMY.Day;
            fullDate.Month = this.dateDMY.Month;
            fullDate.Year = this.dateDMY.Year;

            return fullDate;
        }

        private void DatePicker_SelectedDateChanged(object sender,SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                
            }
            else
            {
                //MessageBox.Show(date.Value.ToShortDateString());
                 this.dateDMY = date.Value;
            }
        }

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {
            if (User.UserInstance.loginType)
            {
                //se apeleaza metoda InsertComponent(reminder)
                Reminder reminder = new Reminder(User.UserInstance.UID, ReminderTextBox.Text, GetFullDateMySqlFormat(), DBManager.Count( "reminder"));
                DBManager.InsertComponent(reminder);
                currentFrame.Navigate(new ReminderListView());
                isOpn = false;
                this.Close();
            }
            else
            {
                //se apeleaza metoda InsertComponent(reminderElement, uid , password)
                //TDO : generator de Id-uri pt componente 
                Reminder reminder = new Reminder(User.UserInstance.UID, ReminderTextBox.Text, GetFullDateMySqlFormat(), XMLManager.NrRem);
                reminder.SetID();
                XMLManager.XMLManagerInstance.InsertComponent(reminder);
                currentFrame.Navigate(new ReminderListView());
                isOpn = false;
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            isOpn = false;

            base.OnClosing(e);
        }

        private void ReminderTextClearTextBox(object sender, RoutedEventArgs e)
        {
            ReminderTextBox.Text = String.Empty;
        }

        private void Increase_Hours(object sender, RoutedEventArgs e)
        {
            CurrentHour++;
        }

        private void Decrease_Hours(object sender, RoutedEventArgs e)
        {
            CurrentHour--;
        }

        private void Increase_Minutes(object sender, RoutedEventArgs e)
        {
            CurrentMinute++;
        }

        private void Decrease_Minutes(object sender, RoutedEventArgs e)
        {
            CurrentMinute--;
        }

        private void Switch_AMPM(object sender, RoutedEventArgs e)
        {
            if (AMPMPr == "AM")
            {
                AMPMPr = "PM";
            }
            else
            {
                AMPMPr = "AM";
            }
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

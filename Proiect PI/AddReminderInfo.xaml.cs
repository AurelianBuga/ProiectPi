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

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for AddReminderInfo.xaml
    /// </summary>
    public partial class AddReminderInfo : Window
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

            if(hour > 12)
            {
                AMPM = "PM";
            }
            else
            {
                AMPM = "AM";
            }

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
        }

        public string CurrentMinute
        {
            get
            {
                if(minute == 0)
                {
                    return "00";
                }
                else
                {
                    return minute.ToString();
                }
                
            }
        }

        public string AMPMPr
        {
            get { return AMPM; }
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
                Reminder reminder = new Reminder(User.UserInstance.UID, ReminderTextBox.Text, GetFullDateMySqlFormat(), DBManager.Count(User.UserInstance.UID, "reminder"));
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
                XMLManager.InsertComponent(reminder, User.UserInstance.UID, User.UserInstance.Pasword);
                currentFrame.Navigate(new ReminderListView());
                isOpn = false;
                this.Close();
            }
        }

        private void ReminderTextClearTextBox(object sender, RoutedEventArgs e)
        {
            ReminderTextBox.Text = String.Empty;
        }

    }
}

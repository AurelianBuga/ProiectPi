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


        public AddReminderInfo()
        {
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

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

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {
            Reminder reminder = new Reminder();


            if (User.UserInstance.loginType)
            {
                //se apeleaza metoda InsertComponent(reminder)
                DBManager.InsertComponent(reminder);
            }
            else
            {
                //se apeleaza metoda InsertComponent(reminderElement, uid , password)
                XMLManager.InsertComponent(reminder, User.UserInstance.UID, User.UserInstance.Pasword);
            }
        }

        private void ReminderTextClearTextBox(object sender, RoutedEventArgs e)
        {
            ReminderTextBox.Text = String.Empty;
        }
    }
}

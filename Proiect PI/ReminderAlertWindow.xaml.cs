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
using Components;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for ReminderAlertWindows.xaml
    /// </summary>
    public partial class ReminderAlertWindow : Window
    {
        private Reminder reminder;

        public ReminderAlertWindow(ref Reminder reminder)
        {
            this.reminder = reminder;
            InitializeComponent();
        }

        public string ReminderText
        {
            get { return reminder.Text; }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            AddReminderInfo editWindow = new AddReminderInfo(ref reminder);
            editWindow.Show();
            this.Close();
        }

        private void snoozeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void doneButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.User.UserInstance.loginType)
            {
                DataManager.DBManager.DeleteComponent(reminder.IdComp, "reminder", "REMINDERID");
                MainWindow.MainWIndowInstance.ReminderList.LoadList();
                this.Close();
            }
            else
            {

            }
        }
    }
}

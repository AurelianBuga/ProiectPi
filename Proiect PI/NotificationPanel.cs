using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components;
using System.Windows.Threading;

namespace Proiect_PI
{
    class NotificationPanel
    {
        private readonly static NotificationPanel notificationPanelInstance = new NotificationPanel();
        private DispatcherTimer timer;

        private List<Reminder> reminders;
        private List<ToDo> toDos;

        private NotificationPanel()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += new EventHandler(Verify);
            timer.Start();
        }

        public List<Reminder> Reminders
        {
            get { return reminders; }
            set { reminders = value; }
        }

        public List<ToDo> ToDos
        {
            get { return toDos; }
            set { toDos = value; }
        }

        public static NotificationPanel NotificationPanelInstance
        {
            get
            {
                return notificationPanelInstance;
            }
        }

        //for reminders
        public void Verify(object sender, EventArgs e)
        {
            MySql.Data.Types.MySqlDateTime currTime = new MySql.Data.Types.MySqlDateTime(DateTime.Now);

            for (int i = 0; i < reminders.Count; i++)
            {
                if((reminders[i].Date.Day == currTime.Day) && (reminders[i].Date.Hour == currTime.Hour) && (reminders[i].Date.Minute == currTime.Minute))
                {
                    Reminder reminder = reminders[i];
                    ReminderAlertWindow alert = new ReminderAlertWindow(ref reminder);
                    System.Media.SystemSounds.Beep.Play();
                    alert.Show();
                }
            }
        }
    }
}

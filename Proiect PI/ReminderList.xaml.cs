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
using Components;
using System.Collections.ObjectModel;
using DataManager;
using UserManager;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for ReminderList.xaml
    /// </summary>
    public partial class ReminderListView : Page
    {
        readonly ObservableCollection<Reminder> reminders = new ObservableCollection<Reminder>();

        public ReminderListView()
        {
            InitializeComponent();
            DataContext = this;
            LoadList();
        }

        public ObservableCollection<Reminder> ListRem
        {
            get { return reminders; }
        }

        public void LoadList()
        {
            ListRem.Clear();
            var listRem = new List<Reminder>();
            if (User.UserInstance.loginType)
            {
                if (DBManager.OpenConnection())
                {
                    listRem = DBManager.GetRemList( User.UserInstance.UID);
                    foreach(Reminder rem in listRem)
                    {
                        reminders.Add(rem);
                    }
                }
            }
            else
            {
                //listRem = XMLManager.GetComponentListOneType(1, User.UserInstance.UID);
            }
        }
    }
}

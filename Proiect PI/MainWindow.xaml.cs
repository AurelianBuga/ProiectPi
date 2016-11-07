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
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Net;
using DataManager;
using UserManager;


namespace Proiect_PI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewReminderList(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReminderListView());
        }

        private void ViewNoteList(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new NoteListView());
        }

        private void ViewToDoList(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ToDoListView());
        }

        private void ViewLinkList(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LinkListView());
        }

        private void ViewTimer(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TimerPage());
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //TDO ... sa verifice ce page este deschis in frame ... si in funtie de asta se decida e fel de obiect este adaugat in lista
            if(MainFrame.Content is ReminderListView)
            {
                if (User.UserInstance.loginType)
                {
                    //online

                }
                else
                {
                    //offline

                }
            }
            else if(MainFrame.Content is NoteListView)
            {

            }
            else if(MainFrame.Content is ToDoListView)
            {

            }
            else if(MainFrame.Content is LinkListView)
            {

            }
            else if(MainFrame.Content is TimerPage)
            {

            }
            else
            {
                MessageBox.Show("Please click on one element type in the left column.");
            }
        }
    }
}


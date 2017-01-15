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
using Components;
using MySql.Data.Types;


namespace Proiect_PI
{
    public partial class MainWindow : Window
    {
        private readonly static MainWindow mainWIndowInstance = new MainWindow();

        private ReminderListView reminderList;
        private NoteListView noteList;
        private ToDoListView toDoList;
        private LinkListView linkList;
        private TimerSetPage timerSetPage; 

        private MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new WelcomePage());
            reminderList = new ReminderListView();
            noteList = new NoteListView();
            toDoList = new ToDoListView();
            linkList = new LinkListView();
            timerSetPage = new TimerSetPage(ref MainFrame);
        }

        public static MainWindow MainWIndowInstance
        {
            get
            {
                return mainWIndowInstance;
            }
        }

        public ReminderListView ReminderList
        {
            get { return reminderList; }
        }

        private void ViewReminderList(object sender, RoutedEventArgs e)
        {
                MainFrame.Navigate(reminderList);   
        }

        private void ViewNoteList(object sender, RoutedEventArgs e)
        {
                MainFrame.Navigate(noteList);
        }

        private void ViewToDoList(object sender, RoutedEventArgs e)
        {
                MainFrame.Navigate(toDoList);
        }

        private void ViewLinkList(object sender, RoutedEventArgs e)
        {
                MainFrame.Navigate(linkList);
        }

        private void ViewTimer(object sender, RoutedEventArgs e)
        {
            if ((!(MainFrame.Content is TimerSetPage)) && (!(MainFrame.Content is TimerPage)) && (!(MainFrame.Content is MessageCountDown2020Rule)) && (!(MainFrame.Content is Rule2020TimerPage)))
            {
                MainFrame.Navigate(timerSetPage);
            }
        }

        private void OpnAddReminderInfo(object sender, RoutedEventArgs e)
        {
            if (!AddReminderInfo.isOpn)
            {
                AddReminderInfo remWindow = new AddReminderInfo(/*ref MainFrame ,*/ ref reminderList);
                remWindow.Show();
            }
        }

        private void OpnAddNoteInfo(object sender, RoutedEventArgs e)
        {
            if (!AddNoteInfo.isOpn)
            {
                AddNoteInfo noteWindow = new AddNoteInfo(ref MainFrame);
                noteWindow.Show();
            }
        }

        private void OpnAddToDoInfo(object sender, RoutedEventArgs e)
        {
            if (!AddToDoInfo.isOpn)
            {
                AddToDoInfo toDoWindow = new AddToDoInfo(ref MainFrame);
                toDoWindow.Show();
            }
        }

        private void OpnAddLinkInfo(object sender, RoutedEventArgs e)
        {
            if (!AddLinkInfo.isOpn)
            {
                AddLinkInfo linkWindow = new AddLinkInfo(ref MainFrame);
                linkWindow.Show();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //TDO ... sa verifice ce page este deschis in frame ... si in funtie de asta se decida e fel de obiect este adaugat in lista
            if(MainFrame.Content is ReminderListView)
            {
                //apare window pentru introducerea datelor
                if (!AddReminderInfo.isOpn)
                {
                    AddReminderInfo remWindow = new AddReminderInfo(/*ref MainFrame ,*/ ref reminderList);
                    remWindow.Show();
                }  
            }
            else if(MainFrame.Content is NoteListView)
            {
                //apare window pentru introducerea datelor
                if (!AddNoteInfo.isOpn)
                {
                    AddNoteInfo noteWindow = new AddNoteInfo(ref MainFrame);
                    noteWindow.Show();
                }  
            }
            else if(MainFrame.Content is ToDoListView)
            {
                //apare window pentru introducerea datelor
                if (!AddToDoInfo.isOpn)
                {
                    AddToDoInfo toDoWindow = new AddToDoInfo(ref MainFrame);
                    toDoWindow.Show();
                }
            }
            else if(MainFrame.Content is LinkListView)
            {
                //apare window pentru introducerea datelor
                if (!AddLinkInfo.isOpn)
                {
                    AddLinkInfo linkWindow = new AddLinkInfo(ref MainFrame);
                    linkWindow.Show();
                }
            }
            else if(MainFrame.Content is TimerPage)
            {
                //se inlocuieste page-ul curent cu un page de setare timer    
                MainFrame.Navigate(new TimerSetPage(ref MainFrame));
            }
            else
            {
                MessageBox.Show("Please click on one element type in the left column.");
            }
        }

        private void RefreshTimerPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TimerSetPage(ref MainFrame));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


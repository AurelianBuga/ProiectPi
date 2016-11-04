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

/// <summary>
/// IDEA: -sa se salveze automat in XML-uri pt a facilita solosirea programului si offline 
/// IDEA: - sa adaug contacte (nr de telefon , adrese de email)
/// </summary>

public static partial class Helper
{
    public static bool IsSpace(char ch)
    {
        return (ch == ' ');
    }

    public static string GetPreviewText(string text , int writeableSpace)
    {
        if(text.Length > writeableSpace)
        {
            text = text.Substring(0, writeableSpace-3) + "...";
        }

        return text;
    }

    public static string GetFirstWord(string text , int writeSpace)
    {
        if (text.Length > writeSpace)
        {
            text = text.Substring(0, writeSpace-3) + "...";
        }
        else
        {
            text = text.Substring(0, text.IndexOf(' ', 0));
        }
        return text;
    }

    public static DateTime GetDatePreview(DateTime dateAndTime)
    {
        //TDO
        return dateAndTime;
    }


    /*public static int GetNrOrdine(string type , int uid)
    {
        if (UserManager.User.LoginType)
        {
            //daca aplicatie este rulata online 
            DB.DBConnection conn = new DB.DBConnection();
            return conn.Count(uid, type.ToLower()) + 1;
        }
        else
        {
            //daca aplicatie este rulata offline
            //TDO
            return 0;
        }
    }*/
}

namespace DB
{
    public class DBConnection
    {
        public MySql.Data.MySqlClient.MySqlConnection conn;
        public bool connStatus;
        

        public DBConnection()
        {
            string strServer = "localhost";
            string strDatabase = "proiect";
            string strUserID = "root"; 
            string strPassword = "112aurelian"; 
            string strconn = "Server=" + strServer +";Uid="+strUserID+";Pwd="+strPassword+";Database="+strDatabase+";";
            conn = new MySql.Data.MySqlClient.MySqlConnection(strconn);
            connStatus = false;
        }


        public bool OpenConnection() // Open database Connection
        {
            try
            {
                if(connStatus == true)
                {
                    return true;
                }
                else
                {
                    conn.Open();
                    connStatus = true;
                    return true;
                }
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection() // database connection close
        {
            try
            {
                conn.Close();
                connStatus = false;
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<string>[] GetOneTypeComponentList(Components.Component.component comp, int UID)
        {
            List<string>[] list = new List < string >[4];
            for (int i = 0; i < 4; i++)
                list[i] = new List<string>();

            switch (comp)
            {
                case Components.Component.component.reminder:
                    list = GetComponentList3("reminder", "NRORD", "REMINDERTEXT", "DATEANDTIME" , UID.ToString());
                    break;

                case Components.Component.component.note:
                    list = GetComponentList4("note", "NRORD", "NOTETITLE", "NOTETEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.component.todo:
                    list = GetComponentList4("todo", "NRORD", "STATUSCHECK", "TODOTEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.component.link:
                    list = GetComponentList4("link", "NRORD", "TEXT", "LINKTEXT", "DATEANDTIME" , UID.ToString());
                    break;

                case Components.Component.component.timer:
                    list = GetComponentList4("timer", "TIMERTEXT", "HOURS", "MINUTES", "SECONDS", UID.ToString());
                    break;
            }
            return list;
        }

        public List<string>[] GetComponentList4(string tip, string col1, string col2, string col3, string col4 , string UID)
        {
            List<string>[] list = new List<string>[4];

            for (int i = 0; i < 4; i++)
                list[i] = new List<string>();

            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM "+ tip +" WHERE USERID = " + UID;
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySql.Data.MySqlClient.MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[col1] + "");
                    list[1].Add(dataReader[col2] + "");
                    list[2].Add(dataReader[col3] + "");
                    list[3].Add(dataReader[col4] + "");
                }

                dataReader.Close();

                this.CloseConnection();
            }
            return list;
        }

        public List<string>[] GetComponentList3(string tip, string col1, string col2, string col3, string UID)
        {
            List<string>[] list = new List<string>[3];

            for (int i = 0; i < 3; i++)
                list[i] = new List<string>();

            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM " + tip + " WHERE USERID = " + UID;
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySql.Data.MySqlClient.MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[col1] + "");
                    list[1].Add(dataReader[col2] + "");
                    list[2].Add(dataReader[col3] + "");
                }

                dataReader.Close();

                this.CloseConnection();
            }
            return list;
        }



        public void ExecuteNonQueryCommand(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void InsertReminder(int UID , int remID , string dateAndTime , string text , int nrOrdine)
        {
            string query = "INSERT INTO reminder (REMINDERID ,  USERID , DATEANDTIME , REMINDERTEXT , NRORD) VALUES( '"+
                            remID + "' ,'"+ UID + "' , '"+ dateAndTime + "' , '"+ text + "' , '"+ nrOrdine + "')";
            ExecuteNonQueryCommand(query);
        }

        public void InsertNote(int UID, int noteID, string dateAndTime, string text, string title ,  int nrOrdine)
        {
            string query = "INSERT INTO note (NOTEID ,  USERID , DATEANDTIME , NOTETEXT , NOTETITLE , NRORD) VALUES( '" + 
                            noteID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '"+ title + "' , '" + nrOrdine + "')";
            ExecuteNonQueryCommand(query);
        }

        public void InsertToDo(int UID, int toDoID, string dateAndTime, string text, int statusCheck, int nrOrdine)
        {
            string query = "INSERT INTO todo (TODOID ,  USERID , DATEANDTIME , TODOTEXT , STATUSCHECK , NRORD) VALUES( '" + 
                            toDoID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + statusCheck + "' , '" + nrOrdine + "')";
            ExecuteNonQueryCommand(query);
        }

        public void InsertLink(int UID, int linkID, string dateAndTime, string text, string linkText, int nrOrdine)
        {
            string query = "INSERT INTO link (LINKID ,  USERID , DATEANDTIME , TEXT , LINKTEXT , NRORD) VALUES( '" + 
                            linkID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + linkText + "' , '" + nrOrdine + "')";
            ExecuteNonQueryCommand(query);
        }

        public void InsertTimer(int UID , int timerID , string text , int hours , int minutes , int seconds)
        {
            string query = "INSERT INTO timer (TIMERID , USERID , TIMERTEXT , HOURS , MINUTES , SECONDS) VALUES( '" + 
                            timerID + "' ,'" + UID + "' , '" + text + "' , '" + hours + "' , '" + minutes + "' , '" + seconds + "')";
            ExecuteNonQueryCommand(query);
        }

        public void DeleteComponent(int componentID , string componentType)
        {
            string query = "DELETE FROM " + componentType + " WHERE REMINDERID = " + componentID;
            ExecuteNonQueryCommand(query);
        }

        public int Count(int uid , string compType)
        {
            string query = "SELECT Count(*) FROM " + compType + " WHERE ";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar()+"");
        
                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        /*public void Update()
        {
            //TDO /// un fel de refresh
        }

        

        public void Restore()
        {
            TDO /// info se vor extrage din x
        }*/





    }
}


namespace Components
{

    public abstract class Component
    {
        private string text;
        private string previewText;
        private int nrOrdine;
        private DateTime dateAndTime;
        private static int NR = 0;
        public enum component { reminder, note, todo, link, timer }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string PreviewText
        {
            get { return this.previewText; }
            set { this.previewText = value; }
        }

        public int NrOrdine
        {
            get { return this.nrOrdine; }
            set { if (value > 0) this.nrOrdine = value; } // sa nu se repete/// de facut un API
        }

        public DateTime DateAndTime
        {
            get { return this.dateAndTime; }
            set { this.dateAndTime = value; }
        }

        public static void IncreaseNR()
        {
            NR++;
        }
    }

    public class Reminder : Component
    {
        private DateTime datePreview;

        public Reminder(string text , DateTime dateAndTime)
        {
            Text = text;
            PreviewText = Helper.GetPreviewText(text, 30); // cat sa fie afisat /// OP : posibil in functie de dimensiunea window-ului si a textului
            //NrOrdine = Helper.GetNrOrdine("reminder" , );
            DateAndTime = DateTime.Now; //TDO : dateAndTime trebuie setat intr-un date and time picker
            DatePreview = Helper.GetDatePreview(DateAndTime);
            /*DB.DBConnection conn = new DB.DBConnection();
            List<string>[] list = conn.GetOneTypeComponentList(DB.DBConnection.component.reminder, 2);
            conn.CloseConnection();*/

            Link link = new Link("link", 1, "www.google.com");

            UserManager.User user = UserManager.User.UserInstance;
            user.UId = 1113;
            user.FName = "aurelian";
            user.LName = "buga";
            user.UserName = "aquatrick";
            user.Email = "@yahoo.com";
            user.LoginType = true;
            user.Password = "passwd";

            Proiect_PI.XMLManager.CreateUsrXMLFile(user);
            Proiect_PI.XMLManager.AddComponent(link, 5970);
        }

        public DateTime DatePreview
        {
            get { return datePreview; }
            set { datePreview = value; }
        }
    }

    public class Note : Component
    {
        private string title;

        public Note(string text, int nrOrdine)
        {
            Text = text;
            Title = Helper.GetPreviewText(text, 1);   ///// sau se pune preview-ul
             NrOrdine = nrOrdine;
                PreviewText = Helper.GetFirstWord(text, 30) + "...";// cat sa fie afisat /// OP : posibil in functie de dimensiunea window-ului si a textului
                DateAndTime = DateTime.Now;
            }

            public Note(string title, string text, int nrOrdine)
            {
                Title = title;
                Text = text;
                PreviewText = text.Substring(0, 40) + "...";
                NrOrdine = nrOrdine;
                DateAndTime = DateTime.Now;
            }

            public string Title
            {
                get { return title; }
                set { title = value; }
            }
        }

    public class ToDo : Component
        {
            private bool statusCheck;

            public ToDo(string text, DateTime dateAndTime, int nrOrdine)
            {
                Text = text;
                PreviewText = Helper.GetPreviewText(text, 30);  /// IDEA : preview-ul sa se face in timpul executiei .....nu sa fie in constructor
                DateAndTime = dateAndTime; // datePicker
                NrOrdine = nrOrdine;
                StatusCheck = false;
            }

            public bool StatusCheck
            {
                get { return statusCheck; }
                set { statusCheck = value; }
            }
        }

    public class Link : Component
        {
            private string linkText;
            private DateTime datePreview;

            public Link(string text, int nrOrdine, string linkText)
            {
                Text = text;
                PreviewText = Helper.GetPreviewText(text, 30); // OP : posibil in functie de dimensiunea window-ului si a textului ///IDEA : preview-ul sa se face in timpul executiei .....nu sa fie in constructor
                NrOrdine = nrOrdine;
                DateAndTime = DateTime.Now;
                DatePreview = Helper.GetDatePreview(DateAndTime);
            }

            public string LinkText
            {
                get { return linkText; }
                set { linkText = value; }
            }

            public DateTime DatePreview
            {
                get { return datePreview; }
                set { datePreview = value; }
            }

        }

    public class Timer : Component
        {
            private int hours;
            private int minutes;
            private int seconds;

            // TDO : alerta , refresh times 1/sec

            public Timer(string text, int minutes, int seconds)
            {
                Text = text;
                PreviewText = Helper.GetPreviewText(text, 30); // sa aiba o lungime maxima ( text scurt) /// tot la executie nu in constructor
                DateAndTime = DateTime.Now;
                Minutes = minutes;
                Seconds = seconds;
            }

            public int Hours
            {
                get { return hours; }
                set { hours = value; }
            }

            public int Minutes
            {
                get { return minutes; }
                set { minutes = value; }
            }

            public int Seconds
            {
                get { return seconds; }
                set { seconds = value; }
            }
        }
    }

namespace TabManager
{
    class  ReminderTab
    {
        public static TabControl CreateReminderTab(TabControl tabIn)
        {
            TabControl tabOut = new TabControl();
            StackPanel stack = new StackPanel();
            Label lab = new Label();
            TabItem itemx = new TabItem();
            tabOut = tabIn;
            Components.Reminder reminder = new Components.Reminder("nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn" , DateTime.Now);

            lab.Content = reminder.PreviewText;
            stack.Children.Add(lab);

            stack.Orientation = Orientation.Horizontal;

            itemx.Header = "Reminder";
            itemx.Content = stack;
            tabOut.Items.Add(itemx);

            return tabOut;
        }
    }
}

namespace Proiect_PI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CreateReminderTab(object sender, RoutedEventArgs e)
        {
            tabControl = TabManager.ReminderTab.CreateReminderTab(tabControl);
        }
    }
}


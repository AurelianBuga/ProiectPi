using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Components;
using MySql.Data.Types;
using UserManager;



namespace DataManager
{
    public static class Helper
    {
        public static string GetPreviewText(string text, int writeableSpace)
        {
            if (text.Length > writeableSpace)
            {
                text = text.Substring(0, writeableSpace - 3) + "...";
            }

            return text;
        }

        public static string GetFirstWord(string text, int writeSpace)
        {
            if (text.Length > writeSpace)
            {
                text = text.Substring(0, writeSpace - 3) + "...";
            }
            else
            {
                if(text.Contains(' ') && text.IndexOf(' ') < writeSpace)
                {
                    text = text.Substring(0, text.IndexOf(' ', 0));
                }
                else if(text.Length > writeSpace)
                {
                    text = text.Substring(0, writeSpace - 3) + "...";
                }
                else
                {
                    //nothing
                }
            }
            return text;
        }

        public static string GetDatePreview(MySqlDateTime dateAndTime)
        {
            //TDO
            return dateAndTime.ToString();
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch(WebException wex)
            {
                if(wex.Message.ToString() == "The remote server returned an error: (407) Proxy Authentication Required.")
                {
                    return true;
                }else
                {
                    return false;
                }  
            }
        }

        public static string Get16CharPassword(string password)
        {
            string passwordX2 = password + password;
            return passwordX2.Substring(0, 16);
        }

        public static MySqlDateTime ConvertDateAndTime(string dateAndTime)
        {
            //TDO
            MySqlDateTime a = new MySqlDateTime(DateTime.Now);
            return a;
        }

        public static string ConvertDateAndTimeMySql(string dateAndTime)
        {
            //TDO
            return dateAndTime;
        }
    }

    public static class DBManager
    {
        public static MySqlConnection conn = new MySqlConnection("Server=localhost; Uid=root; Pwd=112aurelian; Database=proiect;");
        public static bool connStatus = false;


        public static bool OpenConnection() // Open database Connection
        {
            try
            {
                if (connStatus == true)
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
            catch (MySqlException ex)
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

        public static bool CloseConnection() // database connection close
        {
            try
            {
                conn.Close();
                connStatus = false;
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<Reminder> GetRemList()
        {
            List<Reminder> list = new List<Reminder>(Count( "reminder"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM reminder WHERE USERID = " + User.UserInstance.UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Reminder reminder = new Reminder(dataReader.GetInt32("REMINDERID"), dataReader.GetInt32("USERID"), dataReader.GetString("REMINDERTEXT"),
                                                    dataReader.GetMySqlDateTime("DATEANDTIME"), dataReader.GetInt32("NRORD"));
                    list.Add(reminder);
                }

                dataReader.Close();

                CloseConnection();
            }

            return list;
        }

        public static List<Note> GetNoteList()
        {
            List<Note> list = new List<Note>(Count( "note"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM note WHERE USERID = " + User.UserInstance.UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Note note = new Note(dataReader.GetInt32("NOTEID"), dataReader.GetInt32("USERID"), dataReader.GetString("NOTETEXT"),
                                                    dataReader.GetMySqlDateTime("DATEANDTIME"), dataReader.GetInt32("NRORD"), dataReader.GetString("NOTETITLE"));
                    list.Add(note);
                }

                dataReader.Close();

                CloseConnection();
            }
            return list;
        }

        public static List<ToDo> GetToDoList()
        {
            List<ToDo> list = new List<ToDo>(Count( "todo"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM todo WHERE USERID = " + User.UserInstance.UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ToDo toDo = new ToDo(dataReader.GetInt32("TODOID"), dataReader.GetInt32("USERID"), dataReader.GetString("TODOTEXT"),
                                                    dataReader.GetMySqlDateTime("DATEANDTIME"), dataReader.GetInt32("NRORD"), dataReader.GetBoolean("STATUSCHECK"));
                    list.Add(toDo);
                }

                dataReader.Close();

                CloseConnection();  
            }
            return list;
        }

        public static List<Link> GetLinkList()
        {
            List<Link> list = new List<Link>(Count("link"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM link WHERE USERID = " + User.UserInstance.UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Link link = new Link(dataReader.GetInt32("LINKID"), dataReader.GetInt32("USERID"), dataReader.GetString("TEXT"),
                                                    dataReader.GetMySqlDateTime("DATEANDTIME"), dataReader.GetInt32("NRORD"), dataReader.GetString("LINKTEXT"));
                    list.Add(link);
                }

                dataReader.Close();

                CloseConnection(); 
            }
            return list;
        }

        public static void GetTimer()
        {
            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM timer WHERE USERID = " + User.UserInstance.UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                Timer.TimerInstance.TimerId = dataReader.GetInt32("TIMERID");
                Timer.TimerInstance.UID = User.UserInstance.UID;
                Timer.TimerInstance.Selection = dataReader.GetInt32("SELECTALERT");
                Timer.TimerInstance.Hours = dataReader.GetInt32("HOURS");
                Timer.TimerInstance.Minutes = dataReader.GetInt32("MINUTES");
                Timer.TimerInstance.Seconds = dataReader.GetInt32("SECONDS");

                dataReader.Close();
                CloseConnection();
            }
        }

        public static void ExecuteNonQueryCommand(string query)
        {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.ExecuteNonQuery();

                CloseConnection();
            }
        }

        public static void InsertComponent(Reminder reminder)
        {
            string query = "INSERT INTO reminder (REMINDERID ,  USERID , DATEANDTIME , REMINDERTEXT , NRORD) VALUES( '" +
                            reminder.IdComp + "' ,'" + reminder.IdUsr + "' , STR_TO_DATE('"+ reminder.Date +"', '%c/%e/%Y %r') , '" + reminder.Text + "' , '" + reminder.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(Note note)
        {
            string query = "INSERT INTO note (NOTEID ,  USERID , DATEANDTIME , NOTETEXT , NOTETITLE , NRORD) VALUES( '" +
                            note.IdComp + "' ,'" + note.IdUsr + "' , STR_TO_DATE('" + note.Date + "', '%c/%e/%Y %r') , '" + note.Text + "' , '" + note.Title + "' , '" + note.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(ToDo toDo)
        {
            string query = "INSERT INTO todo (TODOID ,  USERID , DATEANDTIME , TODOTEXT , STATUSCHECK , NRORD) VALUES( '" +
                            toDo.IdComp + "' ,'" + toDo.IdUsr + "' , STR_TO_DATE('" + toDo.Date + "', '%c/%e/%Y %r') , '" + toDo.Text + "' , '" + Convert.ToInt32(toDo.StatusCheck) + "' , '" + toDo.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(Link link)
        {
            string query = "INSERT INTO link (LINKID ,  USERID , DATEANDTIME , TEXT , LINKTEXT , NRORD) VALUES( '" +
                            link.IdComp + "' ,'" + link.IdUsr + "' , STR_TO_DATE('" + link.Date + "', '%c/%e/%Y %r') , '" + link.Text + "' , '" + link.LinkText + "' , '" + link.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(Timer timer)
        {
            string query;
            if(Timer.TimerInstance == null)
            {
                 query = "INSERT INTO timer (TIMERID , USERID , SELECTALERT , HOURS , MINUTES , SECONDS) VALUES( '" +
                            timer.TimerId + "' ,'" + timer.UID + "' , '" + timer.Selection + "' , '" + timer.Hours + "' , '" + timer.Minutes + "' , '" + timer.Seconds + "')";
            }
            else
            {
                 query = "INSERT INTO timer (TIMERID , USERID , SELECTALERT , HOURS , MINUTES , SECONDS) VALUES( '" +
                 Timer.TimerInstance.TimerId + "' ,'" + timer.UID + "' , '" + timer.Selection + "' , '" + timer.Hours + "' , '" + timer.Minutes + "' , '" + timer.Seconds + "')";
            }
            ExecuteNonQueryCommand(query);
        }

        public static void DeleteComponent(int componentID, string componentType , string componentIdTag)
        {
            string query = "DELETE FROM " + componentType + " WHERE "+ componentIdTag +" = " + componentID;

            ExecuteNonQueryCommand(query);
        }

        public static int Count(string compType)
        {
            string query = "SELECT Count(*) FROM " + compType.ToLower().ToString() + " WHERE USERID = " + User.UserInstance.UID;
            int Count = -1;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                Count = int.Parse(cmd.ExecuteScalar() + "");

                CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public static bool UserExists(string userName)
        {
            string query = "SELECT Count(*) FROM users WHERE USERNAME = '" + userName + "';";
            int Count = -1;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                CloseConnection();
            }

            if (Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UserExists(string userName, string password)
        {
            string query = "SELECT Count(*) FROM users WHERE USERNAME = '" + userName + "' AND PWD = '" + password + "' ;";
            int Count = -1;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                Count = int.Parse(cmd.ExecuteScalar() + "");

                CloseConnection();
            }

            if (Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool UserExists(int uid)
        {
            string query = "SELECT Count(*) FROM users WHERE UID = '" + uid + "';";
            int Count = -1;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                Count = int.Parse(cmd.ExecuteScalar() + "");

                CloseConnection();
            }

            if (Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static UserInfo GetUserInfo(string userName, string password)
        {
            UserManager.UserInfo userInfo = new UserManager.UserInfo();
            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM users WHERE USERNAME = '" + userName + "' LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    userInfo.uId = Convert.ToInt32(reader["UID"]);
                    userInfo.fName = reader["FNAME"].ToString();
                    userInfo.lName = reader["LNAME"].ToString();
                    userInfo.userName = reader["USERNAME"].ToString();
                    userInfo.email = reader["EMAIL"].ToString();
                    userInfo.password = reader["PWD"].ToString();
                }
                CloseConnection();
            }
            return userInfo;
        }

        public static void AddUser(UserManager.UserInfo userInfo)
        {
            string query = "INSERT INTO `proiect`.`users` (`FNAME`, `LNAME`, `USERNAME`, `EMAIL`, `PWD`) VALUES" +
                           " ('" + userInfo.fName + "', '" + userInfo.lName + "', '" + userInfo.userName +
                           "', '" + userInfo.email + "', '" + userInfo.password + "');";

            ExecuteNonQueryCommand(query);
        }


        /*public static  void UpdateComponent()
        {
            //TDO /// un fel de refresh
        }

        public static  void UpdateUserInfo()
        {
            //TDO
        }

        public static  void Restore()
        {
            TDO /// info se vor extrage din x
        }*/
    }

    public sealed class XMLManager
    {
        private XDocument userFile;

        private static readonly XMLManager xmlManagerInstance = new XMLManager();

        public enum compType { reminder , todo , note , link , timer}

        private XMLManager()
        {

        }

        public static XMLManager XMLManagerInstance
        {
            get { return xmlManagerInstance; }
        }

        private string usrFolderPath;
        private string usrAppDataFolderPath;
        private string usrFilePath;
        private string usrAppDataFilePath;

        public static DirectoryInfo applicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Documents\MyApplication"));
        public static DirectoryInfo appDataApplicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication"));

        public static int NrRem = 0;
        public static int NrLink = 0;
        public static int NrToDo = 0;
        public static int NrNote = 0;

        public void GetPaths(int uid)
        {
            usrFolderPath = applicationPath.ToString() + @"\" + uid;
            usrAppDataFolderPath = Path.Combine(appDataApplicationPath.ToString(), uid.ToString());
            usrFilePath = Path.Combine(applicationPath.ToString() + @"\" + uid, uid + ".xml");
            usrAppDataFilePath = Path.Combine(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()), uid + ".xml");
        }

        private void CreateApplicationFolder()
        {
            /// daca nu exista folder pt aplicatie se va creea 

            DirectoryInfo applicationDirectory = new DirectoryInfo(applicationPath.ToString());

            if (!applicationDirectory.Exists)
            {
                Directory.CreateDirectory(applicationDirectory.ToString());
            }
        }

        private void CreateUserFolder()
        {
            /// daca nu exista folder pt user se va creea 

            
            DirectoryInfo userFolderDirectory = new DirectoryInfo(usrFolderPath);

            if (!userFolderDirectory.Exists)
            {
                Directory.CreateDirectory(userFolderDirectory.ToString());
            }
        }

        private void CreateAppDataUsrFolder()
        {
            
            DirectoryInfo appDataAppDir = new DirectoryInfo(usrAppDataFolderPath);

            if (!appDataAppDir.Exists)
                Directory.CreateDirectory(appDataAppDir.ToString());
        }

        private void CreateUsrXMLFile(UserInfo userInfo)
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            
            DirectoryInfo userFileDir = new DirectoryInfo(usrFilePath);

            if (!userFileDir.Exists)
            {
                /// se va creea xml-ul user-ului 
                /// pas : 1. se creaza un fisier temporar in AppData
                /// pas : 2. se cripteaza fisierul , cheia fiind uid-ul userului , si se pune in folder-ul user-ului din folder-ul aplicatiei
                /// pas : 3. se sterge fisierul temporar din AppData

                //------------1---------------------------
                XDocument xmlUser = new XDocument();
                XElement userDoc = new XElement("User", string.Empty);

                userDoc.SetAttributeValue("UID", userInfo.uId);
                userDoc.SetAttributeValue("FName", userInfo.fName);
                userDoc.SetAttributeValue("LName", userInfo.lName);
                userDoc.SetAttributeValue("UserName", userInfo.userName);
                userDoc.SetAttributeValue("Email", userInfo.email);
                userDoc.SetAttributeValue("Password", userInfo.password);


                userDoc.Add(new XElement("Reminders", string.Empty));
                userDoc.Add(new XElement("Notes", string.Empty));
                userDoc.Add(new XElement("Links", string.Empty));
                userDoc.Add(new XElement("ToDos", string.Empty));
                xmlUser.Add(userDoc);

                FileStream write = new FileStream(usrAppDataFilePath, FileMode.Create, FileAccess.Write);
                xmlUser.Save(write);
                write.Close();

                //--------------2-------------------
                EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(userInfo.password));
                File.Delete(usrAppDataFilePath);

                //--------------3-------------------
                write.Close();
            }
        }

        public void CreateUserDirsAndFiles(UserInfo userInfo)
        {
            CreateApplicationFolder();
            CreateUserFolder();
            CreateAppDataUsrFolder();
            CreateUsrXMLFile(userInfo);
        }

        public void LoadUsrXMLFile(string password)
        {
            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(password));
            File.Delete(usrFilePath);

            userFile = XDocument.Load(usrAppDataFilePath);

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
            File.Delete(usrAppDataFilePath);
        }

        public List<Reminder> GetReminderList()
        {
            List<Reminder> list = new List<Reminder>();

            //se ia din fisierul temporar
            XElement remindersNode = (from xnode in userFile.Descendants("Reminders") select xnode).SingleOrDefault();

            if(remindersNode.Descendants("Reminder").Count() != 0)
            {
                IEnumerable<XElement> reminders = remindersNode.Descendants("Reminder");
                foreach (XElement rem in reminders)
                {
                    IEnumerable<XElement> reminderComps = rem.Descendants();
                    string ReminderId = (from xnode in rem.Descendants("ReminderId") select xnode).SingleOrDefault().Value;
                    string Text = (from xnode in rem.Descendants("Text") select xnode).SingleOrDefault().Value;
                    string NrOrd = (from xnode in rem.Descendants("NrOrd") select xnode).SingleOrDefault().Value;
                    string date = (from xnode in rem.Descendants("DateAndTime") select xnode).SingleOrDefault().Value;
                    DateTime DateAndTime = Convert.ToDateTime(date);
                    Reminder reminder = new Reminder(Convert.ToInt32(ReminderId), User.UserInstance.UID, Text, new MySqlDateTime(DateAndTime), Convert.ToInt32(NrOrd));
                    list.Add(reminder);
                }
            }

            return list;

        }

        public List<ToDo> GetToDoList()
        {
            List<ToDo> list = new List<ToDo>();

            XElement toDosNode = (from xnode in userFile.Descendants("ToDos") select xnode).SingleOrDefault();

            if(toDosNode.Descendants("ToDo").Count() != 0)
            {
                IEnumerable<XElement> toDos = toDosNode.Descendants("ToDo");
                foreach (XElement toDo in toDos)
                {
                    IEnumerable<XElement> reminderComps = toDo.Descendants();
                    string ReminderId = (from xnode in toDo.Descendants("ToDoId") select xnode).SingleOrDefault().Value;
                    string Text = (from xnode in toDo.Descendants("Text") select xnode).SingleOrDefault().Value;
                    string NrOrd = (from xnode in toDo.Descendants("NrOrd") select xnode).SingleOrDefault().Value;
                    string date = (from xnode in toDo.Descendants("DateAndTime") select xnode).SingleOrDefault().Value;
                    string statusCheck = (from xnode in toDo.Descendants("StatusCheck") select xnode).SingleOrDefault().Value;
                    DateTime DateAndTime = Convert.ToDateTime(date);
                    ToDo todo = new ToDo(Convert.ToInt32(ReminderId), User.UserInstance.UID, Text, new MySqlDateTime(DateAndTime), Convert.ToInt32(NrOrd), statusCheck == "true");
                    list.Add(todo);
                }
            }

            return list;

        }

        public List<Note> GetNoteList()
        {
            List<Note> list = new List<Note>();

            XElement notesNode = (from xnode in userFile.Descendants("Notes") select xnode).SingleOrDefault();

            if(notesNode.Descendants("Note").Count() != 0)
            {
                IEnumerable<XElement> toDos = notesNode.Descendants("Note");
                foreach (XElement toDo in toDos)
                {
                    IEnumerable<XElement> reminderComps = toDo.Descendants();
                    string NoteId = (from xnode in toDo.Descendants("NoteId") select xnode).SingleOrDefault().Value;
                    string Text = (from xnode in toDo.Descendants("Text") select xnode).SingleOrDefault().Value;
                    string NrOrd = (from xnode in toDo.Descendants("NrOrd") select xnode).SingleOrDefault().Value;
                    string date = (from xnode in toDo.Descendants("DateAndTime") select xnode).SingleOrDefault().Value;
                    string Title = (from xnode in toDo.Descendants("Title") select xnode).SingleOrDefault().Value;
                    DateTime DateAndTime = Convert.ToDateTime(date);
                    Note note = new Note(Convert.ToInt32(NoteId), User.UserInstance.UID, Text, new MySqlDateTime(DateAndTime), Convert.ToInt32(NrOrd), Title);
                    list.Add(note);
                }
            }
            

            return list;

        }

        public List<Link> GetLinkList()
        {
            List<Link> list = new List<Link>();

            XElement linksNode = (from xnode in userFile.Descendants("Links") select xnode).SingleOrDefault();

            if(linksNode.Descendants("Link").Count() != 0)
            {
                IEnumerable<XElement> toDos = linksNode.Descendants("Link");
                foreach (XElement toDo in toDos)
                {
                    IEnumerable<XElement> reminderComps = toDo.Descendants();
                    string LinkId = (from xnode in toDo.Descendants("LinkId") select xnode).SingleOrDefault().Value;
                    string Text = (from xnode in toDo.Descendants("Text") select xnode).SingleOrDefault().Value;
                    string NrOrd = (from xnode in toDo.Descendants("NrOrd") select xnode).SingleOrDefault().Value;
                    string date = (from xnode in toDo.Descendants("DateAndTime") select xnode).SingleOrDefault().Value;
                    string LinkText = (from xnode in toDo.Descendants("LinkText") select xnode).SingleOrDefault().Value;
                    DateTime DateAndTime = Convert.ToDateTime(date);
                    Link link = new Link(Convert.ToInt32(LinkId), User.UserInstance.UID, Text, new MySqlDateTime(DateAndTime), Convert.ToInt32(NrOrd), LinkText);
                    list.Add(link);
                }
            }
            
            return list;

        }

        public void GetTimer()
        {
            XElement timerNode = (from xnode in userFile.Descendants("Timer") select xnode).SingleOrDefault();
            if(timerNode.Descendants().Count() != 0)
            {
                int Hours = Convert.ToInt32((from xnode in timerNode.Descendants("Hours") select xnode).SingleOrDefault().Value);
                int Minutes = Convert.ToInt32((from xnode in timerNode.Descendants("Minutes") select xnode).SingleOrDefault().Value);
                int Seconds = Convert.ToInt32((from xnode in timerNode.Descendants("Seconds") select xnode).SingleOrDefault().Value);
                int Selection = Convert.ToInt32((from xnode in timerNode.Descendants("Selection") select xnode).SingleOrDefault().Value);
                int TimerId = Convert.ToInt32((from xnode in timerNode.Descendants("TimerId") select xnode).SingleOrDefault().Value);

                Timer.TimerInstance.Hours = Hours;
                Timer.TimerInstance.Minutes = Minutes;
                Timer.TimerInstance.Seconds = Seconds;
                Timer.TimerInstance.Selection = Selection;
                Timer.TimerInstance.TimerId = TimerId;
            }
        }

        public int Count(compType type)
        {
            switch (type)
            {
                case compType.reminder:
                    {
                        return GetReminderList().Count;
                    }
                case compType.note:
                    {
                        return GetNoteList().Count;
                    }
                case compType.todo:
                    {
                        return GetToDoList().Count;
                    }
                case compType.link:
                    {
                        return GetLinkList().Count;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }

        public void InsertComponent(Link linkElement)
        {

            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));
            File.Delete(usrFilePath);

            XDocument userXML = XDocument.Load(usrAppDataFilePath);
            FileStream tmpFile = new FileStream(usrAppDataFilePath, FileMode.Open, FileAccess.ReadWrite);
            XElement linksNode = (from xnode in userXML.Descendants("Links") select xnode).FirstOrDefault();

            XElement link = new XElement("Link");
            XElement userId = new XElement("UserId", User.UserInstance.UID);
            XElement linkId = new XElement("LinkId", linkElement.IdComp);
            XElement linkText = new XElement("LinkText", linkElement.LinkText);
            XElement text = new XElement("Text", linkElement.Text);
            XElement nrOrd = new XElement("NrOrd", linkElement.NrOrd);
            XElement date = new XElement("DateAndTime", linkElement.Date);
            link.Add(linkId, userId, linkText, text, nrOrd, date);
            linksNode.Add(link);


            userXML.Save(tmpFile);
            tmpFile.Close();

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));

            LoadUsrXMLFile(User.UserInstance.Pasword);

            File.Delete(usrAppDataFilePath);
        }

        public void InsertComponent(Note noteElement)
        {
            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));
            File.Delete(usrFilePath);

            XDocument userXML = XDocument.Load(usrAppDataFilePath);
            FileStream tmpFile = new FileStream(usrAppDataFilePath, FileMode.Open, FileAccess.ReadWrite);
            XElement notesNode = (from xnode in userXML.Descendants("Notes") select xnode).FirstOrDefault();

            XElement note = new XElement("Note");
            XElement userId = new XElement("UserId", User.UserInstance.UID);
            XElement noteId = new XElement("NoteId", noteElement.IdComp);
            XElement noteTitle = new XElement("Title", noteElement.Title);
            XElement text = new XElement("Text", noteElement.Text);
            XElement nrOrd = new XElement("NrOrd", noteElement.NrOrd);
            XElement date = new XElement("DateAndTime", noteElement.Date);
            note.Add(noteId, userId, noteTitle, text, nrOrd, date);
            notesNode.Add(note);

            userXML.Save(tmpFile);
            tmpFile.Close();

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));

            LoadUsrXMLFile(User.UserInstance.Pasword);

            File.Delete(usrAppDataFilePath);
        }

        public void InsertComponent(Reminder reminderElement)
        {

            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));
            File.Delete(usrFilePath);

            XDocument userXML = XDocument.Load(usrAppDataFilePath);
            FileStream tmpFile = new FileStream(usrAppDataFilePath, FileMode.Open, FileAccess.ReadWrite);
            XElement remindersNode = (from xnode in userXML.Descendants("Reminders") select xnode).FirstOrDefault();

            XElement reminder = new XElement("Reminder");
            XElement userId = new XElement("UserId", User.UserInstance.UID);
            XElement reminderId = new XElement("ReminderId", reminderElement.IdComp);
            XElement text = new XElement("Text", reminderElement.Text);
            XElement nrOrd = new XElement("NrOrd", reminderElement.NrOrd);
            XElement date = new XElement("DateAndTime", reminderElement.Date);
            reminder.Add(reminderId, userId, text, nrOrd, date);
            remindersNode.Add(reminder);

            userXML.Save(tmpFile);
            tmpFile.Close();

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));

            LoadUsrXMLFile(User.UserInstance.Pasword);

            File.Delete(usrAppDataFilePath);
        }

        public void InsertComponent(ToDo toDoElement)
        {
            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));
            File.Delete(usrFilePath);

            XDocument userXML = XDocument.Load(usrAppDataFilePath);
            FileStream tmpFile = new FileStream(usrAppDataFilePath, FileMode.Open, FileAccess.ReadWrite);
            XElement toDosNode = (from xnode in userXML.Descendants("ToDos") select xnode).FirstOrDefault();

            XElement toDo = new XElement("ToDo");
            XElement userId = new XElement("UserId", User.UserInstance.UID);
            XElement toDoId = new XElement("ToDoId", toDoElement.IdComp);
            XElement statusCheck = new XElement("StatusCheck", toDoElement.StatusCheck);
            XElement text = new XElement("Text", toDoElement.Text);
            XElement nrOrd = new XElement("NrOrd", toDoElement.NrOrd);
            XElement date = new XElement("DateAndTime", toDoElement.Date);
            toDo.Add(toDoId, userId, statusCheck, text, nrOrd, date);
            toDosNode.Add(toDo);

            userXML.Save(tmpFile);
            tmpFile.Close();

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));

            LoadUsrXMLFile(User.UserInstance.Pasword);

            File.Delete(usrAppDataFilePath);
        }

        public void InsertComponent(Timer timerElement)
        {
            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));
            File.Delete(usrFilePath);

            XDocument userXML = XDocument.Load(usrAppDataFilePath);
            FileStream tmpFile = new FileStream(usrAppDataFilePath, FileMode.Open, FileAccess.ReadWrite);
            XElement timerNode = (from xnode in userXML.Descendants("Timer") select xnode).FirstOrDefault();

            XElement hours = new XElement("Hours", timerElement.Hours);
            XElement selection = new XElement("Selection", timerElement.Selection); // 20-20 rule or classic
            XElement minutes = new XElement("Minutes", timerElement.Minutes);
            XElement seconds = new XElement("Seconds", timerElement.Seconds);
            XElement timerId = new XElement("TimerId", timerElement.TimerId);

            timerNode.Add(hours, selection ,  minutes, seconds, timerId);

            userXML.Save(tmpFile);
            tmpFile.Close();

            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(User.UserInstance.Pasword));

            LoadUsrXMLFile(User.UserInstance.Pasword);

            File.Delete(usrAppDataFilePath);
        }

        public static bool UserExists(string userName, string password)
        {

            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();
            XName userNamex = "UserName";

            foreach (DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                foreach (FileInfo userFile in userFileList)
                {
                    string userAppDataApplicationFile = Path.Combine(appDataApplicationPath.ToString(), userFile.Name);

                    EncryptManager.DecryptFile(userFile.FullName, userAppDataApplicationFile, Helper.Get16CharPassword(password));

                    FileStream tmpUserFile = new FileStream(userAppDataApplicationFile, FileMode.Open, FileAccess.Read);
                    try
                    {
                        XDocument file = XDocument.Load(tmpUserFile);
                        tmpUserFile.Close();

                        XElement userNode = (from xnode in file.Descendants("User") select xnode).FirstOrDefault();
                        XAttribute atr = userNode.Attribute(userNamex);
                        if (atr != null)
                        {
                            if (atr.Value.ToString() == userName)
                            {
                                File.Delete(userFile.FullName);
                                EncryptManager.EncryptFile(userAppDataApplicationFile, userFile.FullName, Helper.Get16CharPassword(password));
                                File.Delete(userAppDataApplicationFile);
                                return true;
                            }
                        }
                    }
                    catch
                    {
                        tmpUserFile.Close();
                    }

                    File.Delete(userAppDataApplicationFile);
                }
            }
            return false;
        }

        public static bool UserExists(int uid)
        {

            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();

            foreach (DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                foreach (FileInfo userFile in userFileList)
                {
                    if (userFile.Name.Contains(uid.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static UserInfo GetUserInfo(string userName, string password)
        {

            UserInfo user = new UserManager.UserInfo();
            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();


            foreach (DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                foreach (FileInfo userFile in userFileList)
                {
                    string userAppDataApplicationFile = Path.Combine(appDataApplicationPath.ToString() + @"\" + userFile.Name.Substring(0, userFile.Name.IndexOf('.')), userFile.Name);

                    EncryptManager.DecryptFile(userFile.FullName, userAppDataApplicationFile, Helper.Get16CharPassword(password));

                    FileStream tmpUserFile = new FileStream(userAppDataApplicationFile, FileMode.Open, FileAccess.Read);

                    try
                    {
                        XDocument file = XDocument.Load(tmpUserFile);
                        tmpUserFile.Close();
                        IEnumerable<XElement> xElements = file.Descendants();
                        foreach (XElement el in xElements)
                        {
                            XName userNamex = "UserName";
                            XAttribute atr = el.Attribute(userNamex);
                            if (atr != null)
                            {
                                if (atr.Value.ToString() == userName)
                                {
                                    XName uid = "UID";
                                    XName fName = "FName";
                                    XName lName = "LName";
                                    XName email = "Email";
                                    XName pwd = "Password";

                                    user.uId = Convert.ToInt32(el.Attribute(uid).Value);
                                    user.userName = el.Attribute(userNamex).Value.ToString();
                                    user.fName = el.Attribute(fName).Value.ToString();
                                    user.lName = el.Attribute(lName).Value.ToString();
                                    user.email = el.Attribute(email).Value.ToString();
                                    user.password = el.Attribute(pwd).Value.ToString();


                                    File.Delete(userFile.FullName);
                                    EncryptManager.EncryptFile(userAppDataApplicationFile, userFile.FullName, Helper.Get16CharPassword(password));
                                    File.Delete(userAppDataApplicationFile);

                                    return user;
                                }
                            }
                        }
                        File.Delete(userAppDataApplicationFile);
                    }
                    catch
                    {

                    }

                }
            }

            return user;
        }

        public bool ComponentExists(int idComp, int type, int uid, string password)
        {
            EncryptManager.DecryptFile(usrFilePath, usrAppDataFilePath, Helper.Get16CharPassword(password));
            File.Delete(usrFilePath);

            XDocument usrDoc = XDocument.Load(usrAppDataFilePath);

            switch (type)
            {
                //reminder
                case 1:
                    {
                        XElement RemNode = (from xnode in usrDoc.Descendants("Reminders") select xnode).SingleOrDefault();
                        IEnumerable<XElement> rems = RemNode.Descendants("Reminder");
                        foreach (XElement rem in rems)
                        {
                            XElement idNode = (from xnode in rem.Descendants("ReminderId") select xnode).SingleOrDefault();
                            String nodeValOnlyDigits = new String(idNode.Value.Where(Char.IsDigit).ToArray());
                            if (nodeValOnlyDigits == idComp.ToString())
                            {
                                EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                                File.Delete(usrAppDataFilePath);
                                return true;
                            }
                        }
                        EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                        File.Delete(usrAppDataFilePath);
                        return false;
                    }
                //toDo
                case 2:
                    {
                        XElement RemNode = (from xnode in usrDoc.Descendants("ToDos") select xnode).SingleOrDefault();
                        IEnumerable<XElement> rems = RemNode.Descendants("ToDo");
                        foreach (XElement rem in rems)
                        {
                            XElement idNode = (from xnode in rem.Descendants("ToDoId") select xnode).SingleOrDefault();
                            String nodeValOnlyDigits = new String(idNode.Value.Where(Char.IsDigit).ToArray());
                            if (nodeValOnlyDigits == idComp.ToString())
                            {
                                EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                                File.Delete(usrAppDataFilePath);
                                return true;
                            }
                        }
                        EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                        File.Delete(usrAppDataFilePath);
                        return false;
                    }
                //Note
                case 3:
                    {
                        XElement RemNode = (from xnode in usrDoc.Descendants("Notes") select xnode).SingleOrDefault();
                        IEnumerable<XElement> rems = RemNode.Descendants("Note");
                        foreach (XElement rem in rems)
                        {
                            XElement idNode = (from xnode in rem.Descendants("NoteId") select xnode).SingleOrDefault();
                            String nodeValOnlyDigits = new String(idNode.Value.Where(Char.IsDigit).ToArray());
                            if (nodeValOnlyDigits == idComp.ToString())
                            {
                                EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                                File.Delete(usrAppDataFilePath);
                                return true;
                            }
                        }
                        EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                        File.Delete(usrAppDataFilePath);
                        return false;
                    }
                //Link
                case 4:
                    {
                        XElement RemNode = (from xnode in usrDoc.Descendants("Links") select xnode).SingleOrDefault();
                        IEnumerable<XElement> rems = RemNode.Descendants("Link");
                        foreach (XElement rem in rems)
                        {
                            XElement idNode = (from xnode in rem.Descendants("LinkId") select xnode).SingleOrDefault();
                            String nodeValOnlyDigits = new String(idNode.Value.Where(Char.IsDigit).ToArray());
                            if (nodeValOnlyDigits == idComp.ToString())
                            {
                                EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                                File.Delete(usrAppDataFilePath);
                                return true;
                            }
                        }
                        EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
                        File.Delete(usrAppDataFilePath);
                        return false;
                    }
            }
            EncryptManager.EncryptFile(usrAppDataFilePath, usrFilePath, Helper.Get16CharPassword(password));
            File.Delete(usrAppDataFilePath);
            return false;
        }
    }

    public static class EncryptManager
    {
        public static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to encrypt file
            }
        }

        public static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to decrypt file
            }
        }
    }
}

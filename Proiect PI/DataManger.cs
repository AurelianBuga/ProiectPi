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
                text = text.Substring(0, text.IndexOf(' ', 0));
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
            catch
            {
                return false;
            }
        }

        public static string Get16CharPassword(string password)
        {
            string passwordX2 = password + password;
            return passwordX2.Substring(0, 16);
        }

        public static int GetUID()
        {
            Random randGen = new Random();
            int uidTest = -1;
            if(CheckForInternetConnection())
            {
                do
                {
                    uidTest = randGen.Next(1000, 9999);
                } while (DBManager.UserExists(uidTest));

                return uidTest;
            }
            else
            {
                do
                {
                    uidTest = randGen.Next(1000, 9999);
                } while (XMLManager.UserExists(uidTest));

                return uidTest;
            }
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

        public static List<Reminder> GetRemList(int uid)
        {
            List<Reminder> list = new List<Reminder>(Count(uid, "reminder"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM reminder WHERE USERID = " + uid;
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

        public static List<Note> GetNoteList(int uid)
        {
            List<Note> list = new List<Note>(Count(uid, "note"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM note WHERE USERID = " + uid;
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

        public static List<ToDo> GetToDoList(int uid)
        {
            List<ToDo> list = new List<ToDo>(Count(uid, "todo"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM todo WHERE USERID = " + uid;
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

        public static List<Link> GetLinkList(int uid)
        {
            List<Link> list = new List<Link>(Count(uid, "link"));

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM link WHERE USERID = " + uid;
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

        /*public static Timer GetTimer(int uid)
        {
            if (OpenConnection() == true)
            {
                
                CloseConnection();

            ///TDO
            }

        }*/

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
                            reminder.IdREm + "' ,'" + reminder.IdUsr + "' , '" + reminder.Date + "' , '" + reminder.Text + "' , '" + reminder.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(Note note)
        {
            string query = "INSERT INTO note (NOTEID ,  USERID , DATEANDTIME , NOTETEXT , NOTETITLE , NRORD) VALUES( '" +
                            note.IdNote + "' ,'" + note.IdUsr + "' , '" + note.Date + "' , '" + note.Text + "' , '" + note.Title + "' , '" + note.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(ToDo toDo)
        {
            string query = "INSERT INTO todo (TODOID ,  USERID , DATEANDTIME , TODOTEXT , STATUSCHECK , NRORD) VALUES( '" +
                            toDo.IdToDo + "' ,'" + toDo.IdUsr + "' , '" + toDo.Date + "' , '" + toDo.Text + "' , '" + toDo.StatusCheck + "' , '" + toDo.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertComponent(Link link)
        {
            string query = "INSERT INTO link (LINKID ,  USERID , DATEANDTIME , TEXT , LINKTEXT , NRORD) VALUES( '" +
                            link.IdLink + "' ,'" + link.IdUsr + "' , '" + link.Date + "' , '" + link.Text + "' , '" + link.LinkText + "' , '" + link.NrOrd + "')";

            ExecuteNonQueryCommand(query);
        }

        /*public static void InsertComponent(Timer timer)
        {
            string query = "INSERT INTO timer (TIMERID , USERID , TIMERTEXT , HOURS , MINUTES , SECONDS) VALUES( '" +
                            timerID + "' ,'" + UID + "' , '" + text + "' , '" + hours + "' , '" + minutes + "' , '" + seconds + "')";
            
            //TDO

            ExecuteNonQueryCommand(query);
        }*/

        public static void DeleteComponent(int componentID, string componentType , string componentIdTag)
        {
            string query = "DELETE FROM " + componentType + " WHERE "+ componentIdTag +" = " + componentID;

            ExecuteNonQueryCommand(query);
        }

        public static int Count(int uid, string compType)
        {
            string query = "SELECT Count(*) FROM " + compType.ToString() + " WHERE USERID = " + uid;
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

        public static bool UserExists(int uid)
        {
            string query = "SELECT Count(*) FROM users WHERE UID = '" + uid + "';";
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

        public static UserManager.UserInfo GetUserInfo(string userName, string password)
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

    public static class XMLManager
    {
        /*TDO:
        - a method that create a folder(for each user) wich contains XML files(one for each type of component) for users (if they don't exists) 
        - a method that add content to XML files created for that user and component type .....when that component is created ( it is saved in xml file and DB ) 
        - a method that  clear content in XML files ... when a component is deleted ( it is deleted form XML file and DB)
        - a method that update content in XML files ... when a component is updated ( it is updated in XML file and DB)
        - a getter method that returns a List<string>[] for a component type and user
        - a method that count elements in a XML file 
        - additional : \
            - a method that restore files for an user for XML file
         */

        public static DirectoryInfo applicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Documents\MyApplication"));
        public static DirectoryInfo appDataApplicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication"));

        //valorile vor fi asignate in metoda GetComponentListOneType()
        private static int NrRem = 0; 
        private static int NrLink = 0;
        private static int NrToDo = 0;
        private static int NrNote = 0;

        private static void CreateApplicationFolder()
        {
            /// daca nu exista folder pt aplicatie se va creea 

            DirectoryInfo applicationDirectory = new DirectoryInfo(applicationPath.ToString());

            if (!applicationDirectory.Exists)
            {
                Directory.CreateDirectory(applicationDirectory.ToString());
            }
        }

        private static void CreateUserFolder(int uid)
        {
            /// daca nu exista folder pt user se va creea 

            string folderPath = applicationPath.ToString() + @"\" + uid;
            DirectoryInfo userFolderDirectory = new DirectoryInfo(folderPath);

            if (!userFolderDirectory.Exists)
            {
                Directory.CreateDirectory(userFolderDirectory.ToString());
            }
        }

        private static void CreateAppDataUsrFolder(int uid)
        {
            DirectoryInfo appDataAppDir = new DirectoryInfo(Path.Combine(appDataApplicationPath.ToString() , uid.ToString() ));

            if (!appDataAppDir.Exists)
                Directory.CreateDirectory(appDataAppDir.ToString());
        }

        private static void CreateUsrXMLFile(UserManager.UserInfo userInfo)
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            string userFilePath = Path.Combine(applicationPath.ToString() + @"\" + userInfo.uId, userInfo.uId + ".xml");
            DirectoryInfo userFileDir = new DirectoryInfo(userFilePath);

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



                string tmpUsrFilePath = Path.Combine(Path.Combine(appDataApplicationPath.ToString(), userInfo.uId.ToString()), userInfo.uId + ".xml");

                FileStream write = new FileStream(tmpUsrFilePath, FileMode.Create, FileAccess.Write);
                xmlUser.Save(write);
                write.Close();

                //--------------2-------------------
                EncryptManager.EncryptFile(tmpUsrFilePath, userFilePath, Helper.Get16CharPassword(userInfo.password));
                File.Delete(tmpUsrFilePath);

                //--------------3-------------------
                write.Close();
            }
        }

        public static void CreateUserDirsAndFiles(UserManager.UserInfo userInfo)
        {
            CreateApplicationFolder();
            CreateUserFolder(userInfo.uId);
            CreateAppDataUsrFolder(userInfo.uId);
            CreateUsrXMLFile(userInfo);
        }

        /*public static List<Component> GetComponentListOneType(int type , int uid)
        {
            //TDO
        }

        public static Timer GetTimer(int uid)
        {
            //TDO
        }*/

        public static void InsertComponent(Link linkElement, int uid , string password)
        {
            DirectoryInfo userFolder = new DirectoryInfo(applicationPath.ToString() + @"\" + uid);
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, Helper.Get16CharPassword(password));
            File.Delete(encrypfile.FullName);

            

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            FileStream tmpFile = new FileStream(appDataUsrFile.FullName, FileMode.Open, FileAccess.ReadWrite);
            XElement linksNode = (from xnode in userXML.Descendants("Links") select xnode).FirstOrDefault();

            XElement link = new XElement("Link");
            XElement userId = new XElement("UserId", uid);
            XElement linkId = new XElement("LinkId", linkElement.IdLink);
            XElement linkText = new XElement("LinkText", linkElement.LinkText);
            XElement text = new XElement("Text", linkElement.Text);
            XElement nrOrd = new XElement("NrOrd", NrLink + 1);
            XElement date = new XElement("DateAndTime", linkElement.Date);
            link.Add(linkId, userId, linkText, text, nrOrd, date);
            linksNode.Add(link);


            userXML.Save(tmpFile);

            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, Helper.Get16CharPassword(password));

            tmpFile.Close();
            File.Delete(appDataUsrFile.FullName);
        }

        public static void InsertComponent(Note noteElement, int uid, string password)
        {
            DirectoryInfo userFolder = new DirectoryInfo(applicationPath.ToString() + @"\" + uid);
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, Helper.Get16CharPassword(password));
            File.Delete(encrypfile.FullName);

            FileStream tmpFile = new FileStream(appDataApplicationPath.FullName, FileMode.Open, FileAccess.ReadWrite);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);

            XElement notesNode = (from xnode in userXML.Descendants("Notes") select xnode).FirstOrDefault();

            XElement note = new XElement("Note");
            XElement userId = new XElement("UserId", uid);
            XElement noteId = new XElement("NoteId", noteElement.IdNote);
            XElement noteTitle = new XElement("Title", noteElement.Title);
            XElement text = new XElement("Text", noteElement.Text);
            XElement nrOrd = new XElement("NrOrd", NrNote + 1);
            XElement date = new XElement("DateAndTime", noteElement.Date);
            note.Add(noteId, userId, noteTitle, text, nrOrd, date);
            notesNode.Add(note);

            userXML.Save(tmpFile);

            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, Helper.Get16CharPassword(password));
            tmpFile.Close();
            File.Delete(appDataUsrFile.FullName);
        }

        public static void InsertComponent(Reminder reminderElement, int uid , string password)
        {
            DirectoryInfo userFolder = new DirectoryInfo(applicationPath.ToString() + @"\" + uid);
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, Helper.Get16CharPassword(password));
            File.Delete(encrypfile.FullName);

            FileStream tmpFile = new FileStream(appDataApplicationPath.FullName, FileMode.Open, FileAccess.ReadWrite);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);

            XElement remindersNode = (from xnode in userXML.Descendants("Reminders") select xnode).FirstOrDefault();

            XElement reminder = new XElement("Reminder");
            XElement userId = new XElement("UserId", uid);
            XElement reminderId = new XElement("ReminderId", reminderElement.IdREm);
            XElement text = new XElement("Text", reminderElement.Text);
            XElement nrOrd = new XElement("NrOrd", NrRem + 1);
            XElement date = new XElement("DateAndTime", reminderElement.Date);
            reminder.Add(reminderId, userId, text, nrOrd, date);
            remindersNode.Add(reminder);

            userXML.Save(tmpFile);

            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, Helper.Get16CharPassword(password));
            tmpFile.Close();
            File.Delete(appDataUsrFile.FullName);
        }

        public static void InsertComponent(Timer timerElement, int uid, string password)
        {
            DirectoryInfo userFolder = new DirectoryInfo(applicationPath.ToString() + @"\" + uid);
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, Helper.Get16CharPassword(password));
            File.Delete(encrypfile.FullName);

            FileStream tmpFile = new FileStream(appDataApplicationPath.FullName, FileMode.Open, FileAccess.ReadWrite);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);

            XElement timerNode = (from xnode in userXML.Descendants("Timer") select xnode).FirstOrDefault();

            XElement hours = new XElement("Hours", timerElement.Hours);
            XElement minutes = new XElement("Minutes", timerElement.Minutes);
            XElement seconds = new XElement("Seconds", timerElement.Seconds);
            XElement timerId = new XElement("TimerId", timerElement.TimerId);

            timerNode.Add(hours , minutes , seconds , timerId);

            userXML.Save(tmpFile);

            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, Helper.Get16CharPassword(password));
            tmpFile.Close();
            File.Delete(appDataUsrFile.FullName);
        }

        public static void InsertComponent(ToDo toDoElement, int uid, string password)
        {
            DirectoryInfo userFolder = new DirectoryInfo(applicationPath.ToString() + @"\" + uid);
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(Path.Combine(appDataApplicationPath.ToString(), uid.ToString()) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, Helper.Get16CharPassword(password));
            File.Delete(encrypfile.FullName);

            FileStream tmpFile = new FileStream(appDataApplicationPath.FullName, FileMode.Open, FileAccess.ReadWrite);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);

            XElement linksNode = (from xnode in userXML.Descendants("Links") select xnode).FirstOrDefault();

            XElement link = new XElement("Link");
            XElement userId = new XElement("UserId", uid);
            XElement linkId = new XElement("LinkId", linkElement.IdLink);
            XElement linkText = new XElement("LinkText", linkElement.LinkText);
            XElement text = new XElement("Text", linkElement.Text);
            XElement nrOrd = new XElement("NrOrd", NrLink + 1);
            XElement date = new XElement("DateAndTime", linkElement.Date);
            link.Add(linkId, userId, linkText, text, nrOrd, date);
            linksNode.Add(link);

            userXML.Save(tmpFile);

            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, Helper.Get16CharPassword(password));
            tmpFile.Close();
            File.Delete(appDataUsrFile.FullName);
        }

        public static bool UserExists(string userName, string password)
        {

            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();
            XName userNamex = "UserName";

            foreach(DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                foreach(FileInfo userFile in userFileList)
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

        public static UserManager.UserInfo GetUserInfo(string userName, string password)
        {

            UserManager.UserInfo user = new UserManager.UserInfo();
            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();


            foreach (DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                foreach (FileInfo userFile in userFileList)
                {
                    string userAppDataApplicationFile = Path.Combine(appDataApplicationPath.ToString() + @"\"+ userFile.Name.Substring(0 , userFile.Name.IndexOf('.')), userFile.Name);

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
                            else
                            {
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

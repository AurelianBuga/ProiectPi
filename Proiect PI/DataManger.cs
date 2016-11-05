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



namespace DataManager
{
    public static class Helper
    {
        public static bool IsSpace(char ch)
        {
            return (ch == ' ');
        }

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

        public static DateTime GetDatePreview(DateTime dateAndTime)
        {
            //TDO
            return dateAndTime;
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

    public static class DBConnection
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

        public static List<string>[] GetOneTypeComponentList(Components.Component.componentType comp, int UID)
        {
            List<string>[] list = new List<string>[4];
            for (int i = 0; i < 4; i++)
                list[i] = new List<string>();

            switch (comp)
            {
                case Components.Component.componentType.reminder:
                    list = GetComponentList3("reminder", "NRORD", "REMINDERTEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.componentType.note:
                    list = GetComponentList4("note", "NRORD", "NOTETITLE", "NOTETEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.componentType.todo:
                    list = GetComponentList4("todo", "NRORD", "STATUSCHECK", "TODOTEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.componentType.link:
                    list = GetComponentList4("link", "NRORD", "TEXT", "LINKTEXT", "DATEANDTIME", UID.ToString());
                    break;

                case Components.Component.componentType.timer:
                    list = GetComponentList4("timer", "TIMERTEXT", "HOURS", "MINUTES", "SECONDS", UID.ToString());
                    break;
            }
            return list;
        }

        public static List<string>[] GetComponentList4(string tip, string col1, string col2, string col3, string col4, string UID)
        {
            List<string>[] list = new List<string>[4];

            for (int i = 0; i < 4; i++)
                list[i] = new List<string>();

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM " + tip + " WHERE USERID = " + UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[col1] + "");
                    list[1].Add(dataReader[col2] + "");
                    list[2].Add(dataReader[col3] + "");
                    list[3].Add(dataReader[col4] + "");
                }

                dataReader.Close();

                CloseConnection();
            }
            return list;
        }

        public static List<string>[] GetComponentList3(string tip, string col1, string col2, string col3, string UID)
        {
            List<string>[] list = new List<string>[3];

            for (int i = 0; i < 3; i++)
                list[i] = new List<string>();

            if (OpenConnection() == true)
            {
                string query = "SELECT * FROM " + tip + " WHERE USERID = " + UID;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[col1] + "");
                    list[1].Add(dataReader[col2] + "");
                    list[2].Add(dataReader[col3] + "");
                }

                dataReader.Close();

                CloseConnection();
            }
            return list;
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

        public static void InsertReminder(int UID, int remID, string dateAndTime, string text, int nrOrdine)
        {
            string query = "INSERT INTO reminder (REMINDERID ,  USERID , DATEANDTIME , REMINDERTEXT , NRORD) VALUES( '" +
                            remID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + nrOrdine + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertNote(int UID, int noteID, string dateAndTime, string text, string title, int nrOrdine)
        {
            string query = "INSERT INTO note (NOTEID ,  USERID , DATEANDTIME , NOTETEXT , NOTETITLE , NRORD) VALUES( '" +
                            noteID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + title + "' , '" + nrOrdine + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertToDo(int UID, int toDoID, string dateAndTime, string text, int statusCheck, int nrOrdine)
        {
            string query = "INSERT INTO todo (TODOID ,  USERID , DATEANDTIME , TODOTEXT , STATUSCHECK , NRORD) VALUES( '" +
                            toDoID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + statusCheck + "' , '" + nrOrdine + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertLink(int UID, int linkID, string dateAndTime, string text, string linkText, int nrOrdine)
        {
            string query = "INSERT INTO link (LINKID ,  USERID , DATEANDTIME , TEXT , LINKTEXT , NRORD) VALUES( '" +
                            linkID + "' ,'" + UID + "' , '" + dateAndTime + "' , '" + text + "' , '" + linkText + "' , '" + nrOrdine + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void InsertTimer(int UID, int timerID, string text, int hours, int minutes, int seconds)
        {
            string query = "INSERT INTO timer (TIMERID , USERID , TIMERTEXT , HOURS , MINUTES , SECONDS) VALUES( '" +
                            timerID + "' ,'" + UID + "' , '" + text + "' , '" + hours + "' , '" + minutes + "' , '" + seconds + "')";

            ExecuteNonQueryCommand(query);
        }

        public static void DeleteComponent(int componentID, string componentType)
        {
            string query = "DELETE FROM " + componentType + " WHERE REMINDERID = " + componentID;

            ExecuteNonQueryCommand(query);
        }

        public static int Count(int uid, Components.Component.componentType compType)
        {
            string query = "SELECT Count(*) FROM " + compType.ToString() + " WHERE USERID = " + uid;
            int Count = -1;

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                CloseConnection();

                return Count;
            }
            else
            {
                return Count;
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
                    userInfo.uId = reader["UID"].ToString();
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
            string query = "INSERT INTO `proiect`.`users` (`UID`, `FNAME`, `LNAME`, `USERNAME`, `EMAIL`, `PWD`) VALUES" +
                           " ('" + userInfo.uId + "', '" + userInfo.fName + "', '" + userInfo.lName + "', '" + userInfo.userName +
                           "', '" + userInfo.email + "', '" + userInfo.password + "');";

            ExecuteNonQueryCommand(query);
        }

        /*public static  void UpdateComponent()
        {
            //TDO /// un fel de refresh
        }

        public static  void UpdateUserInfo()
        {

        }

        public static  void Restore()
        {
            TDO /// info se vor extrage din x
        }*/
    }

    static class XMLManager
    {
        /*TDO:
        - a method that create a folder(for each user) wich contains XML files(one for each type of component) for users (if they don't exists) 
        - a method that add content to XML files created for that user and component type .....when that component is created ( it is saved in xml file and DB ) 
        - a method that  clear content in XML files ... when a component is deleted ( it is deleted form XML file and DB)
        - a method that update content in XML files ... when a component is updated ( it is updated in XML file and DB)
        - a getter method that returns a List<string>[] for a component type and user
        - a method that count elements in a XML file 
        - additional : 
            - a method that make a backup of all files for an user
            - a method that restore files for an user for backup
         */

        public static DirectoryInfo applicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Documents\MyApplication"));
        public static DirectoryInfo appDataApplicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication"));


        public static string CreateApplicationFolder()
        {
            /// daca nu exista folder pt aplicatie se va creea 
            /// retureaza calea catre acest folder


            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string applicationFolderPath = @"Documents\MyApplication";
            string path = Path.Combine(userProfilePath, applicationFolderPath);
            DirectoryInfo applicationDirectory = new DirectoryInfo(path);

            if (!applicationDirectory.Exists)
            {
                Directory.CreateDirectory(applicationDirectory.ToString());
            }

            return path;
        }

        public static string CreateUserFolder(string uid)
        {
            /// daca nu exista folder pt user se va creea 
            /// returneaza calea catre acest folder


            string applicationDirectory = CreateApplicationFolder();

            string folderName = "uid" + uid;
            string folderPath = applicationDirectory.ToString() + @"\" + folderName;
            DirectoryInfo userFolderDirectory = new DirectoryInfo(folderPath);

            if (!userFolderDirectory.Exists)
            {
                Directory.CreateDirectory(userFolderDirectory.ToString());
            }

            return folderPath;
        }

        public static string CreateAppDataUsrFile(string uid)
        {
            string appDataApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication");
            DirectoryInfo appDataAppDir = new DirectoryInfo(appDataApplicationPath);

            if (!appDataAppDir.Exists)
                Directory.CreateDirectory(appDataApplicationPath);

            return appDataApplicationPath;
        }

        public static void CreateUsrXMLFile(UserManager.UserInfo userInfo)
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            string fileName = userInfo.uId.ToString();
            string userFilePath = Path.Combine(CreateUserFolder(userInfo.uId), fileName + ".xml");
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


                XElement content = new XElement("Content");
                content.Add(new XElement("Reminders", string.Empty));
                content.Add(new XElement("Notes", string.Empty));
                content.Add(new XElement("Links", string.Empty));
                content.Add(new XElement("ToDos", string.Empty));
                userDoc.Add(content);
                xmlUser.Add(userDoc);



                string tmpUsrFilePath = Path.Combine(CreateAppDataUsrFile(userInfo.uId), fileName + ".xml");

                FileStream write = new FileStream(tmpUsrFilePath, FileMode.Create, FileAccess.Write);
                xmlUser.Save(write);
                write.Close();

                //--------------2-------------------
                EncryptManager.EncryptFile(tmpUsrFilePath, userFilePath, fileName + fileName + fileName + fileName);

                //--------------3-------------------
                File.Delete(tmpUsrFilePath);
                write.Close();
            }
        }


        public static void AddComponent(Components.Link linkElement, string uid)
        {
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO - ar trebui sa existe o metoda care sa returneze XDocumentul


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }


        public static void AddComponent(Components.Note noteElement, string uid)
        {
            //TDO
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }

        public static void AddComponent(Components.Reminder reminderElement, string uid)
        {
            //TDO
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }

        public static void AddComponent(Components.Timer timerElement, string uid)
        {
            //TDO
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }

        public static void AddComponent(Components.ToDo toDoElement, string uid)
        {
            //TDO
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile = userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }

        /*public static bool UserExists(string userName, string password)
        {
            //DirectoryInfo applicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Documents\MyApplication"));
            // appDataApplicationPath = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication"));
            DirectoryInfo[] userFoldersList = applicationPath.GetDirectories();
            foreach(DirectoryInfo userFolder in userFoldersList)
            {
                FileInfo[] userFileList = userFolder.GetFiles();
                //TDO
            }
        }

        public UserManager.UserInfo GetUserInfo(string userName, string password)
        {
            //TDO
        }*/
    }

    static class EncryptManager
    {
        // cand se va creea un cont se va creea fisierul xml ( care va avea ca nume UID-ul user-ului)
        // in acest fisier se vor salva datele user-ului ( la fel ca in DB )
        // dupa....vor fi criptate... cheia fiind parola ...introdusa de user ....la logare se decripteaza fisierul

        public static void EncryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
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

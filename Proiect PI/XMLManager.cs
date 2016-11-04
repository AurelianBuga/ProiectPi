using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;


namespace Proiect_PI
{
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

        public static string CreateUserFolder(int uid)
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

        public static string CreateAppDataUsrFile(int uid)
        {
            string appDataApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local\MyApplication");
            DirectoryInfo appDataAppDir = new DirectoryInfo(appDataApplicationPath);

            if (!appDataAppDir.Exists)
                Directory.CreateDirectory(appDataApplicationPath);

            return appDataApplicationPath;
        }

        public static void CreateUsrXMLFile(UserManager.User user)
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            string fileName = user.UId.ToString();
            string userFilePath = Path.Combine(CreateUserFolder(user.UId), fileName + ".xml");
            DirectoryInfo userFileDir = new DirectoryInfo(userFilePath);

            if (!userFileDir.Exists)
            {
                /// se va creea xml-ul user-ului 
                /// pas : 1. se creaza un fisier temporar in AppData
                /// pas : 2. se cripteaza fisierul , cheia fiind uid-ul userului , si se pune in folder-ul user-ului din folder-ul aplicatiei
                /// pas : 3. se sterge fisierul temporar din AppData

                //------------1---------------------------
                XDocument xmlUser = new XDocument();
                XElement userDoc = new XElement("User" , string.Empty);

                userDoc.SetAttributeValue("UID", user.UId);
                userDoc.SetAttributeValue("FName", user.FName);
                userDoc.SetAttributeValue("LName", user.LName);
                userDoc.SetAttributeValue("UserName", user.UserName);
                userDoc.SetAttributeValue("Email", user.Email);
                userDoc.SetAttributeValue("Password", user.Password);
                

                XElement content = new XElement("Content");
                content.Add(new XElement("Reminders", string.Empty));
                content.Add(new XElement("Notes", string.Empty));
                content.Add(new XElement("Links", string.Empty));
                content.Add(new XElement("ToDos", string.Empty));
                userDoc.Add(content);
                xmlUser.Add(userDoc);

                

                string tmpUsrFilePath = Path.Combine(CreateAppDataUsrFile(user.UId), fileName + ".xml");

                FileStream write = new FileStream( tmpUsrFilePath , FileMode.Create, FileAccess.Write);
                xmlUser.Save(write);
                write.Close();

                //--------------2-------------------
                EncryptManager.EncryptFile(tmpUsrFilePath, userFilePath, fileName + fileName + fileName + fileName);

                //--------------3-------------------
                File.Delete(tmpUsrFilePath);
                write.Close();
            }
        }


        public static void AddComponent(Components.Link linkElement , int uid)
        {
            DirectoryInfo userFolder = new DirectoryInfo(CreateUserFolder(uid));
            FileInfo encrypfile =  userFolder.GetFiles(uid + ".xml").FirstOrDefault();
            FileInfo appDataUsrFile = new FileInfo(CreateAppDataUsrFile(uid) + @"\" + uid + ".xml");

            EncryptManager.DecryptFile(encrypfile.FullName, appDataUsrFile.FullName , uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(encrypfile.FullName);

            XDocument userXML = XDocument.Load(appDataUsrFile.FullName);
            //TDO - ar trebui sa existe o metoda care sa returneze XDocumentul


            EncryptManager.EncryptFile(appDataUsrFile.FullName, encrypfile.FullName, uid.ToString() + uid.ToString() + uid.ToString() + uid.ToString());
            File.Delete(appDataUsrFile.FullName);
        }


        public static void AddComponent(Components.Note noteElement, int uid)
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

        public static void AddComponent(Components.Reminder reminderElement, int uid)
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

        public static void AddComponent(Components.Timer timerElement, int uid)
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

        public static void AddComponent(Components.ToDo toDoElement, int uid)
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
    }
}

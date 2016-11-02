using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;


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

        public static void CreateUsrXMLFile(List<string> dataUser )
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            string fileName = dataUser[0];
            string userFilePath = Path.Combine(CreateUserFolder(dataUser[0]), fileName + ".xml"); 
            DirectoryInfo userFileDir = new DirectoryInfo(userFilePath);

            if (!userFileDir.Exists)
            {
                /// se va creea xml-ul user-ului 
                /// pasi : 1. se creaza un fisier temporar in AppData
                ///        2. se cripteaza fisierul , cheia fiind uid-ul userului , si se pune in folder-ul user-ului din folder-ul aplicatiei
                ///        3. se sterge fisierul temporar din AppData

                //------------1---------------------------
                XDocument xmlUser = new XDocument();
                XElement user = new XElement("User");

                user.SetAttributeValue("UID", dataUser[0]);
                user.SetAttributeValue("FName", dataUser[1]);
                user.SetAttributeValue("LName", dataUser[2]);
                user.SetAttributeValue("UserName", dataUser[3]);
                user.SetAttributeValue("Email", dataUser[4]);
                user.SetAttributeValue("Password", dataUser[5]);
                xmlUser.Add(user);

                string appDataApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\Local");
                DirectoryInfo appDataAppDir = new DirectoryInfo(appDataApplicationPath);

                if (!appDataAppDir.Exists)
                    Directory.CreateDirectory(appDataApplicationPath);

                string tmpUsrFilePath = Path.Combine(appDataApplicationPath, fileName + ".xml");

                FileStream write = new FileStream( tmpUsrFilePath , FileMode.Create, FileAccess.Write);
                xmlUser.Save(write);
                write.Close();

                //--------------2-------------------
                EncryptManager.EncryptFile(tmpUsrFilePath, userFilePath, fileName + fileName);
                EncryptManager.DecryptFile(userFilePath, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\exemplu.xml", fileName + fileName);
                int i = 5;
                //--------------3-------------------
                //Directory.Delete(tmpUsrFilePath);
            }
        }




        /*public void DeleteUserFolder()
        {
            //TDO
        }

        /*public string GetUserName(int uid)
        {
            string userName;

            //TDO

            return userName;
        }*/


    }
}

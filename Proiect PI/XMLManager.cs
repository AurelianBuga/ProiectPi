using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace Proiect_PI
{
    class XMLManager
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

        private DirectoryInfo applicationDirectory;
        private DirectoryInfo userFolderDirectory;
        private int uid;

        public XMLManager(int uid)
        {
            this.uid = uid;

            // set applicationDirectory
            string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string applicationFolderPath = @"Documents\MyApplication";
            string path = Path.Combine(userProfilePath, applicationFolderPath);
            applicationDirectory = new DirectoryInfo(path);

            // set userFolderDirectory
            string folderName = "uid" + uid;
            string folderPath = applicationDirectory.ToString() + @"\" + folderName;
            userFolderDirectory = new DirectoryInfo(folderPath);

            // create application and user folder , if they not exists
            CreateApplicationFolder();
            CreateUserFolder();
        }

        public void CreateApplicationFolder()
        {
            if (!applicationDirectory.Exists)
            {
                Directory.CreateDirectory(applicationDirectory.ToString());
            }
        }

        public void CreateUserFolder()
        {
            if (!userFolderDirectory.Exists)
            {
                Directory.CreateDirectory(userFolderDirectory.ToString());
            }
        }

        public void CreateUsrXMLFile()
        {
            //IDEA : datele din fisier vor fi criptate /// TDO : cauta metoda de criptare a fisierelor
            // Fiecare user va avea un fisier criptat

            string fileName = uid + ".txt";
            string userFilePath = Path.Combine(userFolderDirectory.ToString(), fileName); ///VEZI CE RETURNEAZA
            DirectoryInfo userFileDir = new DirectoryInfo(userFilePath);

            if (!userFileDir.Exists)
            {
                
            }


        }




        public void DeleteUserFolder()
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

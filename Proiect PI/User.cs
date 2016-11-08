using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager;


namespace UserManager
{

    public struct UserInfo
    {
        public int uId;
        public string fName;
        public string lName;
        public string userName;
        public string email;
        public string password;
    }

    /// <summary>
    /// Clasa User este de tip singleton
    /// </summary>
    public sealed class User
    {
        private int uId;
        private string fName;
        private string lName;
        private string userName;
        private string email;
        private string password; // trebuie sa fie minim de 8 caractere
        public bool loginType; // true = online , false = offline

        private static readonly User userInstance = new User();

        public int UID
        {
            get { return uId; }
        }

        public string FName
        {
            get { return fName; }
        }

        public string LName
        {
            get { return lName; }
        }

        public string UserName
        {
            get { return UserName; }
        }

        public string Email
        {
            get { return email; }
        }

        public string Pasword
        {
            get { return password; }
        }

        private User()
        {
            
        }

        public static  User UserInstance
        {
            get
            {
                return userInstance;
            }
            
            
        }

        // Login() method return true if username & password are valid
        // else return false
        public bool Login(string userName , string password)
        {
            // if internet access exists -> login online
            if (Helper.CheckForInternetConnection())
            {
                if (DBManager.UserExists(userName, password))
                {
                    //se verifica daca User-ul exista in baza de date
                    //se se pun valorile din baza de date
                    UserInfo userInfo = DataManager.DBManager.GetUserInfo(userName, password);
                    userInstance.email = userInfo.email;
                    userInstance.fName = userInfo.fName;
                    userInstance.lName = userInfo.lName;
                    userInstance.userName = userInfo.userName;
                    userInstance.uId = userInfo.uId;
                    userInstance.password = userInfo.password;
                    userInstance.loginType = true;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            //else -> login offline /// va aparea un window care sa intrebe user-ul daca 
            // doreste se se logeze offline
            else
            {
                if (XMLManager.UserExists(userName, password))
                {
                    //se creeaza obiect User
                    //se verifica daca User-ul exista in XML-uri
                    //se se pun valorile din XML
                    UserInfo userInfo = XMLManager.GetUserInfo(userName, password);
                    userInstance.email = userInfo.email;
                    userInstance.fName = userInfo.fName;
                    userInstance.lName = userInfo.lName;
                    userInstance.userName = userInfo.userName;
                    userInstance.uId = userInfo.uId;
                    userInstance.password = userInfo.password;
                    userInstance.loginType = false;

                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
        }

        public void LogOut()
        {
            //TDO
        }

        public void Register(UserInfo userInfo)
        {
            // if internet access exists -> register online
            if (DataManager.Helper.CheckForInternetConnection())
            {
                DBManager.AddUser(userInfo);
            }
            //else -> register offline /// va aparea un window care sa intrebe user-ul daca 
            // doreste se se logeze offline
            else
            {
                userInfo.uId = DataManager.Helper.GetUID();
                XMLManager.CreateUserDirsAndFiles(userInfo);
            }
        }
     }
}

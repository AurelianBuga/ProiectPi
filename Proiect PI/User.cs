using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    /// Clasa User este de tip singleton : o singura instanta poate fi creeata
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
            if (DataManager.Helper.CheckForInternetConnection())
            {
                if (DataManager.DBConnection.UserExists(userName, password))
                {
                    //se verifica daca User-ul exista in baza de date
                    //se se pun valorile din baza de date
                    UserInfo userInfo = DataManager.DBConnection.GetUserInfo(userName, password);
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
                if (DataManager.XMLManager.UserExists(userName, password))
                {
                    //se creeaza obiect User
                    //se verifica daca User-ul exista in XML-uri
                    //se se pun valorile din XML
                    UserInfo userInfo = DataManager.XMLManager.GetUserInfo(userName, password);
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

        public void Register(UserInfo userInfo)
        {
            // if internet access exists -> register online
            if (DataManager.Helper.CheckForInternetConnection())
            {
                DataManager.DBConnection.AddUser(userInfo);
            }
            //else -> register offline /// va aparea un window care sa intrebe user-ul daca 
            // doreste se se logeze offline
            else
            {
                DataManager.XMLManager.CreateUserDirsAndFiles(userInfo);
            }
        }
     }
}

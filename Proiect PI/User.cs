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
using System.Windows.Shapes;
using DataManager;
using Proiect_PI;

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
            get { return userName; }
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
        public int Login(string userName , string password , bool afterOffReg)
        {
            //daca deja s-a facut inregistrarea userlui offline
            if (afterOffReg)
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

                    return 1;
                }
                else
                {
                     return 0;
                }
            }
            else 
            {
                // if internet access exists -> login online
                if (Helper.CheckForInternetConnection())
                {
                    if (DBManager.UserExists(userName, password))
                    {
                        //se verifica daca User-ul exista in baza de date
                        //se se pun valorile din baza de date
                        UserInfo userInfo = DBManager.GetUserInfo(userName, password);
                        userInstance.email = userInfo.email;
                        userInstance.fName = userInfo.fName;
                        userInstance.lName = userInfo.lName;
                        userInstance.userName = userInfo.userName;
                        userInstance.uId = userInfo.uId;
                        userInstance.password = userInfo.password;
                        userInstance.loginType = true;

                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
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

                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
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
            if (Helper.CheckForInternetConnection())
            {
                DBManager.AddUser(userInfo);
            }
            //else -> register offline /// va aparea un window care sa intrebe user-ul daca 
            // doreste se se logeze offline
            else
            {
                userInfo.uId = GetUID();
                XMLManager.CreateUserDirsAndFiles(userInfo);
            }
        }

        public static int GetUID()
        {
            Random randGen = new Random();
            int uidTest = -1;
            do
            {
                uidTest = randGen.Next(1000, 9999);
            } while (XMLManager.UserExists(uidTest));

            return uidTest;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserManager
{

    public struct UserInfo
    {
        public string uId;
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
        private string uId;
        private string fName;
        private string lName;
        private string userName;
        private string email;
        private string password;
        public bool loginType; // true = online , false = offline
        private static readonly User userInstance = new User();

        private User()
        {
            
        }

        //TDO : trebuie sterse
        /*public int UId { get { return this.uId; } set { this.uId = value;} }
        public string FName { get { return this.fName; } set { this.fName = value; } }
        public string LName { get { return this.lName; } set { this.lName = value;} }
        public string UserName { get { return this.userName; } set { this.userName = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public string Password { get { return this.password; } set { this.password = value;  } }*/

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
                DB.DBConnection conn = new DB.DBConnection();
                if (conn.UserExists(userName, password))
                {
                    //se verifica daca User-ul exista in baza de date
                    //se se pun valorile din baza de date
                    UserInfo userInfo = conn.GetUserInfo(userName, password);
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
                if (UserExists)
                {
                    //se creeaza obiect User
                    //se verifica daca User-ul exista in XML-uri
                    //se se pun valorile din baza de date 

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
            if (Helper.CheckForInternetConnection())
            {
                DB.DBConnection conn = new DB.DBConnection();
                conn.AddUser(userInfo);
            }
            //else -> register offline /// va aparea un window care sa intrebe user-ul daca 
            // doreste se se logeze offline
            else
            {

            }
        }
     }


}

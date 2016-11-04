using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserManager
{

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
        private string password;
        private bool loginType;
        private static readonly User userInstance = new User();
        // va fi folosita pt output-ul metodei Login() // daca user.emptyUserInstance == true atunci User-ul nu exista in baza de date / XML-uri
        public static bool emptyUserInstance = true;

        private User()
        {
            
        }

        //TDO : validare date introduse
        public int UId { get { return this.uId; } set { this.uId = value; emptyUserInstance = false; } }
        public string FName { get { return this.fName; } set { this.fName = value; emptyUserInstance = false; } }
        public string LName { get { return this.lName; } set { this.lName = value; emptyUserInstance = false; } }
        public string UserName { get { return this.userName; } set { this.userName = value; emptyUserInstance = false; } }
        public string Email { get { return this.email; } set { this.email = value; emptyUserInstance = false; } }
        public bool LoginType { get { return this.loginType; } set { this.loginType = value; emptyUserInstance = false; } }
        public string Password { get { return this.password; } set { this.password = value; emptyUserInstance = false; } }

        public static  User UserInstance
        {
            get
            {
                return userInstance;
            }
            
            
        }

        /*public User Login(string userName , string password)
        {
            // if internet access exists -> login online
            if (Helper.CheckForInternetConnection())
            {
                loginType = true;
                if (accountExists)
                {
                    //se creeaza obiect User
                    //se verifica daca User-ul exista in baza de date
                    //se se pun valorile din baza de date 
                    User user =  UserInstance;

                    return user;
                }    
            }
            //else -> login offline
            else
            {
                loginType = false;
                if (accountExists)
                {
                    //se creeaza obiect User
                    //se verifica daca User-ul exista in baza de date
                    //se se pun valorile din baza de date 
                    User user = UserInstance;

                    return user;
                }
            }
            return true;
        }*/
     }
}

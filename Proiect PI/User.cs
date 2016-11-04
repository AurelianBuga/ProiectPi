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

        private User()
        {
            
        }

        public int UId { get { return this.uId; } set { this.uId = value; } }
        public string FName { get { return this.fName; } set { this.fName = value; } }
        public string LName { get { return this.lName; } set { this.lName = value; } }
        public string UserName { get { return this.userName; } set { this.userName = value; } }
        public string Email { get { return this.email; } set { this.email = value; } }
        public bool LoginType { get { return this.loginType; } set { this.loginType = value; } }
        public string Password { get { return this.password; } set { this.password = value; } }

        public static  User UserInstance
        {
            get
            {
                return userInstance;
            }
            
            
        }

        public bool OfflineLogin(string userName , string password)
        {
            loginType = false;
            //TDO
            return true;
        }

        public bool OnlineLogin(string userName , string password)
        {
            loginType = true;
            //TDO
            return true;
        }

        



     }
}

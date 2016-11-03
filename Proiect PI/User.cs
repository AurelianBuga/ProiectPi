using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UserManager
{
    class User
    {
        private int uId;
        private string fName;
        private string lName;
        private string userName;
        private string email;
        private string password;
        private bool loginType;

        private User(int uId , string fName , string lName , string userName , string email , string password)
        {
            this.uId = uId;
            this.fName = fName;
            this.lName = lName;
            this.userName = userName;
            this.email = email;
            this.password = password;
        }

        public int UId { get { return this.uId; } }
        public string FName { get { return this.fName; } }
        public string LName { get { return this.lName; } }
        public string UserName { get { return this.userName; } }
        public string Email { get { return this.email; } }
        public bool LoginType { get { return this.loginType; } }
        public string Password { get { return this.password; } }

        /// <summary>
        /// Pentru testare------!!!!
        /// </summary>
        /// <returns></returns>
        public static User CreateUser()
        {
            User user = new User(5970, "fname", "lname", "aquatrick", "@yahoo", "pass");
            return user;
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

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

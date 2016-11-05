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

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }


        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TDO validate username , username unique
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TDO validate email , username email
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserManager.User usr = UserManager.User.UserInstance;
            UserManager.UserInfo userInfo = new UserManager.UserInfo();

            userInfo.email = email.Text;
            userInfo.fName = fName.Text;
            userInfo.lName = lName.Text;
            userInfo.password = passwordBox.Password;
            userInfo.userName = username.Text;
            userInfo.uId = DataManager.Helper.GetUID();

            usr.Register(userInfo);

            this.Hide();
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }
    }
}

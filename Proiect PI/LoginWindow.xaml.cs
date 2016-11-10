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

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserManager.User usr = UserManager.User.UserInstance;
            //cu internet
            if (Helper.CheckForInternetConnection())
            {
                if(usr.Login(usr.UserName , usr.Pasword , false) == 1)
                {
                    MainWindow mn = new MainWindow();
                    mn.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong password or username !");
                }
               
            }
            else // fara internet
            {
                //usr.Login(username.Text, passwordBox.Password, true);
                ErrInternetConn win = new ErrInternetConn(username.Text, passwordBox.Password);
                win.Show();
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            WelcomeWindow ww = new WelcomeWindow();
            ww.Show();
            this.Close();
        }
    }
}

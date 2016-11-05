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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TDO : validare input live
        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TDO : validare input live
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserManager.User usr = UserManager.User.UserInstance;
            if(usr.Login(username.Text , passwordBox.Password))
            {
                this.Hide();
                MainWindow mn = new MainWindow();
                mn.Show();
                this.Close();
            }
        }
    }
}

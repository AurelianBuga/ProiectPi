﻿using System;
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
    /// Interaction logic for ErrInternetConn.xaml
    /// </summary>
    public partial class ErrInternetConn : Window
    {
        //var option este pt a da un feedback window-ului de login cu optiunea aleasa // option == true daca este aleasa optiunea de register , else false
        private string username;
        private string password;

        public ErrInternetConn(string username , string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegisterWindow winReg = new RegisterWindow();
            winReg.Show();
        }

        private void LoginOffline_Click(object sender, RoutedEventArgs e)
        {
            if(UserManager.User.UserInstance.Login(username, password, true) == 1)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Hide();
            }
        }
    }
}

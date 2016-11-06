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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Net;
using DataManager;


namespace Proiect_PI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViewReminderList(object sender, RoutedEventArgs e)
        {
            DBManager.GetComponentListOneType(1, 2);
        }

        private void ViewNoteList(object sender, RoutedEventArgs e)
        {

        }

        private void ViewToDoList(object sender, RoutedEventArgs e)
        {

        }

        private void ViewLinkList(object sender, RoutedEventArgs e)
        {

        }

        private void ViewTimer(object sender, RoutedEventArgs e)
        {

        }
    }
}


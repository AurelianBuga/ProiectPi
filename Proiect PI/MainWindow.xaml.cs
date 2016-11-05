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




namespace TabManager
{
    class  ReminderTab
    {
        public static TabControl CreateReminderTab(TabControl tabIn)
        {
            TabControl tabOut = new TabControl();
            StackPanel stack = new StackPanel();
            Label lab = new Label();
            TabItem itemx = new TabItem();
            tabOut = tabIn;
            Components.Reminder reminder = new Components.Reminder("nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn" , DateTime.Now);

            lab.Content = reminder.PreviewText;
            stack.Children.Add(lab);

            stack.Orientation = Orientation.Horizontal;

            itemx.Header = "Reminder";
            itemx.Content = stack;
            tabOut.Items.Add(itemx);

            return tabOut;
        }
    }
}

namespace Proiect_PI
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void CreateReminderTab(object sender, RoutedEventArgs e)
        {
            tabControl = TabManager.ReminderTab.CreateReminderTab(tabControl);
        }
    }
}


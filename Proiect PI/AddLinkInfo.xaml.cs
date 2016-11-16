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
using UserManager;
using DataManager;
using Components;
using MySql.Data.Types;
using System.ComponentModel;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for AddLinkInfo.xaml
    /// </summary>
    public partial class AddLinkInfo : Window
    {
        private Frame currentFrame;

        public static bool isOpn { get; set; }

        public AddLinkInfo(ref Frame currentFrame)
        {
            InitializeComponent();
            this.currentFrame = currentFrame;

            isOpn = true;
        }

        private void alias_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Link link;
            if (User.UserInstance.loginType)
            {
                //se apeleaza metoda InsertComponent(reminder)
                
                if (aliaTextBox.Text == "" || aliaTextBox.Text == "Alias...")
                {
                    link = new Link(User.UserInstance.UID , LinkTextBox.Text , new MySqlDateTime(DateTime.Now) , DBManager.Count( "link"));
                }
                else
                {
                    link = new Link(User.UserInstance.UID, LinkTextBox.Text, new MySqlDateTime(DateTime.Now), DBManager.Count( "link") , aliaTextBox.Text);
                }
                DBManager.InsertComponent(link);
                currentFrame.Navigate(new LinkListView());
                isOpn = false;
                this.Close();
            }
            else
            {
                if (aliaTextBox.Text == "" || aliaTextBox.Text == "Alias...")
                {
                    link = new Link(User.UserInstance.UID , LinkTextBox.Text , new MySqlDateTime(DateTime.Now) , XMLManager.XMLManagerInstance.Count(XMLManager.compType.link)) ;
                    link.SetID();
                }
                else
                {
                    link = new Link(User.UserInstance.UID, LinkTextBox.Text, new MySqlDateTime(DateTime.Now), XMLManager.NrLink, aliaTextBox.Text);
                    link.SetID();
                }
                XMLManager.XMLManagerInstance.InsertComponent(link);
                currentFrame.Navigate(new LinkListView());
                isOpn = false;
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            isOpn = false;

            base.OnClosing(e);
        }

        private void aliaTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            aliaTextBox.Text = String.Empty;
        }
    }
}

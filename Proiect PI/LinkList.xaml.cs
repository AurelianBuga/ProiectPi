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
using Components;
using System.Collections.ObjectModel;
using DataManager;
using UserManager;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for LinkList.xaml
    /// </summary>
    public partial class LinkListView : Page
    {
        readonly ObservableCollection<Link> links = new ObservableCollection<Link>();

        public LinkListView()
        {
            InitializeComponent();
            DataContext = this;
            LoadList();
        }

        public ObservableCollection<Link> LinkList
        {
            get { return links; }
        }

        public void LoadList()
        {
            LinkList.Clear();

            var linkList = new List<Link>();

            if (User.UserInstance.loginType)
            {
                if (DBManager.OpenConnection())
                {
                    linkList = DBManager.GetLinkList(User.UserInstance.UID);
                    foreach (Link link in linkList)
                    {
                        links.Add(link);
                    }
                }
            }
            else
            {
                //listRem = XMLManager.GetComponentListOneType(1, User.UserInstance.UID);
            }
        }
    }
}

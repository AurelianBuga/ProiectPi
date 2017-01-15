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
    /// Interaction logic for ToDoList.xaml
    /// </summary>
    public partial class ToDoListView : Page
    {
        readonly ObservableCollection<ToDo> toDos = new ObservableCollection<ToDo>();
        private string errorVisibility;

        public ToDoListView()
        {
            DataContext = this;
            LoadList();
            if (toDos.Count == 0)
                errorVisibility = "Visible";
            else
                errorVisibility = "Hidden";
            InitializeComponent();
        }

        public string ErrorVisibility
        {
            get { return errorVisibility; }
        }

        public ObservableCollection<ToDo> ListToDo
        {
            get { return toDos; }
        }

        public void LoadList()
        {
            ListToDo.Clear();

            var listToDo = new List<ToDo>();

            if (User.UserInstance.loginType)
            {
                if (DBManager.OpenConnection())
                {
                    listToDo = DBManager.GetToDoList();
                    foreach (ToDo toDo in listToDo)
                    {
                        toDos.Add(toDo);
                    }

                    NotificationPanel.NotificationPanelInstance.ToDos = toDos.ToList<ToDo>();
                }
            }
            else
            {
                listToDo = XMLManager.XMLManagerInstance.GetToDoList();
                foreach (ToDo toDo in listToDo)
                {
                    toDos.Add(toDo);
                }
            }
        }
    }
}

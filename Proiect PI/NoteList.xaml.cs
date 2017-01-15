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
    /// Interaction logic for NoteList.xaml
    /// </summary>
    public partial class NoteListView : Page
    {
        readonly ObservableCollection<Note> notes = new ObservableCollection<Note>();
        private string errorVisibility;

        public NoteListView()
        {
            DataContext = this;
            LoadList();
            if (notes.Count == 0)
                errorVisibility = "Visible";
            else
                errorVisibility = "Hidden";
            InitializeComponent();
        }

        public string ErrorVisibility
        {
            get { return errorVisibility; }
        }

        public ObservableCollection<Note> ListNote
        {
            get { return notes; }
        }

        public void LoadList()
        {
            ListNote.Clear();
            var listNote = new List<Note>();
            if (User.UserInstance.loginType)
            {
                if (DBManager.OpenConnection())
                {
                    listNote = DBManager.GetNoteList();
                    foreach (Note note in listNote)
                    {
                        notes.Add(note);
                    }
                }
            }
            else
            {
                listNote = XMLManager.XMLManagerInstance.GetNoteList();
                foreach (Note note in listNote)
                {
                    notes.Add(note);
                }
            }
        }
    }
}

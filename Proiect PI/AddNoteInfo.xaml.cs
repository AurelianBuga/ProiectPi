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
using UserManager;
using DataManager;
using Components;
using MySql.Data.Types;

namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for AddNoteInfo.xaml
    /// </summary>
    public partial class AddNoteInfo : Window
    {
        private Frame currentFrame;

        public static bool isOpn { get; set; }

        public AddNoteInfo(ref Frame currentFrame)
        {
            InitializeComponent();
            this.currentFrame = currentFrame;

            isOpn = true;
        }

        private void title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Note note;
            if (User.UserInstance.loginType)
            {
                //se apeleaza metoda InsertComponent(reminder)
                
                if(title.Text == "" || title.Text == "Title...")
                {
                    note = new Note(User.UserInstance.UID, NoteTextBox.Text, new MySqlDateTime(DateTime.Now), DBManager.Count(User.UserInstance.UID, "note"));
                }
                else
                {
                    note = new Note( User.UserInstance.UID, NoteTextBox.Text, new MySqlDateTime(DateTime.Now), DBManager.Count(User.UserInstance.UID, "note") , title.Text);
                }
                DBManager.InsertComponent(note);
                currentFrame.Navigate(new NoteListView());
                isOpn = false;
                this.Hide();
            }
            else
            {
                if (title.Text == "" || title.Text == "Title...")
                {
                    note = new Note(User.UserInstance.UID, NoteTextBox.Text, new MySqlDateTime(DateTime.Now), DBManager.Count(User.UserInstance.UID, "note"));
                    note.SetID();
                }
                else
                {
                    note = new Note(User.UserInstance.UID, NoteTextBox.Text, new MySqlDateTime(DateTime.Now), DBManager.Count(User.UserInstance.UID, "note"), title.Text);
                    note.SetID();
                }
                XMLManager.XMLManagerInstance.InsertComponent(note, User.UserInstance.UID, User.UserInstance.Pasword);
                currentFrame.Navigate(new NoteListView());
                isOpn = false;
                this.Close();
            }
        }

        private void title_GotFocus(object sender, RoutedEventArgs e)
        {
            title.Text = String.Empty;
        }
    }
}
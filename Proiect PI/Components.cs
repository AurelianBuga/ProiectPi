using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IDEA: - sa adaug contacte (nr de telefon , adrese de email)
/// </summary>

namespace Components
{

    public abstract class Component
    {
        private string text;
        private string previewText;
        private int nrOrdine;
        private DateTime dateAndTime;
        public enum componentType { reminder, note, todo, link, timer }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string PreviewText
        {
            get { return this.previewText; }
            set { this.previewText = value; }
        }

        public int NrOrdine
        {
            get { return this.nrOrdine; }
            set { if (value > 0) this.nrOrdine = value; } // sa nu se repete/// de facut un API
        }

        public DateTime DateAndTime
        {
            get { return this.dateAndTime; }
            set { this.dateAndTime = value; }
        }
    }

    public class Reminder : Component
    {
        private DateTime datePreview;

        public Reminder(string text, DateTime dateAndTime)
        {
            Text = text;
            PreviewText = DataManager.Helper.GetPreviewText(text, 30); // cat sa fie afisat /// OP : posibil in functie de dimensiunea window-ului si a textului
            //NrOrdine = Helper.GetNrOrdine("reminder" , );
            DateAndTime = DateTime.Now; //TDO : dateAndTime trebuie setat intr-un date and time picker
            DatePreview = DataManager.Helper.GetDatePreview(DateAndTime);
            /*UserManager.User user = UserManager.User.UserInstance;
            bool val = user.Login("aquatrick", "parola");
            UserManager.UserInfo userInfo = new UserManager.UserInfo();
            userInfo.uId = "5970";
            userInfo.fName = "aurelian";
            userInfo.lName = "buga";
            userInfo.userName = "aquatrick";
            userInfo.email = "email@yahoo.com";
            userInfo.password = "parola";
            DataManager.DBConnection.AddUser(userInfo);
            //List<string>[] list = conn.GetOneTypeComponentList(componentType.reminder, 2);
            //int nr = conn.Count(2, componentType.reminder);
            //conn.CloseConnection();*/

         
            UserManager.UserInfo user = new UserManager.UserInfo();
            user.uId = 1113;
            user.fName = "aurelian";
            user.lName = "buga";
            user.userName = "aquatrick";
            user.email = "@yahoo.com";
            user.password = "aqwertyu";

            UserManager.User usr = UserManager.User.UserInstance;
            usr.Login(user.userName, user.password);
            //DataManager.XMLManager.CreateUsrXMLFile(user);
            //Proiect_PI.XMLManager.AddComponent(link, 5970);
        }

        public DateTime DatePreview
        {
            get { return datePreview; }
            set { datePreview = value; }
        }
    }

    public class Note : Component
    {
        private string title;

        public Note(string text, int nrOrdine)
        {
            Text = text;
            Title = DataManager.Helper.GetPreviewText(text, 1);   ///// sau se pune preview-ul
            NrOrdine = nrOrdine;
            PreviewText = DataManager.Helper.GetFirstWord(text, 30) + "...";// cat sa fie afisat /// OP : posibil in functie de dimensiunea window-ului si a textului
            DateAndTime = DateTime.Now;
        }

        public Note(string title, string text, int nrOrdine)
        {
            Title = title;
            Text = text;
            PreviewText = text.Substring(0, 40) + "...";
            NrOrdine = nrOrdine;
            DateAndTime = DateTime.Now;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }

    public class ToDo : Component
    {
        private bool statusCheck;

        public ToDo(string text, DateTime dateAndTime, int nrOrdine)
        {
            Text = text;
            PreviewText = DataManager.Helper.GetPreviewText(text, 30);  /// IDEA : preview-ul sa se face in timpul executiei .....nu sa fie in constructor
            DateAndTime = dateAndTime; // datePicker
            NrOrdine = nrOrdine;
            StatusCheck = false;
        }

        public bool StatusCheck
        {
            get { return statusCheck; }
            set { statusCheck = value; }
        }
    }

    public class Link : Component
    {
        private string linkText;
        private DateTime datePreview;

        public Link(string text, int nrOrdine, string linkText)
        {
            Text = text;
            PreviewText = DataManager.Helper.GetPreviewText(text, 30); // OP : posibil in functie de dimensiunea window-ului si a textului ///IDEA : preview-ul sa se face in timpul executiei .....nu sa fie in constructor
            NrOrdine = nrOrdine;
            DateAndTime = DateTime.Now;
            DatePreview = DataManager.Helper.GetDatePreview(DateAndTime);
        }

        public string LinkText
        {
            get { return linkText; }
            set { linkText = value; }
        }

        public DateTime DatePreview
        {
            get { return datePreview; }
            set { datePreview = value; }
        }

    }

    public class Timer : Component
    {
        private int hours;
        private int minutes;
        private int seconds;

        // TDO : alerta , refresh times 1/sec

        public Timer(string text, int minutes, int seconds)
        {
            Text = text;
            PreviewText = DataManager.Helper.GetPreviewText(text, 30); // sa aiba o lungime maxima ( text scurt) /// tot la executie nu in constructor
            DateAndTime = DateTime.Now;
            Minutes = minutes;
            Seconds = seconds;
        }

        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;
using DataManager;
using UserManager;

/// <summary>
/// IDEA: - sa adaug contacte (nr de telefon , adrese de email)
/// </summary>

namespace Components
{
    public class TextComponent
    {
        protected string datePreview;
        protected string text;
        protected string previewText;
        protected int nrOrd;
        protected MySqlDateTime dateAndTime;
        protected int idComp;
        protected int idUsr;

        public string Text { get { return this.text; } }
        public string PreviewText { get { return previewText; } }
        public int NrOrd { get { return nrOrd; } }
        public MySqlDateTime Date { get { return dateAndTime; } }
        public int IdComp { get { return idComp; } }
        public int IdUsr { get { return idUsr; } }

        public void ModifyText(string newText)
        {
            text = newText;
            previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public void ModifyDateAndTime(MySqlDateTime newDateAndTime)
        {
            dateAndTime = newDateAndTime;
            datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
        }
    }

    public class Reminder : TextComponent
    {
        public Reminder(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public Reminder(int idRem , int idUsr, string text, MySqlDateTime dateAndTime , int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idRem;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }
    }

    public class Note : TextComponent
    {
        private string title;

        public string Title { get { return title; } }

        public Note()
        {

        }

        public Note(int idNote , int  idUsr , string text , MySqlDateTime dateAndTime , int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idNote;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.title = DataManager.Helper.GetFirstWord(text, 30);   
        }

        public Note(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.title = DataManager.Helper.GetFirstWord(text, 30);
        }

        public Note(int idNote , int idUsr,string text ,MySqlDateTime dateAndTime , int nrOrd , string title)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idNote;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.title = title;
        }

        public Note(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd, string title)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.title = title;
        }


    }

    public class ToDo : TextComponent
    {
        private bool statusCheck;

        public bool StatusCheck { get { return statusCheck; } }

        public ToDo()
        {

        }

        public ToDo(int idToDo , int idUsr,string text , MySqlDateTime dateAndTime, int nrOrd , bool statusCheck)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idToDo;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.statusCheck = statusCheck;
        }

        public ToDo(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd, bool statusCheck)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.statusCheck = statusCheck;
        }

        public void ModifyStatusCheck()
        {
            statusCheck = (!statusCheck);
        }

        public void ChangedStatusCheck()
        {
            if (statusCheck)
            {
                //TDO
                // declansare event (apelare metoda) pt activare window pt selectarea noii date
            }
            else
            {
                //TDO
                // declansare event (apelare metoda) pt activare window de interogare a user-ului daca doreste sa stearga ToDo-ul
            }
        }
    }

    public class Link : TextComponent
    {
        private string linkText;

        public string LinkText { get { return linkText; } }

        public Link()
        {

        }

        public Link(int idLink , int idUsr,string text , MySqlDateTime dateAndTime , int nrOrd , string linkText)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idLink;
            this.idUsr = idUsr;
            this.linkText = linkText;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public Link(int idLink, int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idComp = idLink;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public Link(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd, string linkText)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.linkText = linkText;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public Link(int idUsr, string text, MySqlDateTime dateAndTime, int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

        public void SetID()
        {
            Random idGen = new Random();
            int id;
            while(XMLManager.ComponentExists(id = idGen.Next(), 1, User.UserInstance.UID , User.UserInstance.Pasword) || (id < 1000) || (id > 9999))
            {
                //nothing
            }
            this.idComp = id;
        }

    }

    public sealed class Timer
    {
        private string text;
        private int hours;
        private int minutes;
        private int seconds;
        private int uid;
        private int timerId;
        private readonly static Timer timerInstance = new Timer();

        public string Text
        {
            get { return text; }
        }

        public int Hours
        {
            get { return hours; }
        }

        public int Minutes
        {
            get { return minutes; }
        }

        public int Seconds
        {
            get { return seconds; }
        }

        public int UID
        {
            get { return uid; }
        }

        public int TimerId
        {
            get { return timerId; }
        }


        private Timer()
        {
           
        }

        public static Timer TimerInstance
        {
            get
            {
                return timerInstance;
            }
        }

        public bool TimeLapse()
        {
            
            if(hours == 0 && minutes == 0 && seconds == 0)
            {
                return false; // alerata activata
            }
            else
            {
                //TDO
                return true;
            }
        }

        ///TDO : mecanismul de functionare a timer-ului
        
    }
}

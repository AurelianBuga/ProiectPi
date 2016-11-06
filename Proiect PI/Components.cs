using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

/// <summary>
/// IDEA: - sa adaug contacte (nr de telefon , adrese de email)
/// </summary>

namespace Components
{
    public interface Component
    {
        void ModifyText(string newText);

        void ModifyDateAndTime(MySqlDateTime newDateAndTime);
    }

    public class Reminder : Component
    {
        private string datePreview;
        private string text;
        private string previewText;
        private int nrOrd;
        private MySqlDateTime dateAndTime;
        private int idRem;
        private int idUsr;

        public string Text { get { return text; } }
        public int NrOrd { get { return nrOrd; } }
        public MySqlDateTime Date { get { return dateAndTime; } }
        public int IdREm { get { return idRem; } }
        public int IdUsr { get { return idUsr; } }

        public Reminder(int idRem , int idUsr, string text, MySqlDateTime dateAndTime , int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idRem = idRem;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

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

    public class Note : Component
    {
        private string datePreview;
        private string text;
        private string previewText;
        private int nrOrd;
        private MySqlDateTime dateAndTime;
        private int idNote;
        private int idUsr;
        private string title;

        public string Text { get { return text; } }
        public int NrOrd { get { return nrOrd; } }
        public MySqlDateTime Date { get { return dateAndTime; } }
        public int IdNote { get { return idNote; } }
        public int IdUsr { get { return idUsr; } }
        public string Title { get { return title; } }

        public Note(int idNote , int  idUsr , string text , MySqlDateTime dateAndTime , int nrOrd)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idNote = idNote;
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
            this.idNote = idNote;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.title = title;
        }

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

    public class ToDo : Component
    {
        private string datePreview;
        private string text;
        private string previewText;
        private MySqlDateTime dateAndTime;
        private int nrOrd;
        private int idToDo;
        private int idUsr;
        private bool statusCheck;

        public string Text { get { return text; } }
        public int NrOrd { get { return nrOrd; } }
        public MySqlDateTime Date { get { return dateAndTime; } }
        public int IdToDo { get { return idToDo; } }
        public int IdUsr { get { return idUsr; } }
        public bool StatusCheck { get { return statusCheck; } }

        public ToDo(int idToDo , int idUsr,string text , MySqlDateTime dateAndTime, int nrOrd , bool statusCheck)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idToDo = idToDo;
            this.idUsr = idUsr;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
            this.statusCheck = statusCheck;
        }

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

    public class Link : Component
    {
        private string datePreview;
        private string text;
        private string previewText;
        private MySqlDateTime dateAndTime;
        private int nrOrd;
        private int idLink;
        private int idUsr;
        private string linkText;

        public string Text { get { return text; } }
        public int NrOrd { get { return nrOrd; } }
        public MySqlDateTime Date { get { return dateAndTime; } }
        public int IdLink { get { return idLink; } }
        public int IdUsr { get { return idUsr; } }
        public string LinkText { get { return linkText; } }


        public Link(int idLink , int idUsr,string text , MySqlDateTime dateAndTime , int nrOrd , string linkText)
        {
            this.text = text;
            this.nrOrd = nrOrd;
            this.dateAndTime = dateAndTime;
            this.idLink = idLink;
            this.idUsr = idUsr;
            this.linkText = linkText;
            this.datePreview = DataManager.Helper.GetDatePreview(this.dateAndTime);
            this.previewText = DataManager.Helper.GetPreviewText(text, 30);
        }

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

    public sealed class Timer
    {
        private string text { get; set; }
        private int hours { get; set; }
        private int minutes { get; set; }
        private int seconds { get; set; }
        private int uid { get; set; }
        private int timerId { get; set; }
        private readonly static Timer timerInstance = new Timer();


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

        ///TDO : mecanismul de functionare a timer-ului
        
    }
}

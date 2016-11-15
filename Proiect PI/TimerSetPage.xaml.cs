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
using UserManager;
using DataManager;


namespace Proiect_PI
{
    /// <summary>
    /// Interaction logic for TimerSetPage.xaml
    /// </summary>
    public partial class TimerSetPage : Page
    {

        private Frame mainFrame;

        public int Hours
        {
            get { return Timer.TimerInstance.Hours; }
            set { Timer.TimerInstance.Hours = value; }
        }

        public int Minutes
        {
            get { return Timer.TimerInstance.Minutes; }
            set { Timer.TimerInstance.Minutes = value; }
        }

        public int Seconds
        {
            get { return Timer.TimerInstance.Seconds; }
            set { Timer.TimerInstance.Seconds = value; }
        }

        public TimerSetPage(ref Frame mainFrame)
        {
            
            InitializeComponent();
            LoadTimer();
            this.mainFrame = mainFrame;
        }

        public void LoadTimer()
        {
            if (User.UserInstance.loginType)
            {
                //online
                DBManager.GetTimer(User.UserInstance.UID);
            }
            else
            {
                //offline
                XMLManager.XMLManagerInstance.GetTimer();
            }

            if(Timer.TimerInstance != null)
            {
                StringBuilder expr = new StringBuilder();
                if(Timer.TimerInstance.Hours != 0)
                {
                    expr.Append(Timer.TimerInstance.Hours + "h ");
                }
                if(Timer.TimerInstance.Minutes != 0)
                {
                    expr.Append(Timer.TimerInstance.Minutes + "m ");
                }
                if(Timer.TimerInstance.Seconds != 0)
                {
                    expr.Append(Timer.TimerInstance.Seconds + "s");
                }

                Expr.Text = expr.ToString();
            }
        }

        public void EvalExp()
        {
            string expression = Expr.Text.ToLower();

            //-----------------------------------------------------------------------------------hours

            if (expression.Contains("hours"))
            {
                Hours = Convert.ToInt32(expression.Substring(0, expression.IndexOf("hours")));
                expression = expression.Substring(expression.IndexOf("hours") + 5);
            }
            else
                if (expression.Contains("hour"))
            {
                Hours = Convert.ToInt32(expression.Substring(0, expression.IndexOf("hour")));
                expression = expression.Substring(expression.IndexOf("hour") + 4);
            }
            else
                    if (expression.Contains("hou"))
            {
                Hours = Convert.ToInt32(expression.Substring(0, expression.IndexOf("hou")));
                expression = expression.Substring(expression.IndexOf("hou") + 3);
            }
            else
                        if (expression.Contains("ho"))
            {
                Hours = Convert.ToInt32(expression.Substring(0, expression.IndexOf("ho")));
                expression = expression.Substring(expression.IndexOf("ho") + 2);
            }
            else
                            if (expression.Contains("h"))
            {
                Hours = Convert.ToInt32(expression.Substring(0, expression.IndexOf("h")));
                expression = expression.Substring(expression.IndexOf("h") + 1);
            }
            else
            {
                Hours = 0;
            }

            //-----------------------------------------------------------------------------------minutes

            if (expression.Contains("minutes"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("minutes")));
                expression = expression.Substring(expression.IndexOf("minutes") + 7);
            }
            else
                if (expression.Contains("minute"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("minute")));
                expression = expression.Substring(expression.IndexOf("minute") + 6);
            }
            else
                    if (expression.Contains("minut"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("minut")));
                expression = expression.Substring(expression.IndexOf("minut") + 5);
            }
            else
                        if (expression.Contains("minu"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("minu")));
                expression = expression.Substring(expression.IndexOf("minu") + 4);
            }
            else
                            if (expression.Contains("min"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("min")));
                expression = expression.Substring(expression.IndexOf("min") + 3);
            }
            else
                                if (expression.Contains("mi"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("mi")));
                expression = expression.Substring(expression.IndexOf("mi") + 2);
            }
            else
                                    if (expression.Contains("m"))
            {
                Minutes = Convert.ToInt32(expression.Substring(0, expression.IndexOf("m")));
                expression = expression.Substring(expression.IndexOf("m") + 1);
            }
            else
            {
                Minutes = 0;
            }

            //----------------------------------------------------------------------------------seconds

            if (expression.Contains("seconds"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("seconds")));
                expression = expression.Substring(expression.IndexOf("seconds") + 7);
            }
            else
                if (expression.Contains("second"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("second")));
                expression = expression.Substring(expression.IndexOf("second") + 6);
            }
            else
                    if (expression.Contains("secon"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("secon")));
                expression = expression.Substring(expression.IndexOf("secon") + 5);
            }
            else
                        if (expression.Contains("seco"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("seco")));
                expression = expression.Substring(expression.IndexOf("seco") + 4);
            }
            else
                            if (expression.Contains("sec"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("sec")));
                expression = expression.Substring(expression.IndexOf("sec") + 3);
            }
            else
                                if (expression.Contains("se"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("se")));
                expression = expression.Substring(expression.IndexOf("se") + 2);
            }
            else
                                    if (expression.Contains("s"))
            {
                Seconds = Convert.ToInt32(expression.Substring(0, expression.IndexOf("s")));
                expression = expression.Substring(expression.IndexOf("s") + 1);
            }
            else
            {
                Seconds = 0;
            }
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            EvalExp();
            TimerPage timerP = new TimerPage(Hours, Minutes, Seconds , ref mainFrame);
            mainFrame.Navigate(timerP);
        }

        private void Rule2020Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Rule2020TimerPage(ref mainFrame));
            
        }
    }
}

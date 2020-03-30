using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNCNotify.Properties;

namespace TNCNotify
{
    public partial class Main : MetroFramework.Forms.MetroForm
    
    {
        public Main()
        {
            InitializeComponent();
            //FirebaseAuthService authService = new FirebaseAuthService();
            //authService.Signout();

            if (!Settings.Default.LoggedIn)
            {
                metroUserControl.Controls.Add(new SignInControl());
            }
            else
            {
                metroUserControl.Controls.Add(new TabControl());
            }
            
        }

        public void RemoveSignIn()
        {
            metroUserControl.Controls.RemoveAt(0);
            Console.WriteLine("Remove Sign in");
            metroUserControl.Controls.Add(new TabControl());

        }

        public void Main_Resize(Object sender, EventArgs e)
        {
            Console.WriteLine("resize");

            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }

        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }

}

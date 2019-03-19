using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kinvey;
using ADONTEC.Comm;


namespace TNCNotify
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();

            UserControl userControl = new UserControl1();
            metroPanel1.Controls.Add(userControl);
        }


        private void SaveError_Click(object sender, EventArgs e)
        {

            MachineCom MCOM = new MachineCom();
            MCOM.CreateConnection(0, "192.168.1.151", "19000");
        }

        private void signInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Client.SharedClient.ActiveUser == null)
            {
                Console.WriteLine("Sign In");
                Form signInForm = new SignIn();
                signInForm.Show();

            }
            else
            {
                Console.WriteLine("Sign Out");

                User user = Client.SharedClient.ActiveUser;
                user.Logout();

            }
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Client.SharedClient.ActiveUser == null)
            {
                signInToolStripMenuItem.Text = "Sign In";

            }
            else
            {
                signInToolStripMenuItem.Text = "Sign Out";
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MachineCom MCOM = new MachineCom();
            MCOM.CreateConnection(0, "192.168.1.151", "19000");
        }

    }
}

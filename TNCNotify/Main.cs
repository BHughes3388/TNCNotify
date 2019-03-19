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

namespace TNCNotify
{
    public partial class Main : MetroFramework.Forms.MetroForm
    
    {
        public Main()
        {
            InitializeComponent();

            User user = Client.SharedClient.ActiveUser;

            if (user == null)
            {
                metroUserControl.Controls.Add(new SignInControl());
            }
            else
            {
                metroUserControl.Controls.Add(new TabControl());
            }

            //GetUsersMachines();
            
        }

        public void RemoveSignIn()
        {
            metroUserControl.Controls.RemoveAt(0);
            Console.WriteLine("Remove Sign in");
        }


    }
}

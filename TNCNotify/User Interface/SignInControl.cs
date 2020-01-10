using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kinvey;

namespace TNCNotify
{
    public partial class SignInControl : MetroFramework.Controls.MetroUserControl
    {
        public SignInControl()
        {
            InitializeComponent();
        }

        private void SignInControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            Console.WriteLine("did load control");
        }

        private async void Login(string username, string password)
        {
            User myUser = await User.LoginAsync(username, password);

            if (myUser != null)
            {
                Main mainForm = (Main)this.ParentForm;
                mainForm.RemoveSignIn();

                Console.WriteLine(myUser.UserName + " logged in");
            }
            else
            {
                Console.WriteLine("There was a problem logging in");
            }
           

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Login(usernameLabel.Text, passwordLabel.Text);
        }
    }
}

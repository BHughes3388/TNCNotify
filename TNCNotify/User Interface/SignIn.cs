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
    public partial class SignIn : MetroFramework.Forms.MetroForm
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add if statement to make sure there is username and password
            Login(textBox1.Text, textBox2.Text);
        }

        private async void Login(string username, string password)
        {
            User myUser = await User.LoginAsync(username, password);
            this.Close();
            Console.WriteLine(myUser.UserName + " logged in");

        }
    }
}

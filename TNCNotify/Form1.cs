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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //hack log in
            if (Client.SharedClient.ActiveUser == null)
            {
                Login("bhughes3388@gmail.com", "password");
            }
            else
            {
                //User user = Client.SharedClient.ActiveUser;
                //user.Logout();

                //Client.SharedClient.ActiveUser.RetrieveAsync(Client.SharedClient.ActiveUser.Id);

                //Console.WriteLine("machineIds " + Client.SharedClient.ActiveUser.Attributes["Machines"].Count());

               KinveyNetworking networking = new KinveyNetworking();
               networking.GetMachines(null);
               Console.WriteLine("Already Logged in");
            }

        }

        private static async void Login(string username, string password)
        {
            User myUser = await User.LoginAsync(username, password);
            Console.WriteLine(myUser.UserName + " logged in");

        }

        private void SaveError_Click(object sender, EventArgs e)
        {

            MachineCom MCOM = new MachineCom();
            MCOM.CreateConnection(0, "192.168.1.151", "19000");
        }
    }
}

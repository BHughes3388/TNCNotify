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
    public partial class TabControl : MetroFramework.Controls.MetroUserControl
    {
        List<Machine> machines;

        public TabControl()
        {
            InitializeComponent();
            PopulateListView();
        }

        private void TabControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private async void PopulateListView()
        {
            User user = Client.SharedClient.ActiveUser;

            List<string> machineids = user.Attributes["Machines"].ToObject<List<string>>();
            Console.WriteLine("machine ids: " + machineids);


            KinveyNetworking network = new KinveyNetworking();

            machines = await network.GetMachines(machineids);

            machineListView.Clear();

            machineListView.Columns.Add("Name", 120);
            machineListView.Columns.Add("ip", 100);
            machineListView.Columns.Add("Machine Name", 130);

            foreach (Machine machine in machines)
            {
                Console.WriteLine("machines name: {0} machine ip: {0} ", machine.Name, machine.IP);

                machineListView.Items.Add(new ListViewItem(new string[] {machine.Name, machine.IP, machine.MachineName }));
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            PopulateListView();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            foreach (int index in machineListView.CheckedIndices)      
            {
                Console.WriteLine("Check Indexs: " + index);
            }
        }
    }
}

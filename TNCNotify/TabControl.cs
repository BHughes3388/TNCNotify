using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Kinvey;


namespace TNCNotify
{
    public partial class TabControl : MetroFramework.Controls.MetroUserControl
    {
        List<Machine> machines;
        List<Machine> onlineMachines;


        public TabControl()
        {
            InitializeComponent();
 
            GetMachines();
        }

        private void TabControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private async void GetMachines()
        {
            User user = Client.SharedClient.ActiveUser;

            List<string> machineids = user.Attributes["Machines"].ToObject<List<string>>();
            Console.WriteLine("machine ids: " + machineids);

            KinveyNetworking network = new KinveyNetworking();

            machines = await network.GetMachines(machineids);

            PopulateOnlineListView(machines);
            PopulateMchineListView(machines);
        }

        private void PopulateOnlineListView(List<Machine> machines)
        {

            onlineMachines = new List<Machine>();


            foreach (Machine machine in machines)
            {
                if (PingHost(machine.IP, 19000))
                {
                    onlineMachines.Add(machine);
                    Console.WriteLine("PingHost passed");
                }
                else
                {
                    Console.WriteLine("PingHost failed");
                }
            }

            onlineMachineListView.Clear();

            onlineMachineListView.Columns.Add("Name", 200);
            onlineMachineListView.Columns.Add("ip", 100);
            onlineMachineListView.Columns.Add("Machine Name", 130);

            foreach (Machine machine in onlineMachines)
            {
                Console.WriteLine("machines name: {0} machine ip: {1} ", machine.Name, machine.IP);

                onlineMachineListView.Items.Add(new ListViewItem(new string[] {machine.Name, machine.IP, machine.MachineName }));
            }
        }

        private void PopulateMchineListView(List<Machine> machines)
        {

            machineListView.Clear();

            machineListView.Columns.Add("Name", 200);
            machineListView.Columns.Add("ip", 100);
            machineListView.Columns.Add("Machine Name", 130);

            foreach (Machine machine in machines)
            {
                Console.WriteLine("machines name: {0} machine ip: {1} ", machine.Name, machine.IP);

                machineListView.Items.Add(new ListViewItem(new string[] { machine.Name, machine.IP, machine.MachineName }));
            }
        }

        private static bool PingHost(string hostUri, int portNumber)
        {
            var client = new TcpClient();
            var result = client.BeginConnect(hostUri, portNumber, null, null);

            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));

            if (!success)
            {
                //Console.WriteLine("failed to connect to: " + hostUri);
                return false;
            }
            else
            {
                client.EndConnect(result);
                return true;
            }

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            //PopulateOnlineListView(machines);
            Console.WriteLine("fix this.. refreshing is currently broken");
        }

        private void startButton_Click(object sender, EventArgs e)
        {

            foreach (int index in onlineMachineListView.CheckedIndices)      
            {
                MachineCom machineCom = new MachineCom();

                Machine machine = onlineMachines[index];
                Console.WriteLine("Check Indexs: " + machine.MachineName);

                machineCom.StartMachine(machine, index);

            }
        }
    }
}

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

        User[] users;

        List<MachineCom> machineComs;

        bool isRunning= false;

        public TabControl()
        {
            InitializeComponent();

            GetMachines();
        }

        private void TabControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

        }

        private void initListView(MetroFramework.Controls.MetroListView listView)
        {
            listView.Clear();
            listView.Columns.Add("Name", 200);
            listView.Columns.Add("ip", 100);
            listView.Columns.Add("Machine Name", 130);
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("did select index {0}", metroTabControl1.SelectedTab);
            if (metroTabControl1.SelectedIndex == 2)
            {
                Console.WriteLine("Get Users");
            }
        }

        private async void GetMachines()
        {
            FirestoreNetworking networking = new FirestoreNetworking();

            List<Machine> machines = await networking.GetMachines();
            PopulateOnlineListView(machines);
            PopulateMchineListView(machines);
        }

        private void PopulateOnlineListView(List<Machine> machines)
        {
            initListView(onlineMachineListView);

            onlineMachines = new List<Machine>();

            foreach (Machine machine in machines)
            {
                if (PingHost(machine.Ip, 19000))
                {
                    onlineMachineListView.Items.Add(new ListViewItem(new string[] { machine.Name, machine.Ip, machine.MachineName }));

                    onlineMachines.Add(machine);
                }
            }
        }

        private void PopulateMchineListView(List<Machine> machines)
        {
            initListView(machineListView);

            foreach (Machine machine in machines)
            {
                machineListView.Items.Add(new ListViewItem(new string[] { machine.Name, machine.Ip, machine.MachineName }));
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
            GetMachines();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                machineComs = new List<MachineCom>();

                foreach (int index in onlineMachineListView.CheckedIndices)
                {
                    MachineCom machineCom = new MachineCom();

                    Machine machine = onlineMachines[index];

                    machineCom.StartMachine(machine, index);

                    machineComs.Add(machineCom);
                }

                isRunning = true;
            }
            else
            {
                foreach (MachineCom machineCom in machineComs)
                {
                    machineCom.closeConnection();   
                }

                machineComs = null;

                isRunning = false;
            }
            
            startButton.Text = isRunning ? "Stop" : "Start";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kinvey;

namespace TNCNotify
{
    
    [JsonObject]
    public class Machine
    {
        [JsonProperty("_id")]
        public string machineid { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IP")]
        public string IP { get; set; }

        [JsonProperty("Error_ID")]
        public string Error_ID { get; set; }

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("ToolName")]
        public string ToolName { get; set; }

        [JsonProperty("ToolNr")]
        public string ToolNr { get; set; }

        [JsonProperty("ToolIndex")]
        public string ToolIndex { get; set; }

        [JsonProperty("ToolAxis")]
        public string ToolAxis { get; set; }

        [JsonProperty("ToolLen")]
        public string ToolLen { get; set; }

        [JsonProperty("ToolRad")]
        public string ToolRad { get; set; }

        [JsonProperty("ToolRad2")]
        public string ToolRad2 { get; set; }

        [JsonProperty("ToolLenOversize")]
        public string ToolLenOversize { get; set; }

        [JsonProperty("ToolRadOversize")]
        public string ToolRadOversize { get; set; }

        [JsonProperty("ToolReplacmentToolNr")]
        public string ToolReplacmentToolNr { get; set; }

        [JsonProperty("ToolTime2")]
        public string ToolTime2 { get; set; }

        [JsonProperty("ToolCurTime")]
        public string ToolCurTime { get; set; }


        [JsonProperty("NCSpindleOverride")]
        public string NCSpindleOverride { get; set; }

        [JsonProperty("NCFeedOverride")]
        public string NCFeedOveride { get; set; }

        [JsonProperty("PLCSPindleOverride")]
        public string PLCSpindleOverride { get; set; }

        [JsonProperty("PLCFeedOverride")]
        public string PLCFeedOverride { get; set; }

        [JsonProperty("PAL")]
        public string PAL { get; set; }

        [JsonProperty("ProgramStatus")]
        public string ProgramStatus { get; set; }

        [JsonProperty("SelectedProgram")]
        public string SelectedProgram { get; set; }

        [JsonProperty("ActiveProgram")]
        public string ActiveProgram { get; set; }

        [JsonProperty("BlockNr")]
        public string BlockNr { get; set; }

        [JsonProperty("ExecutionMode")]
        public string ExecutionMode { get; set; }

        [JsonProperty("Connected")]
        public bool Connected { get; set; }
    }
    
    [JsonObject(MemberSerialization.OptIn)]
    public class Error : Entity
    {
        [JsonProperty("Machine ID")]
        public string Machine_ID { get; set; }

        [JsonProperty("Error Nr")]
        public string Error_Nr { get; set; }

        [JsonProperty("Error Text")]
        public string Error_Text { get; set; }

        [JsonProperty("Error Group")]
        public string Error_Group { get; set; }

        [JsonProperty("Error Class")]
        public string Error_Class { get; set; }
    }

    class KinveyNetworking
    {

        public async Task<List<Machine>> GetMachines(List<string> machineIds)
        {

            DataStore<Machine> dataStore = DataStore<Machine>.Collection("Machines", DataStoreType.NETWORK);

            try
            {
                List<Machine> machines = await dataStore.FindWithMongoQueryAsync(QueryStringForMachineIds(machineIds));
                //Console.WriteLine("Machines : {0}", machines);
                return machines;
            }
            catch (KinveyException ke)
            {
                // handle error
                Console.WriteLine("Get Machines Exception: " + ke);
                return null;
            }

           
        }

        public async Task<User[]> GetUsers()
        {

            var criteria = new UserDiscovery
            {
               
            };

            try
            {
                User[] loopupUsers = await Client.SharedClient.ActiveUser.LookupAsync(criteria);
                Console.WriteLine("Active User : {0}", Client.SharedClient.ActiveUser.UserName);

                Console.WriteLine("Users : {0}", loopupUsers);
                return loopupUsers;
            }
            catch (KinveyException ke)
            {
                // handle error
                Console.WriteLine("Get Users Exception: " + ke);
                return null;
            }


        }

        public async void UpdateMachine(Machine machine)
        {
            DataStore<Machine> dataStore = DataStore<Machine>.Collection("Machines", DataStoreType.NETWORK);

            try
            {
                Machine updatedMachine = await dataStore.SaveAsync(machine);
                Console.WriteLine("Machine Updated : {0}", updatedMachine.MachineName);
            }
            catch (KinveyException ke)
            {
                // handle error
                Console.WriteLine("Exception: " + ke);
            }

        }


        public async void SaveError(Error NewError, Machine machine)
        {
            DataStore<Error> dataStore = DataStore<Error>.Collection("Errors", DataStoreType.NETWORK);

            NewError.Machine_ID = machine.machineid;

            try
            {
                Error savedError = await dataStore.SaveAsync(NewError);
            }
            catch (KinveyException ke)
            {
                // handle error
                Console.WriteLine("Exception: " + ke);

            }

        }

        private string QueryStringForMachineIds(List<string> machineIds)
        {
            //formats list of machine ids to string ["machine id 1", "machine id 2", machine id 3]
            string machineIdsString = string.Format("[\"{0}\"]", string.Join("\", \"", machineIds));

            //formats query string with above machines ids to { "_id" : { "$in" : ["machine id 1", "machine id 2", "machine id 3"] } } 
            //meaning get objects whose _id are $in the array of ids
            return string.Format("{{\"_id\":{{\"$in\": {0} }}}}", machineIdsString);
        }

    }

}

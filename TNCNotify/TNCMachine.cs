using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kinvey;

namespace TNCNotify
{
    [JsonObject(MemberSerialization.OptIn)]
    class TNCMachine : Entity
    {
        [JsonProperty("programstatus")]
        public string ProgramStatus { get; set; }

    }

}

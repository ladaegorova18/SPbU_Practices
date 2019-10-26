using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKClient
{
    [JsonObject]
    class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string deactivated { get; set; }
        public bool is_closed { get; set; }
        public bool can_access_closed { get; set; }
        public Personal personal { get; set; }
    }
}

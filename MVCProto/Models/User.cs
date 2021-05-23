using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProto.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
    }

    public class Client
    {
        public string ConnectionId { get; set; }
        public List<int> DroneList { get; set; }
    }
}
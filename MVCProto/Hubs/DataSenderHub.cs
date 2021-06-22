using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCProto.Models;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace MVCProto.Hubs
{
    /// <summary>
    /// Этот класс отвечает за отправку данных клиентам 
    /// </summary>
    [HubName("dataSource")]
    public class DataSenderHub : Hub
    {
        private readonly DataTicker _dataTicker;

        public DataSenderHub() : this(DataTicker.Instance) 
        {

        }
        static List<Client> clients = new List<Client>();

        public DataSenderHub(DataTicker dataTicker)
        {
            _dataTicker = dataTicker;
        }

        public List<string> GetInitial()
        {
            //return new List<string> { "ID1; 0; 0; 0", "ID2; 0; 0; 0", "ID3; 0; 0; 0", "ID4; 0; 0; 0" };
            return new List<string> {
                "ID_1; 56.7307; 37.1949; 0", "ID_2; 56.7212; 37.1952; 0",
                "ID_3; 56.7185; 37.2269; 0", "ID_4; 56.732837; 37.221782; 0",
                "ID_5; 56.7644; 37.3898; 0 ", "ID_6; 56.7465; 37.4080; 0",
                "ID_7; 56.7784; 37.4142; 0", "ID_8; 56.6899; 37.2981; 0",
                "ID_9; 56.7056; 37.3115; 0", "ID_10; 56.6890; 37.3362; 0",
                "ID_11; 56.6804; 37.3131; 0", "ID_12; 56.7591; 37.4264; 0",
            };
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            _dataTicker.AddGroup(groupName);
        }

        



    }
}
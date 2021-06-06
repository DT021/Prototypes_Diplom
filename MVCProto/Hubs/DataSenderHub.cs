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
            return new List<string> { "ID1; 56.736506; 37.22194; 0", "ID2; 56.736842; 37.218743; 0", "ID3; 56.737078; 37.223097; 0", "ID4; 56.732837; 37.221782; 0" };
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
            _dataTicker.AddGroup(groupName);
        }







        //public void Connect(string profile)
        //{
        //    var id = Context.ConnectionId;

        //    if (!clients.Any(x => x.ConnectionId == id))
        //    {
        //        string[] sp = profile.Split(';');
        //        List<int> drones = new List<int>();

        //        foreach(var elem in sp)
        //        {
        //            drones.Add(Convert.ToInt32(elem));
        //        }

        //        clients.Add(new Client { ConnectionId = id, DroneList = drones });

        //        //// Посылаем сообщение текущему пользователю
        //        //Clients.Caller.onConnected(id, userName, Users);

        //        //// Посылаем сообщение всем пользователям, кроме текущего
        //        //Clients.AllExcept(id).onNewUserConnected(id, userName);
        //    }

        //}


        //private readonly DataTicker _droneTicker;

        //public DataSenderHub(DataTicker droneTicker)
        //{
        //    _droneTicker = droneTicker;
        //}


        //public DataSenderHub() : this(DataTicker.Instance)
        //{

        //}



        //public IEnumerable<Drone> GetAllDrones()
        //{
        //    return _droneTicker.GetAllDrones();
        //}




    }
}
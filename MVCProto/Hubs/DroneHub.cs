using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCProto.Models;

namespace MVCProto.Hubs
{
    public class DroneHub : Hub
    {
        static List<Client> clients = new List<Client>();

        public void Announce(string message)
        {
            Clients.All.Announce(message);
        }

        public void SendId(int id)
        {
            Clients.All.SendId(id);
        }


        public void Connect(string profile)
        {
            var id = Context.ConnectionId;


            if (!clients.Any(x => x.ConnectionId == id))
            {
                string[] sp = profile.Split(';');
                List<int> drones = new List<int>();

                foreach(var elem in sp)
                {
                    drones.Add(Convert.ToInt32(elem));
                }

                clients.Add(new Client { ConnectionId = id, DroneList = drones });

                //// Посылаем сообщение текущему пользователю
                //Clients.Caller.onConnected(id, userName, Users);

                //// Посылаем сообщение всем пользователям, кроме текущего
                //Clients.AllExcept(id).onNewUserConnected(id, userName);
            }

        }
    }
}
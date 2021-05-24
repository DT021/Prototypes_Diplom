using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using MVCProto.Hubs;

namespace MVCProto
{
    public class DroneTicker
    {
        private readonly static Lazy<DroneTicker> _instance = new Lazy<DroneTicker>(() => new DroneTicker(GlobalHost.ConnectionManager.GetHubContext<DroneHub>().Clients));
        private readonly ConcurrentDictionary<int, Drone> _drones = new ConcurrentDictionary<int, Drone>();
        
        private readonly Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(1000);

        private readonly object _updateDroneCoordsLock = new object();
        private volatile bool _updatingDronePrices = false;

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }


        private DroneTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;

            _drones.Clear();
            var drones = new List<Drone>
            {
                new Drone { droneId = 1, X = 56.736506, Y = 37.221947 },
                new Drone { droneId = 2, X = 56.736842, Y = 37.218743 },
                new Drone { droneId = 3, X = 56.737078, Y = 37.223097 },
            };
            drones.ForEach(drone => _drones.TryAdd(drone.droneId, drone));
            _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

        }


        public static DroneTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IEnumerable<Drone> GetAllDrones()
        {
            return _drones.Values;
        }



        private void UpdateStockPrices(object state)
        {
            lock (_updateDroneCoordsLock)
            {
                
            }
        }

        private bool TryUpdateDroneCoords(Drone drone)
        {   
            
            return true;
        }


        private void BroadcastDroneCoordinates(Drone drone)
        {
            Clients.All.updateCoordinates(drone);
        }

    }
}
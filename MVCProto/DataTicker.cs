using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MVCProto.Hubs;
using MVCProto.Models;

namespace MVCProto
{
    public class DataTicker
    {
        // Singleton instance
        private readonly static Lazy<DataTicker> _instance = new Lazy<DataTicker>(() => new DataTicker(GlobalHost.ConnectionManager.GetHubContext<DataSenderHub>().Clients));
        private static List<string> _groups = new List<string>();

        // Для синхронизации обмена данных
        private readonly object _updateLocker = new object();
        private volatile bool _updating = false;


        // таймер для обновлений
        private readonly Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(1000);

        private readonly Random _rand = new Random();
        private DateTime _unixTimeBase = new DateTime(1970, 1, 1);

        //List<string> _drones = new List<string> { "ID1", "ID2", "ID3", "ID4" };
        List<Drone> _drones = new List<Drone> {
           new Drone(1, 56.736506, 37.221947),
           new Drone(2, 56.736842, 37.218743),
           new Drone(3, 56.737078, 37.223097),
           new Drone(4, 56.732837, 37.221782),
           new Drone(5, 56.732837, 37.221782),
           new Drone(6, 56.736506, 37.221947),
           new Drone(7, 56.736842, 37.218743),
           new Drone(8, 56.737078, 37.223097),
           new Drone(9, 56.732837, 37.221782),
           new Drone(10, 56.732837, 37.221782),
           new Drone(11, 56.732837, 37.221782),
           new Drone(12, 56.732837, 37.221782)
        };

        /// <summary>
        /// Приватный конструктор. Вызвать нельзя!
        /// </summary>
        /// <param name="clients">
        /// Обертка для всех соединений к некоторому SingleR хабу.
        /// </param>
        /// 

        private DataTicker(IHubConnectionContext<dynamic> clients)
        {
             Clients = clients;
            _timer = new Timer(UpdatePositions, null, _updateInterval, _updateInterval);
        }


        /// <summary>
        /// Реализация singleton. Конструктор private.
        /// </summary>
        public static DataTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        /// <summary>
        /// Получатели сообщений
        /// </summary>
        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }


        /// <summary>
        /// Получает текущее время в формате Unix
        /// </summary>
        /// <returns>текущее время</returns>
        private uint UnixTimeStamp()
        {
            TimeSpan uts = (DateTime.Now - _unixTimeBase);
            return (uint)uts.TotalSeconds;
        }
        //var update = new List<String>();
        //update.Add(String.Format("ID_{0}; {1}; {2}; {3}", id.droneId, pos.Lat, pos.Lon, pos.UTCTime));
     
        private void UpdatePositions(object state)
        {
            lock (_updateLocker)
            {
                if (!_updating)
                {
                    _updating = true;
                    //var Messages = new Dictionary<int, string>();
                    var Messages = new List<string>();
                    if (_groups.Count > 0)
                    {
                        foreach (var drone in _drones)
                        {
                            if (_rand.NextDouble() > 0.5)
                            {
                                Position pos = new Position
                                {
                                    Lat = (float)(55 + 2 * _rand.NextDouble() - 1),
                                    Lon = (float)(37 + 2 * _rand.NextDouble() - 1),
                                    UTCTime = UnixTimeStamp()
                                };                                                 
                                Messages.Add(
                                    
                                    String.Format("ID_{0}; {1}; {2}; {3}", drone.droneId, pos.Lat, pos.Lon, pos.UTCTime
                                    ));
                            }
                        }
                    }
                    if (Messages.Count > 0)
                        BroadcastPositions(Messages);
                    _updating = false;
                }
            }
        }


        public void AddGroup(string name)
        {
            lock (_updateLocker)
            {
                if (!_groups.Contains(name))
                {
                    _groups.Add(name);
                }
            }
        }


        /// <summary>
        /// Собственно рассылка обновлений клиентам
        /// </summary>
        /// <param name="newPositions">список измененных позиций</param>
        /// 

        //В цикле по каждой группе ищем все ID в update и отправляем группе

        private void BroadcastPositions(List<string> newPositions)
        {        
            foreach (var group in _groups)
            {
                string[] v = group.Split(';');
                List<string> groupPositions = new List<string>();
                foreach (var e in newPositions)
                    for (int i = 0; i < v.Length; i++)
                        if (e.Contains(v[i] + ';'))
                            groupPositions.Add(e);
                if (groupPositions.Count > 0)
                    Clients.Group(group).updatePositions(groupPositions);
            }
        }



        //private void BroadcastPositions(Dictionary<int, string> newPositions)
        //{
        //    foreach (var group in _groups)
        //    {
        //        string[] v = group.Split(';');
        //        List<string> groupPositions = new List<string>();
        //        foreach (var e in newPositions)
        //            for (int i = 0; i < v.Length; i++)
        //                if (e.Contains(v[i] + ';'))
        //                    groupPositions.Add(e);
        //        if (groupPositions.Count > 0)
        //            Clients.Group(group).updatePositions(groupPositions);
        //    }
        //}


    }
}
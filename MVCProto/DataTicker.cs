using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MVCProto.Hubs;

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

        List<string> _drones = new List<string> { "ID1", "ID2", "ID3", "ID4" };
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


        private void UpdatePositions(object state)
        {
            lock (_updateLocker)
            {

                if (!_updating)
                {
                    _updating = true;

                    var update = new List<String>();
                    if (_groups.Count > 0)
                    {
                        foreach (var id in _drones)
                        {
                            if (_rand.NextDouble() > 0.5)
                            {
                                Position p = new Position
                                {
                                    Lat = (float)(55 + 2 * _rand.NextDouble() - 1),
                                    Lon = (float)(37 + 2 * _rand.NextDouble() - 1),
                                    UTCTime = UnixTimeStamp()
                                };
                                update.Add(String.Format("{0}; {1}; {2}; {3}", id, p.Lat, p.Lon, p.UTCTime));
                            }
                        }
                    }
                    if (update.Count > 0)
                        BroadcastPositions(update);

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
        private void BroadcastPositions(List<string> newPositions)
        {
            //В цикле по каждой группе ищем все ID в update и отправляем группе
            foreach (var g in _groups)
            {
                string[] v = g.Split(';');
                List<string> groupPositions = new List<string>();
                foreach (var e in newPositions)
                    for (int i = 0; i < v.Length; i++)
                        if (e.Contains(v[i] + ';'))
                            groupPositions.Add(e);
                if (groupPositions.Count > 0)
                    Clients.Group(g).updatePositions(groupPositions);
            }
        }


        /// <summary>
        /// Позиция: Широта, долгота, UTC Unix timestamp
        /// </summary>
        public class Position
        {
            public float Lat { get; set; }
            public float Lon { get; set; }
            public uint UTCTime { get; set; }
        }
    }
}
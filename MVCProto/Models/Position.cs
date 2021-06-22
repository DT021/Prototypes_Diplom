using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProto.Models
{
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
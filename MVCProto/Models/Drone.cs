using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProto
{
    public class Drone
    {
        public int droneId { get; set; }
        public static int id = 1;
        public string droneName { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

    public string droneDescription { get; set; }

        public Drone()
        {   
            this.droneId = id;
            this.droneName = "Drone "+ id.ToString() ;
            this.droneDescription = "Some Description";
            id++;
        }

        public Drone(double x, double y): base()
        {
            this.X = x;
            this.Y = y;
        }

    }
}
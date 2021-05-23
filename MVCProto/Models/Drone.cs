using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProto
{
    public class Drone
    {
        public int droneId { get; set; }
        public string droneName { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string droneDescription { get; set; }

        public Drone()
        {   
            this.droneName = "Drone";
            this.droneDescription = "Some Description";          
        }

        public Drone(double x, double y) : this()
        {
            this.X = x;
            this.Y = y;
        }


        public Drone(int id, double x, double y) : this(x, y)
        {         
            this.droneId = id;
            this.droneName = "Drone" + droneId.ToString();
        }




    }
}
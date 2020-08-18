using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Race_Track.Models
{
    public class Vehicle
    {
        public int Vehicle_ID { get; set; }
        public bool Tow_Strap { get; set; }
        public double Tire_Wear { get; set; }
        public double Ground_Clearance { get; set; }
    }
}
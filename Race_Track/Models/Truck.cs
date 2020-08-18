using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Race_Track.Models
{
    public class Truck
    {
        public string Name;
        public int ID;
        public bool Tow_Strap;
        public double Ground_Clearance;
        public Vehicle _vehicle;
        public Truck(string truckName, Vehicle vehicle)
        {
            _vehicle = vehicle;
            ID = _vehicle.Vehicle_ID;
            Name = truckName;
            Tow_Strap = _vehicle.Tow_Strap;
            Ground_Clearance = _vehicle.Ground_Clearance;
        }
    }
}
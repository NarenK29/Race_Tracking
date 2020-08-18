using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Race_Track.Models
{
    public class Car
    {
        public string Name;
        public int ID;
        public bool Tow_Strap;
        public double Tire_Wear;        
        public Vehicle _vehicle;
        public Car(string carName,Vehicle vehicle)
        {
            _vehicle = vehicle;
            Name = carName;
            ID = _vehicle.Vehicle_ID;
            Tow_Strap = _vehicle.Tow_Strap;
            Tire_Wear = _vehicle.Tire_Wear;
        }
    }
}
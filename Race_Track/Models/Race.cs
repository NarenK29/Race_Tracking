using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Race_Track.Models;

namespace Race_Track.Models
{
    public class Race
    {
        public int ID { get; set; }
        [Display(Name = "Vehicle Name")]
        public string Name { get; set; }
        public string Type { get; set; }
        public int VehicleID { get; set; }
    }
}
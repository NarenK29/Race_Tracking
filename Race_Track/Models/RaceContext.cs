using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Race_Track.Models
{
    public class RaceContext : DbContext
    {
        public DbSet<Race> race { get; set; }

    }
}
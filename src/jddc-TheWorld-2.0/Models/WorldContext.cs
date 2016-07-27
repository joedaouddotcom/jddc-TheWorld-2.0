using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace jddc_TheWorld_2._0.Models
{
    public class WorldContext : DbContext
    {
    public WorldContext()
    {

    }

    
    public DbSet<Trip> Trip { get; set; }
    public DbSet<Stop> Stops { get; set; }

    }
}

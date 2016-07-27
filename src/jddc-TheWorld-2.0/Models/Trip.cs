using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Models
{
    public class Trip
    {

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public string UserName { get; set; }

    public ICollection<Stop> Stops { get; set; }
    }

}

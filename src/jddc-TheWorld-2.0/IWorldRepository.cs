using System.Collections.Generic;

namespace jddc_TheWorld_2._0.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
    }
}
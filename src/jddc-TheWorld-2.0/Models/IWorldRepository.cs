using System.Collections.Generic;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName);

        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
        
    }
}
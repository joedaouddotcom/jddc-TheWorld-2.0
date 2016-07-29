using System.Collections.Generic;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsWithStops();
        void AddTrip(Trip trip);
        bool SaveAll();

        Trip GetTripByName(string tripName, string username);    
        void AddStop(string tripName, string username, Stop newStop);

        //Task<bool> SaveChangesAsync();
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}
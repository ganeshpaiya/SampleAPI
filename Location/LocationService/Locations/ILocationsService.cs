using LocationData.Models;
using System.Threading.Tasks;

namespace LocationService.Locations
{
    public interface ILocationsService
    {
        Task<Location> GetLocation(int id);

        Task<int> PostLocation(Location location);

        Task<bool> PutLocation(int id, Location location);

        Task<bool> DeleteLocation(int id);
    }
}

using LocationData.Data;
using LocationData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Locations
{
    public class LocationsService : ILocationsService
    {

        private readonly LocationContext context;

        public LocationsService(LocationContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteLocation(int id)
        {
            var location = await context.Locations.FindAsync(id);

            if (location == null)
            {
                return false;
            }

            context.Locations.Remove(location);
            await context.SaveChangesAsync();


            return true;
        }
        public async Task<Location> GetLocation(int id)
        {
            var location = await context.Locations.FindAsync(id);
            return location;
        }

        public async Task<int> PostLocation(Location location)
        {
            context.Locations.Add(location);
            await context.SaveChangesAsync();

            return location.Id;
        }

        public async Task<bool> PutLocation(int id, Location location)
        {
            context.Entry(location).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool locationExists(int id)
        {
            return context.Locations.Any(e => e.Id == id);
        }
    }
}

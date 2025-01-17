using LookUpData.Data;
using LookUpData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookUpService
{
    public class LookUpsService : ILookUpsService
    {
        private readonly LookUpDbContext lookUpDbContext;

        public LookUpsService(LookUpDbContext lookUpDbContext)
        {
            this.lookUpDbContext = lookUpDbContext;
        }

        public async Task<bool> DeleteLookUp(int id)
        {
            var lookUp = await lookUpDbContext
                .LookUps
                .FindAsync(id);

            if (lookUp != null)
            {
                lookUpDbContext.Remove(lookUp);

                await lookUpDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<LookUp> GetLookUp(int id)
        {
            var lookUp = await lookUpDbContext
                .LookUps
                .FindAsync(id);

            return lookUp;
        }

        public async Task<List<LookUp>> GetLookUps(int TypeId)
        {
            var ListOfLookUps = await lookUpDbContext
                .LookUps
                .AsNoTracking()
                .Where(L => L.LookUpTypeId == TypeId)
                .ToListAsync();

            return ListOfLookUps;
        }

        public async Task<int> PostLookUp(LookUp newLookUp)
        {
            if (newLookUp != null)
            {
                lookUpDbContext.LookUps.Add(newLookUp);

                await lookUpDbContext.SaveChangesAsync();

                return newLookUp.Id;
            }

            return default;
        }

        public async Task<bool> PutLookUp(int id, LookUp updatedLookUp)
        {
            lookUpDbContext.Entry(updatedLookUp).State = EntityState.Modified;

            try
            {
                await lookUpDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LookUpExists(id))
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

        private bool LookUpExists(int id)
        {
            return lookUpDbContext.LookUps.Any(L => L.Id == id);
        }
    }
}

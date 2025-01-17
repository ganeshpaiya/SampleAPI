using LookUpData.Data;
using LookUpData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookUpService
{
    public class LookUpTypesService : ILookUpTypesService
    {
        private readonly LookUpDbContext lookUpDbContext;

        public LookUpTypesService(LookUpDbContext lookUpDbContext)
        {
            this.lookUpDbContext = lookUpDbContext;
        }

        public async Task<bool> DeleteLookUpType(int id)
        {
            var lookUpType = await lookUpDbContext.LookUpTypes.FindAsync(id);

            if (lookUpType != null)
            {
                lookUpDbContext.Remove(lookUpType);

                await lookUpDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<LookUpType> GetLookUpType(int id)
        {
            var lookUpType = await lookUpDbContext.LookUpTypes.FindAsync(id);

            return lookUpType;
        }

        public async Task<List<LookUpType>> GetLookUpTypes()
        {
            var LookUpTypes = await lookUpDbContext
                .LookUpTypes
                .AsNoTracking()
                .ToListAsync();

            return LookUpTypes;
        }

        public async Task<int> PostLookUpType(LookUpType newLookUpType)
        {
            if (newLookUpType != null)
            {
                lookUpDbContext.Add(newLookUpType);

                await lookUpDbContext.SaveChangesAsync();

                return newLookUpType.Id;
            }

            return 0;
        }

        public async Task<bool> PutLookUpType(int id, LookUpType updatedLookUpType)
        {
            if (updatedLookUpType != null)
            {
                lookUpDbContext.Entry(updatedLookUpType).State = EntityState.Modified;

                try
                {
                    await lookUpDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LookUpTypeExists(id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return true;
        }

        private bool LookUpTypeExists(int id)
        {
            return lookUpDbContext.LookUpTypes.Any(e => e.Id == id);
        }
    }
}

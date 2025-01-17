using LookUpData.Data;
using LookUpData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookUpService
{
    public class CascadingLookUpsService : ICascadingLookUpsService
    {
        private readonly LookUpDbContext lookUpDbContext;

        public CascadingLookUpsService(LookUpDbContext lookUpDbContext)
        {
            this.lookUpDbContext = lookUpDbContext;
        }

        public async Task<bool> DeleteCascadingLookUp(int id)
        {
            var cascadingLookUp = await lookUpDbContext.CascadingLookUps.FindAsync(id);

            if (cascadingLookUp != null)
            {
                lookUpDbContext.Remove(cascadingLookUp);

                await lookUpDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CascadingLookUp> GetCascadingLookUp(int id)
        {
            var cascadingLookup = await lookUpDbContext.CascadingLookUps.FindAsync(id);

            return cascadingLookup;
        }

        public async Task<List<CascadingLookUp>> GetCascadingLookUpsForParent(int parentId)
        {
            var cascadingLookUps = await lookUpDbContext
                .CascadingLookUps
                .AsNoTracking()
                .Where(C => C.ParentId == parentId)
                .ToListAsync();

            return cascadingLookUps;
        }

        public async Task<int> PostCascadingLookUp(CascadingLookUp cascadingLookUp)
        {
            if (cascadingLookUp != null)
            {
                lookUpDbContext.Add(cascadingLookUp);

                await lookUpDbContext.SaveChangesAsync();

                return cascadingLookUp.Id;
            }

            return 0;
        }

        public async Task<bool> PutCascadingLookUp(int id, CascadingLookUp cascadingLookUp)
        {
            lookUpDbContext.Entry(cascadingLookUp).State = EntityState.Modified;

            try
            {
                await lookUpDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CascadingLookUpExists(id))
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

            private bool CascadingLookUpExists(int id)
            {
                return lookUpDbContext.CascadingLookUps.Any(C => C.Id == id);
            }
        }
    }

using LookUpData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LookUpService
{
    public interface ILookUpTypesService
    {

        Task<LookUpType> GetLookUpType(int id);

        Task<List<LookUpType>> GetLookUpTypes();

        Task<int> PostLookUpType(LookUpType newLookUpType);

        Task<bool> PutLookUpType(int id, LookUpType updatedLookUpType);

        Task<bool> DeleteLookUpType(int id);

    }
}

using LookUpData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LookUpService
{
    public interface ICascadingLookUpsService
    {
        Task<int> PostCascadingLookUp(CascadingLookUp cascadingLookUp);

        Task<CascadingLookUp> GetCascadingLookUp(int id);

        Task<bool> PutCascadingLookUp(int id, CascadingLookUp cascadingLookUp);

        Task<bool> DeleteCascadingLookUp(int id);

        Task<List<CascadingLookUp>> GetCascadingLookUpsForParent(int parentId);
    }
}

using System.ComponentModel.DataAnnotations;

namespace LookUpAbstraction.DTO.CascadingLookUp.Request
{
    public class CreateCascadingLookUpDTO
    {
        [Required]
        public int ParentId
        {
            get;
            set;
        }

        [Required]
        public int ChildId
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LookUpAbstraction.DTO.CascadingLookUp.Request
{
    public class UpdateCascadingLookUpDTO
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

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

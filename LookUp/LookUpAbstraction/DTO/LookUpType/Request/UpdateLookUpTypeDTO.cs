using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LookUpAbstraction.DTO.LookUpType.Request
{
    public class UpdateLookUpTypeDTO
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Type
        {
            get;
            set;
        }
    }
}

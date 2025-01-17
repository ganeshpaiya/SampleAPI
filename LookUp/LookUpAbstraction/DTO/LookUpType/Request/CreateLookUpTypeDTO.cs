using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LookUpAbstraction.DTO.LookUpType.Request
{
    public class CreateLookUpTypeDTO
    {
        [Required]
        public string Type
        {
            get;
            set;
        }
    }
}

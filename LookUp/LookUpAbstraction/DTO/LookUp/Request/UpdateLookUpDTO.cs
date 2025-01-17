using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LookUpAbstraction.DTO.LookUp.Request
{
    public class UpdateLookUpDTO
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public int LookUpTypeId
        {
            get;
            set;
        }

        [Required]
        public string NameEn
        {
            get;
            set;
        }

        [Required]
        public string NameAr
        {
            get;
            set;
        }
    }
}

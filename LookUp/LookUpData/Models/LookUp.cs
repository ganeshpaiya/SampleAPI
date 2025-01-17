using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LookUpData.Models
{
    public class LookUp
    {
        public int Id
        {
            get;
            set;
        }

        public int LookUpTypeId
        {
            get;
            set;
        }

        public LookUpType LookUpType
        {
            get;
            set;
        }

        public string NameEn
        {
            get;
            set;
        }

        public string NameAr
        {
            get;
            set;
        }

        public ICollection<CascadingLookUp> ParentInCascadingLookUps { get; set; }

        public ICollection<CascadingLookUp> ChildInCascadingLookUps { get; set; }
    }
}

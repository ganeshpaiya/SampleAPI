using System;
using System.Collections.Generic;
using System.Text;

namespace LookUpData.Models
{
    public class LookUpType
    {
        public int Id
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public ICollection<LookUp> LookUps
        {
            get;
            set;
        }
    }
}

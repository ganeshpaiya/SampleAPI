using System;
using System.Collections.Generic;
using System.Text;

namespace LookUpAbstraction.DTO.CascadingLookUp.Response
{
    public class CascadingLookUpDTO
    {
        public int Id
        {
            get;
            set;
        }

        public int ParentId
        {
            get;
            set;
        }

        public int ChildId
        {
            get;
            set;
        }
    }
}

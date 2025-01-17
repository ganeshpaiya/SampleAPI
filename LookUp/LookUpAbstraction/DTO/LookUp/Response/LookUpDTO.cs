using System;
using System.Collections.Generic;
using System.Text;

namespace LookUpAbstraction.DTO.LookUp.Response
{
    public class LookUpDTO
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
    }
}

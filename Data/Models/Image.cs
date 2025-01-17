using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
   public class Image
    {
        public int Id
        {
            get;
            set;
        }

        public string ImageTitle
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public byte[] ImageData
        {
            get;
            set;
        }

        public int ProductId
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }
    }
}

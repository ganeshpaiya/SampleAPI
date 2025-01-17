using System;
using System.Collections.Generic;

namespace MerchantData.Models
{
    public sealed class Product
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public bool HasSale
        {
            get;
            set;
        }

        public double SalePercent
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public DateTime ModifiedDate
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public bool Deleted
        {
            get;
            set;
        }

        public ICollection<Image> Image
        {
            get;
            set;
        }
    }
}

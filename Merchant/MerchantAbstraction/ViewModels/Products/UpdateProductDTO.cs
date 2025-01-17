using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantAbstraction.ViewModels.Products
{
    public sealed class UpdateProductDTO
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

        public DateTime ModifiedBy
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
    }
}

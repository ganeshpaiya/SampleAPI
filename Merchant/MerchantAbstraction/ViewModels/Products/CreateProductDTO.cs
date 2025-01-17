using System;

namespace MerchantAbstraction.ViewModels.Products
{
    public sealed class CreateProductDTO
    {
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

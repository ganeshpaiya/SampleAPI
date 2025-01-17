using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.ViewModels
{
    public class ProductOrder
    {
        public int Id
        {
            get;
            set;
        }

        public int Quintity
        {
            get;
            set;
        }

        public int ProductId
        {
            get;
            set;
        }

        public int OrderId
        {
            get;
            set;
        }

        public Order Order
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }

        internal static ProductOrder ToViewModel(Data.Models.ProductOrder productOrder)
        {
            return new ProductOrder()
            {
                Id = productOrder.Id,
                OrderId = productOrder.OrderId,
                Product = Product.ToViewModel(productOrder.Product),
                ProductId= productOrder.ProductId,
                Quintity= productOrder.Quintity
            };
        }

        internal Data.Models.ProductOrder ToUpdateModel()
        {
                return new Data.Models.ProductOrder()
                {
                    Id = this.Id,
                    OrderId = this.OrderId,
                    ProductId = this.ProductId,
                    Quintity = this.Quintity
                };
        }

        internal Data.Models.ProductOrder ToModel()
        {
                return new Data.Models.ProductOrder()
                {
                    ProductId = this.ProductId,
                    Quintity = this.Quintity
                };
        }

    }
}

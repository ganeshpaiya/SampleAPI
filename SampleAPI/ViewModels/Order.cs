using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.ViewModels
{
    public class Order
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime OrderPlaced
        {
            get;
            set;
        }

        public DateTime? OrderFulfiled
        {
            get;
            set;
        }

        [Required]
        public int CustomerId
        {
            get;
            set;
        }

        public ICollection<ProductOrder> ProductOrders
        {
            get;
            set;
        }

        internal static Order ToViewModel(Data.Models.Order order)
        {
            return new Order()
            {
                Id = order.Id,
               CustomerId= order.CustomerId,
               OrderFulfiled= order.OrderFulfiled,
               OrderPlaced=DateTime.Now.ToUniversalTime(),
               ProductOrders= order.ProductOrders.Select(x=> ProductOrder.ToViewModel(x)).ToList(),
            };
        }

        internal Data.Models.Order ToModel(bool isUpdate)
        {
            if (isUpdate)
            {
                return new Data.Models.Order()
                {
                    Id = this.Id,
                    CustomerId = this.CustomerId,
                    OrderFulfiled = this.OrderFulfiled,
                    ProductOrders = this.ProductOrders.Select(x => x.ToUpdateModel()).ToList()
                };
            }
            else
            {
                return new Data.Models.Order()
                {
                    CustomerId = this.CustomerId,
                    OrderPlaced = DateTime.Now.ToUniversalTime(),
                    ProductOrders = this.ProductOrders.Select(x => x.ToModel()).ToList()
                };
            }

        }
    }
}

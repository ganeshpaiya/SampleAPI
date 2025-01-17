using System;
using System.Collections.Generic;

namespace Data.Models
{
    public sealed class Order
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

        public int CustomerId
        {
            get;
            set;
        }

        public Customer Customer
        {
            get;
            set;
        }

        public ICollection<ProductOrder> ProductOrders
        {
            get;
            set;
        }
    }
}
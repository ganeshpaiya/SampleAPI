﻿namespace Data.Models
{
    public sealed class ProductOrder
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
    }
}
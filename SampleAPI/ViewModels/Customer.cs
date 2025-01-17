using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.ViewModels
{
    public sealed class Customer
    {

        public int Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string? Address
        {
            get;
            set;
        }

        public string? Phone
        {
            get;
            set;
        }

        //public ICollection<Order> orders
        //{
        //    get;
        //    set;
        //}

        internal static Customer ToViewModel(Data.Models.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Address = customer.Address,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone
            };
        }

        internal Data.Models.Customer ToModel(bool isUpdate)
        {
            if (isUpdate)
            {
                return new Data.Models.Customer()
                {
                    Id = this.Id,
                    Address = this.Address,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Phone = this.Phone
                };
            }
            else
            {
                return new Data.Models.Customer()
                {
                    Address = this.Address,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Phone = this.Phone
                };
            }

        }
    }

}

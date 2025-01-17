using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.ViewModels
{
    public sealed class Product
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Name
        {
            get;
            set;
        }

        [Required]
        public decimal Price
        {
            get;
            set;
        }

        internal static Product ToViewModel(Data.Models.Product product)
        {
                return new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
        }

        internal  Data.Models.Product ToModel(bool isUpdate)
        {
            if(isUpdate)
            {
                return new Data.Models.Product()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Price = this.Price
                };
            }
            else
            {
                return new Data.Models.Product()
                {
                    Name = this.Name,
                    Price = this.Price
                };
            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleAPI.ViewModels
{
    public class Image
    {

        public int Id
        {
            get;
            set;
        }

        [Required]
        public string ImageTitle
        {
            get;
            set;
        }

        [Required]
        public string Type
        {
            get;
            set;
        }

        [Required]
        public int ProductId
        {
            get;
            set;
        }

        [Required]
        public string ImageBase64Data
        {
            get;
            set;
        }
        
        internal static Image ToViewModel(Data.Models.Image image)
        {
            return new Image()
            {
                Id = image.Id,
                ImageBase64Data = Convert.ToBase64String(image.ImageData),
                ImageTitle = image.ImageTitle,
                ProductId = image.ProductId,
                Type = image.Type
            };
        }

        internal Data.Models.Image ToModel(bool isUpdate)
        {
            if (isUpdate)
            {
                return new Data.Models.Image()
                {
                    Id = this.Id,
                    ImageData = Encoding.ASCII.GetBytes(this.ImageBase64Data),
                    ImageTitle = this.ImageTitle,
                    ProductId = this.ProductId,
                    Type = this.Type
                };
            }
            else
            {
                return new Data.Models.Image()
                {
                    ImageData = Encoding.ASCII.GetBytes(this.ImageBase64Data),
                    ImageTitle = this.ImageTitle,
                    ProductId = this.ProductId,
                    Type = this.Type
                };
            }

        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MerchantAbstraction.ViewModels.Images
{
    public sealed class UpdateImageDTO
    {
        [Required]
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
    }
}

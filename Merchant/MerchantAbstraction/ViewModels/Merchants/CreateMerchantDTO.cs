using MerchantAbstraction.ViewModels.Locations;
using System;
using System.ComponentModel.DataAnnotations;

namespace MerchantAbstraction.ViewModels.Merchants
{
    public sealed class CreateMerchantDTO
    {
        [Required]
        public string ShopName
        {
            get;
            set;
        }

        [Required]
        public string FirstName
        {
            get;
            set;
        }

        [Required]
        public string LastName
        {
            get;
            set;
        }

        [Required]
        public string Phone
        {
            get;
            set;
        }

        [Required]
        public int[] ServiceType
        {
            get;
            set;
        }

        [Required]
        public bool FreeDeliveryActive
        {
            get;
            set;
        }

        [Required]
        public DateTime StartWorkingHour
        {
            get;
            set;
        }

        [Required]
        public DateTime StopWorkingHour
        {
            get;
            set;
        }

        [Required]
        public int[] AcceptedTypeOfPayment
        {
            get;
            set;
        }

        [Required]
        public string Email
        {
            get;
            set;
        }
    }
}

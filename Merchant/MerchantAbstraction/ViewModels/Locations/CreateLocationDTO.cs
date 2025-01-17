using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MerchantAbstraction.ViewModels.Locations
{
    public sealed class CreateLocationDTO
    {
        [Required]
        public string CityId
        {
            get;
            set;
        }

        [Required]
        public double Latitude
        {
            get;
            set;
        }

        [Required]
        public double Longitude
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }


        public string BuildingNumber
        {
            get;
            set;
        }

        public string FloorNumber
        {
            get;
            set;
        }
    }
}

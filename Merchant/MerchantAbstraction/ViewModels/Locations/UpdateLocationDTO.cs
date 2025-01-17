using System.ComponentModel.DataAnnotations;

namespace MerchantAbstraction.ViewModels.Locations
{
    public sealed class UpdateLocationDTO
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

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

namespace LocationAbstraction.ViewModels.Locations
{
    public sealed class LocationDTO
    {
        public int Id
        {
            get;
            set;
        }

        public string CityId
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

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

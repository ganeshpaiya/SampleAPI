using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MerchantData.Models
{
    public sealed class Merchant
    {
        public int Id
        {
            get;
            set;
        }

        public string ShopName
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

        public int LocationId
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public bool Deleted
        {
            get;
            set;
        }

        public string ServiceTypeList
        {
            get;
            set;
        }

        [NotMapped]
        public int[] ServiceType
        {
            get
            {
                return Array.ConvertAll(ServiceTypeList.Split(';'), int.Parse);
            }
            set
            {
                ServiceType = value;
                ServiceTypeList = String.Join(";", ServiceType.Select(p => p.ToString()).ToArray());
            }
        }

        public  bool FreeDeliveryActive 
        {
            get;
            set;
        }

        public bool HasPromoCode
        {
            get;
            set;
        }

        public DateTime StartWorkingHour
        {
            get;
            set;
        }

        public DateTime StopWorkingHour
        {
            get;
            set;
        }

        public string AcceptedTypeOfPaymentList
        {
            get;
            set;
        }

        [NotMapped]
        public int[] AcceptedTypeOfPayment
        {
            get
            {
                return Array.ConvertAll(AcceptedTypeOfPaymentList.Split(';'), int.Parse);
            }
            set
            {
                AcceptedTypeOfPayment = value;
                AcceptedTypeOfPaymentList = String.Join(";", AcceptedTypeOfPayment.Select(p => p.ToString()).ToArray());
            }
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public DateTime ModifiedDate
        {
            get;
            set;
        }


        public string ModifiedBy
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public sealed  class Product
    {
        public int Id 
        { 
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price
        {
            get;
            set;
        }

        public ICollection<Image> Image
        {
            get;
            set;
        }
    }
}

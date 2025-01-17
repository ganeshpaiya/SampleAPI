using System.ComponentModel.DataAnnotations.Schema;

namespace LookUpData.Models
{
    public class CascadingLookUp
    {
        public int Id
        {
            get;
            set;
        }

        [ForeignKey(nameof(ParentLookUp)), Column(Order = 0)]

        public int ParentId
        {
            get;
            set;
        }

        public LookUp ParentLookUp
        {
            get;
            set;
        }

        [ForeignKey(nameof(ChildLookUp)), Column(Order = 0)]

        public int ChildId
        {
            get;
            set;
        }

        public LookUp ChildLookUp
        {
            get;
            set;
        }

    }
}

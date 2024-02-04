using System.ComponentModel;

namespace IntegracijskiModul.BLModels
{
    public class BLCountry
    {
        public int Id { get; set; }

        [DisplayName("Country")]
        public string Name { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public virtual ICollection<BLUser> Users { get; set; } = new List<BLUser>();
    }
}

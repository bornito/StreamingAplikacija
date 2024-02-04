namespace IntegracijskiModul.BLModels
{
    public class BLImage
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public virtual ICollection<BLVideo> Videos { get; set; } = new List<BLVideo>();
    }
}

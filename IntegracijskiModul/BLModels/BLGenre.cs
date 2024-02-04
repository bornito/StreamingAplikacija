namespace IntegracijskiModul.BLModels
{
    public class BLGenre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual ICollection<BLVideo> Videos { get; set; } = new List<BLVideo>(); // public List<string>? Videos { get; set; }
    }
}

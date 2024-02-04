namespace IntegracijskiModul.BLModels
{
    public class BLTag
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<BLVideoTag> VideoTags { get; set; } = new List<BLVideoTag>();
    }
}

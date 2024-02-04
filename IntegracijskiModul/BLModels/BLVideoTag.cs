namespace IntegracijskiModul.BLModels
{
    public class BLVideoTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int VideoId { get; set; }

        public virtual BLTag Tag { get; set; } = null!;
        public virtual BLVideo Video { get; set; } = null!;
    }
}

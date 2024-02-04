namespace AdministrativniModul.ViewModeli
{
    public class VMVideoTag
    {
        public int Id { get; set; }

        public int VideoId { get; set; }
        public virtual VMVideo Video { get; set; } = null!;

        public int TagId { get; set; }
        public virtual VMTag Tag { get; set; } = null!;

    }
}

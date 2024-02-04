namespace IntegracijskiModul.BLModels
{
    public class BLNotification
    {
        public int Id { get; set; }
        public string Reciever { get; set; } = null!;
        public string? Subject { get; set; }
        public string? EmailBody { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Sent { get; set; }
    }
}

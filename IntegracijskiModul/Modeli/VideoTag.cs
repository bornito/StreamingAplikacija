﻿namespace IntegracijskiModul.Modeli
{
    public partial class VideoTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int VideoId { get; set; }

        public virtual Tag Tag { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;

    }
}

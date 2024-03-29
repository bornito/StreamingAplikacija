﻿namespace IntegracijskiModul.Modeli
{
    public partial class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<VideoTag> VideoTags { get; set; } = new List<VideoTag>();
    }
}

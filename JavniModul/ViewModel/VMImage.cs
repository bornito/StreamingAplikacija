﻿using System.ComponentModel.DataAnnotations;

namespace AdministrativniModul.ViewModeli
{
    public class VMImage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }

        public virtual ICollection<VMVideo> Videos { get; set; } = new List<VMVideo>();
    }
}

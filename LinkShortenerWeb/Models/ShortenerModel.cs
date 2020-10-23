using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortenerWeb.Models
{
    public class ShortenerModel
    {
        [Required]
        [Display(Name = "Link: ")]
        public string Link { get; set; }
        [Required]
        [Display(Name = "Slug: ")]
        public string Slug { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortenerWeb.DataAccess
{
    public class Shortener
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Slug { get; set; }
        
        [Required]
        public string Link { get; set; }

    }
}

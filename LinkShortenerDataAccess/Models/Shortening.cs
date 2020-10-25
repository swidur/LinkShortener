using System.ComponentModel.DataAnnotations;

namespace LinkShortenerDataAccess.Models
{
    public class Shortening
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Slug { get; set; }
        
        [Required]
        public string Link { get; set; }

    }
}

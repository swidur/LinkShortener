using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public bool IsDeleted { get; set; } = false;
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}

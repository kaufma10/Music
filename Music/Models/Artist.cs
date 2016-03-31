using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        [Required(ErrorMessage = "Artist's name is required")]
        public string Name { get; set; }
        public string Bio { get; set; }
    }
}
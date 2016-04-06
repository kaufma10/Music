using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Music.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        [Required(ErrorMessage = "Playlist Name is required")]
        public string Name { get; set; }
        public List<Album> Albums { get; set; }

    }
}
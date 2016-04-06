using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Music.Models
{
    public class NewModel
    {
        public int PlayListId { get; set; }
        public int AlbumID { get; set; }
        public string Title { get; set; }
        public SelectList Playlist { get; set; }
        public NewModel()
        {
            PlayListId = 0;
            AlbumID = 0;
        }
    }
}
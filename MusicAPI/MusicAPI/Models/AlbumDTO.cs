using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicAPI.Models
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string  Album { get; set; }
        public string ArtistName { get; set; }
    }
}
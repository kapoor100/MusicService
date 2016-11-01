using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MusicAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    
}
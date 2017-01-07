using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String Name { get; set; }
        [Required]
        [Display(Name="Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        
        [Required]
        [Display(Name ="Number in Stock")]
        public int NumbersInStock { get; set; }

        public MovieGenre MovieGenre { get; set; }
        public byte MovieGenreId { get; set; }

        
    }
}
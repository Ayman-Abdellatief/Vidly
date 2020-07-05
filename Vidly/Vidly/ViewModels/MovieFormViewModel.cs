using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please Enter Movie Name.")]
        [StringLength(255)]
        public string Name { get; set; }
        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Display(Name = "Relase Date")]
        public DateTime? RelaseDate { get; set; }
        
        [Display(Name = "Number In Stock")]
        [Range(1, 20, ErrorMessage = "The field number in stock must be between 1 and 20 .")]
        [Required]
        public short? NumberInStock { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Title
        {
            get
            {
                if(Id != 0)
                {
                  return   "Edit";
                }
                else
                {
                    return "New";
                }
            }

            
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }
        public MovieFormViewModel(Movie Movie)
        {
            Id = Movie.Id;
            Name = Movie.Name;
            RelaseDate = Movie.RelaseDate;
            NumberInStock = Movie.NumberInStock;
            GenreId = Movie.GenreId;
        }
      

    }
}
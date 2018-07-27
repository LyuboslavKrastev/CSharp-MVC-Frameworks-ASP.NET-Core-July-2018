using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Models.BindingModels
{
    public class MovieBindingModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 symbols long.")]
        [MaxLength(50, ErrorMessage = "Title must not be longer than 50 symbols")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 symbols long.")]
        [MaxLength(400, ErrorMessage = "Description must not be longer than 400 symbols")]
        public string Description { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Director name must be at least 2 symbols long.")]
        [MaxLength(30, ErrorMessage = "Director ")]
        public string Director { get; set; }

        [Required]
        [Url]
        [Display(Name = "Cover image")]
        public string ImageUrl { get; set; }
    }
}

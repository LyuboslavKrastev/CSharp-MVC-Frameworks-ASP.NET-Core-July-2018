using System.ComponentModel.DataAnnotations;

namespace FDMC.App.Models.ViewModels
{
    public class CatDetailsViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Breed { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
    }
}

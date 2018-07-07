using System.ComponentModel.DataAnnotations;

namespace FDMC.App.Models.BindingModels
{
    public class CatCreatingBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        [Display(Name = "Image Address")]
        public string ImageUrl { get; set; }
    }
}

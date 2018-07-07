using System;
using System.ComponentModel.DataAnnotations;

namespace FDMC.Models
{
    public class Cat
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        public string ImageUrl { get; set; }
    }
}

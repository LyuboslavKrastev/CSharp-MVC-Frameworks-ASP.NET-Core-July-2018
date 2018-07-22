using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Director
    {
        public Director()
        {
            this.Movies = new List<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}
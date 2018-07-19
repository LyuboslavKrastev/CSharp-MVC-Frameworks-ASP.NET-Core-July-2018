using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Author
    {
        public int Id { get; set; }

        //[Required]
        //[MinLength(2)]
        //[MaxLength(30)]
        public string Name { get; set; }

        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}

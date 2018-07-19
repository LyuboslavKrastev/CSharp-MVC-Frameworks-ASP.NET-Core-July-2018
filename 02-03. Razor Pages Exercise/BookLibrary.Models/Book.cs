using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        //[Required]
        //[MinLength(2)]
        //[MaxLength(50)]
        public string Title { get; set; }

        //[Required]
        //[MinLength(5)]
        //[MaxLength(250)]
        public string Description { get; set; }

        //[Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public string CoverImage { get; set; }

        public IEnumerable<BorrowersBooks> Borrowers { get; set; } = new List<BorrowersBooks>();
    }
}
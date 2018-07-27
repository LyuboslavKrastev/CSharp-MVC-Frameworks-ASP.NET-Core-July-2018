using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        public Book()
        {
            this.Borrowers = new List<BorrowersBooks>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 symbols long.")]
        [MaxLength(50, ErrorMessage = "Title must not be longer than 50 symbols")]
        public string Title { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 symbols long.")]
        [MaxLength(400, ErrorMessage = "Description must not be longer than 400 symbols")]
        public string Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        [Url]
        public string CoverImage { get; set; }

        public IEnumerable<BorrowersBooks> Borrowers { get; set; }
    }
}
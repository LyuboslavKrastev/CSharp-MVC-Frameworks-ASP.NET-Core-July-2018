using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Borrower
    {
        public Borrower()
        {
            this.BorrowedBooks = new List<BorrowersBooks>();
            this.BorrowedMovies = new List<BorrowersMovies>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Borrower Name must be at least 2 symbols long")]
        [MaxLength(30, ErrorMessage = "Borrower Name must not be longer than 30 symbols")]
        public string Name { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Address must be at least 5 symbols long")]
        [MaxLength(250, ErrorMessage = "Address must not be longer than 250 symbols")]
        public string Address { get; set; }

        public IEnumerable<BorrowersBooks> BorrowedBooks { get; set; }

        public IEnumerable<BorrowersMovies> BorrowedMovies { get; set; }

    }
}

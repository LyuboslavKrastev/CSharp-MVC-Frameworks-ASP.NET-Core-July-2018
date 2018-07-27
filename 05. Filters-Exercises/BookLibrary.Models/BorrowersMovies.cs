using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class BorrowersMovies
    {
        public int Id { get; set; }

        [Required]
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
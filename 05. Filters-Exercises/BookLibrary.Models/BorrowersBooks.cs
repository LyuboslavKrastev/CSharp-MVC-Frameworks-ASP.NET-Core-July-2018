﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class BorrowersBooks
    {
        public int Id { get; set; }

        [Required]
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}

using BookLibrary.App.Models;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookLibrary.App.Pages.Books
{
    public class DetailsModel : BaseModel
    {
        public DetailsModel(BookLibraryAppContext dbContext) 
            : base(dbContext)
        {
            this.UnavailablePeriods = new List<BorrowersBooks>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public bool IsBorrowed { get; set; }

        public List<BorrowersBooks> UnavailablePeriods { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        public IActionResult OnGet(int id)
        {
            var book = this.DbContext
                .Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return this.NotFound();
            }

            this.Id = book.Id;
            this.Title = book.Title;
            this.Description = book.Description;
            this.ImageUrl = book.CoverImage;
            this.Author = book.Author.Name;
            this.IsBorrowed = this.DbContext.BorrowedBooks.Any(b => b.BookId == this.Id &&(b.EndDate == null || b.EndDate > DateTime.Now));

            if (this.IsBorrowed)
            {
                var borrowingPeriods = this.DbContext.BorrowedBooks
                    .Include(b => b.Borrower)
                    .Where(b => b.BookId == this.Id && b.IsAvailable == false).ToList();

                foreach (var borrowingPeriod in borrowingPeriods)
                {
                    this.UnavailablePeriods.Add(borrowingPeriod);
                }
            }

            return this.Page();
        }
        
        public IActionResult OnPost(int borrowedId)
        {
            var borrowedBook = this.DbContext.BorrowedBooks.Where(b => b.Id == borrowedId && b.IsAvailable == false).FirstOrDefault();

            borrowedBook.EndDate = DateTime.Now;
            borrowedBook.IsAvailable = true;

            this.DbContext.SaveChanges();

            return this.OnGet(borrowedBook.BookId);
        }
    }
}
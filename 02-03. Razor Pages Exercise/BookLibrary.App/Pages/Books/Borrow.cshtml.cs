using BookLibrary.App.Models;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookLibrary.App.Pages.Books
{
    public class BorrowModel : BaseModel
    {
        public BorrowModel(BookLibraryAppContext dbContext)
            : base(dbContext)
        {
            this.Borrowers = new List<SelectListItem>();
            this.StartDate = DateTime.Now; 
        }

        [BindProperty]
        [Display(Name = "Borrower")]
        [Required]
        public int BorrowerId { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; } 

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<SelectListItem> Borrowers { get; private set; }

        public IActionResult OnGet()
        {
            this.Borrowers = this.DbContext.Borrowers
            .Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            })
            .ToList();

            return this.Page();
        }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.OnGet();                
            }

            if (this.EndDate != null && this.EndDate < this.StartDate.Date)
            {
                this.ModelState.AddModelError("Bad End Date", "End Date can't be before the start date.");
                return this.OnGet();           
            }

            var borrower = this.DbContext.Borrowers.Find(this.BorrowerId);

            var bookId = Convert.ToInt32(this.RouteData.Values["id"]);


            var book = this.DbContext.Books.Find(bookId);

            if (borrower == null || book == null)
            {
                this.ModelState.AddModelError("InvalidBookOrBorrower", "Invalid book or borrower");
                return this.OnGet();
               
            }

            bool currentlyTaken = this.DbContext.BorrowedBooks
                .Any(b => b.BookId == book.Id && 
                (b.EndDate > this.StartDate || b.EndDate == null) 
                && b.IsAvailable == false);

            if (currentlyTaken)
            {
                this.ModelState.AddModelError("BookTaken", "This Book Is Already Taken For This Period");
                return this.OnGet();           
            }

            var borrowedBook = new BorrowersBooks
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };

            this.DbContext.BorrowedBooks.Add(borrowedBook);
            this.DbContext.SaveChanges();

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }
    }
}
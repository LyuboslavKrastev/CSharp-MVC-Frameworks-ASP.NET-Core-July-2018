using BookLibrary.App.Models;
using BookLibrary.App.Models.BindingModels;
using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace BookLibrary.App.Pages.Books
{
    public class BorrowModel : PageModel
    {
        private BorrowersService borrowersService;
        private BooksService booksService;

        public BorrowModel(BorrowersService  borrowersService, BooksService booksService) 
        {
            this.borrowersService = borrowersService;
            this.booksService = booksService;
            this.BorrowBindingModel = new BorrowBindingModel();
        }

        [BindProperty]
        public BorrowBindingModel BorrowBindingModel { get; set; }

        public IActionResult OnGet()
        {
            var borrowers = this.borrowersService.GetBorrowers();

            this.BorrowBindingModel.Borrowers = borrowers
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

            var borrower = this.borrowersService.FindById(this.BorrowBindingModel.BorrowerId);

            var bookId = Convert.ToInt32(this.RouteData.Values["id"]);

            var book = this.booksService.FindBookById(bookId);

            if (borrower == null || book == null)
            {
                this.ViewData["Errors"] = "Invalid book or borrower";
                return this.OnGet();
               
            }

            bool bookTaken = this.booksService.CheckBookBorrowingPeriods(book.Id, this.BorrowBindingModel.StartDate);

            if (bookTaken)
            {
                this.ViewData["Errors"] = "This book is already borrowed for this period.";
                return this.OnGet();           
            }

            var borrowedBook = new BorrowersBooks
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                StartDate = this.BorrowBindingModel.StartDate,
                EndDate = this.BorrowBindingModel.EndDate
            };

            this.booksService.BorrowBook(borrowedBook);

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }
    }
}
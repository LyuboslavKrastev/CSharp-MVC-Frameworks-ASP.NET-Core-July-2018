using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.App.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private BooksService booksService;

        public DetailsModel(BooksService booksService)
        {
            this.booksService = booksService;
            this.ProductDetails = new ProductDetailsViewModel();
        }

        public ProductDetailsViewModel ProductDetails { get; set; }

        public IActionResult OnGet(int id)
        {
            var book = this.booksService.GetBookWithAuthor(id);

            if (book == null)
            {
                return this.NotFound();
            }

            this.ProductDetails.Id = book.Id;
            this.ProductDetails.Title = book.Title;
            this.ProductDetails.Type = "Book";
            this.ProductDetails.Description = book.Description;
            this.ProductDetails.ImageUrl = book.CoverImage;
            this.ProductDetails.Creator = book.Author.Name;
            this.ProductDetails.IsBorrowed = booksService.CheckIfBookIsBorrowed(ProductDetails.Id);

            if (this.ProductDetails.IsBorrowed)
            {
                var borrowingPeriods = booksService.GetBookUnavailablePeriods(this.ProductDetails.Id);

                foreach (var borrowingPeriod in borrowingPeriods)
                {
                    this.ProductDetails.UnavailablePeriods.Add(new UnavailableProductPeriodsViewModel
                    {
                        Id = borrowingPeriod.Id,
                        StartDate = borrowingPeriod.StartDate,
                        EndDate = borrowingPeriod.EndDate,
                        BorrowerName = borrowingPeriod.Borrower.Name
                    });
                }
            }

            return this.Page();
        }

        public IActionResult OnPost(int borrowedId)
        {
            var borrowedBook = this.booksService
                .GetBorrowedBook(borrowedId);

            this.booksService.ReturnBook(borrowedBook);

            return this.OnGet(borrowedBook.BookId);
        }
    }
}
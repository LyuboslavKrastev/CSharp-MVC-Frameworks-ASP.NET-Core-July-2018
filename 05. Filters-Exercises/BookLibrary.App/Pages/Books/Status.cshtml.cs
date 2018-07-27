using System.Collections.Generic;
using System.Linq;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.App.Pages.Books
{
    public class StatusModel : PageModel
    {
        private BooksService booksService;

        public StatusModel(BooksService booksService) 
        {
            this.booksService = booksService;
            this.BorrowingPeriods = new List<ProductHistoryViewModel>();
        }

        public IEnumerable<ProductHistoryViewModel> BorrowingPeriods { get; set; }

        public void OnGet(int id)
        {
            this.BorrowingPeriods = this.booksService.GetBookHistory(id)
                .Select(b => new ProductHistoryViewModel
                {
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    BorrowerName = b.Borrower.Name
                })
                .ToList();
        }
    }
}
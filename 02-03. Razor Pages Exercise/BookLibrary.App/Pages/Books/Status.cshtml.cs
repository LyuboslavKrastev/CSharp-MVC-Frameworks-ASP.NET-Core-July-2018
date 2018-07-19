using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrary.App.Models;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.App.Pages.Books
{
    public class StatusModel : BaseModel
    {
        public StatusModel(BookLibraryAppContext dbContext)
           : base(dbContext)
        {
            this.BorrowingPeriods = new List<BookHistoryViewModel>();
        }
        public List<BookHistoryViewModel> BorrowingPeriods { get; set; }

        public void OnGet(int id)
        {
            this.BorrowingPeriods = this.DbContext.BorrowedBooks
                .Include(b => b.Borrower)
                .Where(b => b.BookId == id)
                .Select(b => new BookHistoryViewModel
                {
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    BorrowerName = b.Borrower.Name
                })
                .ToList();
        }
    }
}
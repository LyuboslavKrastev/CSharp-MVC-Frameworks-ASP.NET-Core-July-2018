using BookLibrary.App.Models;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.App.Pages
{
    public class IndexModel : BaseModel
    {
        public IndexModel(BookLibraryAppContext dbContext) 
            : base(dbContext){}

        public IEnumerable<BookDisplayViewModel> Books { get; set; }

        public void OnGet()
        {
            this.Books = this.DbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Borrowers)
                .OrderBy(b => b.Title)
                .Select(book => new BookDisplayViewModel
                {
                    Title = book.Title,
                    BookId = book.Id,
                    AuthorId = book.AuthorId,
                    Author = book.Author.Name,
                    Status = book.Borrowers.Any(b => b.BookId == book.Id && b.IsAvailable == false)? "Borrowed" : "At Home"
                })
                .ToList();

        }
    }
}

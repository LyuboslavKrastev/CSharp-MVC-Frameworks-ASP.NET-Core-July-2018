using BookLibrary.App.Models;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.App.Pages.Authors
{
    public class DetailsModel : BaseModel
    {
        public DetailsModel(BookLibraryAppContext dbContext) 
            : base(dbContext){}

        public string Name { get; set; }

        public IEnumerable<BookDisplayViewModel> Books { get; set; }

        public IActionResult OnGet(int id)
        {
            var author = this.DbContext
                .Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return this.NotFound();
            }

            this.Name = author.Name;
            this.Books = author.Books
                .Select(book => new BookDisplayViewModel
                {
                    Title = book.Title,
                    BookId = book.Id,
                    AuthorId = book.AuthorId,
                    Author = book.Author.Name,
                    Status = this.DbContext.BorrowedBooks.Any(b => b.BookId == book.Id && (b.StartDate < DateTime.Now)) == true ? "Borrowed" : "At Home"
                })
                .ToList();

            return this.Page();
        }
    }
}
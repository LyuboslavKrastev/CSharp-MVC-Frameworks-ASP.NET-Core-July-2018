using BookLibrary.App.Models;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookLibrary.App.Pages.Books
{
    public class AddModel : BaseModel
    {
        public AddModel(BookLibraryAppContext dbContext) 
            : base(dbContext) {}

        [BindProperty]
        [Required]
        public string Title { get; set; }

        [BindProperty]
        [Required]
        public string Description { get; set; }

        [BindProperty]
        [Required]
        public string Author { get; set; }

        [BindProperty]
        [Display(Name ="Image URL")]
        [Url]
        [Required]
        public string ImageUrl { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            var book = this.AddBook();

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }

        private Book AddBook()
        {
            Author author = GetOrCreateAuthor();

            var book = new Book
            {
                Title = this.Title,
                Description = this.Description,
                CoverImage = this.ImageUrl,
                AuthorId = author.Id
            };

            this.DbContext.Books.Add(book);

            this.DbContext.SaveChanges();

            return book;
        }

        private Author GetOrCreateAuthor()
        {
            var author = this.DbContext.Authors
                .FirstOrDefault(a => a.Name == this.Author);

            if (author == null)
            {
                author = new Author
                {
                    Name = this.Author
                };

                this.DbContext.Authors
                    .Add(author);

                this.DbContext.SaveChanges();
            }

            return author;
        }
    }
}
using BookLibrary.Models;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Pages.Books
{
    public class AddModel : PageModel
    {
        private AuthorsService authorsService;
        private BooksService booksService;

        public AddModel(AuthorsService authorsService, BooksService booksService)
        {
            this.authorsService = authorsService;
            this.booksService = booksService;
        }

        [BindProperty]
        [Required]
        [MinLength(2, ErrorMessage = "Title must be at least 2 symbols long.")]
        [MaxLength(50, ErrorMessage = "Title must not be longer than 50 symbols")]
        public string Title { get; set; }

        [BindProperty]
        [Required]
        [MinLength(5, ErrorMessage = "Description must be at least 5 symbols long.")]
        [MaxLength(400, ErrorMessage = "Description must not be longer than 400 symbols")]
        public string Description { get; set; }

        [BindProperty]
        [Required]
        public string Author { get; set; }

        [BindProperty]
        [Url]
        [Required]
        [Display(Name = "Cover image")]
        public string ImageUrl { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return this.Page();
            }

            var author = this.authorsService.GetOrCreateAuthor(this.Author);

            var book = new Book
            {
                Title = this.Title,
                Description = this.Description,
                CoverImage = this.ImageUrl,
                AuthorId = author.Id
            };

            this.booksService.AddBook(book);

            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }
    }
}
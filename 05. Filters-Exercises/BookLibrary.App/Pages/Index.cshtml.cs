using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.App.Pages
{
    public class IndexModel : PageModel
    {
        private BooksService booksService;
        private MoviesService moviesService;


        public IndexModel(BooksService booksService, MoviesService moviesService)
        {
            this.booksService = booksService;
            this.moviesService = moviesService;
            this.Books = new List<BookDisplayViewModel>();
            this.Movies = new List<MovieDisplayViewModel>();
        }

        public IEnumerable<BookDisplayViewModel> Books { get; set; }
        public IEnumerable<MovieDisplayViewModel> Movies { get; set; }

        public void OnGet()
        {
            this.Books = this.booksService.GetAllBooksWithAuthorsAndBorrowers()
                .Select(book => new BookDisplayViewModel
                {
                    Title = book.Title,
                    BookId = book.Id,
                    AuthorId = book.AuthorId,
                    Author = book.Author.Name,
                    Status = book.Borrowers.Any(b => b.BookId == book.Id && b.IsAvailable == false) ? "Borrowed" : "At Home"
                })
                .ToList();

            this.Movies = this.moviesService.GetAllMoviesWithDirectorsAndBorrowers()
                .Select(movie => new MovieDisplayViewModel
                {
                    Title = movie.Title,
                    MovieId = movie.Id,
                    DirectorId = movie.DirectorId,
                    Director = movie.Director.Name,
                    Status = movie.Borrowers.Any(m => m.MovieId == movie.Id && m.IsAvailable == false) ? "Borrowed" : "At Home"
                })
                .ToList();

        }
    }
}

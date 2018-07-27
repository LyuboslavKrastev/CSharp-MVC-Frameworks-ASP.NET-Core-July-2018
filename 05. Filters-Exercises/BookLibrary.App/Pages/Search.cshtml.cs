using BookLibrary.App.Models;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BookLibrary.App.Pages
{
    public class SearchModel : PageModel
    {
        private BooksService booksService;
        private AuthorsService authorsService;
        private DirectorsService directorsService;
        private MoviesService moviesService;


        public SearchModel(BooksService booksService, AuthorsService authorsService, DirectorsService directorsService, MoviesService moviesService) 
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.directorsService = directorsService;
            this.moviesService = moviesService;
            this.SearchResult = new List<SearchViewModel>();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<SearchViewModel> SearchResult { get; private set; }

        public void OnGet()
        {
            if (this.SearchTerm == null)
            {
                return;
            }

            var authors = this.authorsService.FindAuthorsBySearchTerm(this.SearchTerm)
                .Select(a => new SearchViewModel
                {
                    Id = a.Id,
                    Name = this.ColorizeMatch(a.Name),
                    Type = "Author"
                });

            var directors = this.directorsService.FindDirectorsBySearchTerm(this.SearchTerm)
                .Select(a => new SearchViewModel
                {
                    Id = a.Id,
                    Name = this.ColorizeMatch(a.Name),
                    Type = "Director"
                });


            var books = this.booksService.FindBooksBySearchTerm(this.SearchTerm)
                .Select(m => new SearchViewModel
                {
                    Id = m.Id,
                    Name = this.ColorizeMatch(m.Title),
                    Type = "Book"
                });

            var movies = this.moviesService.FindMoviessBySearchTerm(this.SearchTerm)
                .Select(m => new SearchViewModel
                {
                    Id = m.Id,
                    Name = this.ColorizeMatch(m.Title),
                    Type = "Movie"
                });

            this.SearchResult.AddRange(authors);
            this.SearchResult.AddRange(directors);
            this.SearchResult.AddRange(books);
            this.SearchResult.AddRange(movies);

            this.SearchResult.OrderBy(sr => sr.Type).ThenBy(sr => sr.Name);
        }


        private string ColorizeMatch(string foundEntity)
        {
            return Regex.Replace(foundEntity, $"{Regex.Escape(this.SearchTerm)}",
                match => $"<strong class=\"text-danger\">{match.Groups[0].Value}</strong>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

    }
}
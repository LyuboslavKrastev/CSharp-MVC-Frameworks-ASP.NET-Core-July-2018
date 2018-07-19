using BookLibrary.App.Models;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BookLibrary.App.Pages
{
    public class SearchModel : BaseModel
    {
        public SearchModel(BookLibraryAppContext dbContext)
            : base(dbContext)
        {
            this.FoundAuthors = new List<AuthorSearchViewModel>();
            this.FoundBooks = new List<BookSearchViewModel>();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IList<AuthorSearchViewModel> FoundAuthors { get; private set; }
        public IList<BookSearchViewModel> FoundBooks { get; private set; }

        public void OnGet()
        {
            if (this.SearchTerm == null)
            {
                return;
            }

            this.FindAuthors();
            this.FindBooks();
        }

        private void FindAuthors()
        {
            this.FoundAuthors = this.DbContext.Authors
                .Where(a => a.Name.ToLower()
                .Contains(this.SearchTerm.ToLower()))
                .OrderBy(a => a.Name)
                .Select(a => new AuthorSearchViewModel
                {
                    Id = a.Id,
                    Name = this.ColorizeMatch(a.Name)
                })
                .ToList();
        }

        private void FindBooks()
        {
            this.FoundBooks = this.DbContext.Books
                .Where(b => b.Title.ToLower().Contains(this.SearchTerm.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => new BookSearchViewModel
                {
                    Id = b.Id,
                    Title = ColorizeMatch(b.Title),
                })
                .ToList();
        }

        private string ColorizeMatch(string foundEntity)
        {
            return Regex.Replace(foundEntity, $"{Regex.Escape(this.SearchTerm)}",
                match => $"<strong class=\"text-danger\">{match.Groups[0].Value}</strong>",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.App.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private AuthorsService authorsService;

        public DetailsModel(AuthorsService authorsService) 
        {
            this.authorsService = authorsService;
        }

        public string Name { get; set; }

        public IEnumerable<ProductListingViewModel> Books { get; set; }

        public IActionResult OnGet(int id)
        {
            var author = this.authorsService.GetAuthorWithBooks(id);

            if (author == null)
            {
                return this.NotFound();
            }

            this.Name = author.Name;
            this.Books = author.Books
                .Select(book => new ProductListingViewModel
                {
                    Id = book.Id,
                    Title = book.Title,              
                })
                .ToList();

            return this.Page();
        }
    }
}
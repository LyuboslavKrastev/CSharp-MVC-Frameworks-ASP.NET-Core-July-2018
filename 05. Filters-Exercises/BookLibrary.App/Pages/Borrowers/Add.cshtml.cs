using BookLibrary.App.Filters;
using BookLibrary.Models;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Pages.Borrowers
{
    [Authorize]
    public class AddModel : PageModel
    {
        private BorrowersService borrowersService;

        public AddModel(BorrowersService borrowersService) 
        {
            this.borrowersService = borrowersService;
        }

        [BindProperty]
        [Required]
        [MinLength(2, ErrorMessage = "Borrower Name must be at least 2 symbols long")]
        [MaxLength(30, ErrorMessage = "Borrower Name must not be longer than 30 symbols")]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [MinLength(5, ErrorMessage = "Address must be at least 5 symbols long")]
        [MaxLength(250, ErrorMessage = "Address Name must not longer than 250 symbols")]
        public string Address { get; set; }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var borrower = new Borrower
            {
                Name = this.Name,
                Address = this.Address
            };

            this.borrowersService.AddBorrower(borrower);

            return this.RedirectToPage("/Index");
        }
    }
}
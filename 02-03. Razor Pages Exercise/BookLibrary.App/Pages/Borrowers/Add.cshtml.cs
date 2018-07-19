using BookLibrary.App.Models;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Pages.Borrowers
{
    public class AddModel : BaseModel
    {
        public AddModel(BookLibraryAppContext dbContext) 
            : base(dbContext){}

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Address { get; set; }

        public IActionResult OnPost()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this.DbContext.Borrowers.Add(new Borrower
            {
                Name = this.Name,
                Address = this.Address
            });

            this.DbContext.SaveChanges();

            return this.RedirectToPage("/Index");
        }
    }
}
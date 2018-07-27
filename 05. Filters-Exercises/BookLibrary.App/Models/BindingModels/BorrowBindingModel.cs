using BookLibrary.App.Helpers.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Models.BindingModels
{
    public class BorrowBindingModel
    {
        public BorrowBindingModel()
        {
            this.Borrowers = new List<SelectListItem>();
            this.StartDate = DateTime.Now;
        }

        [BindProperty]
        [Display(Name = "Borrower")]
        [Required]
        public int BorrowerId { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [Required]
        [StartDateValidation]
        public DateTime StartDate { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [EndDateValidation("StartDate")]
        public DateTime? EndDate { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem> Borrowers { get; set; }
    }
}

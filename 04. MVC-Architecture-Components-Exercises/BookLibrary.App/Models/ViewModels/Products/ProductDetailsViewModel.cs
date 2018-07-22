using BookLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.App.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            this.UnavailablePeriods = new List<UnavailableProductPeriodsViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Creator { get; set; }

        public string ImageUrl { get; set; }

        public bool IsBorrowed { get; set; }

        public IList<UnavailableProductPeriodsViewModel> UnavailablePeriods { get; set; }

        public string Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}

using System.Collections.Generic;

namespace BookLibrary.App.Models.ViewModels
{
    public class DirectorDetailsViewModel
    {
        public DirectorDetailsViewModel()
        {
            this.Movies = new List<ProductListingViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<ProductListingViewModel> Movies { get; set; }
    }
}

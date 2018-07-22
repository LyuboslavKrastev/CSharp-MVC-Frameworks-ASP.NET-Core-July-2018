using System;

namespace BookLibrary.App.Models.ViewModels
{
    public class UnavailableProductPeriodsViewModel
    {
        public int Id { get; set; }

        public string BorrowerName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

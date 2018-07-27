using System;

namespace BookLibrary.App.Models.ViewModels
{
    public class ProductHistoryViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string BorrowerName { get; set; }
    }
}

using BookLibrary.Models;
using System;

namespace BookLibrary.App.Models.ViewModels
{
    public class BookDisplayViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }

        public string Status { get; set; }
    }
}

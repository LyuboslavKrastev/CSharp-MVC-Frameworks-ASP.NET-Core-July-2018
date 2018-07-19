using BookLibrary.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BookLibrary.App.Models
{
    public abstract class BaseModel : PageModel
    {
        protected BaseModel(BookLibraryAppContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public BookLibraryAppContext DbContext { get; private set; }
    }
}

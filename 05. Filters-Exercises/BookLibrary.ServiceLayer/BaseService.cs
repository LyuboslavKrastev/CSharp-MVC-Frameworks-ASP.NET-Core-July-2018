using BookLibrary.Data;

namespace BookLibrary.ServiceLayer
{
    public abstract class BaseService
    {
        protected BaseService(BookLibraryAppContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected BookLibraryAppContext DbContext { get; set; }
    }
}

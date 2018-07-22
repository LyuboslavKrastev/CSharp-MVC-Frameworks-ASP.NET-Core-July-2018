using BookLibrary.Data;
using BookLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.ServiceLayer
{
    public class BorrowersService : BaseService
    {
        public BorrowersService(BookLibraryAppContext dbContext) 
            : base(dbContext)
        {}

        public Borrower FindById(int id)
        {
            return this.DbContext.Borrowers.Find(id);
        }

        public void AddBorrower(Borrower borrower)
        {
            this.DbContext.Borrowers.Add(borrower);

            this.DbContext.SaveChanges();
        }

        public ICollection<Borrower> GetBorrowers()
        {
            return this.DbContext.Borrowers.ToArray();
        }
    }
}

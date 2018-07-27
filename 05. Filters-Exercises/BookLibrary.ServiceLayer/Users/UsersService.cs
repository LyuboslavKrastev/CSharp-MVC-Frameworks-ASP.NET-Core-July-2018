using BookLibrary.Data;
using BookLibrary.Models;
using System.Linq;

namespace BookLibrary.ServiceLayer
{
    public class UsersService : BaseService
    {
        public UsersService(BookLibraryAppContext dbContext) 
            : base(dbContext){}

        public bool CheckIfUserExists(string username, string passwordHash)
        {
            return this.DbContext.Users.Any(u => u.Username == username && u.PasswordHash == passwordHash);
        }
    }
}

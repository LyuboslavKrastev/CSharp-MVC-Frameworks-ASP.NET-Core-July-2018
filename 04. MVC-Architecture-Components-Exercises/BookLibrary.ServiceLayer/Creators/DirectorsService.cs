using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.ServiceLayer
{
    public class DirectorsService : BaseService
    {
        public DirectorsService(BookLibraryAppContext dbContext) : base(dbContext)
        {
        }

        public Director GetDirectorWithMovies(int directorId)
        {
            return this.DbContext
                .Directors
                .Include(d => d.Movies)
                .FirstOrDefault(d => d.Id == directorId);
        }

        public Director GetOrCreateDirector(string name)
        {
            var director = this.DbContext.Directors
                .FirstOrDefault(a => a.Name == name);

            if (director == null)
            {
                director = new Director
                {
                    Name = name
                };

                this.DbContext.Directors
                    .Add(director);

                this.DbContext.SaveChanges();
            }

            return director;
        }

        public ICollection<Director> FindDirectorsBySearchTerm(string searchTerm)
        {
            return this.DbContext.Directors
                .Where(a => a.Name.ToLower()
                .Contains(searchTerm.ToLower()))
                .ToArray();
        }
    }
}

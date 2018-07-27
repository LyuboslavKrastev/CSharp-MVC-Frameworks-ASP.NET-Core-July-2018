using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.ServiceLayer
{
    public class AuthorsService : BaseService
    {
        public AuthorsService(BookLibraryAppContext dbContext)
            : base(dbContext)
        {
        }

        public Author GetAuthorWithBooks(int authorId)
        {
            return this.DbContext
                .Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == authorId);
        }

        public Author GetOrCreateAuthor(string name)
        {
            var author = this.DbContext.Authors
                .FirstOrDefault(a => a.Name == name);

            if (author == null)
            {
                author = new Author
                {
                    Name = name
                };

                this.DbContext.Authors
                    .Add(author);

                this.DbContext.SaveChanges();
            }

            return author;
        }

        public ICollection<Author> FindAuthorsBySearchTerm(string searchTerm)
        {
            return this.DbContext.Authors
                .Where(a => a.Name.ToLower()
                .Contains(searchTerm.ToLower()))
                .ToArray();
        }


    }
}

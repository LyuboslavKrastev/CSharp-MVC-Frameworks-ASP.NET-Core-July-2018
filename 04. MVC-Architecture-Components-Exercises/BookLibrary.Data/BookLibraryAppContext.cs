using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class BookLibraryAppContext : DbContext
    {
        public BookLibraryAppContext(DbContextOptions<BookLibraryAppContext> options)
            :base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<BorrowersBooks> BorrowedBooks { get; set; }

        public DbSet<BorrowersMovies> BorrowedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(book => book.Borrowers)
                .WithOne(borrower => borrower.Book)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrower>()
               .HasMany(borrower => borrower.BorrowedBooks)
               .WithOne(book => book.Borrower)
               .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<Movie>()
               .HasMany(movie => movie.Borrowers)
               .WithOne(borrower => borrower.Movie)
               .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Borrower>()
               .HasMany(borrower => borrower.BorrowedMovies)
               .WithOne(movie => movie.Borrower)
               .HasForeignKey(m => m.BorrowerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

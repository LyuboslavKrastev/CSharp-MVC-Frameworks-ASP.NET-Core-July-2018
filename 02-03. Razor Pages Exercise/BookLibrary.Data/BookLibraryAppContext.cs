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

        public DbSet<Author> Authors { get; set; }

        public DbSet<Borrower> Borrowers { get; set; }

        public DbSet<BorrowersBooks> BorrowedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BorrowersBooks>()
            //    .HasKey(b => new { b.BorrowerId, b.BookId });

            modelBuilder.Entity<Book>()
                .HasMany(book => book.Borrowers)
                .WithOne(borrower => borrower.Book)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrower>()
               .HasMany(borrower => borrower.Books)
               .WithOne(book => book.Borrower)
               .HasForeignKey(b => b.BorrowerId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

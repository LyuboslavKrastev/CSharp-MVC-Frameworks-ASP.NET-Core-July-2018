using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.ServiceLayer
{
    public class BooksService : BaseService
    {
        public BooksService(BookLibraryAppContext dbContext) 
            : base(dbContext)
        {
        }

        public Book FindBookById(int bookId)
        {
            return this.DbContext.Books.Find(bookId);
        }

        public ICollection<Book> FindBooksBySearchTerm(string searchTerm)
        {
             return this.DbContext.Books
                .Where(b => b.Title.ToLower()
                .Contains(searchTerm.ToLower()))
                .ToArray();
        }

        public ICollection<Book> GetAllBooksWithAuthorsAndBorrowers()
        {
            return this.DbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Borrowers)
                .OrderBy(b => b.Title)
                .ToArray();
        }

        public void AddBook(Book book)
        {
            this.DbContext.Books.Add(book);

            this.DbContext.SaveChanges();
        }

        public void BorrowBook(BorrowersBooks borrowedBook)
        {
            this.DbContext.BorrowedBooks.Add(borrowedBook);
            this.DbContext.SaveChanges();
        }

        public Book GetBookWithAuthor(int bookId)
        {
            var book = this.DbContext.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == bookId);

            return book;
        }

        public ICollection<BorrowersBooks> GetBookUnavailablePeriods(int productDetailsId)
        {
            return this.DbContext.BorrowedBooks
                    .Include(b => b.Borrower)
                    .Where(b => b.BookId == productDetailsId && b.IsAvailable == false)
                    .ToArray();
        }

        public ICollection<BorrowersBooks> GetBookHistory(int bookId)
        {

            return this.DbContext.BorrowedBooks
                .Include(b => b.Borrower)
                .Where(b => b.BookId == bookId)
                .OrderBy(b => b.StartDate).ToArray();
        }

        public BorrowersBooks GetBorrowedBook(int borrowedId)
        {
            return this.DbContext.BorrowedBooks.Where(b => b.Id == borrowedId && b.IsAvailable == false)
                .FirstOrDefault();
        }

        public void ReturnBook(BorrowersBooks borrowedBook)
        {
            borrowedBook.EndDate = DateTime.Now;
            borrowedBook.IsAvailable = true;

            this.DbContext.SaveChanges();
        }

        public bool CheckBookBorrowingPeriods(int bookId, DateTime startDate)
        {
             return this.DbContext.BorrowedBooks.Any(b => b.BookId == bookId && (b.EndDate > startDate || b.EndDate == null)
               && b.IsAvailable == false);
        }

        public bool CheckIfBookIsBorrowed(int productDetailsId)
        {
            return this.DbContext.BorrowedBooks.Any(b => b.BookId == productDetailsId && (b.EndDate == null || b.EndDate > DateTime.Now));
        }
    }
}

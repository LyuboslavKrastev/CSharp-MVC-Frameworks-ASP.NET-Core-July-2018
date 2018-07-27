using System;
using System.Collections.Generic;
using System.Linq;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.ServiceLayer
{
    public class MoviesService : BaseService
    {
        public MoviesService(BookLibraryAppContext dbContext) : base(dbContext)
        {
        }

        public Movie FindById(int movieId)
        {
            return this.DbContext.Movies.Find(movieId);
        }

        public ICollection<Movie> FindMoviessBySearchTerm(string searchTerm)
        {
            return this.DbContext.Movies
               .Where(b => b.Title.ToLower()
               .Contains(searchTerm.ToLower()))
               .ToArray();
        }

        public ICollection<Movie> GetAllMoviesWithDirectorsAndBorrowers()
        {
            return this.DbContext.Movies
                .Include(b => b.Director)
                .Include(b => b.Borrowers)
                .OrderBy(b => b.Title)
                .ToArray();
        }

        public void AddMovie (Movie movie)
        {
            this.DbContext.Movies.Add(movie);

            this.DbContext.SaveChanges();
        }

        public void BorrowMovie(BorrowersMovies borrowedMovie)
        {
            this.DbContext.BorrowedMovies.Add(borrowedMovie);
            this.DbContext.SaveChanges();
        }

        public Movie GetMovieWithDirector(int movieId)
        {
            return this.DbContext.Movies
                .Include(m => m.Director)
                .FirstOrDefault(m => m.Id == movieId);
        }

        public ICollection<BorrowersMovies> GetMovieUnavailablePeriods(int productDetailsId)
        {
            return this.DbContext.BorrowedMovies
                    .Include(b => b.Borrower)
                    .Where(b => b.MovieId == productDetailsId && b.IsAvailable == false)
                    .ToArray();
        }

        public ICollection<BorrowersMovies> GetMovieHistory(int movieId)
        {
            return this.DbContext.BorrowedMovies
                .Include(m => m.Borrower)
                .Where(m => m.MovieId == movieId)
                .OrderBy(b => b.StartDate).ToArray();
        }

        public BorrowersMovies GetBorrowedMovie(int borrowedId)
        {
            return this.DbContext.BorrowedMovies.Where(b => b.Id == borrowedId && b.IsAvailable == false)
                .FirstOrDefault();
        }

        public void ReturnMovie(BorrowersMovies borrowedMovie)
        {
            borrowedMovie.EndDate = DateTime.Now;
            borrowedMovie.IsAvailable = true;

            this.DbContext.SaveChanges();
        }

        public bool CheckMovieBorrowingPeriods(int movieId, DateTime startDate)
        {
            return this.DbContext.BorrowedMovies.Any(b => b.MovieId == movieId && (b.EndDate > startDate || b.EndDate == null)
              && b.IsAvailable == false);
        }

        public bool CheckIfMovieIsborrowed(int productDetailsId)
        {
            return this.DbContext.BorrowedMovies.Any(b => b.MovieId == productDetailsId && (b.EndDate == null || b.EndDate > DateTime.Now));
        }
    }
}

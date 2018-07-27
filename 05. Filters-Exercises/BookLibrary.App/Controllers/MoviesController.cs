using BookLibrary.App.Filters;
using BookLibrary.App.Models.BindingModels;
using BookLibrary.App.Models.ViewModels;
using BookLibrary.Models;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace BookLibrary.App.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesService moviesService;
        private BorrowersService borrowersService;
        private DirectorsService directorsService;

        public MoviesController(DirectorsService directorsService, MoviesService moviesService, BorrowersService borrowersService)
        {
            this.directorsService = directorsService;
            this.moviesService = moviesService;
            this.borrowersService = borrowersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(MovieBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            Director director = this.directorsService.GetOrCreateDirector(model.Director);

            var movie = new Movie
            {
                Title = model.Title,
                Description = model.Description,
                PosterImage = model.ImageUrl,
                DirectorId = director.Id
            };

             this.moviesService.AddMovie(movie);

            return this.RedirectToAction("Details", new { id = movie.Id });
        }

        public IActionResult Details(int id)
        {

            var movie = this.moviesService.GetMovieWithDirector(id);

            if (movie == null)
            {
                return this.NotFound();
            }

            var movieDetails = new ProductDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Type = "Movie",
                Description = movie.Description,
                ImageUrl = movie.PosterImage,
                Creator = movie.Director.Name,
                IsBorrowed = moviesService.CheckIfMovieIsborrowed(movie.Id)
            };

            if (movieDetails.IsBorrowed)
            {
                var borrowingPeriods = moviesService.GetMovieUnavailablePeriods(movieDetails.Id);

                foreach (var borrowingPeriod in borrowingPeriods)
                {
                    movieDetails.UnavailablePeriods.Add(new UnavailableProductPeriodsViewModel
                    {
                        Id = borrowingPeriod.Id,
                        StartDate = borrowingPeriod.StartDate,
                        EndDate = borrowingPeriod.EndDate,
                        BorrowerName = borrowingPeriod.Borrower.Name
                    });
                }
            }

            return this.View(movieDetails);
        }

        [HttpPost]
        [ActionName("Details")]
        public IActionResult DetailsPost(int borrowedId)
        {
            var borrowedMovie = this.moviesService
               .GetBorrowedMovie(borrowedId);

            this.moviesService.ReturnMovie(borrowedMovie);

            return this.RedirectToAction("Details", new { id = borrowedMovie.MovieId});
        }

        [HttpGet]
        public IActionResult Borrow(int id)
        {
            if (id == 0)
            {
                return this.NotFound();
            }

            var movie = this.moviesService.FindById(id);

            if (movie == null)
            {
                return this.NotFound();
            }

            var borrowers = this.borrowersService.GetBorrowers();

            var model = new BorrowBindingModel
            {
                Borrowers = borrowers
                   .Select(b => new SelectListItem
                   {
                       Text = b.Name,
                       Value = b.Id.ToString()
                   })
                   .ToList()
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Borrow(BorrowBindingModel model)
        {
            var movieId = Convert.ToInt32(this.RouteData.Values["id"]);

            if (!this.ModelState.IsValid)
            {
                return this.Borrow(movieId);
            }

            var borrower = this.borrowersService.FindById(model.BorrowerId);

            var movie = this.moviesService.FindById(movieId);

            if (borrower == null || movie == null)
            {
                this.ViewData["Errors"] = "Invalid movie or borrower";
                return this.Borrow(movieId);

            }

            bool movieTaken = this.moviesService.CheckMovieBorrowingPeriods(movie.Id, model.StartDate);

            if (movieTaken)
            {
                this.ViewData["Errors"] = "This movie is already borrowed for this period";
                return this.Borrow(movie.Id);
            }
     
            var borrowedMovie = new BorrowersMovies
            {
                MovieId = movie.Id,
                BorrowerId = borrower.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            this.moviesService.BorrowMovie(borrowedMovie);

            return this.RedirectToAction("Details", new { id = movie.Id });
        }

        public IActionResult Status(int id)
        {
            var borrowingPeriods = this.moviesService.GetMovieHistory(id)
                .Select(b => new ProductHistoryViewModel
                {
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    BorrowerName = b.Borrower.Name
                })
                .ToList();

            return this.View(borrowingPeriods);
        }

        public IActionResult Excep()
        {
            throw new NotImplementedException();
        }
    }
}
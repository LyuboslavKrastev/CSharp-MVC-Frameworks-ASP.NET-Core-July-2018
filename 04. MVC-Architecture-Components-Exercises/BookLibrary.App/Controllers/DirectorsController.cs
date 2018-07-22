using BookLibrary.App.Models.ViewModels;
using BookLibrary.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookLibrary.App.Controllers
{
    public class DirectorsController : Controller
    {
        private DirectorsService directorsService;
        private MoviesService moviesService;

        public DirectorsController(DirectorsService directorsService, MoviesService moviesService)
        {
            this.directorsService = directorsService;
            this.moviesService = moviesService;
            this.DirectorDetails = new DirectorDetailsViewModel();
        }

        public DirectorDetailsViewModel DirectorDetails { get; set; }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return this.NotFound();
            }

            var director = this.directorsService.GetDirectorWithMovies(id);

            if (director == null)
            {
                return this.NotFound();
            }

            this.DirectorDetails.Name = director.Name;
            this.DirectorDetails.Movies = director.Movies
                .Select(movie => new ProductListingViewModel
                {
                    Id = movie.Id,
                    Title = movie.Title
                })
                .ToList();

            return this.View(DirectorDetails);
        }
    }
}
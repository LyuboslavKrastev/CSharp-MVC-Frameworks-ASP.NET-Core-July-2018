using FDMC.App.Models.BindingModels;
using FDMC.App.Models.ViewModels;
using FDMC.Data;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FDMC.App.Controllers
{
    public class CatsController : BaseController
    {
        public CatsController(CatAppContext context) 
            : base(context){}

        public IActionResult Details(int id)
        {
            var cat = this.Context.Cats.Find(id);

            var catDetailsModel = new CatDetailsViewModel()
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };

            if (cat == null)
            {
                return NotFound();
            }

            return this.View(catDetailsModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatCreatingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
               return this.View();
            }

            var cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };
            
            this.Context.Add(cat);
            this.Context.SaveChanges();

            return RedirectToAction("Details", new { id = cat.Id });
        }

        public IActionResult All()
        {
            var cats = this.Context.Cats
                .Select(c => new CatDetailsViewModel
                {
                    Name = c.Name,
                    Age = c.Age,
                    Breed = c.Breed,
                    ImageUrl = c.ImageUrl
                });


            return this.View(cats);
        }
    }
}
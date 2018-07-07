using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FDMC.App.Models.ViewModels;
using FDMC.Data;

namespace FDMC.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(CatAppContext context)
              : base(context) { }

        public IActionResult Index()
        {
            var cats = this.Context.Cats
                .Select(c => new CatListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            return View(cats);
        }
    }
}

using FDMC.Data;
using Microsoft.AspNetCore.Mvc;

namespace FDMC.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected BaseController(CatAppContext context)
        {
            this.Context = context;
        }

        protected CatAppContext Context { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Tasks.WebApi.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public RedirectResult Get()
        {
            return Redirect("/swagger");
        }
    }
}
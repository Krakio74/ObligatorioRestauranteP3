using Microsoft.AspNetCore.Mvc;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

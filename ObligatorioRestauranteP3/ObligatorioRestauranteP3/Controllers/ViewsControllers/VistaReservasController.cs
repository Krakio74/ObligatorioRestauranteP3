using Microsoft.AspNetCore.Mvc;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaReservasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaAdministracionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

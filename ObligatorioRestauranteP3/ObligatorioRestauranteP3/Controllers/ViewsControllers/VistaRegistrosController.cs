using Microsoft.AspNetCore.Mvc;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaRegistrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

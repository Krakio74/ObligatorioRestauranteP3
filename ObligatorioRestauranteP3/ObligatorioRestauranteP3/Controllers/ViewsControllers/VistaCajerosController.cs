using Microsoft.AspNetCore.Mvc;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaCajerosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

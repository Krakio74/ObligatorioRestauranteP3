using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;
using System.Diagnostics;

namespace ObligatorioP3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ObligatorioContext _context;

        public HomeController(ILogger<HomeController> logger, ObligatorioContext context)
        {
            _logger = logger;
            _context = context;
            //var parent = _context.Restaurantes
            //            .Include(p => p.Reservas)  // Eager loading children
            //            .FirstOrDefault(p => p.Id == RestauranteId);
        }


        public async Task<ViewResult> Index()
        {

            List<string> fotos = ImageProcessing.GetfullFileName("FotoRestaurantes", "");
            ViewBag.FotoRestaurantes = fotos;

            await _context.Restaurantes
                .Include(c => c.Reservas).FirstOrDefaultAsync(x => x.Id == 2);
            return View(await _context.Restaurantes.ToListAsync());// _context.Set<Restaurante>());//await _context.Restaurantes.ToListAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

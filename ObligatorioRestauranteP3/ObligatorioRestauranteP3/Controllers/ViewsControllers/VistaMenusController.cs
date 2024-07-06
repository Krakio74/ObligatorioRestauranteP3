using Microsoft.AspNetCore.Mvc;
using ObligatorioRestauranteP3.Models;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaMenusController : Controller
    {
        private readonly Op3v5Context _context;
        public VistaMenusController(Op3v5Context context)
        {
            _context = context;
        }
        public IActionResult Index(string categoria, string buscar, int? restauranteId)
        {
            List<string> fotos = ImageProcessing.GetfullFileName("FotoRestaurantes", "");
            ViewBag.FotoRestaurantes = fotos;

            List<string> fotosMenu = ImageProcessing.GetfullFileName("FotoMenu", "");
            ViewBag.FotoMenu = fotosMenu;

            var menus = _context.Menus.OrderBy(x => x.Categoria).ToList();
            var restaurantes = _context.Restaurantes.ToList();


            if (!string.IsNullOrEmpty(buscar))
            {

                restaurantes = restaurantes.Where(x =>
                    x.Nombre.Contains(buscar) ||
                    x.Direccion.Contains(buscar)
                ).ToList();
            }


            if (restauranteId != null)
            {
                menus = _context.Menus
             .Where(menu => menu.IdRestaurantes.Any(idRestaurante => idRestaurante.Id == restauranteId))
             .ToList();
            }

            ViewData["restauranteId"] = restauranteId;
            ViewData["Categoria"] = categoria;

            switch (categoria)
            {
                case "Entradas":
                    menus = menus.Where(x => x.Categoria == "Entradas").ToList();
                    break;
                case "Principales":
                    menus = menus.Where(x => x.Categoria == "Principales").ToList();
                    break;
                case "Postres":
                    menus = menus.Where(x => x.Categoria == "Postres").ToList();
                    break;
                case "Bebidas":
                    menus = menus.Where(x => x.Categoria == "Bebidas").ToList();
                    break;
            }


            var vistaMenu = new VistaMenu
            {
                Restaurantes = restaurantes,
                Menus = menus
            };

            return View(vistaMenu);
        }
    }
}

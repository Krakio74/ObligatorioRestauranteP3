using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ObligatorioRestauranteP3.Models;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaReseñasController : Controller
    {
        private readonly Op3v5Context _context;
        //Login userData = LoginData.userData;
        public VistaReseñasController(Op3v5Context context)
        {
            _context = context;

        }



        public IActionResult Index(string orden, int? numpag, string filtroActual)
        {


            List<string> fotos = ImageProcessing.GetfullFileName("FotoUsuario", "");
            ViewBag.FotoRestaurantes = fotos;

            var lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.IdNavigation)
                           .ToList();

            int reseñas = _context.Reseñas.Count();
            int puntaje = _context.Reseñas.Sum(x => x.Puntaje);

            double promedio = reseñas > 0 ? (double)puntaje / reseñas : 0;

            ViewData["TotalReseñas"] = reseñas;
            ViewData["Promedio"] = promedio;




            ViewData["Orden"] = orden;

            switch (orden)
            {
                case "OrdenarFechaAsc":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.IdNavigation)
                           .OrderBy(x => x.Fecha)
                           .ToList();
                    break;
                case "OrdenarFechaDes":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.IdNavigation)
                           .OrderByDescending(x => x.Fecha)
                           .ToList();
                    break;
                case "OrdenarPuntajeAsc":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.IdNavigation)
                           .OrderBy(x => x.Puntaje)
                           .ToList();
                    break;
                case "OrdenarPuntajeDes":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.IdNavigation)
                           .OrderByDescending(x => x.Puntaje)
                           .ToList();
                    break;
            }
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> PublicarReseña(string comentario, int puntaje)
        {

            //if (userData == null)
            //{
            //    return BadRequest("asdasd");
            //}
            var reseña = new Reseña
            {
                ClienteId = 1,
                ReservaId = 15,
                Comentario = comentario,
                Puntaje = puntaje,
                Fecha = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Reseñas.Add(reseña);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
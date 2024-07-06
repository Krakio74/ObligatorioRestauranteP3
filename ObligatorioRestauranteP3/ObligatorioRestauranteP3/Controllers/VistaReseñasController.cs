using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;
using System.Linq;
using ObligatorioP3.Data;

namespace ObligatorioP3.Controllers
{
    public class VistaReseñasController : Controller
    {
        private readonly ObligatorioContext _context;
        Login userData = LoginData.userData;
        public VistaReseñasController(ObligatorioContext context)
        {
            _context = context;

        }

  

        public IActionResult Index(string orden, int? numpag, string filtroActual)
        {

            var lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.Usuario)
                           .ToList();

            ViewData["Orden"] = orden;

            switch (orden)
            {
                case "OrdenarFechaAsc":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.Usuario)
                           .OrderBy(x => x.Fecha)
                           .ToList();
                    break;
                case "OrdenarFechaDes":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.Usuario)
                           .OrderByDescending(x => x.Fecha)
                           .ToList();
                    break;
                case "OrdenarPuntajeAsc":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.Usuario)
                           .OrderBy(x => x.Puntaje)
                           .ToList();
                    break;
                case "OrdenarPuntajeDes":
                    lista = _context.Reseñas
                           .Include(r => r.Cliente)
                           .ThenInclude(c => c.Usuario)
                           .OrderByDescending(x => x.Puntaje)
                           .ToList();
                    break;
            }
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> PublicarReseña(string comentario, int puntaje)
        {

            if (userData == null)
            {
                return BadRequest("asdasd");
            }
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
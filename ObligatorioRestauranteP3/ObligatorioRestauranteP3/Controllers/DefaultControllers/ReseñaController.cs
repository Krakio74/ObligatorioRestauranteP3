using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioRestauranteP3.Models;

namespace ObligatorioRestauranteP3.Controllers.DefaultControllers
{
    public class ReseñaController : Controller
    {
        private readonly Op3v5Context _context;

        public ReseñaController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Reseña
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var reseñas = from Reseña in _context.Reseñas select Reseña;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            if (!string.IsNullOrEmpty(busqueda))
            {
                if (int.TryParse(busqueda, out int puntaje))
                {
                    reseñas = reseñas.Where(x => x.Puntaje == puntaje);
                }
            }

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;
            ViewData["cantidadregistros"] = cantidadregistros;

            switch (orden)
            {
                case "PuntajeAsc":
                    reseñas = reseñas.OrderBy(x => x.Puntaje);
                    break;
                case "PuntajeDes":
                    reseñas = reseñas.OrderByDescending(x => x.Puntaje);
                    break;
                case "FechaAsc":
                    reseñas = reseñas.OrderBy(x => x.Fecha);
                    break;
                case "FechaDes":
                    reseñas = reseñas.OrderByDescending(x => x.Fecha);
                    break;
            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }

            var paginacion = await Paginacion<Reseña>.crearPaginacion(reseñas.AsNoTracking(), numpag ?? 1, cantidadregistros);
            return View(paginacion);
        }

        // GET: Reseña/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas
                .Include(r => r.Cliente)
                .Include(r => r.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reseña == null)
            {
                return NotFound();
            }

            return View(reseña);
        }

        // GET: Reseña/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id");
            return View();
        }

        // POST: Reseña/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservaId,Fecha,Comentario,Puntaje,ClienteId")] Reseña reseña)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reseña);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reseña.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", reseña.ReservaId);
            return View(reseña);
        }

        // GET: Reseña/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reseña.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", reseña.ReservaId);
            return View(reseña);
        }

        // POST: Reseña/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservaId,Fecha,Comentario,Puntaje,ClienteId")] Reseña reseña)
        {
            if (id != reseña.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reseña);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReseñaExists(reseña.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reseña.ClienteId);
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", reseña.ReservaId);
            return View(reseña);
        }

        // GET: Reseña/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reseña = await _context.Reseñas
                .Include(r => r.Cliente)
                .Include(r => r.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reseña == null)
            {
                return NotFound();
            }

            return View(reseña);
        }

        // POST: Reseña/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña != null)
            {
                _context.Reseñas.Remove(reseña);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReseñaExists(int id)
        {
            return _context.Reseñas.Any(e => e.Id == id);
        }
    }
}

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
    public class ReservasController : Controller
    {
        private readonly Op3v5Context _context;

        public ReservasController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var reservas = from Reserva in _context.Reservas select Reserva;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            if (!string.IsNullOrEmpty(busqueda))
            {
                if (int.TryParse(busqueda, out int restaurante))
                {
                    reservas = reservas.Where(x => x.RestauranteId == restaurante);
                }
            }

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;
            ViewData["cantidadregistros"] = cantidadregistros;

            switch (orden)
            {
                case "Estado":
                    reservas = reservas.OrderBy(x => x.Estado);
                    break;
                case "FechaAsc":
                    reservas = reservas.OrderBy(x => x.Fecha);
                    break;
                case "FechaDes":
                    reservas = reservas.OrderByDescending(x => x.Fecha);
                    break;
            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }

            var paginacion = await Paginacion<Reserva>.crearPaginacion(reservas.AsNoTracking(), numpag ?? 1, cantidadregistros);
            return View(paginacion);
        }


        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Mesa)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["Clienteid"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Id");
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Clienteid,RestauranteId,MesaId,Fecha,Estado")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clienteid"] = new SelectList(_context.Clientes, "Id", "Id", reserva.Clienteid);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Id", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", reserva.RestauranteId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["Clienteid"] = new SelectList(_context.Clientes, "Id", "Id", reserva.Clienteid);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Id", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", reserva.RestauranteId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Clienteid,RestauranteId,MesaId,Fecha,Estado")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["Clienteid"] = new SelectList(_context.Clientes, "Id", "Id", reserva.Clienteid);
            ViewData["MesaId"] = new SelectList(_context.Mesas, "Id", "Id", reserva.MesaId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", reserva.RestauranteId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Mesa)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}

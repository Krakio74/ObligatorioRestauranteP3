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
    public class OrdensController : Controller
    {
        private readonly Op3v5Context _context;

        public OrdensController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var ordenes = from Orden in _context.Ordens select Orden;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            //if (!String.IsNullOrEmpty(busqueda))
            //{
            //    ordenesdetalle = ordenesdetalle.Where(x =>
            //         x.MenuId.Contains(busqueda) ||
            //         x.Categoria.Contains(busqueda)
            //     );
            //}

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;
            ViewData["cantidadregistros"] = cantidadregistros;

            switch (orden)
            {
                case "TotalAsc":
                    ordenes = ordenes.OrderBy(x => x.Total);
                    break;
                case "TotalDes":
                    ordenes = ordenes.OrderByDescending(x => x.Total);
                    break;
                case "DescCliente":
                    ordenes = ordenes.OrderBy(x => x.DescCliente);
                    break;
                case "DescTemperatura":
                    ordenes = ordenes.OrderBy(x => x.DescTemperatura);
                    break;
                case "DescClima":
                    ordenes = ordenes.OrderBy(x => x.DescClima);
                    break;

            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }

            var paginacion = await Paginacion<Orden>.crearPaginacion(ordenes.AsNoTracking(), numpag ?? 1, cantidadregistros);
            return View(paginacion);
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordens
                .Include(o => o.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public IActionResult Create()
        {
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id");
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservaId,Total,DescCliente,DescTemperatura,DescClima")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", orden.ReservaId);
            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordens.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", orden.ReservaId);
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservaId,Total,DescCliente,DescTemperatura,DescClima")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
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
            ViewData["ReservaId"] = new SelectList(_context.Reservas, "Id", "Id", orden.ReservaId);
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordens
                .Include(o => o.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Ordens.FindAsync(id);
            if (orden != null)
            {
                _context.Ordens.Remove(orden);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return _context.Ordens.Any(e => e.Id == id);
        }
    }
}

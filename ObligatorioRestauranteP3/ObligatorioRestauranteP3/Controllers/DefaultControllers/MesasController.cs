using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioRestauranteP3.Models;

namespace ObligatorioRestauranteP3.Controllers.DefaultControllers
{
    public class MesasController : Controller
    {
        private readonly Op3v5Context _context;

        public MesasController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Mesas
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var mesas = from Mesa in _context.Mesas select Mesa;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            if (!string.IsNullOrEmpty(busqueda))
            {
                if (int.TryParse(busqueda, out int capacidad))
                {
                    mesas = mesas.Where(x =>
                    x.Capacidad == capacidad
                );
                }

            }

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;
            ViewData["cantidadregistros"] = cantidadregistros;

            switch (orden)
            {
                case "CapacidadAsc":
                    mesas = mesas.OrderBy(x => x.Capacidad);
                    break;
                case "CapacidadDes":
                    mesas = mesas.OrderByDescending(x => x.Capacidad);
                    break;
                case "RestauranteDes":
                    mesas = mesas.OrderByDescending(x => x.Restauranteid);
                    break;
                case "RestauranteAsc":
                    mesas = mesas.OrderBy(x => x.Restauranteid);
                    break;
            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }

            var paginacion = await Paginacion<Mesa>.crearPaginacion(mesas.AsNoTracking(), numpag ?? 1, cantidadregistros);
            return View(paginacion);
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            ViewData["Restauranteid"] = new SelectList(_context.Restaurantes, "Id", "Id");
            return View();
        }

        // POST: Mesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroMesa,Capacidad,Restauranteid")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Restauranteid"] = new SelectList(_context.Restaurantes, "Id", "Id", mesa.Restauranteid);
            return View(mesa);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            ViewData["Restauranteid"] = new SelectList(_context.Restaurantes, "Id", "Id", mesa.Restauranteid);
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroMesa,Capacidad,Restauranteid")] Mesa mesa)
        {
            if (id != mesa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesaExists(mesa.Id))
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
            ViewData["Restauranteid"] = new SelectList(_context.Restaurantes, "Id", "Id", mesa.Restauranteid);
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas
                .Include(m => m.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa != null)
            {
                _context.Mesas.Remove(mesa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesaExists(int id)
        {
            return _context.Mesas.Any(e => e.Id == id);
        }
    }
}

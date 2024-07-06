using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public class OrdenDetallesController : Controller
    {
        private readonly ObligatorioContext _context;

        public OrdenDetallesController(ObligatorioContext context)
        {
            _context = context;
        }

        // GET: OrdenDetalles
        public async Task<IActionResult> Index()
        {
            var obligatorioContext = _context.OrdenDetalles.Include(o => o.Menu).Include(o => o.Orden).Include(o => o.Restaurante);
            return View(await obligatorioContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult CreateOrden([FromBody] Orden nuevaOrden)
        {
            if (ModelState.IsValid)
            {
                // Crear y guardar la orden en la base de datos
                _context.Ordens.Add(nuevaOrden);
                _context.SaveChanges();

                return Json(new { success = true, message = "Orden creada correctamente" });
            }

            return Json(new { success = false, message = "Error al crear la orden" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdenDetalles([FromBody] OrdenDetallesController[] detalles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var detalle in detalles)
                    {
                        _context.Add(detalle);
                    }
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Detalles de orden creados correctamente" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Error al crear detalles de orden: {ex.Message}" });
                }
            }

            return BadRequest(new { message = "Datos de entrada no válidos" });
        }

        // GET: OrdenDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles
                .Include(o => o.Menu)
                .Include(o => o.Orden)
                .Include(o => o.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }

            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id");
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id");
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id");
            return View();
        }

        // POST: OrdenDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrdenId,MenuId,RestauranteId,Cantidad")] OrdenDetalle ordenDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", ordenDetalle.MenuId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenDetalle.OrdenId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", ordenDetalle.RestauranteId);
            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", ordenDetalle.MenuId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenDetalle.OrdenId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", ordenDetalle.RestauranteId);
            return View(ordenDetalle);
        }

        // POST: OrdenDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrdenId,MenuId,RestauranteId,Cantidad")] OrdenDetalle ordenDetalle)
        {
            if (id != ordenDetalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDetalleExists(ordenDetalle.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", ordenDetalle.MenuId);
            ViewData["OrdenId"] = new SelectList(_context.Ordens, "Id", "Id", ordenDetalle.OrdenId);
            ViewData["RestauranteId"] = new SelectList(_context.Restaurantes, "Id", "Id", ordenDetalle.RestauranteId);
            return View(ordenDetalle);
        }

        // GET: OrdenDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDetalle = await _context.OrdenDetalles
                .Include(o => o.Menu)
                .Include(o => o.Orden)
                .Include(o => o.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDetalle == null)
            {
                return NotFound();
            }

            return View(ordenDetalle);
        }

        // POST: OrdenDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);
            if (ordenDetalle != null)
            {
                _context.OrdenDetalles.Remove(ordenDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenDetalleExists(int id)
        {
            return _context.OrdenDetalles.Any(e => e.Id == id);
        }
    }
}

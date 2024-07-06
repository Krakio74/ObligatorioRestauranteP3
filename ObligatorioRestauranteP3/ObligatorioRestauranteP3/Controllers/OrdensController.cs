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
    public class OrdensController : Controller
    {
        private readonly ObligatorioContext _context;

        public OrdensController(ObligatorioContext context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            var obligatorioContext = _context.Ordens.Include(o => o.Reserva);
            return View(await obligatorioContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,ReservaId,Total,Descuento")] Orden orden)
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

        [HttpPost]
        public IActionResult CreateOrden([FromBody] OrdenRequest request)
        {
            if (request == null || request.Detalles == null || !request.Detalles.Any())
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Crear la orden
                var orden = new Orden
                {
                    ReservaId = request.ReservaId,
                    Total = request.Total,
                    Descuento = request.Descuento,
                    // Otros campos necesarios
                };

                // Agregar la orden a la base de datos
                _context.Ordens.Add(orden);
                _context.SaveChanges();

                // Crear los detalles de la orden
                foreach (var detalle in request.Detalles)
                {
                    var ordenDetalle = new OrdenDetalle
                    {
                        OrdenId = orden.Id, // ID de la orden recién creada
                        MenuId = detalle.MenuId,
                        Cantidad = detalle.Cantidad,
                        // Otros campos necesarios
                    };

                    // Agregar cada detalle a la base de datos
                    _context.OrdenDetalles.Add(ordenDetalle);
                }

                // Guardar todos los cambios
                _context.SaveChanges();

                return Ok(new { Message = "Orden creada correctamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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

        public IActionResult Edit(int id)
        {
            var orden = _context.Ordens.Find(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        [HttpPost]
        public IActionResult Edit(Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Update(orden);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservaId,Total,Descuento")] Orden orden)
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

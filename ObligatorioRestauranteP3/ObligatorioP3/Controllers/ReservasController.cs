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
    public class ReservasController : Controller
    {
        private readonly ObligatorioContext _context;

        public ReservasController(ObligatorioContext context)
        {
            _context = context;
            
        }
        //[HttpGet("Reservas/Reservar/{ResId}")]
        public async Task<IActionResult> Reservar(int ResId)
        {
            if (Data.LoginData.userData == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //ViewData["ResId"] = ResId;
            ViewBag.ClientId = Data.LoginData.userData.Usr.Id;

            var reservas = await _context.Reservas.Include(r => r.Restaurante).FirstOrDefaultAsync(r => r.RestauranteId == ResId);/*Include(m => m.Mesa).FirstOrDefaultAsync(r => r.RestauranteId == ResId);*/
            return View(reservas);
        }
        public async Task<IActionResult> Reserva()
        {

            return PartialView("~/Views/Reservas/_Reservar.cshtml");
        }
        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var obligatorioContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.Mesa).Include(r => r.Restaurante);
            return View(await obligatorioContext.ToListAsync());
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

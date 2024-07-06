using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Data;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ObligatorioContext _context;
        private readonly Login usrData = LoginData.userData;


        public RestaurantesController(ObligatorioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (usrData == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (usrData.Usr.Empleado.Rango == null || (usrData.Usr.Empleado.Rango != "Administrador" && usrData.Usr.Empleado.Rango != "Administrativo"))
            {
                return RedirectToAction("UserPanel", "Usuarios");
            }
            return View(await _context.Restaurantes.ToListAsync());
        }

        // GET: Restaurantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // GET: Restaurantes/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Telefono,HoraApertura,HoraCierre")] Restaurante restaurante, IFormFile FotoRestaurante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                int ResId = _context.Restaurantes.FirstOrDefault(x => x.Nombre == restaurante.Nombre).Id;
                await ImageProcessing.SavePicture("FotoRestaurantes", ResId, FotoRestaurante);
                var fileExt = Path.GetExtension(FotoRestaurante.FileName);
                FotoRestaurante fotoRestaurante = new FotoRestaurante {
                RestauranteId = ResId,
                Foto = ResId + fileExt,
                };
                _context.Add(fotoRestaurante);
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Restaurantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            return View(restaurante);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,HoraApertura,HoraCierre")] Restaurante restaurante)
        {
            if (id != restaurante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestauranteExists(restaurante.Id))
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
            return View(restaurante);
        }

        // GET: Restaurantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurante == null)
            {
                return NotFound();
            }

            return View(restaurante);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante != null)
            {
                _context.Restaurantes.Remove(restaurante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestauranteExists(int id)
        {
            return _context.Restaurantes.Any(e => e.Id == id);
        }
    }
}

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
    public class MenusController : Controller
    {
        private readonly Op3v5Context _context;

        public MenusController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var menus = from menu in _context.Menus select menu;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            if (!string.IsNullOrEmpty(busqueda))
            {
                menus = menus.Where(x =>
                     x.NombrePlato.Contains(busqueda) ||
                     x.Categoria.Contains(busqueda)
                 );
            }

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;
            ViewData["cantidadregistros"] = cantidadregistros;

            switch (orden)
            {
                case "NombreAsc":
                    menus = menus.OrderBy(x => x.NombrePlato);
                    break;
                case "PrecioAsc":
                    menus = menus.OrderBy(x => x.Precio);
                    break;
                case "PrecioDes":
                    menus = menus.OrderByDescending(x => x.Precio);
                    break;
                case "Categoria":
                    menus = menus.OrderBy(x => x.Categoria);
                    break;
            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }

            var paginacion = await Paginacion<Menu>.crearPaginacion(menus.AsNoTracking(), numpag ?? 1, cantidadregistros);
            return View(paginacion);
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombrePlato,Descripcion,Precio,Categoria")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombrePlato,Descripcion,Precio,Categoria")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}

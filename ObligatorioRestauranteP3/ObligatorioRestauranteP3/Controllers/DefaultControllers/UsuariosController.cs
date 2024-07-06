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
    public class UsuariosController : Controller
    {
        private readonly Op3v5Context _context;

        public UsuariosController(Op3v5Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(string busqueda, string orden, int? numpag, string filtro, int cantidadregistros)
        {
            var usuarios = from usuario in _context.Usuarios select usuario;


            if (busqueda != null)
                numpag = 1;
            else
                busqueda = filtro;

            if (!string.IsNullOrEmpty(busqueda))
            {
                usuarios = usuarios.Where(x =>
                     x.Nombre.Contains(busqueda) ||
                     x.Apellido.Contains(busqueda) ||
                     x.Email.Contains(busqueda)
                 );
            }

            ViewData["orden"] = orden;
            ViewData["filtro"] = busqueda;

            //ViewData["busqueda"] = String.IsNullOrEmpty(orden) ? "NombreDescendente" : "";
            //ViewData["FiltroFecha"] = orden == "FechaAscendente" ? "FechaDescendente" : "FechaAscendente";


            switch (orden)
            {
                case "NombreAsc":
                    usuarios = usuarios.OrderBy(x => x.Nombre);
                    break;
                case "NombreDes":
                    usuarios = usuarios.OrderByDescending(x => x.Nombre);
                    break;
                case "EmailAsc":
                    usuarios = usuarios.OrderBy(x => x.Email);
                    break;
                case "EmailDes":
                    usuarios = usuarios.OrderByDescending(x => x.Email);
                    break;
                case "FechaAsc":
                    usuarios = usuarios.OrderBy(x => x.FechaCreacion);
                    break;
                case "FechaDes":
                    usuarios = usuarios.OrderByDescending(x => x.FechaCreacion);
                    break;
            }

            if (cantidadregistros == 0)
            {
                cantidadregistros = 12;
            }


            return View(await Paginacion<Usuario>.crearPaginacion(usuarios.AsNoTracking(), numpag ?? 1, cantidadregistros));
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Telefono,Contraseña")] Usuario usuario)
        {
            usuario.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Telefono,Contraseña,FechaCreacion")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}

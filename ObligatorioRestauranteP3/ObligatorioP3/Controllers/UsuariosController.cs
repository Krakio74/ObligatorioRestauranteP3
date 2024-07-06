﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Versioning;
using ObligatorioP3.Data;
using ObligatorioP3.Models;
using System.IO;


namespace ObligatorioP3.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ObligatorioContext _context;
        private readonly Login usrData = LoginData.userData;
        public UsuariosController(ObligatorioContext context)
        {
            _context = context;
            
        }
        
        public async Task<IActionResult> Index(string busqueda,string orden, int? numpag, string filtro)
        {
            var usuarios = from usuario in _context.Usuarios select usuario;

            if(usrData == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if(usrData.Usr.Empleado.Rango == null || (usrData.Usr.Empleado.Rango != "Administrador" && usrData.Usr.Empleado.Rango != "Administrativo"))
            {

                return RedirectToAction("UserPanel");
            }
            if (!String.IsNullOrEmpty(busqueda))
            {
                numpag = 1;

                usuarios = usuarios.Where(x =>
                    x.Nombre.Contains(busqueda) ||
                    x.Apellido.Contains(busqueda) ||
                    x.Email.Contains(busqueda)
                );
            }

            ViewData["OrdenActual"] = orden;
            ViewData["FiltroActual"] = busqueda;

            ViewData["FiltroNombre"] = String.IsNullOrEmpty(orden) ? "NombreDescendente" : "";
            ViewData["FiltroFecha"] = orden == "FechaAscendente" ? "FechaDescendente" : "FechaAscendente";


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
            }

            int cantidadregistros = 12;
            return View(await Paginacion<Usuario>.crearPaginacion(usuarios.AsNoTracking(),numpag?? 1,cantidadregistros));
        }
        public IActionResult UserPanel()
        {
            var filePath = "wwwroot/FotoUsuarios";
            var files = Directory.GetFiles(filePath);
            var ProfilePicture = Directory.GetFiles(filePath, $"{usrData.ID}.*").FirstOrDefault();
            var name = ImageProcessing.GetfullFileName("FotoUsuarios", usrData.ID.ToString());
            ViewBag.ProfilePicture = name[0];
            return View();
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
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Email,Telefono,Contraseña")] Usuario usuario, IFormFile FotoUsuario)
        {
            if (ModelState.IsValid)
            {
                if (EmailExist(usuario.Email))
                {
                    return BadRequest("USER EXIST");
                }
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                int userId = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email).Id;
                await ImageProcessing.SavePicture("FotoUsuarios", userId, FotoUsuario);
                
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }
        private bool EmailExist(string Email){
            var emailCheck = _context.Usuarios.FirstOrDefault(x => x.Email == Email);
            if (emailCheck == null || emailCheck.Email.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
        public async Task<IActionResult> SearchEmp(int? id)
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
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> CreateEmp([Bind("Id,Rango,Estado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Index);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Telefono,Contraseña")] Usuario usuario)
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

 
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return null;
        }
        public IActionResult Register()
        {
            return View("Create"); 
        }

        
        

    }
}

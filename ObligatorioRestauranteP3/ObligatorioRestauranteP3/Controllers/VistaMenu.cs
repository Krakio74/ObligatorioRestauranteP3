using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;
using System.Linq;
using ObligatorioP3.Data;

namespace ObligatorioP3.Controllers
{
    public class VistaMenu : Controller
    {
        private readonly ObligatorioContext _context;

        public VistaMenu(ObligatorioContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            MultiModel model = new MultiModel();
            model.Mesas = GetMesas();
            model.Restaurantes = GetRestaurantes();
            model.Reservas = GetReservas();
            model.Menus = GetMenus();
            model.Clientes = GetClientes();
            return View(model);
        }

        private List<Mesa> GetMesas()
        {
            return _context.Mesas.ToList();
        }

        private List<Restaurante> GetRestaurantes()
        {
            return _context.Restaurantes.ToList();
        }

        private List<Reserva> GetReservas()
        {
            return _context.Reservas.ToList();
        }

        private List<Cliente> GetClientes()
        {
            return _context.Clientes.ToList();
        }

        private List<Menu> GetMenus()
        {
            return _context.Menus.ToList();
        }

        [HttpPost]
        public IActionResult CambiarEstadoMesa(int mesaId, string nuevoEstado)
        {
            var reservass = _context.Reservas.FirstOrDefault(m => m.MesaId == mesaId);
            if (reservass != null)
            {
                reservass.Estado = nuevoEstado;
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }       
}


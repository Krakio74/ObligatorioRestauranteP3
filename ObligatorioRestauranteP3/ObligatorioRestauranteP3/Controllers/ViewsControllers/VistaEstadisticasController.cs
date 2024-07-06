using Microsoft.AspNetCore.Mvc;
using ObligatorioRestauranteP3.Models;

namespace ObligatorioRestauranteP3.Controllers.ViewsControllers
{
    public class VistaEstadisticasController : Controller
    {

        private readonly Op3v5Context _context;
        //Login userData = LoginData.userData;
        public VistaEstadisticasController(Op3v5Context context)
        {
            _context = context;

        }
        public IActionResult Index()
        {

            int Usuarios = _context.Usuarios.Count();
            int Clientes = _context.Clientes.Count();
            int Empleados = _context.Empleados.Count();

            ViewData["UsuariosTotales"] = Usuarios;
            ViewData["ClientesTotales"] = Clientes;
            ViewData["EmpleadosTotales"] = Empleados;


            int ClientesVip = _context.Clientes.Where(x => x.TipoCliente == "Vip").Count();
            int ClientesFrecuentes = _context.Clientes.Where(x => x.TipoCliente == "Frecuente").Count();
            int ClientesNuevos = _context.Clientes.Where(x => x.TipoCliente == "Nuevo").Count();


            ViewData["ClientesVip"] = ClientesVip;
            ViewData["ClientesFrecuentes"] = ClientesFrecuentes;
            ViewData["ClientesNuevos"] = ClientesNuevos;



            int Reseñas = _context.Reseñas.Count();
            int Puntaje = _context.Reseñas.Sum(x => x.Puntaje);

            double Promedio = Reseñas > 0 ? (double)Puntaje / Reseñas : 0;

            ViewData["TotalReseñas"] = Reseñas;
            ViewData["Promedio"] = Promedio;



            int totalPlatos = _context.Menus.Count();
            int totalEntradas = _context.Menus.Where(x => x.Categoria == "Entradas").Count();
            int totalPrincipales = _context.Menus.Where(x => x.Categoria == "Principales").Count();
            int totalPostres = _context.Menus.Where(x => x.Categoria == "Postres").Count();
            int totalBebidas = _context.Menus.Where(x => x.Categoria == "Bebidas").Count();

            ViewData["totalPlatos"] = totalPlatos;
            ViewData["totalEntradas"] = totalEntradas;
            ViewData["totalPrincipales"] = totalPrincipales;
            ViewData["totalPostres"] = totalPostres;
            ViewData["totalBebidas"] = totalBebidas;



            return View();



        }
    }
}

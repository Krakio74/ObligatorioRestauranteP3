using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Data;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public class LoginController : Controller
    {
        private readonly ObligatorioContext _context;
        Login userData;
        public LoginController(ObligatorioContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginVerifcation(Login login)
        {
            var user = _context.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Contraseña == login.Password);
            if (user != null)
            {
                userData = new Login
                {
                    ID = user.Id,
                    Email = login.Email,
                    Password = login.Password,
                    Usr = new Usuario
                    {
                        Id = user.Id,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        Contraseña = user.Contraseña,
                        Empleado = null,
                    },
                    Activo = true

                };
                var isEmpleado = _context.Empleados.FirstOrDefault(x => x.Id == user.Id);
                if (isEmpleado != null)
                {
                    user.Empleado = new Empleado(isEmpleado.Id, isEmpleado.Rango, isEmpleado.Estado);
                    userData.Usr.Empleado = new Empleado(isEmpleado.Id, isEmpleado.Rango, isEmpleado.Estado);
                }
                LoginData.userData = userData;
                var cliente = _context.Clientes.FirstOrDefault(x => x.Id == user.Id);


                return RedirectToAction("Index", "Usuarios");//Ok(userData);
            }
            

            else
            {
                return NoContent();
            }


        }
    }



}

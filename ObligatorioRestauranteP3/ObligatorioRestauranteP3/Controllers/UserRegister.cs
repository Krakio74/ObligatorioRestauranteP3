using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegister : ControllerBase
    {
        private readonly ObligatorioContext ObContext;
        public UserRegister(ObligatorioContext ObContext)
        {
            this.ObContext = ObContext;
        }
        [HttpPost]
        [Route("Registro")]
        public IActionResult Registro(Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var objUsuario = ObContext.Usuarios.FirstOrDefault(x => x.Email == registro.Email);
            if (objUsuario == null)
            {
                ObContext.Usuarios.Add(new Usuario
                {
                    Email = registro.Email,
                    Contraseña = registro.Contraseña,
                    Nombre = registro.Nombre,
                    Apellido = registro.Apellido,
                    Telefono = registro.Telefono,
                });
                ObContext.SaveChanges();
                return Ok("Usuario registrado correctamente");
            }
            else
            {
                return BadRequest("El usuario ya existe");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            var user = ObContext.Usuarios.FirstOrDefault(x => x.Email == login.Email && x.Contraseña == login.Password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
            

        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ObligatorioP3.Models
{
    public class Login
    {
        public  int ID { get; set; }

        [Required(ErrorMessage = "Ingrese correo electronico!")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Ingrese su contrasena!")]
        public string Password { get; set; }
        public Usuario Usr { get; set; }
        public Cliente Cliente { get; set; }
        public bool Activo { get; set; }
    }
}

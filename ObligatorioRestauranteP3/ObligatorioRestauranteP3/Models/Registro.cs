namespace ObligatorioP3.Models
{
    public class Registro
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? Telefono { get; set; }

        public string Contraseña { get; set; } = null!;
    }
}

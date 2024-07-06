namespace ObligatorioRestauranteP3.Models
{
    public class VistaPerfil
    {
        public Usuario usuario { get; set; }
        public List<Reseña> reseñas { get; set; } = new List<Reseña>();
    }
}

namespace ObligatorioP3.Models
{
    public class MultiModel
    {
        public List<Mesa> Mesas { get; set; } = new List<Mesa>();
        public List<Restaurante> Restaurantes { get; set; } = new List<Restaurante>();
        public List<Reserva> Reservas { get; set; } = new List<Reserva>();
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Orden> Ordens { get; set; } = new List<Orden>();
        public List<OrdenDetalle> DetalleOrden { get; set; } = new List<OrdenDetalle>();

        //public int ReservaId { get; set; }
        //public int MesaId { get; set; }
        //public string ClienteApellido { get; set; }
    }
}

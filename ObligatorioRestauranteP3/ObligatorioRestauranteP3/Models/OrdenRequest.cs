namespace ObligatorioP3.Models
{
    public class OrdenRequest
    {
        public int? ReservaId { get; set; }
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public List<OrdenDetalleRequest> Detalles { get; set; }
    }

    public class OrdenDetalleRequest
    {
        public int MenuId { get; set; }
        public int RestauranteId { get; set; }
        public int Cantidad { get; set; }
    }

}

using Microsoft.EntityFrameworkCore;

namespace ObligatorioRestauranteP3.Models
{
    public class Paginacion<T> : List<T>
    {
        public int PaginaInicio { get; private set; }
        public int PaginasTotales { get; private set; }
        public Paginacion(List<T> items, int contador, int paginaInicio, int cantidadRegistros)
        {
            PaginaInicio = paginaInicio;
            PaginasTotales = (int)Math.Ceiling((double)contador / cantidadRegistros);

            this.AddRange(items);
        }
        public bool PaginasAnteriores => PaginaInicio > 1;
        public bool PaginasPosteriores => PaginaInicio < PaginasTotales;
        public static async Task<Paginacion<T>> crearPaginacion(IQueryable<T> fuente, int paginaInicio, int cantidadRegistros)
        {
            var contador = await fuente.CountAsync();
            var items = await fuente.Skip((paginaInicio - 1) * cantidadRegistros).Take(cantidadRegistros).ToListAsync();
            return new Paginacion<T>(items, contador, paginaInicio, cantidadRegistros);
        }

    }
}

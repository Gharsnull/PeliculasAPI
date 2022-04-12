namespace PeliculasAPI.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;
        private int cantidadRegistrosPorPagina = 10;

        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistrosPorPagina;
            set
            {
                cantidadRegistrosPorPagina = (value > cantidadMaximaRegistrosPorPagina) ? cantidadMaximaRegistrosPorPagina : value;
            }
        }
    }
}

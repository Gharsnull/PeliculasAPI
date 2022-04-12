namespace PeliculasAPI.DTOs
{
    public class ActorPeliculaDetalleDTO: PeliculaDTO
    {
        public int ActorId { get; set; }
        public string Personaje { get; set; }
        public string NombrePersona { get; set; }
    }
}

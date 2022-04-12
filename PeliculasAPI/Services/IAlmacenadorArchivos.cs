namespace PeliculasAPI.Services
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivo(byte[] data, string extension, string contenedor, string contentType);

        Task<string> EditarArchivo(byte[] data, string extension, string contenedor, string ruta, string contentType);

        Task BorrarArchivo(string ruta, string contenedor);
    }
}

using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.DTOs
{
    public class SalaDeCineCercanoFiltroDTO
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }

        private int distanciaEnkms = 10;
        private int distanciaMaximaKms = 50;

        public int DistanciaEnkms
        {
            get { return distanciaEnkms; }
            set
            {
                distanciaEnkms = (value > distanciaMaximaKms) ? distanciaMaximaKms : value;
            }
        }
    }
}

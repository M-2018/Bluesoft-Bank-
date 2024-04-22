using BluesoftBank.Models;

namespace BluesoftBank.Dto
{
    public class ClienteConsignacionDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dirección { get; set; }
        public string Telefono { get; set; }
        public decimal MontoConsignacion { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
    }
}

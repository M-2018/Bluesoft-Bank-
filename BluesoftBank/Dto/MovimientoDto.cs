using BluesoftBank.Models;

namespace BluesoftBank.Dto
{
    public class MovimientoDto
    {
        public long IdCuenta { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public decimal Valor { get; set; }
        public string CiudadMovimiento { get; set; }
    }
}

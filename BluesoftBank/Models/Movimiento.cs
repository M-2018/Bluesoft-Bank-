using System.ComponentModel.DataAnnotations;

namespace BluesoftBank.Models
{
    public class Movimiento
    {
        [Key]
        public long IdMovimiento { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string CiudadMovimiento { get; set; }
        public long IdCuenta { get; set; }
        //public Guid IdCuenta { get; set; }
        //public Cuenta Cuenta { get; set; }
    }
    public enum TipoMovimiento
    {
        Retiro,
        Consignacion
    }

}

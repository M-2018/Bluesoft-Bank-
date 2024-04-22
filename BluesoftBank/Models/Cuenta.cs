using System.ComponentModel.DataAnnotations;

namespace BluesoftBank.Models
{
    public class Cuenta
    {
        [Key]
        public long IdCuenta { get; set; }
        public string Numero { get; set; }
        public TipoCuenta Tipo { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaApertura { get; set; }
        public string CiudadCuenta { get; set; }
        public long IdCliente { get; set; }
        //public Cliente Cliente { get; set; }
        //public ICollection<Movimiento> Movimientos { get; set; }
    }
    public enum TipoCuenta
    {
        Ahorro,
        Corriente
    }
}

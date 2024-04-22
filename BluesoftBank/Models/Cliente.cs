using System.ComponentModel.DataAnnotations;

namespace BluesoftBank.Models
{
    public class Cliente
    {
        [Key]
        public long IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dirección { get; set; }
        public string Telefono { get; set; }
        public long IdCuenta { get; set; }
        //public ICollection<Cuenta> Cuentas { get; set; }



    }
}

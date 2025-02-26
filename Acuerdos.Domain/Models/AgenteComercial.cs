using System.ComponentModel.DataAnnotations;

namespace Acuerdos.Domain.Models
{
    public class AgenteComercial
    {
        [Key]
        public int IdAgente { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string NIF { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Acuerdos.Domain.Models
{
    public class TarifaConsumo
    {
        [Key]
        public int IdTarifa { get; set; }
        public string Nombre { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime? FinVigencia { get; set; }
        public string CodTarifaAcceso { get; set; }
        public int? IdProductoComercial { get; set; }
        public bool AñadirComisionAPrecios { get; set; }
        public int? IdComisionComercialTipo { get; set; }
        public int? IdComisionComercialAjustada { get; set; }
        public string CodCanal { get; set; }
        public int? IdModeloFactura { get; set; }
    }
}

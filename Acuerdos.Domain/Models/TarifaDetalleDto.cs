using System;

namespace Acuerdos.Domain.Models
{
    public class TarifaDetalleDto
    { 
        public int IdTarifaAcuerdo { get; set; }
        public int? IdTarifa { get; set; }
        public int PorcRenovacion { get; set; }
        public DateTime FechaVigor { get; set; } 
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

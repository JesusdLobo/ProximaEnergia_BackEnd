using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Acuerdos.Domain.Models
{
    public class AcuerdoComercial
    {
        [Key]
        public int IdAcuerdo { get; set; }
        public int? IdAgente { get; set; }
        public int? IdTrabajador { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? Ambito { get; set; }
        public byte? DuracionMeses { get; set; }
        public bool? ProrrogaAutomatica { get; set; }
        public byte? DuracionProrrogaMeses { get; set; }
        public byte? Exclusividad { get; set; }
        public string? CodFormaPago { get; set; }
         
        [JsonIgnore]
        public List<TarifaAcuerdo> Tarifas { get; set; } = new();
         
        public AgenteComercial? AgenteComercial { get; set; }
    }
}

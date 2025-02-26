using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Acuerdos.Domain.Models
{
    public class TarifaAcuerdo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTarifaAcuerdo { get; set; }
        public int IdAcuerdo { get; set; }
        public int? IdTarifa { get; set; }
        public int PorcRenovacion { get; set; }
        public DateTime FechaVigor { get; set; }

        [JsonIgnore]
        [ForeignKey("IdAcuerdo")]
        public AcuerdoComercial AcuerdoComercial { get; set; }
    }

    public class TarifaAcuerdoUpdateDto
    {
       
        public int IdAcuerdo { get; set; }
        public int? IdTarifa { get; set; }
        public int PorcRenovacion { get; set; }
        public DateTime FechaVigor { get; set; }
    }
}

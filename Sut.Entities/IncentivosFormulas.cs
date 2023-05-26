using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class IncentivosFormulas
    {
        [Key]

        public long IncentivosFormulasId { get; set; }
        public long EntidadId { get; set; }
        public decimal Numero_Licencias_Bajo { get; set; }
        public decimal Numero_Licencias_medio { get; set; }
        public decimal Numero_Licencias_Alto { get; set; }
        public decimal Numero_Licencias_MuyAlto { get; set; }
        public decimal Licencias_Notificaciones_Bajo { get; set; }
        public decimal Licencias_Notificaciones_Medio { get; set; }
        public decimal Licencias_Emitida_Alto { get; set; }
        public decimal Licencias_Emitida_MuyAlto { get; set; }
        public decimal Total_Bajo_Medio { get; set; }
        public decimal Total_Alto_MuyAlto { get; set; }
        public decimal Meta_Total { get; set; }
        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
        public int Estado { get; set; }


    }
}

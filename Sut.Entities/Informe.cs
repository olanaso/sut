using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Informe
    {
        [Key]
        public long InformeID { get; set; } 
        
        public int Numero { get; set; }

        public long RevisorId { get; set; }
        public long EntidadId { get; set; }
        public string Codigo { get; set; }
        public DateTime? Fecha { get; set; }

        public string Conclusiones { get; set; }
        public string Recomendacion { get; set; }
        public long FirmaId { get; set; }
        public string Estado { get; set; }

        public long ExpedienteId { get; set; }
        public int Anio { get; set; }
        public string Titulo { get; set; }
        public string UserCreacion { get; set; }
        public int valor { get; set; } 
        public int valor2 { get; set; }
        public int valor3 { get; set; }
        public int valor4 { get; set; }
        public int valor5 { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

    }
}

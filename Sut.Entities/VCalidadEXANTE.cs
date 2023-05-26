using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class VCalidadEXANTE
    {
        [Key]
        public int ICODCALIDADEXANTE { get; set; }  
        public string CodEntidad { get; set; } 
        public string Denominacion { get; set; } 
        public string Descripcion { get; set; } 
        public CalificacionProcedimiento CALIFICACION { get; set; }
        public TipoProcedimiento TIPOPROCEDIMIENTO  { get; set; }  
        public int? PLAZOATENCION  { get; set; } 
        public Renovacio RENOVACION  { get; set; } 
        public int? FRECUENCIARENOVACION  { get; set; } 
        public string BASELEGAL  { get; set; } 
        public Plazorenovacion Plazorenovacion { get; set; } 
        //public List<VCalidadEXANTERequisitos> CalidadRequisitos { get; set; } 

        public string ORGANO { get; set; }
        public string sustento { get; set; }


        public long EntidadId { get; set; }
        public string Nombre { get; set; }
        public string Acronimo { get; set; }
        public int codproyecto { get; set; }
        public string nomproyecto { get; set; }

        public string Codigo { get; set; }

        public string NORMASUT { get; set; }
        public string MigrarEntidad { get; set; }


    }

}

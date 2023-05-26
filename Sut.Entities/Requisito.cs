using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sut.Entities
{
    public class Requisito
    {
        public Requisito()
        {
            
        }

        [Key]
        public long RequisitoId { get; set; }

        [ForeignKey("Procedimiento")]
        public long ProcedimientoId { get; set; }
        public Procedimiento Procedimiento { get; set; }

        public int? Orden { get; set; }

        [Required, MaxLength(90000)]
        public string Nombre { get; set; }
        [MaxLength(90000)]
        public string Descripcion { get; set; }

        public TipoRequisito TipoRequisito { get; set; }
        public string CadenaTramite { get; set; }

        public string SustentoTecnico { get; set; }
        public string SustentoLegal { get; set; } 

        [ForeignKey("BaseLegal")]
        public long? BaseLegalId { get; set; }
        public BaseLegal BaseLegal { get; set; }
        public string BaseLegalTexto { get; set; }

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }

        public List<RequisitoFormulario> RequisitoFormulario { get; set; }
        public List<RequisitoProcedimiento> RequisitoProcedimiento { get; set; }

        public int RecNum { get; set; }

        public int Activar { get; set; }
        public string Titulo { get; set; }
        public int EditableTitulo { get; set; }
        public int Eliminado { get; set; }

    }
}

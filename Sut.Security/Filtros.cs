using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Sut.Entities; 


namespace Sut.Security
{
    public class Filtros
    {
        public CalificacionProcedimiento Clasificacion { get; set; }
        public FiltrosOrdenar Prestacionesanuales { get; set; }
        public FiltrosOrdenar PrestacionesCosto { get; set; }
        public FiltrosOrdenar Duracion { get; set; }
        public FiltrosOrdenar PlazoAtencion { get; set; }
        public FiltrosOrdenar Requisitos { get; set; }
        public Periodos Periodo { get; set; }
        public string estado { get; set; }

        public TipoActividad actividad { get; set; }
        public TipoValor valor { get; set; }


        public int seccion { get; set; }
        public int estadoseccion { get; set; }


        public string descripcion { get; set; }

    }
}

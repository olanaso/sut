using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Notificacion
    {
        [Key] 
        public long NotificacionId { get; set; }
        public string Asunto { get; set; }
        public string CCO { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoNotificacion { get; set; }        
        public int Estado { get; set; } 

        public string UserCreacion { get; set; }
        public string UserModificacion { get; set; }
        public DateTime? FecCreacion { get; set; }
        public DateTime? FecModificacion { get; set; }
 

    }
}

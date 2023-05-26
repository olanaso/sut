using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class RolMenu
    {

        [Key]
        public long RolmenuId { get; set; }

        [Required, ForeignKey("Roles")]
        public long RolId { get; set; }
        public Roles Roles { get; set; }

        [Required, ForeignKey("Menu")]
        public long MenuId { get; set; }
        public Menu Menu { get; set; }

      
        public int Orden { get; set; }
        public bool Ver { get; set; }
        public bool Nuevo { get; set; }
        public bool Editar { get; set; }
        public bool Guardar { get; set; }
        public bool Eliminar { get; set; }
        public bool Descargar { get; set; }
        public bool Agregar { get; set; }
        public bool Copiar { get; set; }
        public bool Terminar { get; set; }
        public bool Importar { get; set; }
        public bool Procesar { get; set; }
        public bool Anular { get; set; }
        public bool Activar { get; set; }
        public bool Generar { get; set; }
        public bool Reemplazar { get; set; }
        public bool Presentar { get; set; }
        public bool Sustentar { get; set; }
        public bool Observar { get; set; }
        public bool Publicar { get; set; }


    }
}

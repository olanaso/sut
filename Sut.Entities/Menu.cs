using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Menu
    {
        [Key]
        public long MenuId { get; set; }
 
        public string Descripcion { get; set; }
        public string Estilo { get; set; }
        public int Estado { get; set; }
        public int IdPadreMenu { get; set; }
        public string Gragico { get; set; }
        public string Dimension { get; set; } 
        public string url { get; set; } 

        public int MenuAdmin { get; set; }
        public int Orden { get; set; }
        public bool Ver_Ac { get; set; }
        public bool Nuevo_Ac { get; set; }
        public bool Editar_Ac { get; set; }
        public bool Guardar_Ac { get; set; }
        public bool Eliminar_Ac { get; set; }
        public bool Descargar_Ac { get; set; }
        public bool Agregar_Ac { get; set; }
        public bool Copiar_Ac { get; set; }
        public bool Terminar_Ac { get; set; }
        public bool Importar_Ac { get; set; }
        public bool Procesar_Ac { get; set; }
        public bool Anular_Ac { get; set; }
        public bool Activar_Ac { get; set; }
        public bool Generar_Ac { get; set; }
        public bool Reemplazar_Ac { get; set; }
        public bool Presentar_Ac { get; set; }
        public bool Sustentar_Ac { get; set; }
        public bool Observar_Ac { get; set; }
        public bool Publicar_Ac { get; set; }

        public int Nivel { get; set; }

        [NotMapped]
        public bool Ver { get; set; }
        [NotMapped]
        public bool Nuevo { get; set; }
        [NotMapped]
        public bool Editar { get; set; }
        [NotMapped]
        public bool Guardar { get; set; }
        [NotMapped]
        public bool Eliminar { get; set; }
        [NotMapped]
        public bool Descargar { get; set; }
        [NotMapped]
        public bool Agregar { get; set; }
        [NotMapped]
        public bool Copiar { get; set; }
        [NotMapped]
        public bool Terminar { get; set; }
        [NotMapped]
        public bool Importar { get; set; }
        [NotMapped]
        public bool Procesar { get; set; }
        [NotMapped]
        public bool Anular { get; set; }
        [NotMapped]
        public bool Activar { get; set; }
        [NotMapped]
        public bool Generar { get; set; }
        [NotMapped]
        public bool Reemplazar { get; set; }
        [NotMapped]
        public bool Presentar { get; set; }
        [NotMapped]
        public bool Sustentar { get; set; }
        [NotMapped]
        public bool Observar { get; set; }
        [NotMapped]
        public bool Publicar { get; set; } 




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class Correos
    {
        [Key] 

        public long id_correo { get; set; }
 
        public string Host { get; set; }
        public string Port { get; set; }
        public string CredentialsUser { get; set; }
        public string CredentialsPass { get; set; }
        public string From { get; set; }

    }
}

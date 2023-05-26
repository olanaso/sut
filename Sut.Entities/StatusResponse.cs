using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Entities
{
    public class StatusResponse<T> : StatusResponse
    {
        public T Data { get; set; }
    }

    public class StatusResponse
    {
        public StatusResponse()
        {
            Messages = new List<string>();
        }

        public string Message { get; set; }
        public List<string> Messages { get; set; }
        public bool Success { get; set; }

        public object Value { get; set; }

        public string Json { get; set; }

        public bool Validacion { get; set; }

    }
}

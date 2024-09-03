using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMDNIs.Shared.Models
{
    public class Consulta
    {
        public DateTime Fecha { get; set; }
        public string? Texto { get; set; }
        public bool HasError { get; set; }
        public Consulta(string? texto, bool hasError)
        {
            Fecha = DateTime.Now;
            Texto = texto;
            HasError = hasError;
        }
    }
}

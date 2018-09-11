using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.Caching
{
    public class Filtros
    {
        

        public int idEmp { get; set; }
        public string nombreEmp { get; set; }
        public string tipoEmp { get; set; }
        public DateTime fechaEmp { get; set; }

        public override string ToString()
        {
            return idEmp + " -> " + nombreEmp + " -> "+tipoEmp + " -> " + fechaEmp.ToString() ;
        }
    }
}
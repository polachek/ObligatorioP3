using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace Dominio
{
    public class TipoEvento
    {
        public int idTipoEvento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoEvento(string Nombre, string Descripcion)
        {
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
        }
    }
}

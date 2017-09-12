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
        private string tipo;
        private string desc;

        public TipoEvento(string tipo, string desc)
        {
            this.tipo = tipo;
            this.desc = desc;
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}

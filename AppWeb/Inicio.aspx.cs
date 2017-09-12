using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.Data.SqlClient;

namespace AppWeb
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (IsAvailable())
                {
                }else
                {
                    System.Windows.Forms.MessageBox.Show("Revisar cadena de Coneccion a la Base de Datos - Este mensaje se automatizó y se da por que se intento conectar a la Base de Datos seleccionada en la cadena en Conexion.cs, se automatizó para evitar problemas relacionados a la conexión");
                }
            }

        }

        public static bool IsAvailable()
        {
            SqlConnection cn = null;
            cn = Conexion.CrearConexion();

            try
            {
                cn.Open();
                cn.Close();
            }
            catch (SqlException)
            {
                return false;
            }

            return true;
        }
    }
}
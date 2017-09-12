using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace Dominio
{
    public class Conexion
    {
        #region Manejo de la conexión.
        //La cadena de conexión está configurada para el servidor de prueba 
        //que viene con Visual Studio
        //Cambiarla si se utiliza otro servicio de SQLServer.
        private static string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionSeba"].ConnectionString;
        private static string cadenaConexionPolaNotebook = ConfigurationManager.ConnectionStrings["ConexionPolachekNoteb"].ConnectionString;
        private static string cadenaConexionPolaPC = ConfigurationManager.ConnectionStrings["ConexionPolachekPC"].ConnectionString;

        public static SqlConnection CrearConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public static void AbrirConexion(SqlConnection cn)
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }

        public static void CerrarConexion(SqlConnection cn)
        {
            try
            {
                if (cn.State != ConnectionState.Closed)
                {
                    cn.Close();
                    cn.Dispose();
                }

            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }
        #endregion
    }
}

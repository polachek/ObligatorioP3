using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ServicioProveedor
    {

        public int IdServicio { get; set; }
        public string RutProveedor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }


        #region Acceso a Datos
        public bool InsertarServicioProveedor(SqlCommand cmd, string rut, Servicio miserv)
        {

            try
            {
                cmd.CommandText = @"INSERT INTO ProveedorServicios
                             VALUES(@idServicio, @rutProveedor, @nombre, @descripcion, @imagen)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idServicio", miserv.IdServicio);
                cmd.Parameters.AddWithValue("@RUT", rut);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@imagen", Foto);

                cmd.ExecuteNonQuery();


                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return false;
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ServicioProveedor : IEquatable<ServicioProveedor>
    {

        public int IdServicio { get; set; }
        public string RutProveedor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }

        public bool Equals(ServicioProveedor other)
        {
            if (other == null) return false;
            return (this.Nombre.Equals(other.Nombre));
        }

        public override string ToString()
        {
            string ret = string.Format("{0}", Nombre);
            return ret;
        }


        #region Acceso a Datos
        public bool InsertarServicioProveedor(SqlCommand cmd, ServicioProveedor miserv)
        {

            try
            {
                cmd.CommandText = @"INSERT INTO ProveedorServicios
                             VALUES(@idServicio, @rutProveedor, @nombre, @descripcion, @imagen)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idServicio", miserv.IdServicio);
                cmd.Parameters.AddWithValue("@rutProveedor", miserv.RutProveedor);
                cmd.Parameters.AddWithValue("@nombre", miserv.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", miserv.Descripcion);
                cmd.Parameters.AddWithValue("@imagen", miserv.Foto);

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

        #region Finders

        // FIND SERVICIOS PROVEEDOR
        public static List<ServicioProveedor> FindServiciosProveedor(string rut)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT *
                            FROM ProveedorServicios
                            WHERE rutProveedor = @rut";
            cmd.Parameters.AddWithValue("@rut", rut);
            cmd.Connection = cn;

            List<ServicioProveedor> listaServicios = null;
            try
            {
                Conexion.AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaServicios = new List<ServicioProveedor>();
                    while (dr.Read())
                    {
                        ServicioProveedor serv = CargarDatosDesdeReader(dr);
                        listaServicios.Add(serv);
                    }
                }
                return listaServicios;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        protected static ServicioProveedor CargarDatosDesdeReader(IDataRecord fila)
        {
            ServicioProveedor s = null;
            int idMiServicio = fila.IsDBNull(fila.GetOrdinal("idServicio")) ? 0 : fila.GetInt32(fila.GetOrdinal("idServicio"));
            string rutProveedor = fila.IsDBNull(fila.GetOrdinal("rutProveedor")) ? "" : fila.GetString(fila.GetOrdinal("rutProveedor"));
            string nombreServicio = fila.IsDBNull(fila.GetOrdinal("nombre")) ? "" : fila.GetString(fila.GetOrdinal("nombre"));
            string desc = fila.IsDBNull(fila.GetOrdinal("descripcion")) ? "" : fila.GetString(fila.GetOrdinal("descripcion"));
            string foto = fila.IsDBNull(fila.GetOrdinal("imagen")) ? "" : fila.GetString(fila.GetOrdinal("imagen"));

            if (fila != null)
            {
                s = new ServicioProveedor()
                {
                    IdServicio = idMiServicio,
                    RutProveedor = rutProveedor,
                    Nombre = nombreServicio,
                    Descripcion = desc,
                    Foto = foto,
                };
            }
            return s;
        }

        #endregion

    }
}

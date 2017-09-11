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
    public class Servicio : IActiveRecord
    {
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public string Descripcion { get; set; }
        public List<String> ListaTipoEventos = new List<String>();

        public override string ToString()
        {
            string ret = string.Format("{0}", Nombre);
            return ret;
        }

        #region Acceso a datos
        public bool Insertar()
        {
            throw new NotImplementedException();
        }

        public bool Eliminar()
        {
            throw new NotImplementedException();
        }

        public bool Modificar()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Finders
        
        public static Servicio FindByNombre(string nombre)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Servicio WHERE Nombre = @nombre");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@nombre", nombre);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    if (dr.Read())
                    {
                        string nombreServicio = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre"));
                        string desc = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));
                        string foto = dr.IsDBNull(dr.GetOrdinal("imagen")) ? "" : dr.GetString(dr.GetOrdinal("imagen"));

                        Servicio s = new Servicio
                        {
                            Nombre = nombreServicio,
                            Descripcion = desc,
                            Foto = foto,
                            ListaTipoEventos = new List<String>()
                        };                       
                        return s;
                    }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Servicio");
            }
            finally { cn.Close(); cn.Dispose(); }
        }        

        public static List<Servicio> FindAll()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT s.nombre AS Servicio, s.descripcion AS 'Descripción del servicio', s.imagen as 'Foto', t.nombre as 'Tipo de evento'
                                FROM Servicio AS s 
                                INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
                                INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento";
            cmd.Connection = cn;
            List<Servicio> listaServicios = null;
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaServicios = new List<Servicio>();
                    while (dr.Read())
                    {
                        Servicio s = CargarDatosDesdeReader(dr);
                        listaServicios.Add(s);
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

        public static List<String> FindTiposEvento(string nombre) {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT t.nombre
                                FROM Servicio AS s 
                                INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
                                INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento
                                WHERE t.nombre = @nombre";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@nombre", nombre);
            List<String> listaTiposEvento = null;
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaTiposEvento = new List<String>();
                    while (dr.Read())
                    {
                        string s = dr.GetString(dr.GetOrdinal("nombre"));
                        listaTiposEvento.Add(s);
                    }
                }
                return listaTiposEvento;
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

        protected static Servicio CargarDatosDesdeReader(IDataRecord fila)
        {
            Servicio s = null;
            string nombreServicio = fila.IsDBNull(fila.GetOrdinal("Servicio")) ? "" : fila.GetString(fila.GetOrdinal("Servicio"));
            string desc = fila.IsDBNull(fila.GetOrdinal("Descripción del servicio")) ? "" : fila.GetString(fila.GetOrdinal("Descripción del servicio"));
            string foto = fila.IsDBNull(fila.GetOrdinal("Foto")) ? "" : fila.GetString(fila.GetOrdinal("Foto"));
            string tipoEvento = fila.IsDBNull(fila.GetOrdinal("Tipo de evento")) ? "" : fila.GetString(fila.GetOrdinal("Tipo de evento"));

            if (fila != null)
            {
                s = new Servicio()
                {
                    Nombre = nombreServicio,
                    Descripcion = desc,
                    Foto = foto
                };
                s.ListaTipoEventos.Add(tipoEvento);
            }
            return s;
        }
        #endregion

    }
}

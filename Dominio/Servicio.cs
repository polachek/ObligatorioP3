using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Dominio
{
    public class Servicio : IActiveRecord
    {
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public string Descripcion { get; set; }
        List<TipoEvento> ListaTipoEventos = new List<TipoEvento>();

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
        public static List<Servicio> FindAll()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT s.nombre, s.descripcion, s.imagen, t.nombre 
                                FROM Servicio s, TipoEvento t, TipoEventoYServicio e
                                WHERE s.idServicio = e.idServicio
                                AND e.idTipoEvento = t.idTipoEvento";
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

        protected static Servicio CargarDatosDesdeReader(IDataRecord fila)
        {
            Servicio s = null;
            if (fila != null)
            {
                s = new Servicio
                {
                    Nombre = fila.IsDBNull(fila.GetOrdinal("Nombre")) ? "" : fila.GetString(fila.GetOrdinal("Nombre")),
                    Descripcion = fila.IsDBNull(fila.GetOrdinal("Descripcion")) ? "" : fila.GetString(fila.GetOrdinal("Descripcion")),
                    Foto = fila.IsDBNull(fila.GetOrdinal("imagen")) ? "" : fila.GetString(fila.GetOrdinal("imagen"))
                };
            }
            return s;
        }
        #endregion

    }
}

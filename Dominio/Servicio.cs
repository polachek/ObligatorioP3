using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Dominio
{
    public class Servicio : IActiveRecord
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<TipoEvento> ListaTipoEventos = new List<TipoEvento>();

        public override string ToString()
        {
            string ret = string.Format("{0}", Nombre);
            return ret;
        }

        public bool Equals(Servicio other)
        {
            if (other == null) return false;
            return (this.Nombre.Equals(other.Nombre));
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

        #region Exportar catálogo a txt
        public static bool grabarCatalogoTxt(string rutaArchivo)
        {
            try
            {
                File.WriteAllText(rutaArchivo, String.Empty);

                FileStream fs = new FileStream(rutaArchivo, FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                List<Servicio> listaServicios = Servicio.FindAll();

                foreach (Servicio serv in listaServicios)
                {
                    serv.ListaTipoEventos = Servicio.FindTiposEventoByServicio(serv.Nombre);
                    string listaServTipoEvento = "";
                    foreach (TipoEvento tipoEv in serv.ListaTipoEventos)
                    {
                        listaServTipoEvento += tipoEv.Nombre +":";
                    }
                    int largo = listaServTipoEvento.Length;
                    string tiposDeEventoPorServicio = listaServTipoEvento.Substring(0,largo-1);
                    sw.WriteLine(serv.Nombre + "#" + tiposDeEventoPorServicio);
                }

                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
                return false;
            }
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
                        int miIdServicio = dr.IsDBNull(dr.GetOrdinal("idServicio")) ? 0 : dr.GetInt32(dr.GetOrdinal("idServicio"));
                        string nombreServicio = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre"));
                        string desc = dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? "" : dr.GetString(dr.GetOrdinal("Descripcion"));

                        Servicio s = new Servicio
                        {
                            IdServicio = miIdServicio,
                            Nombre = nombreServicio,
                            Descripcion = desc,
                            //ListaTipoEventos = new List<TipoEvento>()
                        };                       
                        return s;
                    }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Servicio" + ex);
            }
            finally { cn.Close(); cn.Dispose(); }
        }        

        public static List<Servicio> FindAll()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT * FROM Servicio";
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

        public static List<Servicio> FindServicioTipo()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT s.IdServicio AS IdServicio, s.nombre AS Servicio, s.descripcion AS 'Descripción del servicio', t.nombre as 'Tipo de evento'
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
                        Servicio s = CargarDatosDesdeReaderServicioTipo(dr);
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


        public static List<TipoEvento> FindTiposEventoByServicio(string servicio)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT t.nombre, t.descripcion, t.idTipoEvento
                                FROM Servicio AS s 
                                INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
                                INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento
                                WHERE s.nombre = @servicio";

            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@servicio", servicio);
            List<TipoEvento> listaTipoEvento = null;
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaTipoEvento = new List<TipoEvento>();
                    while (dr.Read())
                    {
                        Servicio s = Servicio.FindByNombre(servicio);
                        
                        string tipo = dr.IsDBNull(dr.GetOrdinal("nombre")) ? "" : dr.GetString(dr.GetOrdinal("nombre"));
                        string desc = dr.IsDBNull(dr.GetOrdinal("descripcion")) ? "" : dr.GetString(dr.GetOrdinal("descripcion"));
                        TipoEvento t = new TipoEvento(tipo, desc);
                        listaTipoEvento.Add(t);
                    }
                }
                return listaTipoEvento;
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
            int idMiServicio = fila.IsDBNull(fila.GetOrdinal("idServicio")) ? 0 : fila.GetInt32(fila.GetOrdinal("idServicio"));
            string nombreServicio = fila.IsDBNull(fila.GetOrdinal("nombre")) ? "" : fila.GetString(fila.GetOrdinal("nombre"));
            string desc = fila.IsDBNull(fila.GetOrdinal("descripcion")) ? "" : fila.GetString(fila.GetOrdinal("descripcion"));

            if (fila != null)
            {
                s = new Servicio()
                {
                    IdServicio = idMiServicio,
                    Nombre = nombreServicio,
                    Descripcion = desc,
                };
            }
            return s;
        }

        protected static Servicio CargarDatosDesdeReaderServicioTipo(IDataRecord fila)
        {
            Servicio s = null;
            int idMiServicio = fila.IsDBNull(fila.GetOrdinal("idServicio")) ? 0 : fila.GetInt32(fila.GetOrdinal("idServicio"));
            string nombreServicio = fila.IsDBNull(fila.GetOrdinal("Servicio")) ? "" : fila.GetString(fila.GetOrdinal("Servicio"));
            string desc = fila.IsDBNull(fila.GetOrdinal("Descripción del servicio")) ? "" : fila.GetString(fila.GetOrdinal("Descripción del servicio"));

            if (fila != null)
            {
                s = new Servicio()
                {
                    IdServicio = idMiServicio,
                    Nombre = nombreServicio,
                    Descripcion = desc,
                };
            }
            return s;
        }
        #endregion

    }
}

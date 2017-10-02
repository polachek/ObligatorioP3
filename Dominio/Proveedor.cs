﻿using System;
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
    public abstract class Proveedor : IActiveRecord
    {
        #region Propiedades

        public string RUT { get; set; }
        public string NombreFantasia { get; set; }
        public string Email { get; set; }
        public Usuario MiUsuario { get; set; } = new Usuario();
        public string Telefono { get; set; }
        public string FechaRegistro { get; set; }
        public bool esInactivo { get; set; }
        public static double Arancel{ get; set; }
        public string Tipo { get; set; }
        public List<ServicioProveedor> ListaServicios { get; set; }

        #endregion

        public override string ToString()
        {
            string ret = string.Format("{0} {1}", "Rut: " + RUT + " - ", "NombreFantasia:" + NombreFantasia);
            return ret;
        }

        #region Métodos de lógica

        /// <summary>
        /// VALIDA LOS DATOS EN FORM PARA AGREGAR PROVEEDOR
        /// </summary>
        /// <returns></returns>
        public virtual bool Validar()
        {
            return this.RUT.Length == 12 
                && this.NombreFantasia.Length > 3
                && this.Email.Length > 3
                && this.Telefono.Length > 3
                 ;
        }

        /// <summary>
        /// CONTROLA SI EXISTE EL RUT INGRESADO
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public bool ExisteRut(string rut)
        {
            bool ret = false;
            if (FindByRUT(rut) != null)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// CONTROLA SI EXISTE EL EMAIL INGRESADO
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExisteEmail(string email)
        {
            bool ret = false;
            if (FindByEmail(email) != null)
            {
                ret = true;
            }
            return ret;
        }
        #endregion

        #region Manejo de Usuario

        /// <summary>
        /// AGREGA EL OBJETO USUARIO
        /// </summary>
        /// <param name="usu"></param>
        /// <returns></returns>
        public bool AgregarUsuario(Usuario usu)
        {
            this.MiUsuario = usu;
            return true;
        }
        #endregion

        #region Acceso a datos
        /// <summary>
        /// AGREGA UN PROVEEDOR A LA BASE DE DATOS
        /// </summary>
        /// <returns></returns>
        public bool Insertar()
        {
            SqlConnection cn = null;
            if (!this.Validar()) return false;
            SqlTransaction trn = null;

            cn = Conexion.CrearConexion();

            try
            {
                SqlCommand cmd = new SqlCommand();

                cn.Open();
                trn = cn.BeginTransaction();

                cmd.Connection = cn;
                cmd.Transaction = trn;
                
                Usuario usuarioAInsertar = new Usuario();

                usuarioAInsertar.User = MiUsuario.User;
                usuarioAInsertar.Passw = MiUsuario.Passw;
                usuarioAInsertar.Rol = MiUsuario.Rol;
                usuarioAInsertar.Email = MiUsuario.Email;
                usuarioAInsertar.Insertar(cmd);
                
                cmd.CommandText=
                    @"INSERT INTO Proveedor 
                    VALUES (@rut, @nombrefantasia, @email, @telefono, @fecharegistro, @esInactivo, @tipo);
                    SELECT CAST (SCOPE_IDENTITY() AS INT)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@RUT", this.RUT);
                cmd.Parameters.AddWithValue("@nombreFantasia", this.NombreFantasia);
                cmd.Parameters.AddWithValue("@email", this.Email);
                cmd.Parameters.AddWithValue("@telefono", this.Telefono);
                cmd.Parameters.AddWithValue("@fechaRegistro", this.FechaRegistro);
                cmd.Parameters.AddWithValue("@esInactivo", this.esInactivo);
                cmd.Parameters.AddWithValue("@tipo", this.Tipo);

                cmd.Transaction = trn;
                cmd.ExecuteNonQuery();

                //Se implementó condición para lista de servicios igual null para evitar conflicto al cargar wcf con proveedor nulo
                if (ListaServicios == null)
                {

                }else if (ListaServicios.Count() > 0)
                {
                    foreach (ServicioProveedor miServ in ListaServicios)
                    {
                        miServ.InsertarServicioProveedor(cmd, miServ);
                    }
                }

                if (Tipo == "VIP")
                {
                    ProveedorVIP.Insertar(cmd, this.RUT);
                }else if (Tipo == "COMUN")
                {
                    cmd.CommandText =
                    @"INSERT INTO ProveedorComun 
                    VALUES (@rutProveedor);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@rutProveedor", this.RUT);
                    cmd.ExecuteNonQuery();
                }

                trn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return false;
            }
            finally { cn.Close(); cn.Dispose(); trn.Dispose(); }
        }
    
        /// <summary>
        /// ELIMINA UN PROVEEDOR DADO RUT
        /// </summary>
        /// <returns></returns>
        public bool Eliminar()
        {
            string cadenaDelete = @"DELETE Proveedor WHERE RUT=@rut;";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cadenaDelete;
            cmd.Parameters.Add(new SqlParameter("@rut", this.RUT));
            SqlConnection cn = Conexion.CrearConexion();
            try
            {
                Conexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
                return false;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        public bool Modificar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OBTIENE ARANCEL
        /// </summary>
        /// <returns></returns>
        public static decimal ObtenerArancel()
        {
            decimal ret = 0;
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(); 
            cmd.Connection = cn;
            cmd.CommandText = @"SELECT Arancel FROM Parametros";
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read()) {
                        ret = dr.IsDBNull(dr.GetOrdinal("arancel")) ? 0 : dr.GetDecimal(dr.GetOrdinal("arancel"));
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return ret;
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }

        /// <summary>
        /// OBTIENE PORCENTAJE EXTRA
        /// </summary>
        /// <returns></returns>
        public static int ObtenerPorcentajeExtra()
        {
            int ret = 0;
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = @"SELECT porcentajeExtra FROM Parametros";
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read()) {
                        ret = dr.IsDBNull(dr.GetOrdinal("porcentajeExtra")) ? 0 : dr.GetInt32(dr.GetOrdinal("porcentajeExtra"));
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return ret;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }

        /// <summary>
        /// MODIFICAR ARANCEL
        /// </summary>
        /// <param name="nuevoArancel"></param>
        /// <returns></returns>
        public static bool ModificarArancel(double nuevoArancel)
        {
            bool ret = false;
            SqlConnection cn = null;
            cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            try
            {
                Conexion.AbrirConexion(cn);
                cmd.Connection = cn;
                cmd.CommandText =@"UPDATE Parametros SET arancel = @nuevoArancel";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nuevoArancel", nuevoArancel);
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                ret = false;
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// MODIFICAR PORCENTAJE
        /// </summary>
        /// <param name="nuevoPorcentaje"></param>
        /// <returns></returns>
        public static bool ModificarPorcentajeExtra(int nuevoPorcentaje)
        {
            bool ret = false;
            SqlConnection cn = null;
            cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            try
            {
                Conexion.AbrirConexion(cn);
                cmd.Connection = cn;
                cmd.CommandText = @"UPDATE Parametros SET porcentajeExtra = @nuevoPorcentaje";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nuevoPorcentaje", nuevoPorcentaje);
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                ret = false;
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// DESACTIVAR PROVEEDOR
        /// </summary>
        /// <returns></returns>
        public bool DesactivarProv()
        {
            bool ret = false;
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Proveedor WHERE Rut = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", this.RUT);
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd.CommandText = @"UPDATE Proveedor SET esInactivo = 1 WHERE RUT = @rut;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@rut", this.RUT);
                        cmd.ExecuteNonQuery();
                        ret = true;
                    }
                }
                else
                {
                    ret = false;
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Proveedor" + ex);
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }
        #endregion

        #region Exportar Proveedores Txt
        public static bool grabarProveedoresTxt(string rutaArchivo)
        {
            try
            {
                File.WriteAllText(rutaArchivo, String.Empty);

                FileStream fs = new FileStream(rutaArchivo, FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);

                List<Proveedor> listaProv = Proveedor.FindAll();

                foreach (Proveedor prov in listaProv)
                {
                    prov.ListaServicios = ServicioProveedor.FindServiciosProveedor(prov.RUT);
                    string listaServProv = "";
                    foreach (ServicioProveedor servProv in prov.ListaServicios)
                    {
                        listaServProv += servProv.Nombre + ":" + servProv.Descripcion + ":" + servProv.Foto + "#";
                    }
                    sw.WriteLine(prov.RUT + "#" + prov.NombreFantasia + "#" + prov.Email + "#" + prov.Telefono + "|" + listaServProv);
                }
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió un error: '{0}'", e);
                return false;
            }
        }
        #endregion

        #region Finders

        /// <summary>
        /// BUSCAR PROVEEDOR POR RUT
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public static Proveedor FindByRUT(string rut)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Proveedor WHERE Rut = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", rut);
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        string miTipo = dr["tipo"].ToString();

                        if (miTipo == "COMUN")
                        {
                            Proveedor p = new ProveedorComun
                            {
                                RUT = rut,
                                NombreFantasia = dr["NombreFantasia"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                esInactivo = (bool)dr["esInactivo"],
                                Tipo = miTipo,
                            };
                            return p;
                        }
                        else if (miTipo == "VIP")
                        {
                            Proveedor p = new ProveedorVIP
                            {
                                RUT = rut,
                                NombreFantasia = dr["NombreFantasia"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                esInactivo = (bool)dr["esInactivo"],
                                Tipo = miTipo,
                            };
                            return p;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Proveedor" + ex);
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }


        /// <summary>
        /// BUSCAR PROVEEDOR POR MAIL
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Proveedor FindByEmail(string email)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Proveedor WHERE Email = @email");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@email", email);
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    if (dr.Read())
                    {
                        string miTipo = dr["tipo"].ToString();
                        if (miTipo == "COMUN")
                        {
                            Proveedor p = new ProveedorComun
                            {
                                RUT = dr["rut"].ToString(),
                                NombreFantasia = dr["NombreFantasia"].ToString(),
                                Email = email,
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                esInactivo = (bool)dr["esInactivo"],
                                Tipo = miTipo,
                            };
                            return p;
                        }
                        else if (miTipo == "VIP")
                        {
                            Proveedor p = new ProveedorVIP
                            {
                                RUT = dr["rut"].ToString(),
                                NombreFantasia = dr["NombreFantasia"].ToString(),
                                Email = email,
                                Telefono = dr["Telefono"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                esInactivo = (bool)dr["esInactivo"],
                                Tipo = miTipo,
                            };
                            return p;
                        }
                    }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Proveedor" + ex);
            }
            finally {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }
       
        
        /// <summary>
        /// OBTENER TODOS LOS PROVEEDORES
        /// </summary>
        /// <returns></returns>
        public static List<Proveedor> FindAll()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Proveedor";
            cmd.Connection = cn;
            List<Proveedor> listaProveedores = null;
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaProveedores = new List<Proveedor>();
                    while (dr.Read())
                    {
                        Proveedor p = CargarDatosDesdeReader(dr);
                        listaProveedores.Add(p);
                    }
                }
                return listaProveedores;
            }
            catch (SqlException ex)
            {                
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
                cn.Dispose();
            }
        }


        /// <summary>
        /// CARGAR DATOS DESDE READER
        /// </summary>
        /// <param name="fila"></param>
        /// <returns></returns>
        protected static Proveedor CargarDatosDesdeReader(IDataRecord fila)
        {
            Proveedor p = null;
            if (fila != null)
            {
                string miTipo;
                miTipo = fila.IsDBNull(fila.GetOrdinal("tipo")) ? "" : fila.GetString(fila.GetOrdinal("tipo"));

                if (miTipo == "COMUN")
                {
                    p = new ProveedorComun
                    {
                        RUT = fila.IsDBNull(fila.GetOrdinal("Rut")) ? "" : fila.GetString(fila.GetOrdinal("Rut")),
                        NombreFantasia = fila.IsDBNull(fila.GetOrdinal("NombreFantasia")) ? "" : fila.GetString(fila.GetOrdinal("NombreFantasia")),
                        Email = fila.IsDBNull(fila.GetOrdinal("Email")) ? "" : fila.GetString(fila.GetOrdinal("Email")),
                        Telefono = fila.IsDBNull(fila.GetOrdinal("Telefono")) ? "" : fila.GetString(fila.GetOrdinal("Telefono")),
                        FechaRegistro = fila.GetDateTime(fila.GetOrdinal("fechaRegistro")).ToString("yyyy/MM/dd"),
                        esInactivo = fila.GetBoolean(fila.GetOrdinal("esInactivo")),
                        Tipo = miTipo,
                    };
                }else if (miTipo == "VIP")
                {
                    p = new ProveedorVIP
                    {
                        RUT = fila.IsDBNull(fila.GetOrdinal("Rut")) ? "" : fila.GetString(fila.GetOrdinal("Rut")),
                        NombreFantasia = fila.IsDBNull(fila.GetOrdinal("NombreFantasia")) ? "" : fila.GetString(fila.GetOrdinal("NombreFantasia")),
                        Email = fila.IsDBNull(fila.GetOrdinal("Email")) ? "" : fila.GetString(fila.GetOrdinal("Email")),
                        Telefono = fila.IsDBNull(fila.GetOrdinal("Telefono")) ? "" : fila.GetString(fila.GetOrdinal("Telefono")),
                        FechaRegistro = fila.GetDateTime(fila.GetOrdinal("fechaRegistro")).ToString("yyyy/MM/dd"),
                        esInactivo = fila.GetBoolean(fila.GetOrdinal("esInactivo")),
                        Tipo = miTipo,
                    };
                }

            }
            return p;
        }

        /// <summary>
        /// OBTIENE EL PORCENTAJE EXTRA DADO RUT
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public static int FindPorcentajeVip(string rut)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From ProveedorVip WHERE rutProveedor = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", rut);
            try
            {
                Conexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        int porcentajeExtra;
                        {
                            porcentajeExtra = Convert.ToInt32(dr["porcentExtraAsign"]);
                        };
                        return porcentajeExtra;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Proveedor" + ex);
            }
            finally { cn.Close(); cn.Dispose(); }
        }
        #endregion
    }
}

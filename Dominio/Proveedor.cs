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
        public List<Servicio> ListaServicios { get; set; }

        #endregion

        public override string ToString()
        {
            string ret = string.Format("{0} {1}", "Rut: " + RUT + " - ", "NombreFantasia:" + NombreFantasia);
            return ret;
        }


        #region Métodos de lógica
        public virtual bool Validar()
        {
            return this.RUT.Length == 12 
                && this.NombreFantasia.Length > 3
                && this.Email.Length > 3
                && this.Telefono.Length > 3
                 ;
        }
        #endregion

        #region Manejo de Usuario
        
        public bool AgregarUsuario(Usuario usu)
        {
            this.MiUsuario = usu;
            return true;
        }
        #endregion

        #region Acceso a datos
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
                
                Usuario miUsuario = new Usuario();

                miUsuario.User = MiUsuario.User;
                miUsuario.Passw = MiUsuario.Passw;
                miUsuario.Rol = 2;
                miUsuario.Insertar(cmd);

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

                


                /*cmd.CommandText = @"INSERT INTO Usuario
                            VALUES(@usuario,@password,@rol)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", MiUsuario.User);
                cmd.Parameters.AddWithValue("@password", MiUsuario.Passw);
                cmd.Parameters.AddWithValue("@rol", 2);
                cmd.ExecuteNonQuery();*/

                if (Tipo == "VIP")
                {
                    cmd.CommandText = @"INSERT INTO ProveedorVip
                            VALUES(@idProveedor,@porcentajeExtra)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idProveedor", this.RUT);
                    cmd.Parameters.AddWithValue("@porcentajeExtra", 5);
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
        #endregion

        #region Finders
        public static Proveedor FindByRUT(string rut)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Proveedor WHERE Rut = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", rut);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        /*Proveedor p = new Proveedor
                        {
                            RUT = rut,
                            NombreFantasia = dr["NombreFantasia"].ToString(),
                            Email = dr["Email"].ToString(),
                        };
                        return p;*/
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("No existe el Proveedor");

            }
            finally { cn.Close(); cn.Dispose(); }
        }

        public static Proveedor FindByEmail(string email)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Proveedor WHERE Email = @email");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@email", email);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    if (dr.Read())
                    {
                        /*Proveedor p = new Proveedor
                        {
                            RUT = dr["RUT"].ToString(),
                            NombreFantasia = dr["NombreFantasia"].ToString(),
                            Email = email,
                        };
                        return p;*/
                    }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("No existe el Proveedor");
            }
            finally { cn.Close(); cn.Dispose(); }
        }
       
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
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                Conexion.CerrarConexion(cn);
            }
        }

        protected static Proveedor CargarDatosDesdeReader(IDataRecord fila)
        {
            Proveedor p = null;
            if (fila != null)
            {
                /*p = new Proveedor
                {
                    RUT = fila.IsDBNull(fila.GetOrdinal("Rut")) ? "" : fila.GetString(fila.GetOrdinal("Rut")),
                    NombreFantasia = fila.IsDBNull(fila.GetOrdinal("NombreFantasia")) ? "" : fila.GetString(fila.GetOrdinal("NombreFantasia")),
                    Email = fila.IsDBNull(fila.GetOrdinal("Email")) ? "" : fila.GetString(fila.GetOrdinal("Email")),
                    Telefono = fila.IsDBNull(fila.GetOrdinal("Telefono")) ? "" : fila.GetString(fila.GetOrdinal("Telefono")),
                    FechaRegistro = fila.GetDateTime(fila.GetOrdinal("fechaRegistro")).ToString("yyyy/MM/dd"),
                    esInactivo = fila.GetBoolean(fila.GetOrdinal("esInactivo")),
                    esVip = fila.GetBoolean(fila.GetOrdinal("esVip")),
                };*/
            }
            return p;
        }

        public static int FindPorcentajeVip(string rut)
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From ProveedorVip WHERE idProveedor = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", rut);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        int porcentajeExtra;
                        {
                            porcentajeExtra = Convert.ToInt32(dr["porcentajeExtra"]);
                        };
                        return porcentajeExtra;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {

                throw new Exception("No existe el Proveedor");

            }
            finally { cn.Close(); cn.Dispose(); }
        }
        #endregion
    }
}

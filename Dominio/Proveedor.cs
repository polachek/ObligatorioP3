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
    public class Proveedor : IActiveRecord
    {
        #region Propiedades

        public string RUT { get; set; }
        public string NombreFantasia { get; set; }
        public string Email { get; set; }
        //public string Usuario { get; set; } lo comento para ver despues
        public string Telefono { get; set; }
        public string Password { get; set; }        
        //public DateTime FechaRegistro { get; set; } de momento lo comento para test con BD y agrego string con formato Date de la BD
        public string FechaRegistro { get; set; }
        public bool esInactivo { get; set; }
        public static double Arancel{ get; set; }
        public double Arancelll { get; set; } // Solo para test con BD
        public int porcentajeExtra { get; set; }

        #endregion

        public override string ToString()
        {
            string ret = string.Format("{0} {1}", "Rut: " + RUT + " - ", "NombreFantasia:" + NombreFantasia);
            return ret;
        }

        #region Métodos de lógica
        public virtual bool Validar()
        {
            return this.RUT.Length > 1 // Hay que poner 12, pero para test lo dejamos así
                && this.NombreFantasia.Length > 3
                && this.Email.Length > 3
                && this.Telefono.Length > 3
               // && this.FechaRegistro > DateTime.Now
                 ;
        }
        #endregion

        #region Acceso a datos
        public bool Insertar()
        {
            SqlConnection cn = null;
            if (!this.Validar()) return false;
            try
            {
                cn =Conexion.CrearConexion();
                SqlCommand cmd = new SqlCommand(
                   @"INSERT INTO Proveedor 
                    VALUES (@rut, @nombrefantasia, @email, @telefono, @password, @arancel, @fecharegistro, @esInactivo, @porcentajeExtra);
                    SELECT CAST (SCOPE_IDENTITY() AS INT)", cn
                );
                cmd.Parameters.AddWithValue("@RUT", this.RUT);
                cmd.Parameters.AddWithValue("@nombreFantasia", this.NombreFantasia);
                cmd.Parameters.AddWithValue("@email", this.Email);
                cmd.Parameters.AddWithValue("@telefono", this.Telefono);
                cmd.Parameters.AddWithValue("@password", this.Password);
                cmd.Parameters.AddWithValue("@arancel", this.Arancelll);                
                cmd.Parameters.AddWithValue("@fechaRegistro", this.FechaRegistro);
                cmd.Parameters.AddWithValue("@esInactivo", this.esInactivo);
                cmd.Parameters.AddWithValue("@porcentajeExtra", this.porcentajeExtra);
                cn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return false;
            }
            finally { cn.Close(); cn.Dispose();}
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
                        Proveedor p = new Proveedor
                        {
                            RUT = rut,
                            NombreFantasia = dr["NombreFantasia"].ToString(),
                            Email = dr["Email"].ToString(),
                        };
                        return p;
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
                        Proveedor p = new Proveedor
                        {
                            RUT = dr["RUT"].ToString(),
                            NombreFantasia = dr["NombreFantasia"].ToString(),
                            Email = email,
                        };
                        return p;
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
                p = new Proveedor
                {
                    RUT = fila.IsDBNull(fila.GetOrdinal("Rut")) ? "" : fila.GetString(fila.GetOrdinal("Rut")),
                    NombreFantasia = fila.IsDBNull(fila.GetOrdinal("NombreFantasia")) ? "" : fila.GetString(fila.GetOrdinal("NombreFantasia")),
                    Email = fila.IsDBNull(fila.GetOrdinal("Email")) ? "" : fila.GetString(fila.GetOrdinal("Email")),
                };
            }
            return p;
        }
        #endregion
    }
}

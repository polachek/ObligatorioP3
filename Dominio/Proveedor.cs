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
        //public bool Activo { get; set; } de momento lo comento para test con BD y agrego int
        public int Activo { get; set; }
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
        static string cadenaConexion =  ConfigurationManager.ConnectionStrings
         ["ConexionPolachekNoteb"].ConnectionString; // Seba Agrega la tuya en Web.Config así como agregue las mías
                                                  // y para conectarte cambias aquí el string de conexion
        public bool Insertar()
        {
            SqlConnection cn = null;
            if (!this.Validar()) return false;
            try
            {
                cn =
                   new SqlConnection(cadenaConexion);
                SqlCommand cmd = new SqlCommand(
                   @"INSERT INTO Proveedor 
VALUES (@rut, @nombrefantasia, @email, @telefono, @password, @arancel, @fecharegistro, @esInactivo, @porcentajeExtra);
SELECT CAST (SCOPE_IDENTITY() AS INT)", cn);
                cmd.Parameters.AddWithValue
                    ("@RUT", this.RUT);
                cmd.Parameters.AddWithValue
                  ("@nombreFantasia", this.NombreFantasia);
                cmd.Parameters.AddWithValue
                  ("@email", this.Email);
                cmd.Parameters.AddWithValue
                  ("@telefono", this.Telefono);
                cmd.Parameters.AddWithValue
                  ("@password", this.Password);
                cmd.Parameters.AddWithValue
                  ("@arancel", this.Arancelll);                
                cmd.Parameters.AddWithValue
                  ("@fechaRegistro", this.FechaRegistro);
                cmd.Parameters.AddWithValue
                  ("@esInactivo", this.Activo);
                cmd.Parameters.AddWithValue
                  ("@porcentajeExtra", this.porcentajeExtra);
                cn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.
                    Debug.Assert(false, "Error: " + ex.Message);
                return false;
            }
            finally { cn.Close(); cn.Dispose();}
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
        public static Proveedor FindByRUT(string rut)
        {
            SqlConnection cn = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand
                (@"SELECT * From Proveedor WHERE Rut = @rut");
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@rut", rut);
            try
            {
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    if (dr.Read())
                    {
                        Proveedor p = new Proveedor
                        {
                            RUT = rut,
                            NombreFantasia = dr["NombreFantasia"].ToString(),
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
        #endregion
    }
}

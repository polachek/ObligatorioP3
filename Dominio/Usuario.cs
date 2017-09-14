using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public string User { get; set; }
        public string Passw { get; set; }
        public int Rol { get; set; }

        #region Encriptar Pass
        public static string EncriptarPassSHA512(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        #endregion

        /* #region Agregar Usuario
         public bool Insertar(SqlCommand cmd)
         {
             SqlConnection cn = null;
              SqlTransaction trn = null;

             cn = Conexion.CrearConexion();
             cn.Open();
             trn = cn.BeginTransaction();

             try
             {
                 cmd.CommandText = @"INSERT INTO Usuario
                             VALUES(@usuario,@password,@rol)";
                 cmd.Parameters.AddWithValue("@usuario", user);
                 cmd.Parameters.AddWithValue("@password", pass);
                 cmd.Parameters.AddWithValue("@rol", rol);
                 cmd.ExecuteNonQuery();


                 return true;
             }
             catch (Exception ex)
             {
                 System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                 return false;
             }
            // finally { cn.Close(); cn.Dispose(); trn.Dispose(); }
         }
         #endregion*/
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class ProveedorVIP : Proveedor
    {
        int PorcentajeExtra { get; set; }

        #region Agregar ProveedorVIP
        public static bool Insertar(SqlCommand cmd, string rut)
        {
            int porcentajeExtra = ObtenerPorcentajeExtra(cmd);
            try
            {
                cmd.CommandText = @"INSERT INTO ProveedorVip
                             VALUES(@rutProveedor,@porcentExtraAsign)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@rutProveedor", rut);
                cmd.Parameters.AddWithValue("@porcentExtraAsign", porcentajeExtra);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);
                return false;
            }
        }

        public static int ObtenerPorcentajeExtra(SqlCommand cmd)
        {
            int ret = 0;
            cmd.CommandText = @"SELECT porcentajeExtra FROM Parametros";
            try {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ret = (int)dr["porcentajeExtra"];
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error: " + ex.Message);

            }
            return ret;
        }
        #endregion
    }
}

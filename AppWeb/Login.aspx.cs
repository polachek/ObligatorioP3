using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["usu"] = null;
            }
        }

        private SqlConnection cn;

        private void db_connection()
        {
            
            try
            {
                cn = Conexion.CrearConexion();
                cn.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool validate_login(string user, string pass)
        {
            db_connection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Select * from Usuario where usuario=@user and password=@pass";

            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            cmd.Connection = cn;

            SqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                // login es la consulta - GetInt32 obtiene un int - GetOrdinal obtiene el reultado de la column
                string rol = login.GetInt32(login.GetOrdinal("rol")).ToString();

                Session["User"] = user;
                Session["Rol"] = rol;
                cn.Close();
                return true;
            }
            else
            {
                cn.Close();
                return false;
            }
        }


        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string user = Login1.UserName;
            string pass = Usuario.EncriptarPassSHA512(Login1.Password);

            bool r = validate_login(user, pass);

            if (r)
            {
                e.Authenticated = true;

                if (Session["Rol"].ToString() == "2")
                {
                    Response.Redirect("~/PanelProveedor.aspx");
                } else if (Session["Rol"].ToString() == "1")
                {
                    Response.Redirect("~/PanelAdministrador.aspx");
                }else
                {
                    Response.Redirect("~/Inicio.aspx");
                }

            }
            else
                e.Authenticated = false;
        }
    }
}
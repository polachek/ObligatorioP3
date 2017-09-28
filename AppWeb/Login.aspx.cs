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

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //Guarda el usuario y el password ingresado en el form de login
            //Encripta el password con el generador de hash SHA 512
            string user = Login1.UserName;
            string pass = Usuario.EncriptarPassSHA512(Login1.Password);

            //Guarda el resultado de validar el usuario y el pass 
            //validate_login guarda el usuario y rol en variables de sesión
            bool r = validate_login(user, pass);

            if (r)
            {
                e.Authenticated = true;

                //Si el rol es 2 => usuario es PROVEEDOR
                if (Session["Rol"].ToString() == "2")
                {
                    Response.Redirect("~/PanelProveedor.aspx");
                }
                // Si el rol es 1 => el usuario es ADMINISTRADOR
                else if (Session["Rol"].ToString() == "1")
                {
                    Response.Redirect("~/PanelAdministrador.aspx");
                }
                else
                {
                    Response.Redirect("~/Inicio.aspx");
                }
            }
            else
                e.Authenticated = false;
        }

        private bool validate_login(string user, string pass)
        {
            //Crea y abre conexión
            SqlConnection cn = Conexion.CrearConexion();          
            cn.Open();

            //Declara el SqlCommand y le asigna CommandText y conexión
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * 
                            FROM Usuario 
                            WHERE usuario=@user 
                            AND password=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Connection = cn;

            //Ejecuta el SqlDataReader (lee la secuencia de filas que devolvió la consulta)
            SqlDataReader login = cmd.ExecuteReader();

            //Controlo si se realizó la lectura de las filas
            //Si leyó la fila, guardo el usuario y el rol en variables de sesión
            if (login.Read())
            {
                // login es la consulta - GetInt32 obtiene un int - GetOrdinal obtiene el resultado de la column
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
    }
}
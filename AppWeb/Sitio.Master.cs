using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class Sitio : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                userNotlog.Visible = true;
            }else
            {
                userLog.Visible = true;
                LBUser.Text = "Bienvenido " + Session["User"].ToString();
            }
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["Rol"] = null;
            Session["usu"] = null;

            Response.Redirect("~/Inicio.aspx");
        }
    }
}
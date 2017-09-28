using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppWeb
{
    public partial class PanelAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                System.Windows.Forms.MessageBox.Show("Para ver este contenido debes iniciar sesión.");
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}
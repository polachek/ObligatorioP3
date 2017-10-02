using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference6;
using Dominio;

namespace AppWeb
{
    public partial class ServicioGuardarProv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblGuardado.Visible = false;
        }

        protected void BtnGuardarProveedores_Click(object sender, EventArgs e)
        {
            GuardarProvTxtClient clienteWCF = new GuardarProvTxtClient();
            clienteWCF.Open();
            clienteWCF.guardarProveedoresTxt();
            LblGuardado.Visible = true;
            clienteWCF.Close();
        }
    }
}
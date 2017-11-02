using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference5;
using Dominio;

namespace AppWeb
{
    public partial class ServicioGuardarCatalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LblGuardado.Visible = false;
        }

        protected void BtnGuardarCatalogo_Click(object sender, EventArgs e)
        {
            GuardarCatalogoTxtClient clienteWCF = new GuardarCatalogoTxtClient();
            clienteWCF.Open();
            clienteWCF.guardarCatalogoTxt();
            LblGuardado.Visible = true;
            clienteWCF.Close();
        }
    }
}
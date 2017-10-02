using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference4;
using Dominio;


namespace AppWeb
{
    public partial class ServicioListadoProv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicioListaProvClient clienteWCF = new ServicioListaProvClient();
            clienteWCF.Open();
            GVProveedores.DataSource = clienteWCF.ObtenerProveedores();
            GVProveedores.DataBind();
            clienteWCF.Close();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace AppWeb
{
    public partial class TestServicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btnservicios_Click(object sender, EventArgs e)
        {
            List<Servicio> listaServ = Servicio.FindAll();
            ListBox1.DataSource = listaServ;
            ListBox1.DataBind();
        }
    }
}
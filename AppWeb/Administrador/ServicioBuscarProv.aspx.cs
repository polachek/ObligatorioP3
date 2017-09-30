using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference1;
using WCFProveedorDadoRUT;
using Dominio;

namespace AppWeb
{
    public partial class ServicioBuscarProv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BuscarProv_Click(object sender, EventArgs e)
        {
            string rut = TxtBuscarProv.Text;
            ProveedorDadoRUTClient provWCF = new ProveedorDadoRUTClient();
            provWCF.Open();
            DtoProveedor p = provWCF.buscarProveedorRut(rut);
            Session["Proveedor"] = p;
            provWCF.Close();
            if (Session["Proveedor"] != null)
            {
                LblRUT.Text = "RUT: " + p.RUT;
                LblNombre.Text = "Nombre: " + p.NombreFantasia;
                string servicios = "";
                foreach (ServicioProveedor s in p.ListaServicios)
                {
                    servicios += s.Nombre + " ";
                }
                LblServicios.Text = "Servicios: " + servicios;
            }
        }
    }
}
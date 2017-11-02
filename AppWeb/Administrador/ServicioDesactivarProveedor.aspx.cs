using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference8;

namespace AppWeb
{
    public partial class ServicioDesactivarProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PanelDatosProv.Visible = false;
            PanelDesactivar.Visible = false;
            PanelConfirmacion.Visible = false;
        }

        protected void BtnBuscarProv_Click(object sender, EventArgs e)
        {
            string rut = TBBuscarProv.Text;
            DesactivarProvClient clienteWCF = new DesactivarProvClient();
            clienteWCF.Open();
            DtoProveedor p = clienteWCF.BuscarProveedorRut(rut);
            Session["Proveedor"] = p;
            clienteWCF.Close();
            LBRUT.Text = p.RUT;
            LBNomFant.Text = p.NombreFantasia;
            LBEmail.Text = p.Email;
            LBInactivo.Text = p.esInactivo.ToString();
            LBTelefono.Text = p.Telefono;
            LBVip.Text = p.Tipo;
            PanelDatosProv.Visible = true;
            PanelDesactivar.Visible = true;
        }

        protected void BtnDesactivarProv_Click(object sender, EventArgs e)
        {
            DesactivarProvClient clienteWCF = new DesactivarProvClient();
            clienteWCF.Open();
            DtoProveedor p = Session["Proveedor"] as DtoProveedor;
            clienteWCF.DesactivarProveedor(p);
            clienteWCF.Close();
            PanelConfirmacion.Visible = true;
        }
    }
}
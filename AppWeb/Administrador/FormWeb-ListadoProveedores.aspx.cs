using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace AppWeb.Administrador
{
    public partial class FormWeb_ListadoProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Proveedor> listaProv = Proveedor.FindAll();
            if (listaProv == null || listaProv.Count == 0)
            {
                PanelCantProveedores.Visible = true;
            }
            else
            {
                PanelCantProveedores.Visible = false;
                GridViewListadoProveedores.DataSource = listaProv;
                GridViewListadoProveedores.DataBind();
            }
        }

        protected void GridProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = int.Parse(e.CommandArgument + "");
            List<Proveedor> listaProv = Proveedor.FindAll();

            if (e.CommandName == "VerDatos")
            {
                PanelDatos.Visible = true;
                Proveedor prov = listaProv[fila];
                LBRUT.Text = "RUT :" + prov.RUT;
                LBNomFant.Text = "Nombre Fantasia :" + prov.NombreFantasia;
                LBEmail.Text = "Email :" + prov.Email;
                LBTelefono.Text = "Telefono :" + prov.Telefono;
                prov.ListaServicios = ServicioProveedor.FindServiciosProveedor(prov.RUT);
                GridViewServiciosProv.DataSource = prov.ListaServicios;
                GridViewServiciosProv.DataBind();

                if (!prov.esInactivo) {
                    LBInactivo.ForeColor = System.Drawing.Color.Green;
                    string strEsInactivo = "Es Inactivo : No";
                    LBInactivo.Text = strEsInactivo;
                }else
                {
                    LBInactivo.ForeColor = System.Drawing.Color.Red;
                    string strEsInactivo = "Es Inactivo : Si";
                    LBInactivo.Text = strEsInactivo;
                }

                if (prov.Tipo == "COMUN")
                {
                    LBVip.ForeColor = System.Drawing.Color.Green;
                    string strEsVip = "Es VIP : No";
                    LBVip.Text = strEsVip;
                }
                else if(prov.Tipo == "VIP")
                {
                    LBVip.ForeColor = System.Drawing.Color.Red;
                    string strEsVip = "Es VIP : Si";
                    LBVip.Text = strEsVip;
                }

                if (prov.Tipo == "VIP")
                {
                    int porcentExt = Proveedor.FindPorcentajeVip(prov.RUT);
                    Extra.Text = "Porcentaje extra: " + porcentExt;
                }else
                {
                    Extra.Text = "";
                }
            }
           
        }
    }
}
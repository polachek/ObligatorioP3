using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference3;
using Dominio;

namespace AppWeb
{
    public partial class ServicioMostrarCatalogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Inicializar();
            }
            if (Session["Servicio"] == null)
                Session["Servicio"] = new DtoServicio();
        }

        private void Inicializar()
        {
            CatalogoServiciosClient clienteWCF = new CatalogoServiciosClient();
            clienteWCF.Open();
            GVServicios.DataSource = clienteWCF.ObtenerServicios();
            GVServicios.DataBind();
            clienteWCF.Close();
            PanelTipoEvento.Visible = false;
        }

        protected void GVServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = int.Parse(e.CommandArgument + "");
            int id = fila + 1;
            CatalogoServiciosClient clienteWCF = new CatalogoServiciosClient();
            clienteWCF.Open();
            IEnumerable<DtoServicio> servicios = clienteWCF.ObtenerServicios();
            DtoServicio miServicio = clienteWCF.BuscarServicio(id);
            List<DtoTipoEvento> eventos = new List<DtoTipoEvento>();
            string tipos = "";
            if (e.CommandName == "VerTipoEvento")
            {
                PanelTipoEvento.Visible = true;
                LBlServicio.Text = "Servicio :" + miServicio.Servicio;
                LBlDescripcion.Text = "Descripción :" + miServicio.Descripcion;
                foreach (DtoTipoEvento t in miServicio.misTiposEventos) {
                    tipos += t.Nombre+"</br> ";
                }
                LBlEventos.Text = "Tipos de eventos: "+tipos;
            }
            clienteWCF.Close();
        }
    }
}
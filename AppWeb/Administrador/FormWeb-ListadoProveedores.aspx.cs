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
        {/*
            Rematadora r = Rematadora.Instancia;
            int fila = int.Parse(e.CommandArgument + "");

            if (e.CommandName == "Remove")
            {
                if (r.ListaRematadores[fila].RemateAsignado.Count == 0)
                {
                    TxtError.Text = "";
                    r.ListaRematadores.RemoveAt(fila);

                    GridViewRematadores.DataSource = r.ListaRematadores;
                    GridViewRematadores.DataBind();
                    CheckCantidadPersonas();
                }
                else
                {
                    TxtError.Text = "No es posible eliminar un Rematador con Remates Asociados a él";
                }


            }
            else if (e.CommandName == "Actualizar")
            {
                Rematador pAModif = r.ListaRematadores[fila];
                TxtID.Text = pAModif.Id + "";
                TxtNombre.Text = pAModif.Nombre;
                TxtApellido.Text = pAModif.Apellido;
                TxtTelefono.Text = pAModif.Telefono;
                BtnAccion.Text = "Modificar";

                Session["filaMod"] = fila;
            }*/
        }
    }
}
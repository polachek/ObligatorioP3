using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace AppWeb
{
    public partial class WFRegProveedores : System.Web.UI.Page
    {
        private static List<Servicio> ListaMiServicios;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarServicios();
            if (ListaMiServicios == null)
            {
                ListaMiServicios = new List<Servicio>();
            }
        }

        protected void BtnAccion_Click(object sender, EventArgs e)
        {
            Asignacion.Text = "";

            string rut = TxtRut.Text;
            string nomFant = TxtNomFantasia.Text;
            string email = TxtEmail.Text;
            string tel = TxtTel.Text;
            string pass = TxtPass.Text;
            string tipo = "";


            if (ListaMiServicios.Count() == 0)
            {
                Asignacion.Text = "No se puede agregar un Proveedor sin Servicios asociados";
            }else
            {
                Asignacion.Text = "";
                if (CheckBoxVip.Checked)
                { tipo = "VIP"; }
                else { tipo = "COMUN"; }

                DateTime fechaRegDateTime = DateTime.Now;
                string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");

                if (tipo == "COMUN")
                {
                    Proveedor p = new ProveedorComun { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo};
                    if (validarRutyEmail(p)) { insertarProveedor(p, pass); }
                }
                else
                {
                    Proveedor p = new ProveedorVIP { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo};
                    if (validarRutyEmail(p)) { insertarProveedor(p, pass); }
                }
            }

        }

        private bool validarRutyEmail(Proveedor prov)
        {
            bool ret = false;
            bool existeRut = prov.ExisteRut(prov);
            bool existeEmail = prov.ExisteEmail(prov);
            if (existeRut) Asignacion.Text = "Ya existe un Proveedor con ese Rut";
            if (existeEmail) Asignacion.Text = "Ya existe un Proveedor con ese Email";
            if (!existeRut && !existeEmail)
            {
                ret = true;
            }
            return ret;
        }

        private void insertarProveedor(Proveedor p, string pass)
        {
            // Verificaciones de Rut y Email OK
            Asignacion.Text = "";
            string passEncriptada = Usuario.EncriptarPassSHA512(pass);
            Usuario usu = new Usuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

            p.AgregarUsuario(usu);

            p.ListaServicios = ListaMiServicios;

            if (p.Insertar())
            {
                Asignacion.Text = "Insertaste a : " + p.RUT;
            }
            else
                Asignacion.Text = "No";
        }

        private void cargarServicios()
        {
            List<Servicio> listaServicios = Servicio.FindAll();
            if (listaServicios == null || listaServicios.Count == 0)
            {
                PanelCantServicios.Visible = true;
            }
            else
            {
                PanelCantServicios.Visible = false;
                GridViewListadoServicios.DataSource = listaServicios;
                GridViewListadoServicios.DataBind();
            }
        }

        protected void GridServicios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            List<Servicio> listaServicios = Servicio.FindAll();

            int fila = int.Parse(e.CommandArgument + "");

            if (e.CommandName == "AgregarServicio")
            {
                Servicio serv = new Servicio();
                serv = listaServicios[fila];

                if (serv != null)
                {
                    if (ListaMiServicios.Contains(serv))
                    {
                        Asignacion.Text = "Servicio ya agregado";
                    }else
                    {
                        Asignacion.Text = "";
                        ListaMiServicios.Add(serv);
                    }
                    
                }

                if (ListaMiServicios.Count() != 0)
                {
                    ListBoxServicios.DataSource = ListaMiServicios;
                    ListBoxServicios.DataBind();
                }               

            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.IO;

namespace AppWeb
{
    public partial class WFRegProveedores : System.Web.UI.Page
    {
        private static List<ServicioProveedor> ListaMiServicios;

        protected void Page_Load(object sender, EventArgs e)
        {            

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

                Asignacion.Text = "";
                if (CheckBoxVip.Checked)
                { tipo = "VIP"; }
                else { tipo = "COMUN"; }

                DateTime fechaRegDateTime = DateTime.Now;
                string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");

                if (tipo == "COMUN")
                {
                    Proveedor p = new ProveedorComun { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo};
                    if (p.ExisteRut(p.RUT))
                    {
                        Asignacion.Text = "Ya existe un proveedor con el RUT ingresado.";
                    }
                    else if (p.ExisteEmail(p.Email))
                    {
                        Asignacion.Text = "Ya existe un proveedor con el email ingresado.";
                    }
                    else
                    {
                        Asignacion.Text = "";
                        Session["ProvINSessionPass"] = pass;
                        Session["ProvINSession"] = p;
                        Paso1AltaProv.Visible = false;
                        Paso2ServProv.Visible = true;
                        cargarServicios();
                        if (ListaMiServicios == null)
                        {
                            ListaMiServicios = new List<ServicioProveedor>();
                        }
                 }
                }
                else if (tipo == "VIP")
                {
                    Proveedor p = new ProveedorVIP { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo};
                    if (p.ExisteRut(p.RUT))
                    {
                        Asignacion.Text = "Ya existe un proveedor con el RUT ingresado.";
                    }
                    else if (p.ExisteEmail(p.Email))
                    {
                        Asignacion.Text = "Ya existe un proveedor con el email ingresado.";
                    }
                    else
                    {
                        Asignacion.Text = "";
                        Session["ProvINSessionPass"] = pass;
                        Session["ProvINSession"] = p;
                        Paso1AltaProv.Visible = false;
                        Paso2ServProv.Visible = true;
                        cargarServicios();
                        if (ListaMiServicios == null)
                        {
                            ListaMiServicios = new List<ServicioProveedor>();
                        }
                  }
                } 

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
                limpiarForm();
            }
            else
                Asignacion.Text = "No";
        }

        private void limpiarForm()
        {
            TxtRut.Text = "";
            TxtNomFantasia.Text = "";
            TxtEmail.Text = "";
            TxtTel.Text = "";
            TxtPass.Text = "";
            CheckBoxVip.Checked = false;
            ListaMiServicios.Clear();
            ListBoxServicios.Items.Clear();
            ListBoxServicios.DataSource = null;
            ListBoxServicios.DataBind();

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
                PanelAsignarServicio.Visible = true;

                Servicio serv = new Servicio();
                serv = listaServicios[fila];
                HiddeIdServicio.Value = serv.IdServicio.ToString();

                if (serv != null) { ServNombre.Text = serv.Nombre; }
            }
        }

        protected void BtnAsigServAccion_Click(object sender, EventArgs e)
        {
            ServicioProveedor servProv = new ServicioProveedor();

            Proveedor miProveedor = Session["ProvINSession"] as Proveedor;

            servProv.Nombre = ServNombre.Text;
            servProv.IdServicio = int.Parse(HiddeIdServicio.Value);
            servProv.RutProveedor = miProveedor.RUT;
            servProv.Descripcion = ServDesc.Text;
            string ruta = Server.MapPath("~/images/servicios-proveedor/");

            if (ServFotoUpload.HasFile)
            {
                string[] partesNombre = ServFotoUpload.FileName.Split('.');
                string extension = partesNombre[partesNombre.Length - 1];
                string nombreArch = string.Format("Rut_{0}__ServicioID_{1}.{2}", servProv.RutProveedor, servProv.IdServicio, extension);

                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                ServFotoUpload.SaveAs(ruta + nombreArch);

                servProv.Foto = "~/images/servicios-proveedor/" + nombreArch;
            }


            if (ListaMiServicios.Contains(servProv))
            {
                Asignacion.Text = "Servicio ya agregado";
            }
            else
            {
                Asignacion.Text = "";
                ListaMiServicios.Add(servProv);

                ServNombre.Text = "";
                ServDesc.Text = "";
                PanelAsignarServicio.Visible = false;
            }

            if (ListaMiServicios.Count() != 0)
            {
                ListBoxServicios.DataSource = ListaMiServicios;
                ListBoxServicios.DataBind();
            }

        }

        protected void BtnRegistroProv_Click(object sender, EventArgs e)
        {
            if (ListaMiServicios.Count() == 0)
            {
                LBAsignacionReg.Text = "No se puede agregar un Proveedor sin Servicios asociados";
            }else
            {
                Paso1AltaProv.Visible = true;
                Paso2ServProv.Visible = false;
                Proveedor p = Session["ProvINSession"] as Proveedor;
                string pass = Session["ProvINSessionPass"].ToString();
                insertarProveedor(p, pass); 
            }
        }

    }
}
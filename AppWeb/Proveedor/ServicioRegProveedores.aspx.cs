using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference9;
using System.IO;

namespace AppWeb
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        List<DtoServicioProveedor> misDtosServicios = new List<DtoServicioProveedor>();

        protected void Page_Load(object sender, EventArgs e)
        {
            AgregarProvClient clienteWCF = new AgregarProvClient();
            clienteWCF.Open();
            ListBoxServicios.DataSource = clienteWCF.CargarServicios();
            ListBoxServicios.DataBind();
            clienteWCF.Close();
        }

        protected void BtnRegistroProv_Click(object sender, EventArgs e)
        {
            AgregarProvClient clienteWCF = new AgregarProvClient();
            clienteWCF.Open();

            LblAsignacion.Text = "";

            string rut = TxtRut.Text;
            string nomFant = TxtNomFantasia.Text;
            string email = TxtEmail.Text;
            string tel = TxtTel.Text;
            string pass = TxtPass.Text;
            string tipo = "";
            bool esInactivo = false;

            LblAsignacion.Text = "";
            if (CheckBoxVip.Checked)
            { tipo = "VIP"; }
            else { tipo = "COMUN"; }

            DateTime fechaRegDateTime = DateTime.Now;
            string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");

            if (tipo == "COMUN")
            {
                DtoProveedor p = new DtoProveedor { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo };
                if (clienteWCF.ExisteRUT(p.RUT))
                {
                    LblAsignacion.Text = "Ya existe un proveedor con el RUT ingresado.";
                }
                else if (clienteWCF.ExisteMail(p.Email))
                {
                    LblAsignacion.Text = "Ya existe un proveedor con el email ingresado.";
                }
                else
                {
                    bool esVip = false;
                    clienteWCF.InsertarProveedor(rut, nomFant, email, tel, esInactivo, esVip, pass);
                    foreach (DtoServicioProveedor s in misDtosServicios) {
                        clienteWCF.AgregarServicioAProveedor(s,p);
                    }
                    LblAsignacion.Text = "";
                }
                Session["prov"] = p as DtoProveedor;
            }
            else if (tipo == "VIP")
            {
                DtoProveedor p = new DtoProveedor { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo };
                if (clienteWCF.ExisteRUT(p.RUT))
                {
                    LblAsignacion.Text = "Ya existe un proveedor con el RUT ingresado.";
                }
                else if (clienteWCF.ExisteMail(p.Email))
                {
                    LblAsignacion.Text = "Ya existe un proveedor con el email ingresado.";
                }
                else
                {
                    bool esVip = true;
                    clienteWCF.InsertarProveedor(rut, nomFant, email, tel, esInactivo, esVip, pass);
                    foreach (DtoServicioProveedor s in misDtosServicios)
                    {
                        clienteWCF.AgregarServicioAProveedor(s, p);
                    }
                    LblAsignacion.Text = "";
                }
                Session["prov"] = p as DtoProveedor;
            }

            clienteWCF.Close();
        }
        
        

        private void limpiarForm()
        {
            TxtRut.Text = "";
            TxtNomFantasia.Text = "";
            TxtEmail.Text = "";
            TxtTel.Text = "";
            TxtPass.Text = "";
            CheckBoxVip.Checked = false;
            ListBoxServicios.Items.Clear();
            ListBoxServicios.DataSource = null;
            ListBoxServicios.DataBind();

        }
        
        

        protected void BtnAsigServAccion_Click(object sender, EventArgs e)
        {
            AgregarProvClient clienteWCF = new AgregarProvClient();
            clienteWCF.Open();

            DtoProveedor miProveedor = Session["prov"] as DtoProveedor;

            DtoServicioProveedor servProv = new DtoServicioProveedor();

            string mitmpServ = ListBoxServicios.SelectedValue;

            servProv.IdServicio = ListBoxServicios.SelectedIndex;
            servProv.Nombre = ListBoxServicios.DataTextField;
            servProv.Descripcion = TBDescripcion.Text;
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

            misDtosServicios.Add(servProv);
        }

    }
}
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
                    LblAsignacion.Text = "";
                }
                Session["prov"] = p as DtoProveedor;
            }

            clienteWCF.Close();
        }
        
        private void insertarProveedor(Proveedor p, string pass)
        {
            // Verificaciones de Rut y Email OK
            LblAsignacion.Text = "";
            string passEncriptada = Usuario.EncriptarPassSHA512(pass);
            Usuario usu = new Usuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

            p.AgregarUsuario(usu);

            p.ListaServicios = ListaMiServicios;

            if (p.Insertar())
            {
                LblAsignacion.Text = "Insertaste a : " + p.RUT;
                limpiarForm();
            }
            else
                LblAsignacion.Text = "No";
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
        
        

        protected void BtnAsigServAccion_Click(object sender, EventArgs e)
        {
            ServicioProveedor servProv = new ServicioProveedor();

            Proveedor miProveedor = Session["ProvINSession"] as Proveedor;
            servProv.RutProveedor = miProveedor.RUT;
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


            if (ListaMiServicios.Contains(servProv))
            {
                LblAsignacion.Text = "Servicio ya agregado";
            }
            else
            {
                LblAsignacion.Text = "";
                ListaMiServicios.Add(servProv);
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
            }
            else
            {
                Proveedor p = Session["ProvINSession"] as Proveedor;
                string pass = Session["ProvINSessionPass"].ToString();
                insertarProveedor(p, pass);
            }
        }

    }
}
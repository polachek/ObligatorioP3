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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAccion_Click(object sender, EventArgs e)
        {
            string rut = TxtRut.Text;
            string nomFant = TxtNomFantasia.Text;
            string email = TxtEmail.Text;
            string tel = TxtTel.Text;
            string pass = TxtPass.Text;
            bool CheckVip;

            if (CheckBoxVip.Checked)
            {CheckVip = true;}else {CheckVip = false;}

            DateTime fechaRegDateTime = DateTime.Now;
            string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");

            Proveedor p = new Proveedor { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, Arancelll = 25, FechaRegistro = fechaRegistro, esInactivo = false, esVip = CheckVip };

            // Validacion si ya existe un Proveedor con ese Rut o email ingresado
            if(Proveedor.FindByRUT(p.RUT) != null)
            {
                Asignacion.Text = "Ya existe un Proveedor con ese Rut";
            }else if (Proveedor.FindByEmail(p.Email) != null)
            {
                Asignacion.Text = "Ya existe un Proveedor con ese Email";
            }else
            {
                // Verificaciones de Rut y Email OK
                Asignacion.Text = "";
                string passEncriptada = Usuario.EncriptarPassSHA512(pass);
                Usuario usu = new Usuario { User = rut, Passw = passEncriptada };

                p.AgregarUsuario(usu);

                if (p.Insertar())
                {
                    Asignacion.Text = "Insertaste a : " + p.RUT;
                }
                else
                    Asignacion.Text = "No";

            }
            

        }

        

            /* RESPALDO PARA LISRA PROVEEDORES
            protected void BtnAsig3_Click(object sender, EventArgs e)
            {
                List<Proveedor> listaProv = Proveedor.FindAll();
                if (listaProv == null || listaProv.Count == 0)
                {
                    Asignacion.Text = "No hay proovedores";
                }
                else
                {
                    listprov.DataSource = listaProv;
                    listprov.DataBind();
                }
            }
            */
        }
}
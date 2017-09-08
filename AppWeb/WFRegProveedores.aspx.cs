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

        protected void BtnAsig_Click(object sender, EventArgs e)
        {
            Proveedor p = new Proveedor { RUT = "12345", NombreFantasia = "Nombre Fantasia Prov", Email = "Email@proveedor", Telefono = "12345", Password = "1234", Arancelll = 25, FechaRegistro = "2017/09/15", Activo = 0, porcentajeExtra = 2 };
            if (p.Insertar())
            {
                Asignacion.Text = "Insertaste a : " + p.RUT;
            }
            else
                Asignacion.Text = "No";
        }

        protected void BtnAsig2_Click(object sender, EventArgs e)
        {
            Asignacion.Text = "Buscar por Rut :" + Proveedor.FindByRUT("ProvTest");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppWeb.ServiceReference7;
using Dominio;

namespace AppWeb
{
    public partial class ServicioModificarParametros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicioModificarParametrosClient clienteWCF = new ServicioModificarParametrosClient();
            clienteWCF.Open();
            LblValorArancel.Text = clienteWCF.ObtenerArancel().ToString();
            LblValorPorcentaje.Text = clienteWCF.ObtenerPorcentajeExtra().ToString();
            clienteWCF.Close();
        }

        protected void BtnArancel_Click(object sender, EventArgs e)
        {
            ServicioModificarParametrosClient clienteWCF = new ServicioModificarParametrosClient();
            clienteWCF.Open();
            string arancelStr = TBArancel.Text;
            double arancel = Convert.ToDouble(arancelStr);
            clienteWCF.ModificarArancel(arancel);
            LblValorArancel.Text = clienteWCF.ObtenerArancel().ToString();
            clienteWCF.Close();
        }

        protected void BtnPorcentaje_Click(object sender, EventArgs e)
        {
            ServicioModificarParametrosClient clienteWCF = new ServicioModificarParametrosClient();
            clienteWCF.Open();
            string porcentajeExtraStr = TBPorcentaje.Text;
            int porcentajeExtra = Convert.ToInt32(porcentajeExtraStr);
            clienteWCF.ModificarPorcentajeExtra(porcentajeExtra);
            LblValorPorcentaje.Text = clienteWCF.ObtenerPorcentajeExtra().ToString();
            clienteWCF.Close();
        }
    }
}
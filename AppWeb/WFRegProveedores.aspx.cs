using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace AppWeb
{
    public partial class WFRegProveedores : System.Web.UI.Page
    {
        //Inicializa la lista de servicios seleccionados por el proveedor.
        List<Servicio> listaServiciosSeleccionados = new List<Servicio>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // La primera vez
            {
                cargarServicios();
                BindGridView();
            }
            else // Segunda vez y demás
            {

            }

        }

        protected void BtnAccion_Click(object sender, EventArgs e)
        {
            Asignacion.Text = "";

            //Obtiene la fecha actual
            DateTime fechaRegDateTime = DateTime.Now;

            //Declara las variables y las inicializo con los valores del formulario
            string rut = TxtRut.Text;
            string nomFant = TxtNomFantasia.Text;
            string email = TxtEmail.Text;
            string tel = TxtTel.Text;
            string pass = TxtPass.Text;
            string tipo = "";
            bool vip = CheckBoxVip.Checked;
            string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");
            List<Servicio> listaServicios = new List<Servicio>();

            if (tipo == "Hola")
            {
                Asignacion.Text = "No se puede agregar un Proveedor sin Servicios asociados";
            }
            else
            {
                //Controla el tipo de proveedor
                if (vip){ tipo = "VIP"; } else { tipo = "COMUN";}

                //Construye la instancia con el tipo de proveedor que corresponda
                if (tipo == "COMUN")
                {
                    Proveedor p = new ProveedorComun { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo };
                    p.ListaServicios = listaServiciosSeleccionados;
                    if (validarRutyEmail(p)) { insertarProveedor(p, pass); }
                }
                else
                {
                    Proveedor p = new ProveedorVIP { RUT = rut, NombreFantasia = nomFant, Email = email, Telefono = tel, FechaRegistro = fechaRegistro, esInactivo = false, Tipo = tipo };
                    p.ListaServicios = listaServiciosSeleccionados;
                    if (validarRutyEmail(p)) { insertarProveedor(p, pass); }
                }
            }

        }

        private bool validarRutyEmail(Proveedor prov)
        {
            bool ret = false;
            bool existeRut = Proveedor.ExisteRut(prov);
            bool existeEmail = Proveedor.ExisteEmail(prov);
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

            if (p.Insertar())
            {
                Asignacion.Text = "El proveedor con RUT: " + p.RUT + " se agregó exitosamente.";
            }
            else
                Asignacion.Text = "No";
        }

        private List<Servicio> cargarServicios()
        {
            List<Servicio> listaServicios = Servicio.FindAll();
            if (listaServicios == null || listaServicios.Count == 0)
            {
                PanelCantServicios.Visible = true;
            }
            else
            {
                PanelCantServicios.Visible = false;
            }

            return listaServicios;
        }

        //Carga la GridView de servicios con todos los servicios cargados en la BD
        private void BindGridView()
        {
            SqlConnection cn = Conexion.CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT * From Servicio", cn);
            cmd.Connection = cn;
            try
            {
                cn.Open();
                GridViewListadoServicios.DataSource = cmd.ExecuteReader();
                GridViewListadoServicios.DataBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception("No existe el Proveedor");
            }
            finally
            {
                cn.Close();
            }
        }

        //Obtiene los servicios seleccionados en la GridView
        protected void ObtenerServiciosSeleccionados(object sender, EventArgs e)
        {
            //Cargo los servicios en una lista
            List<Servicio> listaServicios = cargarServicios();

            //Inicializo una List<String> para agregar valores que vienen de la columna 'Nombre' de la GridView 
            List<String> listaNombres = new List<String>();

            //Creo una DataTable
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("Nombre"),
                new DataColumn("Descripción"),
                new DataColumn("Imagen")
            });

            //Recorro las filas y si el checkbox está chequeado, agrego el dato de la columna NOMBRE a la lista dt.Rows
            //Con el método HttpUtility.HtmlDecode obtengo el valor original de la columna nombre (evito conflictos con caracteres especiales)
            foreach (GridViewRow row in GridViewListadoServicios.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkServicio = (row.Cells[0].FindControl("chkServicio") as CheckBox);
                    if (chkServicio.Checked)
                    {
                        string valor = row.Cells[1].Text;
                        string nombre = HttpUtility.HtmlDecode(valor);
                        dt.Rows.Add(nombre);
                        listaNombres.Add(nombre);
                    }
                }
            }

            //Recorro dt.Rows y, si su valor coinicide con el nombre del servicio, agrego el servicio a la ListaServiciosSeleccionados
            foreach (string nombre in listaNombres) {
                foreach (Servicio s in listaServicios) {
                    if (nombre == s.Nombre) {
                        listaServiciosSeleccionados.Add(s);
                    }
                }
            }

            //Cargo la GridView de Servicios seleccionados con los servicios seleccionados en la GridViewListadoServicios
            GridViewSeleccionados.DataSource = dt;
            GridViewSeleccionados.DataBind();
        }

    }
}
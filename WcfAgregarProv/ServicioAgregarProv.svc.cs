using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WcfAgregarProv
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioAgregarProv : IAgregarProv
    {
        public bool AgregarServicioAProveedor(DtoServicioProveedor servicio, DtoProveedor prov)
        {
            bool ret = false;
            if (prov.Tipo == "COMUN")
            {
                //Construye un Proveedor
                Proveedor p = new ProveedorComun();
                //Inicializa la lista de ServicioProveedor
                p.ListaServicios = new List<ServicioProveedor>();
                //Con el DtoServicioProveedor que viene por parámetro crea un nuevo ServicioProveedor y lo agrega a la lista del Proveedor
                ServicioProveedor miServicio = new ServicioProveedor();
                miServicio.IdServicio = servicio.IdServicio;
                miServicio.RutProveedor = servicio.RutProveedor;
                miServicio.Descripcion = servicio.Descripcion;
                miServicio.Foto = servicio.Foto;
                ServicioProveedor.InsertarServicioProveedorSinTnr(miServicio);
                p.ListaServicios.Add(miServicio);
            }
            else if (prov.Tipo == "VIP")
            {
                //Construye un Proveedor
                Proveedor p = new ProveedorVIP();
                //Inicializa la lista de ServicioProveedor
                p.ListaServicios = new List<ServicioProveedor>();
                //Con el DtoServicioProveedor que viene por parámetro crea un nuevo ServicioProveedor y lo agrega a la lista del Proveedor
                ServicioProveedor miServicio = new ServicioProveedor();
                miServicio.IdServicio = servicio.IdServicio;
                miServicio.RutProveedor = servicio.RutProveedor;
                miServicio.Descripcion = servicio.Descripcion;
                miServicio.Foto = servicio.Foto;
                ServicioProveedor.InsertarServicioProveedorSinTnr(miServicio);
                p.ListaServicios.Add(miServicio);
            }
            return ret;
        }

        public bool AgregarUsuario(DtoUsuario usu, DtoProveedor prov)
        {
            //Construye Usuario a partir de DtoUsuario
            Usuario u = new Usuario();
            u.Email = usu.Email;
            u.Rol = usu.Rol;
            u.User = usu.User;

            //Construye Proveedor a partir de DtoProveedor
            if (prov.Tipo == "COMUN")
            {
                Proveedor p = new ProveedorComun();
                p.RUT = prov.RUT;
                p.NombreFantasia = prov.NombreFantasia;
                p.Email = prov.Email;
                p.Telefono = prov.Telefono;
                p.FechaRegistro = prov.FechaRegistro;
                p.esInactivo = prov.esInactivo;
                p.Tipo = "COMUN";

                return p.AgregarUsuario(u);
            }
            else if (prov.Tipo == "COMUN") {
                Proveedor p = new ProveedorComun();
                p.RUT = prov.RUT;
                p.NombreFantasia = prov.NombreFantasia;
                p.Email = prov.Email;
                p.Telefono = prov.Telefono;
                p.FechaRegistro = prov.FechaRegistro;
                p.esInactivo = prov.esInactivo;
                p.Tipo = "VIP";

                return p.AgregarUsuario(u);
            }
            return false;
        }

        public DtoServicio CargarServicios()
        {
            return Servicio.FindAll();
        }

        public bool ExisteMail(string rut)
        {
            return Proveedor.ExisteEmail(rut);
        }

        public bool ExisteRUT(string rut)
        {
            return Proveedor.ExisteEmail(rut);
        }

        public bool InsertarProveedor(string rut, string nombreFantasia, string email, string tel, bool esInactivo, bool esVip, string pass)
        {
            bool ret = false;
            DateTime fechaRegDateTime = DateTime.Now;
            string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");
            // Construyo un DtoProveedor con los parámetros que llegan desde el cliente y controlo el tipo de proveedor
            if (!esVip)
            {
                DtoProveedor p = new DtoProveedor()
                {
                    RUT = rut,
                    NombreFantasia = nombreFantasia,
                    Email = email,
                    Telefono = tel,
                    FechaRegistro = fechaRegistro,
                    esInactivo = esInactivo,
                    Tipo = "COMUN"
                };

                string passEncriptada = Usuario.EncriptarPassSHA512(pass);
                DtoUsuario usu = new DtoUsuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

                // Agrego el DtoUsuario al DtoProveedor p
                this.AgregarUsuario(usu, p);

                //Construye un Proveedor 
                Proveedor proveedorAInsertar = new ProveedorComun();
                proveedorAInsertar.RUT = p.RUT;
                proveedorAInsertar.NombreFantasia = p.NombreFantasia;
                proveedorAInsertar.Email = p.Email;
                proveedorAInsertar.Telefono = p.Telefono;
                proveedorAInsertar.FechaRegistro = p.FechaRegistro;
                proveedorAInsertar.esInactivo = p.esInactivo;
                proveedorAInsertar.Tipo = "COMUN";

                //Inserto el Proveedor en BD
                proveedorAInsertar.Insertar();

                ret = true;
            }
            else if (esVip)
            {
                DtoProveedor p = new DtoProveedor()
                {
                    RUT = rut,
                    NombreFantasia = nombreFantasia,
                    Email = email,
                    Telefono = tel,
                    FechaRegistro = fechaRegistro,
                    esInactivo = esInactivo,
                    Tipo = "VIP"
                };

                string passEncriptada = Usuario.EncriptarPassSHA512(pass);
                DtoUsuario usu = new DtoUsuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

                // Agrego el DtoUsuario al DtoProveedor p
                AgregarUsuario(usu, p);

                //Construye un Proveedor 
                Proveedor proveedorAInsertar = new ProveedorVIP();
                proveedorAInsertar.RUT = p.RUT;
                proveedorAInsertar.NombreFantasia = p.NombreFantasia;
                proveedorAInsertar.Email = p.Email;
                proveedorAInsertar.Telefono = p.Telefono;
                proveedorAInsertar.FechaRegistro = p.FechaRegistro;
                proveedorAInsertar.esInactivo = p.esInactivo;
                proveedorAInsertar.Tipo = "VIP";

                //Inserto el Proveedor en BD
                proveedorAInsertar.Insertar();

                ret = true;
            }
            return ret;
        }

   }
}

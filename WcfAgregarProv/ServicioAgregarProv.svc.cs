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
        public bool InsertarProveedor(string rut, string nombreFantasia, string email, string tel, bool esInactivo, bool esVip, string pass)
        {
            bool ret = false;
            DateTime fechaRegDateTime = DateTime.Now;
            string fechaRegistro = fechaRegDateTime.ToString("yyyy-MM-dd");
            // Construyo un proveedor con los parámetros que llegan desde el servicio y controlo el tipo de proveedor
            if (!esVip)
            {
                Proveedor p = new ProveedorComun()
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
                Usuario usu = new Usuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

                // Agrego el usuario al proveedor p
                p.AgregarUsuario(usu);
                p.Insertar();

                ret = true;
            }
            else if (esVip)
            {
                Proveedor p = new ProveedorVIP()
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
                Usuario usu = new Usuario { User = p.RUT, Passw = passEncriptada, Rol = 2, Email = p.Email };

                // Agrego el usuario al proveedor p
                p.AgregarUsuario(usu);
                p.Insertar();

                ret = true;
            }

            return ret;
        }

   }
}

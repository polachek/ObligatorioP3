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
        public bool InsertarProveedor(string rut, string nombreFantasia, string email, string tel, string fechaRegistro, bool esInactivo, string tipo, string pass)
        {
            bool ret = false;

            string miTipo = tipo;
            // Construyo un proveedor con los parámetros que llegan desde el servicio y controlo el tipo de proveedor
            if (tipo == "COMUN")
            {
                Proveedor p = new ProveedorComun()
                {
                    RUT = rut,
                    NombreFantasia = nombreFantasia,
                    Email = email,
                    Telefono = tel,
                    FechaRegistro = fechaRegistro,
                    esInactivo = esInactivo,
                    Tipo = tipo
                };

                // Encripto el password y construyo un usuario
                string passEncriptada = Usuario.EncriptarPassSHA512(pass);
                Usuario usu = new Usuario { User = rut, Passw = passEncriptada };

                // Agrego el usuario al proveedor p
                p.AgregarUsuario(usu);
                p.Insertar();
                ret = true;
            }
            else if (tipo == "VIP")
            {
                Proveedor p = new ProveedorVIP()
                {
                    RUT = rut,
                    NombreFantasia = nombreFantasia,
                    Email = email,
                    Telefono = tel,
                    FechaRegistro = fechaRegistro,
                    esInactivo = esInactivo,
                    Tipo = tipo
                };

                // Encripto el password y construyo un usuario
                string passEncriptada = Usuario.EncriptarPassSHA512(pass);
                Usuario usu = new Usuario { User = rut, Passw = passEncriptada };

                // Agrego el usuario al proveedor p
                p.AgregarUsuario(usu);
                p.Insertar();
                ret = true;
            }

            return ret;
        }
    }
}

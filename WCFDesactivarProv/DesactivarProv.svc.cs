using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WCFDesactivarProv
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DesactivarProv : IDesactivarProv
    {
        public bool DesactivarProveedor(DtoProveedor dtoProv)
        {
            return Proveedor.DesactivarProv(dtoProv.RUT);            
        }

        public DtoProveedor BuscarProveedorRut(string rut)
        {
            Proveedor miprov = Proveedor.FindByRUT(rut);
            miprov.ListaServicios = new List<ServicioProveedor>();
            if (miprov == null)
            {
                return null;
            }
            else
            {
                DtoProveedor miDtoProv = new DtoProveedor();

                miDtoProv.RUT = miprov.RUT;
                miDtoProv.NombreFantasia = miprov.NombreFantasia;
                miDtoProv.Email = miprov.Email;
                miDtoProv.Telefono = miprov.Telefono;
                miDtoProv.FechaRegistro = miprov.FechaRegistro;
                miDtoProv.esInactivo = miprov.esInactivo;
                miDtoProv.Tipo = miprov.Tipo;
                miDtoProv.ListaServicios = new List<DtoServicioProveedor>();

                foreach (ServicioProveedor s in miprov.ListaServicios)
                {
                    DtoServicioProveedor miDtoServicioProv = new DtoServicioProveedor();
                    miDtoServicioProv.IdServicio = s.IdServicio;
                    miDtoServicioProv.Nombre = s.Nombre;
                    miDtoServicioProv.RutProveedor = s.RutProveedor;
                    miDtoServicioProv.Descripcion = s.Descripcion;
                    miDtoServicioProv.Foto = s.Foto;

                    miDtoProv.ListaServicios.Add(miDtoServicioProv);
                }
                return miDtoProv;
            }
        }
    }
}

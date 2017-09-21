using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WcfListaProv
{
    public class ServicioListaProv : IServicioListaProv
    {
        public IEnumerable<DtoProveedor> ObtenerProveedores()
        {
            List<Proveedor> listaCompleta = Proveedor.FindAll();
            if (listaCompleta == null) return null;
            List<DtoProveedor> proveedores = new List<DtoProveedor>();
            foreach (Proveedor p in listaCompleta)
            {
                proveedores.Add(
                    new DtoProveedor()
                    {
                        NombreFantasia = p.NombreFantasia,
                        RUT = p.RUT,
                        Email = p.Email,
                        Telefono = p.Telefono,
                        FechaRegistro = p.FechaRegistro,
                        esInactivo = p.esInactivo,
                        Tipo = p.Tipo,
                   }
                );
            }
            return proveedores;
        }
    }
}

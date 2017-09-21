using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WCFProveedorDadoRUT
{
    public class ProveedorDadoRUT : IProveedorDadoRUT
    {
        public DtoProveedor buscarProveedorRut(string rut)
        {
            Proveedor miprov = Proveedor.FindByRUT(rut);

            if(miprov == null)
            {
                return null;
            }else
            {
                DtoProveedor miDtoProv = new DtoProveedor();

                miDtoProv.RUT = miprov.RUT;
                miDtoProv.NombreFantasia = miprov.NombreFantasia;
                miDtoProv.Email = miprov.Email;
                miDtoProv.Telefono = miprov.Telefono;
                miDtoProv.FechaRegistro = miprov.FechaRegistro;
                miDtoProv.esInactivo = miprov.esInactivo;
                miDtoProv.Tipo = miprov.Tipo;
                miDtoProv.ListaServicios = ServicioProveedor.FindServiciosProveedor(miDtoProv.RUT);

                return miDtoProv;
            }
        }
    }
}

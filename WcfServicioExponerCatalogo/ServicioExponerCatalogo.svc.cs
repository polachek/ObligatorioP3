using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WcfServicioExponerCatalogo
{
    public class ServicioExponerCatalogo : IServicioExponerCatalogo
    {
        public IEnumerable<DtoServicio> ObtenerServicios()
        {
            List<Servicio> listaCompleta = Servicio.FindAll();
            if (listaCompleta == null) return null;
            List<DtoServicio> servicios = new List<DtoServicio>();
            foreach (Servicio s in listaCompleta)
            {
                servicios.Add(
                    new DtoServicio()
                    {
                        Nombre = s.Nombre,
                        Descripcion = s.Descripcion
                    }
                );
            }
            return servicios;
        }
    }
}


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
                        Servicio = s.Nombre,
                        Descripcion = s.Descripcion,
                        Foto = s.Foto,
                        TipoEvento = s.ListaTipoEventos[0]
                    }
                );
            }
            return servicios;
        }

        /*
        public IEnumerable<DtoServicioYTiposEvento> ObtenerServiciosYTiposEvento()
        {
            List<Servicio> listaCompleta = Servicio.FindAll();
            List<DtoServicioYTiposEvento> serviciosYTiposEvento = new List<DtoServicioYTiposEvento>();
            return serviciosYTiposEvento;
        }
        */
    }
}


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
            List<Servicio> listaCompleta = Servicio.FindServicioTipo();
            if (listaCompleta == null) return null;
            List<DtoServicio> servicios = new List<DtoServicio>();
            /*foreach (Servicio s in listaCompleta)
            {
                List<TipoEvento> listaTipoEvento = Servicio.FindTiposEventoByServicio(s.Nombre);
                servicios.Add(
                    new DtoServicio()
                    {
                        Servicio = s.Nombre,
                        Descripcion = s.Descripcion,
                        Foto = s.Foto,
                        TipoEvento = listaTipoEvento

                    }
                );
            }*/
            return servicios;
        }
    }
}


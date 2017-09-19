using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WCFCatalogoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class CatalogoServicios : ICatalogoServicios
    {
        public IEnumerable<DtoServicio> ObtenerServicios()
        {
            List<Servicio> listaCompleta = Servicio.FindAll();
            if (listaCompleta == null) return null;
            List<DtoServicio> servicios = new List<DtoServicio>();

            List<String> milistaTipoEvento = new List<String>();

            foreach (Servicio s in listaCompleta)
            {
                
                List<TipoEvento> listaTipoEvento = Servicio.FindTiposEventoByServicio(s.Nombre);

                if (listaTipoEvento.Count() == 1)
                {
                    List<String> miListaString = new List<string>();
                    TipoEvento miTipoEv = listaTipoEvento[0];
                    miListaString.Add(miTipoEv.Nombre);

                    servicios.Add(
                    new DtoServicio()
                    {
                        IdServicio = s.IdServicio,
                        Servicio = s.Nombre,
                        Descripcion = s.Descripcion,
                        miTipoEvento = miListaString,
                    }
                   );
                }else
                {
                    List<String> miListaString = new List<string>();
                    foreach (TipoEvento elTipoEv in listaTipoEvento)
                    {
                        miListaString.Add(elTipoEv.Nombre);
                    }

                    servicios.Add(
                    new DtoServicio()
                    {
                        IdServicio = s.IdServicio,
                        Servicio = s.Nombre,
                        Descripcion = s.Descripcion,
                        miTipoEvento = miListaString,
                    }
                   );
                }

                
            }
            return servicios;
        }
    }
}

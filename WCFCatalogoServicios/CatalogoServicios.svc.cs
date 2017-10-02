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
        public IEnumerable<DtoServicio> AgregarServicios()
        {
            //Obtiene los servicios
            IEnumerable<Servicio> listaCompleta = Servicio.FindAll();
            List<DtoServicio> listaDtos = new List<DtoServicio>();
            if (listaCompleta != null)
            {
                foreach (Servicio s in listaCompleta)
                {
                    DtoServicio dtoServicio = new DtoServicio
                    {
                        IdServicio = s.IdServicio,
                        Servicio = s.Nombre,
                        Descripcion = s.Descripcion,
                        misTiposEventos = new List<DtoTipoEvento>()
                    };
                    listaDtos.Add(dtoServicio);
                }
            }
            return listaDtos;
        }

        public IEnumerable<DtoServicio> ObtenerServicios() {
            IEnumerable<DtoServicio> servicios = AgregarServicios();            
            return servicios;
        }

        public DtoServicio BuscarServicio(int id)
        {
            Servicio s = Servicio.FindById(id);
            if (s != null) {
                List<TipoEvento> tipos = Servicio.FindTiposEventoByServicio(s.Nombre);
                List<DtoTipoEvento> dtoTipos = new List<DtoTipoEvento>();
                foreach (TipoEvento t in tipos)
                {
                    DtoTipoEvento miTipo = new DtoTipoEvento
                    {
                        Nombre = t.Nombre,
                        Descripcion = t.Descripcion
                    };
                    dtoTipos.Add(miTipo);
                };
                DtoServicio miServicio = new DtoServicio
                {
                    IdServicio = id,
                    Servicio = s.Nombre,
                    Descripcion = s.Descripcion,
                    misTiposEventos = dtoTipos
                };
                return miServicio;
            }
            return null;
        }
    }
} 

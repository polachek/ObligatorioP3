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
    [ServiceContract]
    public interface IServicioExponerCatalogo
    {

        [OperationContract]
        IEnumerable<DtoServicio> ObtenerServicios();

        /*
        [OperationContract]
        IEnumerable<DtoServicioYTiposEvento> ObtenerServiciosYTiposEvento();
        */
    }

    
    [DataContract]
    public class DtoServicio
    {
        [DataMember]
        public string Servicio { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Foto { get; set; }

        [DataMember]
        public string TipoEvento { get; set; }
    }
    
    /*
    [DataContract]
    public class DtoServicioYTiposEvento
    {
        [DataMember]
        public string Servicio { get; set; }



        [DataMember]
        public string TipoEvento { get; set; }
    }
    */
}

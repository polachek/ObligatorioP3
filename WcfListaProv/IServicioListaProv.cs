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
    [ServiceContract]
    public interface IServicioListaProv
    {
        [OperationContract]
        IEnumerable<DtoProveedor> ObtenerProveedores();
    }

    [DataContract]
    public class DtoProveedor
    {
        [DataMember]
        public string RUT { get; set; }

        [DataMember]
        public string NombreFantasia { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string FechaRegistro { get; set; }

        [DataMember]
        public bool esInactivo { get; set; }

        [DataMember]
        public string Tipo { get; set; }

    }
}


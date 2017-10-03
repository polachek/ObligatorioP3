using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFDesactivarProv
{
    [ServiceContract]
    public interface IDesactivarProv
    {
        [OperationContract]
        bool DesactivarProveedor(DtoProveedor dtoProv);
        [OperationContract]
        DtoProveedor BuscarProveedorRut(string rut);
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
        [DataMember]
        public List<DtoServicioProveedor> ListaServicios { get; set; }
    }

    [DataContract]
    public class DtoServicioProveedor
    {
        [DataMember]
        public int IdServicio { get; set; }
        [DataMember]
        public string RutProveedor { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Foto { get; set; }
    }
}

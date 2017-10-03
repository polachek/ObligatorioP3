using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WcfAgregarProv
{
    [ServiceContract]
    public interface IAgregarProv
    {
        [OperationContract]
        bool InsertarProveedor(string rut, string nombreFantasia, string email, string tel, bool esInactivo, bool esVip, string pass);

        [OperationContract]
        bool AgregarUsuario(DtoUsuario usu, DtoProveedor prov);

        [OperationContract]
        bool AgregarServicioAProveedor(DtoServicioProveedor servicio, DtoProveedor prov);
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
        public Usuario MiUsuario { get; set; } = new Usuario();
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string FechaRegistro { get; set; }
        [DataMember]
        public bool esInactivo { get; set; }
        [DataMember]
        public static double Arancel { get; set; }
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

    [DataContract]
    public class DtoUsuario
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Passw { get; set; }
        [DataMember]
        public int Rol { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}

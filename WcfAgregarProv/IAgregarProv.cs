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
    }
}

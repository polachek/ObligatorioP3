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
        string desactivarProvRut(string Rut);

    }
}

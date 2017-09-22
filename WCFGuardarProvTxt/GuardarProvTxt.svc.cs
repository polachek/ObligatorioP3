using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFGuardarProvTxt
{

    public class GuardarProvTxt : IGuardarProvTxt
    {
        public string guardarProveedoresTxt()
        {
            if (Global.GrabarProveedores())
            {
                return "Se han guardado los Proveedores correctamente";
            }else
            {
                return "Error al guardar";
            }
            
        }
    }
}

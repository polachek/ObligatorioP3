using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;

namespace WcfServiceModificarParametros
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioModificarParametros : IServicioModificarParametros
    {
        public string ModificarArancel(double nuevoArancel)
        {            
            bool modificado = Proveedor.ModificarArancel(nuevoArancel);
            if (modificado)
                return string.Format("Ahora el arancel es de: ${0}", nuevoArancel);
            else
                return "No se modificó.";
        }

        public string ModificarPorcentajeExtra(int nuevoPorcentaje)
        {
            bool modificado = Proveedor.ModificarPorcentajeExtra(nuevoPorcentaje);
            if (modificado)
                return string.Format("Ahora el porcentaje extra es de: %{0}", nuevoPorcentaje);
            else
                return "No se modificó.";
        }
    }
}

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
        public bool ModificarArancel(double nuevoArancel)
        {
            return Proveedor.ModificarArancel(nuevoArancel);            
        }

        public bool ModificarPorcentajeExtra(int nuevoPorcentaje)
        {
            return Proveedor.ModificarPorcentajeExtra(nuevoPorcentaje);            
        }

        public decimal ObtenerArancel()
        {
            return Proveedor.ObtenerArancel();            
        }

        public int ObtenerPorcentajeExtra()
        {
            return Proveedor.ObtenerPorcentajeExtra();
        }
    }
}

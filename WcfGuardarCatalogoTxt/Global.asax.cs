using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Dominio;
using System.IO;

namespace WcfGuardarCatalogoTxt
{
    public class Global : System.Web.HttpApplication
    {

protected void Application_Start(object sender, EventArgs e)
        {
            string parametros = HttpRuntime.AppDomainAppPath + @"config\catalogo.txt";
            string directorio = HttpRuntime.AppDomainAppPath + @"config";

            if (File.Exists(parametros))
            {
            }else
            {
                if (Directory.Exists(directorio))
                {
                    FileStream fs = File.Create(parametros);
                }else
                {
                    Directory.CreateDirectory(directorio);
                    FileStream fs = File.Create(parametros);
                }
                
            }

        }

        public static bool GrabarCatalogo()
        {
            bool ret = false;

            string parametros = HttpRuntime.AppDomainAppPath + @"config\catalogo.txt";
            bool grabado = Servicio.grabarCatalogoTxt(parametros);
            if (grabado)
            {
                ret = true;
            }else
            {
                ret = false;
            }

            return ret;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
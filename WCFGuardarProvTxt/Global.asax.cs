using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Dominio;

namespace WCFGuardarProvTxt
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            string parametros = HttpRuntime.AppDomainAppPath + @"config\proveedores.txt";
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

        public static bool GrabarProveedores()
        {
            bool ret = false;

            string parametros = HttpRuntime.AppDomainAppPath + @"config\proveedores.txt";
                        
            if (Proveedor.grabarProveedoresTxt(parametros))
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
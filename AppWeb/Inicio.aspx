<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="AppWeb.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="stylesheet/main.css" rel="stylesheet" />
    <title>ProEventos</title>
</head>
<body class="">
  <div id="wrapper">
	<div id="bg"></div>
	<div id="overlay"></div>
	<div id="main">
      <header id="header">
		<h1>ProvEventos</h1>
		<p>Eventos &nbsp;&bull;&nbsp; Fotografía &nbsp;&bull;&nbsp; Discoteca &nbsp;&bull;&nbsp; Fiestas</p>
         <form id="form1" runat="server">
          <asp:Menu ID="MenuInicio" runat="server" Orientation="Horizontal" StaticSubMenuIndent="16px">
              <Items>
                  <asp:MenuItem Text="Catalogo de Servicios" Value="Catalogo de Servicios"></asp:MenuItem>
                  <asp:MenuItem Text="Iniciar Sesion" Value="Iniciar Sesion" NavigateUrl="Login.aspx"></asp:MenuItem> 
                  <asp:MenuItem Text="Registro de Proveedores" Value="Registro de Proveedores" NavigateUrl="WFRegProveedores.aspx"></asp:MenuItem>
              </Items>
          </asp:Menu>
         </form>
	  </header>

				<!-- Footer -->
					<footer id="footer">
						<span class="copyright">&copy; Guillermo Polachek - 153924 / Sebastián Villar - 177751</span>
					</footer>
        </div>
  </div>
</body>
</html>

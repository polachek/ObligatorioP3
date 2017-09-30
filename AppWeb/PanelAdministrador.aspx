<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" Inherits="AppWeb.PanelAdministrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
    <h1 class="main-title">Panel de administracion para Administradores</h1>
    <asp:Menu ID="Menu1" runat="server">
        <Items>
            <asp:MenuItem Text="WebForms" Value="WebForms">
                <asp:MenuItem NavigateUrl="~/Administrador/FormWeb-ListadoProveedores.aspx" Text="Listado de proveedores" Value="Listado de proveedores"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Servicios WCF" Value="Servicios WCF">
                <asp:MenuItem NavigateUrl="~/Inicio.aspx" Text="Listado de Proveedores" Value="ListadoProv"></asp:MenuItem>
            </asp:MenuItem>
        </Items>
    </asp:Menu>

    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Administrador/FormWeb-ListadoProveedores.aspx">Listado de proveedores</asp:HyperLink>
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Administrador/FormWeb-AgregarProveedor.aspx">Agregar proveedor</asp:HyperLink>
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Administrador/FormWeb-VerCatalogo.aspx">Ver catálogo</asp:HyperLink>
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Administrador/FormWeb-Desactivar.aspx">Desactivar proveedor</asp:HyperLink>
    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Administrador/FormWeb-Parametros.aspx">Modificar parámetros</asp:HyperLink>
    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Administrador/FormWeb-GuardarProveedor.aspx">Guardar datos de proveedor en archivo</asp:HyperLink>
    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Administrador/FormWeb-GuardarCatalogo.aspx">Guardar catálogo en archivo</asp:HyperLink>
</asp:Content>

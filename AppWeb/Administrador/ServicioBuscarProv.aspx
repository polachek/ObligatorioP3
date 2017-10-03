<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="~/Administrador/ServicioBuscarProv.aspx.cs" Inherits="AppWeb.ServicioBuscarProv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Buscar proveedor</h2>

    <asp:Panel ID="PanelBusqueda" runat="server">    
        <asp:TextBox ID="TBBuscarProv" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LnkBuscarProv" runat="server" OnClick="BuscarProv_Click" CssClass="btn btn-default">Buscar proveedor</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="PanelResultado" runat="server">
        <asp:Label ID="LblRUT" runat="server">
        </asp:Label>
        <asp:Label ID="LblNombre" runat="server">
        </asp:Label>
        <asp:Label ID="LblServicios" runat="server">
        </asp:Label>

    </asp:Panel>
</asp:Content>

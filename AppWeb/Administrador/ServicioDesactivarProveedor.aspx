<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="ServicioDesactivarProveedor.aspx.cs" Inherits="AppWeb.ServicioDesactivarProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h1>Desactivar proveedor</h1>
    <br />
    <br />
    <h2>Buscar Proveedor</h2>
    <asp:Panel ID="PanelBuscarProv" runat="server">
        <br />
        <asp:Label ID="LblBuscarProv" Text="Ingrese el rut del proveedor" runat="server"></asp:Label>
        <br />
        <asp:TextBox ID="TBBuscarProv" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="BtnBuscarProv" runat="server" Text="Buscar Proveedor" OnClick="BtnBuscarProv_Click" />
    </asp:Panel>
    <br />
    <br />
    <h2>Datos del proveedor</h2>
    <asp:Panel ID="PanelDatosProv" runat="server">
        <div class="paneldatos-proveedor">
            <h1 class="title-datos-proveedor">Datos de Proveedor</h1>
            <asp:Label ID="LBRUT" runat="server" Text="RUT: "></asp:Label>
            <asp:Label ID="LBNomFant" runat="server" Text="Nombre Fantasía: "></asp:Label>
            <asp:Label ID="LBEmail" runat="server" Text="Email: "></asp:Label>
            <asp:Label ID="LBTelefono" runat="server" Text="Telefono: "></asp:Label>
            <asp:Label ID="LBInactivo" runat="server" Text="Actividad: "></asp:Label>
            <asp:Label ID="LBVip" runat="server" Text="Vip: "></asp:Label>
            <asp:Label ID="Extra" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </asp:Panel>
    <br />
    <br />
    <h2>Desactivar proveedor</h2>
    <asp:Panel ID="PanelDesactivar" runat="server">
        <br />
        <asp:Label ID="Label1" Text="Confirme que desea desactivar al proveedor" runat="server"></asp:Label>
        <br />
        <asp:Button ID="BtnDesactivarProv" runat="server" Text="Desactivar" OnClick="BtnDesactivarProv_Click" />
    </asp:Panel>
    <asp:Panel ID="PanelConfirmacion" runat="server">
        <br />
        <asp:Label ID="LblConfirmacion" runat="server" Text="El proveedor fue desactivado">

        </asp:Label>
    </asp:Panel>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.Master" AutoEventWireup="true" CodeBehind="ServicioGuardarProv.aspx.cs" Inherits="AppWeb.ServicioGuardarProv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Guardar proveedores en archivo de texto</h2>
    <asp:Button ID="BtnGuardarProveedores" runat="server" Text="Guardar proveedores en archivo" OnClick="BtnGuardarProveedores_Click" />
    <br><br>
    <asp:Label ID="LblGuardado" runat="server" Text="Los datos de los proveedores se guardaron exitosamente.">
    </asp:Label>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="ServicioGuardarCatalogo.aspx.cs" Inherits="AppWeb.ServicioGuardarCatalogo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Guardar catálogo en archivo de texto</h2>
    <asp:Button ID="BtnGuardarCatalogo" runat="server" Text="Guardar catálogo en archivo" OnClick="BtnGuardarCatalogo_Click" />
    <br><br>
    <asp:Label ID="LblGuardado" runat="server" Text="El catálogo se guardó exitosamente.">
    </asp:Label>
</asp:Content>

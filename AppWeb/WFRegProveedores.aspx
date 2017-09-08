<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="WFRegProveedores.aspx.cs" Inherits="AppWeb.WFRegProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
    <asp:Button ID="BtnAsig" runat="server" Text="Intentar Asignacion" OnClick="BtnAsig_Click" />
    
    <asp:Button ID="BtnAsig2" runat="server" Text="Buscar por Rutn" OnClick="BtnAsig2_Click" />

    <br />
    <br />
    <br />

    <asp:Label ID="Asignacion" runat="server" Text="Label"></asp:Label>
</asp:Content>

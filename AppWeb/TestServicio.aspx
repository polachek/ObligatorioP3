<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="TestServicio.aspx.cs" Inherits="AppWeb.TestServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">

    <asp:Button ID="Button1" CssClass="boton_personalizado" runat="server" Text="Test Servicios" OnClick="Btnservicios_Click" />
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
</asp:Content>

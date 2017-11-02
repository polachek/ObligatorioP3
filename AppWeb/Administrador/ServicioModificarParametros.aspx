<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="ServicioModificarParametros.aspx.cs" Inherits="AppWeb.ServicioModificarParametros" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Modificar parámetros</h2>
    <p>Valores actuales</p>
    <asp:Label ID="LblArancel" runat="server" Text="Arancel actual"></asp:Label>
    <asp:Label ID="LblValorArancel" runat="server"></asp:Label>
    <br />
    <asp:Label ID="LblPorcentaje" runat="server" Text="Porcentaje extra actual"></asp:Label>
    <asp:Label ID="LblValorPorcentaje" runat="server"></asp:Label>
    <br />
    <br />
    <p>Actualizar valores</p>
    <asp:Label ID="LblNuevoArancel" runat="server" Text="Nuevo arancel"></asp:Label>
    <asp:TextBox ID="TBArancel" runat="server"></asp:TextBox>
    <asp:Button ID="BtnArancel" runat="server" Text="Actualizar arancel" OnClick="BtnArancel_Click" />
    <br />
    <asp:Label ID="LblNuevoPorcentaje" runat="server" Text="Nuevo porcentaje extra"></asp:Label>
    <asp:TextBox ID="TBPorcentaje" runat="server"></asp:TextBox>
    <asp:Button ID="BtnPorcentaje" runat="server" Text="Actualizar porcentaje" OnClick="BtnPorcentaje_Click" />
    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="ServicioListadoProv.aspx.cs" Inherits="AppWeb.ServicioListadoProv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Listado de proveedores</h2>
    <asp:GridView ID="GVProveedores" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField  DataField="RUT" HeaderText="RUT" />
            <asp:BoundField  DataField="NombreFantasia" HeaderText="Nombre fantasía" />
            <asp:BoundField  DataField="Email" HeaderText="Correo Electrónico" />
            <asp:BoundField  DataField="Telefono" HeaderText="Telefono" />
            <asp:BoundField  DataField="FechaRegistro" HeaderText="Fecha de registro" />
            <asp:BoundField  DataField="EsInactivo" HeaderText="Es inactivo" />
            <asp:BoundField  DataField="Tipo" HeaderText="Tipo" />
        </Columns>
    </asp:GridView>
</asp:Content>

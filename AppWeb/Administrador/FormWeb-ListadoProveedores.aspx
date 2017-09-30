<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="FormWeb-ListadoProveedores.aspx.cs" Inherits="AppWeb.FormWeb_ListadoProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoAdmin" runat="server">
     <div class="page-listado-proveedores">
    <h1>Listado de Proveedores</h1>

    <asp:Panel ID="Panel1" runat="server">

        <asp:GridView ID="GridViewListadoProveedores" CssClass="grid_View_Style_1" PagerStyle-CssClass="grid_1_pager"
 HeaderStyle-CssClass="grid_1_header" RowStyle-CssClass="grid_1_rows" runat="server" AutoGenerateColumns="False" OnRowCommand="GridProveedores_RowCommand">
         <Columns>
           <asp:BoundField DataField="RUT" HeaderText="RUT" />
           <asp:BoundField DataField="NombreFantasia" HeaderText="Nombre Fantasia" />
           <asp:ButtonField ButtonType="Link" CommandName="VerDatos" Text="Ver Datos" />
         </Columns>
         <SelectedRowStyle CssClass="grid_1_selectedrow" />
        </asp:GridView>
        <asp:Panel ID="PanelCantProveedores" runat="server" Visible="false">
        <asp:Label ID="Label7" runat="server" Text="No hay Proveedores registrados en el sistema."></asp:Label>
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="PanelDatos" runat="server" Visible="false">
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
         <h2>Listado de Servicios que ofrece</h2>
         <asp:GridView ID="GridViewServiciosProv"  runat="server" AutoGenerateColumns="False">
          <Columns>
           <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
           <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
           <asp:ImageField DataImageUrlField="Foto" HeaderText="Foto" ItemStyle-CssClass="img-servprov" />
          </Columns>
</asp:Content>

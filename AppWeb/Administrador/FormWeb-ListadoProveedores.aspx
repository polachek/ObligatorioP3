<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="FormWeb-ListadoProveedores.aspx.cs" Inherits="AppWeb.Administrador.FormWeb_ListadoProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
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
</asp:Content>

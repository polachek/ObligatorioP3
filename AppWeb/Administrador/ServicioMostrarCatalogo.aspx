<%@ Page Title="" Language="C#" MasterPageFile="~/Administrador/Administrador.master" AutoEventWireup="true" CodeBehind="~/Administrador/ServicioMostrarCatalogo.aspx.cs" Inherits="AppWeb.ServicioMostrarCatalogo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoAdmin" runat="server">
    <h2>Catálogo de servicios</h2>
    <asp:GridView ID="GVServicios" CssClass="grid_View_Style_1" PagerStyle-CssClass="grid_1_pager"
 HeaderStyle-CssClass="grid_1_header" RowStyle-CssClass="grid_1_rows" runat="server" AutoGenerateColumns="False" OnRowCommand="GVServicios_RowCommand">
        <Columns>
            <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:TemplateField  HeaderText="Tipo de evento">
                <ItemTemplate>
                    <asp:Button ID="BtnTipoEvento" runat="server" CommandName="VerTipoEvento" 
                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                          Text="Ver tipos de eventos" />
                </ItemTemplate> 
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Panel ID="PanelTipoEvento" runat="server">
        <asp:Label ID="LBlServicio" runat="server"></asp:Label><br />
        <asp:Label ID="LBlDescripcion" runat="server"></asp:Label><br />
        <asp:Label ID="LBlEventos" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>

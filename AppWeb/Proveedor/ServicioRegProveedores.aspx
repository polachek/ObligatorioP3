<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ServicioRegProveedores.aspx.cs" Inherits="AppWeb.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
<div class="page-regprov">
  <asp:Panel ID="Paso1AltaProv" runat="server">
   <div id="wpaltaproveedor">
      <h1 class="main-title">PASO 1 - DATOS DE PROVEEDOR</h1>
      <h1 class="main-title">Registro de Proveedores</h1>
    <asp:Panel ID="Panel2" runat="server">
      <asp:Label ID="Label4" runat="server" Text="Rut: "></asp:Label>
      <asp:TextBox ID="TxtRut" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtRut" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Tomato" ControlToValidate="TxtRut" ErrorMessage="Debe ser un numero de 12 digitos" ValidationExpression="^([0-9]{12})$"></asp:RegularExpressionValidator>
      <asp:Label ID="ErrorRut" runat="server" Text="" ForeColor="Tomato"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="Panel3" runat="server">
      <asp:Label ID="Label1" runat="server" Text="Nombre Fantasía: "></asp:Label>
      <asp:TextBox ID="TxtNomFantasia" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtNomFantasia" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Tomato" ControlToValidate="TxtNomFantasia" ErrorMessage="Mínimo 3 caracteres y máximo 50" ValidationExpression="^[a-zA-Z](\s?[a-zA-Z]){3,50}$"></asp:RegularExpressionValidator>
    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server">
      <asp:Label ID="Label2" runat="server" Text="Email: "></asp:Label>
      <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtEmail" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Tomato" ControlToValidate="TxtEmail" ErrorMessage="El formato de Email ingresado no es válido" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"></asp:RegularExpressionValidator>
    </asp:Panel>

    <asp:Panel ID="Panel4" runat="server">
      <asp:Label ID="Label3" runat="server" Text="Teléfono: "></asp:Label>
      <asp:TextBox ID="TxtTel" runat="server"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtTel" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Tomato" ControlToValidate="TxtTel" ErrorMessage="Solo numeros o caracteristica Uruguay - Formatos aceptados: XXXXXXX ó +598 XXXXXXX" ValidationExpression="(^[0-9]{8,9}$)|(^\+[0-9]{3}\s+[0-9]{2}\s+[0-9]{6}$)|(^\+[0-9]{3}\s+[0-9]{8,9}$)"></asp:RegularExpressionValidator>
    </asp:Panel>

    <asp:Panel ID="Panel5" runat="server">
      <asp:Label ID="Label5" runat="server" Text="Contraseña para su Usuario: "></asp:Label>
      <asp:TextBox ID="TxtPass" runat="server" TextMode="Password"></asp:TextBox>
      <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPass" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ForeColor="Tomato" ControlToValidate="TxtPass" ErrorMessage="Su contraseña debe tener un largo mínimo de 6, sin espacios y contener por lo menos una mayúscula" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z]).{6,}$"></asp:RegularExpressionValidator>
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel6" runat="server">
      <asp:CheckBox ID="CheckBoxVip" runat="server" Text="Proveedor Vip" />
    </asp:Panel>
    <br />

    <asp:Button ID="BtnAccion" CssClass="boton_personalizado" runat="server" Text="Ir a Paso 2 - Seleccion de Servicios" OnClick="BtnAccion_Click" />
      
    <br />
    <br />
    <asp:Label ID="Asignacion" runat="server" Text=""></asp:Label>
    </div>
  </asp:Panel>
  
  <asp:Panel ID="Paso2ServProv" runat="server" Visible="false">
    <div id="regprov-right">

        <h2>Servicios Ofrecidos</h2>
    <asp:ListBox ID="ListBoxServicios" runat="server"></asp:ListBox><br />
    <br />

      <h1 class="main-title">Seleccionar los Servicios ofrecidos</h1>
       <asp:GridView ID="GridViewListadoServicios" CssClass="grid_View_Style_1" PagerStyle-CssClass="grid_1_pager"
 HeaderStyle-CssClass="grid_1_header" RowStyle-CssClass="grid_1_rows" runat="server" AutoGenerateColumns="False" OnRowCommand="GridServicios_RowCommand">
         <Columns>
           <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
           <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
           <asp:ButtonField ButtonType="Link" CommandName="AgregarServicio" Text="Agregar Servicio" />
         </Columns>
         <SelectedRowStyle CssClass="grid_1_selectedrow" />
        </asp:GridView>
      <asp:panel id="PanelCantServicios" runat="server" Visible="false">
          <asp:Label ID="Label7" runat="server" Text="No hay Proveedores registrados en el sistema."></asp:Label>
      </asp:panel>

      <asp:Panel ID="PanelAsignarServicio" runat="server" Visible="false">
          <h2>Asignación de Servicio</h2>
          <asp:HiddenField ID="HiddeIdServicio" runat="server" />
          <asp:TextBox ID="ServNombre" runat="server" ReadOnly="true"></asp:TextBox><br />
          <asp:TextBox ID="ServDesc" runat="server"></asp:TextBox><br />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ServDesc" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ForeColor="Tomato" ControlToValidate="ServDesc" ErrorMessage="Mínimo 3 caracteres y máximo 50" ValidationExpression="^[a-zA-Z](\s?[a-zA-Z]){3,50}$"></asp:RegularExpressionValidator>
          <asp:FileUpload ID="ServFotoUpload" runat="server" /><br />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ServFotoUpload" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
          <br />
          <asp:Button ID="BtnAsigServ" CssClass="boton_personalizado" runat="server" Text="Asignar Servicio" OnClick="BtnAsigServAccion_Click" />
      </asp:Panel>

    </div>
          <asp:Button ID="BtnRegistroProv" CssClass="boton_personalizado" runat="server" Text="Registrar Proveedor" OnClick="BtnRegistroProv_Click" />
          <asp:Label ID="LBAsignacionReg" runat="server" Text=""></asp:Label>
  </asp:Panel>
  
 </div>
</asp:Content>

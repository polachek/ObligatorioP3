<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="WFRegProveedores.aspx.cs" Inherits="AppWeb.WFRegProveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
 <div class="page-regprov">
  <h1 class="main-title">Registro de Proveedores</h1>
  <div ID="wpAltaProveedor">

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

    <asp:Panel ID="Panel6" runat="server">
      <asp:CheckBox ID="CheckBoxVip" runat="server" Text="Proveedor Vip" />
    </asp:Panel>

    <asp:Button ID="BtnAccion" CssClass="boton_personalizado" runat="server" Text="Registrarse" OnClick="BtnAccion_Click" />

      
    <br />
    <br />
    <asp:Label ID="Asignacion" runat="server" Text=""></asp:Label>
  </div>

  <div id="regprov-right">

  </div>
 </div>
</asp:Content>

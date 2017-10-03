<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="ServicioRegProveedores.aspx.cs" Inherits="AppWeb.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadSitio" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceSitio" runat="server">
    <div class="page-regprov">
        <asp:Panel ID="PanelRegistroProv" runat="server">
            <div id="wpaltaproveedor">
            <h1 class="main-title">REGISTRO DE PROVEEDOR</h1>
            <h2 class="main-title">Datos del proveedor</h2>
            <asp:Panel ID="PanelRUT" runat="server">
                <asp:Label ID="LblRut" runat="server" Text="Rut: "></asp:Label>
                <asp:TextBox ID="TxtRut" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtRut" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="Tomato" ControlToValidate="TxtRut" ErrorMessage="Debe ser un numero de 12 digitos" ValidationExpression="^([0-9]{12})$"></asp:RegularExpressionValidator>
                <asp:Label ID="ErrorRut" runat="server" Text="" ForeColor="Tomato"></asp:Label>
                <br />
            </asp:Panel>

            <asp:Panel ID="PanelNombre" runat="server">
                <asp:Label ID="LblNombre" runat="server" Text="Nombre Fantasía: "></asp:Label>
                <asp:TextBox ID="TxtNomFantasia" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtNomFantasia" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Tomato" ControlToValidate="TxtNomFantasia" ErrorMessage="Mínimo 3 caracteres y máximo 50" ValidationExpression="^[a-zA-Z](\s?[a-zA-Z]){3,50}$"></asp:RegularExpressionValidator>
                <br />
            </asp:Panel>

            <asp:Panel ID="PanelEMail" runat="server">
                <asp:Label ID="LblEMail" runat="server" Text="Email: "></asp:Label>
                <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtEmail" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Tomato" ControlToValidate="TxtEmail" ErrorMessage="El formato de Email ingresado no es válido" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"></asp:RegularExpressionValidator>
            <br />
            </asp:Panel>

            <asp:Panel ID="PanelTelefono" runat="server">
                <asp:Label ID="LblTelefono" runat="server" Text="Teléfono: "></asp:Label>
                <asp:TextBox ID="TxtTel" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtTel" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ForeColor="Tomato" ControlToValidate="TxtTel" ErrorMessage="Solo numeros o caracteristica Uruguay - Formatos aceptados: XXXXXXX ó +598 XXXXXXX" ValidationExpression="(^[0-9]{8,9}$)|(^\+[0-9]{3}\s+[0-9]{2}\s+[0-9]{6}$)|(^\+[0-9]{3}\s+[0-9]{8,9}$)"></asp:RegularExpressionValidator>
            <br />
            </asp:Panel>

            <asp:Panel ID="PanelPass" runat="server">
                <asp:Label ID="LblPass" runat="server" Text="Contraseña para su Usuario: "></asp:Label>
                <asp:TextBox ID="TxtPass" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="TxtPass" ErrorMessage="*" ForeColor="#FF0000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ForeColor="Tomato" ControlToValidate="TxtPass" ErrorMessage="Su contraseña debe tener un largo mínimo de 6, sin espacios y contener por lo menos una mayúscula" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z]).{6,}$"></asp:RegularExpressionValidator>
            <br />
            </asp:Panel>
            
            <asp:Panel ID="PanelVIP" runat="server">
                <asp:CheckBox ID="CheckBoxVip" runat="server" Text="Proveedor Vip" />
            <br />
            </asp:Panel>
            <asp:Label ID="LblAsignacion" runat="server" Text=""></asp:Label>
            <br />      
            <br />
            <h2>Servicios Ofrecidos</h2>
            <asp:Label ID="LblServicios" runat="server" Text="Seleccione el tipo de servicio:"></asp:Label>
            <br />
            <asp:ListBox ID="ListBoxServicios" runat="server"></asp:ListBox>
            <br />
            <asp:Label ID="LblDescripcion" runat="server" Text="Describa el servicio ofrecido:"></asp:Label>
            <br />
            <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="LblFoto" runat="server" Text="Agregue una foto al servicio:"></asp:Label>
            <br />
            <asp:FileUpload ID="ServFotoUpload" runat="server" />
            <br />
            <br />
            <asp:Button ID="BtnAgregarServicio" runat="server" Text="AgregarServicio" CssClass="boton_personalizado"/>
            <br />
            <br />
            <asp:Button ID="BtnRegistroProv" CssClass="boton_personalizado" runat="server" Text="Registrar Proveedor" OnClick="BtnRegistroProv_Click" />
            <asp:Label ID="LBAsignacionReg" runat="server" Text=""></asp:Label>
        </div>
    </asp:Panel>
 </div>
</asp:Content>

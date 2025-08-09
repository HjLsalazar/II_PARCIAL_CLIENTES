<%@ Page Title="" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master"
    CodeBehind="CLIENTES.aspx.vb"
    Inherits="II_PARCIAL_CLIENTES.II_PARCIAL_CLIENTES.Clientes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Encabezado / usuario -->
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="h3">Gestión de Clientes</h2>
        <div>
            <asp:Label ID="lblUsuario" runat="server" CssClass="me-2 fw-semibold" />
            <asp:HyperLink ID="lnkLogout" runat="server" NavigateUrl="~/Logout.aspx" CssClass="btn-logout">
    Cerrar sesión
            </asp:HyperLink>


        </div>
    </div>

    <!-- Mensaje de error -->
    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger w-100" Visible="false" />



    <!-- Card del listado -->
    <div class="card shadow-sm mb-4">
        <div class="card-header">
            <h5 class="card-title mb-0">Listado</h5>
        </div>
        <div class="card-body">
            <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="False"
                DataKeyNames="ClienteId"
                OnSelectedIndexChanged="gvClientes_SelectedIndexChanged"
                OnRowDeleting="gvClientes_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ClienteId" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido1" HeaderText="Apellido 1" />
                    <asp:BoundField DataField="Apellido2" HeaderText="Apellido 2" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- Card del formulario -->
    <div class="card shadow-lg p-4" style="max-width: 900px;">
        <h5 class="mb-3">Formulario</h5>

        <!-- Hidden para saber si es UPDATE -->
        <asp:HiddenField ID="hfClienteId" runat="server" />

        <div class="row g-3">
            <div class="col-md-6">
                <div class="form-floating">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" />
                    <label for="MainContent_txtNombre">Nombre</label>
                </div>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                    ControlToValidate="txtNombre" CssClass="text-danger"
                    ErrorMessage="Nombre es obligatorio" Display="Dynamic" />
            </div>

            <div class="col-md-6">
                <div class="form-floating">
                    <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control" placeholder="Apellido 1" />
                    <label for="MainContent_txtApellido1">Apellido 1</label>
                </div>
                <asp:RequiredFieldValidator ID="rfvApellido1" runat="server"
                    ControlToValidate="txtApellido1" CssClass="text-danger"
                    ErrorMessage="Apellido 1 es obligatorio" Display="Dynamic" />
            </div>

            <div class="col-md-6">
                <div class="form-floating">
                    <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control" placeholder="Apellido 2" />
                    <label for="MainContent_txtApellido2">Apellido 2</label>
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-floating">
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Teléfono" />
                    <label for="MainContent_txtTelefono">Teléfono</label>
                </div>
                <asp:RequiredFieldValidator ID="rfvTelefono" runat="server"
                    ControlToValidate="txtTelefono" CssClass="text-danger"
                    ErrorMessage="Teléfono es obligatorio" Display="Dynamic" />
            </div>


            <div class="col-12">
                <div class="form-floating">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" />
                    <label for="MainContent_txtEmail">Email</label>
                </div>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail" CssClass="text-danger"
                    ErrorMessage="Email es obligatorio" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                    ControlToValidate="txtEmail" CssClass="text-danger"
                    ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                    ErrorMessage="Formato de Email inválido" Display="Dynamic" />
            </div>
        </div>

        <div class="d-flex gap-2 mt-3">
            <asp:Button ID="btnGuardar" runat="server" CssClass=" btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" CssClass=" btn-secondary" Text="Cancelar" CausesValidation="False" OnClick="btnCancelar_Click" />
        </div>
    </div>

</asp:Content>

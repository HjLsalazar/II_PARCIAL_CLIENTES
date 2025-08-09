<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LOGIN.aspx.vb" Inherits="II_PARCIAL_CLIENTES.LOGIN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      
    <div class="card shadow-lg p-4" style="max-width:400px;width:100%;">
          <div class="d-flex align-items-center py-4 bg-body-tertiary">
    <main class="form-signin w-100 m-auto">
        <h1 class="h3 mb-3 fw-normal">Iniciar Sesion</h1>

        <div class="form-floating">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Email"></asp:TextBox> 
            <label for="MainContent_txtEmail">Email </label>
        </div>

        <div class="form-floating">
            <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="SingleLine" placeholder="Contraseña"></asp:TextBox> 
            <label for="MainContent_txtContraseña">Contraseña</label>
        </div>

        <div class="form-check text-start my-3"> 
            <input class="form-check-input" type="checkbox" value="remember-me" id="flexCheckDefault">
            <label class="form-check-label" for="flexCheckDefault">
                Recordar
            </label>
        </div>
        <asp:Button CssClass="btn btn-primary w-100 py-2" ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
    </main>
</div>
<a href="Registro.aspx">¿Primera vez que ingresa?</a>
<asp:Label ID="lblError" runat="server" Text="" CssClass="alert alert-danger" Visible="false"></asp:Label>

</div>
</asp:Content>

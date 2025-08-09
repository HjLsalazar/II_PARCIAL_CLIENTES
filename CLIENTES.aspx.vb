Imports System
Imports System.Data
Imports System.Web.UI.WebControls

Namespace II_PARCIAL_CLIENTES
    Partial Public Class Clientes
        Inherits System.Web.UI.Page

        ' Repositorio para manejar la lógica de acceso a datos
        Private ReadOnly repo As New ClienteRepository()

        'Control de errores
        Protected lblError As Label
        Protected lblUsuario As Label
        Protected hfClienteId As HiddenField
        Protected txtNombre As TextBox
        Protected txtApellido1 As TextBox
        Protected txtApellido2 As TextBox
        Protected txtTelefono As TextBox
        Protected txtEmail As TextBox
        Protected gvClientes As GridView


        ' Evento de carga de la página
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Session("Usuario") Is Nothing Then
                Response.Redirect("~/LOGIN.aspx")
                Return
            End If

            lblUsuario.Text = "Usuario: " & Convert.ToString(Session("Usuario"))

            If Not IsPostBack Then
                CargarGrid()
            End If
        End Sub

        ' Método para cargar los datos en el GridView
        Private Sub CargarGrid()
            Try
                gvClientes.DataSource = repo.GetAll()
                gvClientes.DataBind()
                lblError.Visible = False
            Catch ex As Exception
                lblError.Text = "Error al cargar datos: " & ex.Message
                lblError.Visible = True
            End Try
        End Sub

        ' Evento para manejar la selección de una fila en el GridView
        Protected Sub gvClientes_SelectedIndexChanged(sender As Object, e As EventArgs)
            lblError.Visible = False
            Dim row As GridViewRow = gvClientes.SelectedRow

            ' Verificar si la fila seleccionada es válida
            If row Is Nothing Then Return
            hfClienteId.Value = gvClientes.SelectedDataKey.Value.ToString()
            txtNombre.Text = Server.HtmlDecode(row.Cells(1).Text)
            txtApellido1.Text = Server.HtmlDecode(row.Cells(2).Text)
            txtApellido2.Text = Server.HtmlDecode(row.Cells(3).Text)
            txtTelefono.Text = Server.HtmlDecode(row.Cells(4).Text)
            txtEmail.Text = Server.HtmlDecode(row.Cells(5).Text)
        End Sub

        ' Evento para manejar la eliminación de una fila en el GridView
        Protected Sub gvClientes_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
            lblError.Visible = False
            Try
                Dim id As Integer = Convert.ToInt32(e.Keys("ClienteId"))
                repo.Delete(id)
                CargarGrid()
                LimpiarFormulario()
            Catch ex As Exception
                lblError.Text = "No se pudo eliminar: " & ex.Message
                lblError.Visible = True
            End Try
            e.Cancel = True
        End Sub

        ' Evento para manejar la edición de una fila en el GridView
        Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
            ' Validar los campos del formulario
            lblError.Visible = False
            If Not Page.IsValid Then Return

            ' Validar campos obligatorios y formato de email
            If String.IsNullOrWhiteSpace(txtNombre.Text) OrElse
               String.IsNullOrWhiteSpace(txtApellido1.Text) OrElse
               String.IsNullOrWhiteSpace(txtTelefono.Text) Then
                lblError.Text = "Complete los campos obligatorios."
                lblError.Visible = True
                Return
            End If

            ' Validar formato de email
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, "^[^@\s]+@[^@\s]+\.[^@\s]+$") Then
                lblError.Text = "Email inválido."
                lblError.Visible = True
                Return
            End If

            ' Guardar o actualizar el cliente
            Try
                If String.IsNullOrEmpty(hfClienteId.Value) Then
                    repo.Insert(txtNombre.Text.Trim(), txtApellido1.Text.Trim(), txtApellido2.Text.Trim(),
                                txtTelefono.Text.Trim(), txtEmail.Text.Trim())
                Else
                    Dim id As Integer = Convert.ToInt32(hfClienteId.Value)
                    repo.Update(id, txtNombre.Text.Trim(), txtApellido1.Text.Trim(), txtApellido2.Text.Trim(),
                                txtTelefono.Text.Trim(), txtEmail.Text.Trim())
                End If
                CargarGrid()
                LimpiarFormulario()
            Catch ex As Exception
                lblError.Text = "Error al guardar: " & ex.Message
                lblError.Visible = True
            End Try
        End Sub

        ' Evento para manejar la cancelación de la edición
        Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
            lblError.Visible = False
            LimpiarFormulario()
            gvClientes.SelectedIndex = -1
        End Sub

        ' Método para limpiar el formulario
        Private Sub LimpiarFormulario()
            hfClienteId.Value = ""
            txtNombre.Text = ""
            txtApellido1.Text = ""
            txtApellido2.Text = ""
            txtTelefono.Text = ""
            txtEmail.Text = ""
        End Sub
    End Class
End Namespace

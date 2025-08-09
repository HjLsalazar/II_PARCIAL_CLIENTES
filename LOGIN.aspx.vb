Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class LOGIN
    Inherits System.Web.UI.Page

    ' evento para cargar la pagina 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ' Limpia cualquier sesión previa al mostrar el login
            Session.Clear()
        End If
    End Sub

    ' Click en "Ingresar"
    Protected Sub btnIngresar_Click(sender As Object, e As EventArgs)
        lblLoginError.Text = "" ' Limpia mensaje de error

        ' Toma valores del formulario
        Dim email As String = txtEmail.Text.Trim()
        Dim clave As String = txtClave.Text.Trim()

        ' Validaciones simples del lado servidor
        If String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(clave) Then
            lblLoginError.Text = "Ingrese email y contraseña."
            Return
        End If

        ' ADO.NET: validar credenciales
        Try
            Using cn As SqlConnection = DatabaseHelper.GetConnection()
                Using cmd As New SqlCommand("
                    SELECT TOP 1 Email
                    FROM Usuarios
                    WHERE Email=@Email AND Clave=@Clave", cn)

                    ' Parámetros para consulta segura
                    cmd.Parameters.AddWithValue("@Email", email)
                    cmd.Parameters.AddWithValue("@Clave", clave) ' *** En producción usar hashing ***

                    cn.Open()
                    Dim result = cmd.ExecuteScalar() ' Devuelve Email si existe

                    If result IsNot Nothing Then
                        ' Guarda el email en sesión y redirige a la página CRUD
                        Session("Usuario") = Convert.ToString(result)
                        Response.Redirect("~/Clientes.aspx")
                    Else
                        lblLoginError.Text = "Credenciales inválidas."
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Error controlado (muestra mensaje genérico)
            lblLoginError.Text = "Error al iniciar sesión: " & ex.Message
        End Try
    End Sub
End Class
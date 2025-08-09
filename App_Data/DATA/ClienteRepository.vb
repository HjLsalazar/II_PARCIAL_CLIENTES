Imports System.Data
Imports System.Data.SqlClient

Public Class ClienteRepository
    'instancia de la clase DatabaseHelper para manejar la conexión a la base de datos y registrar errores
    Private ReadOnly _db As DatabaseHelper

    'constructor que inicializa la instancia de DatabaseHelper
    Public Sub New()
        _db = New DatabaseHelper()
    End Sub

    ' Método para insertar un nuevo cliente en la base de datos
    Public Function GetAll() As DataTable
        ' Consulta simple ordenada por PK
        Dim sql As String =
            "SELECT ClienteId, Nombre, Apellido1, Apellido2, Telefono, Email " &
            "FROM Clientes " &
            "ORDER BY ClienteId;"

        ' DatabaseHelper.ExecuteQuery ya abre/cierra y maneja errores
        Return _db.ExecuteQuery(sql)
    End Function

    Public Function Insert(nombre As String, apellido1 As String, apellido2 As String, telefono As String, email As String) As Integer

        ' OUTPUT INSERTED.ClienteId permite obtener el ID sin otra consulta
        Dim sql As String =
            "INSERT INTO Clientes (Nombre, Apellido1, Apellido2, Telefono, Email) " &
            "OUTPUT INSERTED.ClienteId " &
            "VALUES (@Nombre, @Apellido1, @Apellido2, @Telefono, @Email);"

        ' Si apellido2 viene vacío, mandamos Nothing para que el helper lo convierta a DBNull
        Dim p As New List(Of SqlParameter) From {
            _db.CreateParameter("@Nombre", nombre),
            _db.CreateParameter("@Apellido1", apellido1),
            _db.CreateParameter("@Apellido2", If(String.IsNullOrWhiteSpace(apellido2), Nothing, apellido2)),
            _db.CreateParameter("@Telefono", telefono),
            _db.CreateParameter("@Email", email)
        }

        ' ExecuteQuery devuelve un DataTable; tomamos la primera celda como nuevo ID
        Dim dt As DataTable = _db.ExecuteQuery(sql, p)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return Convert.ToInt32(dt.Rows(0)(0))
        End If

        Return 0 ' Si no devolvió filas, regresamos 0 (no insertado)
    End Function

    ' Método para actualizar un cliente existente
    Public Sub Update(idCliente As Integer, nombre As String, apellido1 As String, apellido2 As String, telefono As String, email As String)
        Dim sql As String =
            "UPDATE Clientes " &
            "SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2, Telefono = @Telefono, Email = @Email " &
            "WHERE ClienteId = @ClienteId;"
        Dim p As New List(Of SqlParameter) From {
            _db.CreateParameter("@ClienteId", idCliente),
            _db.CreateParameter("@Nombre", nombre),
            _db.CreateParameter("@Apellido1", apellido1),
            _db.CreateParameter("@Apellido2", If(String.IsNullOrWhiteSpace(apellido2), Nothing, apellido2)),
            _db.CreateParameter("@Telefono", telefono),
            _db.CreateParameter("@Email", email)
        }
        _db.ExecuteNonQuery(sql, p)
    End Sub

    ' Método para eliminar un cliente por su ID
    Public Sub Delete(idCliente As Integer)
        Dim sql As String =
            "DELETE FROM Clientes " &
            "WHERE ClienteId = @ClienteId;"
        Dim p As New List(Of SqlParameter) From {
            _db.CreateParameter("@ClienteId", idCliente)
        }
        _db.ExecuteNonQuery(sql, p)
    End Sub

    ' Método para verificar si un email ya existe, excluyendo un ID específico (para actualizaciones)
    Public Function ExistsEmail(email As String, Optional excludeId As Integer = 0) As Boolean
        Dim sql As String =
            "SELECT COUNT(1) AS Cnt " &
            "FROM Clientes " &
            "WHERE Email = @Email AND (@ExcludeId = 0 OR ClienteId <> @ExcludeId);"

        Dim p As New List(Of SqlParameter) From {
            _db.CreateParameter("@Email", email),
            _db.CreateParameter("@ExcludeId", excludeId)
        }

        Dim dt As DataTable = _db.ExecuteQuery(sql, p)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim cnt As Integer = Convert.ToInt32(dt.Rows(0)("Cnt"))
            Return cnt > 0
        End If
        Return False
    End Function



End Class


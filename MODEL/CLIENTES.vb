Imports System

'clase CLIENTES
Public Class CLIENTES
    Inherits PERSONAS

    Private _idCliente As Integer

    ' Constructor vacío
    Public Sub New()

    End Sub

    ' Constructor con parámetros
    Public Sub New(idCliente As Integer, nombre As String, apellido1 As String, apellido2 As String, telefono As String, email As String)
        MyBase.New(nombre, apellido1, apellido2, telefono, email)
        Me.IdCliente = idCliente
    End Sub

    ' Propiedades públicas para acceder a los campos privados
    Public Property IdCliente As Integer
        Get
            Return _idCliente
        End Get
        Set(value As Integer)
            _idCliente = value
        End Set
    End Property

End Class

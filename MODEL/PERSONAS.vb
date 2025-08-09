Imports System

' clase PERSONAS
Public Class PERSONAS
    Private _nombre As String
    Private _apellido1 As String
    Private _apellido2 As String
    Private _telefono As String
    Private _email As String

    'constructor vacío
    Public Sub New()

    End Sub

    'constructor con parámetros
    Public Sub New(nombre As String, apellido1 As String, apellido2 As String, telefono As String, email As String)
        Me.Nombre = nombre
        Me.Apellido1 = apellido1
        Me.Apellido2 = apellido2
        Me.Telefono = telefono
        Me.Email = email
    End Sub

    'propiedades públicas para acceder a los campos privados
    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
        End Set
    End Property

    Public Property Apellido1 As String
        Get
            Return _apellido1
        End Get
        Set(value As String)
            _apellido1 = value
        End Set
    End Property

    Public Property Apellido2 As String
        Get
            Return _apellido2
        End Get
        Set(value As String)
            _apellido2 = value
        End Set
    End Property

    Public Property Telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property

    Public Property Email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set
    End Property




End Class

Imports System.Data
Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System

Public Class Grupo : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Propiedades Privadas "
    Private _idGrupo As Integer
    Private _Nombre As String
    Private _Descripcion As String
    Private _idUsuario As Integer
    Private _Vid As String
#End Region

#Region " Propiedades Publicas "
    Public Property Vid As String
        Get
            Return _Vid
        End Get
        Set(value As String)
            _Vid = value
        End Set
    End Property

    Public Property idGrupo() As Integer
        Get
            Return _idGrupo
        End Get
        Set(ByVal value As Integer)
            _idGrupo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public Property IdUsuario() As Integer
        Get
            Return _idUsuario
        End Get
        Set(ByVal value As Integer)
            _idUsuario = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(value As String)
            _Descripcion = value
        End Set
    End Property

#End Region

#Region " Constructores "
    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _idUsuario = _PaginaActual.Session("IdUsuario")
        _UsuarioLogin = _PaginaActual.Session("Usuario")

        If (_ip = String.Empty) Then
            _ip = Pagina.Request.ServerVariables("REMOTE_ADDR")
        End If
    End Sub

    Public Sub New(ByVal Pagina As Page, ByVal IdPais As String)
        MyBase.New(IdPais)

        _IdPaisLogin = IdPais
        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _idUsuario = _PaginaActual.Session("IdUsuario")
        _UsuarioLogin = _PaginaActual.Session("Usuario")

        If (_ip = String.Empty) Then
            _ip = Pagina.Request.ServerVariables("REMOTE_ADDR")
        End If

        BD = New OdinDataContext(getCadenaConexionPais(IdPais, False))
    End Sub

    Public Sub New(ByVal IdPais As String)
        MyBase.New(IdPais)

        _IdPaisLogin = IdPais

        BD = New OdinDataContext(getCadenaConexionPais(IdPais, False))
    End Sub
#End Region

    Public Overrides Function Ejecutar(Accion As String) As Boolean
        Console.WriteLine("Ejecutar")
    End Function

    Public Overrides Function Consultar(Optional Codigo As String = "") As DataSet
        Try
            Call InicializarConexion()
            Call InicializarDatos()

            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)

            If Codigo = "" Then
                Me.AdicionarParametros("@IdGrupo", 0)
            Else
                Me.AdicionarParametros("@IdGrupo", Codigo)
            End If
            Me.AdicionarParametros("@Vid", _Vid)

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "[spGrupoListar]",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

End Class

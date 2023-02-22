Imports System.Data
Imports System.Text
Imports System.Collections
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration.ConfigurationManager
Imports System.Web.UI
Imports System

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ApplicationServices

Public Class ActivoAlerta : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Propiedades Privadas "
    Private _IdAlerta As Integer
    Private _IdUsuario As Integer
    Private _IdActivo As String
    Private _Activo As String
    Private _SMS As String
    Private _Email As String
    Private _Estado As String
#End Region

#Region " Propiedades Publicas "
    Public Property idUsuario() As Integer
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Integer)
            _IdUsuario = value
        End Set
    End Property

    Public Property IdAlerta() As Integer
        Get
            Return _IdAlerta
        End Get
        Set(ByVal value As Integer)
            _IdAlerta = value
        End Set
    End Property

    Public Property IdActivo() As String
        Get
            Return _IdActivo
        End Get
        Set(ByVal value As String)
            _IdActivo = value
        End Set
    End Property

    Public Property Activo() As String
        Get
            Return _Activo
        End Get
        Set(ByVal value As String)
            _Activo = value
        End Set
    End Property

    Public Property SMS() As String
        Get
            Return _SMS
        End Get
        Set(ByVal value As String)
            _SMS = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return _Estado
        End Get
        Set(ByVal value As String)
            _Estado = value
        End Set
    End Property
#End Region

#Region " Constructores "

    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _UsuarioLogin = PaginaActual.Session("Usuario")
        _IdUsuario = PaginaActual.Session("IdUsuario")

        If (_ip = String.Empty) Then
            _ip = Pagina.Request.ServerVariables("REMOTE_ADDR")
        End If
    End Sub

    Public Sub New(ByVal Pagina As Page, ByVal IdPais As String)
        MyBase.New(IdPais)

        _IdPaisLogin = IdPais
        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _IdUsuario = _PaginaActual.Session("IdUsuario")
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

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            If Codigo = "" Then
                Codigo = 0
            End If
            Me.AdicionarParametros("@IdAlerta", Codigo)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spActivoD_AlertaConsultar",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function Consultar2(ByVal Activo As Integer, ByVal Codigo As Integer) As Integer
        Try
            Dim adp As SqlDataAdapter
            Try
                Call InicializarDatos()
                Call InicializarConexion()
                adp = New SqlDataAdapter("SELECT * FROM dbo.ActivoD_Alerta with (nolock) WHERE IdActivo=" & Activo & " AND IdAlerta=" & Codigo & "", objConexion)

                adp.SelectCommand.CommandTimeout = 4500000
                adp.Fill(Datos, "PuntosU")
                Datos.AcceptChanges()

                Return Datos.Tables(0).Rows.Count
            Catch ex As SqlException
                Throw ex
                Return 0
            Finally
                Call TerminarConexion()
            End Try
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Try
            Me.LimpiarParametros()
            AdicionarParametros("@IdActivo", Me._IdActivo)
            Select Case Accion
                Case "ING", "MOD"
                    AdicionarParametros("@IdAlerta", Me.IdAlerta)
                    AdicionarParametros("@EmailsAlerta", Me._Email)
                    AdicionarParametros("@SMSAlerta", Me._SMS)

                    If Accion = "MOD" Then
                        Me.Procedimiento = "[spActivoD_AlertaActualizar]"

                        AdicionarParametros("@UsuarioModificacion", Me.UsuarioLogin)
                    Else
                        Me.Procedimiento = "[spActivoD_AlertaIngresar]"

                        AdicionarParametros("@UsuarioIngreso", Me.UsuarioLogin)
                    End If
                Case "ELM"
                    AdicionarParametros("@IdAlerta", Me._IdAlerta)
                    AdicionarParametros("@UsuarioEliminacion", Me._UsuarioLogin)

                    Me.Procedimiento = "[spActivoD_AlertaEliminar]"
            End Select

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                              CommandType.StoredProcedure,
                              Me.Procedimiento,
                              Me.Parametros)
        Catch ex As Exception
            Throw ex
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarAlertasActivo() As Integer
        Try
            Consultar()

            Try
                Return Datos.Tables(0).Rows.Count
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Public Function ContarAlertasActivo2(ByVal activo As Integer, Optional ByVal codigo As String = "") As Integer
        Try
            Consultar2(activo, codigo)

            Try
                Return Datos.Tables(0).Rows.Count
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

End Class

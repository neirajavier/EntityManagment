Imports System.Data
Imports System.Text
Imports System.Collections
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration.ConfigurationManager
Imports System
Imports System.Web.UI
Imports Microsoft.VisualBasic
Imports System.Exception

Public Class Conductor : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Variables Privadas "
    Private _Identificacion As String
    Private _NuevaIdentificacion As String
    Private _Nombres As String
    Private _Apellidos As String
    Private _Genero As String
    Private _Altura As Double
    Private _TipoSangre As String
    Private _NumeroSeguroSocial As String
    Private _RestriccionLicencia As String
    Private _FechaExpiracionLicencia As String
    Private _PolizaSeguro As String
    Private _IdCiaSeguro As Integer
    Private _FechaExpiracionSeguro As String
    Private _DireccionDomicilio As String
    Private _TelefonoDomicilio As String
    Private _TelefonoCelular As String
    Private _Email As String
    Private _FechaIngreso As String
    Private _UsuarioIngreso As String
    Private _DadoBaja As Boolean
    Private _MotivoBaja As String
    Private _FechaBaja As String
    Private _idUsuario As Integer
    Private _DriverID As String
    Private _ContactoEmergencia As String
    Private _NumeroEmergencia As String
    Private _TipoLicencia As String
    Private _AlertarVencimiento As Boolean

#End Region

#Region " Propiedades Publicas "
    Public Property AlertarVencimiento As Boolean
        Get
            Return _AlertarVencimiento
        End Get
        Set(value As Boolean)
            _AlertarVencimiento = value
        End Set
    End Property

    Public Property TipoLicencia() As String
        Get
            Return _TipoLicencia
        End Get
        Set(ByVal value As String)
            _TipoLicencia = value
        End Set
    End Property

    Public Property ContactoEmergencia() As String
        Get
            Return _ContactoEmergencia
        End Get
        Set(ByVal value As String)
            _ContactoEmergencia = value
        End Set
    End Property

    Public Property NumeroEmergencia() As String
        Get
            Return _NumeroEmergencia
        End Get
        Set(ByVal value As String)
            _NumeroEmergencia = value
        End Set
    End Property

    Public Property DriverID() As String
        Get
            Return _DriverID
        End Get
        Set(ByVal value As String)
            _DriverID = value
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

    Public Property Identificacion() As String
        Get
            Return _Identificacion
        End Get
        Set(ByVal value As String)
            _Identificacion = value
        End Set
    End Property

    Public Property NuevaIdentificacion() As String
        Get
            Return _NuevaIdentificacion
        End Get
        Set(ByVal value As String)
            _NuevaIdentificacion = value
        End Set
    End Property

    Public Property Nombres() As String
        Get
            Return _Nombres
        End Get
        Set(ByVal value As String)
            _Nombres = value
        End Set
    End Property

    Public Property Apellidos() As String
        Get
            Return _Apellidos
        End Get
        Set(ByVal value As String)
            _Apellidos = value
        End Set
    End Property

    Public Property Genero() As String
        Get
            Return _Genero
        End Get
        Set(ByVal value As String)
            _Genero = value
        End Set
    End Property

    Public Property Altura() As Double
        Get
            Return _Altura
        End Get
        Set(ByVal value As Double)
            _Altura = value
        End Set
    End Property

    Public Property TipoSangre() As String
        Get
            Return _TipoSangre
        End Get
        Set(ByVal value As String)
            _TipoSangre = value
        End Set
    End Property

    Public Property NumeroSeguroSocial() As String
        Get
            Return _NumeroSeguroSocial
        End Get
        Set(ByVal value As String)
            _NumeroSeguroSocial = value
        End Set
    End Property

    Public Property RestriccionLicencia() As String
        Get
            Return _RestriccionLicencia
        End Get
        Set(ByVal value As String)
            _RestriccionLicencia = value
        End Set
    End Property

    Public Property FechaExpiracionLicencia() As String
        Get
            Return _FechaExpiracionLicencia
        End Get
        Set(ByVal value As String)
            _FechaExpiracionLicencia = value
        End Set
    End Property

    Public Property DireccionDomicilio() As String
        Get
            Return _DireccionDomicilio
        End Get
        Set(ByVal value As String)
            _DireccionDomicilio = value
        End Set
    End Property

    Public Property TelefonoDomicilio() As String
        Get
            Return _TelefonoDomicilio
        End Get
        Set(ByVal value As String)
            _TelefonoDomicilio = value
        End Set
    End Property

    Public Property TelefonoCelular() As String
        Get
            Return _TelefonoCelular
        End Get
        Set(ByVal value As String)
            _TelefonoCelular = value
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

    Public Property FechaIngreso() As String
        Get
            Return _FechaIngreso
        End Get
        Set(ByVal value As String)
            _FechaIngreso = value
        End Set
    End Property

    Public Property UsuarioIngreso() As String
        Get
            Return _UsuarioIngreso
        End Get
        Set(ByVal value As String)
            _UsuarioIngreso = value
        End Set
    End Property

    Public Property DadoBaja() As Boolean
        Get
            Return _DadoBaja
        End Get
        Set(ByVal value As Boolean)
            _DadoBaja = value
        End Set
    End Property

    Public Property MotivoBaja() As String
        Get
            Return _MotivoBaja
        End Get
        Set(ByVal value As String)
            _MotivoBaja = value
        End Set
    End Property

    Public Property FechaBaja() As String
        Get
            Return _FechaBaja
        End Get
        Set(ByVal value As String)
            _FechaBaja = value
        End Set
    End Property

    Public Property IdCiaSeguro() As Integer
        Get
            Return _IdCiaSeguro
        End Get
        Set(ByVal value As Integer)
            _IdCiaSeguro = value
        End Set
    End Property

    Public Property PolizaSeguro() As String
        Get
            Return _PolizaSeguro
        End Get
        Set(ByVal value As String)
            _PolizaSeguro = value
        End Set
    End Property

    Public Property FechaExpiracionSeguro() As String
        Get
            Return _FechaExpiracionSeguro
        End Get
        Set(ByVal value As String)
            _FechaExpiracionSeguro = value
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

    Public Sub New()
        MyBase.New()
    End Sub

#End Region

    Public Function ListarConductores(Optional ByVal Codigo As String = "") As DataSet
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()
            Me.AdicionarParametros("@idUsuario", _idUsuario)

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                          CommandType.StoredProcedure,
                                          "spConductoresConsultar",
                                          Me.Parametros)
            Return Datos
        Catch ex As Exception

        End Try
    End Function

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()

            Me.AdicionarParametros("@idUsuario", _idUsuario)
            Me.AdicionarParametros("@Identificacion", Codigo)
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spConductorConsultar",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Select Case Accion

                Case "ING", "MOD"
                    AdicionarParametros("@Identificacion", Me._Identificacion)
                    AdicionarParametros("@Nombre", Me._Nombres)
                    AdicionarParametros("@Apellido", Me._Apellidos)
                    AdicionarParametros("@Genero", Me._Genero)
                    AdicionarParametros("@Altura", Me._Altura)
                    AdicionarParametros("@TipoSangre", Me._TipoSangre)
                    AdicionarParametros("@NumeroSeguroSocial", Me._NumeroSeguroSocial)
                    AdicionarParametros("@RestriccionLicencia", Me._RestriccionLicencia)
                    AdicionarParametros("@FechaExpiracionLicencia", Me._FechaExpiracionLicencia)
                    AdicionarParametros("@PolizaSeguro", Me._PolizaSeguro)
                    AdicionarParametros("@IdCiaSeguro", Me._IdCiaSeguro)
                    AdicionarParametros("@FechaExpiracionSeguro", Me._FechaExpiracionSeguro)
                    AdicionarParametros("@DireccionDomicilio", Me._DireccionDomicilio)
                    AdicionarParametros("@TelefonoDomicilio", Me._TelefonoDomicilio)
                    AdicionarParametros("@TelefonoCelular", Me._TelefonoCelular)
                    AdicionarParametros("@Email", Me._Email)
                    AdicionarParametros("@DriverID", Me._DriverID)
                    AdicionarParametros("@ContactoEmergencia", Me._ContactoEmergencia)
                    AdicionarParametros("@NumeroEmergencia", Me._NumeroEmergencia)
                    AdicionarParametros("@TipoLicencia", Me._TipoLicencia)
                    AdicionarParametros("@AlertarVencimiento", IIf(Me._AlertarVencimiento, 1, 0))

                    If Accion = "MOD" Then
                        AdicionarParametros("@NuevaIdentificacion", Me._NuevaIdentificacion)
                        AdicionarParametros("@UsuarioModificacion", Me._idUsuario)
                        Me.Procedimiento = "spConductorActualizar"
                    Else
                        AdicionarParametros("@UsuarioIngreso", Me._idUsuario)
                        Me.Procedimiento = "spConductorIngresar"
                    End If
                Case "ELM"
                    AdicionarParametros("@Identificacion", Me._Identificacion)
                    AdicionarParametros("@IdUsuario", Me._idUsuario)
                    Me.Procedimiento = "spConductorEliminar"
                Case "BAJ"
                    'AdicionarParametros("@Identificacion", _Identificacion)
                    'AdicionarParametros("@MotivoBaja", _MotivoBaja)
                    'AdicionarParametros("@Usuario", _UsuarioIngreso)
                    'Me.Procedimiento = "spConductorDarBaja"
                    AdicionarParametros("@parIdentificacion", Me._Identificacion)
                    AdicionarParametros("@parMotivoBaja", Me._MotivoBaja)
                    AdicionarParametros("@parUsuario", Me._UsuarioIngreso)
                    AdicionarParametros("@parIdUsuario", Me._idUsuario)
                    Me.Procedimiento = "spConductorDarBajaV2"
            End Select

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

    Public Function ContarConductores(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM Conductor with (nolock) WHERE UsuarioIngreso = {0}", IdUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

End Class

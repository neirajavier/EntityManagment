Imports System.Data
Imports System.Text
Imports System.Collections
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration.ConfigurationManager
Imports System.Web.UI
Imports System
Imports Microsoft.VisualBasic
Imports System.Data.OleDb

Public Class Alerta : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Propiedades Privadas "
    Private _IdAlerta As Integer
    Private _IdUsuario As Integer
    Private _Nombre As String
    Private _Descripcion As String
    Private _IdTipoDispositivo As Integer
    Private _IdTipoEvento As Integer
    Private _Evento As Integer
    Private _IdGeocerca As Integer
    Private _Kilometraje As Double
    Private _Porcentaje As Double
    Private _Estado As String
    Private _IdTipoAlerta As Integer
    Private _IdProducto As String
    Private _HoraDesde As String
    Private _HoraHasta As String
    Private _LimiteVelocidad As Double
    Private _DentroGeo As Boolean
    Private _IdDespacho As String
    Private _IdGrupoGeocerca As Integer
    Private _Lunes As Boolean
    Private _Martes As Boolean
    Private _Miercoles As Boolean
    Private _Jueves As Boolean
    Private _Viernes As Boolean
    Private _Sabado As Boolean
    Private _Domingo As Boolean
    Private _idSonido As Integer

    Private _CampoValorControl As Integer
    Private _TipoControl As Integer
    Private _FormaControl As Integer
    Private _ParametroControl1 As Double
    Private _ParametroControl2 As Double
    Private _IdSuceso As Integer
    Private _FechaCaducidad As String

#End Region

#Region " Propiedades Publicas "
    Public Property IdSonido As Integer
        Get
            Return _idSonido
        End Get
        Set(value As Integer)
            _idSonido = value
        End Set
    End Property

    Public Property FechaCaducidad As String
        Get
            Return _FechaCaducidad
        End Get
        Set(value As String)
            _FechaCaducidad = value
        End Set
    End Property

    Public Property IdSuceso As Integer
        Get
            Return _IdSuceso
        End Get
        Set(value As Integer)
            _IdSuceso = value
        End Set
    End Property

    Public Property ParametroControl2 As Double
        Get
            Return _ParametroControl2
        End Get
        Set(value As Double)
            _ParametroControl2 = value
        End Set
    End Property

    Public Property ParametroControl1 As Double
        Get
            Return _ParametroControl1
        End Get
        Set(value As Double)
            _ParametroControl1 = value
        End Set
    End Property

    Public Property FormaControl As Integer
        Get
            Return _FormaControl
        End Get
        Set(value As Integer)
            _FormaControl = value
        End Set
    End Property

    Public Property TipoControl As Integer
        Get
            Return _TipoControl
        End Get
        Set(value As Integer)
            _TipoControl = value
        End Set
    End Property

    Public Property CampoValorControl As Integer
        Get
            Return _CampoValorControl
        End Get
        Set(value As Integer)
            _CampoValorControl = value
        End Set
    End Property

    Public Property Lunes As Boolean
        Get
            Return _Lunes
        End Get
        Set(value As Boolean)
            _Lunes = value
        End Set
    End Property

    Public Property Martes As Boolean
        Get
            Return _Martes
        End Get
        Set(value As Boolean)
            _Martes = value
        End Set
    End Property

    Public Property Miercoles As Boolean
        Get
            Return _Miercoles
        End Get
        Set(value As Boolean)
            _Miercoles = value
        End Set
    End Property

    Public Property Jueves As Boolean
        Get
            Return _Jueves
        End Get
        Set(value As Boolean)
            _Jueves = value
        End Set
    End Property

    Public Property Viernes As Boolean
        Get
            Return _Viernes
        End Get
        Set(value As Boolean)
            _Viernes = value
        End Set
    End Property

    Public Property Sabado As Boolean
        Get
            Return _Sabado
        End Get
        Set(value As Boolean)
            _Sabado = value
        End Set
    End Property

    Public Property Domingo As Boolean
        Get
            Return _Domingo
        End Get
        Set(value As Boolean)
            _Domingo = value
        End Set
    End Property

    Public Property idGrupoGeocerca As Integer
        Get
            Return _IdGrupoGeocerca
        End Get
        Set(value As Integer)
            _IdGrupoGeocerca = value
        End Set
    End Property

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

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property

    Public Property IdTipoDispositivo() As Integer
        Get
            Return _IdTipoDispositivo
        End Get
        Set(ByVal value As Integer)
            _IdTipoDispositivo = value
        End Set
    End Property

    Public Property Evento() As Integer
        Get
            Return _Evento
        End Get
        Set(ByVal value As Integer)
            _Evento = value
        End Set
    End Property

    Public Property IdGeocerca() As Integer
        Get
            Return _IdGeocerca
        End Get
        Set(ByVal value As Integer)
            _IdGeocerca = value
        End Set
    End Property

    Public Property Kilometraje() As Double
        Get
            Return _Kilometraje
        End Get
        Set(ByVal value As Double)
            _Kilometraje = value
        End Set
    End Property

    Public Property Porcentaje() As Double
        Get
            Return _Porcentaje
        End Get
        Set(ByVal value As Double)
            _Porcentaje = value
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

    Public Property IdTipoEvento() As Integer
        Get
            Return _IdTipoEvento
        End Get
        Set(ByVal value As Integer)
            _IdTipoEvento = value
        End Set
    End Property

    Public Property IdTipoAlerta() As Integer
        Get
            Return _IdTipoAlerta
        End Get
        Set(ByVal value As Integer)
            _IdTipoAlerta = value
        End Set
    End Property

    Public Property IdProducto() As String
        Get
            Return _IdProducto
        End Get
        Set(ByVal value As String)
            _IdProducto = value
        End Set
    End Property

    Public Property HoraDesde() As String
        Get
            Return _HoraDesde
        End Get
        Set(ByVal value As String)
            _HoraDesde = value
        End Set
    End Property

    Public Property HoraHasta() As String
        Get
            Return _HoraHasta
        End Get
        Set(ByVal value As String)
            _HoraHasta = value
        End Set
    End Property

    Public Property LimiteVelocidad() As Double
        Get
            Return _LimiteVelocidad
        End Get
        Set(ByVal value As Double)
            _LimiteVelocidad = value
        End Set
    End Property

    Public Property DentroGeo() As Boolean
        Get
            Return _DentroGeo
        End Get
        Set(ByVal value As Boolean)
            _DentroGeo = value
        End Set
    End Property

    Public Property IdDespacho() As Integer
        Get
            Return _IdDespacho
        End Get
        Set(ByVal value As Integer)
            _IdDespacho = value
        End Set
    End Property
#End Region

#Region " Constructores "

    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _UsuarioLogin = PaginaActual.Session("Usuario")

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

    Public Sub New()
        MyBase.New()
    End Sub
#End Region

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()
            If Codigo = "" Then
                Codigo = 0
            End If
            Call InicializarConexion()
            adp = New SqlDataAdapter("spD_AlertaConsultar " & idUsuario & "," & Codigo.ToString(), objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function Consultar2(ByVal Usuario As Integer, Optional ByVal Codigo As String = "") As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()
            If Codigo = "" Then
                Codigo = 0
            End If
            Call InicializarConexion()
            adp = New SqlDataAdapter("spD_AlertaConsultarV2 " & Usuario & "," & Codigo.ToString(), objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarSubAlt(ByVal Usuario As Integer, ByVal Subusuario As Integer) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            adp = New SqlDataAdapter("spD_AlertaSubUsuarioConsultar " & Usuario & "," & Subusuario, objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Try
            Me.LimpiarParametros()

            Select Case Accion
                Case "ING", "MOD"
                    If _IdGeocerca > 0 Then
                        _IdGrupoGeocerca = -1
                    End If

                    AdicionarParametros("@TipoAlerta", Me._IdTipoAlerta)
                    AdicionarParametros("@Nombre", Me._Nombre)
                    AdicionarParametros("@Descripcion", Me._Descripcion)
                    AdicionarParametros("@IdTipoDispositivo", Me._IdTipoDispositivo)
                    AdicionarParametros("@Evento", Me._Evento)
                    AdicionarParametros("@IdGeocerca", Me._IdGeocerca)
                    AdicionarParametros("@Kilometraje", _Kilometraje)
                    AdicionarParametros("@PorcentajeAnticipacion", _Porcentaje)
                    AdicionarParametros("@IdUsuario", _IdUsuario)
                    AdicionarParametros("@IdProducto", _IdProducto)
                    AdicionarParametros("@HoraDesde", _HoraDesde)
                    AdicionarParametros("@HoraHasta", _HoraHasta)
                    AdicionarParametros("@LimiteVelocidad", _LimiteVelocidad)
                    AdicionarParametros("@DentroGeo", IIf(_DentroGeo, 1, 0))
                    AdicionarParametros("@IdDespacho", _IdDespacho)
                    AdicionarParametros("@IdGrupoGeo", _IdGrupoGeocerca)

                    AdicionarParametros("@Lun", _Lunes)
                    AdicionarParametros("@Mar", _Martes)
                    AdicionarParametros("@Mie", _Miercoles)
                    AdicionarParametros("@Jue", _Jueves)
                    AdicionarParametros("@Vie", _Viernes)
                    AdicionarParametros("@Sab", _Sabado)
                    AdicionarParametros("@Dom", _Domingo)

                    AdicionarParametros("@CampoValorControl", _CampoValorControl)
                    AdicionarParametros("@TipoControl", _TipoControl)
                    AdicionarParametros("@FormaControl", _FormaControl)
                    AdicionarParametros("@Parametro1Control", _ParametroControl1)
                    AdicionarParametros("@Parametro2Control", _ParametroControl2)
                    AdicionarParametros("@FechaCaducidad", _FechaCaducidad)
                    AdicionarParametros("@IdSonido", _idSonido)

                    If Accion = "MOD" Then
                        AdicionarParametros("@IdAlerta", Me._IdAlerta)
                        Me.Procedimiento = "spD_AlertaActualizar"

                        AdicionarParametros("@UsuarioModificacion", Me.UsuarioLogin)
                    Else
                        Me.Procedimiento = "spD_AlertaIngresar"

                        AdicionarParametros("@UsuarioIngreso", Me.UsuarioLogin)
                    End If
                Case "ELM"
                    AdicionarParametros("@IdUsuario", _IdUsuario)
                    AdicionarParametros("@IdAlerta", Me._IdAlerta)
                    AdicionarParametros("@UsuarioEliminacion", Me.UsuarioLogin)

                    Me.Procedimiento = "spD_AlertaEliminar"
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

    Public Function Ejecutar2(ByVal Accion As String) As Boolean
        Try
            Me.LimpiarParametros()

            Select Case Accion
                Case "ING", "MOD"
                    If _IdGeocerca > 0 Then
                        _IdGrupoGeocerca = -1
                    End If

                    AdicionarParametros("@parTipoAlerta", Me._IdTipoAlerta)
                    AdicionarParametros("@parNombre", Me._Nombre)
                    AdicionarParametros("@parDescripcion", Me._Descripcion)
                    AdicionarParametros("@parIdTipoDispositivo", Me._IdTipoDispositivo)
                    AdicionarParametros("@parEvento", Me._Evento)
                    AdicionarParametros("@parIdGeocerca", Me._IdGeocerca)
                    AdicionarParametros("@parKilometraje", _Kilometraje)
                    AdicionarParametros("@parPorcentajeAnticipacion", _Porcentaje)
                    AdicionarParametros("@parIdUsuario", _IdUsuario)
                    AdicionarParametros("@parIdProducto", _IdProducto)
                    AdicionarParametros("@parHoraDesde", _HoraDesde)
                    AdicionarParametros("@parHoraHasta", _HoraHasta)
                    AdicionarParametros("@parLimiteVelocidad", _LimiteVelocidad)
                    AdicionarParametros("@parDentroGeo", IIf(_DentroGeo, 1, 0))
                    AdicionarParametros("@parIdDespacho", _IdDespacho)
                    AdicionarParametros("@parIdGrupoGeo", _IdGrupoGeocerca)
                    AdicionarParametros("@parIdSuceso", _IdSuceso)

                    AdicionarParametros("@parLun", _Lunes)
                    AdicionarParametros("@parMar", _Martes)
                    AdicionarParametros("@parMie", _Miercoles)
                    AdicionarParametros("@parJue", _Jueves)
                    AdicionarParametros("@parVie", _Viernes)
                    AdicionarParametros("@parSab", _Sabado)
                    AdicionarParametros("@parDom", _Domingo)

                    AdicionarParametros("@parCampoValorControl", _CampoValorControl)
                    AdicionarParametros("@parTipoControl", _TipoControl)
                    AdicionarParametros("@parFormaControl", _FormaControl)
                    AdicionarParametros("@parParametro1Control", _ParametroControl1)
                    AdicionarParametros("@parParametro2Control", _ParametroControl2)
                    AdicionarParametros("@parFechaCaducidad", _FechaCaducidad)
                    AdicionarParametros("@parIdSonido", _idSonido)

                    If Accion = "MOD" Then
                        AdicionarParametros("@parIdAlerta", Me._IdAlerta)
                        AdicionarParametros("@parUsuarioModificacion", Me.UsuarioLogin)

                        Me.Procedimiento = "spD_AlertaActualizarV2"
                    Else
                        AdicionarParametros("@parUsuarioIngreso", Me.UsuarioLogin)

                        Me.Procedimiento = "spD_AlertaIngresar"
                    End If
                Case "ELM"
                    AdicionarParametros("@parIdUsuario", _IdUsuario)
                    AdicionarParametros("@parIdAlerta", Me._IdAlerta)
                    AdicionarParametros("@parUsuarioEliminacion", Me.UsuarioLogin)

                    Me.Procedimiento = "spD_AlertaEliminarV2"
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

    Public Function UpdateNombreAlerta(ByVal IdAlerta As Integer, ByVal Nombre As String, ByVal Usuario As String, ByVal IdUser As Integer) As Integer

        Try
            Dim numero As Integer = 0
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("UPDATE D_Alerta SET Nombre = '{0}', UsuarioModificacion = '{1}', FechaModificacion = getdate() WHERE IdAlerta = {2} AND IdUsuario = {3}",
                                                         Nombre, Usuario, IdAlerta, IdUser))

            numero = 1
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function InsertarAlerta() As Integer
        Try
            Dim numero As Integer = 0
            Me.LimpiarParametros()

            If _IdGeocerca > 0 Then
                _IdGrupoGeocerca = -1
            End If

            Me.Procedimiento = "spD_AlertaIngresarV2"
            Call InicializarConexion()

            Dim cmd1 As SqlCommand = New SqlCommand(Me.Procedimiento, Me.objConexion)
            cmd1.CommandType = CommandType.StoredProcedure

            cmd1.Parameters.Add("@parTipoAlerta", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdTipoAlerta
            cmd1.Parameters.Add("@parNombre", SqlDbType.VarChar, 255, ParameterDirection.Input).Value = Me._Nombre
            cmd1.Parameters.Add("@parDescripcion", SqlDbType.VarChar, 255, ParameterDirection.Input).Value = Me._Descripcion
            cmd1.Parameters.Add("@parIdTipoDispositivo", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdTipoDispositivo
            cmd1.Parameters.Add("@parEvento", SqlDbType.Int, ParameterDirection.Input).Value = Me._Evento
            cmd1.Parameters.Add("@parIdGeocerca", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdGeocerca
            cmd1.Parameters.Add("@parKilometraje", SqlDbType.Float, ParameterDirection.Input).Value = _Kilometraje
            cmd1.Parameters.Add("@parPorcentajeAnticipacion", SqlDbType.Float, ParameterDirection.Input).Value = _Porcentaje
            cmd1.Parameters.Add("@parIdUsuario", SqlDbType.Int, ParameterDirection.Input).Value = _IdUsuario
            cmd1.Parameters.Add("@parIdProducto", SqlDbType.Int, ParameterDirection.Input).Value = _IdProducto
            cmd1.Parameters.Add("@parHoraDesde", SqlDbType.Time, ParameterDirection.Input).Value = _HoraDesde
            cmd1.Parameters.Add("@parHoraHasta", SqlDbType.Time, ParameterDirection.Input).Value = _HoraHasta
            cmd1.Parameters.Add("@parLimiteVelocidad", SqlDbType.Float, ParameterDirection.Input).Value = _LimiteVelocidad
            cmd1.Parameters.Add("@parDentroGeo", SqlDbType.Bit, ParameterDirection.Input).Value = IIf(_DentroGeo, 1, 0)
            cmd1.Parameters.Add("@parIdSuceso", SqlDbType.Int, ParameterDirection.Input).Value = _IdSuceso
            cmd1.Parameters.Add("@parIdDespacho", SqlDbType.Int, ParameterDirection.Input).Value = _IdDespacho
            cmd1.Parameters.Add("@parIdGrupoGeocerca", SqlDbType.Int, ParameterDirection.Input).Value = _IdGrupoGeocerca

            cmd1.Parameters.Add("@parLun", SqlDbType.Bit, ParameterDirection.Input).Value = _Lunes
            cmd1.Parameters.Add("@parMar", SqlDbType.Bit, ParameterDirection.Input).Value = _Martes
            cmd1.Parameters.Add("@parMie", SqlDbType.Bit, ParameterDirection.Input).Value = _Miercoles
            cmd1.Parameters.Add("@parJue", SqlDbType.Bit, ParameterDirection.Input).Value = _Jueves
            cmd1.Parameters.Add("@parVie", SqlDbType.Bit, ParameterDirection.Input).Value = _Viernes
            cmd1.Parameters.Add("@parSab", SqlDbType.Bit, ParameterDirection.Input).Value = _Sabado
            cmd1.Parameters.Add("@parDom", SqlDbType.Bit, ParameterDirection.Input).Value = _Domingo

            cmd1.Parameters.Add("@parCampoValorControl", SqlDbType.Int, ParameterDirection.Input).Value = _CampoValorControl
            cmd1.Parameters.Add("@parTipoControl", SqlDbType.Int, ParameterDirection.Input).Value = _TipoControl
            cmd1.Parameters.Add("@parFormaControl", SqlDbType.Int, ParameterDirection.Input).Value = _FormaControl
            cmd1.Parameters.Add("@parParametro1Control", SqlDbType.Float, ParameterDirection.Input).Value = _ParametroControl1
            cmd1.Parameters.Add("@parParametro2Control", SqlDbType.Float, ParameterDirection.Input).Value = _ParametroControl2
            cmd1.Parameters.Add("@parFechaCaducidad", SqlDbType.VarChar, ParameterDirection.Input).Value = _FechaCaducidad
            cmd1.Parameters.Add("@parIdSonido", SqlDbType.Int, ParameterDirection.Input).Value = _idSonido
            cmd1.Parameters.Add("@parUsuarioIngreso", SqlDbType.VarChar, 50, ParameterDirection.Input).Value = Me.UsuarioLogin

            cmd1.Parameters.Add("@parIdAlerta", SqlDbType.Int)
            cmd1.Parameters("@parIdAlerta").Direction = ParameterDirection.Output

            Me.objConexion.Open()
            cmd1.ExecuteNonQuery()
            numero = cmd1.Parameters("@parIdAlerta").Value

            Return numero
        Catch ex As Exception
            Throw ex
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ActualizarAlerta() As Integer
        Try
            Me.LimpiarParametros()

            If _IdGeocerca > 0 Then
                _IdGrupoGeocerca = -1
            End If

            Me.Procedimiento = "spD_AlertaActualizarV2"
            Call InicializarConexion()

            Dim cmd1 As SqlCommand = New SqlCommand(Me.Procedimiento, Me.objConexion)
            cmd1.CommandType = CommandType.StoredProcedure

            cmd1.Parameters.Add("@parTipoAlerta", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdTipoAlerta
            cmd1.Parameters.Add("@parNombre", SqlDbType.VarChar, 255, ParameterDirection.Input).Value = Me._Nombre
            cmd1.Parameters.Add("@parDescripcion", SqlDbType.VarChar, 255, ParameterDirection.Input).Value = Me._Descripcion
            cmd1.Parameters.Add("@parIdTipoDispositivo", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdTipoDispositivo
            cmd1.Parameters.Add("@parEvento", SqlDbType.Int, ParameterDirection.Input).Value = Me._Evento
            cmd1.Parameters.Add("@parIdGeocerca", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdGeocerca
            cmd1.Parameters.Add("@parKilometraje", SqlDbType.Float, ParameterDirection.Input).Value = _Kilometraje
            cmd1.Parameters.Add("@parPorcentajeAnticipacion", SqlDbType.Float, ParameterDirection.Input).Value = _Porcentaje
            cmd1.Parameters.Add("@parIdUsuario", SqlDbType.Int, ParameterDirection.Input).Value = _IdUsuario
            cmd1.Parameters.Add("@parIdProducto", SqlDbType.Int, ParameterDirection.Input).Value = _IdProducto
            cmd1.Parameters.Add("@parHoraDesde", SqlDbType.Time, ParameterDirection.Input).Value = _HoraDesde
            cmd1.Parameters.Add("@parHoraHasta", SqlDbType.Time, ParameterDirection.Input).Value = _HoraHasta
            cmd1.Parameters.Add("@parLimiteVelocidad", SqlDbType.Float, ParameterDirection.Input).Value = _LimiteVelocidad
            cmd1.Parameters.Add("@parDentroGeo", SqlDbType.Bit, ParameterDirection.Input).Value = IIf(_DentroGeo, 1, 0)
            cmd1.Parameters.Add("@parIdSuceso", SqlDbType.Int, ParameterDirection.Input).Value = _IdSuceso
            cmd1.Parameters.Add("@parIdDespacho", SqlDbType.Int, ParameterDirection.Input).Value = _IdDespacho
            cmd1.Parameters.Add("@parIdGrupoGeo", SqlDbType.Int, ParameterDirection.Input).Value = _IdGrupoGeocerca

            cmd1.Parameters.Add("@parLun", SqlDbType.Bit, ParameterDirection.Input).Value = _Lunes
            cmd1.Parameters.Add("@parMar", SqlDbType.Bit, ParameterDirection.Input).Value = _Martes
            cmd1.Parameters.Add("@parMie", SqlDbType.Bit, ParameterDirection.Input).Value = _Miercoles
            cmd1.Parameters.Add("@parJue", SqlDbType.Bit, ParameterDirection.Input).Value = _Jueves
            cmd1.Parameters.Add("@parVie", SqlDbType.Bit, ParameterDirection.Input).Value = _Viernes
            cmd1.Parameters.Add("@parSab", SqlDbType.Bit, ParameterDirection.Input).Value = _Sabado
            cmd1.Parameters.Add("@parDom", SqlDbType.Bit, ParameterDirection.Input).Value = _Domingo

            cmd1.Parameters.Add("@parCampoValorControl", SqlDbType.Int, ParameterDirection.Input).Value = _CampoValorControl
            cmd1.Parameters.Add("@parTipoControl", SqlDbType.Int, ParameterDirection.Input).Value = _TipoControl
            cmd1.Parameters.Add("@parFormaControl", SqlDbType.Int, ParameterDirection.Input).Value = _FormaControl
            cmd1.Parameters.Add("@parParametro1Control", SqlDbType.Float, ParameterDirection.Input).Value = _ParametroControl1
            cmd1.Parameters.Add("@parParametro2Control", SqlDbType.Float, ParameterDirection.Input).Value = _ParametroControl2
            cmd1.Parameters.Add("@parFechaCaducidad", SqlDbType.VarChar, ParameterDirection.Input).Value = _FechaCaducidad
            cmd1.Parameters.Add("@parIdSonido", SqlDbType.Int, ParameterDirection.Input).Value = _idSonido
            cmd1.Parameters.Add("@parUsuarioModificacion", SqlDbType.VarChar, 50, ParameterDirection.Input).Value = Me.UsuarioLogin

            cmd1.Parameters.Add("@parIdAlerta", SqlDbType.Int, ParameterDirection.Input).Value = Me._IdAlerta

            Me.objConexion.Open()
            cmd1.ExecuteNonQuery()

            Return 1
        Catch ex As Exception
            Throw ex
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarAlertaActivos(ByVal FechaDesde As String,
                                           ByVal FechaHasta As String,
                                           Optional ByVal Minutos As Integer = 0) As DataSet
        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            If Minutos <> 0 Then
                Me.AdicionarParametros("@Minutos", Minutos)
            End If
            Me.AdicionarParametros("@FechaDesde", FechaDesde)
            Me.AdicionarParametros("@FechaHasta", FechaHasta)

            InicializarConexion()
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                       CommandType.StoredProcedure,
                                                       "spAlertaActivoConsultar",
                                                       Parametros)
            Return DatosDetalle
        Catch ex As Exception

        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarAlertaActivosSub(ByVal FechaDesde As String,
                                          ByVal FechaHasta As String,
                                          ByVal idSubUsuario As Integer,
                                          Optional ByVal Minutos As Integer = 0) As DataSet
        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IdSubUsuario", idSubUsuario)
            If Minutos <> 0 Then
                Me.AdicionarParametros("@Minutos", Minutos)
            End If
            Me.AdicionarParametros("@FechaDesde", FechaDesde)
            Me.AdicionarParametros("@FechaHasta", FechaHasta)

            InicializarConexion()
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                       CommandType.StoredProcedure,
                                                       "spAlertaActivoConsultarSub",
                                                       Parametros)
            Return DatosDetalle
        Catch ex As Exception

        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function AtenderAlertaActivo(ByVal SecuenciaAlerta As Integer, ByVal Observaciones As String) As Boolean
        Try
            LimpiarParametros()
            AdicionarParametros("@Usuario", _UsuarioLogin)
            AdicionarParametros("@SecuenciaAlerta", SecuenciaAlerta)
            AdicionarParametros("@Observaciones", Observaciones)

            InicializarConexion()
            SqlHelper.ExecuteNonQuery(objConexion,
                                      CommandType.StoredProcedure,
                                      "spAlertaActivoAtender",
                                      Me.Parametros)
            Return True
        Catch ex As Exception
            Return False
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarAlertasActivosSub(ByVal IdSubUsuario As Integer,
                                            Optional ByVal Minutos As Integer = 0) As Integer
        Try
            If Minutos = 0 Then
                Minutos = 30
            End If

            ConsultarAlertaActivosSub("",
                                   "",
                                   IdSubUsuario,
                                   Minutos)

            Try
                Return DatosDetalle.Tables(0).Rows.Count
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception

        End Try
    End Function

    Public Function ContarAlertasActivos(Optional ByVal Minutos As Integer = 0) As Integer
        Try
            If Minutos = 0 Then
                Minutos = 30
            End If

            ConsultarAlertaActivos("",
                                   "",
                                   Minutos)

            Try
                Return DatosDetalle.Tables(0).Rows.Count
            Catch ex As Exception
                Return 0
            End Try
        Catch ex As Exception

        End Try
    End Function

    Public Function ConsultarAlertaEntidades() As DataSet
        Try
            InicializarDatos()
            LimpiarParametros()
            AdicionarParametros("@IdAlerta", _IdAlerta)

            InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(objConexion,
                                      CommandType.StoredProcedure,
                                      "spD_AlertaEntidadConsultar",
                                      Me.Parametros)
            Return Datos
        Catch ex As Exception
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarAlertas(ByVal Usuario As Integer, Optional ByVal Codigo As String = "") As Integer
        Dim adp As SqlDataAdapter
        Dim contar As Integer = 0
        Try
            Call InicializarDatos()
            If Codigo = "" Then
                Codigo = 0
            End If
            Call InicializarConexion()
            adp = New SqlDataAdapter("spD_AlertaConsultar " & Usuario & "," & Codigo.ToString(), objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos.Tables(0).Rows.Count
        Catch ex As SqlException
            Return contar
        Finally
            Call TerminarConexion()
        End Try

    End Function

    Public Function ContarAlertas2(ByVal Usuario As Integer, ByVal Subusuario As Integer) As Integer
        Dim adp As SqlDataAdapter
        Dim contar As Integer = 0
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            adp = New SqlDataAdapter("spD_AlertaSubUsuarioConsultar " & Usuario & "," & Subusuario, objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos.Tables(0).Rows.Count
        Catch ex As SqlException
            Return contar
        Finally
            Call TerminarConexion()
        End Try

    End Function

    Public Function ContarGruposAlertas(ByVal Usuario As Integer, Optional ByVal Codigo As String = "") As Integer
        Dim adp As SqlDataAdapter
        Dim contar As Integer = 0
        Try
            Call InicializarDatos()
            If Codigo = "" Then
                Codigo = 0
            End If
            Call InicializarConexion()
            adp = New SqlDataAdapter("spAlertaGrupoAlertaConsultar " & Codigo.ToString() & ",0," & Usuario, objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Alertas")
            Datos.AcceptChanges()

            Return Datos.Tables(0).Rows.Count
        Catch ex As SqlException
            Return contar
        Finally
            Call TerminarConexion()
        End Try

    End Function

    Public Function ConsultarSucesos() As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT * FROM dbo.Suceso", objConexion)

            adp.SelectCommand.CommandTimeout = 450000
            adp.Fill(Datos, "Sucesos")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConfUsuarioConsultar2(ByVal User As Integer) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT * FROM dbo.UsuarioConfiguracion with (nolock) WHERE IdUsuario=" & User, objConexion)

            adp.SelectCommand.CommandTimeout = 4500000
            adp.Fill(Datos, "PuntosU")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

End Class

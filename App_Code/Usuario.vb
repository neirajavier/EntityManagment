Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration.ConfigurationManager

Public Class Usuario : Inherits ClassCore

    Private Const CODIGO_TIPO_ENTIDAD_CLIENTE As String = "1"
    Private Const CODIGO_TIPO_ENTIDAD_ESPGOB As String = "5"
    'Private Const CODIGO_TIPO_ENTIDAD_CONCESIONARIO As String = "3"

    Private Const APLICACION_WEB_GEOSYS As String = "1"
    Private BD As OdinDataContext

#Region " Constructores "

    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

        If (_ip = String.Empty) Then
            _ip = Pagina.Request.ServerVariables("REMOTE_ADDR")
        End If

        _PaginaActual = Pagina
        _UsuarioIngreso = Pagina.Session("Usuario")
        _IdUsuario = Pagina.Session("IdUsuario")

        _Entidades = Nothing

        ReDim _Entidades(0)
    End Sub

    Public Sub New(ip As String, idusuario As Integer, usuario As String)
        MyBase.New()

        _ip = ip

        _UsuarioIngreso = usuario
        _IdUsuario = idusuario

        ReDim _Entidades(0)
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

#Region " Propiedades Privadas "
    Private _IdUsuario As Integer
    Private _SubUsuario As String
    Private _idSubUsuario As Integer
    Private _Usuario As String
    Private _Clave As String
    Private _Estado As String
    Private _Nombre As String
    Private _Entidades As String()
    Private _Administrador As Integer
    Private _Perfil As String
    Private _FuncionesAcciones As ArrayList
    'Private _CodigoConcesionario As String
    Private _TipoEntidad As String
    Private _CodigoEntidad As String
    Private _CodigoCliente As String
    Private _AdminConcesionario As Integer
    Private _DirLogo As String
    Private _Usuarios As String()
    Private _AplicacionGeoSYS As String
    Private _ConfirmoEmail As Boolean
    Private _ConfirmoCelular As Boolean
    Private _uid As Object

    Private _UsuarioIngreso As String
    Private _FechaIngreso As String
    Private _UsuarioModificacion As String
    Private _FechaModificacion As String
    Private _ExisteAdministrador As Boolean
    Private _Suspendido As String
    Private _Email As String
    Private _ConfigUsuario As ConfigUsuario
    Private _ConfigSubUsuario As ConfigSubUsuario

    Private _Aplicaciones As Array()
    Private _ErrorIngreso As String

    Private _ConfirmaCelular As Integer
    Private _ConfirmaEmail As Integer
    Private _EsSubUsuario As Boolean
    Private _EstadoFlotaInicial As Boolean
    Private _AdminGrupos As Boolean
    Private _EditarEtiquetas As Boolean

    Private _lLogo As String
    Private _ColorFondo As String
    Private _KeyU As String
    Private _IdPais As String
    Private _IdLocalidad As Integer
    Private _Localidad As String

#End Region

#Region " Propiedades Publicas "

    Public Property IdPais As String
        Get
            Return _IdPais
        End Get
        Set(value As String)
            _IdPais = value
        End Set
    End Property

    Public Property uid() As Object
        Get
            Return _uid
        End Get
        Set(ByVal value As Object)
            _uid = value
        End Set
    End Property

    Public Property lLogo() As String
        Get
            Return _lLogo
        End Get
        Set(ByVal value As String)
            _lLogo = value
        End Set
    End Property

    Public Property ColorFondo() As String
        Get
            Return _ColorFondo
        End Get
        Set(ByVal value As String)
            _ColorFondo = value
        End Set
    End Property

    Public Property AdminGrupos() As Boolean
        Get
            Return _AdminGrupos
        End Get
        Set(ByVal value As Boolean)
            Me._AdminGrupos = value
        End Set
    End Property

    Public Property SubUsuario() As String
        Get
            Return _SubUsuario
        End Get
        Set(ByVal value As String)
            _SubUsuario = value
        End Set
    End Property

    Public Property IdSubUsuario() As Integer
        Get
            Return _idSubUsuario
        End Get
        Set(ByVal value As Integer)
            _idSubUsuario = value
        End Set
    End Property

    Public Property EsSubUsuario() As Boolean
        Get
            Return _EsSubUsuario
        End Get
        Set(ByVal value As Boolean)
            _EsSubUsuario = value
        End Set
    End Property

    Public Property IdUsuario() As Integer
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Integer)
            _IdUsuario = value
        End Set
    End Property

    Public Property ConfiguracionSubUsuario() As ConfigSubUsuario
        Get
            Return _ConfigSubUsuario
        End Get
        Set(ByVal value As ConfigSubUsuario)
            _ConfigSubUsuario = value
        End Set
    End Property

    Public Property ConfiguracionUsuario() As ConfigUsuario
        Get
            Return _ConfigUsuario
        End Get
        Set(ByVal value As ConfigUsuario)
            _ConfigUsuario = value
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

    Public ReadOnly Property Aplicaciones() As Array
        Get
            Return _Aplicaciones
        End Get
    End Property

    Public Property Administrador() As Integer
        Get
            Return _Administrador
        End Get
        Set(ByVal value As Integer)
            _Administrador = value
        End Set
    End Property

    Public ReadOnly Property Entidades() As String()
        Get
            Return _Entidades
        End Get
    End Property

    Public Overloads Property Usuario() As String
        Get
            Return _Usuario
        End Get
        Set(ByVal value As String)
            _Usuario = value
        End Set
    End Property

    Public ReadOnly Property ConfirmoEmail() As Boolean
        Get
            Return _ConfirmoEmail
        End Get
    End Property

    Public ReadOnly Property ConfirmoCelular() As Boolean
        Get
            Return _ConfirmoCelular
        End Get
    End Property

    Public Property Clave() As String
        Get
            Return _Clave
        End Get
        Set(ByVal value As String)
            _Clave = value
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

    Public Property UsuarioIngreso() As String
        Get
            Return _UsuarioIngreso
        End Get
        Set(ByVal value As String)
            _UsuarioIngreso = value
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

    Public Property UsuarioModificacion() As String
        Get
            Return _UsuarioModificacion
        End Get
        Set(ByVal value As String)
            _UsuarioModificacion = value
        End Set
    End Property

    Public Property FechaModificacion() As String
        Get
            Return _FechaModificacion
        End Get
        Set(ByVal value As String)
            _FechaModificacion = value
        End Set
    End Property

    Public Property Perfil() As String
        Get
            Return _Perfil
        End Get
        Set(ByVal value As String)
            _Perfil = value
        End Set
    End Property

    Public Property DirLogo() As String
        Get
            Return _DirLogo
        End Get
        Set(ByVal value As String)
            _DirLogo = value
        End Set
    End Property

    Public Property FuncionesAcciones() As ArrayList
        Get
            Return _FuncionesAcciones
        End Get
        Set(ByVal value As ArrayList)
            _FuncionesAcciones = value
        End Set
    End Property

    'Public Property CodigoConcesionario() As String
    '    Get
    '        Return _CodigoConcesionario
    '    End Get
    '    Set(ByVal value As String)
    '        _CodigoConcesionario = value
    '    End Set
    'End Property

    Public Property CodigoEntidad() As String
        Get
            Return _CodigoEntidad
        End Get
        Set(ByVal value As String)
            _CodigoEntidad = value
        End Set
    End Property

    Public Property Suspendido() As String
        Get
            Return _Suspendido
        End Get
        Set(ByVal value As String)
            _Suspendido = value
        End Set
    End Property

    Public Property TipoEntidad() As String
        Get
            Return _TipoEntidad
        End Get
        Set(ByVal value As String)
            _TipoEntidad = value
        End Set
    End Property

    Public Property CodigoCliente() As String
        Get
            Return _CodigoCliente
        End Get
        Set(ByVal value As String)
            _CodigoCliente = value
        End Set
    End Property

    Public Property AdminConcesionario() As Integer
        Get
            Return _AdminConcesionario
        End Get
        Set(ByVal value As Integer)
            _AdminConcesionario = value
        End Set
    End Property

    Public Property AplicacionGeoSYS() As String
        Get
            Return _AplicacionGeoSYS
        End Get
        Set(ByVal value As String)
            _AplicacionGeoSYS = value
        End Set
    End Property

    Public ReadOnly Property Usuarios() As String()
        Get
            Return _Usuarios
        End Get
    End Property

    Public ReadOnly Property ErrorIngreso() As String
        Get
            Return _ErrorIngreso
        End Get
    End Property

    Public ReadOnly Property Email() As String
        Get
            Return _Email
        End Get
    End Property

    Public Property ConfirmaCelular() As Integer
        Get
            Return _ConfirmaCelular
        End Get
        Set(ByVal value As Integer)
            _ConfirmaCelular = value
        End Set
    End Property

    Public Property ConfirmaEmail() As Integer
        Get
            Return _ConfirmaEmail
        End Get
        Set(ByVal value As Integer)
            _ConfirmaEmail = value
        End Set
    End Property

    Public Property EstadoFlotaInicial() As Boolean
        Get
            Return _EstadoFlotaInicial
        End Get
        Set(ByVal value As Boolean)
            _EstadoFlotaInicial = value
        End Set
    End Property

    Public Property KeyU() As String
        Get
            Return _KeyU
        End Get
        Set(value As String)
            _KeyU = value
        End Set
    End Property

    Public Property IdLocalidad As Integer
        Get
            Return _IdLocalidad
        End Get
        Set(value As Integer)
            _IdLocalidad = value
        End Set
    End Property

    Public Property Localidad As String
        Get
            Return _Localidad
        End Get
        Set(value As String)
            _Localidad = value
        End Set
    End Property

#End Region

#Region " Funciones Heredades "

    Public Function BitacoraIngresar(ByVal Usuario As String,
                                     ByVal Accion As String,
                                     ByVal Descripcion As String,
                                     ByVal UsuarioEquipo As String,
                                     ByVal IP As String) As Boolean
        Try
            Me.LimpiarParametros()

            If Me._FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                'SqlHelper.ExecuteNonQuery(Me.objConexion, _
                '                          CommandType.Text, _
                '                          String.Format("insert into logUsuario(Usuario,FechaHora,Accion,Descripcion,UsuarioEquipo,IP) values('{0}',getdate(),'{1}','{2}','{3}','{4}')", _
                '                                        Usuario, _
                '                                        Accion, _
                '                                        Descripcion, _
                '                                        UsuarioEquipo, _
                '                                        IP), _
                '                          Me.Parametros)
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                                         CommandType.Text,
                                         String.Format("exec spLogUsuarioGeoIngresar '{0}','{1}','{2}','{3}','{4}'", Usuario,
                                                                                                                    Accion,
                                                                                                                    Descripcion,
                                                                                                                    UsuarioEquipo,
                                                                                                                    IP),
                                         Me.Parametros)

            Else
                'Call InicializarConexionGR()
                ''SqlHelper.ExecuteNonQuery(Me.objConexionGR, _
                ''                          CommandType.Text, _
                ''                          String.Format("insert into logUsuario(Usuario,FechaHora,Accion,Descripcion,UsuarioEquipo,IP) values('{0}',getdate(),'{1}','{2}','{3}','{4}')", _
                ''                                        Usuario, _
                ''                                        Accion, _
                ''                                        Descripcion, _
                ''                                        UsuarioEquipo, _
                ''                                        IP), _
                ''                          Me.Parametros)

                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '                         CommandType.Text,
                '                         String.Format("exec spLogUsuarioGeoIngresar '{0}','{1}','{2}','{3}','{4}'", Usuario,
                '                                                                                                    Accion,
                '                                                                                                    Descripcion,
                '                                                                                                    UsuarioEquipo,
                '                                                                                                    IP),
                '                         Me.Parametros)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ActualizarUltimoLogin(ByVal idusuario As Integer,
                                          Optional ByVal IdSubUsuario As Integer = 0,
                                          Optional ByVal SubUsuario As Boolean = False) As Boolean
        Try
            Me.LimpiarParametros()
            Me.AdicionarParametros("@Usuario", Usuario)

            If IdSubUsuario > 0 Then
                Me.AdicionarParametros("@IdSubUsuario", IdSubUsuario)
            End If

            If Me._FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()

                If SubUsuario Then
                    SqlHelper.ExecuteNonQuery(Me.objConexion,
                                              CommandType.Text,
                                              String.Format("Update SubUsuario set FechaUltimoLogin = getdate() where IdUsuario = {0} and IdSubUsuario = {1}",
                                                             idusuario,
                                                             IdSubUsuario),
                                              Me.Parametros)
                Else
                    SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.Text,
                          String.Format("Update Usuario set FechaUltimoLogin = getdate() where IdUsuario = {0}",
                                         idusuario),
                          Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '                          CommandType.Text,
                '                          String.Format("Update Usuario set FechaUltimoLogin = getdate() where IdUsuario = {0}",
                '                                         idusuario),
                '                          Me.Parametros)
            End If
        Catch ex As Exception
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
                    AdicionarParametros("@Usuario", Me._Usuario)
                    AdicionarParametros("@Nombre", Me._Nombre)
                    AdicionarParametros("@Clave", Me._Clave)
                    AdicionarParametros("@Administrador", Me._Administrador)
                    AdicionarParametros("@AdministradorEntidad", Me._AdminConcesionario)
                    AdicionarParametros("@ConfirmoCelular", Me._ConfirmaCelular)
                    AdicionarParametros("@ConfirmoEmail", Me._ConfirmaEmail)

                    If Accion = "MOD" Then
                        Me.Procedimiento = "spUsuarioActualizar"

                        AdicionarParametros("@IdUsuario", Me._IdUsuario)
                        AdicionarParametros("@UsuarioModificacion", Me._UsuarioIngreso)
                    Else
                        Me.Procedimiento = "spUsuarioIngresar"

                        AdicionarParametros("@UsuarioIngreso", Me._UsuarioIngreso)
                    End If
                Case "ELM"
                    AdicionarParametros("@IdUsuario", Me._IdUsuario)
                    Me.Procedimiento = "spUsuarioEliminar"
                Case "INH"
                    AdicionarParametros("@Usuario", Me._Usuario)
                    Me.Procedimiento = "spUsuarioDeshabilitar"
                Case "HAB"
                    AdicionarParametros("@Usuario", Me._Usuario)
                    Me.Procedimiento = "spUsuarioHabilitar"
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

    Public Function ConsultarSubUsuarios(ByVal SubUsuario As String,
                                         Optional ByVal Codigo As String = "") As DataSet
        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            Call InicializarConexion()
            Me.AdicionarParametros("@IdUsuario", 0)
            Me.AdicionarParametros("@SubUsuario", SubUsuario)
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                           CommandType.StoredProcedure,
                                           "[spSubUsuarioConsultar]",
                                           Me.Parametros)

            Return DatosDetalle
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Dim oDataAdapter As SqlDataAdapter
        Try
            Me._FuenteUsuario = "Artemis"
            'GeoSyS
            Try
                Call InicializarDatos()
                Me.LimpiarParametros()
                Call InicializarConexion()

                objConexion.Open()
                oDataAdapter = New SqlDataAdapter("spUsuarioConsultarPRUEBA '" & Codigo & "'", objConexion)
                oDataAdapter.Fill(Me.Datos)
                objConexion.Close()
            Catch ex As Exception
                Console.Write(ex.Message)
            Finally
                TerminarConexion()
            End Try

            Return Datos
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        End Try
    End Function
#End Region

#Region " Procedimientos y Funciones Propios"

    Public Function ContarTiposVehiculoUsuario(ByVal IdUsuario As Integer, ByVal TipoVehiculo As String, ByVal RegistroUnidades As Integer) As String
        Dim contadortipos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()

            If RegistroUnidades = 0 Then
                If TipoVehiculo = "N.D." Then
                    contadortipos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("select count(distinct IdActivo) as Total from vieActivosEntidad v with (nolock) where v.idEntidad in (select IdEntidad from UsuarioEntidad with (nolock) where IdUsuario = {0}) and Estado in ('A','I') and TipoVehiculo is null",
                                                  IdUsuario,
                                                  TipoVehiculo))
                Else
                    contadortipos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("select count(distinct IdActivo) as Total from vieActivosEntidad v with (nolock) where v.idEntidad in (select IdEntidad from UsuarioEntidad with (nolock) where IdUsuario = {0}) and Estado in ('A','I') and TipoVehiculo = '{1}'",
                                                  IdUsuario,
                                                  TipoVehiculo))
                End If
            Else
                If TipoVehiculo = "N.D." Then
                    contadortipos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("select count(distinct IdActivo) as Total from vieActivosEntidad v with (nolock) where v.idEntidad in (select IdEntidad from UsuarioEntidad with (nolock) where IdUsuario = {0}) and TipoVehiculo is null",
                                                  IdUsuario,
                                                  TipoVehiculo))
                Else
                    contadortipos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("select count(distinct IdActivo) as Total from vieActivosEntidad v with (nolock) where v.idEntidad in (select IdEntidad from UsuarioEntidad with (nolock) where IdUsuario = {0}) and TipoVehiculo = '{1}'",
                                                  IdUsuario,
                                                  TipoVehiculo))
                End If
            End If

            Return contadortipos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarTiposVehiculoSubUsuario(ByVal IdUsuario As Integer, ByVal IdSubUsuario As Integer, ByVal TipoVehiculo As String) As String
        Dim contadortipos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            contadortipos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("select count(distinct IdActivo) as Total from vieActivosEntidad v with (nolock) where v.idActivo in (Select IdActivo from ActivoSubUsuario asu with (nolock) where convert(varchar,asu.IdUsuario) + '' + convert(varchar,asu.idSubUsuario) = '{0}{1}' union Select IdActivo from vieActivoGrupoDispositivoUsuario asud with (nolock) where convert(varchar,asud.idUsuario) + '' + convert(varchar,asud.idSubUsuario) = '{0}{1}') and Estado in ('A','I') and TipoVehiculo = '{2}'",
                                                  IdUsuario,
                                                  IdSubUsuario,
                                                  TipoVehiculo))

            Return contadortipos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarProductosSubUsuario(ByVal IdUsuario As Integer, ByVal idSubUsuario As Integer, ByVal CodigoProducto As String) As String
        Dim contadorproductos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            contadorproductos = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("select count(distinct IdDispositivo) as Codigo from Dispositivo dis WITH (NOLOCK) where iddispositivo in (select IdDispositivo from vieActivoSubUsuarioDispositivo WITH (NOLOCK) where convert(Varchar,IdUsuario) + '' + convert(Varchar,IdSubUsuario) = '{0}{1}' union select IdDispositivo from vieActivoGrupoDispositivoUsuario vg with (nolock) where convert(varchar,vg.idUsuario) + '' + convert(varchar,vg.idSubUsuario) = '{0}{1}') and idproducto = '{2}'",
                                                              IdUsuario,
                                                              idSubUsuario,
                                                              CodigoProducto))

            Return contadorproductos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarProductosUsuario(ByVal IdUsuario As Integer, ByVal CodigoProducto As String, ByVal RegistroUnidades As Integer) As String
        Dim contadorproductos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            'contadorproductos = SqlHelper.ExecuteScalar(Me.objConexion, _
            '                                    CommandType.Text, _
            '                                    String.Format("SELECT COUNT(IdDispositivo) AS Dispositivos FROM dbo.vieActivoEntidad_DispositivoUsuario v WITH (NOLOCK) WHERE IdUsuario = {0} AND (SELECT TOP 1 idproducto FROM dbo.Dispositivo d WITH (NOLOCK) WHERE d.IdDispositivo = v.IdDispositivo AND CONVERT(VARCHAR,d.IdProducto) + '_' + CONVERT(VARCHAR,d.TipoDispositivo) = '{1}') IS NOT null", _
            '                                                  IdUsuario, _
            '                                                  CodigoProducto))

            If RegistroUnidades = 0 Then
                contadorproductos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("SELECT COUNT(distinct(IdDispositivo)) AS Dispositivos FROM dbo.vieActivoEntidad_DispositivoUsuario v WITH (NOLOCK) WHERE IdUsuario = {0} AND Estado in ('A','I') AND (SELECT TOP 1 idproducto FROM dbo.Dispositivo d WITH (NOLOCK) WHERE d.IdDispositivo = v.IdDispositivo AND CONVERT(VARCHAR,d.IdProducto) = '{1}') IS NOT null",
                                                  IdUsuario,
                                                  CodigoProducto))
            Else
                contadorproductos = SqlHelper.ExecuteScalar(Me.objConexion,
                                    CommandType.Text,
                                    String.Format("SELECT COUNT(distinct(IdDispositivo)) AS Dispositivos FROM dbo.vieActivoEntidad_DispositivoUsuario v WITH (NOLOCK) WHERE IdUsuario = {0} AND (SELECT TOP 1 idproducto FROM dbo.Dispositivo d WITH (NOLOCK) WHERE d.IdDispositivo = v.IdDispositivo AND CONVERT(VARCHAR,d.IdProducto) = '{1}') IS NOT null",
                                                  IdUsuario,
                                                  CodigoProducto))
            End If

            Return contadorproductos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarGruposSubUsuario(ByVal IdUsuario As Integer,
                                              ByVal idSubUsuario As Integer) As DataSet
        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarDatosDetalle()

            InicializarConexion()
            DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select 0 as Codigo, upper(" & "'" & "Sin Grupo" & "'" & ") as Grupo " & ",''''" & "  as Descripcion, {0} as IdUsuario, {1} as IdSubUsuario union select convert(varchar,idGrupo) as Codigo, Grupo, Descripcion,IdUsuario,IdSubUsuario from vieSubUsuarioGrupo with (nolock) where idUsuario={0} and idSubusuario={1} order by 2 asc",
                                                              IdUsuario,
                                                              idSubUsuario,
                                                              "_"))

            Return DatosDetalle
        Catch ex As Exception
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGruposSubUsuario(ByVal IdUsuario As Integer,
                                           ByVal idSubUsuario As Integer) As String
        Dim ContadorGrupos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Grupos from SubUsuarioGrupo with (nolock) where idUsuario={0} and idSubusuario={1}",
                                                              IdUsuario,
                                                              idSubUsuario))

            Return ContadorGrupos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarVehiculosGruposUsuario(ByVal IdUsuario As Integer) As DataSet
        Try
            InicializarDatos()
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            InicializarConexion()
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select 0 as IdGrupo, upper(" & "'" & "Sin Grupo" & "'" & ") as Grupo " & ",''''" & "  as Descripcion union select idGrupo,upper(Grupo) as Grupo, Descripcion from Grupo with (nolock) where idUsuario = '{0}' order by 2",
                                                              IdUsuario))

            Return DatosDetalle
        Catch ex As Exception
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarGruposUsuario(ByVal IdUsuario As Integer) As DataSet
        Try
            InicializarDatos()
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            InicializarConexion()
            'Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion, _
            '                                    CommandType.Text, _
            '                                    String.Format("Select 0 as IdGrupo, upper(" & "'" & "Sin Grupo" & "'" & ") as Grupo " & ",''''" & "  as Descripcion union select idGrupo,upper(Grupo) as Grupo, Descripcion from Grupo with (nolock) where idUsuario = '{0}' order by 2", _
            '                                                  IdUsuario))

            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("spActivoGrupoUsuarioConsultar {0}",
                                                              IdUsuario))


            Return DatosDetalle
        Catch ex As Exception
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGruposUsuario(ByVal IdUsuario As Integer) As String
        Dim ContadorGrupos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Grupo from Grupos with (nolock) where idUsuario = '{0}'",
                                                              IdUsuario))

            Return ContadorGrupos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarVehiculosGruposSubUsuario(ByVal IdUsuario As Integer,
                                                    ByVal idSubUsuario As Integer,
                                                    ByVal idGrupo As Integer) As String
        Dim ContadorGrupos As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            'SELECT * FROM dbo.vieActivoEntidad_Usuario WHERE IdUsuario = 2 AND IdActivo IN (SELECT IdActivo FROM dbo.ActivoGrupo WHERE IdUsuario = 2 AND idgrupo = 1)           
            If idGrupo = 0 Then
                ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
                                              CommandType.Text,
                                              String.Format("SELECT count(*) FROM ActivoSubusuario (nolock) WHERE IdUsuario = {0} AND IdSubUsuario = {1} and IdActivo NOT IN (SELECT IdActivo FROM dbo.vieActivosGrupoSubUsuario with (nolock) WHERE IdUsuario = {0} and IdSubUsuario = {1} )",
                                                            IdUsuario,
                                                            idSubUsuario,
                                                            idGrupo))
            Else
                ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
                                              CommandType.Text,
                                                String.Format("SELECT count(*) FROM dbo.vieActivoGrupoDispositivoUsuario with (nolock) WHERE IdUsuario = {0} AND IdSubUsuario = {1} AND idgrupo = {2} and Estado in ('A','I')",
                                                            IdUsuario,
                                                            idSubUsuario,
                                                            idGrupo))
            End If

            Return ContadorGrupos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarVehiculosGruposUsuario(ByVal IdUsuario As Integer,
                                                 ByVal idGrupo As Integer,
                                                 ByVal RegistroUnidades As Integer) As String
        Dim ContadorGrupos As Integer = 0
        Dim GCommand As SqlCommand

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            objConexion.Open()

            'SELECT * FROM dbo.vieActivoEntidad_Usuario WHERE IdUsuario = 2 AND IdActivo IN (SELECT IdActivo FROM dbo.ActivoGrupo WHERE IdUsuario = 2 AND idgrupo = 1)
            'If idGrupo = 0 Then
            '    If RegistroUnidades = 0 Then
            '        'ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
            '        '                          CommandType.Text,
            '        '                          String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND Estado in ('A','I') AND IdActivo NOT IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo))

            '        'ContadorGrupos = New SqlCommand(String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND Estado in ('A','I') AND IdActivo NOT IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo), objConexion).ExecuteScalar()

            '        ContadorGrupos = New SqlCommand(String.Format("SELECT dbo.ContarGruposUsuario({0},{1}),0",
            '                                              IdUsuario,
            '                                              idGrupo), objConexion).ExecuteScalar()
            '    Else
            '        'ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
            '        '                          CommandType.Text,
            '        '                          String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND IdActivo NOT IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo))

            '        'ContadorGrupos = New SqlCommand(String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND IdActivo NOT IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo), objConexion).ExecuteScalar()
            '        ContadorGrupos = New SqlCommand(String.Format("SELECT dbo.ContarGruposUsuario({0},{1}),1",
            '                          IdUsuario,
            '                          idGrupo), objConexion).ExecuteScalar()
            '    End If
            'Else
            '    If RegistroUnidades = 0 Then
            '        'ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
            '        '                          CommandType.Text,
            '        '                            String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND Estado in ('A','I') AND IdActivo IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0} AND idgrupo = {1})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo))

            '        'ContadorGrupos = New SqlCommand(String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND Estado in ('A','I') AND IdActivo IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0} AND idgrupo = {1})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo), objConexion).ExecuteScalar()

            '        ContadorGrupos = New SqlCommand(String.Format("SELECT dbo.ContarGruposUsuario({0},{1}),0",
            '                          IdUsuario,
            '                          idGrupo), objConexion).ExecuteScalar()
            '    Else
            '        'ContadorGrupos = SqlHelper.ExecuteScalar(Me.objConexion,
            '        '                          CommandType.Text,
            '        '                            String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND IdActivo IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0} AND idgrupo = {1})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo))

            '        'ContadorGrupos = New SqlCommand(String.Format("SELECT count(distinct(idactivo)) FROM dbo.vieActivoEntidad_Usuario with (nolock) WHERE IdUsuario = {0} AND IdActivo IN (SELECT IdActivo FROM dbo.vieActivosGrupoUsuario with (nolock) WHERE IdUsuario = {0} AND idgrupo = {1})",
            '        '                                        IdUsuario,
            '        '                                        idGrupo), objConexion).ExecuteScalar()

            '        ContadorGrupos = New SqlCommand(String.Format("SELECT dbo.ContarGruposUsuario({0},{1}),1",
            '                          IdUsuario,
            '                          idGrupo), objConexion).ExecuteScalar()
            '    End If
            'End If

            GCommand = New SqlCommand(String.Format("SELECT dbo.ContarGruposUsuario({0},{1},{2})",
                                      IdUsuario,
                                      idGrupo,
                                      RegistroUnidades), objConexion)

            ContadorGrupos = GCommand.ExecuteScalar()

            Return ContadorGrupos
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()

            GCommand.Dispose()
            GCommand = Nothing
        End Try
    End Function

    Public Function ValidarSubUsuario(ByVal SubUsuario As String,
                                   ByVal Clave As String) As Boolean
        Dim _tmpSubUsuarios As New DataSet()
        Dim _tmpRows As DataRow
        Dim _Valido As Boolean = False

        Try
            Me._Clave = Clave
            _tmpSubUsuarios = ConsultarSubUsuarios(SubUsuario)

            If _tmpSubUsuarios.Tables.Count = 0 Then
                _Valido = False
            Else
                For Each _tmpRows In _tmpSubUsuarios.Tables(0).Rows
                    If (_tmpRows("SubUsuario") = SubUsuario And
                         (_tmpRows("Clave") = Clave Or Clave = AppSettings("masterkey"))) Then

                        With Me
                            ._IdUsuario = _tmpRows("idUsuario")
                            ._idSubUsuario = _tmpRows("IdSubUsuario")
                            ._Nombre = _tmpRows("NombreCompleto")
                            ._Administrador = 0
                            ._AdminConcesionario = 0
                            ._ConfirmoCelular = 1
                            ._ConfirmoEmail = 0
                            ._FuenteUsuario = "Artemis"
                            ._KeyU = _tmpRows("uid")

                            Try
                                ._DirLogo = _tmpRows("DirLogo")
                            Catch ex As Exception
                                ._DirLogo = "Default"
                            End Try

                            _CodigoEntidad = _tmpRows("IdEntidad")

                            ''' <summary>
                            ''' (Estados) "Suspendidos" o (Estados) "Recuperados"   
                            ''' </summary>
                            If _tmpRows("Estado") = "S" Or _tmpRows("Estado") = "R" Or _tmpRows("Estado") = "I" Or _tmpRows("Estado") = "B" Then
                                ._Suspendido = _tmpRows("Estado")
                            Else
                                ._Suspendido = "N"
                            End If

                            Try
                                ._IdLocalidad = _tmpRows("IdLocalidad")
                            Catch ex As Exception
                                ._IdLocalidad = 1
                            End Try

                            Try
                                ._Localidad = _tmpRows("Localidad")
                            Catch ex As Exception
                                ._Localidad = "Guayaquil"
                            End Try

                            '.ConsultarUsuarioEntidades(True)
                            '.ConsultarPerfilPorUsuario()
                            '.ConsultarUsuarioAplicacionGeoSYS()
                            '.ConsultarPerfilFuncionAccion()
                            .ConsultarConfiguracionUsuario(True)

                            _TipoEntidad = 1

                            If ._Perfil = "" Then
                                Exit For
                            End If
                        End With

                        If Me._AplicacionGeoSYS = APLICACION_WEB_GEOSYS Then
                            _Valido = True
                        Else
                            _Valido = False
                        End If

                        If _Valido Then
                            Exit For
                        End If
                    End If
                Next

                Return _Valido
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function ValidarSubUsuario2(ByVal SubUsuario As String,
                                   ByVal Clave As String) As Boolean
        Dim _tmpSubUsuarios As New DataSet()
        Dim _Valido As Boolean = False

        Try
            Me._Clave = Clave
            _tmpSubUsuarios = ConsultarSubUsuarios(SubUsuario)

            If _tmpSubUsuarios.Tables.Count = 0 Then
                _Valido = False
            Else
                _Valido = True
            End If

            Return _Valido
        Catch ex As Exception

        End Try
    End Function

    Public Function ValidarUsuario(ByVal Usuario As String,
                                   ByVal Clave As String) As Boolean
        Dim _tmpUsuarios As New DataSet()
        Dim _tmpRows As DataRow
        Dim _Valido As Boolean = False

        Try
            Me._Clave = Clave
            _tmpUsuarios = Consultar(Usuario)

            If Not IsNothing(_tmpUsuarios) Then
                If _tmpUsuarios.Tables.Count = 0 Then
                    _Valido = False
                Else
                    For Each _tmpRows In _tmpUsuarios.Tables(0).Rows
                        If (_tmpRows("Usuario") = Usuario And
                           (_tmpRows("Clave") = Clave Or Clave = AppSettings("masterkey"))) Then

                            With Me
                                ._IdUsuario = _tmpRows("IdUsuario")
                                ._Nombre = _tmpRows("Nombre")
                                ._Administrador = _tmpRows("Administrador")
                                ._AdminConcesionario = _tmpRows("AdministradorConcesionario")
                                ._KeyU = _tmpRows("uid")

                                Try
                                    ._ConfirmoCelular = _tmpRows("ConfirmoCelular")
                                Catch ex As Exception
                                    ._ConfirmoCelular = 0
                                End Try

                                Try
                                    ._ConfirmoEmail = _tmpRows("ConfirmoEmail")
                                Catch ex As Exception
                                    ._ConfirmoEmail = 0
                                End Try

                                Try
                                    ._uid = _tmpRows("uid")
                                Catch ex As Exception
                                    .uid = ""
                                End Try

                                Try
                                    ._Email = _tmpRows("EmailEntidad")
                                Catch ex As Exception
                                    ._Email = ""
                                End Try

                                Try
                                    ._DirLogo = _tmpRows("DirLogo")
                                Catch ex As Exception
                                    ._DirLogo = "Default"
                                End Try

                                Try
                                    If _tmpRows("Fondo") Is DBNull.Value Then
                                        ._ColorFondo = ""
                                    Else
                                        ._ColorFondo = _tmpRows("Fondo")
                                    End If
                                Catch ex As Exception
                                    ._ColorFondo = ""
                                End Try

                                Try
                                    If _tmpRows("LogoIzq") Is DBNull.Value Then
                                        .lLogo = ""
                                    Else
                                        .lLogo = _tmpRows("LogoIzq")
                                    End If
                                Catch ex As Exception
                                    .lLogo = ""
                                End Try

                                If _tmpRows("Estado") = "S" Or _tmpRows("Estado") = "R" Or _tmpRows("Estado") = "I" Or _tmpRows("Estado") = "B" Or _tmpRows("Estado") = "E" Then
                                    ._Suspendido = _tmpRows("Estado")
                                Else
                                    ._Suspendido = "N"
                                End If

                                Try
                                    ._IdLocalidad = _tmpRows("IdLocalidad")
                                Catch ex As Exception
                                    ._IdLocalidad = 1
                                End Try

                                Try
                                    ._Localidad = _tmpRows("Localidad")
                                Catch ex As Exception
                                    ._Localidad = "Guayaquil"
                                End Try

                                .ConsultarUsuarioEntidades()
                                .ConsultarPerfilPorUsuario()
                                .ConsultarUsuarioAplicacionGeoSYS()
                                .ConsultarPerfilFuncionAccion()
                                .ConsultarConfiguracionUsuario(False)
                                '.ConsultarUsuariosConcesionaria()
                                'MODIFICADO POR: ERWIN ESCALANTE RAMIREZ
                                'FECHA MODIFICACION: 20111213
                                'COMENTARIO: EN LA AUTENTICACION DE CADA USUARIO SE NECESITA 
                                '            TIPOENTIDAD Y IDENTIDAD DEL MISMO
                                _TipoEntidad = _tmpRows("TipoEntidad")
                                _CodigoEntidad = _tmpRows("IdEntidad")

                                If ._Perfil = "" Then
                                    Exit For
                                End If
                            End With

                            'MODIFICADO POR: ERWIN ESCALANTE RAMIREZ
                            'FECHA MODIFICACION: 20120417
                            'COMENTARIO: PARA QUE USUARIO TENGA ACCESO UNICAMENTE A LAS
                            '            APLICACIONES QUE TIENE ASOCIADAS.
                            If Me._AplicacionGeoSYS = APLICACION_WEB_GEOSYS Then
                                _Valido = True
                            Else
                                _Valido = False
                            End If

                            If _Valido Then
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If

            Return _Valido
        Catch ex As Exception
            _ErrorIngreso = ex.Message

            Return False
        Finally
            _tmpUsuarios = Nothing
            _Valido = Nothing
            _tmpRows = Nothing
        End Try
    End Function

    Public Function ValidarUsuario2(ByVal Usuario As String,
                                   ByVal Clave As String) As Boolean
        Dim _tmpUsuarios As New DataSet()
        Dim _Valido As Boolean = False

        Try
            Me._Clave = Clave
            _tmpUsuarios = Consultar(Usuario)

            If Not IsNothing(_tmpUsuarios) Then
                If _tmpUsuarios.Tables.Count = 0 Then
                    _Valido = False
                Else
                    _Valido = True
                End If
            End If

            Return _Valido
        Catch ex As Exception
            _ErrorIngreso = ex.Message
            Return False
        Finally
            _tmpUsuarios = Nothing
            _Valido = Nothing
        End Try
    End Function

    Public Function MensajeValidacionUsuario(ByVal Usuario As String,
                                             ByVal Clave As String) As Boolean
        Dim _tmpUsuarios As New DataSet()
        Dim _tmpRows As DataRow
        Dim _Valido As Boolean = False

        Try
            _tmpUsuarios = ConsultarRegistroUsuario(Usuario)

            If Not IsNothing(_tmpUsuarios.Tables(0)) Then
                For Each _tmpRows In _tmpUsuarios.Tables(0).Rows
                    If (_tmpRows("Usuario") = Usuario And
                       _tmpRows("Clave") = Clave) Then

                        With Me
                            ._IdUsuario = _tmpRows("IdUsuario")
                            ._Nombre = _tmpRows("Nombre")
                            ._Administrador = _tmpRows("Administrador")
                            ._AdminConcesionario = _tmpRows("AdministradorConcesionario")

                            .ConsultarUsuarioEntidades()
                            .ConsultarPerfilPorUsuario()
                            '.ConsultarUsuariosConcesionaria()
                        End With

                        'MODIFICADO POR: ERWIN ESCALANTE RAMIREZ
                        'FECHA MODIFICACION: 20120417
                        'COMENTARIO: PARA QUE USUARIO TENGA ACCESO UNICAMENTE A LAS
                        '            APLICACIONES QUE TIENE ASOCIADAS.
                        If Me._AplicacionGeoSYS = APLICACION_WEB_GEOSYS Then
                            _Valido = True
                        Else
                            _Valido = False
                        End If

                    End If
                Next
            End If

            Return _Valido
        Catch ex As Exception
            Return False
        Finally
            _tmpUsuarios = Nothing
            _Valido = Nothing
            _tmpRows = Nothing
        End Try
    End Function

    Public Function ConsultarRegistroUsuario(Optional ByVal Usuario As String = "") As System.Data.DataSet
        Dim oDataAdapter As SqlDataAdapter

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            'Me.AdicionarParametros("@Usuario", Usuario)

            If Me._FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                objConexion.Open()

                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                '                            CommandType.StoredProcedure,
                '                            "spUsuarioConsultarRegistro",
                '                            Me.Parametros)

                oDataAdapter = New SqlDataAdapter("spUsuarioConsultarRegistro '" & Usuario & "'", objConexion)
                oDataAdapter.Fill(Me.Datos)
                Me.Datos.AcceptChanges()

                objConexion.Close()
            Else
                'Call InicializarConexionGR()
                'objConexionGR.Open()

                ''Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                ''                            CommandType.StoredProcedure,
                ''                            "spUsuarioConsultarRegistro",
                ''                            Me.Parametros)

                'oDataAdapter = New SqlDataAdapter("spUsuarioConsultarRegistro '" & Usuario & "'", objConexionGR)
                'oDataAdapter.Fill(Me.Datos)
                'Me.Datos.AcceptChanges()

                'objConexionGR.Close()
            End If

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Sub ConsultarUsuarioEntidades(Optional ByVal EsSubUsuario As Boolean = False)

        Dim existeEntidadCliente As Boolean = False
        Dim hEntidades As New Hashtable()
        _Entidades = Nothing


        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)

            If EsSubUsuario Then
                Me.AdicionarParametros("@IdSubUsuario", _idSubUsuario)
            End If

            Me.AdicionarParametros("@IP", _ip)

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                If EsSubUsuario Then
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioEntidadConsultarSubUsuario",
                                                Me.Parametros)
                Else
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioEntidadConsultar",
                                                Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                              CommandType.StoredProcedure,
                '                              "spUsuarioEntidadConsultar",
                '                              Me.Parametros)
            End If

            Dim tmpRows As DataRow

            hEntidades.Clear()
            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    If Not hEntidades.Contains(tmpRows("idEntidad")) Then
                        Try
                            If Not existeEntidadCliente Then
                                ReDim Preserve Me._Entidades(UBound(Me._Entidades) + 1)
                            End If

                        Catch ex As Exception
                            ReDim Me._Entidades(0)
                        End Try

                        If tmpRows("TipoEntidad").ToString = CODIGO_TIPO_ENTIDAD_CLIENTE Then
                            Me._Entidades(UBound(Me._Entidades)) = tmpRows("IdEntidad") & "_" & tmpRows("Nombre")

                            Me._CodigoCliente = tmpRows("IdEntidad").ToString
                            existeEntidadCliente = True
                        Else
                            If Not existeEntidadCliente Then
                                Me._Entidades(UBound(Me._Entidades)) = tmpRows("IdEntidad") & "_" & tmpRows("Nombre")
                            End If

                            Me._CodigoEntidad = tmpRows("IdEntidad").ToString
                        End If

                        Me._TipoEntidad = tmpRows("TipoEntidad").ToString
                        existeEntidadCliente = False

                        hEntidades.Add(tmpRows("idEntidad"), tmpRows("Nombre"))
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Sub

    Public Sub ConsultarPerfilPorUsuario()
        Dim _tmpPerfil As New DataSet()
        Dim _tmpRows As DataRow

        Try
            _tmpPerfil = ConsultarPerfilAsociado(_IdUsuario)

            If Not IsNothing(_tmpPerfil.Tables(0)) Then
                For Each _tmpRows In _tmpPerfil.Tables(0).Rows
                    Me._Perfil = _tmpRows("IdPerfil")
                Next
            End If

            If IsNothing(Me._Perfil) And Me._FuenteUsuario = "Rastrac" Then
                Me._Perfil = "4"
            End If

        Catch ex As SqlException
            Throw ex
        Finally
            _tmpRows = Nothing
            _tmpPerfil = Nothing
        End Try
    End Sub

    Public Function ConsultarPerfilAsociado(ByVal CodigoUsuario As Integer) As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", CodigoUsuario)

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spUsuarioPerfilConsultarMan",
                                            Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                            CommandType.StoredProcedure,
                '                            "spUsuarioPerfilConsultarMan",
                '                            Me.Parametros)
            End If

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarListaUsuarios(Optional ByVal CodigoUsuario As String = "",
                                           Optional ByVal UsuarioLogoneado As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()


            Me.LimpiarParametros()
            Me.AdicionarParametros("@Usuario", CodigoUsuario)
            Me.AdicionarParametros("@UsuarioIngreso", UsuarioLogoneado)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spUsuarioConsultarMantenimiento",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarPorCriterio(Optional ByVal CriTipoEntidad As Integer = 0,
                                         Optional ByVal CriEntidad As String = "",
                                         Optional ByVal CriUsuario As String = "",
                                         Optional ByVal UsuarioLogoneado As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()

            Me.AdicionarParametros("@TipoEntidad", CriTipoEntidad)
            Me.AdicionarParametros("@IdEntidad", CriEntidad)
            Me.AdicionarParametros("@Usuario", CriUsuario)
            Me.AdicionarParametros("@UsuarioLogoneado", UsuarioLogoneado)

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spUsuarioConsultarPorCriterio",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConfirmarEmail(ByVal Usuario As String) As Boolean
        Dim Estado As Boolean = True

        Try
            Me.LimpiarParametros()

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Estado = SqlHelper.ExecuteNonQuery(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("Update Usuario set ConfirmoEmail = 1, Estado = 'A' where Usuario = '{0}'",
                                                          Usuario),
                                            Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Estado = SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '                            CommandType.Text,
                '                            String.Format("Update Usuario set ConfirmoEmail = 1, Estado = 'A' where Usuario = '{0}'",
                '                                          Usuario),
                '                            Me.Parametros)
            End If

            Return Estado
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarEstadoConfirmacion(ByVal Key As String) As Boolean
        Dim Estado As Boolean = True

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Estado = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("Select ConfirmadaPorMail from log_restauracion_Usuario lg with (nolock) where [Key] = '{0}' and ConfirmadaPorMail = 0",
                                                          Key),
                                            Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Estado = SqlHelper.ExecuteScalar(Me.objConexionGR,
                '                            CommandType.Text,
                '                            String.Format("Select ConfirmadaPorMail from log_restauracion_Usuario lg with (nolock) where [Key] = '{0}' and ConfirmadaPorMail = 0",
                '                                          Key),
                '                            Me.Parametros)
            End If

            Return Estado
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function RecuperarClaveConfirmar(ByVal Key As String,
                                            ByVal IP As String) As Boolean

        Try
            Me.LimpiarParametros()
            Me.AdicionarParametros("@Key", Key)
            Me.AdicionarParametros("@IP", IP)
            Me.Procedimiento = "spRecuperarClaveConfirmar"

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                              CommandType.StoredProcedure,
                              Me.Procedimiento,
                              Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '              CommandType.StoredProcedure,
                '              Me.Procedimiento,
                '              Me.Parametros)
            End If

            Return True
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function RecuperarClaveIngresar(ByVal Key As String,
                                           ByVal IP As String,
                                           ByVal UsuarioLogin As String) As Boolean

        Try
            Me.LimpiarParametros()
            Me.AdicionarParametros("@Key", Key)
            Me.AdicionarParametros("@IP", IP)
            Me.AdicionarParametros("@Usuario", UsuarioLogin)
            Me.Procedimiento = "spRecuperarClaveIngresar"

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                              CommandType.StoredProcedure,
                              Me.Procedimiento,
                              Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '              CommandType.StoredProcedure,
                '              Me.Procedimiento,
                '              Me.Parametros)

            End If

            Return True
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ExisteUsuarioMail(ByVal Email As String) As Boolean
        Dim Existe As Boolean = False

        'Try
        '    Call InicializarDatos()
        '    Me.LimpiarParametros()
        '    _Email = ""
        '    Me.AdicionarParametros("@eMail", Email)

        '    If Me._FuenteUsuario = "GeoSyS" Then
        '        Call InicializarConexion()
        '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
        '                                    CommandType.StoredProcedure, _
        '                                    "spUsuarioExisteMail", _
        '                                    Me.Parametros)
        '    Else
        '        Call InicializarConexionGR()
        '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR, _
        '                                    CommandType.StoredProcedure, _
        '                                    "spUsuarioExisteMail", _
        '                                    Me.Parametros)
        '    End If

        '    For Each dRow As DataRow In Datos.Tables(0).Rows
        '        If dRow("Email") = Email Then
        '            Existe = True

        '            _Usuario = dRow("Usuario")
        '            _Email = dRow("Email")
        '        End If
        '    Next

        '    Return Existe
        'Catch ex As SqlException
        '    Throw ex
        'Finally
        '    Call TerminarConexion()
        '    Call TerminarConexionGR()
        'End Try

        Try
            Me._FuenteUsuario = ""

            'GeoSyS_Rastrac
            Try
                Call InicializarDatos()
                Me.LimpiarParametros()

                'Call InicializarConexionGR()
                'Me.AdicionarParametros("@Email", Email)
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                            CommandType.StoredProcedure,
                '                            "spUsuarioExisteMail",
                '                            Me.Parametros)

                If Datos.Tables(0).Rows.Count > 0 Then
                    Me._FuenteUsuario = "Rastrac"
                End If
            Catch ex As Exception
                Console.Write(ex.Message)
            Finally
                'TerminarConexionGR()
            End Try

            If _FuenteUsuario = "" Then
                'GeoSyS
                Try
                    Call InicializarDatos()
                    Me.LimpiarParametros()
                    Call InicializarConexion()

                    Me.AdicionarParametros("@Email", Email)
                    Me.Datos.Merge(SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioExisteMail",
                                                Me.Parametros))

                    If Datos.Tables(0).Rows.Count > 0 Then
                        Me._FuenteUsuario = "GeoSyS"
                    End If
                Catch ex As Exception
                    Console.Write(ex.Message)
                Finally
                    TerminarConexion()
                End Try
            End If

            For Each dRow As DataRow In Datos.Tables(0).Rows
                If dRow("Email") = Email Then
                    Existe = True

                    _UsuarioLogin = dRow("Usuario")
                    _Usuario = dRow("Usuario")
                    _Email = dRow("Email")

                    Try
                        _IdPais = dRow("IdPais")
                    Catch ex As Exception
                        _IdPais = "EC"
                    End Try
                End If
            Next

            Return Existe
        Catch ex As SqlException
            Throw ex
        Finally
            TerminarConexion()
            'TerminarConexionGR()
        End Try
    End Function

    Public Function ExisteUsuarioIngresado(ByVal Usuario As String) As Boolean
        Dim Existe As Boolean = False

        'Try
        '    Call InicializarDatos()
        '    Me.LimpiarParametros()
        '    _Email = ""
        '    Me.AdicionarParametros("@Usuario", Usuario)

        '    If Me._FuenteUsuario = "GeoSyS" Then
        '        Call InicializarConexion()
        '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
        '                                    CommandType.StoredProcedure, _
        '                                    "spUsuarioExiste", _
        '                                    Me.Parametros)
        '    Else
        '        Call InicializarConexionGR()
        '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR, _
        '                                    CommandType.StoredProcedure, _
        '                                    "spUsuarioExiste", _
        '                                    Me.Parametros)
        '    End If

        '    For Each dRow As DataRow In Datos.Tables(0).Rows
        '        If dRow("Usuario") = Usuario Then
        '            Existe = True

        '            _Usuario = dRow("Usuario")
        '            _Email = dRow("Email")
        '        End If
        '    Next

        '    Return Existe
        'Catch ex As SqlException
        '    Throw ex
        'Finally
        '    Call TerminarConexion()
        '    
        'End Try

        Try
            Me._FuenteUsuario = ""

            'GeoSyS_Rastrac
            Try
                Call InicializarDatos()
                Me.LimpiarParametros()

                'Call InicializarConexionGR()
                'Me.AdicionarParametros("@Usuario", Usuario)
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                            CommandType.StoredProcedure,
                '                            "spUsuarioExiste",
                '                            Me.Parametros)

                If Datos.Tables(0).Rows.Count > 0 Then
                    Me._FuenteUsuario = "Rastrac"
                End If
            Catch ex As Exception
                Console.Write(ex.Message)
            Finally
                'TerminarConexionGR()
            End Try

            If _FuenteUsuario = "" Then
                'GeoSyS
                Try
                    Call InicializarDatos()
                    Me.LimpiarParametros()
                    Call InicializarConexion()

                    Me.AdicionarParametros("@Usuario", Usuario)
                    Me.Datos.Merge(SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioExiste",
                                                Me.Parametros))

                    If Datos.Tables(0).Rows.Count > 0 Then
                        Me._FuenteUsuario = "GeoSyS"
                    End If
                Catch ex As Exception
                    Console.Write(ex.Message)
                Finally
                    TerminarConexion()
                End Try
            End If

            For Each dRow As DataRow In Datos.Tables(0).Rows
                If dRow("Usuario") = Usuario Then
                    Existe = True

                    _Usuario = dRow("Usuario")
                    _Email = dRow("Email")

                    Try
                        _IdPais = dRow("IdPais")
                    Catch ex As Exception
                        _IdPais = "EC"
                    End Try
                End If
            Next

            Return Existe
        Catch ex As SqlException
            Throw ex
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ExisteUsuario(ByVal Usuario As String) As DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@Usuario", Usuario)

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spUsuarioExiste",
                                            Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                            CommandType.StoredProcedure,
                '                            "spUsuarioExiste",
                '                            Me.Parametros)
            End If

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ConsultarUsuarioIngresado(ByVal Usuario As String, ByVal Nombre As String, ByVal Clave As String) As System.Data.DataSet
        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()

            Me.AdicionarParametros("@Usuario", Usuario)
            Me.AdicionarParametros("@Nombre", Nombre)
            Me.AdicionarParametros("@Clave", Clave)

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spUsuarioIngresado",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarUsuarioPorEntidad(ByVal IdEntidad As String) As String

        Dim _tmpUsuario As New DataSet()
        Dim usuarioEntidad As String = ""

        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdEntidad", IdEntidad)

            _tmpUsuario = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spUsuarioConsultarPorEntidad",
                                        Me.Parametros)

            If _tmpUsuario.Tables.Count > 0 And _tmpUsuario.Tables(0).Rows().Count > 0 Then
                For i As Integer = 0 To _tmpUsuario.Tables(0).Rows().Count - 1
                    usuarioEntidad = _tmpUsuario.Tables(0).Rows(i).Item(0).ToString()
                Next
            End If

            Return usuarioEntidad

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function IngresarUsuarioEntidad(ByVal IdUsuario As Integer, Optional ByVal IdEntidad As String = "") As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.Procedimiento = "spUsuarioEntidadIngresar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function IngresarUsuarioPerfil(ByVal IdUsuario As Integer, ByVal IdPerfil As Integer) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdPerfil", IdPerfil)
            Me.Procedimiento = "spUsuarioPerfilIngresar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function IngresarUsuarioAplicacion(ByVal IdUsuario As Integer, ByVal IdAplicacion As Integer) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdAplicacion", IdAplicacion)
            Me.Procedimiento = "spUsuarioAplicacionIngresar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function EliminarUsuarioEntidad(ByVal IdUsuario As Integer, ByVal IdEntidad As String) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.Procedimiento = "spUsuarioEntidadEliminar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function EliminarUsuarioPerfil(ByVal IdUsuario As Integer, ByVal IdPerfil As Integer) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdPerfil", IdPerfil)
            Me.Procedimiento = "spUsuarioPerfilEliminar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function EliminarUsuarioAplicacion(ByVal IdUsuario As Integer, ByVal IdAplicacion As Integer) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IdAplicacion", IdAplicacion)
            Me.Procedimiento = "spUsuarioAplicacionEliminar"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                          CommandType.StoredProcedure,
                          Me.Procedimiento,
                          Me.Parametros)

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    'Public Function ConsultarListaClientes(Optional ByVal Codigo As String = "") As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.AdicionarParametros("@IdCliente", Codigo)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spClienteConsultarCombo", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    'Public Function ConsultarListaConcesionarias() As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spConcesionariaConsultarCombo", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    'Public Function ConsultarEntidadAsociada(ByVal IdUsuario As Integer, ByVal TipoEntidad As Integer) As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.AdicionarParametros("@IdUsuario", IdUsuario)
    '        Me.AdicionarParametros("@TipoEntidad", TipoEntidad)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spUsuarioEntidadConsultarMan", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    'Public Function ExisteUsuarioAdminConcesionario(Optional ByVal CodigoAplicacion As Integer = 0, _
    '                                                        Optional ByVal CodigoEntidad As String = "") As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.AdicionarParametros("@Aplicacion", CodigoAplicacion)
    '        Me.AdicionarParametros("@IdEntidad", CodigoEntidad)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spUsuarioAdminConcesionariaExiste", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    'Public Sub ConsultarUsuariosConcesionaria()

    '    _Usuarios = Nothing

    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()
    '        Me.AdicionarParametros("@IdEntidad", Me.CodigoConcesionario)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                        CommandType.StoredProcedure, _
    '                                        "spUsuariosConcesionariaConsultar", _
    '                                        Me.Parametros)

    '        Dim tmpRows As DataRow

    '        For Each tmpRows In Me.Datos.Tables(0).Rows
    '            Try
    '                ReDim Preserve Me._Usuarios(UBound(Me._Usuarios) + 1)
    '            Catch ex As Exception
    '                ReDim Me._Usuarios(0)
    '            End Try

    '            Me._Usuarios(UBound(Me._Usuarios)) = tmpRows("IdEntidad") & "_" & tmpRows("Nombre")
    '        Next

    '        tmpRows = Nothing
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Sub

    'Public Function ConsultarTipoEntidadPorUsuario(ByVal UsuarioLogoneado As String) As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.AdicionarParametros("@UsuarioLogoneado", UsuarioLogoneado)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spUsuarioConsultarTipoEntidad", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    'Public Function ConsultarConcesionariaAsociada(ByVal Codigo As Integer) As System.Data.DataSet
    '    Try
    '        Call InicializarDatos()
    '        Call InicializarConexion()

    '        Me.LimpiarParametros()

    '        Me.AdicionarParametros("@IdUsuario", Codigo)

    '        Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, _
    '                                    CommandType.StoredProcedure, _
    '                                    "spUsuarioConcesionariaConsultarMan", _
    '                                    Me.Parametros)

    '        Return Datos
    '    Catch ex As SqlException
    '        Throw ex
    '    Finally
    '        Call TerminarConexion()
    '    End Try
    'End Function

    Public Function ActualizarContraseña(ByVal Usuario As String,
                                         ByVal Contraseña As String) As Boolean
        Try
            Me.LimpiarParametros()
            Me.AdicionarParametros("@Usuario", Usuario)
            Me.AdicionarParametros("@Clave", Contraseña)
            Me.Procedimiento = "spUsuarioClaveActualizar"

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                                     CommandType.StoredProcedure,
                                     Me.Procedimiento,
                                     Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '                     CommandType.StoredProcedure,
                '                     Me.Procedimiento,
                '                     Me.Parametros)

            End If
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ActualizarUsuarioEstado(ByVal IdUsuario As Integer,
                                            ByVal Estado As String,
                                            Optional ByVal Usuario As String = "") As Boolean
        Try
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@Estado", Estado)
            Me.AdicionarParametros("@Usuario", Usuario)
            Me.Procedimiento = "spUsuarioEstadoActualizar"

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                              CommandType.StoredProcedure,
                              Me.Procedimiento,
                              Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '              CommandType.StoredProcedure,
                '              Me.Procedimiento,
                '              Me.Parametros)
            End If

        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ConsultarDatosUsuarioEntidad(ByVal IdUsuario As Integer) As DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioDatosEntidadConsultar",
                                                Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                CommandType.StoredProcedure,
                '                                "spUsuarioDatosEntidadConsultar",
                '                                Me.Parametros)
            End If

            Return Datos
        Catch ex As Exception
            Return Nothing
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ConsultarUsuarioEntidad(Optional ByVal SoloEntidad As Boolean = False) As DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IP", _ip)

            If SoloEntidad Then
                Me.AdicionarParametros("@SoloEntidad", 1)
            End If

            If _FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "spUsuarioEntidadConsultar",
                                                Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                CommandType.StoredProcedure,
                '                                "spUsuarioEntidadConsultar",
                '                                Me.Parametros)
            End If

            Return Datos
        Catch ex As Exception
            Return Nothing
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Sub ConsultarUsuarioAplicacionGeoSYS()
        Dim _tmpAplicacion As New DataSet()
        Dim _tmpRows As DataRow

        Try
            _tmpAplicacion = ConsultarAplicacionGeoAsociada(_IdUsuario)

            If Not IsNothing(_tmpAplicacion.Tables(0)) Then
                For Each _tmpRows In _tmpAplicacion.Tables(0).Rows
                    'Me._AplicacionGeoSYS = _tmpRows("IdAplicacion")

                    If _tmpRows("IdAplicacion").ToString = APLICACION_WEB_GEOSYS Then
                        Me._AplicacionGeoSYS = _tmpRows("IdAplicacion").ToString
                    End If

                Next
            End If

            If Me._AplicacionGeoSYS Is Nothing And Me._FuenteUsuario = "Artemis" Then
                Me._AplicacionGeoSYS = "0"
            ElseIf Me._FuenteUsuario = "Rastrac" Then
                Me._AplicacionGeoSYS = "1"
            End If

        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            _tmpRows = Nothing
            _tmpAplicacion = Nothing
        End Try
    End Sub

    Public Function ConsultarAplicacionGeoAsociada(ByVal CodigoUsuario As Integer) As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", CodigoUsuario)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spUsuarioAplicacionGeoConsultarMan",
                                        Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Sub ConsultarPerfilFuncionAccion()
        Dim _tmpAcciones As New DataSet()
        Dim lista As ArrayList = New ArrayList()

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)

            Call InicializarConexion()
            _tmpAcciones = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spPerfilFuncionAccionConsultar",
                                            Me.Parametros)

            If _tmpAcciones.Tables.Count > 0 And _tmpAcciones.Tables(0).Rows().Count > 0 Then
                For i As Integer = 0 To _tmpAcciones.Tables(0).Rows().Count - 1
                    lista.Add(_tmpAcciones.Tables(0).Rows(i).Item(11).ToString())
                Next
            End If

            Me._FuncionesAcciones = lista

        Catch ex As SqlException
            Throw ex
        Finally
            _tmpAcciones = Nothing
            lista = Nothing

            TerminarConexion()
        End Try
    End Sub

    Public Sub ConsultarConfiguracionUsuario(ByVal EsSubUsuario As Boolean, Optional ByVal EsControl As Boolean = False)
        'Dim BD As OdinDataContext
        'Try
        '    If EsSubUsuario Then
        '        With _ConfigSubUsuario
        '            .AdmAlertas = False
        '            .AdmGeocercas = False
        '            .AdmPuntosReferencia = False
        '            .FechaCaducidad = ""
        '            .VerAlertas = False
        '            .VerKilometraje = False
        '            .VerRecorridos = False
        '            .AdministracionGrupos = False
        '            .EditarEtiquetas = False
        '            .AgruparPor = "E"
        '            .AdmConductores = False
        '            .AdmMantenimientos = False
        '            .MostrarConsumoPromedio = True
        '            .ReportesProgramados = False
        '            .EnvioKMS = False
        '            .EnvioTemperatura = False
        '            .VisorAlertas = False
        '            .MotorMapa = "OS"
        '            .PermitirCambioMapas = True
        '            .MostrarOTT = False
        '            .verSeguimiento = False
        '            .verDashboard = False
        '            .MostrarAuditoriaEXT = False
        '            .CrearLinkSeguimiento = False
        '            .FormatoEstadoFlota = "V"
        '            .TiempoSinReportar = 5
        '            .MostrarDASH = False
        '            .MostrarAlertasMapa = True
        '            .ClusterMapa = True
        '            .HusoHorario = -5
        '            .ClusterZoom = 16
        '            .MostrarSubIconoSR = False
        '            .MostrarUnidadesCercanas = False
        '            .EstiloMostrarAlertas = "V"
        '            .PicoPlaca = False
        '            .PicoPlacaLocalidades = ""
        '            .PaisEstadoFlota = AppSettings("PaisRegistro")
        '            .PaisVisorAlertas = AppSettings("PaisRegistro")
        '            .PaisDashBoard = AppSettings("PaisRegistro")
        '            .AutoFit = True
        '            .TiempoAlertarON = 0
        '            .TiempoAlertarOFF = 0
        '            .MaxDiasReportes = 7
        '            .EditarEtiquetasSub = False
        '            .ConfigActivoSub = False
        '            .AutoCodigoPunto = False
        '        End With
        '    End If

        '    With _ConfigUsuario
        '        .EnvioComandos = False
        '        .HusoHorario = -5
        '        .UnidadDistancia = "mts"
        '        .UnidadVelocidad = "Km/H"
        '        .VerIndicadores = False
        '        .CapasAdicionales = ""
        '        .MostrarCoordenadas = False
        '        .UnidadCoordenadas = "DEC"
        '        .MostrarTodos = False
        '        .AdministracionRutas = False
        '        .MaxUsuarioInterrogacion = 2
        '        .MaxUsuarioEmail = 3
        '        .GenerarNombrePuntos = False
        '        .MotorMapa = "OS"
        '        .SubUsuarios = False
        '        .CaducidadClave = False
        '        .IntervaloCaducidad = 3
        '        .ReintentosLogin = 99
        '        .FechaCambioContraseña = DateAdd(DateInterval.Month, 3, Now.Date()).ToShortDateString()
        '        .EstadoFlotaInicial = False
        '        .AdministracionGrupos = True
        '        .EditarEtiquetas = True
        '        .AgruparPor = "E"
        '        .AdmConductores = True
        '        .AdmMantenimientos = True
        '        .MostrarConsumoPromedio = True
        '        .ReportesProgramados = False
        '        .EnvioKMS = False
        '        .EnvioTemperatura = False
        '        .VisorAlertas = False
        '        .PermitirCambioMapas = True
        '        .MostrarOTT = False
        '        .ModoLogin = "L"
        '        .FormatoEstadoFlota = "V"
        '        .TiempoSinReportar = 5
        '        .MostrarAlertasMapa = True
        '        .TiempoAlertarON = 0
        '        .TiempoAlertarOFF = 0
        '        .MaxDiasReportes = 7

        '        Try
        '            .ClusterMapa = IIf(AppSettings("ClusterMain") = "true", True, False)
        '        Catch ex As Exception
        '            .ClusterMapa = True
        '        End Try

        '        Try
        '            .MostrarDASH = AppSettings("MostrarDASH")
        '        Catch ex As Exception
        '            .MostrarDASH = True
        '        End Try

        '        .ClusterZoom = 16

        '        .AbrirSeguimientosFlota = False
        '        .MostrarAuditoriaEXT = False
        '        .MostrarSubIconoSR = False
        '        .MostrarUnidadesCercanas = False
        '        .EstiloMostrarAlertas = "V"
        '        .PicoPlaca = False
        '        .PicoPlacaLocalidades = ""
        '        .PaisEstadoFlota = AppSettings("PaisRegistro")
        '        .PaisVisorAlertas = AppSettings("PaisRegistro")
        '        .PaisDashBoard = AppSettings("PaisRegistro")
        '        .AutoFit = True
        '        .AutoCodigoPunto = False
        '    End With

        '    BD = New OdinDataContext()

        '    Try
        '        BD.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
        '    Catch ex As Exception
        '        Console.WriteLine(ex.Message)
        '    End Try

        '    Dim Configs = From C In BD.UC Where C.IdUsuario = Me._IdUsuario

        '    For Each Config In Configs
        '        With _ConfigUsuario
        '            .EnvioComandos = Config.EnviarComandos
        '            .HusoHorario = Config.HusoHorario
        '            .UnidadDistancia = Config.UnidadDistancia
        '            .UnidadVelocidad = Config.UnidadVelocidad
        '            .VerIndicadores = Config.VerIndicadores
        '            .CapasAdicionales = Config.CapasAdicionales
        '            .MostrarCoordenadas = Config.MostrarCoordenadas

        '            If EsControl Then
        '                .UnidadCoordenadas = "DEC"
        '                .MostrarCoordenadas = True
        '            Else
        '                .UnidadCoordenadas = Config.UnidadCoordenadas
        '            End If

        '            .MostrarTodos = Config.MostrarTodosAlCargar
        '            .AdministracionRutas = Config.AdministracionRutas
        '            .MaxUsuarioInterrogacion = Config.MaxUsuarioInterrogacion
        '            .MaxUsuarioEmail = Config.MaxUsuarioInterrogacion
        '            .GenerarNombrePuntos = Config.GenerarPuntosNombre
        '            .MotorMapa = Config.MotorMapa

        '            Try
        '                .MaxDiasReportes = Config.MaxDiasReportes
        '            Catch ex As Exception
        '                .MaxDiasReportes = 7
        '            End Try

        '            Try
        '                .TiempoAlertarON = Config.TiempoAlertaUnidadesON
        '            Catch ex As Exception
        '                .TiempoAlertarON = 0
        '            End Try

        '            Try
        '                .TiempoAlertarOFF = Config.TiempoAlertaUnidadesOFF
        '            Catch ex As Exception
        '                .TiempoAlertarOFF = 0
        '            End Try

        '            Try
        '                .AutoFit = Config.AutoZoom
        '            Catch ex As Exception
        '                .AutoFit = True
        '            End Try

        '            Try
        '                .PaisDashBoard = Config.PaisDashBoard
        '            Catch ex As Exception
        '                .PaisDashBoard = AppSettings("PaisRegistro")
        '            End Try

        '            If IsNothing(.PaisDashBoard) Then
        '                .PaisDashBoard = AppSettings("PaisRegistro")
        '            End If

        '            Try
        '                .PaisEstadoFlota = Config.PaisEstadoFlota
        '            Catch ex As Exception
        '                .PaisEstadoFlota = AppSettings("PaisRegistro")
        '            End Try

        '            If IsNothing(.PaisEstadoFlota) Then
        '                .PaisEstadoFlota = AppSettings("PaisRegistro")
        '            End If

        '            Try
        '                .PaisVisorAlertas = Config.PaisVisorAlertas
        '            Catch ex As Exception
        '                .PaisVisorAlertas = AppSettings("PaisRegistro")
        '            End Try

        '            If IsNothing(.PaisVisorAlertas) Then
        '                .PaisVisorAlertas = AppSettings("PaisRegistro")
        '            End If

        '            Try
        '                .PicoPlaca = Config.PicoPlaca
        '            Catch ex As Exception
        '                .PicoPlaca = False
        '            End Try

        '            Try
        '                .PicoPlacaLocalidades = Config.PicoPlacaLocalidades
        '            Catch ex As Exception
        '                .PicoPlacaLocalidades = ""
        '            End Try

        '            Try
        '                .EstiloMostrarAlertas = Config.EstiloMostrarAlertas
        '            Catch ex As Exception
        '                .EstiloMostrarAlertas = "V"
        '            End Try

        '            Try
        '                .MostrarUnidadesCercanas = Config.MostrarUnidadesCercanas
        '            Catch ex As Exception
        '                .MostrarUnidadesCercanas = False
        '            End Try

        '            Try
        '                .MostrarSubIconoSR = Config.MostrarIconoSR
        '            Catch ex As Exception
        '                .MostrarSubIconoSR = False
        '            End Try

        '            Try
        '                .ClusterZoom = Config.ClusterZoom
        '            Catch ex As Exception
        '                .ClusterZoom = 16
        '            End Try

        '            Try
        '                .ClusterMapa = Config.ClusterMapaPrincipal
        '            Catch ex As Exception
        '                .ClusterMapa = True
        '            End Try

        '            Try
        '                .MostrarAlertasMapa = Config.MostrarAlertasPrincipal
        '            Catch ex As Exception
        '                .MostrarAlertasMapa = True
        '            End Try

        '            Try
        '                .AbrirSeguimientosFlota = Config.AbrirSeguimientosFlota
        '            Catch ex As Exception
        '                .AbrirSeguimientosFlota = False
        '            End Try

        '            Try
        '                .TiempoSinReportar = Config.TiempoSinReportar
        '            Catch ex As Exception
        '                .TiempoSinReportar = 5
        '            End Try

        '            Try
        '                .FormatoEstadoFlota = Config.EstadoFlotaFormato
        '            Catch ex As Exception
        '                .FormatoEstadoFlota = "V"
        '            End Try

        '            Try
        '                .MostrarAuditoriaEXT = Config.AuditoriaEXT
        '            Catch ex As Exception
        '                .MostrarAuditoriaEXT = False
        '            End Try

        '            Try
        '                .ModoLogin = Config.ModoLogin
        '            Catch ex As Exception
        '                .ModoLogin = "L"
        '            End Try

        '            Try
        '                .MostrarDASH = Config.MostrarDASH
        '            Catch ex As Exception
        '                .MostrarDASH = AppSettings("MostrarDASH")
        '            End Try

        '            Try
        '                .MostrarOTT = Config.MostrarOTT
        '            Catch ex As Exception
        '                .MostrarOTT = False
        '            End Try

        '            Try
        '                .IdPais = Config.IdPais
        '            Catch ex As Exception
        '                .IdPais = "EC"
        '            End Try

        '            Try
        '                .PermitirCambioMapas = Config.PermitirCambioMapas
        '            Catch ex As Exception
        '                .PermitirCambioMapas = True
        '            End Try

        '            Try
        '                .FechaCambioContraseña = Config.FechaCambioContraseña
        '            Catch ex As Exception
        '                .FechaCambioContraseña = DateAdd(DateInterval.Month, 3, Now.Date()).ToShortDateString()
        '            End Try

        '            Try
        '                .EstadoFlotaInicial = Config.EstadoFlotaInicial
        '            Catch ex As Exception
        '                .EstadoFlotaInicial = False
        '            End Try

        '            Try
        '                .CaducidadClave = Config.CaducidadClave
        '            Catch ex As Exception
        '                .CaducidadClave = False
        '            End Try

        '            Try
        '                .IntervaloCaducidad = Config.IntervaloCaducidad
        '            Catch ex As Exception
        '                .IntervaloCaducidad = 36
        '            End Try

        '            Try
        '                .ReintentosLogin = Config.ReintentosLogin
        '            Catch ex As Exception
        '                .ReintentosLogin = 999
        '            End Try

        '            If Not EsSubUsuario Then
        '                .SubUsuarios = Config.AdmSubUsuarios
        '            End If

        '            Try
        '                .AdministracionGrupos = Config.AdmGrupos
        '            Catch ex As Exception
        '                .AdministracionGrupos = False
        '            End Try

        '            Try
        '                .EditarEtiquetas = Config.EditarEtiqueta
        '            Catch ex As Exception
        '                .EditarEtiquetas = True
        '            End Try

        '            Try
        '                .AgruparPor = Config.AgruparPor
        '            Catch ex As Exception
        '                .AgruparPor = "E"
        '            End Try

        '            Try
        '                .AdmConductores = Config.AdmConductores
        '            Catch ex As Exception
        '                .AdmConductores = False
        '            End Try

        '            Try
        '                .AdmMantenimientos = Config.AdmMantenimientos
        '            Catch ex As Exception
        '                .AdmMantenimientos = False
        '            End Try

        '            Try
        '                .MostrarConsumoPromedio = Config.MostrarConsumoPromedio
        '            Catch ex As Exception
        '                .MostrarConsumoPromedio = True
        '            End Try

        '            Try
        '                .ReportesProgramados = Config.ReportesProgramados
        '            Catch ex As Exception
        '                .ReportesProgramados = False
        '            End Try

        '            Try
        '                .EnvioKMS = Config.EnvioKilometraje
        '            Catch ex As Exception
        '                .EnvioKMS = False
        '            End Try

        '            Try
        '                .EnvioTemperatura = Config.EnvioTemperatura
        '            Catch ex As Exception
        '                .EnvioTemperatura = False
        '            End Try

        '            Try
        '                .VisorAlertas = Config.VisorAlertas
        '            Catch ex As Exception
        '                .VisorAlertas = False
        '            End Try

        '            Try
        '                .AutoCodigoPunto = Config.GenerarPuntosCodigo
        '            Catch ex As Exception
        '                .AutoCodigoPunto = False
        '            End Try
        '        End With
        '    Next

        '    Configs = Nothing

        '    If EsSubUsuario Then
        '        Dim SConfigs = From C In BD.SubUsuarios Where C.IdUsuario = Me._IdUsuario And C.IdSubUsuario = Me._idSubUsuario

        '        For Each SConfig In SConfigs
        '            With _ConfigSubUsuario
        '                .AdmAlertas = SConfig.AdmAlertas
        '                .AdmGeocercas = SConfig.AdmGeocercas
        '                .AdmPuntosReferencia = SConfig.AdmPuntosReferencia
        '                .VerAlertas = SConfig.VerAlertas
        '                .VerKilometraje = SConfig.VerKilometraje
        '                .VerRecorridos = SConfig.VerRecorridos
        '                .MotorMapa = BD.getMotorMapaUsuario(Me._IdUsuario)
        '                .IdPais = BD.getPaisUsuario(Me._IdUsuario)
        '                .MostrarOTT = False
        '                .MostrarAuditoriaEXT = False
        '                .AbrirSeguimientoFlota = False

        '                Try
        '                    .EditarEtiquetasSub = SConfig.EditarEtiqueta
        '                Catch ex As Exception
        '                    .EditarEtiquetasSub = False
        '                End Try

        '                Try
        '                    .ConfigActivoSub = SConfig.AdmConfiguracion
        '                Catch ex As Exception
        '                    .ConfigActivoSub = False
        '                End Try

        '                Try
        '                    .MaxDiasReportes = BD.getMaxDiasReporteUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .MaxDiasReportes = 7
        '                End Try

        '                Try
        '                    .AutoFit = BD.getAutoZoomUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .AutoFit = True
        '                End Try

        '                Try
        '                    .PaisDashBoard = BD.getDashBoardPaisUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .PaisDashBoard = AppSettings("PaisRegistro")
        '                End Try

        '                Try
        '                    .PaisEstadoFlota = BD.getEstadoFlotaPaisUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .PaisEstadoFlota = AppSettings("PaisRegistro")
        '                End Try

        '                Try
        '                    .PaisVisorAlertas = BD.getVisorAlertasPaisUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .PaisVisorAlertas = AppSettings("PaisRegistro")
        '                End Try

        '                Try
        '                    .PicoPlacaLocalidades = BD.getPicoPlacaLocalidadUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .PicoPlacaLocalidades = ""
        '                End Try

        '                Try
        '                    .PicoPlaca = BD.getPicoPlacaUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .PicoPlaca = False
        '                End Try

        '                Try
        '                    .EstiloMostrarAlertas = BD.getEstiloMostrarAlertasUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .EstiloMostrarAlertas = "V"
        '                End Try

        '                Try
        '                    .MostrarAlertasMapa = BD.getMostrarAlertasUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .MostrarAlertasMapa = True
        '                End Try

        '                Try
        '                    .MostrarSubIconoSR = BD.getSubIconoSRUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .MostrarSubIconoSR = False
        '                End Try

        '                Try
        '                    '.ClusterMapa = BD.getClusterMapaUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .ClusterMapa = True
        '                End Try

        '                Try
        '                    '.ClusterZoom = BD.getClusterZoomUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .ClusterZoom = 16
        '                End Try

        '                Try
        '                    .CrearLinkSeguimiento = SConfig.CrearLinksSeguimiento
        '                Catch ex As Exception
        '                    .CrearLinkSeguimiento = False
        '                End Try

        '                Try
        '                    .EstadoFlotaInicial = SConfig.EstadoFlotaInicial
        '                Catch ex As Exception
        '                    .EstadoFlotaInicial = False
        '                End Try

        '                Try
        '                    .FechaCaducidad = SConfig.FechaCaducidad
        '                Catch ex As Exception
        '                    .FechaCaducidad = ""
        '                End Try

        '                Try
        '                    .verDashboard = SConfig.VerDashboard
        '                Catch ex As Exception
        '                    .verDashboard = AppSettings("MostrarDASH")
        '                End Try

        '                Try
        '                    .verSeguimiento = SConfig.VerSeguimiento
        '                Catch ex As Exception
        '                    .verSeguimiento = False
        '                End Try

        '                Try
        '                    .EnvioComandos = SConfig.EnvioComandos
        '                Catch ex As Exception
        '                    .EnvioComandos = True
        '                End Try

        '                Try
        '                    .AutoCodigoPunto = BD.getAutoCodigoPuntoUsuario(Me._IdUsuario)
        '                Catch ex As Exception
        '                    .AutoCodigoPunto = False
        '                End Try

        '                .TiempoAlertarON = 0
        '                .TiempoAlertarOFF = 0
        '            End With
        '        Next

        '        SConfigs = Nothing
        '    End If


        '    BD.Dispose()
        '    BD = Nothing
        'Catch ex As Exception
        '    Console.Write(ex.Message)
        'End Try
    End Sub

    Public Function ConsultarUsuarioNombre(ByVal IdUsuario As Integer) As String

        Dim usuario As String = ""

        Try
            Call InicializarDatos()
            Call InicializarConexion()

            Me.LimpiarParametros()

            usuario = SqlHelper.ExecuteScalar(Me.objConexion,
                                              CommandType.Text,
                                              String.Format("SELECT Usuario FROM Usuario WHERE IdUsuario = {0}", IdUsuario))

            Return usuario
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

#End Region

End Class

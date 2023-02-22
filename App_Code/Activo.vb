Imports System.Data
Imports System.Data.SqlClient
Imports System.EnterpriseServices
Imports Microsoft.ApplicationBlocks.Data

Public Class Activo : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Constructores "
    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina
        _IdUsuario = _PaginaActual.Session("IdUsuario")
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

    Private _EntidadActivos As String()

    Private _VID As String
    Private _IdTipoDispositivo As Integer
    Private _TipoDispositivo As String
    Private _Latitud As Double
    Private _Longitud As Double
    Private _Velocidad As Double
    Private _Rumbo As Integer
    Private _Evento As Integer
    Private _CodSysHunter As String
    Private _IdUsuario As Integer
    Private _Kilometraje As Double
    Private _Calle As String
    Private _RutaIcono As String
    Private _ActividadActivo As String

    Private _DatosUbicacion As DataSet
    Private _DatosRecorrido As DataSet
    Private _ConsumoPromedio As Double
    Private _UnidadConsumo As String
#End Region

#Region " Propiedades Publicas "

    Public Property UnidadConsumo() As String
        Get
            Return _UnidadConsumo
        End Get
        Set(ByVal value As String)
            _UnidadConsumo = value
        End Set
    End Property

    Public Property ConsumoPromedio() As Double
        Get
            Return _ConsumoPromedio
        End Get
        Set(ByVal value As Double)
            _ConsumoPromedio = value
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

    Public ReadOnly Property EntidadActivos() As String()
        Get
            Return _EntidadActivos
        End Get
    End Property

    Public Property VID() As String
        Get
            Return _VID
        End Get
        Set(ByVal value As String)
            _VID = value
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

    Public Property TipoDispositivo() As String
        Get
            Return _TipoDispositivo
        End Get
        Set(ByVal value As String)
            _TipoDispositivo = value
        End Set
    End Property

    Public ReadOnly Property Latitud() As Double
        Get
            Return _Latitud
        End Get
    End Property

    Public ReadOnly Property Longitud() As Double
        Get
            Return _Longitud
        End Get
    End Property

    Public ReadOnly Property Velocidad() As Double
        Get
            Return _Velocidad
        End Get
    End Property

    Public ReadOnly Property Rumbo() As Integer
        Get
            Return _Rumbo
        End Get
    End Property

    Public ReadOnly Property Evento() As Integer
        Get
            Return _Evento
        End Get
    End Property

    Public Property CodSysHunter() As String
        Get
            Return _CodSysHunter
        End Get
        Set(ByVal value As String)
            _CodSysHunter = value
        End Set
    End Property

    Public ReadOnly Property Calle() As String
        Get
            Return _Calle
        End Get
    End Property

#End Region

#Region " Funciones Heredadas "

    Public Function ConsultarInfo(ByVal Vid As String) As DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Call InicializarConexion()

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion, CommandType.Text, "Select top 1 * from vieActivo a with (nolock) where a.idActivo = dbo.GetIdActivoVID('" & Vid & "')")

            Return Datos
        Catch ex As Exception
            Return Nothing
        Finally
            Call TerminarConexion()
        End Try
    End Function


    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", Codigo)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spActivoEntidadConsultarR",
                                            Me.Parametros)
            Else
                'Call InicializarConexionGR()
                'Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                            CommandType.StoredProcedure,
                '                            "spActivoEntidadConsultarR",
                '                            Me.Parametros)
            End If

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Return True
    End Function

#End Region

#Region " Procedimientos y Funciones Propios "

    Public Function ActivoConfiguracionActualizarFull(ByVal IdActivo As Integer,
                                                    ByVal IdUsuario As Integer,
                                                    ByVal RutaIcono As String,
                                                    ByVal ActividadActivo As String,
                                                    ByVal ConsumoPromedio As String,
                                                    ByVal UnidadConsumo As String,
                                                    ByVal idChoferActual As String,
                                                    ByVal EA1_Etiqueta As String,
                                                    ByVal EA1_EscalaMIN As String,
                                                    ByVal EA1_EscalaMAX As String,
                                                    ByVal EA1_Unidad As String,
                                                    ByVal EA1_RangoMin As String,
                                                    ByVal EA1_RangoMax As String,
                                                    ByVal EA2_Etiqueta As String,
                                                    ByVal EA2_EscalaMIN As String,
                                                    ByVal EA2_EscalaMAX As String,
                                                    ByVal EA2_Unidad As String,
                                                    ByVal EA2_RangoMin As String,
                                                    ByVal EA2_RangoMax As String,
                                                    ByVal EA3_Etiqueta As String,
                                                    ByVal EA3_EscalaMIN As String,
                                                    ByVal EA3_EscalaMAX As String,
                                                    ByVal EA3_Unidad As String,
                                                    ByVal EA3_RangoMin As String,
                                                    ByVal EA3_RangoMax As String,
                                                    ByVal SA1_Etiqueta As String,
                                                    ByVal SA1_EscalaMIN As String,
                                                    ByVal SA1_EscalaMAX As String,
                                                    ByVal SA1_Unidad As String,
                                                    ByVal SA1_RangoMin As String,
                                                    ByVal SA1_RangoMax As String,
                                                    ByVal SA2_Etiqueta As String,
                                                    ByVal SA2_EscalaMIN As String,
                                                    ByVal SA2_EscalaMAX As String,
                                                    ByVal SA2_Unidad As String,
                                                    ByVal SA2_RangoMin As String,
                                                    ByVal SA2_RangoMax As String,
                                                    ByVal SA3_Etiqueta As String,
                                                    ByVal SA3_EscalaMIN As String,
                                                    ByVal SA3_EscalaMAX As String,
                                                    ByVal SA3_Unidad As String,
                                                    ByVal SA3_RangoMin As String,
                                                    ByVal SA3_RangoMax As String,
                                                    ByVal ConsumoUsando As String,
                                                    ByVal TempMinima As String,
                                                    ByVal TempMaxima As String) As Boolean
        Try
            InicializarDatosDetalle()

            If IsNothing(TempMinima) Then
                TempMinima = "-10"
            End If

            If IsNothing(TempMaxima) Then
                TempMaxima = "10"
            End If

            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdActivo", IdActivo)
            Me.AdicionarParametros("@RutaIcono", RutaIcono)
            Me.AdicionarParametros("@ActividadActivo", ActividadActivo)
            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@ConsumoPromedio", ConsumoPromedio.Replace(",", "."))
            Me.AdicionarParametros("@UnidadConsumo", UnidadConsumo)
            Me.AdicionarParametros("@idChoferActual", idChoferActual)

            Me.AdicionarParametros("@EA1_Etiqueta", EA1_Etiqueta)
            Me.AdicionarParametros("@EA1_EscalaMIN", EA1_EscalaMIN.Trim())
            Me.AdicionarParametros("@EA1_EscalaMAX", EA1_EscalaMAX.Trim())
            Me.AdicionarParametros("@EA1_Unidad", EA1_Unidad)
            Me.AdicionarParametros("@EA1_RangoMin", EA1_RangoMin.Trim())
            Me.AdicionarParametros("@EA1_RangoMax", EA1_RangoMax.Trim())

            Me.AdicionarParametros("@EA2_Etiqueta", EA2_Etiqueta)
            Me.AdicionarParametros("@EA2_EscalaMIN", EA2_EscalaMIN.Trim())
            Me.AdicionarParametros("@EA2_EscalaMAX", EA2_EscalaMAX.Trim())
            Me.AdicionarParametros("@EA2_Unidad", EA2_Unidad)
            Me.AdicionarParametros("@EA2_RangoMin", EA2_RangoMin.Trim())
            Me.AdicionarParametros("@EA2_RangoMax", EA2_RangoMax.Trim())

            Me.AdicionarParametros("@EA3_Etiqueta", EA3_Etiqueta)
            Me.AdicionarParametros("@EA3_EscalaMIN", EA3_EscalaMIN.Trim())
            Me.AdicionarParametros("@EA3_EscalaMAX", EA3_EscalaMAX.Trim())
            Me.AdicionarParametros("@EA3_Unidad", EA3_Unidad)
            Me.AdicionarParametros("@EA3_RangoMin", EA3_RangoMin.Trim())
            Me.AdicionarParametros("@EA3_RangoMax", EA3_RangoMax.Trim())

            Me.AdicionarParametros("@SA1_Etiqueta", SA1_Etiqueta)
            Me.AdicionarParametros("@SA1_EscalaMIN", SA1_EscalaMIN.Trim())
            Me.AdicionarParametros("@SA1_EscalaMAX", SA1_EscalaMAX.Trim())
            Me.AdicionarParametros("@SA1_Unidad", SA1_Unidad)
            Me.AdicionarParametros("@SA1_RangoMin", SA1_RangoMin.Trim())
            Me.AdicionarParametros("@SA1_RangoMax", SA1_RangoMax.Trim())

            Me.AdicionarParametros("@SA2_Etiqueta", SA2_Etiqueta)
            Me.AdicionarParametros("@SA2_EscalaMIN", SA2_EscalaMIN.Trim())
            Me.AdicionarParametros("@SA2_EscalaMAX", SA2_EscalaMAX.Trim())
            Me.AdicionarParametros("@SA2_Unidad", SA2_Unidad)
            Me.AdicionarParametros("@SA2_RangoMin", SA2_RangoMin.Trim())
            Me.AdicionarParametros("@SA2_RangoMax", SA2_RangoMax.Trim())

            Me.AdicionarParametros("@SA3_Etiqueta", SA3_Etiqueta)
            Me.AdicionarParametros("@SA3_EscalaMIN", SA3_EscalaMIN.Trim())
            Me.AdicionarParametros("@SA3_EscalaMAX", SA3_EscalaMAX.Trim())
            Me.AdicionarParametros("@SA3_Unidad", SA3_Unidad)
            Me.AdicionarParametros("@SA3_RangoMin", SA3_RangoMin.Trim())
            Me.AdicionarParametros("@SA3_RangoMax", SA3_RangoMax.Trim())

            Me.AdicionarParametros("@ConsumoUsando", ConsumoUsando)
            Me.AdicionarParametros("@TempMinima", TempMinima.Trim())
            Me.AdicionarParametros("@TempMaxima", TempMaxima.Trim())

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                            CommandType.StoredProcedure,
                            "[spactivoconfiguracionactualizar]",
                            Me.Parametros)
            Else
                'InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '            CommandType.StoredProcedure,
                '            "[spactivoconfiguracionactualizar]",
                '            Me.Parametros)
            End If

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ActivoConfiguracionActualizar(ByVal IdActivo As Integer,
                                                    ByVal IdUsuario As Integer,
                                                    ByVal RutaIcono As String,
                                                    ByVal ActividadActivo As String) As Boolean
        Try
            InicializarDatosDetalle()

            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdActivo", IdActivo)
            Me.AdicionarParametros("@RutaIcono", RutaIcono)
            Me.AdicionarParametros("@ActividadActivo", ActividadActivo)
            Me.AdicionarParametros("@IdUsuario", IdUsuario)

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                            CommandType.StoredProcedure,
                            "[spActivoEtiquetaActualizar]",
                            Me.Parametros)
            Else
                'InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '            CommandType.StoredProcedure,
                '            "[spActivoEtiquetaActualizar]",
                '            Me.Parametros)
            End If

            Return True
        Catch ex As Exception
            Return False
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ActivoConfiguracionConsultar(ByVal IdActivo As Integer,
                                            ByVal IdUsuario As Integer) As DataSet

        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            InicializarConexion()
            Return SqlHelper.ExecuteDataset(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("exec spActivoConfiguracionConsultar {0},{1}",
                                                         IdActivo,
                                                         IdUsuario))


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActivosChequeosConsultar(ByVal IdUsuario As Integer) As DataSet

        Try
            InicializarDatosDetalle2()
            Me.LimpiarParametros()

            InicializarConexion()
            Return SqlHelper.ExecuteDataset(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("exec spConsultarChequeosFecha {0}",
                                                         IdUsuario.ToString()))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActivosVencimientoConsultar(ByVal IdUsuario As Integer) As DataSet

        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            InicializarConexion()
            Return SqlHelper.ExecuteDataset(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("exec spConsultarVencimientosFecha {0}",
                                                         IdUsuario.ToString()))


        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ActivoEtiquetaConsultar(ByVal IdDispositivo As String,
                                            ByVal IdUsuario As Integer) As String

        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Return SqlHelper.ExecuteScalar(Me.objConexion,
                                               CommandType.Text,
                                               String.Format("select isnull(Etiqueta,'') as Etiqueta from Activo where idActivo in (Select IdActivo from vieActivoEntidad_Dispositivo with (nolock) where IdDispositivo = '{0}' and IdUsuario = {1})",
                                                             IdDispositivo,
                                                             IdUsuario))
            Else
                'InicializarConexionGR()
                'Return SqlHelper.ExecuteScalar(Me.objConexionGR,
                '                               CommandType.Text,
                '                               String.Format("select isnull(Etiqueta,'') as Etiqueta from Activo where idActivo in (Select IdActivo from vieActivoEntidad_Dispositivo with (nolock) where IdDispositivo = '{0}' and IdUsuario = {1})",
                '                                             IdDispositivo,
                '                                             IdUsuario))
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function ActivoEtiquetaActualizar(ByVal IdDispositivo As String,
                                             ByVal IdUsuario As Integer,
                                             ByVal Etiqueta As String) As Boolean
        Try
            InicializarDatosDetalle()

            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdDispositivo", IdDispositivo)
            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@Etiqueta", Etiqueta)

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                SqlHelper.ExecuteNonQuery(Me.objConexion,
                            CommandType.StoredProcedure,
                            "[spActivoEtiquetaActualizar]",
                            Me.Parametros)
            Else
                'InicializarConexionGR()
                'SqlHelper.ExecuteNonQuery(Me.objConexionGR,
                '            CommandType.StoredProcedure,
                '            "[spActivoEtiquetaActualizar]",
                '            Me.Parametros)
            End If

            Return True
        Catch ex As Exception
            Return False
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosDASHSub(ByVal IdUsuario As Integer, ByVal IdSubUsuario As Integer) As DataSet
        Dim adpad As SqlDataAdapter
        Try
            InicializarDatosDetalle()
            InicializarConexion()
            adpad = New SqlDataAdapter(String.Format("spActivosSinReportarDashSub {0},{1}", IdUsuario, IdSubUsuario), objConexion)
            adpad.SelectCommand.CommandTimeout = 450000
            adpad.Fill(DatosDetalle, "Dash")

            Return DatosDetalle
        Catch ex As Exception
            Throw
        Finally
            adpad = Nothing

            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosGrupo(ByVal IdUsuario As Integer, ByVal IdGrupo As Integer) As DataSet
        Dim agrupo As SqlDataAdapter
        Try
            InicializarDatosDetalle2()
            InicializarConexion()

            agrupo = New SqlDataAdapter(String.Format("[spGrupoActivosConsultarDetalle] {0},{1}", IdGrupo, IdUsuario), objConexion)
            agrupo.SelectCommand.CommandTimeout = 450000
            agrupo.Fill(DatosDetalle2, "GrupoDetalle")

            Return DatosDetalle2
        Catch ex As Exception
            Throw
        Finally
            agrupo = Nothing

            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosEntidadResumen(ByVal IdUsuario As Integer) As DataSet
        Try
            InicializarDatosDetalle()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", IdUsuario)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "[spActivosEntidadResumenConsultar]",
                                                Me.Parametros)
            Else
                'InicializarConexionGR()
                'Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                CommandType.StoredProcedure,
                '                                "[spActivosEntidadResumenConsultar]",
                '                                Me.Parametros)
            End If

            Return DatosDetalle
        Catch ex As Exception
            Throw
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ListarActivosEntidad(ByVal IdEntidad As String,
                                         ByVal RegistroUnidades As Integer,
                                            Optional ByVal Remoto As Boolean = False) As DataSet
        _EntidadActivos = Nothing

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@RegistroUnidades", RegistroUnidades)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                If Remoto Then
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoEntidadConsultarR]",
                                                    Me.Parametros)
                Else
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoEntidadConsultarR]",
                                                    Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'If Remoto Then
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoEntidadConsultarR]",
                '                                    Me.Parametros)
                'Else
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoEntidadConsultarR]",
                '                                    Me.Parametros)
                'End If
            End If

            Return Me.Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function Consultar2(ByVal Codigo As String) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            'Me.LimpiarParametros()
            'Me.AdicionarParametros("@IdPunto", Codigo)
            'Me.AdicionarParametros("@IdUsuario", _IdUsuario)

            'If Codigo = "" Then
            '    Codigo = 0
            'End If

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT * FROM vieActivosEntidad with (nolock) WHERE IdEntidad=" & Codigo & " ORDER BY 2", objConexion)

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

    Public Function ContarActivos(ByVal IdEntidad As String) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM vieActivosEntidad WHERE IdEntidad = '{0}' AND (Estado = 'A' or Estado = 'I')", IdEntidad))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarActivosGrupos(ByVal IdGrupo As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM ActivoGrupo WHERE IdGrupo = {0}", IdGrupo))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarSubusuarios(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM SubUsuario WHERE IdUsuario = {0}", IdUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarSubusuariosGrupos(ByVal IdUsuario As Integer, ByVal IdGrupo As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM SubUsuarioGrupo WHERE idUsuario = {0} AND idGrupo = {1}", IdUsuario, IdGrupo))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosFechaConductor(ByVal vID As String,
                                             ByVal Intervalo As String,
                                             ByVal Desde As String,
                                             ByVal Hasta As String,
                                             ByVal IdUsuario As String)
        Dim Adp As SqlDataAdapter
        Try
            InicializarDatosDetalle()
            InicializarConexion()
            Adp = New SqlDataAdapter(String.Format("spConductorFechaConsultar '{0}','{1}','{2}','{3}',{4}",
                                                       vID,
                                                       Intervalo,
                                                       Desde,
                                                       Hasta,
                                                       IdUsuario),
                                                       objConexion)

            Adp.SelectCommand.CommandTimeout = 150000
            Adp.Fill(DatosDetalle, "Conductores")


            Adp.Dispose()
            Adp = Nothing

            Return DatosDetalle
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivoEtiquetas(ByVal VID As String,
                                             ByVal Intervalo As String,
                                             ByVal Desde As String,
                                             ByVal Hasta As String,
                                             ByVal IdUsuario As String) As DataSet
        Dim Adp As SqlDataAdapter
        Try
            InicializarDatosDetalle()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Adp = New SqlDataAdapter(String.Format("spEtiquetasConsultar '{0}','{1}','{2}','{3}',{4}",
                                                       VID,
                                                       Intervalo,
                                                       Desde,
                                                       Hasta,
                                                       IdUsuario),
                                                       objConexion)
            Else
                'InicializarConexionGR()
                'Adp = New SqlDataAdapter(String.Format("spEtiquetasConsultar '{0}','{1}','{2}','{3}',{4}",
                '                                       VID,
                '                                       Intervalo,
                '                                       Desde,
                '                                       Hasta,
                '                                       IdUsuario),
                '                                       objConexionGR)
            End If

            Adp.SelectCommand.CommandTimeout = 150000
            Adp.Fill(DatosDetalle, "Eventos")


            Adp.Dispose()
            Adp = Nothing

            Return DatosDetalle
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosProducto(ByVal idProducto As String,
                                             Optional ByVal Remoto As Boolean = False) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdProducto", idProducto)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IP", _ip)

            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "[spActivoProductoEntidadConsultarR]",
                                            Me.Parametros)
            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case Else
                            Ignicion = "NULL"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                If tmpRows("Estado") = "A" Then
                    Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                                   & tmpRows("Alias") & "_" _
                                                                   & tmpRows("Latitud") & "_" _
                                                                   & tmpRows("Longitud") & "_" _
                                                                   & tmpRows("Velocidad") & "_" _
                                                                   & tmpRows("Rumbo") & "_" _
                                                                   & tmpRows("Kilometraje") & "_" _
                                                                   & tmpRows("UltimoReporte") & "_" _
                                                                   & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                                   & tmpRows("Calle").ToString.Trim() & "_" _
                                                                   & tmpRows("Codigo Producto") & "_" _
                                                                   & tmpRows("Etiqueta") & "_" _
                                                                   & tmpRows("IdActivo") & "_" _
                                                                   & tmpRows("RutaIcono") & "_" _
                                                                   & Ignicion
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosTipoVehiculo(ByVal IdEntidad As String,
                                                 ByVal TipoVehiculo As String,
                                                 ByVal RegistroUnidades As Integer) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@Tipo", TipoVehiculo.Replace("p_", ""))
            Me.AdicionarParametros("@RegistroUnidades", RegistroUnidades)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoTipoVehiculoEntidadConsultarR]",
                                                    Me.Parametros)
            End If

            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case Else
                            Ignicion = "NULL"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                If tmpRows("Estado") = "A" Or tmpRows("Estado") = "I" Or RegistroUnidades = 1 Then
                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                                  & tmpRows("Alias") & "_" _
                                                                  & tmpRows("Latitud") & "_" _
                                                                  & tmpRows("Longitud") & "_" _
                                                                  & tmpRows("Velocidad") & "_" _
                                                                  & tmpRows("Rumbo") & "_" _
                                                                  & tmpRows("Kilometraje") & "_" _
                                                                  & tmpRows("UltimoReporte") & "_" _
                                                                  & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                                  & tmpRows("Calle").ToString.Trim() & "_" _
                                                                  & tmpRows("Codigo Producto") & "_" _
                                                                  & tmpRows("Etiqueta") & "_" _
                                                                  & tmpRows("IdActivo") & "_" _
                                                                  & tmpRows("RutaIcono") & "_" _
                                                                  & Ignicion & "_" _
                                                                  & tmpRows("Estado")
                    Catch ex As Exception

                    End Try
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosTipoVehiculoSubUsuario(ByVal IdEntidad As String,
                                                    ByVal TipoVehiculo As String,
                                                    ByVal IdSubUsuario As Integer) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IdSubUsuario", IdSubUsuario)
            Me.AdicionarParametros("@Tipo", TipoVehiculo.Replace("p_", ""))
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoTipoVehiculoEntidadConsultarSubUsuarioR]",
                                                    Me.Parametros)
            End If

            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case Else
                            Ignicion = "NULL"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                If tmpRows("Estado") = "A" Or tmpRows("Estado") = "I" Then
                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                                  & tmpRows("Alias") & "_" _
                                                                  & tmpRows("Latitud") & "_" _
                                                                  & tmpRows("Longitud") & "_" _
                                                                  & tmpRows("Velocidad") & "_" _
                                                                  & tmpRows("Rumbo") & "_" _
                                                                  & tmpRows("Kilometraje") & "_" _
                                                                  & tmpRows("UltimoReporte") & "_" _
                                                                  & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                                  & tmpRows("Calle").ToString.Trim() & "_" _
                                                                  & tmpRows("Codigo Producto") & "_" _
                                                                  & tmpRows("Etiqueta") & "_" _
                                                                  & tmpRows("IdActivo") & "_" _
                                                                  & tmpRows("RutaIcono") & "_" _
                                                                  & Ignicion
                    Catch ex As Exception

                    End Try
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosEntidadGrupo(ByVal IdEntidad As String,
                                          ByVal IdGrupo As String,
                                          ByVal RegistroUnidades As Integer,
                                          Optional ByVal Remoto As Boolean = False) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdGrupo", IdGrupo)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@RegistroUnidades", RegistroUnidades)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                If Remoto Then
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoGrupoEntidadConsultarR]",
                                                    Me.Parametros)
                Else
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoGrupoEntidadConsultarR]",
                                                    Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'If Remoto Then
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoGrupoEntidadConsultarR]",
                '                                    Me.Parametros)
                'Else
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoGrupoEntidadConsultarR]",
                '                                    Me.Parametros)
                'End If
            End If

            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    If tmpRows("RutaIcono") Is DBNull.Value Then
                        tmpRows("RutaIcono") = ""
                    End If
                Catch ex As Exception

                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case Else
                            Ignicion = "NULL"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                tmpRows.AcceptChanges()
                If tmpRows("Estado") = "A" Or tmpRows("Estado") = "I" Or RegistroUnidades Then
                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & tmpRows("RutaIcono") & "_" _
                                                               & Ignicion & "_" _
                                                               & tmpRows("Estado")
                    Catch ex As Exception
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & "" & "_" _
                                                               & Ignicion & "_" _
                                                               & tmpRows("Estado")
                    End Try
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosEntidadGrupoSubUsuario(ByVal IdEntidad As String,
                                                           ByVal IdGrupo As String,
                                                           ByVal idSubUsuario As Integer,
                                                           Optional ByVal Remoto As Boolean = False) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IdSubUsuario", idSubUsuario)
            Me.AdicionarParametros("@IdGrupo", IdGrupo)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                If Remoto Then
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoGrupoEntidadConsultarSubUsuarioR]",
                                                    Me.Parametros)
                Else
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoGrupoEntidadConsultarSubUsuarioR]",
                                                    Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'If Remoto Then
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoGrupoEntidadConsultarR]",
                '                                    Me.Parametros)
                'Else
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoGrupoEntidadConsultarR]",
                '                                    Me.Parametros)
                'End If
            End If

            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    If tmpRows("RutaIcono") Is DBNull.Value Then
                        tmpRows("RutaIcono") = ""
                    End If
                Catch ex As Exception

                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case Else
                            Ignicion = "NULL"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                tmpRows.AcceptChanges()
                If tmpRows("Estado") = "A" Or tmpRows("Estado") = "I" Then
                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & tmpRows("RutaIcono") & "_" _
                                                               & Ignicion
                    Catch ex As Exception
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & "" & "_" _
                                                               & Ignicion
                    End Try
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosEntidad(ByVal IdEntidad As String,
                                            ByVal RegistroUnidades As Integer,
                                            Optional ByVal Remoto As Boolean = False) As String()
        _EntidadActivos = Nothing
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@RegistroUnidades", RegistroUnidades)
            Me.AdicionarParametros("@IP", _ip)

            If FuenteUsuario = "GeoSyS" Then
                Call InicializarConexion()
                If Remoto Then
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoEntidadConsultarR]",
                                                    Me.Parametros)
                Else
                    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.StoredProcedure,
                                                    "[spActivoEntidadConsultarR]",
                                                    Me.Parametros)
                End If
            Else
                'Call InicializarConexionGR()
                'If Remoto Then
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoEntidadConsultarR]",
                '                                    Me.Parametros)
                'Else
                '    Me.Datos = SqlHelper.ExecuteDataset(Me.objConexionGR,
                '                                    CommandType.StoredProcedure,
                '                                    "[spActivoEntidadConsultarR]",
                '                                    Me.Parametros)
                'End If
            End If

            Dim tmpRows As DataRow

            For Each tmpRows In Me.Datos.Tables(0).Rows
                Try
                    ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                Catch ex As Exception
                    ReDim Me._EntidadActivos(0)
                End Try

                Try
                    If tmpRows("RutaIcono") Is DBNull.Value Then
                        tmpRows("RutaIcono") = ""
                    End If
                Catch ex As Exception

                End Try

                Try
                    Select Case tmpRows("EstadoIgnicion")
                        Case "True", "1", "true"
                            Ignicion = "ON"
                        Case "False", "0", "false"
                            Ignicion = "OFF"
                        Case "Null", "NULL"
                            Ignicion = "NULL"
                        Case Else
                            Ignicion = "X"
                    End Select
                Catch ex As Exception
                    Ignicion = "NULL"
                End Try

                tmpRows.AcceptChanges()
                If (tmpRows("Estado") = "A" Or tmpRows("Estado") = "I") Or RegistroUnidades = 1 Then
                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & tmpRows("RutaIcono") & "_" _
                                                               & Ignicion & "_" _
                                                               & tmpRows("Estado")
                    Catch ex As Exception
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                               & tmpRows("Alias") & "_" _
                                                               & tmpRows("Latitud") & "_" _
                                                               & tmpRows("Longitud") & "_" _
                                                               & tmpRows("Velocidad") & "_" _
                                                               & tmpRows("Rumbo") & "_" _
                                                               & tmpRows("Kilometraje") & "_" _
                                                               & tmpRows("UltimoReporte") & "_" _
                                                               & tmpRows("Pto. Cercano").ToString.Trim() & "_" _
                                                               & tmpRows("Calle").ToString.Trim() & "_" _
                                                               & tmpRows("Codigo Producto") & "_" _
                                                               & tmpRows("Etiqueta") & "_" _
                                                               & tmpRows("IdActivo") & "_" _
                                                               & "" & "_" _
                                                               & Ignicion & "_" _
                                                               & tmpRows("Estado")

                    End Try
                End If
            Next

            tmpRows = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivosEntidadSubUsuario(ByVal IdEntidad As String,
                                                     ByVal IdSubUsuario As Integer) As String()
        _EntidadActivos = Nothing
        Dim Existe As Boolean = True
        Dim DatosSub As DataSet
        Dim Ignicion As String = ""

        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdEntidad", IdEntidad)
            Me.AdicionarParametros("@IdUsuario", _IdUsuario)
            Me.AdicionarParametros("@IdSubUsuario", IdSubUsuario)
            Me.AdicionarParametros("@IP", _ip)

            Call InicializarConexion()

            DatosSub = Nothing
            DatosSub = New DataSet()

            DatosSub = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.StoredProcedure,
                                                "[spActivoEntidadConsultarSubUsuarioR]",
                                                Me.Parametros)

            Dim tmpRows As DataRow

            For Each tmpRows In DatosSub.Tables(0).Rows
                If tmpRows("Estado") = "A" Or tmpRows("Estado") = "I" Then
                    Existe = True
                    Try
                        ReDim Preserve Me._EntidadActivos(UBound(Me._EntidadActivos) + 1)
                    Catch ex As Exception
                        ReDim Me._EntidadActivos(0)
                    End Try

                    Try
                        Select Case tmpRows("EstadoIgnicion")
                            Case "True", "1", "true"
                                Ignicion = "ON"
                            Case "False", "0", "false"
                                Ignicion = "OFF"
                            Case "Null", "NULL"
                                Ignicion = "NULL"
                            Case Else
                                Ignicion = "X"
                        End Select
                    Catch ex As Exception
                        Ignicion = "NULL"
                    End Try

                    Try
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                                & tmpRows("Alias") & "_" _
                                                                & tmpRows("Latitud") & "_" _
                                                                & tmpRows("Longitud") & "_" _
                                                                & tmpRows("Velocidad") & "_" _
                                                                & tmpRows("Rumbo") & "_" _
                                                                & tmpRows("Kilometraje") & "_" _
                                                                & tmpRows("UltimoReporte") & "_" _
                                                                & tmpRows("Pto. Cercano") & "_" _
                                                                & tmpRows("Calle") & "_" _
                                                                & tmpRows("Codigo Producto") & "_" _
                                                                & tmpRows("Etiqueta") & "_" _
                                                                & tmpRows("IdActivo") & "_" _
                                                                & tmpRows("RutaIcono") & "_" _
                                                                & Ignicion
                    Catch ex As Exception
                        Me._EntidadActivos(UBound(Me._EntidadActivos)) = tmpRows("VID") & "_" _
                                                                & tmpRows("Alias") & "_" _
                                                                & tmpRows("Latitud") & "_" _
                                                                & tmpRows("Longitud") & "_" _
                                                                & tmpRows("Velocidad") & "_" _
                                                                & tmpRows("Rumbo") & "_" _
                                                                & tmpRows("Kilometraje") & "_" _
                                                                & tmpRows("UltimoReporte") & "_" _
                                                                & tmpRows("Pto. Cercano") & "_" _
                                                                & tmpRows("Calle") & "_" _
                                                                & tmpRows("Codigo Producto") & "_" _
                                                                & tmpRows("Etiqueta") & "_" _
                                                                & tmpRows("IdActivo") & "_" _
                                                                & "" & "_" _
                                                                & Ignicion
                    End Try

                End If
            Next

            tmpRows = Nothing
            DatosSub = Nothing
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarActivoConductor(ByVal IdActivo As Integer,
                                              ByVal IdConductor As String,
                                              ByVal Desde As String,
                                              ByVal Hasta As String)

        Try
            InicializarConexion()
            InicializarDatosDetalle()

            Me.LimpiarParametros()
            Me.AdicionarParametros("@idActivo", IdActivo)
            Me.AdicionarParametros("@idConductor", IdConductor)
            Me.AdicionarParametros("@FechaDesde", Desde)
            Me.AdicionarParametros("@FechaHasta", Hasta)

            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                              CommandType.StoredProcedure,
                                              "[spActivoConductorConsultar]",
                                              Me.Parametros)

            Return DatosDetalle
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function ConsultarActividadVID(ByVal Vid As String, ByVal Desde As String, ByVal Hasta As String) As DataSet
        Dim adph As SqlDataAdapter
        Dim tryvid As New DataSet
        Try
            InicializarDatosDetalle()
            InicializarConexion()
            adph = New SqlDataAdapter(String.Format("spGenerarReporteActividadTrayectoWeb {0},'{1}','{2}','127001','{3}'", _IdUsuario, Desde, Hasta, Vid), objConexion)
            adph.SelectCommand.CommandTimeout = 450000
            adph.Fill(tryvid, "TrayectosFlotaWeb")
            tryvid.AcceptChanges()

            Return tryvid
        Catch ex As Exception
            Throw
        Finally
            adph = Nothing

            TerminarConexion()
        End Try
    End Function

    Public Function ComandosConsultar(ByVal User As String, ByVal Clave As String, ByVal IdActivo As String, ByVal IdDispositivo As String) As DataSet
        Dim adp As SqlDataAdapter
        Try

            Call InicializarConexion()
            adp = New SqlDataAdapter(String.Format("declare @Ejecucion as int = 0; declare @Mensaje as varchar(450) = ''; exec spComandosPorIdVehiculoMOBILEDEVICEv2 '{0}','{1}',{2},'{3}',@Ejecucion OUTPUT, @Mensaje OUTPUT;", User, Clave, CLng(IdActivo), IdDispositivo), objConexion)

            adp.SelectCommand.CommandTimeout = 4500000
            adp.Fill(Datos, "Comandos")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function
#End Region

End Class

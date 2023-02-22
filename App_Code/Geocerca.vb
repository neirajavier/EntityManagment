Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.ApplicationBlocks.Data
Imports System
Imports System.Web.UI

Public Class Geocerca : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Propiedades Privadas "
    Private _IdGeocerca As Integer
    Private _Nombre As String
    Private _IdTipoGeocerca As Integer
    Private _TipoGeocerca As String
    Private _Parametro1 As Integer
    Private _idUsuario As Integer
    Private _Latitud As Double
    Private _Longitud As Double
    Private _Secuencia As Integer
    Private _Radio As Double
    Private _Clasificacion As String

    Public Structure ePuntos
        Dim Secuencia As Integer
        Dim Lat As Double
        Dim Lon As Double
    End Structure

    Private _Puntos As ePuntos()

#End Region

#Region " Propiedades Publicas "

    Public Property Clasificacion As String
        Get
            Return _Clasificacion
        End Get
        Set(value As String)
            _Clasificacion = value
        End Set
    End Property

    Public Property idGeocerca() As Integer
        Get
            Return _IdGeocerca
        End Get
        Set(ByVal value As Integer)
            _IdGeocerca = value
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

    Public Property IdTipoGeocerca() As Integer
        Get
            Return _IdTipoGeocerca
        End Get
        Set(ByVal value As Integer)
            _IdTipoGeocerca = value
        End Set
    End Property

    Public Property TipoGeocerca() As String
        Get
            Return _TipoGeocerca
        End Get
        Set(ByVal value As String)
            _TipoGeocerca = value
        End Set
    End Property

    Public Property Parametro1() As Integer
        Get
            Return _Parametro1
        End Get
        Set(ByVal value As Integer)
            _Parametro1 = value
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

    Public Property Puntos() As ePuntos()
        Get
            Return _Puntos
        End Get
        Set(ByVal value As ePuntos())
            _Puntos = value
        End Set
    End Property

    Public Property Latitud() As Double
        Get
            Return _Latitud
        End Get
        Set(ByVal value As Double)
            _Latitud = value
        End Set
    End Property

    Public Property Longitud() As Double
        Get
            Return _Longitud
        End Get
        Set(ByVal value As Double)
            _Longitud = value
        End Set
    End Property

    Public Property Secuencia() As Integer
        Get
            Return _Secuencia
        End Get
        Set(ByVal value As Integer)
            _Secuencia = value
        End Set
    End Property

    Public Property Radio() As Double
        Get
            Return _Radio
        End Get
        Set(ByVal value As Double)
            _Radio = value
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

    Public Function ExisteGeocerca(ByVal Nombre As String) As Boolean
        Dim tmpRows As DataRow
        Dim res As Boolean

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            InicializarConexion()
            Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("Select dbo.ExisteGeocerca('{0}',{1})",
                                                          Nombre,
                                                          _idUsuario))

            res = False
            If Not IsNothing(Datos) Then
                For Each tmpRows In Datos.Tables(0).Rows
                    If tmpRows(0) Then
                        res = True
                    End If
                Next
            End If

            Return res
        Catch ex As Exception
            Throw
        Finally
            Call TerminarConexion()
            tmpRows = Nothing
        End Try
    End Function

    Public Function SubUsuarioConsultar(ByVal IdSubUsuario As Integer) As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)
            Me.AdicionarParametros("@IdSubUsuario", IdSubUsuario)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "[spGeocercaSubUsuarioConsultar]",
                                            Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)


            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spGeocercaConsultar",
                                            Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function Consultar2(ByVal Codigo As Integer) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT * FROM dbo.Geocerca with (nolock) WHERE IdUsuario=" & Codigo.ToString & " ORDER BY 2", objConexion)

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

    Public Function ConfUsuarioConsultar(ByVal User As String, ByVal Clave As String) As DataSet
        Dim adp As SqlDataAdapter
        Try

            Call InicializarConexion()
            adp = New SqlDataAdapter(String.Format("declare @Ejecucion as int = 0; declare @Mensaje as varchar(450) = ''; exec spConfiguracionDeUsuarioConsultaEnPx '{0}','{1}',@Ejecucion OUTPUT, @Mensaje OUTPUT;", User, Clave), objConexion)

            adp.SelectCommand.CommandTimeout = 4500000
            adp.Fill(Datos, "ConfUser")
            Datos.AcceptChanges()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ContarGeocercas(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM Geocerca WHERE IdUsuario = {0}", IdUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGeocercasSub(ByVal IdUsuario As Integer, ByVal IdSubusuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM GeocercaSubUsuario WHERE IdUsuario = {0} AND IdSubUsuario = {1}", IdUsuario, IdSubusuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGeocercasGrupos(ByVal IdGrupo As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM GeocercaGrupo WHERE IdGrupoGeocerca = {0}", IdGrupo))
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
                                            String.Format("SELECT COUNT(*) FROM GrupoGeocercaSubUsuario WHERE IdUsuario = {0} AND IdGrupoGeocerca = {1}", IdUsuario, IdGrupo))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarSubUsuario(IdSubUsuario As Integer) As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)
            Me.AdicionarParametros("@IdSubUsuario", IdSubUsuario)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spGeocercaSubUsuarioConsultar",
                                            Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ConsultarGrupos(Optional ByVal Codigo As String = "0") As System.Data.DataSet
        Try
            Call InicializarDatos()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)
            Me.AdicionarParametros("@IdGrupoGeocerca", Codigo)

            Call InicializarConexion()
            Me.Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spGrupoGeocercaConsultar",
                                            Me.Parametros)

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarGrupoDetalle(ByVal IdGrupoGeocerca As Integer) As System.Data.DataSet
        Try
            Call InicializarDatosDetalle()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdUsuario", _idUsuario)
            Me.AdicionarParametros("@idGrupoGeocerca", IdGrupoGeocerca)

            Call InicializarConexion()
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                            CommandType.StoredProcedure,
                                            "spGeocercaGrupoConsultar",
                                            Me.Parametros)

            Return DatosDetalle
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ConsultarPuntosGeocerca(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Try
            Call InicializarDatosDetalle()
            Me.LimpiarParametros()
            Me.AdicionarParametros("@IdGeocerca", _IdGeocerca)

            Call InicializarConexion()
            Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
                                        CommandType.StoredProcedure,
                                        "spPuntos_GeocercaConsultar",
                                        Me.Parametros)

            Return DatosDetalle
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()

        End Try
    End Function

    Public Function ContarPuntosGeocerca(ByVal IdGeocerca As Integer) As Integer
        Try
            _IdGeocerca = IdGeocerca

            ConsultarPuntosGeocerca()

            Return DatosDetalle.Tables(0).Rows.Count
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public Function PuntosConsultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatosDetalle()
            'Me.LimpiarParametros()
            'Me.AdicionarParametros("@IdGeocerca", _IdGeocerca)
            Call InicializarConexion()
            objConexion.Open()
            'Me.DatosDetalle = SqlHelper.ExecuteDataset(Me.objConexion,
            '                                CommandType.StoredProcedure,
            '                                "spPuntos_GeocercaConsultar",
            '                                Me.Parametros)

            adp = New SqlDataAdapter("spPuntos_GeocercaConsultar " & _IdGeocerca, objConexion)
            adp.Fill(DatosDetalle, "PuntosGeocerca")
            DatosDetalle.AcceptChanges()

            Return DatosDetalle
        Catch ex As SqlException
            Console.WriteLine(ex.Message)
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Try
            Me.LimpiarParametros()

            Select Case Accion
                Case "ING", "MOD"
                    AdicionarParametros("@Nombre", Me._Nombre)
                    AdicionarParametros("@Tipo", Me._IdTipoGeocerca)
                    AdicionarParametros("@Parametro1", Me._Parametro1)
                    AdicionarParametros("@IdUsuario", Me._idUsuario)
                    AdicionarParametros("@Clasificacion", Me._Clasificacion)

                    If Accion = "MOD" Then
                        AdicionarParametros("@IdGeocerca", Me._IdGeocerca)
                        AdicionarParametros("@UsuarioModificacion", Me._UsuarioLogin)

                        Me.Procedimiento = "spGeocercaActualizar"
                    Else
                        AdicionarParametros("@UsuarioIngreso", Me._UsuarioLogin)

                        Me.Procedimiento = "spGeocercaIngresar"
                    End If

                Case "ELM"
                    AdicionarParametros("@IdGeocerca", Me._IdGeocerca)
                    Me.Procedimiento = "spGeocercaEliminar"
            End Select

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                      CommandType.StoredProcedure,
                      Me.Procedimiento,
                      Me.Parametros)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function EjecutarPuntosGeocerca(ByVal Accion As String,
                                           ByVal Ancho As Integer) As Boolean
        Try
            Me.LimpiarParametros()
            AdicionarParametros("@IdGeocerca", Me._IdGeocerca)

            Select Case Accion
                Case "ING", "MOD"
                    AdicionarParametros("@Secuencia", Me._Secuencia)
                    AdicionarParametros("@Latitud", Me._Latitud)
                    AdicionarParametros("@Longitud", Me._Longitud)

                    If Accion = "MOD" Then
                        Me.Procedimiento = "spPuntos_GeocercaActualizar"
                    Else
                        Me.Procedimiento = "spPuntos_GeocercaIngresar"
                    End If

                Case "ELM"
                    AdicionarParametros("@Ancho", Ancho)
                    Me.Procedimiento = "spPuntos_GeocercaEliminar"
            End Select

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                      CommandType.StoredProcedure,
                      Me.Procedimiento,
                      Me.Parametros)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ActualizarPuntosGeocerca(ByVal IdGeocerca As Integer,
                                             ByVal Direccion As Integer,
                                             Optional ByVal Tipo As String = "P") As Boolean
        Try
            Me.LimpiarParametros()
            AdicionarParametros("@IdGeocerca", IdGeocerca)
            AdicionarParametros("@Direccion", Direccion)
            AdicionarParametros("@Tipo", Tipo)
            Me.Procedimiento = "spPuntos_GeocercaGeo"

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                    CommandType.StoredProcedure,
                    Me.Procedimiento,
                    Me.Parametros)

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function EjecutarPuntosGeocercaCircular(ByVal Latitud As Double,
                                               ByVal Longitud As Double,
                                               ByVal Radio As Integer) As Boolean
        Try
            Call InicializarConexion()
            Me.LimpiarParametros()

            AdicionarParametros("@IdGeocerca", Me._IdGeocerca)
            AdicionarParametros("@Latitud", Latitud.ToString.Replace(",", "."))
            AdicionarParametros("@Longitud", Longitud.ToString.Replace(",", "."))

            If Radio = 0 Then
                Radio = 500
            End If

            AdicionarParametros("@Radio", Radio)
            Me.Procedimiento = "spPuntosGeocerca_CircularCli"

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                      CommandType.StoredProcedure,
                      Me.Procedimiento,
                      Me.Parametros)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function IngresarGeocercaLinealxRecorrido(ByVal Vid As String, ByVal FI As String, ByVal FF As String) As Boolean
        Try
            Me.LimpiarParametros()
            Dim ocmd As SqlCommand

            'AdicionarParametros("@IdUsuario", Me._idUsuario)
            'AdicionarParametros("@NombreGeocerca", Me._Nombre)
            'AdicionarParametros("@Parametro1", Me._Parametro1)
            'AdicionarParametros("@Vid", Vid)
            'AdicionarParametros("@Desde", FI)
            'AdicionarParametros("@Hasta", FF)
            'Me.Procedimiento = "spGeocercaLineal_RecorridoImportar"

            'If FuenteUsuario = "GeoSyS" Then
            '    Call InicializarConexion()

            '    SqlHelper.ExecuteNonQuery(Me.objConexion,
            '              CommandType.StoredProcedure,
            '              Me.Procedimiento,
            '              Me.Parametros)
            'Else
            '    Call InicializarConexionGR()

            '    SqlHelper.ExecuteNonQuery(Me.objConexionGR,
            '              CommandType.StoredProcedure,
            '              Me.Procedimiento,
            '              Me.Parametros)
            'End If

            InicializarConexion()

            objConexion.Open()

            ocmd = New SqlCommand(String.Format("exec spGeocercaLineal_RecorridoImportar {0},'{1}',{2},'{3}','{4}','{5}'",
                                                    Me._idUsuario,
                                                    Me._Nombre,
                                                    Me._Parametro1,
                                                    Vid,
                                                    FI,
                                                    FF), objConexion)
            ocmd.CommandTimeout = 5000000
            ocmd.ExecuteNonQuery()

            IngresarGeocercaLinealxRecorrido = True
        Catch ex As Exception
            IngresarGeocercaLinealxRecorrido = False
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function RepararGeocercasUsuario(IdUsuario As Integer) As Boolean
        Try
            Me.LimpiarParametros()
            AdicionarParametros("@IdUsuario", IdUsuario)
            Me.Procedimiento = "spReparaGeocercasUsuario"

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                        CommandType.StoredProcedure,
                        Me.Procedimiento,
                        Me.Parametros)

            Return True
        Catch ex As Exception
            Throw
        Finally
            TerminarConexion()
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

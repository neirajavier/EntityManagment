Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports Microsoft.ApplicationBlocks.Data
Imports System.Globalization

Public MustInherit Class ClassCore

#Region " Propiedades Privadas "
    Private _CadenaConexion As String
    Private _CadenaConexionRS As String

    Private _objConexion As SqlConnection
    Private _objConexionGR As SqlConnection
    Private _objConexionRS As SqlConnection

    Private _dsDatos As New DataSet
    Private _dsDatosDetalle As New DataSet
    Private _dsDatosDetalle2 As New DataSet

    Friend _PaginaActual As Page

    Private _objTransaccion As SqlTransaction
    Private _ModoTransaccion As Boolean
    Private _Procedimiento As String
    Private _Parametros() As SqlParameter
    Private _EnTransaccion As Boolean
    Private _Accion As String
    Private _Usuario As String
    Private _CodOpcion As String
    Private _Sentencia As String
    Private _EnviarPorMail As Boolean
    Private _Fila As DataRow
    Private _ListaMails As String()
    Private _ListaMailsCompleto As String
    Friend _ip As String
    Friend _UsuarioLogin As String
    Friend _FuenteUsuario As String
    Friend _ExisteEnRastrac As Boolean
    Friend _ExisteEnGeoSyS As Boolean
    Friend _IdPaisLogin As String
#End Region

#Region " Propiedades Publicas "
    Public ReadOnly Property ExisteRastrac() As Boolean
        Get
            Return _ExisteEnRastrac
        End Get
    End Property

    Public ReadOnly Property ExisteEnGeoSyS() As Boolean
        Get
            Return _ExisteEnGeoSyS
        End Get
    End Property

    Public Property PaginaActual() As Page
        Get
            Return _PaginaActual
        End Get
        Set(ByVal value As Page)
            _PaginaActual = value
        End Set
    End Property

    Public Property Accion() As String
        Get
            Return Me._Accion
        End Get
        Set(ByVal Value As String)
            Me._Accion = Value
        End Set
    End Property

    Public Property Sentencia() As String
        Get
            Return Me._Sentencia
        End Get
        Set(ByVal Value As String)
            Me._Sentencia = Value
        End Set
    End Property

    Public Property CadenaConexion() As String
        Get
            Return Me._CadenaConexion
        End Get
        Set(ByVal Value As String)
            Me._CadenaConexion = Value
        End Set
    End Property

    Public Property CadenaConexionRS() As String
        Get
            Return Me._CadenaConexionRS
        End Get
        Set(ByVal value As String)
            Me._CadenaConexionRS = value
        End Set
    End Property

    Public Property objConexion() As SqlConnection
        Get
            Return Me._objConexion
        End Get
        Set(ByVal Value As SqlConnection)
            Me._objConexion = Value
        End Set
    End Property

    Public Property objConexionRS() As SqlConnection
        Get
            Return Me._objConexionRS
        End Get
        Set(ByVal value As SqlConnection)
            Me._objConexionRS = value
        End Set
    End Property

    Public Property Datos() As DataSet
        Get
            Return Me._dsDatos
        End Get
        Set(ByVal Value As DataSet)
            Me._dsDatos = Value
        End Set
    End Property

    Public Property DatosDetalle() As DataSet
        Get
            Return Me._dsDatosDetalle
        End Get
        Set(ByVal Value As DataSet)
            Me._dsDatosDetalle = Value
        End Set
    End Property

    Public Property DatosDetalle2() As DataSet
        Get
            Return Me._dsDatosDetalle2
        End Get
        Set(ByVal Value As DataSet)
            Me._dsDatosDetalle2 = Value
        End Set
    End Property

    Public Property objTransaccion() As SqlTransaction
        Get
            Return Me._objTransaccion
        End Get
        Set(ByVal Value As SqlTransaction)
            Me._objTransaccion = Value
        End Set
    End Property

    Public Property ModoTransaccion() As Boolean
        Get
            Return Me._ModoTransaccion
        End Get
        Set(ByVal Value As Boolean)
            Me._ModoTransaccion = Value
        End Set
    End Property

    Public Property Parametros() As SqlParameter()
        Get
            Return Me._Parametros
        End Get
        Set(ByVal Value As SqlParameter())
            Me._Parametros = Value
        End Set
    End Property

    Public Property Procedimiento() As String
        Get
            Return Me._Procedimiento
        End Get
        Set(ByVal Value As String)
            Me._Procedimiento = Value
        End Set
    End Property

    Public Property EnTransaccion() As Boolean
        Get
            Return Me._EnTransaccion
        End Get
        Set(ByVal Value As Boolean)
            Me._EnTransaccion = Value
        End Set
    End Property

    Public Property CodOpcion() As String
        Get
            Return Me._CodOpcion
        End Get
        Set(ByVal Value As String)
            Me._CodOpcion = Value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return Me._Usuario
        End Get
        Set(ByVal Value As String)
            Me._Usuario = Value
        End Set
    End Property

    Public Property Fila() As DataRow
        Get
            Return Me._Fila
        End Get
        Set(ByVal Value As DataRow)
            Me._Fila = Value
        End Set
    End Property

    Public Property FuenteUsuario() As String
        Get
            Return _FuenteUsuario
        End Get
        Set(ByVal Value As String)
            _FuenteUsuario = Value
        End Set
    End Property

    Public Enum TipoHorario
        Inicio = 0
        Fin = 1
    End Enum

    Public ReadOnly Property EnviarPorMail() As Boolean
        Get
            Return IIf(AppSettings("EnvioAlertas") = 1, True, False)
        End Get
    End Property

    Public ReadOnly Property GetListaMails() As String()
        Get
            Me._ListaMails = Nothing

            Me._ListaMails = Split(AppSettings("Mail"), ",")

            Return Me._ListaMails
        End Get
    End Property

    Public ReadOnly Property GetListaMailsCompleto() As String
        Get
            Return Replace(AppSettings("Mail"), ",", ";").ToString
        End Get
    End Property

    Public ReadOnly Property IP() As String
        Get
            Return _ip
        End Get
    End Property

    Public ReadOnly Property UsuarioLogin() As String
        Get
            If IsNothing(_PaginaActual.Session("Usuario")) Then
                Return _UsuarioLogin
            Else
                Return _PaginaActual.Session("Usuario")
            End If
        End Get
    End Property

    Public Property IdPaisLogin() As String
        Get
            Return Me._IdPaisLogin
        End Get
        Set(ByVal Value As String)
            Me._IdPaisLogin = Value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        _CadenaConexion = AppSettings("CadenaConexion")

        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("Es-EC", False)
        Dim FormatoNumeros As New NumberFormatInfo
        Dim FormatoFecha As New DateTimeFormatInfo

        With FormatoNumeros
            .CurrencyDecimalDigits = 2
            .CurrencyDecimalSeparator = "."
            .CurrencyGroupSeparator = ","
        End With

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat = FormatoNumeros
    End Sub

    Public Sub New(IdPais As String)
        _IdPaisLogin = IdPais

        Select Case IdPais.ToUpper()
            Case "EC"
                _CadenaConexion = AppSettings("CadenaConexion")
                _CadenaConexionRS = AppSettings("CadenaConexionRS")
            Case "PE"
                _CadenaConexion = AppSettings("CadenaConexionPE")
                _CadenaConexionRS = AppSettings("CadenaConexionRSPE")
            Case "CO"
                _CadenaConexion = AppSettings("CadenaConexionCO")
                _CadenaConexionRS = AppSettings("CadenaConexionRSCO")
            Case "PA"
                _CadenaConexion = AppSettings("CadenaConexionPA")
                _CadenaConexionRS = AppSettings("CadenaConexionRSPA")
            Case "MX"
                _CadenaConexion = AppSettings("CadenaConexionMX")
                _CadenaConexionRS = AppSettings("CadenaConexionRSMX")
            Case "CH"
                _CadenaConexion = AppSettings("CadenaConexionCH")
                _CadenaConexionRS = AppSettings("CadenaConexionRSCH")
        End Select

        System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("Es-EC", False)
        Dim FormatoNumeros As New NumberFormatInfo
        Dim FormatoFecha As New DateTimeFormatInfo

        With FormatoNumeros
            .CurrencyDecimalDigits = 2
            .CurrencyDecimalSeparator = "."
            .CurrencyGroupSeparator = ","
        End With

        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat = FormatoNumeros
    End Sub

#End Region

    Public Sub InicializarDatos()
        Try
            _dsDatos = Nothing
            _dsDatos = New DataSet()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub InicializarDatosDetalle()
        Try
            _dsDatosDetalle = Nothing
            _dsDatosDetalle = New DataSet()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub InicializarDatosDetalle2()
        Try
            _dsDatosDetalle2 = Nothing
            _dsDatosDetalle2 = New DataSet()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub IniciarTransaccion(ByVal ObjConexion As SqlConnection)
        Try
            Me._EnTransaccion = True

            Me._objTransaccion = Me.objConexion.BeginTransaction()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub ConfirmarTransaccion()
        Try
            Me.objTransaccion.Commit()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Me._EnTransaccion = True
        End Try
    End Sub

    Public Sub CancelarTransaccion()
        Try
            Me.objTransaccion.Rollback()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Me._EnTransaccion = True
        End Try
    End Sub

    Public Sub InicializarConexion(Optional ByVal Servidor As String = "")
        Try
            If IsNothing(objConexion) Then
                objConexion = New SqlConnection(Me.CadenaConexion)
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub AbrirConexion()
        Try
            If Not IsNothing(objConexion) Then
                If objConexion.State = ConnectionState.Closed Then
                    objConexion.Open()
                End If
            End If

            'objConexion = Nothing
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub TerminarConexion()
        Try
            If Not IsNothing(objConexion) Then
                If objConexion.State = ConnectionState.Open Then
                    objConexion.Close()
                End If
            End If

            objConexion = Nothing
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub AdicionarParametros(ByVal Nombre As String,
                                   ByVal Valor As Object)
        Try
            Try
                ReDim Preserve Me._Parametros(UBound(Me._Parametros) + 1)
            Catch ex As Exception
                ReDim Me._Parametros(0)
            End Try

            Me.Parametros(UBound(Me.Parametros)) = New SqlParameter(Nombre, Valor)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Sub LimpiarParametros()
        Try
            Me._Parametros = Nothing
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Public Function LlenarCombo(ByVal Tabla As String,
                                Optional ByVal Parametro As String = "",
                                Optional ByVal Parametro1 As String = "",
                                Optional ByVal Parametro2 As String = "") As DataSet
        Dim tmpDatos As DataSet
        Dim adp As SqlDataAdapter

        Try
            LimpiarParametros()
            InicializarDatos()

            tmpDatos = Nothing
            tmpDatos = New DataSet()

            InicializarConexion()
            adp = New SqlDataAdapter("exec spLlenarCombo '" & Tabla & "','" & Parametro & "','" & Parametro1 & "','" & Parametro2 & "'", Me.objConexion)
            adp.SelectCommand.CommandTimeout = 15000000
            adp.Fill(tmpDatos, "Combo")

            tmpDatos.AcceptChanges()

            Return tmpDatos
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function
    Public Function LlenarCombo_(ByVal Tabla As String,
                                Optional ByVal Parametro As String = "") As DataSet
        Dim tmpDatos As New DataSet

        Try
            LimpiarParametros()
            AdicionarParametros("@Tabla", Tabla)
            If Parametro <> "" Then
                AdicionarParametros("@Parametro", Parametro)
            End If

            InicializarConexion()
            tmpDatos = SqlHelper.ExecuteDataset(Me.objConexion,
                                         CommandType.StoredProcedure,
                                         "spLlenarCombo",
                                         Me.Parametros)

            tmpDatos.AcceptChanges()

            Return tmpDatos
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return Nothing
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ObtenerUltimoId(ByVal Tabla As String) As Integer
        Try
            InicializarConexion()
            Return SqlHelper.ExecuteNonQuery(Me.objConexion,
                                     CommandType.StoredProcedure,
                                     "spObtenerUltimoId",
                                     New SqlParameter("@Tabla", Tabla))

        Catch ex As Exception
            Return Nothing
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function BuscarValor(ByVal Tabla As String,
                                ByVal ColumnaSeleccionada As String,
                                ByVal ColumnaCondicion As String,
                                ByVal ValorCondicion As Object,
                                Optional ByVal Orden As String = "1",
                                Optional ByVal SentenciaCompleta As String = "") As Object

        Dim Sentencia As String = ""
        Dim tmpResultado As String = ""

        Try
            If SentenciaCompleta <> "" Then
                Sentencia = SentenciaCompleta
            Else
                If TypeOf (ValorCondicion) Is Integer Then
                    Sentencia = String.Format("Select {0} from {1} with (nolock) Where {2} = {3} Order by {4}",
                                              ColumnaSeleccionada,
                                              Tabla, ColumnaCondicion,
                                              ValorCondicion,
                                              Orden)

                ElseIf TypeOf (ValorCondicion) Is String Then
                    Sentencia = String.Format("Select {0} from {1} with (nolock) Where {2} = '{3}' Order by {4}",
                                              ColumnaSeleccionada,
                                              Tabla,
                                              ColumnaCondicion,
                                              ValorCondicion,
                                              Orden)
                End If
            End If


            Me.InicializarConexion()
            tmpResultado = SqlHelper.ExecuteScalar(Me.objConexion,
                                               CommandType.Text,
                                               Sentencia)

            If tmpResultado Is DBNull.Value Then
                Return ""
            Else
                Return tmpResultado
            End If
        Catch ex As Exception
            Return Nothing
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function GenerarSentencia() As String
        Dim tmpSentencia As String = ""
        Dim tmpParametros As String = ""
        Dim Ind As Integer

        Try
            tmpSentencia = Me.Procedimiento & " { "

            If Me.Parametros.Length > 0 Then
                For Ind = 0 To Me.Parametros.Length - 1
                    tmpParametros &= String.Format("{0}={1},",
                                                   Parametros(Ind).ParameterName,
                                                   Parametros(Ind).Value)
                Next
            End If

            tmpSentencia &= tmpParametros
            tmpSentencia = Left(tmpSentencia, tmpSentencia.Length - 1)

            tmpSentencia &= " } "

            Return tmpSentencia
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return ""
        End Try
    End Function

    Public MustOverride Function Ejecutar(ByVal Accion As String) As Boolean

    Public MustOverride Function Consultar(Optional ByVal Codigo As String = "") As DataSet

End Class

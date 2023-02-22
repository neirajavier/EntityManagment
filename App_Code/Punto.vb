Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data

Public Class Punto : Inherits ClassCore
    Private BD As OdinDataContext

#Region " Constructores "

    Public Sub New(ByVal Pagina As Page)
        MyBase.New()

        _ip = Pagina.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        _PaginaActual = Pagina

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

#Region " Variables Privadas "
    Private _idPunto As Integer
    Private _Lat As Double
    Private _Lng As Double
    Private _Nombre As String
    Private _Descripcion As String
    Private _IdUsuario As Integer
    Private _IdSubUsuario As Integer
    Private _IdPuntoCliente As String
    Private _TipoPunto As String
    Private _BuscarPorCodigoPunto As Boolean = False
    Private _idColor As Integer
    Private _idCategoria As Integer
    Private _idSubCategoria As Integer
    Private _CategoriaGeneral As Integer
    Private _Email As String
    Private _Sms As String
#End Region

#Region " Propiedades Publicas "
    Public Property IdUsuario() As Integer
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Integer)
            _IdUsuario = value
        End Set
    End Property

    Public Property IdSubUsuario() As Integer
        Get
            Return _IdSubUsuario
        End Get
        Set(ByVal value As Integer)
            _IdSubUsuario = value
        End Set
    End Property

    Public Property Latitud() As Double
        Get
            Return _Lat
        End Get
        Set(ByVal value As Double)
            _Lat = value
        End Set
    End Property

    Public Property Longitud() As Double
        Get
            Return _Lng
        End Get
        Set(ByVal value As Double)
            _Lng = value
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

    Public Property IdPunto() As Integer
        Get
            Return _idPunto
        End Get
        Set(ByVal value As Integer)
            _idPunto = value
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

    Public Property IdPuntoCliente() As String
        Get
            Return _IdPuntoCliente
        End Get
        Set(ByVal value As String)
            _IdPuntoCliente = value
        End Set
    End Property

    Public Property TipoPunto() As String
        Get
            Return _TipoPunto
        End Get
        Set(ByVal value As String)
            _TipoPunto = value
        End Set
    End Property

    Public Property BuscarPorCodigoPunto() As Boolean
        Get
            Return _BuscarPorCodigoPunto
        End Get
        Set(ByVal value As Boolean)
            _BuscarPorCodigoPunto = value
        End Set
    End Property

    Public Property idColor As Integer
        Get
            Return _idColor
        End Get
        Set(value As Integer)
            _idColor = value
        End Set
    End Property

    Public Property idCategoria As Integer
        Get
            Return _idCategoria
        End Get
        Set(value As Integer)
            _idCategoria = value
        End Set
    End Property

    Public Property idSubCategoria As Integer
        Get
            Return _idSubCategoria
        End Get
        Set(value As Integer)
            _idSubCategoria = value
        End Set
    End Property

    Public Property CategoriaGeneral As Integer
        Get
            Return _CategoriaGeneral
        End Get
        Set(value As Integer)
            _CategoriaGeneral = value
        End Set
    End Property

    Public Property SMS As String
        Get
            Return _Sms
        End Get
        Set(value As String)
            _Sms = value
        End Set
    End Property
    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(value As String)
            _Email = value
        End Set
    End Property
#End Region

#Region " Funciones Comunes "
    Public Overrides Function Ejecutar(ByVal Accion As String) As Boolean
        Try
            Me.LimpiarParametros()
            Select Case Accion
                Case "UBI"
                    AdicionarParametros("@IdPunto", _idPunto)
                    AdicionarParametros("@Latitud", Me._Lat)
                    AdicionarParametros("@Longitud", Me._Lng)
                    AdicionarParametros("@IdUsuario", _IdUsuario)
                    Me.Procedimiento = "spPuntoUbicacionActualizar"
                Case "ING", "MOD"
                    AdicionarParametros("@Nombre", Me._Nombre)
                    AdicionarParametros("@Descripcion", Me._Descripcion)
                    AdicionarParametros("@Latitud", Me._Lat)
                    AdicionarParametros("@Longitud", Me._Lng)
                    AdicionarParametros("@IdIcono", 0)
                    AdicionarParametros("@IdUsuario", _IdUsuario)
                    AdicionarParametros("@IdPuntoCliente", _IdPuntoCliente)
                    AdicionarParametros("@IdColor", _idColor)
                    AdicionarParametros("@IdCategoria", _idCategoria)
                    AdicionarParametros("@IdSubCategoria", _idSubCategoria)
                    AdicionarParametros("@CategoriaGeneral", _CategoriaGeneral)

                    If Accion = "MOD" Then
                        AdicionarParametros("@IdPunto", _idPunto)
                        Me.Procedimiento = "spPuntoActualizar"

                        AdicionarParametros("@UsuarioModificacion", Me._UsuarioLogin)
                    Else
                        Me.Procedimiento = "spPuntoIngresar"
                        AdicionarParametros("@UsuarioIngreso", Me._UsuarioLogin)
                    End If
                Case "ELM"
                    AdicionarParametros("@IdPunto", _idPunto)
                    Me.Procedimiento = "spPuntoEliminar"
            End Select

            Call InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                              CommandType.StoredProcedure,
                              Me.Procedimiento,
                              Me.Parametros)

            If Accion = "ING" Then
                Try
                    _idPunto = New OdinDataContext().getLastPuntoUsuario(IdUsuario)
                Catch ex As Exception
                    _idPunto = -1
                End Try
            End If
        Catch ex As Exception
            Throw ex
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Overrides Function Consultar(Optional ByVal Codigo As String = "") As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            'Me.LimpiarParametros()
            'Me.AdicionarParametros("@IdPunto", Codigo)
            'Me.AdicionarParametros("@IdUsuario", _IdUsuario)

            If Codigo = "" Then
                Codigo = 0
            End If

            Call InicializarConexion()
            adp = New SqlDataAdapter("spPuntoConsultar " & IdPunto.ToString() & "," & _IdUsuario.ToString(), objConexion)

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

    Public Function Consultar2(ByVal Codigo As Integer) As System.Data.DataSet
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
            objConexion.Open()
            adp = New SqlDataAdapter("SELECT TOP 125000 IdPunto, Nombre, Descripcion, Latitud, Longitud, IdPuntoUsadoPorCliente FROM dbo.Puntos_Referencia with (nolock) WHERE IdUsuario=" & Codigo.ToString & " AND LEN(Nombre)>0 ORDER BY 2", objConexion)

            adp.SelectCommand.CommandTimeout = 4500000
            adp.Fill(Datos, "PuntosU")
            Datos.AcceptChanges()

            objConexion.Close()

            Return Datos
        Catch ex As SqlException
            Throw ex
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function Consultar3(ByVal User As Integer, ByVal Codigo As Integer) As System.Data.DataSet
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
            adp = New SqlDataAdapter("SELECT TOP 120000 IdPunto, Nombre, Descripcion, Latitud, Longitud, IdPuntoUsadoPorCliente, idColor 
                                      FROM dbo.Puntos_Referencia with (nolock) WHERE IdPunto=" & Codigo & " AND IdUsuario=" & User & " AND LEN(Nombre)>0 ORDER BY 2", objConexion)

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

    Public Function ConsultarPtoSub(ByVal user As Integer, ByVal sbuser As Integer) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT psu.IdPunto, pr.Nombre, pr.Descripcion, pr.Latitud, pr.Longitud, pr.IdPuntoUsadoPorCliente FROM PuntosSubUsuario psu WITH (nolock) INNER JOIN Puntos_Referencia pr WITH (nolock) on psu.IdPunto = pr.IdPunto where (psu.IdSubUsuario =" & sbuser.ToString & " AND psu.idusuario =" & user.ToString & ")  ORDER BY 2", objConexion)

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

    Public Function ConsultarParciales(ByVal User As Integer, ByVal Codigo As Integer) As System.Data.DataSet
        Dim adp As SqlDataAdapter
        Try
            Call InicializarDatos()

            Call InicializarConexion()
            adp = New SqlDataAdapter("SELECT IdPunto, Latitud, Longitud, idColor, idCategoria, idSubCategoria, Celular, Email
                                      FROM dbo.Puntos_Referencia with (nolock) WHERE IdPunto=" & Codigo & " AND IdUsuario=" & User, objConexion)

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

    Public Function ContarPuntos(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM Puntos_Referencia WHERE IdUsuario = {0}", IdUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarPuntosGrupos(ByVal IdGrupo As Integer, ByVal idUser As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("SELECT COUNT(*) FROM PuntoGrupo WHERE IdGrupoPunto = {0} AND IdUsuario = {1}", IdGrupo, idUser))
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
                                            String.Format("SELECT COUNT(*) FROM GrupoPuntoSubUsuario WHERE IdUsuario = {0} AND IdGrupoPunto = {1}", IdUsuario, IdGrupo))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ExistePuntoPlantilla(ByVal IdPunto As Integer,
                                         ByVal IdUsuario As Integer) As Boolean
        Dim tmpRows As DataRow
        Dim res As Boolean

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select dbo.ExistePuntoPlantilla({0},{1})",
                                                              IdPunto,
                                                              IdUsuario))
            End If

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

    Public Function ExistePunto(ByVal Nombre As String,
                                ByVal IdUsuario As Integer) As Boolean
        Dim tmpRows As DataRow
        Dim res As Boolean

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select dbo.ExistePunto('{0}',{1})",
                                                              Nombre,
                                                              IdUsuario))
            End If

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

    Public Function ExistePuntoImportacion(ByVal Nombre As String,
                                           ByVal Descripcion As String,
                                           ByVal IdUsuario As Integer,
                                           ByVal CodigoCliente As String,
                                           Optional ByVal ValidarPorCodigoCliente As Boolean = False) As Boolean
        Dim tmpRows As DataRow
        Dim res As Boolean

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()

                If Not ValidarPorCodigoCliente Then

                    Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.Text,
                                                    String.Format("Select dbo.ExistePuntoImportacion('{0}','{2}',{1})",
                                                                  Nombre,
                                                                  IdUsuario,
                                                                  Descripcion))
                Else
                    Datos = SqlHelper.ExecuteDataset(Me.objConexion,
                                                    CommandType.Text,
                                                    String.Format("Select dbo.ExistePuntoCodigoCliente('{0}',{1})",
                                                                  CodigoCliente,
                                                                  IdUsuario))
                End If
            End If

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

    Public Function ValidarCodigoCli(ByVal Usuario As Integer, ByVal Codigo As String) As Boolean
        Dim _tmpSubUsuarios As New DataSet()
        Dim count As Integer
        Dim _Valido As Boolean = False

        InicializarDatos()
        Me.LimpiarParametros()
        InicializarConexion()

        Try
            count = SqlHelper.ExecuteScalar(Me.objConexion, CommandType.Text,
                                                       String.Format("Select Count(*) from Puntos_Referencia pr with (nolock) where ('{0}' = pr.IdPuntoUsadoPorCliente) and (pr.IdUsuario = {1})",
                                                                  Codigo,
                                                                  Usuario))
            If count = 0 Then
                _Valido = False
            Else
                _Valido = True
            End If

            Return _Valido
        Catch ex As Exception
            Throw
        Finally
            Call TerminarConexion()
        End Try
    End Function

    Public Function ValidarPuntoCli(ByVal Usuario As Integer, ByVal Codigo As String) As Boolean
        Dim _tmpSubUsuarios As New DataSet()
        Dim count As Integer
        Dim _Valido As Boolean = False

        InicializarDatos()
        Me.LimpiarParametros()
        InicializarConexion()

        Try
            count = SqlHelper.ExecuteScalar(Me.objConexion, CommandType.Text,
                                                       String.Format("Select Count(*) from Puntos_Referencia pr with (nolock) where ('{0}' = pr.Nombre) and (pr.IdUsuario = {1})",
                                                                  Codigo,
                                                                  Usuario))
            If count = 0 Then
                _Valido = False
            Else
                _Valido = True
            End If

            Return _Valido
        Catch ex As Exception
            Throw
        Finally
            Call TerminarConexion()
        End Try
    End Function

#End Region

#Region " Funciones Propias "
    Public Function LimpiarImagenPunto(ByVal IdPunto As String)
        Try
            InicializarDatos()
            LimpiarParametros()

            InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("Update Puntos_Referencia set Foto = null, FechaModificacion = getdate() where IdPunto = '{0}'",
            IdPunto))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function UpdateDatosPrincipales(ByVal IdPunto As String, ByVal codigo As String, ByVal nombre As String, ByVal descripcion As String, ByVal user As String)
        Try
            InicializarDatos()
            LimpiarParametros()

            InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("Update Puntos_Referencia set IdPuntoUsadoPorCliente = '{1}', Nombre = '{2}', Descripcion = '{3}', UsuarioModificacion = '{4}', FechaModificacion = getdate() where IdPunto = '{0}'",
            IdPunto, codigo, nombre, descripcion, user))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function UpdateDatosSecundarios(ByVal IdPunto As String, ByVal latitud As Double, ByVal longitud As Double, ByVal color As Integer, ByVal categoria As Integer, ByVal subcategoria As Integer, ByVal celular As String, ByVal email As String, ByVal user As String)
        Try
            InicializarDatos()
            LimpiarParametros()

            InicializarConexion()
            SqlHelper.ExecuteNonQuery(Me.objConexion,
                                           CommandType.Text,
                                           String.Format("UPDATE Puntos_Referencia SET Latitud = {1}, Longitud = {2}, idColor = {3}, idCategoria = {4}, idSubCategoria = {5}, Celular = '{6}', Email = '{7}', UsuarioModificacion = '{8}', FechaModificacion = getdate() where IdPunto = '{0}'",
            IdPunto, latitud, longitud, color, categoria, subcategoria, celular, email, user))
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGeoCercasSubUsuario(ByVal IdUsuario As Integer,
                                              ByVal IdSubUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("Select count(*) as Geoms from GeocercaSubusuario with (nolock) where idUsuario = {0} and IdSubUsuario = {1}",
                                                          IdUsuario,
                                                          IdSubUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGrupoGeoCercasSubUsuario(ByVal IdUsuario As Integer, ByVal IdSubUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Geoms from GrupoGeocercaSubUsuario with (nolock) where idUsuario = '{0}' and idSubUsuario = '{1}'",
                                                              IdUsuario,
                                                              IdSubUsuario))
            End If

            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGrupoGeoCercasUsuario(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Geoms from GrupoGeocerca with (nolock) where idUsuario = '{0}'",
                                                              IdUsuario))
            End If

            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarGeoCercasUsuario(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Geoms from Geocerca with (nolock) where idUsuario = '{0}' and Estado = 'A'",
                                                              IdUsuario))
            End If

            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarPuntosUsuario(ByVal IdUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()

            If FuenteUsuario = "GeoSyS" Then
                InicializarConexion()
                Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                                CommandType.Text,
                                                String.Format("Select count(*) as Puntos from Puntos_Referencia with (nolock) where idUsuario = '{0}' and Estado = 'A'",
                                                              IdUsuario))
            End If

            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ContarPuntosSubUsuario(ByVal IdUsuario As Integer,
                                           ByVal IdSubUsuario As Integer) As String
        Dim Contador As Integer = 0

        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            Contador = SqlHelper.ExecuteScalar(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("Select count(*) as Puntos from PuntosSubUsuario usu with (nolock) where usu.idUsuario = {0} and usu.IdSubUsuario = {1}",
                                                          IdUsuario,
                                                          IdSubUsuario))
            Return Contador
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function EliminarPuntosUsuario(ByVal IdUsuario As Integer) As Object
        Try
            InicializarDatos()
            Me.LimpiarParametros()
            InicializarConexion()

            SqlHelper.ExecuteNonQuery(Me.objConexion,
                                            CommandType.Text,
                                            String.Format("exec spPuntosUsuarioLimpiar {0}",
                                                          IdUsuario))
        Catch ex As Exception
            Return "0"
        Finally
            TerminarConexion()
        End Try
    End Function

    Public Function ConsultarVisitaPuntos(ByVal VID As String,
                                         ByVal Desde As String,
                                         ByVal Hasta As String,
                                         ByVal DistanciaMinima As Integer,
                                         ByVal Punto As String) As DataSet

        Try
            InicializarConexion()
            InicializarDatosDetalle()

            Me.LimpiarParametros()


            Dim Adp As New SqlDataAdapter("[spReportePuntosVisitadosConsultar] '" & VID & "'," _
                                                                                  & _IdUsuario & ",'" _
                                                                                  & Desde.ToString() & "','" _
                                                                                  & Hasta.ToString() & "'," _
                                                                                  & DistanciaMinima.ToString(),
                                 objConexion)

            Adp.SelectCommand.CommandTimeout = 5250000
            Adp.Fill(DatosDetalle, "Visitas")

            Return DatosDetalle
        Catch ex As Exception
            Throw
        Finally
            TerminarConexion()
        End Try
    End Function

#End Region

End Class

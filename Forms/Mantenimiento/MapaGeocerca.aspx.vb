Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System
Imports DevExpress.Web
Imports DevExpress.Utils.Menu
Imports System.Configuration.ConfigurationSettings
Imports Microsoft.ApplicationBlocks.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Reflection
Imports DevExpress.Web.Rendering
Imports System.Threading
Imports Newtonsoft.Json
Imports ArtemisAdmin.Geocerca

Public Class MapaGeocerca
    Inherits System.Web.UI.Page

    Private oGeocerca As Geocerca
    Private objUsuario As Usuario
    Private BD As New OdinDataContext()

    Public ReferenciaGoogle2 As String
    Public SVN2 As String
    Public geoTipo As String
    Public pais As String
    Public geocerca As String
    Private IP As String
    Private datasetUser As DataSet
    Dim GeocercaDet As New Object()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        objUsuario = New Usuario(Me, Session("PaisLogin"))
        IP = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        Try
            SVN2 = AppSettings("SVN")
            geocerca = Request.QueryString("data").ToString
            If (geocerca.Equals("nueva")) Then
                hId.Value = ""
                hNombre.Value = ""
                hTipo.Value = ""
                hParametro.Value = ""

                geoTipo = "0"
                hdpais2.Value = Session("PaisLogin").ToString.ToUpper
                pais = Session("PaisLogin").ToString.ToUpper
                hdindex.Value = "N"
            Else
                hId.Value = Request.QueryString("myId").ToString
                hNombre.Value = Request.QueryString("nombre").ToString

                GeocercaDet = BD.spGeocercaConsultarCli(CInt(hId.Value)).ToList
                For Each Geodet In CType(GeocercaDet, IEnumerable(Of Object))
                    If (Geodet.Tipo_Geocerca.Equals("Poligonal")) Then
                        hTipo.Value = "1"
                        geoTipo = "1"
                    ElseIf (Geodet.Tipo_Geocerca.Equals("Lineal")) Then
                        hTipo.Value = "2"
                        geoTipo = "2"
                    ElseIf (Geodet.Tipo_Geocerca.Equals("Circular")) Then
                        hTipo.Value = "3"
                        geoTipo = "3"
                    End If
                Next

                hdpais2.Value = Session("PaisLogin").ToString.ToUpper
                pais = Session("PaisLogin").ToString.ToUpper
                hParametro.Value = Request.QueryString("gpar").ToString
                hdindex.Value = "E"
            End If

            hdurlgeocercasdetalle.Value = ResolveUrl("~/Services/ServiciosCli.asmx/GetGeocercaDetalle")
            hdurlsetgeocercas.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setGeoInfo")
            hdurlsetgeocercasdetalleC.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setGeoCInfo")
            hdurlsetgeocercasdetalleR.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setGeoRInfo")
            hdurlsetgeocercasdetalleL.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setGeoLInfo")
            hdurlsetgeocercasdetalleP.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setGeoPInfo")

            CargaDatosMapa()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        'If Not IsPostBack Then
        '    CargaDatosMapa()
        '    'CargaDatosMapa2()
        'End If

    End Sub

    Private Sub CargaDatosMapa()
        oGeocerca = New Geocerca(Me, Session("PaisLogin"))
        Try
            hdidusuario.Value = Session("IdUsuario")
        Catch ex As Exception
            hdidusuario.Value = ""
        End Try

        If AppSettings("Protect") = 1 Then
            idusuario.Value = AES_Encrypt(Session("IdUsuario"), AppSettings("sk"))
        Else
            idusuario.Value = Session("IdUsuario")
        End If

        Try
            If AppSettings("Protect") = 1 Then
                If Not IsNothing(Session("idSubUsuario")) Then
                    hdidsubusuario.Value = AES_Encrypt(Session("IdSubUsuario"), AppSettings("sk"))
                Else
                    hdidsubusuario.Value = AES_Encrypt("0", AppSettings("sk"))
                End If
            Else
                If Not IsNothing(Session("idSubUsuario")) Then
                    hdidsubusuario.Value = Session("idSubUsuario")
                Else
                    hdidsubusuario.Value = "0"
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        '    Try
        '        Session("Host") = Request.ServerVariables("LOGON_USER")
        '    Catch ex As Exception
        '        Session("Host") = Request.ServerVariables("REMOTE_HOST")
        '    End Try

        Try
            If IsNothing(Session("IdSubUsuario")) Then
                'Session("Pais") = objUsuario.ConfiguracionUsuario.IdPais
                Session("Pais") = Session("PaisLogin")
            Else
                'Session("Pais") = objUsuario.ConfiguracionSubUsuario.IdPais
                Session("Pais") = Session("PaisLogin")
            End If
        Catch ex As Exception
            Session("Pais") = "EC"
        End Try

        Try
            Session("PermiteGoogle") = BD.getGoogleMapsPais(Session("Pais"))
        Catch ex As Exception
            Session("PermiteGoogle") = 0
        End Try

        Try
            txLatitudInicial.Value = BD.getCoordPromUsuario(CInt(Session("IdUsuario"))).Split("_")(0).ToString()
            txLongitudInicial.Value = BD.getCoordPromUsuario(CInt(Session("IdUsuario"))).Split("_")(1).ToString()
        Catch ex As Exception
            txLatitudInicial.Value = LatitudInicial
            txLongitudInicial.Value = LongitudInicial
        End Try

        Try
            hdpais.Value = Session("Pais")
        Catch ex As Exception
            hdpais.Value = Session("EC")
        End Try

        Try
            datasetUser = oGeocerca.ConfUsuarioConsultar2(CStr(Session("IdUsuario")))
            For Each fila As DataRow In datasetUser.Tables(0).Rows()
                hdlistamapas.Value = fila(11).ToString
            Next

            If Session("PermiteGoogle") = 0 Then
                hdlistamapas.Value = hdlistamapas.Value.Replace("GM", "")
                hdmotormapa.Value = "OS"
            Else
                If hdlistamapas.Value Like "*GM*" Then
                    hdmotormapa.Value = "GM"
                Else
                    hdmotormapa.Value = "OS"
                End If
            End If
        Catch ex As Exception
            hdmotormapa.Value = "OS"
        End Try

        Try
            hdservidorpe.Value = AppSettings("ServidorMapasPE")
        Catch ex As Exception
            hdservidorpe.Value = "http://191.98.146.189/"
        End Try

        Try
            hdservidorcapaspe.Value = AppSettings("ServidorCapasPE")
        Catch ex As Exception
            hdservidorcapaspe.Value = "http://191.98.146.189:8080/"
        End Try

        'If Not (hdmotormapa.Value Like "*GM*") Then
        '    ReferenciaGoogle2 = ""
        'Else
        If AppSettings("Debug") Then
            ReferenciaGoogle2 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & "&key=" & AppSettings("KeyGoogle") & "' type='text/javascript'></script>"
        Else
            ReferenciaGoogle2 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & " type='text/javascript'></script>"
        End If
        'End If
    End Sub

    Private Sub CargaDatosMapa2()
        If objUsuario.ValidarUsuario(Session("Usuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
            Session("IP") = IP

            If Session("IP") = "::1" Then
                Session("IP") = "127001"
            End If

            Session("Perfil") = objUsuario.Perfil
            Session("FuncionesAcciones") = objUsuario.FuncionesAcciones
            Session("UsuarioWindows") = HttpContext.Current.User.Identity.Name

            Session("CodigoCliente") = objUsuario.CodigoCliente
            Session("AplicacionGeo") = objUsuario.AplicacionGeoSYS
            Session("uid") = objUsuario.uid
            Session("r") = Request.QueryString("l")

            Session("FechaHoraLogin") = String.Format("{0} {1}",
                                                           Now.ToLongDateString(),
                                                           Now.ToLongTimeString())

            Session("ConfigUsuario") = objUsuario.ConfiguracionUsuario
            Session("Fuente") = objUsuario.FuenteUsuario

            Try
                hdidusuario.Value = Session("IdUsuario")
            Catch ex As Exception
                hdidusuario.Value = ""
            End Try

            If AppSettings("Protect") = 1 Then
                idusuario.Value = AES_Encrypt(Session("IdUsuario"), AppSettings("sk"))
            Else
                idusuario.Value = Session("IdUsuario")
            End If

            Try
                If AppSettings("Protect") = 1 Then
                    If Not IsNothing(Session("idSubUsuario")) Then
                        hdidsubusuario.Value = AES_Encrypt(Session("IdSubUsuario"), AppSettings("sk"))
                    Else
                        hdidsubusuario.Value = AES_Encrypt("0", AppSettings("sk"))
                    End If
                Else
                    If Not IsNothing(Session("idSubUsuario")) Then
                        hdidsubusuario.Value = Session("idSubUsuario")
                    Else
                        hdidsubusuario.Value = "0"
                    End If
                End If
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            Try
                Session("Host") = Request.ServerVariables("LOGON_USER")
            Catch ex As Exception
                Session("Host") = Request.ServerVariables("REMOTE_HOST")
            End Try

            Try
                If IsNothing(Session("IdSubUsuario")) Then
                    Session("Pais") = objUsuario.ConfiguracionUsuario.IdPais
                Else
                    Session("Pais") = objUsuario.ConfiguracionSubUsuario.IdPais
                End If
            Catch ex As Exception
                Session("Pais") = "EC"
            End Try

            Try
                Session("PermiteGoogle") = BD.getGoogleMapsPais(Session("Pais"))
            Catch ex As Exception
                Session("PermiteGoogle") = 0
            End Try

            Try
                Session("IdLocalidad") = objUsuario.IdLocalidad
            Catch ex As Exception
                Session("IdLocalidad") = 1
            End Try

            Try
                Session("Localidad") = objUsuario.Localidad
            Catch ex As Exception
                Session("Localidad") = "Guayaquil"
            End Try

        End If

        Try
            txLatitudInicial.Value = BD.getCoordPromUsuario(CInt(Session("IdUsuario"))).Split("_")(0).ToString()
            txLongitudInicial.Value = BD.getCoordPromUsuario(CInt(Session("IdUsuario"))).Split("_")(1).ToString()
        Catch ex As Exception
            txLatitudInicial.Value = LatitudInicial
            txLongitudInicial.Value = LongitudInicial
        End Try

        Try
            hdpais.Value = Session("Pais")
        Catch ex As Exception
            hdpais.Value = Session("EC")
        End Try

        Try
            hdlistamapas.Value = CType(Session("ConfigUsuario"), ConfigUsuario).MotorMapa.ToUpper()
            If Session("PermiteGoogle") = 0 Then
                hdlistamapas.Value = hdlistamapas.Value.Replace("GM", "")
                hdmotormapa.Value = "OS"
            Else
                If CType(Session("ConfigUsuario"), ConfigUsuario).MotorMapa Like "*GM*" Then
                    hdmotormapa.Value = "GM"
                Else
                    hdmotormapa.Value = "OS"
                End If
            End If
        Catch ex As Exception
            hdmotormapa.Value = "OS"
        End Try

        Try
            hdservidorpe.Value = AppSettings("ServidorMapasPE")
        Catch ex As Exception
            hdservidorpe.Value = "http://191.98.146.189/"
        End Try

        Try
            hdservidorcapaspe.Value = AppSettings("ServidorCapasPE")
        Catch ex As Exception
            hdservidorcapaspe.Value = "http://191.98.146.189:8080/"
        End Try

        If Not (hdmotormapa.Value Like "*GM*") Then
            ReferenciaGoogle2 = ""
        Else
            If AppSettings("Debug") Then
                ReferenciaGoogle2 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & "&key=" & AppSettings("KeyGoogle") & "' type='text/javascript'></script>"
            Else
                ReferenciaGoogle2 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & " type='text/javascript'></script>"
            End If
        End If
    End Sub

End Class
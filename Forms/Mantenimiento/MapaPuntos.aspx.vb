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

Public Class MapaPuntos
    Inherits System.Web.UI.Page

    Private oPunto As Punto
    Private objUsuario As Usuario
    Private BD As New OdinDataContext()

    Public ReferenciaGoogle3 As String
    Public SVN3 As String
    Public punto As String
    Public pais As String
    Private IP As String
    Private dataSetPto As DataSet
    Private datasetUser As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        objUsuario = New Usuario(Me, Session("PaisLogin"))
        oPunto = New Punto(Me, Session("PaisLogin"))

        datasetUser = oPunto.ConfUsuarioConsultar2(CInt(Session("IdUsuario")))
        IP = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        Try
            SVN3 = AppSettings("SVN")
            punto = Request.QueryString("data").ToString

            If (punto.Equals("nueva")) Then
                hId.Value = ""
                hNombre.Value = ""
                hLatitud.Value = ""
                hLongitud.Value = ""
                hColor.Value = ""

                hdpais2.Value = Session("PaisLogin").ToString.ToUpper
                pais = Session("PaisLogin").ToString.ToUpper
            Else
                dataSetPto = oPunto.Consultar3(CInt(Session("IdUsuario")), CInt(punto))
                For Each fila As DataRow In dataSetPto.Tables(0).Rows()
                    hId.Value = fila(0).ToString
                    hNombre.Value = fila(1).ToString
                    hLatitud.Value = fila(3).ToString
                    hLongitud.Value = fila(4).ToString
                    hColor.Value = fila(6).ToString
                Next

                hdpais2.Value = Session("PaisLogin").ToString.ToUpper
                pais = Session("PaisLogin").ToString.ToUpper
            End If

            For Each fila As DataRow In datasetUser.Tables(0).Rows()
                'hGenera.Value = fila(62).ToString
                If (fila(62).ToString.Equals("")) Then
                    hGenera.Value = "0"
                Else
                    hGenera.Value = IIf(fila(62).ToString, "1", "0")
                End If


            Next

            If (hGenera.Value.Equals("1")) Then
                hMaximo.Value = BD.getSiguienteCodigoPuntoUsuario(CInt(Session("IdUsuario")))
            Else
                hMaximo.Value = ""
            End If

            hdurlpuntos.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setPointInfo")
            hdurlpuntosubicacion.Value = ResolveUrl("~/Services/ServiciosCli.asmx/setPointInfoUbicacion")
            'CargaDatosMapa()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        If Not IsPostBack Then
            CargaDatosMapa()
        End If
    End Sub

    Private Sub CargaDatosMapa()
        oPunto = New Punto(Me, Session("PaisLogin"))
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
            datasetUser = oPunto.ConfUsuarioConsultar2(CStr(Session("IdUsuario")))
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
        '    ReferenciaGoogle3 = ""
        'Else
        If AppSettings("Debug") Then
            ReferenciaGoogle3 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & "&key=" & AppSettings("KeyGoogle") & "' type='text/javascript'></script>"
        Else
            ReferenciaGoogle3 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & " type='text/javascript'></script>"
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

        'Try
        '    hdlistamapas.Value = CType(Session("ConfigUsuario"), ConfigUsuario).MotorMapa.ToUpper()
        '    If Session("PermiteGoogle") = 0 Then
        '        hdlistamapas.Value = hdlistamapas.Value.Replace("GM", "")
        '        hdmotormapa.Value = "OS"
        '    Else
        '        If CType(Session("ConfigUsuario"), ConfigUsuario).MotorMapa Like "*GM*" Then
        '            hdmotormapa.Value = "GM"
        '        Else
        '            hdmotormapa.Value = "OS"
        '        End If
        '    End If
        'Catch ex As Exception
        '    hdmotormapa.Value = "OS"
        'End Try

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
            ReferenciaGoogle3 = ""
        Else
            If AppSettings("Debug") Then
                ReferenciaGoogle3 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & "&key=" & AppSettings("KeyGoogle") & "' type='text/javascript'></script>"
            Else
                ReferenciaGoogle3 = "<script src='https://maps.googleapis.com/maps/api/js?v=" & AppSettings("VersionGoogle") & " type='text/javascript'></script>"
            End If
        End If
    End Sub

    Protected Sub gridMapa_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Dim dato As String
        oPunto = New Punto(Me)
        Try
            If (e.Parameters.Equals("validar")) Then
                dato = hdregs.Value.ToUpper
                If (Not oPunto.ValidarPuntoCli(CInt(Session("IdUsuario")), dato)) Then
                    gridMapa.JSProperties("cpValidar") = "0"
                Else
                    gridMapa.JSProperties("cpValidar") = "1"
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub
End Class
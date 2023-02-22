Imports System.Configuration
Imports System.Math
Imports System.Data
Imports System.Configuration.ConfigurationManager
Imports System.Globalization
Imports DevExpress.Web
Imports System

Partial Class Pages_AdminVehicles
    Inherits System.Web.UI.Page

    'Private oData As db_local
    Public ReferenciaGoogle As String
    Private cPuntos As Integer = 0
    Private cUnidades As Integer = 0
    'Private objActivo As Activo
    Private UsuarioRegistroWeb As String = ""
    Public TextoContacto As String = ""

    Public SVN As String
    Public MostrarSuspendidos As Boolean = False
    Private hsEventos As New Hashtable
    Public ColorMain As String

    Private tmpEntidad As String()
    Private tmpUsuarios As String()
    Private tmpActivosEntidad As String()
    Private Ind As Integer = 0

    Private tUnidades As New Hashtable
    Private tUnidadesON As New Hashtable
    Private tUnidadesOFF As New Hashtable
    Private tUnidadesNULL As New Hashtable
    Public tUnidadesSuspendidas As New Hashtable
    Public tVencimientos As New Hashtable

    Private TotalUnidades As Integer = 0

    Private tUnidadesError As New Hashtable
    Private tUnidadesIgnicion As New Hashtable
    Private hsPropietarios As New Hashtable

    'Private BDThor As New Thor_GEOSYSDataContext()
    'Private BDThorT As New ThorTDataContext()
    Public sUnidades As StringBuilder = New StringBuilder()

    Private Sub Pages_AdminVehicles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If IsNothing(Session("Usuario")) Or Session("Usuario") = "" Or Session.Keys.Count = 0 Then
                Funciones.EnviarMensajeJavascript(Me, "Su sesión expiró... ")
                Response.Redirect("LogOut.aspx")

                Exit Sub
            End If

            'Try
            '    hddec.Value = Request.QueryString("CF")

            '    If hddec.Value = "" Then
            '        hddec.Value = 1
            '    End If
            'Catch ex As Exception
            '    hddec.Value = 1
            'End Try

            Try
                SVN = AppSettings("SVN")
                EsconderControles()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

            'Try
            '    BDThor.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
            'Catch ex As Exception
            '    Console.WriteLine(ex.Message)
            'End Try

            'Try
            '    BDThorT.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED")
            'Catch ex As Exception
            '    Console.WriteLine(ex.Message)
            'End Try

            'Try
            '    BDThor.CommandTimeout = 180
            'Catch ex As Exception
            '    Console.WriteLine(ex.Message)
            'End Try

            'Try
            '    BDThorT.CommandTimeout = 180
            'Catch ex As Exception
            '    Console.WriteLine(ex.Message)
            'End Try

            'Try
            '    hdwuid.Value = Guid.NewGuid.ToString() & "--" & Guid.NewGuid.ToString()
            'Catch ex As Exception
            '    hdwuid.Value = Now.ToFileTimeUtc()
            'End Try

            If Not IsNothing(Session("aunidades")) Then
                Try
                    Page.RegisterStartupScript("UnidadesNameScript", Session("aunidades").ToString())
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If

            If Not IsNothing(Session("agrupos")) Then
                Try
                    Page.RegisterStartupScript("GruposNameScript", Session("agrupos").ToString())
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            'hdurlgrupos.Value = ResolveUrl("~/Services/ServiciosCLI.asmx/setGroupInfo")
        End Try
    End Sub

    Private Sub Pages_AdminVehicles_Init(sender As Object, e As EventArgs) Handles Me.Init
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

    Private Sub EsconderControles()
        Try
            'txUsuario.Value = Session("Usuario")
            'idusuario.Value = Session("IdUsuario")
            'idperfil.Value = Session("Perfil")
            'hdpais.Value = Session("Pais")

            If (Session("Nombre").ToString.Length > 25) Then
                'spusuario.InnerText = Session("Nombre").ToString.Substring(0, 24) & "..."
            Else
                'spusuario.InnerText = Session("Nombre")
            End If
            Me.Title = "ESTADO DE LA FLOTA"
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
End Class
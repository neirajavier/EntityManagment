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

Public Class DeleteGeocerca
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarWindow()
        End If
    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        Try
            Dim idGrupo = CInt(lbIdg.Text)
            BD.spGeocercaEliminar(idGrupo)

            Dim startUpScript As String = String.Format("window.parent.HidePopup5();")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
            texto = "Geocerca :: Eliminada con Exito"
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarWindow()
        lbIdg.Text = Request.QueryString("myId").ToString
        lbMessage.Text = Request.QueryString("message").ToString.Replace("_", " ")
    End Sub

End Class
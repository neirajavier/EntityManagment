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
Imports Tulpep.NotificationWindow

Public Class EliminaConductor
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private objConductor As Conductor
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        LlenarWindow()
    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        Try
            Dim startUpScript As String = String.Format("window.parent.EliminaHide();")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

            objConductor = Nothing
            objConductor = New Conductor(Me, Session("PaisLogin")) With {.IdUsuario = Session("IdUsuario"), .Identificacion = lbCedula.Text}
            objConductor.Ejecutar("ELM")
            texto = "Grupo :: Eliminado con Exito"
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarWindow()
        lbCedula.Text = Request.QueryString("myId").ToString
        lbMessage.Text = Request.QueryString("message").ToString
    End Sub

End Class
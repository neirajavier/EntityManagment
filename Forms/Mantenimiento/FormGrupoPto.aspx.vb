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

Public Class FormGrupoPto
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private idUser As Integer
    Private nmUser As String
    Private Usuario As New Object
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        Usuario = BD.spUsuarioConsultar(Request.QueryString("user").ToString).ToList
        For Each us In CType(Usuario, IEnumerable(Of Object))
            idUser = us.IdUsuario
            nmUser = us.Usuario
        Next

        If Not IsPostBack Then
            LlenarWindow()
        End If
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As EventArgs)
        Dim valor As String = tbID.Text
        Dim message As String
        Try
            If (valor.Equals("0") Or valor.Equals("")) Then
                If tbGrupo.Text = "" Then
                    message = "Ingrese un nombre de grupo para continuar"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
                    Return
                End If

                If tbDescripcion.Text = "" Then
                    message = "Ingrese una descripcion del grupo para continuar"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
                    Return
                End If

                If BD.ExisteGrupoPuntos(tbGrupo.Text.Trim, CInt(Session("IdUsuario"))) = 1 Then
                    message = "El grupo que desea ingresar ya existe"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
                    Return
                End If

                BD.spGrupoPuntoIngresar(tbGrupo.Text.ToUpper, tbDescripcion.Text.ToUpper, idUser, cbMostrar.Value.ToString)

                Dim startUpScript As String = String.Format("window.parent.HidePopup3();")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
                message = "Grupo :: Ingresado con Exito"
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','S');")
            Else
                If tbGrupo.Text = "" Then
                    message = "Ingrese un nombre de grupo para continuar"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
                    Return
                End If

                If tbDescripcion.Text = "" Then
                    message = "Ingrese una descripcion del grupo para continuar"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
                    Return
                End If

                BD.spGrupoPuntoActualizar(CInt(tbID.Text), tbGrupo.Text.ToUpper, tbDescripcion.Text.ToUpper, idUser, cbMostrar.Value.ToString)

                Dim startUpScript As String = String.Format("window.parent.HidePopup3();")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
                message = "Grupo :: Actualizado con Exito"
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','I');")
            End If

            LimpiarControles()
        Catch ex As Exception
            Console.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        tbGrupo.Text = ""
        tbDescripcion.Text = ""
    End Sub

    Private Sub LlenarWindow()
        texto = Request.QueryString("estado").ToString

        If (texto.Equals("E")) Then
            tbID.Text = Request.QueryString("myId").ToString
            tbGrupo.Text = Request.QueryString("myGrp").ToString
            tbDescripcion.Text = Request.QueryString("myDes").ToString
        Else
            tbID.Text = ""
            tbGrupo.Text = ""
            tbDescripcion.Text = ""
        End If
    End Sub

End Class
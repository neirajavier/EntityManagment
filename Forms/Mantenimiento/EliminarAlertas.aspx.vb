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

Public Class EliminarAlertas
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private oAlerta As Alerta
    Private tableG As DataTable
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        Dim cadena As String = Request.QueryString("valores").ToString
        Dim miLista As String() = cadena.Split(",")

        If (miLista.Length > 0) Then
            tableG = New DataTable
            tableG.Columns.AddRange(New DataColumn() {New DataColumn("ida", GetType(String))})

            For indice As Integer = 0 To miLista.Count - 1 Step 1
                tableG.Rows.Add(miLista(indice))
            Next

        Else
            texto = "No se pudo cargar la información de las alertas. Cierre la ventana y vuelva a intentar."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
        End If

        lbMessage.Text = "¿Desea eliminar " + CStr(tableG.Rows.Count) + " alerta(s) seleccionada(s)?"
    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        oAlerta = New Alerta(Me, Session("PaisLogin"))
        Try
            For Each row As DataRow In tableG.Rows
                With oAlerta
                    .IdAlerta = CInt(row("ida"))
                    .idUsuario = Session("IdUsuario")

                    .Ejecutar2("ELM")
                End With
            Next
            texto = "La alerta fue eliminada éxitosamente."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S'); window.parent.hideAlerta2();")
        Catch ex As Exception

        End Try
    End Sub
End Class
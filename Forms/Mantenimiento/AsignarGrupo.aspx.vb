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

Public Class AsignarGrupo
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))
        If Not IsPostBack Then
            'LlenarBox()
            LlenarWindow()
        End If
    End Sub

    'Protected Sub ListBoxGrupoDet_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim list As ASPxListBox = TryCast(sender, ASPxListBox)
    '    Dim valor As Integer = 0
    '    If (Not hfIndex.Value.Equals("")) Then
    '        valor = hfIndex.Value
    '    End If

    '    For Each it As ListEditItem In list.Items
    '        Try
    '            If (it.Index = valor And it.Selected = True) Then
    '                BD.spGrupoUnidadesIngresar(CInt(lbactivo.Text), CInt(it.Value.ToString), Session("Usuario").ToString, "")
    '                texto = "Se asignó el grupo " + CStr(it.Text) + " al Vehiculo " + lbalias.Text + "."
    '                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
    '            ElseIf (it.Index = valor And it.Selected = False) Then
    '                BD.spGrupoUnidadesEliminar(CInt(lbactivo.Text), CInt(it.Value.ToString), Session("Usuario").ToString)
    '                texto = "Se quitó asignación del grupo " + CStr(it.Text) + " al Vehiculo " + lbalias.Text + "."
    '                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
    '            End If
    '        Catch ex As Exception
    '            texto = "No se puede asignar Vehiculo " & it.Text & " " & ex.Message + "."
    '            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
    '        End Try
    '    Next
    'End Sub

    'Private Sub LlenarBox()
    '    Try
    '        Dim GruposDet = BD.spGrupoConsultar(CInt(Session("IdUsuario")), "").ToList

    '        Dim GruposAsig = BD.spGrupoListar(CInt(Session("IdUsuario")), 0, Request.QueryString("myVid").ToString).ToList
    '        lbalias.Text = Request.QueryString("myVeh").ToString
    '        lbactivo.Text = Request.QueryString("myAct").ToString
    '        ListBoxGrupoDet.Items.Clear()

    '        For Each grp_ In GruposDet
    '            If (GruposAsig.Count = 0) Then
    '                ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.IdGrupo.ToString))
    '            Else
    '                For Each grpasg In GruposAsig
    '                    If (grpasg.IdGrupo.Equals(grp_.IdGrupo)) Then
    '                        If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.IdGrupo.ToString)) Then
    '                            ListBoxGrupoDet.Items.Add(New ListEditItem(grpasg.Grupo.ToUpper, grpasg.IdGrupo.ToString))
    '                            Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grpasg.IdGrupo.ToString)
    '                            lstitem.Selected = True
    '                        Else
    '                            Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grp_.IdGrupo.ToString)
    '                            lstitem.Selected = True
    '                        End If
    '                    Else
    '                        If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.IdGrupo.ToString)) Then
    '                            ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.IdGrupo.ToString))
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '        Console.WriteLine(ex.StackTrace)
    '    End Try

    'End Sub

    Private Sub LlenarWindow()
        tbEtiqueta.Text = Request.QueryString("myAct").ToString
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As EventArgs)
        Dim message As String
        Try
            'If tbEtiqueta.Text = "" Then
            '    message = "Debe ingresar una descripcion en la etiqueta para continuar"
            '    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','A');")
            '    Return
            'End If

            BD.spActivoEtiquetaActualizar(Request.QueryString("myVid").ToString, CInt(Session("IdUsuario")), tbEtiqueta.Text.ToUpper, Session("Usuario"))

            Dim startUpScript As String = String.Format("window.parent.GrupoWindowHide3();")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
            message = "Etiqueta :: Actualizado con Exito"
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + message + "','I');")

        Catch ex As Exception
            Console.Write(ex.Message)
        End Try
    End Sub

    Private Sub ShowMessage(ByVal message As String, ByVal type As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, System.Guid.NewGuid().ToString, "ShowMessage('" + message + "','" + type + "')", True)
    End Sub

End Class
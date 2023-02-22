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

Public Class AsignarGrupoS
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarBox()
        End If
    End Sub

    Protected Sub ListBoxGrupoSub_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim list As ASPxListBox = TryCast(sender, ASPxListBox)
        Dim valor As Integer = 0
        If (Not hfIndex2.Value.Equals("")) Then
            valor = hfIndex2.Value
        End If

        For Each it As ListEditItem In list.Items
            Try
                If (it.Index = valor And it.Selected = True) Then
                    BD.spGrupoSubUsuariosIngresar(CInt(Session("IdUsuario")), CInt(lbidsubuser.Text), CInt(it.Value.ToString), Session("Usuario").ToString)
                    texto = "Se asignó el grupo " + CStr(it.Text) + " al Subusuario " + lbsubuser.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
                ElseIf (it.Index = valor And it.Selected = False) Then
                    BD.spGrupoSubUsuariosEliminar(CInt(Session("IdUsuario")), CInt(lbidsubuser.Text), CInt(it.Value.ToString))
                    texto = "Se quitó asignación del grupo " + CStr(it.Text) + " al Subusuario " + lbsubuser.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
                End If
            Catch ex As Exception
                texto = "No se puede asignar Vehiculo " & it.Text & " " & ex.Message + "."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
            End Try
        Next

        'LlenarBox()
    End Sub

    Private Sub LlenarBox()
        Try
            Dim GruposDet = BD.spGrupoConsultar(CInt(Session("IdUsuario")), "").ToList

            Dim GruposAsig = BD.spGrupoSubUsuariosConsultar2(CInt(Session("IdUsuario")), CInt(Request.QueryString("mySid").ToString), 0).ToList
            lbsubuser.Text = Request.QueryString("mySub").ToString
            lbidsubuser.Text = Request.QueryString("mySid").ToString
            ListBoxGrupoSub.Items.Clear()
            For Each grp_ In GruposDet
                If (GruposAsig.Count = 0) Then
                    ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.IdGrupo.ToString))
                Else
                    For Each grpasg In GruposAsig
                        If (grpasg.IdGrupo.Equals(grp_.IdGrupo)) Then
                            If IsNothing(ListBoxGrupoSub.Items.FindByValue(grp_.IdGrupo.ToString)) Then
                                ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.IdGrupo.ToString))
                                Dim lstitem As ListEditItem = ListBoxGrupoSub.Items.FindByValue(grp_.IdGrupo.ToString)
                                lstitem.Selected = True
                            Else
                                Dim lstitem As ListEditItem = ListBoxGrupoSub.Items.FindByValue(grp_.IdGrupo.ToString)
                                lstitem.Selected = True
                            End If
                        Else
                            If IsNothing(ListBoxGrupoSub.Items.FindByValue(grp_.IdGrupo.ToString)) Then
                                ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.IdGrupo.ToString))
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

End Class
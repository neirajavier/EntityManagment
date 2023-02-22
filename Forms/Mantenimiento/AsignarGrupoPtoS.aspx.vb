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

Public Class AsignarGrupoPtoS
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarBox()
        End If
    End Sub

    Private Sub LlenarBox()
        Try
            Dim GruposDet = BD.spGrupoPuntoConsultar(CInt(Session("IdUsuario")), 0).ToList

            Dim GruposAsig = BD.spGrupoPuntoSubusuarioConsultar(0, CInt(Session("IdUsuario")), CInt(Request.QueryString("mySid").ToString)).ToList
            lbsubuser.Text = Request.QueryString("mySub").ToString
            lbidsubuser.Text = Request.QueryString("mySid").ToString
            ListBoxGrupoSub.Items.Clear()

            For Each grp_ In GruposDet
                If (GruposAsig.Count = 0) Then
                    ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.idGrupoPunto.ToString))
                Else
                    For Each grpasg In GruposAsig
                        If (grpasg.idGrupoPunto.Equals(grp_.idGrupoPunto)) Then
                            If IsNothing(ListBoxGrupoSub.Items.FindByValue(grp_.idGrupoPunto.ToString)) Then
                                ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.idGrupoPunto.ToString))
                                Dim lstitem As ListEditItem = ListBoxGrupoSub.Items.FindByValue(grp_.idGrupoPunto.ToString)
                                lstitem.Selected = True
                            Else
                                Dim lstitem As ListEditItem = ListBoxGrupoSub.Items.FindByValue(grp_.idGrupoPunto.ToString)
                                lstitem.Selected = True
                            End If
                        Else
                            If IsNothing(ListBoxGrupoSub.Items.FindByValue(grp_.idGrupoPunto.ToString)) Then
                                ListBoxGrupoSub.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.idGrupoPunto.ToString))
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

    Protected Sub ListBoxGrupoSub_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim list As ASPxListBox = TryCast(sender, ASPxListBox)
        Dim valor As Integer = hfIndex2.Value

        For Each it As ListEditItem In list.Items
            Try
                If (it.Index = valor And it.Selected = True) Then
                    BD.spGrupoPuntoSubUsuarioIngresar(CInt(it.Value.ToString), CInt(Session("IdUsuario")), CInt(lbidsubuser.Text))
                    texto = "Se asignó el grupo " + CStr(it.Text) + " al Subusuario " + lbsubuser.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
                ElseIf (it.Index = valor And it.Selected = False) Then
                    BD.spGrupoPuntoSubUsuarioEliminar(CInt(it.Value.ToString), CInt(Session("IdUsuario")), CInt(lbidsubuser.Text))
                    texto = "Se quitó asignación del grupo " + CStr(it.Text) + " al Subusuario " + lbsubuser.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
                End If
            Catch ex As Exception
                texto = "No se puede asignar Punto Referencial " & it.Text & " " & ex.Message + "."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
            End Try
        Next
    End Sub

End Class
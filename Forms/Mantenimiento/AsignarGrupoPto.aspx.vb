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

Public Class AsignarGrupoPto
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarBox()
        End If
    End Sub

    Protected Sub ListBoxGrupoDet_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim list As ASPxListBox = TryCast(sender, ASPxListBox)
        Dim valor As Integer = 0
        If (Not hfIndex.Value.Equals("")) Then
            valor = hfIndex.Value
        End If

        For Each it As ListEditItem In list.Items
            Try
                If (it.Index = valor And it.Selected = True) Then
                    BD.spPuntoGrupoIngresar(CInt(it.Value.ToString), CInt(lbactivo.Text), CInt(Session("IdUsuario")))
                    texto = "Se asignó el grupo " + CStr(it.Text) + " al Punto Referencial " + lbalias.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
                ElseIf (it.Index = valor And it.Selected = False) Then
                    BD.spPuntoGrupoEliminar(CInt(it.Value.ToString), CInt(lbactivo.Text), CInt(Session("IdUsuario")))
                    texto = "Se quitó asignación del grupo " + CStr(it.Text) + " al Punto Referencial " + lbalias.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
                End If
            Catch ex As Exception
                texto = "No se puede asignar Punto Referencial " & it.Text & " " & ex.Message + "."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
            End Try
        Next
    End Sub

    Private Sub LlenarBox()
        Try
            Dim GruposDet = BD.spGrupoPuntoConsultar(CInt(Session("IdUsuario")), 0).ToList
            Dim GruposAsig = BD.spPuntoGrupoConsultar(0, Request.QueryString("myPid").ToString, CInt(Session("IdUsuario"))).ToList

            If (Request.QueryString("myPto").ToString.Equals("SP")) Then
                lbalias.Text = ""
            Else
                lbalias.Text = Request.QueryString("myPto").ToString.Replace("%", " ")
            End If
            lbactivo.Text = Request.QueryString("myPid").ToString
            ListBoxGrupoDet.Items.Clear()

            For Each grp_ In GruposDet
                If (GruposAsig.Count = 0) Then
                    ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.idGrupoPunto.ToString))
                Else
                    For Each grpasg In GruposAsig
                        If (grpasg.idGrupoPunto.Equals(grp_.idGrupoPunto)) Then
                            If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoPunto.ToString)) Then
                                ListBoxGrupoDet.Items.Add(New ListEditItem(grpasg.Grupo.ToUpper, grpasg.idGrupoPunto.ToString))
                                Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grpasg.idGrupoPunto.ToString)
                                lstitem.Selected = True
                            Else
                                Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoPunto.ToString)
                                lstitem.Selected = True
                            End If
                        Else
                            If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoPunto.ToString)) Then
                                ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Grupo.ToUpper, grp_.idGrupoPunto.ToString))
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
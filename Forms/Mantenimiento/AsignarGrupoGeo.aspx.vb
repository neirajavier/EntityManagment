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

Public Class AsignarGrupoGeo
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
                    BD.spGeocercaGrupoInsertar(CInt(it.Value.ToString), CInt(lbactivo.Text), CInt(Session("IdUsuario")))
                    texto = "Se asignó el grupo " + CStr(it.Text) + " a la Geocerca " + lbalias.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
                ElseIf (it.Index = valor And it.Selected = False) Then
                    BD.spGeocercaGrupoEliminar(CInt(it.Value.ToString), CInt(lbactivo.Text), CInt(Session("IdUsuario")))
                    texto = "Se quitó asignación del grupo " + CStr(it.Text) + " a la Geocerca " + lbalias.Text + "."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','E');")
                End If
            Catch ex As Exception
                texto = "No se puede asignar Geocerca " & it.Text & " " & ex.Message + "."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
            End Try
        Next
    End Sub

    Private Sub LlenarBox()
        Try
            Dim GruposDet = BD.spGrupoGeocercaConsultar(CInt(Session("IdUsuario")), 0).ToList

            Dim GruposAsig = BD.spGeocercaGrupoConsultarV2(0, Request.QueryString("myGid").ToString, CInt(Session("IdUsuario"))).ToList

            lbalias.Text = Request.QueryString("myGeo").ToString.Replace("_", " ")
            lbactivo.Text = Request.QueryString("myGid").ToString
            ListBoxGrupoDet.Items.Clear()

            For Each grp_ In GruposDet
                If (GruposAsig.Count = 0) Then
                    ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
                Else
                    For Each grpasg In GruposAsig
                        If (grpasg.idGrupoGeocerca.Equals(grp_.idGrupoGeocerca)) Then
                            If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                                ListBoxGrupoDet.Items.Add(New ListEditItem(grpasg.Grupo.ToUpper, grpasg.idGrupoGeocerca.ToString))
                                Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grpasg.idGrupoGeocerca.ToString)
                                lstitem.Selected = True
                            Else
                                Dim lstitem As ListEditItem = ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoGeocerca.ToString)
                                lstitem.Selected = True
                            End If
                        Else
                            If IsNothing(ListBoxGrupoDet.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                                ListBoxGrupoDet.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
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
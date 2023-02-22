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

Public Class AsignarGrupoGeoS2
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private tableG As DataTable
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        Dim cadena As String = Request.QueryString("valores").ToString
        Dim miLista As String() = cadena.Split(",")

        If (miLista.Length > 0) Then
            tableG = New DataTable
            tableG.Columns.AddRange(New DataColumn() {New DataColumn("sid", GetType(String))})
            For Each sid In miLista
                tableG.Rows.Add(sid)
            Next

            If Not IsPostBack Then
                LlenarBox()
            End If
        Else
            texto = "No se pudo cargar la información de los subusuarios. Cierre la ventana y vuelva a intentar."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
        End If

    End Sub

    Protected Sub btnAsignarG_Click(sender As Object, e As EventArgs)
        Dim btn As ASPxButton = TryCast(sender, ASPxButton)
        Try
            Dim startUpScript As String = String.Format("window.parent.HidePopup2();")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

            texto = "Los actualizaciones en las asignaciones se realizaron satisfactoriamente."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
            texto = "Hubo un error al realizar las asignaciones."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
        End Try
    End Sub

    Protected Sub ListBoxGrupoSub2_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim list As ASPxListBox = TryCast(sender, ASPxListBox)
        Dim valor As Integer = 0
        Dim GruposAsig = Nothing

        If (Not hfIndex.Value.Equals("")) Then
            valor = hfIndex.Value
        End If

        For Each it As ListEditItem In list.Items
            Try
                For Each row As DataRow In tableG.Rows
                    If (it.Index = valor And it.Selected = True) Then
                        GruposAsig = BD.spGrupoGeocercaSubusuarioConsultarV2(CInt(it.Value.ToString), CInt(Session("IdUsuario")), CInt(row("sid").ToString)).ToList
                        If (GruposAsig.Count = 0) Then
                            BD.spGrupoGeocercaSubUsuarioIngresar(CInt(it.Value.ToString), CInt(Session("IdUsuario")), CInt(row("sid").ToString))
                        End If
                    ElseIf (it.Index = valor And it.Selected = False) Then
                        GruposAsig = BD.spGrupoGeocercaSubusuarioConsultarV2(CInt(it.Value.ToString), CInt(Session("IdUsuario")), CInt(row("sid").ToString)).ToList
                        If (GruposAsig.Count > 0) Then
                            BD.spGrupoGeocercaSubUsuarioEliminar(CInt(Session("IdUsuario")), CInt(row("sid").ToString), CInt(it.Value.ToString))
                        End If
                    End If
                Next
            Catch ex As Exception
                texto = "No se puede asignar Subusuario " & it.Text & " " & ex.Message + "."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','A');")
            End Try
        Next
    End Sub

    Private Sub LlenarBox()
        Try
            Dim contadorG As Integer = 0
            Dim GruposAsig = Nothing
            Dim grupoData As Integer = 0
            Dim GruposDet = BD.spGrupoGeocercaConsultar(CInt(Session("IdUsuario")), 0).ToList
            ListBoxGrupoSub2.Items.Clear()

            For Each grp_ In GruposDet
                contadorG = 0
                If (tableG.Rows.Count = 1) Then
                    For Each row As DataRow In tableG.Rows
                        GruposAsig = BD.spGrupoGeocercaSubusuarioConsultarV2(grp_.idGrupoGeocerca, CInt(Session("IdUsuario")), CInt(row("sid").ToString)).ToList

                        If (GruposAsig.Count > 0) Then
                            contadorG += 1
                        End If
                    Next

                    If (contadorG >= 1) Then
                        If IsNothing(ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                            ListBoxGrupoSub2.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
                            Dim lstitem As ListEditItem = ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)
                            lstitem.Selected = True
                        End If
                    Else
                        If IsNothing(ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                            ListBoxGrupoSub2.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
                        End If
                    End If
                ElseIf (tableG.Rows.Count > 1) Then
                    For Each row As DataRow In tableG.Rows
                        GruposAsig = BD.spGrupoGeocercaSubusuarioConsultarV2(grp_.idGrupoGeocerca, CInt(Session("IdUsuario")), CInt(row("sid").ToString)).ToList

                        If (GruposAsig.Count > 0) Then
                            contadorG += 1
                        End If
                    Next

                    If (contadorG >= 2) Then
                        If IsNothing(ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                            ListBoxGrupoSub2.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
                            Dim lstitem As ListEditItem = ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)
                            lstitem.Selected = True
                        End If
                    Else
                        If IsNothing(ListBoxGrupoSub2.Items.FindByValue(grp_.idGrupoGeocerca.ToString)) Then
                            ListBoxGrupoSub2.Items.Add(New ListEditItem(grp_.Nombre.ToUpper, grp_.idGrupoGeocerca.ToString))
                        End If
                    End If
                End If
            Next
            ListBoxGrupoSub2.Focus()
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try

    End Sub

End Class
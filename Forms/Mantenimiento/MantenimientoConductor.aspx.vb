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
Imports Newtonsoft.Json
Imports System.ComponentModel

Partial Class MantenimientoConductor
    Inherits System.Web.UI.Page

    Private myflag1 = True
    Private objConductor As Conductor
    Private Conductores
    Private BD As New OdinDataContext()

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("TableConductorT") IsNot Nothing) Then
            gridConductor.DataSource = TryCast(Session("TableConductorT"), DataTable)
            gridConductor.DataBind()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Codigo para pruebas internas''
            Dim valor As New Object()

            'If Request.QueryString("CP") <> "" Then
            '    Try
            '        BD = New OdinDataContext(Globales.getCadenaConexionPais(Request.QueryString("CP").ToUpper, False))
            '        Session("PaisLogin") = Request.QueryString("CP").ToUpper.ToString
            '    Catch ex As Exception
            '        Console.WriteLine(ex.Message)
            '    End Try
            'End If

            BD = New OdinDataContext(Globales.getCadenaConexionPais("PE", False))
            Session("PaisLogin") = "PE"

            If Request.QueryString("o") <> "" Then
                Try
                    Dim Keys = BD.spUsuarioConsultarKey(Request.QueryString("O")).ToList
                    For Each Key In Keys
                        Try
                            hidusuario.Value = Key.IdUsuario
                            Session("Usuario") = Key.Usuario
                            Session("Clave") = Key.Clave
                        Catch ex As Exception
                            hidusuario.Value = -1
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    hidusuario.Value = IIf(Keys.Count = 0, 0, hidusuario.Value)
                    If hidusuario.Value = 0 Then
                        Dim KeysS = BD.spSubUsuarioConsultarKey(Request.QueryString("O")).ToList
                        For Each Key In KeysS
                            Try
                                hidusuario.Value = Key.IdUsuario
                                hidsubusuario.Value = Key.IdSubUsuario
                                Session("Usuario") = Key.SubUsuario
                                Session("SubUsuario") = Key.SubUsuario
                                Session("Clave") = Key.Clave
                            Catch ex As Exception
                                hidusuario.Value = -1
                                Console.WriteLine(ex.Message)
                            End Try
                        Next
                        Session("idSubUsuario") = hidsubusuario.Value
                    End If
                    Session("IdUsuario") = hidusuario.Value
                    Keys = Nothing
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Else
                Funciones.EjecutarFuncionJavascript(Me, "window.close();")
            End If

            If Session("baseURL13") Is Nothing Then
                Session("baseURL13") = "NuevoConductor.aspx"
            End If
            If Session("baseURL14") Is Nothing Then
                Session("baseURL14") = "EditarConductor.aspx"
            End If
            If Session("baseURL15") Is Nothing Then
                Session("baseURL15") = "EliminaConductor.aspx"
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            If Not IsPostBack Then
                LlenarConductores()

                objConductor = New Conductor(Me, Session("PaisLogin"))
                Dim cdt As Integer = objConductor.ContarConductores(CInt(Session("IdUsuario")))
                'btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Conductores</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(cdt) + "</span></div> </div>"
                lbCount.Text = "<div style='padding-top: 2px'> <span class='badge badge-dark' style='float: right; font-size: 12px;'>" + CStr(cdt) + "</span></div>"
            End If

        End Try
    End Sub

    Private Sub LlenarConductores()
        Try
            'If (myflagV) Then
            If (Not IsPostBack) AndAlso (Not IsCallback) Then
                Dim id As New GridViewDataColumn()
                id.FieldName = "id"
                id.Caption = "Id"
                id.VisibleIndex = "2"
                id.CellStyle.Font.Size = "10"
                id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                id.CellStyle.Border.BorderStyle = BorderStyle.None
                gridConductor.Columns.Add(id)
                Dim data As New GridViewDataColumn()
                data.FieldName = "nombres"
                data.Caption = "Descripcion"
                data.VisibleIndex = "3"
                data.CellStyle.Font.Size = "10"
                data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                data.CellStyle.Border.BorderStyle = BorderStyle.None
                gridConductor.Columns.Add(data)
            End If

            If (Not IsPostBack) Then
                gridConductor.DataBind()
            Else
                gridConductor.DataBind()
            End If
            'End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridC_DataBinding(sender As Object, e As EventArgs)
        gridConductor.DataSource = GetTableConductor()
        'Dim height As Integer = CInt(hfHeight.Value)
        'gridConductor.SettingsPager.PageSize = ((height / 100) * 2) + 2
    End Sub

    Private Function GetTableConductor() As DataTable
        Dim table As DataTable
        Dim Conductor As New Object()

        'If (Session("TableConductorT") IsNot Nothing) Then
        '    table = TryCast(Session("TableConductorT"), DataTable)
        'Else
        '    Conductor = BD.spConductorConsultar(CInt(Session("IdUsuario"))).ToList
        '    table = TryCast(Conductor, DataTable)

        '    If Table Is Nothing Then
        '        Table = New DataTable()
        '        Table.Columns.Add("id", GetType(String))
        '        Table.Columns.Add("nombres", GetType(String))

        '        For Each cdt In CType(Conductor, IEnumerable(Of Object))
        '            Table.Rows.Add(cdt.Identificacion.ToString, cdt.NombreCompleto.ToString.ToUpper)
        '        Next

        '        Session("TableConductorT") = Table
        '    End If
        'End If

        Conductor = BD.spConductorConsultar(CInt(Session("IdUsuario"))).ToList
        table = TryCast(Conductor, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("nombres", GetType(String))

            For Each cdt In CType(Conductor, IEnumerable(Of Object))
                table.Rows.Add(cdt.Identificacion.ToString, cdt.NombreCompleto.ToString.ToUpper)
            Next

            Session("TableConductorT") = table
        End If

        Return Table
    End Function

    Protected Sub btnNuevo_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)

            If Session("baseURL13") Is Nothing Then
                Session("baseURL13") = "NuevoConductor.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?myId={1}", Session("baseURL13"), "0")

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ OnMoreInfoClick('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub editar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL14") Is Nothing Then
                Session("baseURL14") = "EditarConductor.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim contentUrl As String = String.Format("{0}?myId={1}", Session("baseURL14"), id)

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ OnMoreInfoClick2('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub eliminar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL15") Is Nothing Then
                Session("baseURL15") = "EliminaConductor.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim message As String = "¿Desea eliminar el registro del conductor con C.I. " + id + "?"
            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL15"), id, message)

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ OnMoreInfoClick3('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridConductor_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Dim miLista As New List(Of String)()
        Dim JSONString As String = Nothing
        objConductor = New Conductor(Me, Session("PaisLogin"))
        Dim cdt As Integer = objConductor.ContarConductores(CInt(Session("IdUsuario")))
        Dim height As Integer = CInt(hfHeight.Value)

        miLista.Add(CStr(cdt))
        JSONString = JsonConvert.SerializeObject(miLista)

        gridConductor.JSProperties("cpNumbersJS") = JSONString

        gridConductor.DataBind()
        If (height <= 500) Then
            gridConductor.SettingsPager.PageSize = ((height / 100) * 2) - 5
        ElseIf (height > 500 And height <= 900) Then
            gridConductor.SettingsPager.PageSize = ((height / 100) * 2) + 1
        ElseIf (height > 900) Then
            gridConductor.SettingsPager.PageSize = ((height / 100) * 2)
        End If
    End Sub

End Class
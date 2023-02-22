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
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports AjaxControlToolkit

Public Class MantenimientoGeocerca
    Inherits System.Web.UI.Page

    Private myflagG As Boolean = True
    Private myflagV As Boolean = True
    Private myflagS As Boolean = True
    Private myFlagCustom1 As Boolean = False
    Private myFlagCustom2 As Boolean = False
    Private myFlagCustom3 As Boolean = False
    Private myFlagCustom4 As Boolean = True
    Private myGroup As Integer = 0
    Private tableAux As DataTable
    Private Geocercas
    Private Subusuarios
    Private numGeocercas As Integer = 0
    Private numSubuser As Integer = 0

    Private oGeocerca As Geocerca
    Private objUsuario As Usuario
    Private BD As New OdinDataContext()

    Public ReferenciaGoogle As String
    Public SVN As String
    Private IP As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("TableGeocercasT") IsNot Nothing) Then
            gridGeocerca.DataSource = TryCast(Session("TableGeocercasT"), DataTable)
            gridGeocerca.DataBind()
        End If
        'If (Session("TableSubusuariosT") IsNot Nothing) Then
        '    gridSubusuario.DataSource = TryCast(Session("TableSubusuariosT"), DataTable)
        '    gridSubusuario.DataBind()
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Codigo para pruebas internas''
            Dim valor As New Object()
            btnAsignar.ClientVisible = False
            'btnAsignarS.ClientVisible = False
            'objUsuario = New Usuario(Me)

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
                    If hidusuario.Value = 0 Or hidusuario.Value.Equals("") Then
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

            If Session("baseURL7") Is Nothing Then
                Session("baseURL7") = "AsignarGrupoGeo.aspx"
            End If
            If Session("baseURL8") Is Nothing Then
                Session("baseURL8") = "AsignarGrupoGeo2.aspx"
            End If
            If Session("baseURL9") Is Nothing Then
                Session("baseURL9") = "AsignarGrupoGeoS.aspx"
            End If
            If Session("baseURL10") Is Nothing Then
                Session("baseURL10") = "AsignarGrupoGeoS2.aspx"
            End If
            If Session("baseURL11") Is Nothing Then
                Session("baseURL11") = "FormGrupoGeo.aspx"
            End If
            If Session("baseURL12") Is Nothing Then
                Session("baseURL12") = "DeleteGrupoGeo.aspx"
            End If
            If Session("baseURL12.") Is Nothing Then
                Session("baseURL12.") = "DeleteGrupoGeo.aspx"
            End If

            objUsuario = New Usuario(Me, Session("PaisLogin"))
            If Not IsPostBack Then
                If (objUsuario.ValidarUsuario2(Session("Usuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave"))) Then
                    Session("esUsuario") = True
                    hdregs.Value = "1"
                ElseIf objUsuario.ValidarSubUsuario2(Session("SubUsuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
                    Session("Usuario") = objUsuario.ConsultarUsuarioNombre(CInt(Session("IdUsuario")))
                    Session("esUsuario") = False
                    hdregs.Value = "0"
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Dim miLista As New List(Of String)()
            Dim JSONString As String = Nothing

            If Not IsPostBack Then
                LlenarGrupos()
                LlenarGeocerca()
                'If (Session("esUsuario")) Then
                '    LlenarSubUsuarios()
                'End If

                If (gridGrupo.FocusedRowIndex = -1) Then
                    oGeocerca = New Geocerca(Me, Session("PaisLogin"))
                    If (Session("esUsuario")) Then
                        numGeocercas = oGeocerca.ContarGeocercas(CInt(Session("IdUsuario")))
                    Else
                        numGeocercas = BD.spGeocercaSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                    End If
                    'numSubuser = oGeocerca.ContarSubusuarios(CInt(Session("IdUsuario")))

                    miLista.Add(CStr(numGeocercas))
                    'miLista.Add(CStr(numSubuser))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridGeocerca.JSProperties("cpNumbersJS") = JSONString

                    hgeocercas.Value = numGeocercas
                    'hsubuser.Value = numSubuser

                    'btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Geocercas</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numGeocercas) + "</span></div> </div>"
                    'btnTodos2.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Subusuarios</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numSubuser) + "</span></div> </div>"
                    lbCount.Text = "<div style='padding-top: 2px'> <span class='badge badge-dark' style='float: right; font-size: 12px;'>" + CStr(numGeocercas) + "</span></div>"
                End If
            Else
                If (gridGrupo.FocusedRowIndex = -1) Then
                    oGeocerca = New Geocerca(Me, Session("PaisLogin"))
                    If (Session("esUsuario")) Then
                        numGeocercas = oGeocerca.ContarGeocercas(CInt(Session("IdUsuario")))
                    Else
                        numGeocercas = BD.spGeocercaSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                    End If
                    'numSubuser = oGeocerca.ContarSubusuarios(CInt(Session("IdUsuario")))

                    miLista.Add(CStr(numGeocercas))
                    'miLista.Add(CStr(numSubuser))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridGeocerca.JSProperties("cpNumbersJS") = JSONString

                    hgeocercas.Value = numGeocercas
                    'hsubuser.Value = numSubuser
                End If
            End If
        End Try
    End Sub

#Region "Grupo"
    Private Sub LlenarGrupos()
        Try
            If (Not IsPostBack) AndAlso (Not IsCallback) Then
                Dim id As New GridViewDataColumn()
                id.FieldName = "id"
                id.Caption = "Id"
                id.VisibleIndex = "0"
                id.SetColVisible(False)
                id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                id.CellStyle.Border.BorderStyle = BorderStyle.None
                gridGrupo.Columns.Add(id)
                Dim data As New GridViewDataTextColumn()
                data.FieldName = "grupo"
                data.Caption = "Grupo"
                data.VisibleIndex = "1"
                data.Width = "35"
                data.CellStyle.Font.Size = "9"
                data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                data.CellStyle.Border.BorderStyle = BorderStyle.None
                data.PropertiesEdit.EncodeHtml = False
                gridGrupo.Columns.Add(data)
                Dim descripcion As New GridViewDataColumn()
                descripcion.FieldName = "descripcion"
                descripcion.Caption = "Descripcion"
                descripcion.VisibleIndex = "2"
                descripcion.SetColVisible(False)
                descripcion.HeaderStyle.Border.BorderStyle = BorderStyle.None
                descripcion.CellStyle.Border.BorderStyle = BorderStyle.None
                gridGrupo.Columns.Add(descripcion)

                gridGrupo.Settings.ShowColumnHeaders = False
            End If

            If (Not IsPostBack) Then
                gridGrupo.DataBind()
            Else
                gridGrupo.DataBind()
            End If
            gridGrupo.FocusedRowIndex = -1
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub GridG_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        gridGrupo.DataSource = GetTableGridGrupo()
    End Sub

    Private Function GetTableGridGrupo() As DataTable
        Dim Grupos As New Object()
        Grupos = BD.spGrupoGeocercaConsultar(CInt(Session("IdUsuario")), 0).ToList

        Dim table As DataTable = TryCast(Grupos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("grupo", GetType(String))
            table.Columns.Add("descripcion", GetType(String))
            table.Columns.Add("edt", GetType(Boolean))
            table.Columns.Add("del", GetType(Boolean))

            For Each grp In CType(Grupos, IEnumerable(Of Object))
                table.Rows.Add(grp.IdGrupoGeocerca.ToString, grp.Nombre.ToString.ToUpper, grp.Descripcion.ToString.ToUpper, True, True)
            Next

            Session("TableGrupos") = table
        End If

        Return table
    End Function

    Protected Sub gridGrupo_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If (e.DataColumn.FieldName.Equals("grupo")) Then
            If (Not e.CellValue.Equals("")) Then
                e.Cell.ToolTip = e.CellValue.ToString()
            End If
        End If
    End Sub

    Protected Sub gridGrupo_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "grupo" Then
            If (e.Value.ToString.Length > 12) Then
                'Dim Grupos = BD.spGrupoConsultar(CInt(Session("IdUsuario")), e.Value.ToString.ToUpper).ToList
                e.DisplayText = (e.Value.ToString.Substring(0, 12).ToUpper) + "..."
                'e.EncodeHtml = False
                'For Each grp In CType(Grupos, IEnumerable(Of Object))
                '    e.DisplayText = (grp.Grupo.Substring(0, 10).ToUpper) + "... <span class='badge badge-dark' style='float:right; font-size: 11px;'>" + CStr(grp.Cantidad) + "</span>"
                'Next
            Else
                'Dim Grupos = BD.spGrupoConsultar(CInt(Session("IdUsuario")), e.Value.ToString.ToUpper).ToList
                e.DisplayText = (e.Value.ToString).ToUpper
                'e.EncodeHtml = False
                'For Each grp In CType(Grupos, IEnumerable(Of Object))
                '    e.DisplayText = (grp.Grupo).ToUpper + "<span class='badge badge-dark' style='float:right; font-size: 11px;'>" + CStr(grp.Cantidad) + "</span>"
                'Next
            End If
        End If
    End Sub

    Protected Sub gridGrupo_PreRender(sender As Object, e As EventArgs)
        If (Not IsPostBack) Then
            gridGrupo.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub btnGrupoN_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)

            If Session("baseURL11") Is Nothing Then
                Session("baseURL11") = "FormGrupoGeo.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL11"), "0", "0", "0", "N", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "N")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub editar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL11") Is Nothing Then
                Session("baseURL11") = "FormGrupoGeo.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grp As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim des As String = DataBinder.Eval(parent.DataItem, "descripcion")

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL11"), id, grp, des, "E", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "E")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub borrar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL12") Is Nothing Then
                Session("baseURL12") = "DeleteGrupoGeo.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim message As String = "¿Desea eliminar el grupo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL12"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowMessage('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridGrupo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("1")) Then
            gridGrupo.DataBind()
        ElseIf (e.Parameters.Equals("2")) Then
            gridGrupo.FocusedRowIndex = -1
        End If
    End Sub

#End Region

#Region "Geocercas"
    Private Sub LlenarGeocerca()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "id"
                    id.Caption = "Nombre"
                    id.VisibleIndex = "2"
                    id.Width = "400"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.Name = "geocerca"
                    gridGeocerca.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "data"
                    data.Caption = "Tipo"
                    data.VisibleIndex = "3"
                    data.Width = "50"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "tipo"
                    gridGeocerca.Columns.Add(data)
                    Dim gid As New GridViewDataColumn()
                    gid.FieldName = "gid"
                    gid.Caption = "GId"
                    gid.VisibleIndex = "4"
                    gid.SetColVisible(False)
                    gid.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    gid.CellStyle.Border.BorderStyle = BorderStyle.None
                    gid.Name = "idgeo"
                    gridGeocerca.Columns.Add(gid)
                    Dim gpa As New GridViewDataColumn()
                    gpa.FieldName = "gpa"
                    gpa.Caption = "Ancho / Radio"
                    gpa.VisibleIndex = "5"
                    gpa.Width = "70"
                    gpa.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    gpa.CellStyle.Border.BorderStyle = BorderStyle.None
                    gpa.Name = "param"
                    gridGeocerca.Columns.Add(gpa)
                End If

                If (Not IsPostBack) Then
                    gridGeocerca.DataBind()
                Else
                    gridGeocerca.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub GridGe_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

        If (myFlagCustom1) Then
            If (grupos <= 0) Then
                gridGeocerca.DataSource = GetTableGridGeocercasT()
            Else
                If (grupos > 0) Then
                    If (gridGrupo.FocusedRowIndex = -1) Then
                        gridGeocerca.DataSource = GetTableGridGeocercasT()
                    Else
                        If (myflagV) Then
                            gridGeocerca.DataSource = GetTableGridGeocercasT()
                        Else
                            If (myFlagCustom4) Then
                                gridGeocerca.DataSource = GetTableGridGeocercasT()
                            Else
                                gridGeocerca.DataSource = GetTableGridGeocercas()
                            End If

                        End If

                    End If
                End If
            End If
        ElseIf (myFlagCustom2) Then
            gridGeocerca.DataSource = GetTableGridGeocercasT()
        ElseIf (myFlagCustom3) Then
            If (grupos > 0) Then
                If (myFlagCustom4) Then
                    gridGeocerca.DataSource = GetTableGridGeocercasT()
                    gridGrupo.FocusedRowIndex = -1
                Else
                    gridGeocerca.DataSource = GetTableGridGeocercas()
                End If
            Else
                gridGeocerca.DataSource = GetTableGridGeocercasT()
            End If
        Else
            If (gridGrupo.FocusedRowIndex = -1) Then
                gridGeocerca.DataSource = GetTableGridGeocercasT()
            Else
                gridGeocerca.DataSource = GetTableGridGeocercas()
            End If

        End If

    End Sub

    Private Function GetTableGridGeocercas() As DataTable
        Dim table As DataTable
        Dim radio As String
        Dim Geocercas As New Object()
        Dim GeocercaDet As New Object()

        If (myGroup = 0) Then
            myGroup = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
        End If

        Geocercas = BD.spGeocercaGrupoConsultar(CInt(Session("IdUsuario")), myGroup).ToList
        table = TryCast(Geocercas, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("gid", GetType(String))
            table.Columns.Add("gpa", GetType(String))
            table.Columns.Add("uid", GetType(String))

            For Each Geocerca In CType(Geocercas, IEnumerable(Of Object))
                GeocercaDet = BD.spGeocercaConsultarCli(Geocerca.IdGeocerca).ToList
                For Each Geodet In CType(GeocercaDet, IEnumerable(Of Object))
                    If (Geodet.Parametro1.Equals("0") And Geodet.Tipo_Geocerca.Equals("Poligonal")) Then
                        radio = ""
                    Else
                        'radio = CStr(Math.Truncate(CDbl(Geodet.Parametro1)))
                        radio = Geodet.Parametro1
                    End If
                    table.Rows.Add(Geodet.Nombre.ToString, Geodet.Tipo_Geocerca.ToString, Geodet.IdGeocerca.ToString, radio, Geodet.IdUsuario.ToString)
                Next
            Next

            Session("TableGeocercas") = table
        End If

        'If (Session("TableGeocercas") IsNot Nothing) Then
        '    table = TryCast(Session("TableGeocercas"), DataTable)
        'Else
        '    Geocercas = BD.spGeocercaGrupoConsultar(CInt(Session("IdUsuario")), myGroup).ToList
        '    table = TryCast(Geocercas, DataTable)

        '    If table Is Nothing Then
        '        table = New DataTable()
        '        table.Columns.Add("id", GetType(String))
        '        table.Columns.Add("data", GetType(String))
        '        table.Columns.Add("gid", GetType(String))
        '        table.Columns.Add("uid", GetType(String))

        '        For Each Geocerca In CType(Geocercas, IEnumerable(Of Object))
        '            GeocercaDet = BD.spGeocercaConsultarCli(Geocerca.IdGeocerca).ToList
        '            For Each Geodet In CType(GeocercaDet, IEnumerable(Of Object))
        '                table.Rows.Add(Geodet.Nombre.ToString, Geodet.Tipo_Geocerca.ToString, Geodet.IdGeocerca.ToString, Geodet.IdUsuario.ToString)
        '            Next
        '        Next

        '        Session("TableGeocercas") = table
        '    End If
        'End If

        Return table
    End Function

    Private Function GetTableGridGeocercasT() As DataTable
        Dim table As DataTable
        Dim radio As String
        Dim indice As Integer
        Dim Geocercas As New Object()
        Dim GeocercaDet As New Object()

        'If (Session("TableGeocercasT") IsNot Nothing) Then
        '    table = TryCast(Session("TableGeocercasT"), DataTable)
        'Else
        '    Geocercas = BD.spGeocercaConsultar(CInt(Session("IdUsuario"))).ToList
        '    table = TryCast(Geocercas, DataTable)

        '    If table Is Nothing Then
        '        table = New DataTable()
        '        table.Columns.Add("id", GetType(String))
        '        table.Columns.Add("data", GetType(String))
        '        table.Columns.Add("gid", GetType(String))
        '        table.Columns.Add("gpa", GetType(String))
        '        table.Columns.Add("uid", GetType(String))

        '        For Each Geocerca In CType(Geocercas, IEnumerable(Of Object))
        '            table.Rows.Add(Geocerca.Nombre.ToString, Geocerca.Tipo_Geocerca.ToString, Geocerca.IdGeocerca.ToString, Geocerca.Parametro1.ToString, Geocerca.IdUsuario.ToString)
        '        Next

        '        Session("TableGeocercasT") = table
        '    End If
        'End If

        If (Session("esUsuario")) Then
            Geocercas = BD.spGeocercaConsultar(CInt(Session("IdUsuario"))).ToList
        Else
            Geocercas = BD.spGeocercaSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).ToList
        End If

        table = TryCast(Geocercas, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("gid", GetType(String))
            table.Columns.Add("gpa", GetType(String))
            table.Columns.Add("uid", GetType(String))

            For Each Geocerca In CType(Geocercas, IEnumerable(Of Object))
                GeocercaDet = BD.spGeocercaConsultarCli(Geocerca.IdGeocerca).ToList
                For Each Geodet In CType(GeocercaDet, IEnumerable(Of Object))
                    If (Geodet.Parametro1.Equals("0") And Geodet.Tipo_Geocerca.Equals("Poligonal")) Then
                        radio = ""
                        'ElseIf Geodet.Tipo_Geocerca.Equals("Circular") Then
                        '    indice = Geodet.Parametro1.ToString.IndexOf(".")
                        '    radio = Geodet.Parametro1.ToString.Substring(0, indice)
                    Else
                        radio = Geodet.Parametro1
                    End If
                    table.Rows.Add(Geodet.Nombre.ToString, Geodet.Tipo_Geocerca.ToString, Geodet.IdGeocerca.ToString, radio, Geodet.IdUsuario.ToString)
                Next
            Next

            Session("TableGeocercasT") = table
        End If

        Return table
    End Function

    Protected Sub gridGeocerca_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Try
            Dim grupoId As String = ""
            Dim miLista As New List(Of String)()
            Dim JSONString As String = Nothing
            Dim height As Integer = CInt(hfHeight.Value)
            btnAsignar.ClientVisible = False

            Dim width3 As Integer = CInt(clWidth.Value)
            Dim heigth3 As Integer = CInt(clHeight.Value)

            Dim mobile As Boolean = IsMobileBrowser()
            gridGeocerca.Selection.UnselectAll()

            If (e.Parameters.Equals("1")) Then
                If (Not gridGrupo.FocusedRowIndex < 0) Then
                    grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
                    myGroup = CInt(grupoId)
                    myflagV = False

                    oGeocerca = New Geocerca(Me, Session("PaisLogin"))
                    numGeocercas = oGeocerca.ContarGeocercasGrupos(myGroup)
                    'numSubuser = oGeocerca.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), myGroup)

                    miLista.Add(CStr(numGeocercas))
                    'miLista.Add(CStr(numSubuser))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridGeocerca.JSProperties("cpNumbersJS") = JSONString
                Else
                    myflagV = True
                End If

                myFlagCustom1 = True
                gridGeocerca.DataBind()
            ElseIf (e.Parameters.Equals("geocercas")) Then
                'myflagV = True
                myFlagCustom2 = True
                gridGeocerca.Selection.UnselectAll()
                gridGeocerca.DataBind()

                oGeocerca = New Geocerca(Me, Session("PaisLogin"))

                If (Session("esUsuario")) Then
                    numGeocercas = oGeocerca.ContarGeocercas(CInt(Session("IdUsuario")))
                Else
                    numGeocercas = BD.spGeocercaSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                End If
                'numSubuser = oGeocerca.ContarSubusuarios(CInt(Session("IdUsuario")))

                miLista.Add(CStr(numGeocercas))
                'miLista.Add(CStr(numSubuser))
                JSONString = JsonConvert.SerializeObject(miLista)

                gridGeocerca.JSProperties("cpNumbersJS") = JSONString

            End If

            'gridGeocerca.SettingsPager.PageSize = ((height / 100) * 2) + 2
            If (mobile) Then
                gridGeocerca.SettingsPager.PageSize = 10
            Else
                If (height <= 600) Then
                    gridGeocerca.SettingsPager.PageSize = ((height / 100) * 2) + 2
                ElseIf (height > 600 And height <= 900) Then
                    gridGeocerca.SettingsPager.PageSize = ((height / 100) * 2) + 3
                ElseIf (height > 900) Then
                    gridGeocerca.SettingsPager.PageSize = ((height / 100) * 2)
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridGeocerca_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If (e.DataColumn.Name.Equals("btasig") Or e.DataColumn.Name.Equals("tipo")) Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

    Protected Sub gridGeocerca_HtmlCommandCellPrepared(sender As Object, e As ASPxGridViewTableCommandCellEventArgs)
        If (e.CommandColumn.Name.Equals("chkall")) Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

    Protected Sub asignar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL7") Is Nothing Then
                Session("baseURL7") = "AsignarGrupoGeo.aspx"
            End If

            Dim myId As String = DataBinder.Eval(parent.DataItem, "id").ToString
            Dim myGid As String = DataBinder.Eval(parent.DataItem, "gid").ToString

            Dim contentUrl As String = String.Format("{0}?myGid={1}&myGeo={2}", Session("baseURL7"), myGid, myId.Replace(" ", "_"))

            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ OnMoreInfoClick('{0}'); }}", contentUrl)
            btnAsignar.ClientVisible = False
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

#Region "SubUsuarios"
    'Private Sub LlenarSubUsuarios()
    '    Try
    '        If (myflagS) Then
    '            If (Not IsPostBack) AndAlso (Not IsCallback) Then
    '                Dim id As New GridViewDataColumn()
    '                id.FieldName = "id"
    '                id.Caption = "SubUsuario"
    '                id.VisibleIndex = "1"
    '                id.Width = "210"
    '                id.CellStyle.Font.Size = "10"
    '                id.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                id.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridSubusuario.Columns.Add(id)
    '                Dim data As New GridViewDataColumn()
    '                data.FieldName = "data"
    '                data.Caption = "Nombres"
    '                data.VisibleIndex = "2"
    '                id.CellStyle.Font.Size = "10"
    '                data.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                data.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridSubusuario.Columns.Add(data)
    '                Dim sid As New GridViewDataColumn()
    '                sid.FieldName = "sid"
    '                sid.Caption = "SId"
    '                sid.VisibleIndex = "3"
    '                sid.SetColVisible(False)
    '                sid.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                sid.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridSubusuario.Columns.Add(sid)
    '            End If

    '            If (Not IsPostBack) Then
    '                gridSubusuario.DataBind()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub GridS_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
    '    'Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    'gridSubusuario.DataSource = Nothing
    '    'If (grupos <= 0) Then
    '    '    gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    'Else
    '    '    If (myflagS) Then
    '    '        If (grupos > 0) Then
    '    '            If (Not IsPostBack And Not IsCallback) Then
    '    '                gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '            Else
    '    '                gridSubusuario.DataSource = GetTableGridSubusuarios()
    '    '            End If
    '    '        End If
    '    '    Else
    '    '        gridSubusuario.DataSource = GetTableGridSubusuarios()
    '    '    End If
    '    'End If
    '    Dim grupos As Integer

    '    If (IsNothing(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))) Then
    '        grupos = 0
    '    Else
    '        grupos = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    End If
    '    'Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

    '    If (myFlagCustom1) Then
    '        If (grupos <= 0) Then
    '            gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '        Else
    '            If (grupos > 0) Then
    '                If (gridGrupo.FocusedRowIndex = -1) Then
    '                    gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '                Else
    '                    If (myFlagCustom4) Then
    '                        gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '                    Else
    '                        gridSubusuario.DataSource = GetTableGridSubusuarios()
    '                    End If

    '                End If
    '            End If
    '        End If
    '    ElseIf (myFlagCustom2) Then
    '        gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    ElseIf (myFlagCustom3) Then
    '        If (grupos > 0) Then
    '            If (myFlagCustom4) Then
    '                gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '                gridGrupo.FocusedRowIndex = -1
    '            Else
    '                gridSubusuario.DataSource = GetTableGridSubusuarios()
    '            End If
    '        Else
    '            gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '        End If
    '    Else
    '        If (gridGrupo.FocusedRowIndex = -1) Then
    '            gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '        Else
    '            gridSubusuario.DataSource = GetTableGridSubusuarios()
    '        End If

    '    End If
    'End Sub

    'Private Function GetTableGridSubusuariosT() As DataTable
    '    Dim table As DataTable
    '    Dim Subusuario As New Object()
    '    Dim SubusuarioDet As New Object()

    '    Subusuario = BD.spSubUsuarioConsultar(CInt(Session("IdUsuario"))).ToList

    '    table = TryCast(Subusuario, DataTable)

    '    If table Is Nothing Then
    '        table = New DataTable()
    '        table.Columns.Add("id", GetType(String))
    '        table.Columns.Add("data", GetType(String))
    '        table.Columns.Add("sid", GetType(String))

    '        For Each Subuser In CType(Subusuario, IEnumerable(Of Object))
    '            If (myflagS) Then
    '                table.Rows.Add(Subuser.SubUsuario.ToString, Subuser.NombreCompleto.ToString, Subuser.IdSubUsuario.ToString)
    '            Else
    '                SubusuarioDet = BD.spSubUsuarioDatosConsultar(CInt(Subuser.IdUsuario), CInt(Subuser.IdSubUsuario)).ToList
    '                For Each Subuserdet In CType(SubusuarioDet, IEnumerable(Of Object))
    '                    table.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
    '                Next
    '            End If
    '        Next

    '        Session("TableSubusuariosT") = table
    '    End If
    '    Return table
    'End Function

    'Private Function GetTableGridSubusuarios() As DataTable
    '    Dim table As DataTable
    '    Dim Subusuario As New Object()
    '    Dim SubusuarioDet As New Object()

    '    If (myGroup = 0) Then
    '        myGroup = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    End If

    '    Try
    '        Subusuario = BD.spGrupoGeocercaSubusuarioConsultarV2(myGroup, CInt(Session("IdUsuario")), 0).ToList
    '    Catch ex As Exception
    '        Console.WriteLine(ex.StackTrace)
    '        Console.WriteLine(ex.Message)
    '    End Try

    '    table = TryCast(Subusuario, DataTable)

    '    If table Is Nothing Then
    '        table = New DataTable()
    '        table.Columns.Add("id", GetType(String))
    '        table.Columns.Add("data", GetType(String))
    '        table.Columns.Add("sid", GetType(String))

    '        For Each Subuser In CType(Subusuario, IEnumerable(Of Object))
    '            table.Rows.Add(Subuser.SubUsuario.ToString, Subuser.NombreSubUsuario.ToString, Subuser.IdSubUsuario.ToString)
    '            'SubusuarioDet = BD.spSubUsuarioDatosConsultar(CInt(Subuser.IdUsuario), CInt(Subuser.IdSubUsuario)).ToList
    '            'For Each Subuserdet In CType(SubusuarioDet, IEnumerable(Of Object))
    '            'table.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
    '            'Next
    '        Next

    '        Session("TableSubusuarios") = table
    '    End If
    '    Return table
    'End Function

    'Protected Sub asignarS_Init(sender As Object, e As EventArgs)
    '    Try
    '        Dim btn As ASPxButton = TryCast(sender, ASPxButton)
    '        Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

    '        If Session("baseURL9") Is Nothing Then
    '            Session("baseURL9") = "AsignarGrupoGeoS.aspx"
    '        End If

    '        Dim mySub As String = DataBinder.Eval(parent.DataItem, "id").ToString
    '        Dim myData As String = DataBinder.Eval(parent.DataItem, "data").ToString
    '        Dim mySid As String = DataBinder.Eval(parent.DataItem, "sid").ToString

    '        Dim contentUrl As String = String.Format("{0}?mySub={1}&myData={2}&mySid={3}", Session("baseURL9"), mySub, myData.Replace(" ", "_"), mySid)

    '        btn.ClientSideEvents.Click = String.Format("function(s, e) {{ OnMoreInfoClickS('{0}'); }}", contentUrl)
    '        btnAsignarS.ClientVisible = False
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub gridSubusuario_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
    '    Try
    '        Dim grupoId As String = ""
    '        Dim height As Integer = CInt(hfHeight.Value)
    '        Dim miLista As New List(Of String)()
    '        Dim JSONString As String = Nothing
    '        btnAsignar.ClientVisible = False

    '        'If (pgGrids.ActiveTabPage.Name.Equals("tbSubusuarios")) Then
    '        gridSubusuario.Selection.UnselectAll()

    '        If (e.Parameters.Equals("1")) Then
    '            If (Not gridGrupo.FocusedRowIndex < 0) Then
    '                grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
    '                myGroup = CInt(grupoId)

    '                oGeocerca = New Geocerca(Me, Session("PaisLogin"))
    '                numGeocercas = oGeocerca.ContarGeocercasGrupos(myGroup)
    '                numSubuser = oGeocerca.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), myGroup)

    '                miLista.Add(CStr(numGeocercas))
    '                miLista.Add(CStr(numSubuser))
    '                JSONString = JsonConvert.SerializeObject(miLista)

    '                gridSubusuario.JSProperties("cpNumbers2JS") = JSONString
    '            Else
    '                'myflagV = True
    '            End If
    '            myFlagCustom1 = True
    '            gridSubusuario.DataBind()
    '        ElseIf (e.Parameters.Equals("subusuarios")) Then
    '            oGeocerca = New Geocerca(Me, Session("PaisLogin"))
    '            numGeocercas = oGeocerca.ContarGeocercas(CInt(Session("IdUsuario")))
    '            numSubuser = oGeocerca.ContarSubusuarios(CInt(Session("IdUsuario")))

    '            miLista.Add(CStr(numGeocercas))
    '            miLista.Add(CStr(numSubuser))
    '            JSONString = JsonConvert.SerializeObject(miLista)

    '            gridSubusuario.JSProperties("cpNumbers2JS") = JSONString

    '            myFlagCustom2 = True
    '            gridSubusuario.Selection.UnselectAll()
    '            gridSubusuario.DataBind()
    '        ElseIf (e.Parameters.Equals("2")) Then
    '            oGeocerca = New Geocerca(Me, Session("PaisLogin"))
    '            numGeocercas = oGeocerca.ContarGeocercas(CInt(Session("IdUsuario")))
    '            numSubuser = oGeocerca.ContarSubusuarios(CInt(Session("IdUsuario")))

    '            miLista.Add(CStr(numGeocercas))
    '            miLista.Add(CStr(numSubuser))
    '            JSONString = JsonConvert.SerializeObject(miLista)

    '            gridGeocerca.JSProperties("cpNumbersJS") = JSONString

    '            myFlagCustom3 = True
    '            gridSubusuario.Selection.UnselectAll()
    '            gridSubusuario.DataBind()
    '        End If

    '        gridSubusuario.SettingsPager.PageSize = ((height / 100) * 2) + 2
    '        'End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.StackTrace)
    '        Console.WriteLine(ex.Message)
    '    End Try

    'End Sub
#End Region

    Protected Sub gridGrupo_FocusedRowChanged(sender As Object, e As EventArgs)
        myFlagCustom4 = False
    End Sub

    Protected Sub gridGrupo_BeforePerformDataSelect(sender As Object, e As EventArgs)
        myFlagCustom4 = False
    End Sub

    Protected Sub pmGrupo2_WindowCallback(source As Object, e As PopupWindowCallbackArgs)
        Try
            Dim cadena As String = e.Parameter
            Dim miLista As String() = cadena.Split(",")

            tableAux = New DataTable
            tableAux.Columns.AddRange(New DataColumn() {New DataColumn("id", GetType(String)), New DataColumn("gid", GetType(String))})

            For indice As Integer = 0 To miLista.Count - 1 Step 1
                If (indice Mod 2 = 0) Then
                    tableAux.Rows.Add(miLista(indice), miLista(indice + 1))
                End If
            Next

            Session("TableSelectGeocercas") = tableAux
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub pmGrupo4_WindowCallback(source As Object, e As PopupWindowCallbackArgs)
        Try
            Dim cadena As String = e.Parameter
            Dim miLista As String() = cadena.Split(",")

            tableAux = New DataTable
            tableAux.Columns.AddRange(New DataColumn() {New DataColumn("sid", GetType(String))})
            For Each sid In miLista
                tableAux.Rows.Add(sid)
            Next

            Session("TableSelectSubusuarios") = tableAux
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Protected Sub gridGeocerca_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)
        'e.Handled = True
    End Sub

    Private Sub Update(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary)
        Dim id, nom, par, tip As String

        Try
            For Each itemK In keys.Values
                id = itemK.ToString
            Next itemK

            par = newValues.Values(0)
            nom = newValues.Values(1)
            If (newValues.Values(2).Equals("Poligonal")) Then
                tip = "1"
            ElseIf (newValues.Values(2).Equals("Lineal")) Then
                tip = "2"
            ElseIf (newValues.Values(2).Equals("Circular")) Then
                tip = "3"
            End If

            BD.spGeocercaActualizar(CInt(id), nom.ToUpper, CInt(Session("IdUsuario")), tip, par, Session("Usuario").ToString, "")
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Protected Sub eliminar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL12.1") Is Nothing Then
                Session("baseURL12.1") = "DeleteGeocerca.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "gid")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "id")
            If (grupoData Is Nothing) Then
                grupoData = "."
            Else
                grupoData = grupoData.Replace(" ", "_")
            End If

            Dim message As String = "¿Desea eliminar la geocerca " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL12.1"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage2('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub btnMapas_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL12.2") Is Nothing Then
                Session("baseURL12.2") = "MapaGeocerca.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "gid")
            Dim nombre As String = DataBinder.Eval(parent.DataItem, "id")
            If (nombre Is Nothing) Then
                nombre = "%"
            Else
                nombre = nombre.Replace(" ", "_")
            End If
            Dim data As String = DataBinder.Eval(parent.DataItem, "data")
            Dim gpar As String = DataBinder.Eval(parent.DataItem, "gpa")

            Dim contentUrl As String = String.Format("{0}?myId={1}&nombre={2}&data={3}&gpar={4}", Session("baseURL12.2"), id, nombre, data, gpar)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage3('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridGeocerca_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues)
            CancelEditing(e)
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub Update2(ByVal keys As Integer, ByVal newValues As OrderedDictionary)
        Dim id, nom, par, tip As String

        Try
            'For Each itemK In keys.Values
            '    id = itemK.ToString
            'Next itemK
            id = keys

            par = newValues.Values(0)
            nom = newValues.Values(1)
            If (newValues.Values(2).Equals("Poligonal")) Then
                tip = "1"
            ElseIf (newValues.Values(2).Equals("Lineal")) Then
                tip = "2"
            ElseIf (newValues.Values(2).Equals("Circular")) Then
                tip = "3"
            End If


            BD.spGeocercaActualizar(CInt(id), nom.ToUpper, CInt(Session("IdUsuario")), tip, CStr(Math.Truncate(CDbl(par))), Session("Usuario").ToString, "")
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub CancelEditing(e As CancelEventArgs)
        e.Cancel = True
        gridGeocerca.CancelEdit()
    End Sub

    Protected Sub gridGeocerca_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "data" Then
            If (e.Value IsNot Nothing) Then
                If (e.Value.ToString.Equals("Lineal")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/line.png' title='Lineal' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("Circular")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/circular.png' title='Circular' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("Poligonal")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/poly.png' title='Poligonal' style='width:25px; height:25px'/>"
                End If
            End If
        End If
    End Sub

    Private Function IsMobileBrowser() As Boolean
        Dim opcion As Boolean
        Try
            Dim myBrowserCaps As System.Web.HttpBrowserCapabilities = Request.Browser
            If (CType(myBrowserCaps, System.Web.Configuration.HttpCapabilitiesBase)).IsMobileDevice Then
                opcion = True
            Else
                opcion = False
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            opcion = False
        End Try

        Return opcion
    End Function
End Class
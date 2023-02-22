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
Imports System.Text
Imports System.Configuration.ConfigurationManager
Imports Microsoft.VisualBasic
Imports System.Exception
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Linq
Imports ArtemisAdmin.QueryModifier
Imports System.Windows.Input

Public Class MantenimientoPunto
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
    Private datasetAux As DataSet
    Private Puntos
    Private Subusuarios
    Private numPuntos As Integer = 0
    Private numSubuser As Integer = 0

    Public sPuntos As StringBuilder = New StringBuilder()
    Public dsPunto As New DataSet
    Public oPunto As Punto
    Private objUsuario As Usuario

    Private BD As New OdinDataContext()

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        'If (Session("TablePuntosT") IsNot Nothing) Then
        '    gridPunto.DataSource = TryCast(Session("TablePuntosT"), DataTable)
        '    gridPunto.DataBind()
        'End If
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

            If Request.QueryString("CP") <> "" Then
                Try
                    BD = New OdinDataContext(Globales.getCadenaConexionPais(Request.QueryString("CP").ToUpper, False))
                    Session("PaisLogin") = Request.QueryString("CP").ToUpper.ToString
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End If

            'BD = New OdinDataContext(Globales.getCadenaConexionPais("EC", False))
            'Session("PaisLogin") = "EC"

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

            objUsuario = New Usuario(Me, Session("PaisLogin"))
            If Not IsPostBack Then
                If (objUsuario.ValidarUsuario2(Session("Usuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave"))) Then
                    Session("esUsuario") = True
                    hfCadena.Value = "1"
                ElseIf objUsuario.ValidarSubUsuario2(Session("SubUsuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
                    Session("Usuario") = objUsuario.ConsultarUsuarioNombre(CInt(Session("IdUsuario")))
                    Session("esUsuario") = False
                    hfCadena.Value = "0"
                End If
            End If

            If Session("baseURL16") Is Nothing Then
                Session("baseURL16") = "AsignarGrupoPto.aspx"
            End If
            If Session("baseURL17") Is Nothing Then
                Session("baseURL17") = "AsignarGrupoPto2.aspx"
            End If
            If Session("baseURL18") Is Nothing Then
                Session("baseURL18") = "AsignarGrupoPtoS.aspx"
            End If
            If Session("baseURL19") Is Nothing Then
                Session("baseURL19") = "AsignarGrupoPtoS2.aspx"
            End If
            If Session("baseURL20") Is Nothing Then
                Session("baseURL20") = "FormGrupoPto.aspx"
            End If
            If Session("baseURL21") Is Nothing Then
                Session("baseURL21") = "DeleteGrupoPto.aspx"
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            gridGrupo.JSProperties("cpValidar") = "0"

            LlenarPuntos()
            'If (Session("esUsuario")) Then
            '    LlenarSubUsuarios()
            'End If

            If Not IsPostBack Then
                LlenarGrupos()
                'LlenarSubUsuarios()

                oPunto = New Punto(Me, Session("PaisLogin"))
                If (Session("esUsuario")) Then
                    numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                Else
                    numPuntos = BD.spPuntoSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                End If
                'numSubuser = oPunto.ContarSubusuarios(CInt(Session("IdUsuario")))

                hpuntos.Value = numPuntos
                'hsubuser.Value = numSubuser

                'btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Puntos</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numPuntos) + "</span></div> </div>"
                'btnTodos2.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Subusuarios</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numSubuser) + "</span></div> </div>"
                lbCount.Text = "<div style='padding-top: 2px'> <span class='badge badge-dark' style='float: right; font-size: 12px;'>" + CStr(numPuntos) + "</span></div>"
            Else
                oPunto = New Punto(Me, Session("PaisLogin"))
                If (Session("esUsuario")) Then
                    numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                Else
                    numPuntos = BD.spPuntoSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                End If
                'numSubuser = oPunto.ContarSubusuarios(CInt(Session("IdUsuario")))

                hpuntos.Value = numPuntos
                'hsubuser.Value = numSubuser
            End If
        End Try
    End Sub

#Region "Grupos"
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
                data.Width = "40"
                data.CellStyle.Font.Size = "9"
                data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                data.CellStyle.Border.BorderStyle = BorderStyle.None
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
        Grupos = BD.spGrupoPuntoConsultar(CInt(Session("IdUsuario")), 0).ToList

        Dim table As DataTable = TryCast(Grupos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("grupo", GetType(String))
            table.Columns.Add("descripcion", GetType(String))
            table.Columns.Add("edt", GetType(Boolean))
            table.Columns.Add("del", GetType(Boolean))

            For Each grp In CType(Grupos, IEnumerable(Of Object))
                table.Rows.Add(grp.IdGrupoPunto.ToString, grp.Grupo.ToString.ToUpper, grp.Descripcion.ToString.ToUpper, True, True)
            Next

            Session("TableGrupos") = table
        End If

        Return table
    End Function

    Protected Sub gridGrupo_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        Try
            If (Not IsCallback) Then
                If (e.DataColumn.FieldName.Equals("grupo")) Then
                    If (Not e.CellValue.Equals("")) Then
                        e.Cell.ToolTip = e.CellValue.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Protected Sub gridGrupo_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        Try
            If (Not IsCallback) Then
                If e.Column.FieldName = "grupo" Then
                    If (e.Value.ToString.Length > 12) Then
                        e.DisplayText = (e.Value.ToString.Substring(0, 12).ToUpper) + "..."
                    Else
                        e.DisplayText = (e.Value.ToString).ToUpper
                    End If
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

    Protected Sub gridGrupo_PreRender(sender As Object, e As EventArgs)
        If (Not IsPostBack) Then
            gridGrupo.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub btnGrupoN_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            If Session("baseURL20") Is Nothing Then
                Session("baseURL20") = "FormGrupoPto.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL20"), "0", "0", "0", "N", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "N")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub editar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL20") Is Nothing Then
                Session("baseURL20") = "FormGrupoPto.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grp As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim des As String = DataBinder.Eval(parent.DataItem, "descripcion")

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL20"), id, grp, des, "E", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "E")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub borrar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL21") Is Nothing Then
                Session("baseURL21") = "DeleteGrupoPto.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim message As String = "¿Desea eliminar el grupo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL21"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowMessage('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridGrupo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Try
            Dim datos As String
            Dim miLista As String()
            Dim grupos As Integer
            Dim Puntos As New Object()
            Dim PuntosDet As New Object()
            Dim Subusuarios As New Object()
            Dim table As DataTable
            Dim tableS As DataTable
            Dim JSONString As String = Nothing
            Dim JSONStringS As String = Nothing
            Dim miLista2 As New List(Of String)()
            Dim JSONString2 As String = Nothing

            oPunto = New Punto(Me, Session("PaisLogin"))

            If (e.Parameters.Equals("1")) Then
                gridGrupo.DataBind()
            ElseIf (e.Parameters.Equals("2")) Then
                If (gridGrupo.FocusedRowIndex = -1) Then
                    'If (Session("TablePuntosT") IsNot Nothing) Then
                    'Puntos = BD.spPuntoConsultar(0, CInt(Session("IdUsuario"))).ToList
                    'datasetAux = oPunto.Consultar2(CInt(Session("IdUsuario")))
                    If (Session("esUsuario")) Then
                        datasetAux = oPunto.Consultar2(CInt(Session("IdUsuario")))
                    Else
                        datasetAux = oPunto.ConsultarPtoSub(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))
                    End If

                    table = TryCast(Puntos, DataTable)
                    If table Is Nothing Then
                        table = New DataTable()
                        table.Columns.Add("id", GetType(String))
                        table.Columns.Add("data", GetType(String))
                        table.Columns.Add("pid", GetType(String))
                        table.Columns.Add("puid", GetType(String))
                        table.Columns.Add("lat", GetType(String))
                        table.Columns.Add("lon", GetType(String))
                        table.Columns.Add("uid", GetType(String))

                        For Each fila As DataRow In datasetAux.Tables(0).Rows()
                            table.Rows.Add(fila(1).ToString.Replace(",", ":"), fila(2).ToString, fila(0).ToString, fila(5).ToString, fila(3).ToString, fila(4).ToString, CStr(Session("IdUsuario")))
                        Next

                        JSONString = JsonConvert.SerializeObject(table)
                    End If
                    gridGrupo.JSProperties("cpJSONgroupT") = JSONString
                    'End If

                    'Dim SubusuarioDet As New Object()

                    'Subusuarios = BD.spSubUsuarioConsultar(CInt(Session("IdUsuario"))).ToList
                    'tableS = TryCast(Subusuarios, DataTable)
                    'If tableS Is Nothing Then
                    '    tableS = New DataTable()
                    '    tableS.Columns.Add("id", GetType(String))
                    '    tableS.Columns.Add("data", GetType(String))
                    '    tableS.Columns.Add("sid", GetType(String))

                    '    For Each Subuser In CType(Subusuarios, IEnumerable(Of Object))
                    '        SubusuarioDet = BD.spSubUsuarioDatosConsultar(CInt(Subuser.IdUsuario), CInt(Subuser.IdSubUsuario)).ToList
                    '        For Each Subuserdet In CType(SubusuarioDet, IEnumerable(Of Object))
                    '            tableS.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
                    '        Next
                    '    Next
                    '    JSONStringS = JsonConvert.SerializeObject(tableS)
                    'End If
                    'gridGrupo.JSProperties("cpJSONgroupST") = JSONStringS

                    'numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                    If (Session("esUsuario")) Then
                        numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                    Else
                        numPuntos = BD.spPuntoSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                    End If
                    'numSubuser = oPunto.ContarSubusuarios(CInt(Session("IdUsuario")))

                    miLista2.Add(CStr(numPuntos))
                    'miLista2.Add(CStr(numSubuser))
                    JSONString2 = JsonConvert.SerializeObject(miLista2)

                    gridGrupo.JSProperties("cpNumbersJS") = JSONString2
                End If
            ElseIf (e.Parameters.Equals("update")) Then
                If (IsCallback) Then
                    grupos = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

                    Puntos = BD.spPuntoGrupoConsultar(grupos, 0, CInt(Session("IdUsuario"))).ToList

                    If (Puntos.Count > 0) Then
                        table = TryCast(Puntos, DataTable)
                        If table Is Nothing Then
                            table = New DataTable()
                            table.Columns.Add("id", GetType(String))
                            table.Columns.Add("data", GetType(String))
                            table.Columns.Add("pid", GetType(String))
                            table.Columns.Add("puid", GetType(String))
                            table.Columns.Add("lat", GetType(String))
                            table.Columns.Add("lon", GetType(String))
                            table.Columns.Add("uid", GetType(String))

                            For Each Punto In CType(Puntos, IEnumerable(Of Object))
                                PuntosDet = BD.spPuntoConsultar(CInt(Punto.IdPunto), CInt(Session("IdUsuario"))).ToList
                                For Each Ptodet In CType(PuntosDet, IEnumerable(Of Object))
                                    table.Rows.Add(Punto.NombrePunto.ToString.Replace(",", ":"), Punto.DescripcionPunto.ToString, Punto.IdPunto.ToString, Ptodet.IdPuntoCliente.ToString,
                                                   Ptodet.Latitud.ToString, Ptodet.Longitud.ToString, CStr(Session("IdUsuario")))
                                Next
                            Next

                            JSONString = JsonConvert.SerializeObject(table)
                        End If
                    End If

                    'Subusuarios = BD.spGrupoPuntoSubusuarioConsultar(grupos, CInt(Session("IdUsuario")), 0).ToList
                    'If (Subusuarios.Count > 0) Then
                    '    tableS = TryCast(Subusuarios, DataTable)
                    '    If tableS Is Nothing Then
                    '        tableS = New DataTable()
                    '        tableS.Columns.Add("id", GetType(String))
                    '        tableS.Columns.Add("data", GetType(String))
                    '        tableS.Columns.Add("sid", GetType(String))

                    '        For Each Subuser In CType(Subusuarios, IEnumerable(Of Object))
                    '            tableS.Rows.Add(Subuser.SubUsuario.ToString, Subuser.NombreSubUsuario.ToString, Subuser.IdSubUsuario.ToString)
                    '        Next

                    '        JSONStringS = JsonConvert.SerializeObject(tableS)
                    '    End If
                    'End If

                    gridGrupo.JSProperties("cpJSONgroup") = JSONString
                    'gridGrupo.JSProperties("cpJSONgroupS") = JSONStringS

                    'numPuntos = oPunto.ContarPuntosGrupos(grupos, CInt(Session("IdUsuario")))
                    If (Session("esUsuario")) Then
                        numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                    Else
                        numPuntos = BD.spPuntoSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                    End If
                    'numSubuser = oPunto.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), grupos)

                    miLista2.Add(CStr(numPuntos))
                    'miLista2.Add(CStr(numSubuser))
                    JSONString2 = JsonConvert.SerializeObject(miLista2)

                    gridGrupo.JSProperties("cpNumbersJS") = JSONString2
                End If
            ElseIf (e.Parameters.Equals("actualizar")) Then
                datos = hdregs.Value
                miLista = datos.Split(",")

                If (miLista(4).Equals("1")) Then
                    If (Not oPunto.ValidarCodigoCli(CInt(Session("IdUsuario")), miLista(1))) Then
                        oPunto.UpdateDatosPrincipales(CInt(miLista(0)), miLista(1), miLista(2).ToUpper, miLista(3).ToUpper, Session("Usuario"))
                    Else
                        gridGrupo.JSProperties("cpValidar") = "1"
                    End If
                Else
                    oPunto.UpdateDatosPrincipales(CInt(miLista(0)), miLista(1), miLista(2).ToUpper, miLista(3).ToUpper, Session("Usuario"))
                End If

                If (gridGrupo.FocusedRowIndex = -1) Then
                    If (Session("esUsuario")) Then
                        datasetAux = oPunto.Consultar2(CInt(Session("IdUsuario")))
                    Else
                        datasetAux = oPunto.ConsultarPtoSub(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))
                    End If

                    table = TryCast(Puntos, DataTable)
                    If table Is Nothing Then
                        table = New DataTable()
                        table.Columns.Add("id", GetType(String))
                        table.Columns.Add("data", GetType(String))
                        table.Columns.Add("pid", GetType(String))
                        table.Columns.Add("puid", GetType(String))
                        table.Columns.Add("lat", GetType(String))
                        table.Columns.Add("lon", GetType(String))
                        table.Columns.Add("uid", GetType(String))

                        For Each fila As DataRow In datasetAux.Tables(0).Rows()
                            table.Rows.Add(fila(1).ToString.Replace(",", ":"), fila(2).ToString, fila(0).ToString, fila(5).ToString, fila(3).ToString, fila(4).ToString, CStr(Session("IdUsuario")))
                        Next

                        JSONString = JsonConvert.SerializeObject(table)
                    End If
                    gridGrupo.JSProperties("cpJSONgroupT") = JSONString

                    'numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                    If (Session("esUsuario")) Then
                        numPuntos = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
                    Else
                        numPuntos = BD.spPuntoSubUsuarioConsultar(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario"))).Count
                    End If
                    'numSubuser = oPunto.ContarSubusuarios(CInt(Session("IdUsuario")))

                    miLista2.Add(CStr(numPuntos))
                    'miLista2.Add(CStr(numSubuser))
                    JSONString2 = JsonConvert.SerializeObject(miLista2)

                    gridGrupo.JSProperties("cpNumbersJS") = JSONString2
                Else
                    grupos = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

                    Puntos = BD.spPuntoGrupoConsultar(grupos, 0, CInt(Session("IdUsuario"))).ToList
                    If (Puntos.Count > 0) Then
                        table = TryCast(Puntos, DataTable)
                        If table Is Nothing Then
                            table = New DataTable()
                            table.Columns.Add("id", GetType(String))
                            table.Columns.Add("data", GetType(String))
                            table.Columns.Add("pid", GetType(String))
                            table.Columns.Add("puid", GetType(String))
                            table.Columns.Add("lat", GetType(String))
                            table.Columns.Add("lon", GetType(String))
                            table.Columns.Add("uid", GetType(String))

                            For Each Punto In CType(Puntos, IEnumerable(Of Object))
                                PuntosDet = BD.spPuntoConsultar(CInt(Punto.IdPunto), CInt(Session("IdUsuario"))).ToList
                                For Each Ptodet In CType(PuntosDet, IEnumerable(Of Object))
                                    table.Rows.Add(Punto.NombrePunto.ToString.Replace(",", ":"), Punto.DescripcionPunto.ToString, Punto.IdPunto.ToString,
                                                   Ptodet.IdPuntoCliente.ToString, Ptodet.Latitud.ToString, Ptodet.Longitud.ToString, CStr(Session("IdUsuario")))
                                Next
                            Next

                            JSONString = JsonConvert.SerializeObject(table)
                        End If
                        gridGrupo.JSProperties("cpJSONgroup") = JSONString
                    End If
                End If

                numPuntos = oPunto.ContarPuntosGrupos(grupos, CInt(Session("IdUsuario")))
                'numSubuser = oPunto.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), grupos)

                miLista2.Add(CStr(numPuntos))
                'miLista2.Add(CStr(numSubuser))
                JSONString2 = JsonConvert.SerializeObject(miLista2)

                gridGrupo.JSProperties("cpNumbersJS") = JSONString2

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub

#End Region

#Region "Puntos"
    Private Sub LlenarPuntos()
        Try
            'If (myflagV) Then
            '    If (Not IsPostBack) AndAlso (Not IsCallback) Then
            '        Dim id As New GridViewDataColumn()
            '        id.FieldName = "id"
            '        id.Caption = "Puntos"
            '        id.VisibleIndex = "2"
            '        id.Width = "250"
            '        id.CellStyle.Font.Size = "10"
            '        id.HeaderStyle.Border.BorderStyle = BorderStyle.None
            '        id.CellStyle.Border.BorderStyle = BorderStyle.None
            '        gridPunto.Columns.Add(id)
            '        Dim data As New GridViewDataColumn()
            '        data.FieldName = "data"
            '        data.Caption = "Descripcion"
            '        data.VisibleIndex = "3"
            '        data.CellStyle.Font.Size = "10"
            '        data.HeaderStyle.Border.BorderStyle = BorderStyle.None
            '        data.CellStyle.Border.BorderStyle = BorderStyle.None
            '        gridPunto.Columns.Add(data)
            '        Dim pid As New GridViewDataColumn()
            '        pid.FieldName = "pid"
            '        pid.Caption = "PId"
            '        pid.VisibleIndex = "4"
            '        pid.SetColVisible(False)
            '        pid.HeaderStyle.Border.BorderStyle = BorderStyle.None
            '        pid.CellStyle.Border.BorderStyle = BorderStyle.None
            '        gridPunto.Columns.Add(pid)
            '    End If

            '    If (Not IsPostBack) Then
            '        gridPunto.DataBind()
            '    Else
            '        gridPunto.DataBind()
            '    End If
            'End If

            oPunto = New Punto(Me, Session("PaisLogin"))

            'Dim datos As String
            'Dim miLista As String()
            Dim Puntos As New Object()
            Dim table As DataTable
            Dim JSONString As String = Nothing

            'Puntos = BD.spPuntoConsultar(0, CInt(Session("IdUsuario"))).ToList
            If (Session("esUsuario")) Then
                datasetAux = oPunto.Consultar2(CInt(Session("IdUsuario")))
            Else
                datasetAux = oPunto.ConsultarPtoSub(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))
            End If


            table = TryCast(Puntos, DataTable)
            If table Is Nothing Then
                table = New DataTable()
                table.Columns.Add("id", GetType(String))
                table.Columns.Add("data", GetType(String))
                table.Columns.Add("pid", GetType(String))
                table.Columns.Add("puid", GetType(String))
                table.Columns.Add("lat", GetType(String))
                table.Columns.Add("lon", GetType(String))
                table.Columns.Add("uid", GetType(String))

                'For Each Punto In CType(Puntos, IEnumerable(Of Object))
                '    datos = Punto.Dato
                '    miLista = datos.Split("_")
                '    table.Rows.Add(Punto.Nombre.ToString, miLista(3), Punto.IdPunto.ToString, CStr(Session("IdUsuario")))
                'Next

                For Each fila As DataRow In datasetAux.Tables(0).Rows()
                    table.Rows.Add(fila(1).ToString.Replace(",", ":"), fila(2).ToString, fila(0).ToString, fila(5).ToString, fila(3).ToString, fila(4).ToString, CStr(Session("IdUsuario")))
                Next

                JSONString = JsonConvert.SerializeObject(table)
            End If

            gridGrupo.JSProperties("cpJSONgroupT") = JSONString
            Session("TablePuntosT") = table
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Private Sub LlenarPuntos2()
    '    Try
    '        If (myflagV) Then
    '            If (Not IsPostBack) AndAlso (Not IsCallback) Then
    '                Dim id As New GridViewDataColumn()
    '                id.FieldName = "Nombre"
    '                id.Caption = "Puntos"
    '                id.VisibleIndex = "2"
    '                id.Width = "250"
    '                id.CellStyle.Font.Size = "10"
    '                id.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                id.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridPunto.Columns.Add(id)
    '                Dim data As New GridViewDataColumn()
    '                data.FieldName = "Descripcion"
    '                data.Caption = "Descripcion"
    '                data.VisibleIndex = "3"
    '                data.CellStyle.Font.Size = "10"
    '                data.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                data.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridPunto.Columns.Add(data)
    '                Dim pid As New GridViewDataColumn()
    '                pid.FieldName = "IdPunto"
    '                pid.Caption = "PId"
    '                pid.VisibleIndex = "4"
    '                pid.SetColVisible(False)
    '                pid.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '                pid.CellStyle.Border.BorderStyle = BorderStyle.None
    '                gridPunto.Columns.Add(pid)
    '            End If

    '            '    If (Not IsPostBack) Then
    '            '        gridPunto.DataBind()
    '            '    Else
    '            '        gridPunto.DataBind()
    '            '    End If
    '        End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    Protected Sub GridP_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

        'If (myFlagCustom1) Then
        '    If (grupos <= 0) Then
        '        gridPunto.DataSource = GetTableGridPuntosT()
        '    Else
        '        If (grupos > 0) Then
        '            If (gridGrupo.FocusedRowIndex = -1) Then
        '                gridPunto.DataSource = GetTableGridPuntosT()
        '            Else
        '                If (myflagV) Then
        '                    gridPunto.DataSource = GetTableGridPuntosT()
        '                Else
        '                    If (myFlagCustom4) Then
        '                        gridPunto.DataSource = GetTableGridPuntosT()
        '                    Else
        '                        gridPunto.DataSource = GetTableGridPuntos()
        '                    End If

        '                End If

        '            End If
        '        End If
        '    End If
        'ElseIf (myFlagCustom2) Then
        '    gridPunto.DataSource = GetTableGridPuntosT()
        'ElseIf (myFlagCustom3) Then
        '    If (grupos > 0) Then
        '        If (myFlagCustom4) Then
        '            gridPunto.DataSource = GetTableGridPuntosT()
        '            gridGrupo.FocusedRowIndex = -1
        '        Else
        '            gridPunto.DataSource = GetTableGridPuntos()
        '        End If
        '    Else
        '        gridPunto.DataSource = GetTableGridPuntosT()
        '    End If
        'Else
        '    If (gridGrupo.FocusedRowIndex = -1) Then
        '        gridPunto.DataSource = GetTableGridPuntosT()
        '    Else
        '        gridPunto.DataSource = GetTableGridPuntos()
        '    End If

        'End If

    End Sub

    Private Function GetTableGridPuntos() As DataTable
        Dim table As DataTable
        Dim Puntos As New Object()
        Dim PuntosDet As New Object()

        If (myGroup = 0) Then
            myGroup = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
        End If

        Puntos = BD.spPuntoGrupoConsultar(myGroup, 0, CInt(Session("IdUsuario"))).ToList
        table = TryCast(Puntos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("pid", GetType(String))
            table.Columns.Add("uid", GetType(String))

            For Each Punto In CType(Puntos, IEnumerable(Of Object))
                table.Rows.Add(Punto.NombrePunto.ToString, Punto.DescripcionPunto.ToString, Punto.IdPunto.ToString, CStr(Session("IdUsuario")))
            Next

            Session("TablePuntos") = table
        End If

        'If (Session("TablePuntos") IsNot Nothing) Then
        '    table = TryCast(Session("TablePuntos"), DataTable)
        'Else
        '    Puntos = BD.spPuntoGrupoConsultar(myGroup, 0, CInt(Session("IdUsuario"))).ToList

        '    table = TryCast(Puntos, DataTable)

        '    If table Is Nothing Then
        '        table = New DataTable()
        '        table.Columns.Add("id", GetType(String))
        '        table.Columns.Add("data", GetType(String))
        '        table.Columns.Add("pid", GetType(String))
        '        table.Columns.Add("uid", GetType(String))

        '        For Each Punto In CType(Puntos, IEnumerable(Of Object))
        '            table.Rows.Add(Punto.NombrePunto.ToString, Punto.DescripcionPunto.ToString, Punto.IdPunto.ToString, CStr(Session("IdUsuario")))
        '        Next

        '        Session("TablePuntos") = table
        '    End If
        'End If

        Return table
    End Function

    Private Function GetTableGridPuntosT() As DataTable
        Dim table As DataTable
        Dim Puntos As New Object()

        If (Session("TablePuntosT") IsNot Nothing) Then
            table = TryCast(Session("TablePuntosT"), DataTable)
        Else
            Puntos = BD.spPuntoConsultar(0, CInt(Session("IdUsuario"))).ToList
            table = TryCast(Puntos, DataTable)

            If table Is Nothing Then
                table = New DataTable()
                table.Columns.Add("id", GetType(String))
                table.Columns.Add("data", GetType(String))
                table.Columns.Add("pid", GetType(String))
                table.Columns.Add("uid", GetType(String))

                For Each Punto In CType(Puntos, IEnumerable(Of Object))
                    table.Rows.Add(Punto.Nombre.ToString, "", Punto.IdPunto.ToString, CStr(Session("IdUsuario")))
                Next

                Session("TablePuntosT") = table
            End If
        End If

        Return table
    End Function

    Protected Sub gridPunto_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Try
            Dim grupoId As String = ""
            Dim height As Integer = CInt(hfHeight.Value)
            btnAsignar.ClientVisible = False

            'If (pgGrids.ActiveTabPage.Name.Equals("tbPuntos")) Then
            '    gridPunto.Selection.UnselectAll()

            '    If (e.Parameters.Equals("1")) Then
            '        If (Not gridGrupo.FocusedRowIndex < 0) Then
            '            grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
            '            myGroup = CInt(grupoId)
            '            myflagV = False
            '            'If (gridGrupo.FocusedRowIndex = 0 And myGroup > 0) Then
            '            '    myFlagCustom = True
            '            'End If
            '            'gridVehiculo.DataBind()
            '        Else
            '            myflagV = True
            '            'gridVehiculo.Selection.UnselectAll()
            '            'gridVehiculo.DataBind()
            '        End If

            '        myFlagCustom1 = True
            '        gridPunto.DataBind()
            '    ElseIf (e.Parameters.Equals("puntos")) Then
            '        'myflagV = True
            '        myFlagCustom2 = True
            '        gridPunto.Selection.UnselectAll()
            '        gridPunto.DataBind()
            '    ElseIf (e.Parameters.Equals("2")) Then
            '        'myflagV = True
            '        myFlagCustom3 = True
            '        gridPunto.Selection.UnselectAll()
            '        gridPunto.DataBind()
            '    ElseIf (e.Parameters.Equals("cfpuntos")) Then
            '        myFlagCustom4 = True
            '    ElseIf (e.Parameters.Equals("sfpuntos")) Then
            '        myFlagCustom4 = False
            '        gridPunto.Selection.UnselectAll()
            '        gridPunto.DataBind()
            '    End If

            '    'gridPunto.SettingsPager.PageSize = ((height / 100) * 2) + 2
            '    If (height <= 600) Then
            '        gridPunto.SettingsPager.PageSize = ((height / 100) * 2) + 2
            '    ElseIf (height > 600 And height <= 900) Then
            '        gridPunto.SettingsPager.PageSize = ((height / 100) * 2) + 3
            '    ElseIf (height > 900) Then
            '        gridPunto.SettingsPager.PageSize = ((height / 100) * 2)
            '    End If
            'End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub asignar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL16") Is Nothing Then
                Session("baseURL16") = "AsignarGrupoPto.aspx"
            End If

            Dim myId As String = DataBinder.Eval(parent.DataItem, "id").ToString
            Dim myPid As String = DataBinder.Eval(parent.DataItem, "pid").ToString

            Dim contentUrl As String = String.Format("{0}?myPid={1}&myPto={2}", Session("baseURL16"), myPid, myId.Replace(" ", "_"))

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
    '        'If (myflagS) Then
    '        '    If (Not IsPostBack) AndAlso (Not IsCallback) Then
    '        '        Dim id As New GridViewDataColumn()
    '        '        id.FieldName = "id"
    '        '        id.Caption = "SubUsuario"
    '        '        id.VisibleIndex = "1"
    '        '        id.Width = "250"
    '        '        id.CellStyle.Font.Size = "10"
    '        '        id.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '        '        id.CellStyle.Border.BorderStyle = BorderStyle.None
    '        '        gridSubusuario.Columns.Add(id)
    '        '        Dim data As New GridViewDataColumn()
    '        '        data.FieldName = "data"
    '        '        data.Caption = "Nombres"
    '        '        data.VisibleIndex = "2"
    '        '        id.CellStyle.Font.Size = "10"
    '        '        data.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '        '        data.CellStyle.Border.BorderStyle = BorderStyle.None
    '        '        gridSubusuario.Columns.Add(data)
    '        '        Dim sid As New GridViewDataColumn()
    '        '        sid.FieldName = "sid"
    '        '        sid.Caption = "SId"
    '        '        sid.VisibleIndex = "3"
    '        '        sid.SetColVisible(False)
    '        '        sid.HeaderStyle.Border.BorderStyle = BorderStyle.None
    '        '        sid.CellStyle.Border.BorderStyle = BorderStyle.None
    '        '        gridSubusuario.Columns.Add(sid)
    '        '    End If

    '        '    If (Not IsPostBack) Then
    '        '        gridSubusuario.DataBind()
    '        '    End If
    '        'End If

    '        Dim Subusuarios As New Object()
    '        Dim SubusuarioDet As New Object()
    '        Dim table As DataTable
    '        Dim JSONString As String = Nothing

    '        Subusuarios = BD.spSubUsuarioConsultar(CInt(Session("IdUsuario"))).ToList
    '        table = TryCast(Subusuarios, DataTable)
    '        If table Is Nothing Then
    '            table = New DataTable()
    '            table.Columns.Add("id", GetType(String))
    '            table.Columns.Add("data", GetType(String))
    '            table.Columns.Add("sid", GetType(String))

    '            For Each Subuser In CType(Subusuarios, IEnumerable(Of Object))
    '                SubusuarioDet = BD.spSubUsuarioDatosConsultar(CInt(Subuser.IdUsuario), CInt(Subuser.IdSubUsuario)).ToList
    '                For Each Subuserdet In CType(SubusuarioDet, IEnumerable(Of Object))
    '                    table.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
    '                Next
    '            Next
    '            JSONString = JsonConvert.SerializeObject(table)
    '        End If

    '        gridGrupo.JSProperties("cpJSONgroupST") = JSONString
    '        Session("TableSubusuariosT") = table
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub GridS_DataBinding(ByVal sender As Object, ByVal e As EventArgs)

    '    'Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    Dim grupos As Integer
    '    If (IsNothing(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))) Then
    '        grupos = 0
    '    Else
    '        grupos = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    End If

    '    'If (myFlagCustom1) Then
    '    '    If (grupos <= 0) Then
    '    '        gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '    Else
    '    '        If (grupos > 0) Then
    '    '            If (gridGrupo.FocusedRowIndex = -1) Then
    '    '                gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '            Else
    '    '                If (myFlagCustom4) Then
    '    '                    gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '                Else
    '    '                    gridSubusuario.DataSource = GetTableGridSubusuarios()
    '    '                End If

    '    '            End If
    '    '        End If
    '    '    End If
    '    'ElseIf (myFlagCustom2) Then
    '    '    gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    'ElseIf (myFlagCustom3) Then
    '    '    If (grupos > 0) Then
    '    '        If (myFlagCustom4) Then
    '    '            gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '            gridGrupo.FocusedRowIndex = -1
    '    '        Else
    '    '            gridSubusuario.DataSource = GetTableGridSubusuarios()
    '    '        End If
    '    '    Else
    '    '        gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '    End If
    '    'Else
    '    '    If (gridGrupo.FocusedRowIndex = -1) Then
    '    '        gridSubusuario.DataSource = GetTableGridSubusuariosT()
    '    '    Else
    '    '        gridSubusuario.DataSource = GetTableGridSubusuarios()
    '    '    End If

    '    'End If
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
    '        Subusuario = BD.spGrupoPuntoSubusuarioConsultar(myGroup, CInt(Session("IdUsuario")), 0).ToList
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
    '            '    table.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
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

    '        If Session("baseURL18") Is Nothing Then
    '            Session("baseURL18") = "AsignarGrupoPtoS.aspx"
    '        End If

    '        Dim mySub As String = DataBinder.Eval(parent.DataItem, "id").ToString
    '        Dim myData As String = DataBinder.Eval(parent.DataItem, "data").ToString
    '        Dim mySid As String = DataBinder.Eval(parent.DataItem, "sid").ToString

    '        Dim contentUrl As String = String.Format("{0}?mySub={1}&myData={2}&mySid={3}", Session("baseURL18"), mySub, myData, mySid)

    '        btn.ClientSideEvents.Click = String.Format("function(s, e) {{ OnMoreInfoClickS('{0}'); }}", contentUrl)
    '        'btnAsignarS.ClientVisible = False
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub gridSubusuario_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)

    '    Try
    '        Dim grupoId As String = ""
    '        Dim height As Integer = CInt(hfHeight.Value)
    '        btnAsignar.ClientVisible = False

    '        'If (pgGrids.ActiveTabPage.Name.Equals("tbSubusuarios")) Then
    '        '    gridSubusuario.Selection.UnselectAll()

    '        '    If (e.Parameters.Equals("1")) Then
    '        '        myFlagCustom1 = True
    '        '        gridSubusuario.DataBind()
    '        '    ElseIf (e.Parameters.Equals("subusuarios")) Then
    '        '        myFlagCustom2 = True
    '        '        gridSubusuario.Selection.UnselectAll()
    '        '        gridSubusuario.DataBind()
    '        '    ElseIf (e.Parameters.Equals("2")) Then
    '        '        myFlagCustom3 = True
    '        '        gridSubusuario.Selection.UnselectAll()
    '        '        gridSubusuario.DataBind()
    '        '    End If

    '        '    gridSubusuario.SettingsPager.PageSize = ((height / 100) * 2) + 2
    '        'End If

    '    Catch ex As Exception
    '        Console.WriteLine(ex.StackTrace)
    '        Console.WriteLine(ex.Message)
    '    End Try

    'End Sub

#End Region

    Protected Sub pgGrids_Init(sender As Object, e As EventArgs)
        'Puntos = BD.spPuntoConsultar(0, CInt(Session("IdUsuario"))).ToList
        'Subusuarios = BD.spSubUsuarioConsultar(CInt(Session("IdUsuario"))).ToList

        'oPunto = New Punto(Me)
        'Dim pto As Integer = oPunto.ContarPuntos(CInt(Session("IdUsuario")))
        'Dim sbu As Integer = oPunto.ContarSubusuarios(CInt(Session("IdUsuario")))

        'btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Puntos</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(pto) + "</span></div> </div>"
        'btnTodos2.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Subusuarios</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(sbu) + "</span></div> </div>"
    End Sub

    Protected Sub pmGrupo2_WindowCallback(source As Object, e As PopupWindowCallbackArgs)
        Try
            Dim cadena As String = e.Parameter
            Dim miLista As String() = cadena.Split(",")

            tableAux = New DataTable
            tableAux.Columns.AddRange(New DataColumn() {New DataColumn("vid", GetType(String)), New DataColumn("idac", GetType(String))})

            For indice As Integer = 0 To miLista.Count - 1 Step 1
                If (indice Mod 2 = 0) Then
                    tableAux.Rows.Add(miLista(indice), miLista(indice + 1))
                End If
            Next

            Session("TableSelectPuntos") = tableAux
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

    'Protected Sub EntityServerModeDataSource_Selecting(sender As Object, e As DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs)
    '    Try
    '        Dim db As New PX_DBEntities
    '        e.KeyExpression = "IdPunto"
    '        Dim query = From em In New QueryUpdater(Of Puntos_Referencia)(New PX_DBEntities().Puntos_Referencia).Where(Function(p) p.IdUsuario = CInt(Session("IdUsuario")))
    '                    Select em.IdPunto, em.Nombre, em.Descripcion
    '        e.QueryableSource = query
    '        'e.QueryableSource = From row In db.Puntos_Referencia Where
    '        '                    row.IdUsuario = CInt(Session("IdUsuario"))
    '        '                    Select row.IdPunto, row.Nombre, row.Descripcion
    '    Catch ex As Exception
    '        Console.WriteLine(ex.StackTrace)
    '    End Try
    'End Sub

    'Protected Sub EntityServerModeDataSource_ExceptionThrown(sender As Object, e As DevExpress.Data.ServerModeExceptionThrownEventArgs)
    '    Dim ex = e.Exception
    '    Console.WriteLine(ex.Message)
    'End Sub

    'Protected Sub EntityServerModeDataSource_InconsistencyDetected(sender As Object, e As DevExpress.Data.ServerModeInconsistencyDetectedEventArgs)
    '    Console.WriteLine(e.ToString)
    'End Sub
End Class
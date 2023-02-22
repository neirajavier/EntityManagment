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
Imports AjaxControlToolkit.HtmlEditor.ToolbarButtons
Imports SharpKml.Dom

Public Class MantenimientoAlertas
    Inherits System.Web.UI.Page

    Private myflagG As Boolean = True
    Private myflagV As Boolean = True
    Private myflagS As Boolean = True
    Private myFlagCustom1 As Boolean = False
    Private myFlagCustom2 As Boolean = False
    Private myFlagCustom3 As Boolean = False
    Private myFlagCustom4 As Boolean = True
    Private myGroup As Integer = 0
    Private tableAux As DataSet
    Private tableAux2 As DataSet
    Private Unidades
    Private Subusuarios
    Public numUnidades As Integer = 0
    Public numSubuser As Integer = 0

    Private oAlerta As Alerta
    Private objUsuario As Usuario
    Private BD As New OdinDataContext()

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("TableAlertasT") IsNot Nothing) Then
            gridAlerta.DataSource = TryCast(Session("TableAlertasT"), DataTable)
            gridAlerta.DataBind()
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
                        Session("IdSubUsuario") = hidsubusuario.Value
                        valor = BD.spUsuarioEntidadConsultarSubusuario(CInt(hidusuario.Value), "127001", CInt(hidsubusuario.Value))
                        For Each entidad In valor
                            Session("Entidades") = CStr(entidad.IdEntidad)
                        Next
                    Else
                        valor = BD.spUsuarioEntidadConsultar(CInt(hidusuario.Value), "127001")
                        For Each entidad In valor
                            Session("Entidades") = CStr(entidad.IdEntidad)
                        Next
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
                    hdregs.Value = "1"
                ElseIf objUsuario.ValidarSubUsuario2(Session("SubUsuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
                    Session("Usuario") = objUsuario.ConsultarUsuarioNombre(CInt(Session("IdUsuario")))
                    Session("esUsuario") = False
                    hdregs.Value = "0"
                End If
            End If

            If Session("baseURL27") Is Nothing Then
                Session("baseURL27") = "FormAlerta.aspx"
            End If
            If Session("baseURL28") Is Nothing Then
                Session("baseURL28") = "DeleteAlerta.aspx"
            End If
            If Session("baseURL29") Is Nothing Then
                Session("baseURL29") = "FormGrupoAlert.aspx"
            End If
            If Session("baseURL30") Is Nothing Then
                Session("baseURL30") = "DeleteGrupoAlt.aspx"
            End If
            If Session("baseURL31") Is Nothing Then
                Session("baseURL31") = "AsignarGrupoAlt.aspx"
            End If
            If Session("baseURL32") Is Nothing Then
                Session("baseURL32") = "FormActAlerta.aspx"
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Dim miLista As New List(Of String)()
            Dim JSONString As String = Nothing
            If Not IsPostBack Then
                LlenarGrupos()
                LlenarAlertas()

                If (gridGrupo.FocusedRowIndex = -1) Then
                    oAlerta = New Alerta(Me, Session("PaisLogin"))
                    If (Session("esUsuario")) Then
                        numUnidades = oAlerta.ContarAlertas(CInt(Session("IdUsuario")))
                    Else
                        numUnidades = oAlerta.ContarAlertas2(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))
                    End If

                    miLista.Add(CStr(numUnidades))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridAlerta.JSProperties("cpNumbersJS") = JSONString

                    halertas.Value = numUnidades

                    'btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Alertas</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numUnidades) + "</span></div> </div>"
                    lbCount.Text = "<div style='padding-top: 2px'> <span class='badge badge-dark' style='float: right; font-size: 12px;'>" + CStr(numUnidades) + "</span></div>"
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
        Grupos = BD.spGrupoAlertaConsultar(CInt(Session("IdUsuario")), 0).ToList

        Dim table As DataTable = TryCast(Grupos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("grupo", GetType(String))
            table.Columns.Add("descripcion", GetType(String))
            table.Columns.Add("edt", GetType(Boolean))
            table.Columns.Add("del", GetType(Boolean))

            For Each grp In CType(Grupos, IEnumerable(Of Object))
                table.Rows.Add(grp.IdGrupoAlerta.ToString, grp.NombreGrupoAlerta.ToString.ToUpper, grp.DescripcionGrupoAlerta.ToString.ToUpper, True, True)
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
                e.DisplayText = (e.Value.ToString.Substring(0, 12).ToUpper) + "..."
            Else
                e.DisplayText = (e.Value.ToString).ToUpper
            End If
        End If
    End Sub

    Protected Sub gridGrupo_PreRender(sender As Object, e As EventArgs)
        If (Not IsPostBack) Then
            gridGrupo.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub btnAlert_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)

            If Session("baseURL27") Is Nothing Then
                Session("baseURL27") = "FormAlerta.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?data={1}&id={2}", Session("baseURL27"), "nueva", "0")

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ OnMoreInfoClick('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub btnGrupoN_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)

            If Session("baseURL29") Is Nothing Then
                Session("baseURL29") = "FormGrupoAlert.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL29"), "0", "0", "0", "N", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "N")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub editar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL29") Is Nothing Then
                Session("baseURL29") = "FormGrupoAlert.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grp As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim des As String = DataBinder.Eval(parent.DataItem, "descripcion")

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL29"), id, grp, des, "E", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "E")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub borrar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL30") Is Nothing Then
                Session("baseURL30") = "DeleteGrupoAlt.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim message As String = "¿Desea eliminar el grupo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL30"), grupoId, message)
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

    Protected Sub gridGrupo_FocusedRowChanged(sender As Object, e As EventArgs)
        myFlagCustom4 = False
    End Sub

#End Region

#Region "Alertas"
    Private Sub LlenarAlertas()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "id"
                    id.Caption = "Alertas"
                    id.Name = "alias"
                    id.VisibleIndex = "1"
                    id.Width = "850"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridAlerta.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "data"
                    data.Caption = "Tipo"
                    data.Name = "tipo"
                    data.VisibleIndex = "2"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridAlerta.Columns.Add(data)
                    Dim idac As New GridViewDataColumn()
                    idac.FieldName = "ida"
                    idac.Caption = "IdAl"
                    idac.VisibleIndex = "3"
                    idac.SetColVisible(False)
                    idac.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    idac.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridAlerta.Columns.Add(idac)
                    Dim asg As New GridViewDataColumn()
                    asg.FieldName = "asg"
                    asg.Caption = " "
                    asg.VisibleIndex = "4"
                    asg.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    asg.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridAlerta.Columns.Add(asg)
                    'Dim act As New GridViewDataCheckColumn()
                    'act.FieldName = "act"
                    'act.Caption = " "
                    'act.VisibleIndex = "7"
                    'act.PropertiesCheckEdit.ToggleSwitchDisplayMode = ToggleSwitchDisplayMode.Always
                    'act.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    'act.CellStyle.Border.BorderStyle = BorderStyle.None
                    'gridAlerta.Columns.Add(act)
                End If

                If (Not IsPostBack) Then
                    gridAlerta.DataBind()
                Else
                    gridAlerta.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub GridA_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        'gridAlerta.DataSource = GetTableGridAlertasT()
        Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

        If (myFlagCustom1) Then
            If (grupos <= 0) Then
                gridAlerta.DataSource = GetTableGridAlertasT()
            Else
                If (grupos > 0) Then
                    If (gridGrupo.FocusedRowIndex = -1) Then
                        gridAlerta.DataSource = GetTableGridAlertasT()
                    Else
                        If (myflagV) Then
                            gridAlerta.DataSource = GetTableGridAlertasT()
                        Else
                            If (myFlagCustom4) Then
                                gridAlerta.DataSource = GetTableGridAlertasT()
                            Else
                                gridAlerta.DataSource = GetTableGridAlertas()
                            End If

                        End If

                    End If
                End If
            End If
        ElseIf (myFlagCustom2) Then
            gridAlerta.DataSource = GetTableGridAlertasT()
        ElseIf (myFlagCustom3) Then
            If (grupos > 0) Then
                If (myFlagCustom4) Then
                    gridAlerta.DataSource = GetTableGridAlertasT()
                    gridGrupo.FocusedRowIndex = -1
                Else
                    gridAlerta.DataSource = GetTableGridAlertas()
                End If
            Else
                gridAlerta.DataSource = GetTableGridAlertasT()
            End If
        Else
            If (gridGrupo.FocusedRowIndex = -1) Then
                gridAlerta.DataSource = GetTableGridAlertasT()
            Else
                gridAlerta.DataSource = GetTableGridAlertas()
            End If

        End If
    End Sub

    Private Function GetTableGridAlertas() As DataTable
        Dim table As DataTable
        Dim Alertas As New Object()
        Dim estado As Boolean
        oAlerta = New Alerta(Me, Session("PaisLogin"))

        If (myGroup = 0) Then
            myGroup = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
        End If

        Alertas = BD.spAlertaGrupoAlertaConsultar(myGroup, 0, CInt(Session("IdUsuario"))).ToList
        table = TryCast(Alertas, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("ida", GetType(String))
            table.Columns.Add("asg", GetType(String))
            table.Columns.Add("act", GetType(String))

            For Each alerta In CType(Alertas, IEnumerable(Of Object))
                tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")), alerta.IdAlerta)
                For Each fila As DataRow In tableAux.Tables(0).Rows()
                    estado = IIf(fila(38).Equals("A"), True, False)
                    table.Rows.Add(fila(7).ToString, fila(2).ToString, fila(0).ToString, fila(17).ToString, estado)
                Next
            Next

            Session("TableAlertas") = table
        End If
        Return table
    End Function

    Private Function GetTableGridAlertasT() As DataTable
        Dim table As DataTable
        Dim Alertas As New Object()
        Dim estado As Boolean
        oAlerta = New Alerta(Me, Session("PaisLogin"))

        If (Session("esUsuario")) Then
            tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")))

            table = TryCast(Alertas, DataTable)
            If table Is Nothing Then
                table = New DataTable()
                table.Columns.Add("id", GetType(String))
                table.Columns.Add("data", GetType(String))
                table.Columns.Add("ida", GetType(String))
                table.Columns.Add("asg", GetType(String))
                table.Columns.Add("act", GetType(Boolean))

                For Each fila As DataRow In tableAux.Tables(0).Rows()
                    estado = IIf(fila(38).Equals("A"), True, False)
                    table.Rows.Add(fila(7).ToString, fila(3).ToString, fila(0).ToString, fila(17).ToString, estado)
                Next

                Session("TableAlertasT") = table
            End If
        Else
            tableAux = oAlerta.ConsultarSubAlt(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))

            table = TryCast(Alertas, DataTable)
            If table Is Nothing Then
                table = New DataTable()
                table.Columns.Add("id", GetType(String))
                table.Columns.Add("data", GetType(String))
                table.Columns.Add("ida", GetType(String))
                table.Columns.Add("asg", GetType(String))
                table.Columns.Add("act", GetType(Boolean))

                For Each fila As DataRow In tableAux.Tables(0).Rows()
                    tableAux2 = oAlerta.Consultar2(CInt(Session("IdUsuario")), fila(0).ToString)
                    For Each fila1 As DataRow In tableAux2.Tables(0).Rows()
                        estado = IIf(fila1(38).Equals("A"), True, False)
                        table.Rows.Add(fila1(7).ToString, fila1(2).ToString, fila1(0).ToString, fila1(17).ToString, estado)
                    Next
                Next

                Session("TableAlertasT") = table
            End If
        End If



        Return table
    End Function

    Protected Sub gridAlerta_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Try
            Dim grupoId As String = ""
            Dim alerta As Integer = 0
            Dim miLista As New List(Of String)()
            Dim alertas As String()
            Dim JSONString As String = Nothing
            Dim height As Integer = CInt(hfHeight.Value)
            oAlerta = New Alerta(Me, Session("PaisLogin"))

            Dim mobile As Boolean = IsMobileBrowser()
            gridAlerta.Selection.UnselectAll()

            If (e.Parameters.Equals("1")) Then
                If (Not gridGrupo.FocusedRowIndex < 0) Then
                    grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
                    myGroup = CInt(grupoId)
                    myflagV = False

                    numUnidades = oAlerta.ContarGruposAlertas(CInt(Session("IdUsuario")), myGroup)

                    miLista.Add(CStr(numUnidades))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridAlerta.JSProperties("cpNumbersJS") = JSONString
                Else
                    myflagV = True
                End If

                myFlagCustom1 = True
                gridAlerta.DataBind()
            ElseIf (e.Parameters.Equals("alertas")) Then
                myFlagCustom2 = True

                If (Session("esUsuario")) Then
                    numUnidades = oAlerta.ContarAlertas(CInt(Session("IdUsuario")))
                Else
                    numUnidades = oAlerta.ContarAlertas2(CInt(Session("IdUsuario")), CInt(Session("IdSubUsuario")))
                End If

                miLista.Add(CStr(numUnidades))
                JSONString = JsonConvert.SerializeObject(miLista)

                gridAlerta.JSProperties("cpNumbersJS") = JSONString

                gridAlerta.Selection.UnselectAll()
                gridAlerta.DataBind()
            ElseIf (e.Parameters.Contains("ON")) Then
                alertas = e.Parameters.ToString.Split("-")
                BD.spD_AlertaCambiarEstado(alertas(1), CInt(Session("IdUsuario")), 1)
                gridAlerta.DataBind()
            ElseIf (e.Parameters.Contains("OFF")) Then
                alertas = e.Parameters.ToString.Split("-")
                BD.spD_AlertaCambiarEstado(alertas(1), CInt(Session("IdUsuario")), 0)
                gridAlerta.DataBind()
            End If


            If (mobile) Then
                gridAlerta.SettingsPager.PageSize = 10
            Else
                If (height <= 600) Then
                    gridAlerta.SettingsPager.PageSize = ((height / 100) * 2) + 2
                ElseIf (height > 600 And height <= 900) Then
                    gridAlerta.SettingsPager.PageSize = ((height / 100) * 2) + 3
                ElseIf (height > 900) Then
                    gridAlerta.SettingsPager.PageSize = ((height / 100) * 2)
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridAlerta_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "data" Then
            If (e.Value IsNot Nothing) Then
                If (e.Value.ToString.Equals("EVENTO")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/evento.png' title='Evento' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("GEOCERCA IN")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/geoIn.png' title='Geocerca In' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("GEOCERCA OUT")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/geoOut.png' title='Geocerca Out' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("MULTICRITERIO")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/multicriterio2.png' title='Multicriterio' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("KILOMETRAJE")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/kilometraje.png' title='Kilometraje' style='width:25px; height:25px'/>"
                ElseIf (e.Value.ToString.Equals("SERVICIO")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/servicio2.png' title='Servicio' style='width:25px; height:25px'/>"
                End If
            End If
        End If

        If e.Column.FieldName = "asg" Then
            If (e.Value IsNot Nothing) Then
                If (e.Value.ToString.Equals("SI")) Then
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/car-con.png' title='Alerta asignada' style='width:25px; height:25px'/>"
                Else
                    e.EncodeHtml = False
                    e.DisplayText = "<image src='../../images/car-ninguno.png' title='Alerta sin asignar' style='width:25px; height:25px'/>"
                End If
            End If
        End If
    End Sub

    Protected Sub editar2_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL32") Is Nothing Then
                Session("baseURL32") = "FormActAlerta.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "ida")

            Dim contentUrl As String = String.Format("{0}?data={1}&id={2}", Session("baseURL32"), "edita", id)

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo2('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub borrar2_Init(sender As Object, e As EventArgs)

    End Sub

    'Protected Sub gridAlerta_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
    '    If (e.Column.FieldName.Equals("act")) Then
    '        Dim chkBox As ASPxCheckBox = TryCast(e.Editor, ASPxCheckBox)
    '        Dim parent As GridViewDataItemTemplateContainer = chkBox.NamingContainer

    '        chkBox.ClientSideEvents.CheckedChanged = String.Format("function(s, e){{ alert('{0}'); }}", gridAlerta.GetRowValues(e.Column.VisibleIndex, "id"))
    '    End If
    'End Sub

    Protected Sub chkEstado_Init(sender As Object, e As EventArgs)
        Try
            Dim chkBox As ASPxCheckBox = TryCast(sender, ASPxCheckBox)
            Dim parent As GridViewDataItemTemplateContainer = chkBox.NamingContainer

            Dim valor As Boolean = DataBinder.Eval(parent.DataItem, "act")
            Dim myAlert As String = DataBinder.Eval(parent.DataItem, "ida")

            chkBox.Checked = valor
            chkBox.ToolTip = IIf(valor, "Activa", "Inactiva")
            chkBox.ClientSideEvents.ValueChanged = String.Format("function(s, e){{ var value = s.GetChecked(); onCheckedStatus(value,'{0}'); }}", myAlert)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub CancelEditing(e As CancelEventArgs)
        e.Cancel = True
        gridAlerta.CancelEdit()
    End Sub

    Protected Sub gridAlerta_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
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

    Protected Sub gridAlerta_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Private Sub Update2(ByVal keys As Integer, ByVal newValues As OrderedDictionary)
        Dim id, nom As String
        oAlerta = New Alerta(Me, Session("PaisLogin"))
        Try
            id = keys
            nom = newValues.Values(1)

            oAlerta.UpdateNombreAlerta(CInt(id), nom.ToUpper, Session("Usuario").ToString, CInt(Session("IdUsuario")))
            'BD.spD_AlertaCambiarNombre(CInt(id), nom.ToUpper, Session("Usuario").ToString)
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Protected Sub asignar_Init(sender As Object, e As EventArgs)
    '    Try
    '        Dim btn As ASPxButton = TryCast(sender, ASPxButton)
    '        Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

    '        If Session("baseURL") Is Nothing Then
    '            Session("baseURL") = "AsignarGrupo.aspx"
    '        End If

    '        Dim myVeh As String = DataBinder.Eval(parent.DataItem, "id").ToString
    '        'Dim myAct As String = DataBinder.Eval(parent.DataItem, "idac").ToString
    '        Dim myAct As String = DataBinder.Eval(parent.DataItem, "data").ToString
    '        Dim myVid As String = DataBinder.Eval(parent.DataItem, "vid").ToString

    '        Dim contentUrl As String = String.Format("{0}?myVid={1}&myVeh={2}&myAct={3}", Session("baseURL"), myVid, myVeh, myAct)

    '        btn.ClientSideEvents.Click = String.Format("function(s, e) {{ OnMoreInfoClick('{0}'); }}", contentUrl)
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

#End Region

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
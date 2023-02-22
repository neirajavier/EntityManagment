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

Partial Class MantenimientoGrupo
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
    Private Unidades
    Private Subusuarios
    Public numUnidades As Integer = 0
    Public numSubuser As Integer = 0

    Private oActivo As Activo
    Private objUsuario As Usuario
    Private BD As New OdinDataContext()

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        If (Session("TableUnidadesT") IsNot Nothing) Then
            gridVehiculo.DataSource = TryCast(Session("TableUnidadesT"), DataTable)
            gridVehiculo.DataBind()
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
            If (objUsuario.ValidarUsuario2(Session("Usuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave"))) Then
                Session("esUsuario") = True
                hdregs.Value = "1"
                'accPanel.Visible = True
            ElseIf objUsuario.ValidarSubUsuario2(Session("SubUsuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
                Session("Usuario") = objUsuario.ConsultarUsuarioNombre(CInt(Session("IdUsuario")))
                Session("esUsuario") = False
                hdregs.Value = "0"
                'accPanel.Visible = False
            End If

            If Session("baseURL") Is Nothing Then
                Session("baseURL") = "AsignarGrupo.aspx"
            End If
            If Session("baseURL2") Is Nothing Then
                Session("baseURL2") = "AsignarGrupo2.aspx"
            End If
            If Session("baseURL3") Is Nothing Then
                Session("baseURL3") = "AsignarGrupoS.aspx"
            End If
            If Session("baseURL4") Is Nothing Then
                Session("baseURL4") = "AsignarGrupoS2.aspx"
            End If
            If Session("baseURL5") Is Nothing Then
                Session("baseURL5") = "FormGrupo.aspx"
            End If
            If Session("baseURL6") Is Nothing Then
                Session("baseURL6") = "DeleteGrupo.aspx"
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            Dim miLista As New List(Of String)()
            Dim JSONString As String = Nothing
            If Not IsPostBack Then
                LlenarGrupos()
                LlenarVehiculosUsuario()
                'If (Session("esUsuario")) Then
                '    LlenarSubUsuarios()
                'End If

                If (gridGrupo.FocusedRowIndex = -1) Then
                    oActivo = New Activo(Me, Session("PaisLogin"))
                    If (Session("esUsuario")) Then
                        numUnidades = oActivo.ContarActivos(CStr(Session("Entidades")))
                    Else
                        numUnidades = BD.spListarActivosEntidadSubUsuario(Session("Entidades"), CInt(Session("IdSubUsuario")), CInt(Session("IdUsuario"))).Count
                    End If
                    'numSubuser = oActivo.ContarSubusuarios(CInt(Session("IdUsuario")))

                    miLista.Add(CStr(numUnidades))
                    'miLista.Add(CStr(numSubuser))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridVehiculo.JSProperties("cpNumbersJS") = JSONString

                    hactivos.Value = numUnidades
                    'hsubuser.Value = numSubuser

                    btnTodos.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Vehículos</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numUnidades) + "</span></div> </div>"
                    'btnTodos2.Text = "<div class='row'> <div class='col-sm-8' style='padding-top: 3px'>Subusuarios</div> <div class='col-sm-4'><span class='badge badge-dark' style='float:right; font-size: 12px;'>" + CStr(numSubuser) + "</span></div> </div>"
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
        Grupos = BD.spGrupoConsultar(CInt(Session("IdUsuario")), "").ToList

        Dim table As DataTable = TryCast(Grupos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("grupo", GetType(String))
            table.Columns.Add("descripcion", GetType(String))
            table.Columns.Add("edt", GetType(Boolean))
            table.Columns.Add("del", GetType(Boolean))

            For Each grp In CType(Grupos, IEnumerable(Of Object))
                table.Rows.Add(grp.IdGrupo.ToString, grp.Grupo.ToString.ToUpper, grp.Descripcion.ToString.ToUpper, True, True)
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

    'Protected Sub editar_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim btn As ASPxButton = TryCast(sender, ASPxButton)
    '        Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

    '        tbID.Text = DataBinder.Eval(parent.DataItem, "id")
    '        tbGrupo.Text = DataBinder.Eval(parent.DataItem, "grupo")
    '        tbDescripcion.Text = DataBinder.Eval(parent.DataItem, "descripcion")

    '        pcGrupo.ShowOnPageLoad = True
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnGrupoN_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)

            If Session("baseURL5") Is Nothing Then
                Session("baseURL5") = "FormGrupo.aspx"
            End If

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL5"), "0", "0", "0", "N", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "N")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub editar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL5") Is Nothing Then
                Session("baseURL5") = "FormGrupo.aspx"
            End If

            Dim id As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grp As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim des As String = DataBinder.Eval(parent.DataItem, "descripcion")

            Dim contentUrl As String = String.Format("{0}?myId={1}&myGrp={2}&myDes={3}&estado={4}&user={5}", Session("baseURL5"), id, grp, des, "E", Session("Usuario"))

            btn.ClientSideEvents.Click = String.Format("function(s,e) {{ ShowInfo('{0}','{1}'); }}", contentUrl, "E")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    'Protected Sub borrar_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim grupoData As String = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "grupo").ToString()
    '        lbMessage.Text = "¿Desea eliminar el registro del grupo " + grupoData + "?"
    '        popupDelete.ShowOnPageLoad = True
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    Protected Sub borrar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL6") Is Nothing Then
                Session("baseURL6") = "DeleteGrupo.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "id")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "grupo")
            Dim message As String = "¿Desea eliminar el grupo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL6"), grupoId, message)
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

#Region "Vehiculos"
    Private Sub LlenarVehiculosUsuario()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "id"
                    id.Caption = "Vehiculo"
                    id.Name = "alias"
                    id.VisibleIndex = "2"
                    id.Width = "250"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridVehiculo.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "data"
                    data.Caption = "Etiqueta"
                    data.Name = "etiqueta"
                    data.VisibleIndex = "3"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridVehiculo.Columns.Add(data)
                    Dim vid As New GridViewDataColumn()
                    vid.FieldName = "vid"
                    vid.Caption = "VId"
                    vid.VisibleIndex = "4"
                    vid.SetColVisible(False)
                    vid.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    vid.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridVehiculo.Columns.Add(vid)
                    Dim idac As New GridViewDataColumn()
                    idac.FieldName = "idac"
                    idac.Caption = "IdAct"
                    idac.VisibleIndex = "5"
                    idac.SetColVisible(False)
                    idac.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    idac.CellStyle.Border.BorderStyle = BorderStyle.None
                    gridVehiculo.Columns.Add(idac)
                End If

                If (Not IsPostBack) Then
                    gridVehiculo.DataBind()
                Else
                    gridVehiculo.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub GridU_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))

        If (myFlagCustom1) Then
            If (grupos <= 0) Then
                gridVehiculo.DataSource = GetTableGridUnidadesT()
            Else
                If (grupos > 0) Then
                    If (gridGrupo.FocusedRowIndex = -1) Then
                        gridVehiculo.DataSource = GetTableGridUnidadesT()
                    Else
                        If (myflagV) Then
                            gridVehiculo.DataSource = GetTableGridUnidadesT()
                        Else
                            If (myFlagCustom4) Then
                                gridVehiculo.DataSource = GetTableGridUnidadesT()
                            Else
                                gridVehiculo.DataSource = GetTableGridUnidades()
                            End If

                        End If

                    End If
                End If
            End If
        ElseIf (myFlagCustom2) Then
            gridVehiculo.DataSource = GetTableGridUnidadesT()
        ElseIf (myFlagCustom3) Then
            If (grupos > 0) Then
                If (myFlagCustom4) Then
                    gridVehiculo.DataSource = GetTableGridUnidadesT()
                    gridGrupo.FocusedRowIndex = -1
                Else
                    gridVehiculo.DataSource = GetTableGridUnidades()
                End If
            Else
                gridVehiculo.DataSource = GetTableGridUnidadesT()
            End If
        Else
            If (gridGrupo.FocusedRowIndex = -1) Then
                gridVehiculo.DataSource = GetTableGridUnidadesT()
            Else
                gridVehiculo.DataSource = GetTableGridUnidades()
            End If

        End If
    End Sub

    Private Function GetTableGridUnidades() As DataTable
        Dim table As DataTable
        Dim Entidades As New Object()

        If (myGroup = 0) Then
            myGroup = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
        End If

        Entidades = BD.spActivoGrupoConsultar(CInt(Session("IdUsuario")), myGroup).ToList
        table = TryCast(Entidades, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("vid", GetType(String))
            table.Columns.Add("idac", GetType(String))

            For Each Entidad In CType(Entidades, IEnumerable(Of Object))
                table.Rows.Add(Entidad.Alias.ToString, Entidad.Etiqueta.ToString, Entidad.Vid.ToString, Entidad.IdActivo.ToString)
            Next

            Session("TableUnidades") = table
        End If

        'If (Session("TableUnidades") IsNot Nothing) Then
        '    table = TryCast(Session("TableUnidades"), DataTable)
        'Else
        '    Entidades = BD.spActivoGrupoConsultar(CInt(Session("IdUsuario")), myGroup).ToList
        '    table = TryCast(Entidades, DataTable)

        '    If table Is Nothing Then
        '        table = New DataTable()
        '        table.Columns.Add("id", GetType(String))
        '        table.Columns.Add("data", GetType(String))
        '        table.Columns.Add("vid", GetType(String))
        '        table.Columns.Add("idac", GetType(String))

        '        For Each Entidad In CType(Entidades, IEnumerable(Of Object))
        '            table.Rows.Add(Entidad.Alias.ToString, Entidad.Etiqueta.ToString, Entidad.Vid.ToString, Entidad.IdActivo.ToString)
        '        Next

        '        Session("TableUnidades") = table
        '    End If
        'End If

        Return table
    End Function

    Private Function GetTableGridUnidadesT() As DataTable
        Dim table As DataTable
        Dim Entidades As New Object()

        'If (Session("TableUnidadesT") IsNot Nothing) Then
        '    table = TryCast(Session("TableUnidadesT"), DataTable)
        'Else
        '    If (Session("esUsuario")) Then
        '        Entidades = BD.spListarActivosEntidad(Session("Entidades"), 0).ToList
        '    Else
        '        Entidades = BD.spListarActivosEntidadSubUsuario(Session("Entidades"), CInt(Session("IdSubUsuario")), CInt(Session("IdUsuario"))).ToList
        '    End If

        '    table = TryCast(Entidades, DataTable)

        '    If table Is Nothing Then
        '        table = New DataTable()
        '        table.Columns.Add("id", GetType(String))
        '        table.Columns.Add("data", GetType(String))
        '        table.Columns.Add("vid", GetType(String))
        '        table.Columns.Add("idac", GetType(String))

        '        For Each Entidad In CType(Entidades, IEnumerable(Of Object))
        '            table.Rows.Add(Entidad.Alias.ToString, Entidad.Etiqueta.ToString, Entidad.Vid.ToString, Entidad.IdActivo.ToString)
        '        Next

        '        Session("TableUnidadesT") = table
        '    End If
        'End If

        If (Session("esUsuario")) Then
            Entidades = BD.spListarActivosEntidad(Session("Entidades"), 0).ToList
        Else
            Entidades = BD.spListarActivosEntidadSubUsuario(Session("Entidades"), CInt(Session("IdSubUsuario")), CInt(Session("IdUsuario"))).ToList
        End If
        table = TryCast(Entidades, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("id", GetType(String))
            table.Columns.Add("data", GetType(String))
            table.Columns.Add("vid", GetType(String))
            table.Columns.Add("idac", GetType(String))

            For Each Entidad In CType(Entidades, IEnumerable(Of Object))
                table.Rows.Add(Entidad.Alias.ToString, Entidad.Etiqueta.ToString, Entidad.Vid.ToString, Entidad.IdActivo.ToString)
            Next

            Session("TableUnidadesT") = table
        End If

        Return table
    End Function

    Protected Sub gridVehiculo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        Try
            Dim grupoId As String = ""
            Dim miLista As New List(Of String)()
            Dim JSONString As String = Nothing
            Dim height As Integer = CInt(hfHeight.Value)
            btnAsignar.ClientVisible = False

            oActivo = New Activo(Me, Session("PaisLogin"))
            'If (pgGrids.ActiveTabPage.Name.Equals("tbUnidades")) Then
            gridVehiculo.Selection.UnselectAll()

            If (e.Parameters.Equals("1")) Then
                If (Not gridGrupo.FocusedRowIndex < 0) Then
                    grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
                    myGroup = CInt(grupoId)
                    myflagV = False

                    numUnidades = oActivo.ContarActivosGrupos(myGroup)
                    'numSubuser = oActivo.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), myGroup)

                    miLista.Add(CStr(numUnidades))
                    'miLista.Add(CStr(numSubuser))
                    JSONString = JsonConvert.SerializeObject(miLista)

                    gridVehiculo.JSProperties("cpNumbersJS") = JSONString
                Else
                    myflagV = True
                End If

                myFlagCustom1 = True
                gridVehiculo.DataBind()
            ElseIf (e.Parameters.Equals("unidades")) Then
                myFlagCustom2 = True

                numUnidades = oActivo.ContarActivos(CStr(Session("Entidades")))
                'numUnidades = BD.spListarActivosEntidad(Session("Entidades"), 0).Count
                'numSubuser = oActivo.ContarSubusuarios(CInt(Session("IdUsuario")))

                miLista.Add(CStr(numUnidades))
                'miLista.Add(CStr(numSubuser))
                JSONString = JsonConvert.SerializeObject(miLista)

                gridVehiculo.JSProperties("cpNumbersJS") = JSONString

                gridVehiculo.Selection.UnselectAll()
                gridVehiculo.DataBind()
            ElseIf (e.Parameters.Equals("2")) Then
                myFlagCustom3 = True
                gridVehiculo.Selection.UnselectAll()
                gridVehiculo.DataBind()
            ElseIf (e.Parameters.Equals("cfunidades")) Then
                myFlagCustom4 = True
            ElseIf (e.Parameters.Equals("sfunidades")) Then
                myFlagCustom4 = False
                gridVehiculo.Selection.UnselectAll()
                gridVehiculo.DataBind()
            End If

            'gridVehiculo.SettingsPager.PageSize = ((height / 100) * 2) + 2
            If (height <= 600) Then
                gridVehiculo.SettingsPager.PageSize = ((height / 100) * 2) + 2
            ElseIf (height > 600 And height <= 900) Then
                gridVehiculo.SettingsPager.PageSize = ((height / 100) * 2) + 3
            ElseIf (height > 900) Then
                gridVehiculo.SettingsPager.PageSize = ((height / 100) * 2)
            End If
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

            If Session("baseURL") Is Nothing Then
                Session("baseURL") = "AsignarGrupo.aspx"
            End If

            Dim myVeh As String = DataBinder.Eval(parent.DataItem, "id").ToString
            'Dim myAct As String = DataBinder.Eval(parent.DataItem, "idac").ToString
            Dim myAct As String = DataBinder.Eval(parent.DataItem, "data").ToString
            Dim myVid As String = DataBinder.Eval(parent.DataItem, "vid").ToString

            Dim contentUrl As String = String.Format("{0}?myVid={1}&myVeh={2}&myAct={3}", Session("baseURL"), myVid, myVeh, myAct)

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
    '                id.Width = "250"
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
    '                'Else
    '                '    gridSubusuario.DataBind()
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

    '    'Dim grupos As Integer = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    Dim grupos As Integer
    '    If (IsNothing(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))) Then
    '        grupos = 0
    '    Else
    '        grupos = CInt(gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id"))
    '    End If

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
    '        Subusuario = BD.spGrupoSubUsuariosConsultar2(CInt(Session("IdUsuario")), 0, myGroup).ToList
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
    '            SubusuarioDet = BD.spSubUsuarioDatosConsultar(CInt(Subuser.IdUsuario), CInt(Subuser.IdSubUsuario)).ToList
    '            For Each Subuserdet In CType(SubusuarioDet, IEnumerable(Of Object))
    '                table.Rows.Add(Subuserdet.SubUsuario.ToString, Subuserdet.NombreCompleto.ToString, Subuserdet.IdSubUsuario.ToString)
    '            Next
    '        Next

    '        Session("TableSubusuarios") = table
    '    End If
    '    Return table
    'End Function

    'Protected Sub asignarS_Init(sender As Object, e As EventArgs)
    '    Try
    '        Dim btn As ASPxButton = TryCast(sender, ASPxButton)
    '        Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

    '        If Session("baseURL3") Is Nothing Then
    '            Session("baseURL3") = "AsignarGrupoS.aspx"
    '        End If

    '        Dim mySub As String = DataBinder.Eval(parent.DataItem, "id").ToString
    '        Dim myData As String = DataBinder.Eval(parent.DataItem, "data").ToString
    '        Dim mySid As String = DataBinder.Eval(parent.DataItem, "sid").ToString

    '        Dim contentUrl As String = String.Format("{0}?mySub={1}&myData={2}&mySid={3}", Session("baseURL3"), mySub, myData.Replace(" ", "_"), mySid)

    '        btn.ClientSideEvents.Click = String.Format("function(s, e) {{ OnMoreInfoClickS('{0}'); }}", contentUrl)
    '        btnAsignarS.ClientVisible = False
    '    Catch ex As Exception
    '        Console.WriteLine(ex.Message)
    '    End Try
    'End Sub

    'Protected Sub gridSubusuario_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
    '    Try
    '        Dim grupoId As String = ""
    '        Dim miLista As New List(Of String)()
    '        Dim JSONString As String = Nothing
    '        Dim height As Integer = CInt(hfHeight.Value)
    '        btnAsignar.ClientVisible = False

    '        oActivo = New Activo(Me, Session("PaisLogin"))
    '        'If (pgGrids.ActiveTabPage.Name.Equals("tbSubusuarios")) Then
    '        gridSubusuario.Selection.UnselectAll()

    '        If (e.Parameters.Equals("1")) Then
    '            If (Not gridGrupo.FocusedRowIndex < 0) Then
    '                grupoId = gridGrupo.GetRowValues(gridGrupo.FocusedRowIndex, "id").ToString()
    '                myGroup = CInt(grupoId)
    '                myflagV = False

    '                numUnidades = oActivo.ContarActivosGrupos(myGroup)
    '                numSubuser = oActivo.ContarSubusuariosGrupos(CInt(Session("IdUsuario")), myGroup)

    '                miLista.Add(CStr(numUnidades))
    '                miLista.Add(CStr(numSubuser))
    '                JSONString = JsonConvert.SerializeObject(miLista)

    '                gridSubusuario.JSProperties("cpNumbers2JS") = JSONString
    '            Else
    '                'myflagV = True
    '            End If

    '            myFlagCustom1 = True
    '            gridSubusuario.DataBind()
    '        ElseIf (e.Parameters.Equals("subusuarios")) Then
    '            myFlagCustom2 = True

    '            numUnidades = oActivo.ContarActivos(CStr(Session("Entidades")))
    '            'numUnidades = BD.spListarActivosEntidad(Session("Entidades"), 0).Count
    '            numSubuser = oActivo.ContarSubusuarios(CInt(Session("IdUsuario")))

    '            miLista.Add(CStr(numUnidades))
    '            miLista.Add(CStr(numSubuser))
    '            JSONString = JsonConvert.SerializeObject(miLista)

    '            gridSubusuario.JSProperties("cpNumbers2JS") = JSONString

    '            gridSubusuario.Selection.UnselectAll()
    '            gridSubusuario.DataBind()
    '        ElseIf (e.Parameters.Equals("2")) Then
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

            Session("TableSelectUnidades") = tableAux
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

    Protected Sub gridVehiculo_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As String = ""
            For Each args In e.Keys.Values
                key = args
            Next args

            Update(key, e.NewValues)
            CancelEditing(e)
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub Update(ByVal keys As String, ByVal newValues As OrderedDictionary)
        Dim id, nom As String

        Try
            id = keys
            nom = newValues.Values(0)

            BD.spActivoEtiquetaActualizar(id, CInt(Session("IdUsuario")), nom, Session("Usuario"))
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub CancelEditing(e As CancelEventArgs)
        e.Cancel = True
        gridVehiculo.CancelEdit()
    End Sub

    Protected Sub gridVehiculo_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Protected Sub gridVehiculo_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If (e.DataColumn.Name.Equals("alias")) Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

    Protected Sub gridVehiculo_HtmlCommandCellPrepared(sender As Object, e As ASPxGridViewTableCommandCellEventArgs)
        If (e.CommandColumn.Name.Equals("chkall")) Then
            e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
        End If
    End Sub

End Class
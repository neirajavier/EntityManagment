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

Public Class MantenimientoParametrizacion
    Inherits System.Web.UI.Page

    Private myflagV As Boolean = True
    Private texto As String
    Dim Ejecucion As Integer = 0
    Dim Mensaje As String = ""

    Private BD As New OdinDataContext()
    Private objUsuario As Usuario
    Private datasetAux, datasetAux2 As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim valor As New Object()
            btnNuevo.ClientVisible = True
            btnNuevo2.ClientVisible = False
            txMotivo.ClientVisible = True
            txSubmotivo.ClientVisible = False
            cbMotivo.ClientVisible = False

            'If Request.QueryString("CP") <> "" Then
            '    Try
            '        BD = New OdinDataContext(Globales.getCadenaConexionPais(Request.QueryString("CP").ToUpper, False))
            '        Session("PaisLogin") = Request.QueryString("CP").ToUpper.ToString
            '    Catch ex As Exception
            '        Console.WriteLine(ex.Message)
            '    End Try
            'End If

            BD = New OdinDataContext(Globales.getCadenaConexionPais("EC", False))
            Session("PaisLogin") = "EC"

            If Request.QueryString("o") <> "" Then
                Try
                    Dim Keys = BD.spUsuarioConsultarKey(Request.QueryString("O"))
                    For Each Key In Keys
                        Try
                            hidusuario.Value = Key.IdUsuario
                            Session("Usuario") = Key.Usuario
                        Catch ex As Exception
                            hidusuario.Value = -1
                            Console.WriteLine(ex.Message)
                        End Try
                    Next

                    If hidusuario.Value = 0 Then
                        Dim KeysS = BD.spSubUsuarioConsultarKey(Request.QueryString("O"))
                        For Each Key In KeysS
                            Try
                                hidusuario.Value = Key.IdUsuario
                                hidsubusuario.Value = Key.IdSubUsuario
                                Session("Usuario") = Key.SubUsuario
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
            ElseIf objUsuario.ValidarSubUsuario2(Session("SubUsuario").Replace("%", "").Replace("'", "").Replace("*", "").Replace("""", ""), Session("Clave")) Then
                Session("esUsuario") = False
                hdregs.Value = "0"
            End If

            If Session("baseURL22") Is Nothing Then
                Session("baseURL22") = "DeleteMotivo.aspx"
            End If
            If Session("baseURL23") Is Nothing Then
                Session("baseURL23") = "DeleteSubmotivo.aspx"
            End If
            If Session("baseURL24") Is Nothing Then
                Session("baseURL24") = "DeleteCategoria.aspx"
            End If
            If Session("baseURL25") Is Nothing Then
                Session("baseURL25") = "DeleteSubcategoria.aspx"
            End If
            If Session("baseURL26") Is Nothing Then
                Session("baseURL26") = "DeleteTipoUbicacion.aspx"
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

        If Not IsPostBack Then
            LlenarMotivos()
            LlenarSubMotivos()
            LlenarCategoria()
            LlenarSubCategorias()
            LlenarTipoUbicacion()
            LlenarCombo()
        End If
    End Sub

    Private Sub LlenarCombo()
        Try
            objUsuario = New Usuario(Me, Session("PaisLogin"))

            datasetAux = objUsuario.LlenarCombo("MOTIVOLOG", Session("IdUsuario"))
            datasetAux2 = objUsuario.LlenarCombo("CATEGORIALOG", Session("IdUsuario"))

            cbMotivo.Items.Clear()
            cbCategoria.Items.Clear()

            For Each fila As DataRow In datasetAux.Tables(0).Rows()
                cbMotivo.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next
            For Each fila2 As DataRow In datasetAux2.Tables(0).Rows()
                cbCategoria.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#Region "Motivo"
    Private Sub LlenarMotivos()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "idM"
                    id.Caption = "Id"
                    id.VisibleIndex = "0"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.SetColVisible(False)
                    id.Name = "idm"
                    gridMotivo.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "dataM"
                    data.Caption = "Motivo"
                    data.VisibleIndex = "1"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "nombreM"
                    gridMotivo.Columns.Add(data)
                End If

                If (Not IsPostBack) Then
                    gridMotivo.DataBind()
                Else
                    gridMotivo.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridMotivo_DataBinding(sender As Object, e As EventArgs)
        gridMotivo.DataSource = GetTableGridMotivos()
    End Sub

    Private Function GetTableGridMotivos() As DataTable
        Dim table As DataTable
        Dim miLista As String()
        Dim Motivos As New Object()

        Motivos = BD.spLogisticaMotivoConsultar(CInt(Session("IdUsuario")), 0).ToList
        table = TryCast(Motivos, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("idM", GetType(String))
            table.Columns.Add("dataM", GetType(String))

            For Each Motivo In CType(Motivos, IEnumerable(Of Object))
                miLista = Motivo.Dato.ToString.Split("_")
                table.Rows.Add(miLista(0), Motivo.Motivo.ToString)
            Next

            Session("TableMotivos") = table
        End If

        Return table
    End Function

    Protected Sub gridMotivo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("NUEVO")) Then
            Try
                Dim motivo As String = txMotivo.Text
                BD.spLogisticaMotivoIngresar(CInt(Session("IdUsuario")), motivo.ToUpper)

                texto = "Motivo :: Ingresado con éxito."
                Funciones.EjecutarFuncionJavascript(Me, "showNotification('" + texto + "','S');")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            gridMotivo.DataBind()
        Else
            gridMotivo.DataBind()
        End If
    End Sub

    Protected Sub eliminar_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL22") Is Nothing Then
                Session("baseURL22") = "DeleteMotivo.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "idM")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "dataM")

            Dim message As String = "¿Desea eliminar el motivo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL22"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub cbMotivo_Callback(sender As Object, e As CallbackEventArgsBase)
        Try
            objUsuario = New Usuario(Me, Session("PaisLogin"))
            datasetAux = objUsuario.LlenarCombo("MOTIVOLOG", Session("IdUsuario"))
            cbMotivo.Items.Clear()

            For Each fila As DataRow In datasetAux.Tables(0).Rows()
                cbMotivo.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarSubMotivos()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "idSm"
                    id.Caption = "Id"
                    id.VisibleIndex = "0"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.SetColVisible(False)
                    id.Name = "idsm"
                    gridSubmotivo.Columns.Add(id)
                    Dim idmot As New GridViewDataColumn()
                    idmot.FieldName = "idmot"
                    idmot.Caption = " IdMotivo"
                    idmot.VisibleIndex = "1"
                    idmot.CellStyle.Font.Size = "10"
                    idmot.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    idmot.CellStyle.Border.BorderStyle = BorderStyle.None
                    idmot.SetColVisible(False)
                    idmot.Name = "idmotivo"
                    gridSubmotivo.Columns.Add(idmot)
                    Dim mot As New GridViewDataColumn()
                    mot.FieldName = "mot"
                    mot.Caption = "Motivo"
                    mot.VisibleIndex = "2"
                    mot.CellStyle.Font.Size = "10"
                    mot.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    mot.CellStyle.Border.BorderStyle = BorderStyle.None
                    mot.Name = "motivo"
                    gridSubmotivo.Columns.Add(mot)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "datasm"
                    data.Caption = "Submotivo"
                    data.VisibleIndex = "3"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "nombreSm"
                    gridSubmotivo.Columns.Add(data)
                End If

                If (Not IsPostBack) Then
                    gridSubmotivo.DataBind()
                Else
                    gridSubmotivo.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridSubmotivo_DataBinding(sender As Object, e As EventArgs)
        gridSubmotivo.DataSource = GetTableGridSubMotivos()
    End Sub

    Private Function GetTableGridSubMotivos() As DataTable
        Dim table As DataTable
        Dim miLista As String()
        Dim miLista2 As String()
        Dim SubMotivos As New Object()

        SubMotivos = BD.spLogisticaEntidadSubMotivoListar(CInt(Session("IdUsuario"))).ToList
        table = TryCast(SubMotivos, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("idSm", GetType(String))
            table.Columns.Add("idmot", GetType(String))
            table.Columns.Add("mot", GetType(String))
            table.Columns.Add("datasm", GetType(String))

            For Each SubMotivo In CType(SubMotivos, IEnumerable(Of Object))
                miLista = SubMotivo.Dato.ToString.Split("_")
                miLista2 = SubMotivo.SubMotivo.ToString.Split("-")
                table.Rows.Add(miLista(1), miLista(0), miLista2(0).Trim, miLista2(1).Trim)
            Next

            Session("TableSubMotivos") = table
        End If

        Return table
    End Function

    Protected Sub gridSubmotivo_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("NUEVO")) Then
            Try
                Dim smotivo As String = txSubmotivo.Text
                BD.spLogisticaEntidadSubMotivoIngresar(CInt(cbMotivo.SelectedItem.Value), smotivo.ToUpper, CInt(Session("IdUsuario")))

                texto = "Submotivo :: Ingresado con éxito."
                Funciones.EjecutarFuncionJavascript(Me, "showNotification('" + texto + "','S');")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            gridSubmotivo.DataBind()
        Else
            gridSubmotivo.DataBind()
        End If

    End Sub

    Protected Sub eliminar2_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL23") Is Nothing Then
                Session("baseURL23") = "DeleteSubmotivo.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "idSm")
            Dim grupoId2 As String = DataBinder.Eval(parent.DataItem, "idmot")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "datasm")

            Dim message As String = "¿Desea eliminar el submotivo " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&myId2={2}&message={3}", Session("baseURL23"), grupoId, grupoId2, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage2('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub


#End Region

#Region "Categoria"
    Private Sub LlenarCategoria()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "idC"
                    id.Caption = "Id"
                    id.VisibleIndex = "0"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.SetColVisible(False)
                    id.Name = "idc"
                    gridCategoria.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "dataC"
                    data.Caption = "Categoría"
                    data.VisibleIndex = "1"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "nombreC"
                    gridCategoria.Columns.Add(data)
                End If

                If (Not IsPostBack) Then
                    gridCategoria.DataBind()
                Else
                    gridCategoria.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridCategoria_DataBinding(sender As Object, e As EventArgs)
        gridCategoria.DataSource = GetTableGridCategorias()
    End Sub

    Private Function GetTableGridCategorias() As DataTable
        Dim table As DataTable
        Dim miLista As String()
        Dim Categoria As New Object()

        Categoria = BD.spLogisticaEntidadCategoriaListar(CInt(Session("IdUsuario")), 0).ToList
        table = TryCast(Categoria, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("idC", GetType(String))
            table.Columns.Add("dataC", GetType(String))

            For Each Categoria In CType(Categoria, IEnumerable(Of Object))
                miLista = Categoria.Dato.ToString.Split("_")
                table.Rows.Add(miLista(0), Categoria.Categoria.ToString)
            Next

            Session("TableCategorias") = table
        End If

        Return table
    End Function

    Protected Sub gridCategoria_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("NUEVO")) Then
            Try
                Dim categoria As String = txCategoria.Text
                BD.spLogisticaEntidadCategoriaIngresar(CInt(Session("IdUsuario")), categoria.ToUpper)

                texto = "Categoria :: Ingresada con éxito."
                Funciones.EjecutarFuncionJavascript(Me, "showNotification('" + texto + "','S');")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            gridCategoria.DataBind()
        Else
            gridCategoria.DataBind()
        End If
    End Sub

    Protected Sub eliminar3_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL24") Is Nothing Then
                Session("baseURL24") = "DeleteCategoria.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "idC")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "dataC")

            Dim message As String = "¿Desea eliminar la categoría " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL24"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage3('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub cbCategoria_Callback(sender As Object, e As CallbackEventArgsBase)
        Try
            objUsuario = New Usuario(Me, Session("PaisLogin"))
            datasetAux2 = objUsuario.LlenarCombo("CATEGORIALOG", Session("IdUsuario"))
            cbCategoria.Items.Clear()

            For Each fila2 As DataRow In datasetAux2.Tables(0).Rows()
                cbCategoria.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
            Next
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarSubCategorias()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "idSc"
                    id.Caption = "Id"
                    id.VisibleIndex = "0"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.SetColVisible(False)
                    id.Name = "idsc"
                    gridSubcategoria.Columns.Add(id)
                    Dim idcat As New GridViewDataColumn()
                    idcat.FieldName = "idcat"
                    idcat.Caption = " IdCategoria"
                    idcat.VisibleIndex = "1"
                    idcat.CellStyle.Font.Size = "10"
                    idcat.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    idcat.CellStyle.Border.BorderStyle = BorderStyle.None
                    idcat.SetColVisible(False)
                    idcat.Name = "idcategoria"
                    gridSubcategoria.Columns.Add(idcat)
                    Dim cat As New GridViewDataColumn()
                    cat.FieldName = "cat"
                    cat.Caption = "Categoria"
                    cat.VisibleIndex = "2"
                    cat.CellStyle.Font.Size = "10"
                    cat.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    cat.CellStyle.Border.BorderStyle = BorderStyle.None
                    cat.Name = "categoria"
                    gridSubcategoria.Columns.Add(cat)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "datasc"
                    data.Caption = "Subcategoria"
                    data.VisibleIndex = "3"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "nombreSc"
                    gridSubcategoria.Columns.Add(data)
                End If

                If (Not IsPostBack) Then
                    gridSubcategoria.DataBind()
                Else
                    gridSubcategoria.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridSubcategoria_DataBinding(sender As Object, e As EventArgs)
        gridSubcategoria.DataSource = GetTableGridSubCategorias()
    End Sub

    Private Function GetTableGridSubCategorias() As DataTable
        Dim table As DataTable
        Dim miLista As String()
        Dim miLista2 As String()
        Dim SubCategorias As New Object()

        SubCategorias = BD.spLogisticaEntidadSubCategoriaListar(CInt(Session("IdUsuario")), 0, 0).ToList
        table = TryCast(SubCategorias, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("idSc", GetType(String))
            table.Columns.Add("idcat", GetType(String))
            table.Columns.Add("cat", GetType(String))
            table.Columns.Add("datasc", GetType(String))

            For Each SubCategoria In CType(SubCategorias, IEnumerable(Of Object))
                miLista = SubCategoria.Dato.ToString.Split("_")
                miLista2 = SubCategoria.SubCategoria.ToString.Split("-")
                table.Rows.Add(miLista(1), miLista(0), miLista2(0).Trim, miLista2(1).Trim)
            Next

            Session("TableSubCategorias") = table
        End If

        Return table
    End Function

    Protected Sub gridSubcategoria_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("NUEVO")) Then
            Try
                Dim scategoria As String = txSubcategoria.Text
                BD.spLogisticaEntidadSubCategoriaIngresar(CInt(Session("IdUsuario")), CInt(cbCategoria.SelectedItem.Value), scategoria.ToUpper)

                texto = "Subcategoria :: Ingresada con éxito."
                Funciones.EjecutarFuncionJavascript(Me, "showNotification('" + texto + "','S');")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            gridSubcategoria.DataBind()
        Else
            gridSubcategoria.DataBind()
        End If
    End Sub

    Protected Sub eliminar4_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL25") Is Nothing Then
                Session("baseURL25") = "DeleteSubcategoria.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "idSc")
            Dim grupoId2 As String = DataBinder.Eval(parent.DataItem, "idcat")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "datasc")

            Dim message As String = "¿Desea eliminar la subcategoría " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&myId2={2}&message={3}", Session("baseURL25"), grupoId, grupoId2, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage4('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

#Region "TipoUbicacion"
    Private Sub LlenarTipoUbicacion()
        Try
            If (myflagV) Then
                If (Not IsPostBack) AndAlso (Not IsCallback) Then
                    Dim id As New GridViewDataColumn()
                    id.FieldName = "idT"
                    id.Caption = "Id"
                    id.VisibleIndex = "0"
                    id.CellStyle.Font.Size = "10"
                    id.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    id.CellStyle.Border.BorderStyle = BorderStyle.None
                    id.SetColVisible(False)
                    id.Name = "idt"
                    gridTipoUbicacion.Columns.Add(id)
                    Dim data As New GridViewDataColumn()
                    data.FieldName = "dataT"
                    data.Caption = "Tipo Ubicación"
                    data.VisibleIndex = "1"
                    data.CellStyle.Font.Size = "10"
                    data.HeaderStyle.Border.BorderStyle = BorderStyle.None
                    data.CellStyle.Border.BorderStyle = BorderStyle.None
                    data.Name = "nombreT"
                    gridTipoUbicacion.Columns.Add(data)
                End If

                If (Not IsPostBack) Then
                    gridTipoUbicacion.DataBind()
                Else
                    gridTipoUbicacion.DataBind()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridTipoUbicacion_DataBinding(sender As Object, e As EventArgs)
        gridTipoUbicacion.DataSource = GetTableGridTipoUbicacion()
    End Sub

    Private Function GetTableGridTipoUbicacion() As DataTable
        Dim table As DataTable
        Dim TipoUbicacion As New Object()

        TipoUbicacion = BD.spLogisticaEntidadTipoUbicacionListar(CInt(Session("IdUsuario"))).ToList
        table = TryCast(TipoUbicacion, DataTable)
        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("idT", GetType(String))
            table.Columns.Add("dataT", GetType(String))

            For Each TipoUbi In CType(TipoUbicacion, IEnumerable(Of Object))
                table.Rows.Add(TipoUbi.Dato.ToString, TipoUbi.TipoUbicacion.ToString)
            Next

            Session("TableTipoUbicacion") = table
        End If

        Return table
    End Function

    Protected Sub gridTipoUbicacion_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        If (e.Parameters.Equals("NUEVO")) Then
            Try
                Dim tipoubi As String = txTipoUbicacion.Text
                BD.spLogisticaEntidadTipoUbicacionIngresar(CInt(Session("IdUsuario")), tipoubi.ToUpper)

                texto = "Tipo Ubicación :: Ingresado con éxito."
                Funciones.EjecutarFuncionJavascript(Me, "showNotification('" + texto + "','S');")
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            gridTipoUbicacion.DataBind()
        Else
            gridTipoUbicacion.DataBind()
        End If
    End Sub

    Protected Sub eliminar5_Init(sender As Object, e As EventArgs)
        Try
            Dim btn As ASPxButton = TryCast(sender, ASPxButton)
            Dim parent As GridViewDataItemTemplateContainer = btn.NamingContainer

            If Session("baseURL26") Is Nothing Then
                Session("baseURL26") = "DeleteTipoUbicacion.aspx"
            End If

            Dim grupoId As String = DataBinder.Eval(parent.DataItem, "idT")
            Dim grupoData As String = DataBinder.Eval(parent.DataItem, "dataT")

            Dim message As String = "¿Desea eliminar el tipo de ubicación " + grupoData + "?"

            Dim contentUrl As String = String.Format("{0}?myId={1}&message={2}", Session("baseURL26"), grupoId, message)
            btn.ClientSideEvents.Click = String.Format("function(s, e) {{ ShowMessage5('{0}'); }}", contentUrl)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

    Protected Sub gridMotivo_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        Try
            Update(e.NewValues, "M")
            CancelEditingM(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridMotivo_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues, "M")
            CancelEditingM(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridMotivo_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Protected Sub gridSubmotivo_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues, "SM")
            CancelEditingSM(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridSubmotivo_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Protected Sub gridCategoria_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        Try
            Update(e.NewValues, "C")
            CancelEditingC(e)
            'If (Not IsPostBack) Then
            '    Update(e.NewValues, "C")
            '    CancelEditingC(e)
            'End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Protected Sub gridCategoria_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues, "C")
            CancelEditingC(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridCategoria_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Protected Sub gridSubcategoria_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues, "SC")
            CancelEditingSC(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridSubcategoria_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Protected Sub gridTipoUbicacion_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        Try
            Update(e.NewValues, "T")
            CancelEditingT(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridTipoUbicacion_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        Try
            Dim key As Integer
            For Each args In e.Keys.Values
                key = args
            Next args

            Update2(key, e.NewValues, "T")
            CancelEditingT(e)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub gridTipoUbicacion_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs)

    End Sub

    Private Sub Update(ByVal newValues As OrderedDictionary, ByVal tipo As String)
        Dim id, id2, nom As String

        Try
            'id = keys
            nom = newValues.Values(0)
            If (tipo.Equals("C")) Then
                BD.spLogisticaEntidadCategoriaIngresar(CInt(Session("IdUsuario")), nom)
            ElseIf (tipo.Equals("SC")) Then
                id2 = newValues.Values(1)
                BD.spLogisticaEntidadSubCategoriaIngresar(CInt(Session("IdUsuario")), CInt(id), nom)
            ElseIf (tipo.Equals("M")) Then
                BD.spLogisticaMotivoIngresar(CInt(Session("IdUsuario")), nom)
            ElseIf (tipo.Equals("SM")) Then
                id2 = newValues.Values(1)
                BD.spLogisticaEntidadSubMotivoIngresar(CInt(Session("IdUsuario")), CInt(id), nom)
            ElseIf (tipo.Equals("T")) Then
                BD.spLogisticaEntidadTipoUbicacionIngresar(CInt(Session("IdUsuario")), nom)
            End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub Update2(ByVal keys As Integer, ByVal newValues As OrderedDictionary, ByVal tipo As String)
        Dim id, id2, nom As String

        Try
            id = keys
            nom = newValues.Values(0)
            If (tipo.Equals("C")) Then
                BD.spLogisticaEntidadCategoriaActualizar(CInt(Session("IdUsuario")), CInt(id), nom)
            ElseIf (tipo.Equals("SC")) Then
                id2 = newValues.Values(1)
                BD.spLogisticaEntidadSubCategoriaActualizar(CInt(Session("IdUsuario")), CInt(id), CInt(id2), nom)
            ElseIf (tipo.Equals("M")) Then
                BD.spLogisticaMotivoActualizar(CInt(Session("IdUsuario")), CInt(id), nom)
            ElseIf (tipo.Equals("SM")) Then
                id2 = newValues.Values(1)
                BD.spLogisticaEntidadSubMotivoActualizar(CInt(Session("IdUsuario")), CInt(id), CInt(id2), nom)
            ElseIf (tipo.Equals("T")) Then
                BD.spLogisticaEntidadTipoUbicacionActualizar(CInt(Session("IdUsuario")), CInt(id), nom)
            End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub CancelEditingM(e As CancelEventArgs)
        e.Cancel = True
        gridMotivo.CancelEdit()
    End Sub

    Private Sub CancelEditingSM(e As CancelEventArgs)
        e.Cancel = True
        gridSubmotivo.CancelEdit()
    End Sub
    Private Sub CancelEditingC(e As CancelEventArgs)
        e.Cancel = True
        gridCategoria.CancelEdit()
    End Sub

    Private Sub CancelEditingSC(e As CancelEventArgs)
        e.Cancel = True
        gridSubcategoria.CancelEdit()
    End Sub

    Private Sub CancelEditingT(e As CancelEventArgs)
        e.Cancel = True
        gridTipoUbicacion.CancelEdit()
    End Sub

End Class
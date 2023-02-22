Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Reflection
Imports DevExpress.Web.Rendering
Imports DevExpress.Web
Imports DevExpress.Web.ASPxTreeList
Imports System.Web.DynamicData
Imports System.Collections.Generic
Imports DevExpress.Data
Imports System.Drawing
Imports ArtemisAdmin.FormAlerta
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Net
Imports DevExpress.XtraRichEdit.Commands
Imports DevExpress.DataAccess.Native.EntityFramework
Imports DevExpress.Data.Helpers
Imports ArtemisAdmin.Geocerca

Public Class FormActAlerta
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private asignar As String = ""
    Private param As String = ""
    Private idAlerta As Integer = 0
    Private oAlerta As Alerta
    Private oActivoAlerta As ActivoAlerta
    Private oEntidadAlerta As EntidadAlerta
    Private oGrupoAlerta As GrupoAlerta
    Private tableAux As DataSet
    Private tableAux1 As DataSet
    Private tableAux2 As DataSet
    Private tableAux3 As DataSet
    Private tableAux4 As DataSet
    Private tableAux5 As DataSet
    Private tableAux6 As DataSet
    Private datasetUser As DataSet
    Private tableAlert As DataTable = New DataTable()
    Private tableAlertD As DataTable = New DataTable()
    Private miLista1 As New List(Of String)()
    Private miLista2 As New List(Of String)()
    Private miLista3 As New List(Of String)()
    Private MaxEntidadAlertaSMS As Integer
    Private MaxEntidadAlertaEmail As Integer

    Private BD As New OdinDataContext()

    Public Class Unidades
        Public Sub New()
        End Sub
        Public Property Id As Integer
        Public Property ParentId As Integer
        Public Property Titulo As String
        Public Property Nodo As IList(Of Unidades)
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

            oAlerta = New Alerta(Me, Session("PaisLogin"))
            MaxEntidadAlertaEmail = AppSettings("MaxEntidadAlertaEmail")
            MaxEntidadAlertaSMS = AppSettings("MaxEntidadAlertaSMS")
            hdregs.Value = 0

            tableAlert = New DataTable
            tableAlert.Columns.AddRange(New DataColumn() {New DataColumn("id", GetType(String)), New DataColumn("ide", GetType(String)), New DataColumn("data", GetType(String)), New DataColumn("estado", GetType(String))})
            tableAlertD = New DataTable
            tableAlertD.Columns.AddRange(New DataColumn() {New DataColumn("id", GetType(String)), New DataColumn("ide", GetType(String)), New DataColumn("data", GetType(String)), New DataColumn("estado", GetType(String))})

            If Request.QueryString("data").Equals("nueva") Then
                Dim grp As LayoutItemBase = layoutForm.FindItemOrGroupByName("LyGrp")
                grp.Caption = "Crear Alerta"

                If Not IsPostBack Then
                    LlenarCombo()
                    LlenarAgrupacion()
                    LlenarArbol()
                End If
            Else
                Dim grp As LayoutItemBase = layoutForm.FindItemOrGroupByName("LyGrp")
                grp.Caption = "Editar Alerta"

                tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")), Request.QueryString("id").ToString)

                For Each fila As DataRow In tableAux.Tables(0).Rows()
                    If (fila(1) = 1) Then
                        Funciones.EjecutarFuncionJavascript(Me, "onVisorSelect('" + fila(1).ToString + "');")
                    ElseIf (fila(1) = 3 Or fila(1) = 4) Then
                        Funciones.EjecutarFuncionJavascript(Me, "onVisorSelect('" + fila(1).ToString + "');")
                    ElseIf (fila(1) = 2) Then
                        Funciones.EjecutarFuncionJavascript(Me, "onVisorSelect('" + fila(1).ToString + "');")
                    ElseIf (fila(1) = 6) Then
                        Funciones.EjecutarFuncionJavascript(Me, "onVisorSelect('" + fila(1).ToString + "');")
                    End If
                    If Not IsPostBack Then
                        LlenarDatos()
                        LlenarCombo2(fila(0))
                        LlenarAgrupacion2(fila(0))
                        LlenarArbol()
                    End If
                    idAlerta = CInt(fila(0))
                Next

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            If IsPostBack Then
                LlenarArbol()
            End If
        End Try

    End Sub

    Private Sub LlenarDatos()
        tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")), Request.QueryString("id").ToString)
        For Each fila As DataRow In tableAux.Tables(0).Rows()
            tbNombre.Text = fila(7).ToString
        Next
    End Sub

    Private Sub LlenarCombo()
        Try
            oAlerta = New Alerta(Me, Session("PaisLogin"))

            'COMBOS
            tableAux1 = oAlerta.LlenarCombo("TipoAlerta")
            tableAux2 = oAlerta.LlenarCombo("Sonido")
            tableAux3 = oAlerta.ConsultarSucesos()
            tableAux4 = oAlerta.LlenarCombo("CAMPOVALORCONTROL")
            tableAux5 = oAlerta.LlenarCombo("TIPOCONTROL")
            tableAux6 = oAlerta.LlenarCombo("FORMACONTROL")

            cbAlerta.Items.Clear()
            cbSonido.Items.Clear()
            cbSucesos.Items.Clear()
            cbEvaluar.Items.Clear()
            cbTipo.Items.Clear()
            cbForma.Items.Clear()

            For Each fila As DataRow In tableAux1.Tables(0).Rows()
                cbAlerta.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next

            For Each fila As DataRow In tableAux2.Tables(0).Rows()
                cbSonido.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
                Dim lstitem As ListEditItem = cbSonido.Items.FindByValue("1")
                If (lstitem IsNot Nothing) Then
                    lstitem.Selected = True
                End If
            Next

            For Each fila As DataRow In tableAux3.Tables(0).Rows()
                cbSucesos.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next

            For Each fila As DataRow In tableAux4.Tables(0).Rows()
                cbEvaluar.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next

            For Each fila As DataRow In tableAux5.Tables(0).Rows()
                cbTipo.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next

            For Each fila As DataRow In tableAux6.Tables(0).Rows()
                cbForma.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarCombo2(ByVal codigo As String)
        Try
            oAlerta = New Alerta(Me, Session("PaisLogin"))
            oActivoAlerta = New ActivoAlerta(Me, Session("PaisLogin"))
            oGrupoAlerta = New GrupoAlerta(Me, Session("PaisLogin"))
            oEntidadAlerta = New EntidadAlerta(Me, Session("PaisLogin"))

            tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")), codigo)

            'COMBOS
            tableAux1 = oAlerta.LlenarCombo("TipoAlerta")
            tableAux2 = oAlerta.LlenarCombo("Sonido")
            tableAux3 = oAlerta.ConsultarSucesos()
            tableAux4 = oAlerta.LlenarCombo("CAMPOVALORCONTROL")
            tableAux5 = oAlerta.LlenarCombo("TIPOCONTROL")
            tableAux6 = oAlerta.LlenarCombo("FORMACONTROL")

            cbAlerta.Items.Clear()
            cbSonido.Items.Clear()
            cbSucesos.Items.Clear()
            cbEvaluar.Items.Clear()
            cbTipo.Items.Clear()
            cbForma.Items.Clear()

            For Each fila As DataRow In tableAux.Tables(0).Rows()
                For Each fila2 As DataRow In tableAux1.Tables(0).Rows()
                    cbAlerta.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                    Dim lstitem As ListEditItem = cbAlerta.Items.FindByValue(fila(1).ToString)
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                Next

                For Each fila2 As DataRow In tableAux2.Tables(0).Rows()
                    cbSonido.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                    Dim lstitem As ListEditItem = cbSonido.Items.FindByValue(fila(37).ToString)
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                Next

                For Each fila2 As DataRow In tableAux3.Tables(0).Rows()
                    cbSucesos.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                    Dim lstitem As ListEditItem = cbSucesos.Items.FindByValue(fila(39).ToString)
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                Next

                For Each fila2 As DataRow In tableAux4.Tables(0).Rows()
                    cbEvaluar.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                Next

                For Each fila2 As DataRow In tableAux5.Tables(0).Rows()
                    cbTipo.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                Next

                For Each fila2 As DataRow In tableAux6.Tables(0).Rows()
                    cbForma.Items.Add(New ListEditItem(fila2(1).ToString, fila2(0).ToString))
                Next

                If (fila(12).ToString.Equals("")) And (fila(13).ToString.Equals("")) Then
                    chkDia.Checked = True
                Else
                    chkDia.Checked = False
                    Dim lstitem As ListEditItem = cbHoraDesde.Items.FindByValue(fila(12).ToString.Substring(0, 2))
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                    Dim lstitem2 As ListEditItem = cbMinutoDesde.Items.FindByValue(fila(12).ToString.Substring(3, 2))
                    If (lstitem2 IsNot Nothing) Then
                        lstitem2.Selected = True
                    End If

                    Dim lstitem3 As ListEditItem = cbHoraHasta.Items.FindByValue(fila(13).ToString.Substring(0, 2))
                    If (lstitem3 IsNot Nothing) Then
                        lstitem3.Selected = True
                    End If
                    Dim lstitem4 As ListEditItem = cbMinutoHasta.Items.FindByValue(fila(13).ToString.Substring(3, 2))
                    If (lstitem4 IsNot Nothing) Then
                        lstitem4.Selected = True
                    End If
                End If

                If (fila(1) = 2) Then
                    tbAlertar.Text = fila(10).ToString
                    tbPrealertar.Text = fila(11).ToString
                End If

                If (fila(1) = 6) Then
                    datasetUser = oAlerta.ConfUsuarioConsultar2(CInt(Session("IdUsuario")))
                    For Each fila2 As DataRow In datasetUser.Tables(0).Rows()
                        param = fila2(4).ToString
                    Next

                    Select Case param
                        Case "Nm/H"
                            tbLimite.Text = Funciones.MillasAMillasNauticas(fila(14).ToString)
                        Case "Km/H"
                            tbLimite.Text = Funciones.MillasAKilometros(fila(14).ToString)
                        Case "Mi/H"
                            tbLimite.Text = fila(14).ToString
                    End Select

                    'tbLimite.Text = fila(14).ToString

                    chkDentro.Checked = IIf(fila(15), True, False)
                End If

                Dim dataUni As DataSet = oActivoAlerta.Consultar(codigo)
                Dim dataGrp As DataSet = oGrupoAlerta.Consultar(codigo)
                Dim dataEnt As DataSet = oEntidadAlerta.Consultar(codigo)
                Dim celulares As String = ""
                Dim emails As String = ""

                For Each uni As DataRow In dataUni.Tables(0).Rows()
                    If (uni(4).ToString.Length > 0) Then
                        celulares = uni(4) + "," + vbCrLf
                    End If
                Next
                For Each grp As DataRow In dataGrp.Tables(0).Rows()
                    If (grp(5).ToString.Length > 0) Then
                        celulares = celulares + grp(5) + "," + vbCrLf
                    End If
                Next
                For Each ent As DataRow In dataEnt.Tables(0).Rows()
                    If (ent(3).ToString.Length > 0) Then
                        celulares += ent(3)
                    End If
                Next

                For Each uni As DataRow In dataUni.Tables(0).Rows()
                    If (uni(3).ToString.Length > 0) Then
                        emails = uni(3) + "," + vbCrLf
                    End If
                Next
                For Each grp As DataRow In dataGrp.Tables(0).Rows()
                    If (grp(4).ToString.Length > 0) Then
                        emails = emails + grp(4) + "," + vbCrLf
                    End If
                Next
                For Each ent As DataRow In dataEnt.Tables(0).Rows()
                    If (ent(2).ToString.Length > 0) Then
                        emails += ent(2)
                    End If
                Next

                mnCelular.Text = celulares
                mnEmail.Text = emails

            Next

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarAgrupacion()
        Try
            Dim Geocercas = BD.spGeocercaConsultar(CInt(Session("IdUsuario"))).ToList
            Dim Grupos = BD.spGrupoGeocercaConsultar(CInt(Session("IdUsuario")), 0).ToList

            cbGeocercas.Items.Clear()
            cbGrupos.Items.Clear()

            For Each geo In CType(Geocercas, IEnumerable(Of Object))
                cbGeocercas.Items.Add(New ListEditItem(geo.Nombre.ToString, geo.IdGeocerca.ToString))
            Next

            For Each grp In CType(Grupos, IEnumerable(Of Object))
                cbGrupos.Items.Add(New ListEditItem(grp.Nombre.ToString, grp.IdGrupoGeocerca.ToString))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LlenarAgrupacion2(ByVal codigo As String)
        Try
            Dim Geocercas = BD.spGeocercaConsultar(CInt(Session("IdUsuario"))).ToList
            Dim Grupos = BD.spGrupoGeocercaConsultar(CInt(Session("IdUsuario")), 0).ToList

            oAlerta = New Alerta(Me, Session("PaisLogin"))
            tableAux = oAlerta.Consultar2(CInt(Session("IdUsuario")), codigo)

            cbGeocercas.Items.Clear()
            cbGrupos.Items.Clear()

            For Each fila As DataRow In tableAux.Tables(0).Rows()
                For Each geo In CType(Geocercas, IEnumerable(Of Object))
                    cbGeocercas.Items.Add(New ListEditItem(geo.Nombre.ToString, geo.IdGeocerca.ToString))
                    Dim lstitem As ListEditItem = cbGeocercas.Items.FindByValue(fila(8).ToString)
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                Next
            Next

            For Each fila As DataRow In tableAux.Tables(0).Rows()
                For Each grp In CType(Grupos, IEnumerable(Of Object))
                    cbGrupos.Items.Add(New ListEditItem(grp.Nombre.ToString, grp.IdGrupoGeocerca.ToString))
                    Dim lstitem As ListEditItem = cbGrupos.Items.FindByValue(fila(21).ToString)
                    If (lstitem IsNot Nothing) Then
                        lstitem.Selected = True
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LlenarArbol()
        Try
            If (Not IsPostBack) Then
                tlAsignacion.DataBind()
            Else
                tlAsignacion.DataBind()
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function InitTree() As DataTable
        Dim table As DataTable
        Dim Entidades As New Object()
        Dim Activos As New Object()
        Dim Grupos As New Object()
        Dim cont As Integer = 0

        If (Session("esUsuario")) Then
            Activos = BD.spListarActivosEntidad(Session("Entidades"), 0).ToList
            Entidades = BD.spUsuarioEntidadConsultar(CInt(Session("IdUsuario")), "127001").ToList
        Else
            Activos = BD.spListarActivosEntidadSubUsuario(Session("Entidades"), CInt(Session("IdSubUsuario")), CInt(Session("IdUsuario"))).ToList
            Entidades = BD.spUsuarioEntidadConsultarSubusuario(CInt(Session("IdUsuario")), "127001", CInt(Session("IdSubUsuario"))).ToList
        End If
        Grupos = BD.spGrupoConsultar(CInt(Session("IdUsuario")), "").ToList

        'table = TryCast(Grupos, DataTable)

        If table Is Nothing Then
            table = New DataTable()
            table.Columns.Add("Id", GetType(Integer))
            table.Columns.Add("ParentId", GetType(Integer))
            table.Columns.Add("Ide", GetType(String))
            table.Columns.Add("Data", GetType(String))

            table.Rows.Add(1, 0, "0", "Unidades")
            table.Rows.Add(2, 0, "0", "Grupos")
            table.Rows.Add(3, 0, "0", "Entidades")
            cont = 4

            For Each acti In CType(Activos, IEnumerable(Of Object))
                table.Rows.Add(cont, 1, acti.IdActivo, acti.Alias)
                cont += 1
            Next
            For Each grp In CType(Grupos, IEnumerable(Of Object))
                table.Rows.Add(cont, 2, grp.IdGrupo, grp.Grupo)
                cont += 1
            Next
            For Each entd In CType(Entidades, IEnumerable(Of Object))
                table.Rows.Add(cont, 3, entd.IdEntidad, entd.Nombre)
                cont += 1
            Next

            cont = 0
        End If

        Return table
    End Function

    Protected Sub tlAsignacion_DataBinding(sender As Object, e As EventArgs)
        tlAsignacion.DataSource = InitTree()
    End Sub

    Protected Sub cbForma_Callback(sender As Object, e As CallbackEventArgsBase)
        Try
            oAlerta = New Alerta(Me, Session("PaisLogin"))
            If (e.Parameter IsNot Nothing) Then
                tableAux6 = oAlerta.LlenarCombo("FORMACONTROL", e.Parameter.ToString)
                cbForma.Items.Clear()

                For Each fila As DataRow In tableAux6.Tables(0).Rows()
                    cbForma.Items.Add(New ListEditItem(fila(1).ToString, fila(0).ToString))
                Next
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            oAlerta = Nothing
            oAlerta = New Alerta(Me, Session("PaisLogin"))

            If tbNombre.Text = "" Then
                texto = "Debe ingresar un nombre de Alerta para continuar."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                Return
            End If

            If cbAlerta.Text = "" Then
                texto = "Debe de escoger un tipo de Alerta para continuar."
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                Return
            End If

            If Not chkDia.Checked Then
                If cbHoraDesde.Text <> "" And cbHoraHasta.Text <> "" And
                                        cbMinutoDesde.Text <> "" And cbMinutoHasta.Text <> "" Then
                    If CInt(cbHoraDesde.Text) > CInt(cbHoraHasta.Text) Then
                        texto = "Los intervalos de horas son inválidos (Hora desde mayor a Hora hasta)."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                        Return
                    End If
                End If
            End If

            If (cbAlerta.SelectedItem.Value.Equals("1")) Then
                If cbSucesos.Text = "" Then
                    texto = "Debe de escoger un suceso para continuar."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                    Return
                End If

            ElseIf (cbAlerta.SelectedItem.Value.Equals("3") Or cbAlerta.SelectedItem.Value.Equals("4")) Then
                If cbLicencia.Value.Equals("I") Then
                    If cbGeocercas.Text = "" Then
                        texto = "Debe de escoger una geocerca para continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                        Return
                    End If
                Else
                    If cbGrupos.Text = "" Then
                        texto = "Debe de escoger un grupo de geocerca para continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                        Return
                    End If
                End If

            ElseIf (cbAlerta.SelectedItem.Value.Equals("2")) Then
                If tbAlertar.Text <> "" And Not IsNumeric(tbAlertar.Text) Then
                    texto = "Valor de Kilometraje No Válido."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                    Return
                End If

                If tbPrealertar.Text <> "" And Not IsNumeric(tbPrealertar.Text) Then
                    texto = "Valor de Kilometraje No Válido."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                    Return
                End If

            ElseIf (cbAlerta.SelectedItem.Value.Equals("6")) Then
                If tbLimite.Text <> "" And Not IsNumeric(tbLimite.Text) Then
                    texto = "Valor de Limite de Velocidad No Válido"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                    Return
                End If

                If cbLicencia.Value.Equals("I") Then
                    If cbGeocercas.Text = "" Then
                        texto = "Debe de escoger una geocerca para continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                        Return
                    End If
                Else
                    If cbGrupos.Text = "" Then
                        texto = "Debe de escoger un grupo de geocerca para continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','I');")
                        Return
                    End If
                End If

            ElseIf (cbAlerta.SelectedItem.Value.Equals("1000")) Then

            ElseIf (cbAlerta.SelectedItem.Value.Equals("2000")) Then

            End If

            With oAlerta
                .idUsuario = Session("IdUsuario")
                .Usuario = Session("Usuario")

                .IdTipoAlerta = cbAlerta.Value
                .Nombre = tbNombre.Text.ToUpper
                .Descripcion = ""
                .IdAlerta = 0
                .IdDespacho = 0

                .LimiteVelocidad = IIf(tbLimite.Text = "", 0, tbLimite.Text)
                .IdSonido = cbSonido.Value

                Try
                    datasetUser = oAlerta.ConfUsuarioConsultar2(CInt(Session("IdUsuario")))
                    For Each fila As DataRow In datasetUser.Tables(0).Rows()
                        param = fila(4).ToString
                    Next

                    Select Case param
                        Case "Nm/H"
                            .LimiteVelocidad = Funciones.MillasNauticasAMillas(.LimiteVelocidad)
                        Case "Km/H"
                            .LimiteVelocidad = Funciones.KilometrosAMillas(.LimiteVelocidad)
                        Case "Mi/H"
                            .LimiteVelocidad = .LimiteVelocidad
                    End Select
                Catch ex As Exception
                    .LimiteVelocidad = .LimiteVelocidad
                End Try

                If Not chkDia.Checked Then
                    .HoraDesde = String.Format("{0}:{1}", cbHoraDesde.Text, cbMinutoDesde.Text)
                    If .HoraDesde = ":" Then
                        .HoraDesde = ""
                    End If

                    .HoraHasta = String.Format("{0}:{1}", cbHoraHasta.Text, cbMinutoHasta.Text)
                    If .HoraHasta = ":" Then
                        .HoraHasta = ""
                    End If
                Else
                    .HoraDesde = Nothing
                    .HoraHasta = Nothing
                End If

                If cbSucesos.Value = "" Then
                    .IdSuceso = 0
                Else
                    .IdSuceso = cbSucesos.Value
                End If

                If cbGeocercas.Value = "" Then
                    .IdGeocerca = 0
                Else
                    .IdGeocerca = cbGeocercas.Value
                End If

                If cbGrupos.Value = "" Then
                    .idGrupoGeocerca = 0
                Else
                    .idGrupoGeocerca = cbGrupos.Value
                End If

                .DentroGeo = chkDentro.Checked

                If tbPrealertar.Text = "" Or tbPrealertar.Text = "0" Then
                    .Porcentaje = 0
                Else
                    .Porcentaje = (1 - (CDbl(tbPrealertar.Text) / CDbl(tbAlertar.Text))) * 100
                End If

                If tbAlertar.Text = "" Then
                    .Kilometraje = 0
                Else
                    .Kilometraje = tbAlertar.Text
                End If

                .Lunes = True
                .Martes = True
                .Miercoles = True
                .Jueves = True
                .Viernes = True
                .Sabado = True
                .Domingo = True

                'For Ind As Integer = 0 To tmpDias.Length - 1
                '    Try
                '        Select Case tmpDias(Ind)
                '            Case "Lunes"
                '                .Lunes = True
                '            Case "Martes"
                '                .Martes = True
                '            Case "Miercoles"
                '                .Miercoles = True
                '            Case "Jueves"
                '                .Jueves = True
                '            Case "Viernes"
                '                .Viernes = True
                '            Case "Sabado"
                '                .Sabado = True
                '            Case "Domingo"
                '                .Domingo = True
                '        End Select
                '    Catch ex As Exception
                '        Console.WriteLine(ex.Message)
                '    End Try
                'Next

                'Try
                '    .FechaCaducidad = dtCaducidad.Value
                'Catch ex As Exception
                '    .FechaCaducidad = ""
                'End Try

                Try
                    .CampoValorControl = cbEvaluar.Value
                Catch ex As Exception
                    .CampoValorControl = 0
                End Try

                Try
                    .FormaControl = cbForma.Value
                Catch ex As Exception
                    .FormaControl = 0
                End Try

                Try
                    .TipoControl = cbTipo.Value
                Catch ex As Exception
                    .TipoControl = 0
                End Try

                Try
                    If tbLimiteSup.Text = "" Then
                        .ParametroControl1 = 0
                    Else
                        .ParametroControl1 = tbLimiteSup.Text
                    End If
                Catch ex As Exception
                    .ParametroControl1 = 0
                End Try

                Try
                    If tbLimiteInf.Text = "" Then
                        .ParametroControl2 = 0
                    Else
                        .ParametroControl2 = tbLimiteInf.Text
                    End If
                Catch ex As Exception
                    .ParametroControl1 = 0
                End Try

                Dim alertaID As Integer
                If Request.QueryString("data").Equals("nueva") Then
                    alertaID = oAlerta.InsertarAlerta()
                Else
                    .IdAlerta = idAlerta
                    oAlerta.Ejecutar2("MOD")
                End If

                'Try
                '    idAlerta = BD.getLastD_AlertaUsuario(Session("IdUsuario"))
                'Catch ex As Exception
                '    idAlerta = 0
                'End Try

                If alertaID > 0 And Not IsNothing(Session("IdSubUsuario")) Then
                    BD.spD_AlertaSubUsuarioIngresar(idAlerta, CInt(Session("IdUsuario")), CInt(Session("idSubUsuario")))
                End If

                If alertaID > 0 Then
                    asignarAlertas(alertaID)
                    texto = "Alerta Ingresada con Exito."
                Else
                    asignarAlertas(.IdAlerta)
                    texto = "Alerta Actualizada con Exito."
                End If

                Dim startUpScript As String = String.Format("window.parent.hideAlerta();")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
                Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S');")
                hdregs.Value = 1
            End With

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub asignarAlertas(ByVal alerta As Integer)
        Dim Emails As String()
        Dim Celulares As String()
        Dim contar As Integer = 0

        'Revision de Celulares
        If mnCelular.Text Like "*,*" Then
            Dim IndCelular As String
            Celulares = mnCelular.Text.Split(",")

            If AppSettings("RestriccionSMS") Then
                If Celulares.Length > MaxEntidadAlertaSMS Then
                    texto = String.Format("Debido a restricciones por parte del Proveedor, solo se permiten ingresar hasta {0} celular(es) diferentes por cada alerta definida. Por favor elimine {1} número(s) para poder continuar.", MaxEntidadAlertaSMS, Abs(Celulares.Length - MaxEntidadAlertaSMS))
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            End If

            For ind = 0 To UBound(Celulares)
                If Not IsNumeric(Celulares(ind).Trim()) Or Celulares(ind).Length < CInt(Session("maxlSMS")) Or Celulares(ind).Length > (CInt(Session("maxlSMS")) * 3) Then
                    texto = String.Format("Celular {0} no válido, son válidos Ej: 0994000111 y si es más de uno 0994222111, 0994111222", Celulares(ind))
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            Next

            IndCelular = Nothing
        Else
            If mnCelular.Text <> "" Then
                'If Not IsNumeric(tbCelular.Text) Or tbCelular.Text.Length < CInt(Session("maxlSMS")) Or tbCelular.Text.Length > (CInt(Session("maxlSMS")) * 3) Then
                If Not IsNumeric(mnCelular.Text) Then
                    texto = String.Format("Celular no Válido, son válidos Ej: 0994000111 y si es más de uno 0994222111, 0994111222")
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            End If
        End If

        'Revision de Emails
        If mnEmail.Text Like "*,*" Then
            Emails = mnEmail.Text.Split(",")

            Dim IndEmails As String
            Emails = mnEmail.Text.Split(",")

            If AppSettings("RestriccionSMS") Then
                If Emails.Length > MaxEntidadAlertaEmail Then
                    texto = String.Format("Debido a restricciones por parte del Proveedor, sólo se permiten ingresar hasta {0} emails por cada alerta definida", MaxEntidadAlertaEmail)
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            End If

            For ind = 0 To UBound(Emails)
                If Not Funciones.validar_Mail(Emails(ind).Trim()) Then
                    texto = String.Format("Email no válido {0}. Emails validos Ej: abc@hotmail.com y si es más de uno abd@hotmail.com, abc@gmail.com", Emails(ind))
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            Next

            IndEmails = Nothing
        Else
            If mnEmail.Text <> "" Then
                If Not Funciones.validar_Mail(mnEmail.Text) Then
                    texto = "Email no válido. Emails válidos Ej: abc@hotmail.com y es más de uno abd@hotmail.com, abc@gmail.com"
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','E');")
                    Return
                End If
            End If
        End If

        'Asignaciones
        oActivoAlerta = New ActivoAlerta(Me, Session("PaisLogin"))
        oGrupoAlerta = New GrupoAlerta(Me, Session("PaisLogin"))
        oEntidadAlerta = New EntidadAlerta(Me, Session("PaisLogin"))
        tableAlert = Session("TableAsignar")
        tableAlertD = Session("TableAsignarD")
        asignar = Session("OpcionAsignar")
        Try
            If Not IsNothing(tableAlert) Then
                For Each row As DataRow In tableAlert.Rows
                    If (row("estado").Equals("U")) Then
                        With oActivoAlerta
                            .IdActivo = CInt(row("ide"))
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CInt(row("ide")), alerta)
                            If contar = 0 Then
                                .Ejecutar("ING")
                            End If
                        End With
                    ElseIf (row("estado").Equals("G")) Then
                        With oGrupoAlerta
                            .IdGrupo = CInt(row("ide"))
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CInt(row("ide")), alerta)
                            If contar = 0 Then
                                .Ejecutar("ING")
                            End If
                        End With
                    ElseIf (row("estado").Equals("E")) Then
                        With oEntidadAlerta
                            .IdEntidad = row("ide")
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CStr(row("ide")), alerta)
                            If contar = 0 Then
                                .Ejecutar("ING")
                            End If
                        End With
                    End If
                Next

            End If

            If Not IsNothing(tableAlertD) Then
                For Each row As DataRow In tableAlertD.Rows
                    If (row("estado").Equals("U")) Then
                        With oActivoAlerta
                            .IdActivo = CInt(row("ide"))
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CInt(row("ide")), alerta)
                            If contar = 1 Then
                                .Ejecutar("ELM")
                            End If
                        End With
                    ElseIf (row("estado").Equals("G")) Then
                        With oGrupoAlerta
                            .IdGrupo = CInt(row("ide"))
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CInt(row("ide")), alerta)
                            If contar = 1 Then
                                .Ejecutar("ELM")
                            End If
                        End With
                    ElseIf (row("estado").Equals("E")) Then
                        With oEntidadAlerta
                            .IdEntidad = row("ide")
                            .IdAlerta = alerta
                            .SMS = mnCelular.Text.Trim
                            .Email = mnEmail.Text.Trim

                            contar = .Consultar2(CStr(row("ide")), alerta)
                            If contar = 1 Then
                                .Ejecutar("ELM")
                            End If
                        End With
                    End If
                Next

            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub tlAsignacion_SelectionChanged(sender As Object, e As EventArgs)
        Dim tree As ASPxTreeList = TryCast(sender, ASPxTreeList)
        Dim id, idprt As Integer
        Dim indice, objeto, estado As String

        For Each it As TreeListNode In tree.Nodes
            If (it.Item("Id") = 1) Then
                For x1 = 0 To tree.Nodes(0).ChildNodes.Count - 1
                    id = tree.Nodes(0).ChildNodes(x1).Item("Id")
                    idprt = tree.Nodes(0).ChildNodes(x1).Item("ParentId")
                    indice = tree.Nodes(0).ChildNodes(x1).Item("Ide")
                    objeto = tree.Nodes(0).ChildNodes(x1).Item("Data")

                    If (tree.Nodes(0).ChildNodes(x1).Selected) Then
                        tableAlert.Rows.Add(id, indice, objeto, "U")
                    Else
                        tableAlertD.Rows.Add(id, indice, objeto, "U")
                    End If
                Next
            ElseIf (it.Item("Id") = 2) Then
                For x2 = 0 To tree.Nodes(1).ChildNodes.Count - 1
                    id = tree.Nodes(1).ChildNodes(x2).Item("Id")
                    idprt = tree.Nodes(1).ChildNodes(x2).Item("ParentId")
                    indice = tree.Nodes(1).ChildNodes(x2).Item("Ide")
                    objeto = tree.Nodes(1).ChildNodes(x2).Item("Data")

                    If (tree.Nodes(1).ChildNodes(x2).Selected) Then
                        tableAlert.Rows.Add(id, indice, objeto, "G")
                    Else
                        tableAlertD.Rows.Add(id, indice, objeto, "G")
                    End If
                Next
            ElseIf (it.Item("Id") = 3) Then
                For x3 = 0 To tree.Nodes(2).ChildNodes.Count - 1
                    id = tree.Nodes(2).ChildNodes(x3).Item("Id")
                    idprt = tree.Nodes(2).ChildNodes(x3).Item("ParentId")
                    indice = tree.Nodes(2).ChildNodes(x3).Item("Ide")
                    objeto = tree.Nodes(2).ChildNodes(x3).Item("Data")

                    If (tree.Nodes(2).ChildNodes(x3).Selected) Then
                        tableAlert.Rows.Add(id, indice, objeto, "E")
                    Else
                        tableAlertD.Rows.Add(id, indice, objeto, "E")
                    End If
                Next
            End If
        Next

        'Dim nodes As List(Of TreeListNode) = tree.GetSelectedNodes()
        'Dim node As TreeListNode
        'For Each node In nodes
        '    If (node.Selected) Then
        '        id = node.Item("Id")
        '        idprt = node.Item("ParentId")
        '        indice = node.Item("Ide")
        '        objeto = node.Item("Data")
        '        estado = ""
        '        If (idprt = 0) Then
        '            listarNodos(id, tree)
        '        Else
        '            Select Case idprt
        '                Case 1
        '                    tableAlert.Rows.Add(id, indice, objeto, "U")
        '                Case 2
        '                    tableAlert.Rows.Add(id, indice, objeto, "G")
        '                Case 3
        '                    tableAlert.Rows.Add(id, indice, objeto, "E")
        '            End Select
        '        End If
        '    End If
        'Next
        Session("TableAsignar") = tableAlert
        Session("TableAsignarD") = tableAlertD
    End Sub

    Sub listarNodos(ident As Integer, tree As ASPxTreeList)
        Try
            Dim Activos As New Object()
            Dim id, indice, objeto As String

            If (ident = 1) Then
                Dim x As Integer = 0
                For x = 0 To tree.Nodes(0).ChildNodes.Count - 1 Step x + 1
                    id = tree.Nodes(0).ChildNodes(x).Item("Id")
                    indice = tree.Nodes(0).ChildNodes(x).Item("Ide")
                    objeto = tree.Nodes(0).ChildNodes(x).Item("Data")

                    tableAlert.Rows.Add(id, indice, objeto, "U")
                Next
                asignar = "U"
            ElseIf (ident = 2) Then
                Dim x As Integer = 0
                For x = 0 To tree.Nodes(1).ChildNodes.Count - 1 Step x + 1
                    id = tree.Nodes(1).ChildNodes(x).Item("Id")
                    indice = tree.Nodes(1).ChildNodes(x).Item("Ide")
                    objeto = tree.Nodes(1).ChildNodes(x).Item("Data")

                    tableAlert.Rows.Add(id, indice, objeto, "G")
                Next
                asignar = "G"
            ElseIf (ident = 3) Then
                Dim x As Integer = 0
                For x = 0 To tree.Nodes(2).ChildNodes.Count - 1 Step x + 1
                    id = tree.Nodes(2).ChildNodes(x).Item("Id")
                    indice = tree.Nodes(2).ChildNodes(x).Item("Ide")
                    objeto = tree.Nodes(2).ChildNodes(x).Item("Data")

                    tableAlert.Rows.Add(id, indice, objeto, "E")
                Next
                asignar = "E"
            End If
            Session("OpcionAsignar") = asignar
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub cbRoot_Callback(source As Object, e As CallbackEventArgs)
        Try
            Dim miLista As New List(Of String)()
            If e.Parameter.Contains("OKU") Then
                Dim listaAux As String() = e.Parameter.ToString.Split(",")
                If (Not listaAux(0).Equals("UNIDADES")) Then
                    miLista.Add(listaAux(0))
                End If
            ElseIf e.Parameter.Contains("OKG") Then
                Dim listaAux As String() = e.Parameter.ToString.Split(",")
                If (Not listaAux(0).Equals("GRUPOS")) Then
                    miLista.Add(listaAux(0))
                End If
            ElseIf e.Parameter.Contains("OKE") Then
                Dim listaAux As String() = e.Parameter.ToString.Split(",")
                If (Not listaAux(0).Equals("ENTIDADES")) Then
                    miLista.Add(listaAux(0))
                End If
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub tlAsignacion_PreRender(sender As Object, e As EventArgs)
        Try
            If (hdregs.Value <> 1) Then
                Dim tree As ASPxTreeList = TryCast(sender, ASPxTreeList)
                oActivoAlerta = New ActivoAlerta(Me, Session("PaisLogin"))
                oGrupoAlerta = New GrupoAlerta(Me, Session("PaisLogin"))
                oEntidadAlerta = New EntidadAlerta(Me, Session("PaisLogin"))
                tree.ExpandAll()

                If Request.QueryString("data").Equals("nueva") Then
                    Dim node As TreeListNode = tree.FindNodeByKeyValue("3")
                    node.Selected = True
                Else
                    Dim dataUni As DataSet = oActivoAlerta.Consultar(Request.QueryString("id").ToString)
                    Dim dataGrp As DataSet = oGrupoAlerta.Consultar(Request.QueryString("id").ToString)
                    Dim dataEnt As DataSet = oEntidadAlerta.Consultar(Request.QueryString("id").ToString)

                    Dim nodes As List(Of TreeListNode) = tree.GetVisibleNodes
                    For Each node In nodes
                        Dim aux As Object = node.DataItem()
                        For Each fila As DataRow In dataUni.Tables(0).Rows()
                            If node.Item("Ide").Equals(CStr(fila(0))) Then
                                node = tree.FindNodeByFieldValue("Ide", CStr(fila(0)))
                                If (node IsNot Nothing) Then
                                    node.Selected = True
                                End If
                            End If
                        Next
                        For Each fila2 As DataRow In dataGrp.Tables(0).Rows()
                            If node.Item("Ide").Equals(CStr(fila2(2))) Then
                                node = tree.FindNodeByFieldValue("Ide", CStr(fila2(2)))
                                If (node IsNot Nothing) Then
                                    node.Selected = True
                                End If
                            End If
                        Next
                        For Each fila3 As DataRow In dataEnt.Tables(0).Rows()
                            If node.Item("Ide").Equals(CStr(fila3(0))) Then
                                node = tree.FindNodeByFieldValue("Ide", fila3(0))
                                If (node IsNot Nothing) Then
                                    node.Selected = True
                                End If
                            End If
                        Next
                    Next
                End If

                tree.CollapseAll()
            Else
                'Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S'); window.parent.hideAlerta();")
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub
End Class
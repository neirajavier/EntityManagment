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

Public Class EditarConductor
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private objConductor As Conductor
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarCombo()
            LlenarDatos()
        End If
    End Sub

    Private Sub LlenarCombo()
        Try
            'cbAseguradora.DataBind()
            Dim Aseguradora As New Object()
            Aseguradora = BD.spLlenarCombo("ASEGURADORA").ToList

            cbAseguradora2.Items.Clear()

            For Each aseg In Aseguradora
                cbAseguradora2.Items.Add(New ListEditItem(aseg.Descripcion.ToString, aseg.Codigo.ToString))
            Next

            Aseguradora = Nothing
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarDatos()
        objConductor = Nothing
        objConductor = New Conductor(Me, Session("PaisLogin"))

        With Me
            .tbIdentificacion2.Text = ""
            .tbDriverId2.Text = ""
            .tbNombres2.Text = ""
            .tbApellidos2.Text = ""
            .tbAltura2.Text = "0"
            '.cbGenero2.SelectedIndex = -1
            '.cbTipoSangre2.SelectedIndex = -1
            .tbConvencional2.Text = ""
            .tbCelular2.Text = ""
            .tbDireccion2.Value = ""
            .tbCorreo2.Text = ""
            .fchExpiracionL2.Date = New Date()
            .tbRestriccion2.Value = ""
            '.cbAlertar2.SelectedIndex = -1
            '.cbLicencia2.SelectedIndex = -1
            .tbSeguroSocial2.Text = ""
            .cbAseguradora2.SelectedIndex = 0
            .tbPoliza2.Text = ""
            .fchExpiracionS2.Date = New Date()
            .tbContactoE2.Text = ""
            .tbNumeroE2.Text = ""
            .chkBaja2.Checked = False
            .tbMotivoBaja2.Value = ""

            'Try
            '    tbIdentificacion2.MaxLength = BD.getMaxCI("EC")
            'Catch ex As Exception
            '    tbIdentificacion2.MaxLength = 10
            'End Try
        End With
    End Sub

    Private Sub LlenarDatos()
        Dim Conductor As New Object()
        Conductor = BD.spConductorConsultarV2(CInt(Session("IdUsuario")), Request.QueryString("myId").ToString).ToList
        For Each cdt In CType(Conductor, IEnumerable(Of Object))
            tbIdentificacion2.Text = cdt.Identificacion.ToString
            tbDriverId2.Text = cdt.DriverID.ToString
            tbNombres2.Text = cdt.Nombre.ToString
            tbApellidos2.Text = cdt.Apellido.ToString
            tbAltura2.Text = cdt.Altura.ToString
            cbGenero2.Value = cdt.Genero.ToString
            cbTipoSangre2.Value = cdt.TipoSangre.ToString
            tbConvencional2.Text = cdt.TelefDomicilio.ToString
            tbCelular2.Text = cdt.TelefCelular.ToString
            tbDireccion2.Text = cdt.Domicilio.ToString
            tbCorreo2.Text = cdt.Email.ToString
            If (Not cdt.ExpiracionLicencia.ToString.Equals("")) Then
                fchExpiracionL2.Date = Funciones.ANSI2FechaShort(cdt.ExpiracionLicencia.ToString)
            Else
                fchExpiracionL2.Date = New Date()
            End If
            tbRestriccion2.Value = cdt.RestriccionLicencia.ToString
            cbAlertar2.Value = cdt.AlertarVencimiento.ToString
            cbLicencia2.Value = cdt.TipoLicencia.ToString
            tbSeguroSocial2.Text = cdt.NumSeguroSocial.ToString
            cbAseguradora2.Items.FindByValue(cdt.idCiaSeguro.ToString)
            cbAseguradora2.Value = cdt.idCiaSeguro.ToString
            tbPoliza2.Text = cdt.Poliza.ToString
            If (Not cdt.ExpiracionSeguro.ToString.Equals("")) Then
                fchExpiracionS2.Date = Funciones.ANSI2FechaShort(cdt.ExpiracionSeguro.ToString)
            Else
                fchExpiracionS2.Date = New Date()
            End If
            tbContactoE2.Text = cdt.TelefEmergencia.ToString
            tbNumeroE2.Text = cdt.NumeroEmergencia.ToString
            chkBaja2.Checked = IIf(cdt.DadoDeBaja.Equals("N"), 0, 1)
            tbMotivoBaja2.Value = cdt.MotivoBaja.ToString
            If (chkBaja2.Checked) Then
                tbMotivoBaja2.ClientVisible = True
            End If
        Next
    End Sub

    Protected Sub btGrabar2_Click(sender As Object, e As EventArgs)
        Try
            If (Session("PaisLogin").ToString.Equals("EC")) Then
                If tbNombres2.Text = "" Or tbApellidos2.Text = "" Or tbIdentificacion2.Text = "" Then
                    texto = "Los Datos de Nombre, Apellido, Identificacion, Direccion y Telefonos Celular son Obligatorios."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                Else
                    objConductor = Nothing
                    objConductor = New Conductor(Me, Session("PaisLogin"))

                    If tbIdentificacion2.Text = "" Or tbIdentificacion2.Text.Length < tbIdentificacion2.MaxLength Then
                        texto = "Cédula o Identificación (es menor a " & tbIdentificacion2.MaxLength.ToString() & " números)."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    End If

                    'If BD.ExisteConductor(tbIdentificacion2.Text, CInt(Session("idUsuario"))) <> 0 Then
                    '    Funciones.EjecutarFuncionJavascript(Me, "alert('La identificación del Usuario Ya Existe, favor ingresar una nueva')")
                    '    Exit Sub
                    'End If

                    If tbAltura2.Text = "" Or CInt(tbAltura2.Text) <= 0 Then
                        texto = "Debe de Ingresar la Altura del Conductor para Continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    End If

                    If fchExpiracionL2.Text = "" Then
                        texto = "Debe de Ingresar una Fecha de Expiración de Licencia para Continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    End If

                    If Not IsDate(fchExpiracionL2.Text) And fchExpiracionL2.Text <> "" Then
                        texto = "Formato de Fecha No Válido, para Fecha Expiración Licencia."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    ElseIf DateDiff(DateInterval.Day, Now.Date, CDate(fchExpiracionL2.Text)) <= 0 Then
                        texto = "Fecha No Válida, la Fecha debe de ser mayor a la fecha de hoy, para Fecha Expiración Licencia."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    End If

                    Try
                        If Not IsDate(fchExpiracionS2.Text) And fchExpiracionS2.Text <> "" Then
                            texto = "Formato de Fecha No Válido, para Fecha Expiración Seguro."
                            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                            fchExpiracionL2.Date = New Date()
                            fchExpiracionL2.Focus()
                        ElseIf DateDiff(DateInterval.Day, Now.Date, CDate(fchExpiracionS2.Text)) <= 0 Then
                            texto = "Fecha No Válida, la Fecha debe de ser mayor a la fecha de hoy, para Fecha Expiración Seguro."
                            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                            fchExpiracionS2.Date = New Date()
                            fchExpiracionS2.Focus()
                        End If
                    Catch ex As Exception
                        fchExpiracionS2.Date = New Date()
                    End Try

                    With objConductor

                        .NuevaIdentificacion = tbIdentificacion2.Text
                        .Identificacion = tbIdentificacion2.Text
                        .Nombres = tbNombres2.Text.ToUpper()
                        .Apellidos = tbApellidos2.Text.ToUpper()
                        .IdUsuario = CInt(Session("IdUsuario"))
                        Try
                            .Altura = tbAltura2.Text
                        Catch ex As Exception
                            .Altura = 160
                        End Try

                        .Genero = cbGenero2.SelectedItem.Value
                        .TipoSangre = cbTipoSangre2.SelectedItem.Value
                        .TelefonoDomicilio = tbConvencional2.Text
                        .TelefonoCelular = tbCelular2.Text
                        .DireccionDomicilio = tbDireccion2.Text.ToUpper
                        .Email = tbCorreo2.Text
                        .FechaExpiracionLicencia = fchExpiracionL2.Text
                        .RestriccionLicencia = tbRestriccion2.Text
                        .NumeroSeguroSocial = tbSeguroSocial2.Text
                        .PolizaSeguro = tbPoliza2.Text
                        .FechaExpiracionSeguro = fchExpiracionS2.Text
                        .DriverID = tbDriverId2.Text
                        .ContactoEmergencia = tbContactoE2.Text
                        .NumeroEmergencia = tbNumeroE2.Text
                        .UsuarioIngreso = Session("Usuario")
                        .TipoLicencia = cbLicencia2.SelectedItem.Value
                        .AlertarVencimiento = (cbAlertar2.SelectedItem.Value)

                        If tbSeguroSocial2.Text = "" Then
                            tbSeguroSocial2.Text = tbIdentificacion2.Text
                        End If
                        .NumeroSeguroSocial = tbSeguroSocial2.Text

                        Try
                            .IdCiaSeguro = cbAseguradora2.SelectedItem.Value
                        Catch ex As Exception
                            .IdCiaSeguro = 0
                        End Try

                        Try
                            .DadoBaja = IIf(chkBaja2.Checked, 1, 0)
                        Catch ex As Exception
                            .DadoBaja = 0
                        End Try

                        .MotivoBaja = tbMotivoBaja2.Text.ToUpper

                        .Ejecutar("MOD")
                        .Ejecutar("BAJ")
                        'If .DadoBaja Then
                        '    .Ejecutar("BAJ")
                        'End If

                        Dim startUpScript As String = String.Format("window.parent.HidePopup2();")
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

                        texto = "Conductor modificado con éxito"
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S');")
                    End With

                    'objConductor = Nothing
                End If
            Else
                If tbNombres2.Text = "" Or tbApellidos2.Text = "" Or tbIdentificacion2.Text = "" Then
                    texto = "Los Datos de Nombre, Apellido e Identificacion son Obligatorios."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                Else
                    objConductor = Nothing
                    objConductor = New Conductor(Me, Session("PaisLogin"))

                    'If tbIdentificacion2.Text = "" Or tbIdentificacion2.Text.Length < tbIdentificacion2.MaxLength Then
                    '    texto = "Cédula o Identificación (es menor a " & tbIdentificacion2.MaxLength.ToString() & " números)."
                    '    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    'End If

                    'If BD.ExisteConductor(tbIdentificacion2.Text, CInt(Session("idUsuario"))) <> 0 Then
                    '    Funciones.EjecutarFuncionJavascript(Me, "alert('La identificación del Usuario Ya Existe, favor ingresar una nueva')")
                    '    Exit Sub
                    'End If

                    With objConductor

                        .NuevaIdentificacion = tbIdentificacion2.Text
                        .Identificacion = tbIdentificacion2.Text
                        .Nombres = tbNombres2.Text.ToUpper()
                        .Apellidos = tbApellidos2.Text.ToUpper()
                        .IdUsuario = CInt(Session("IdUsuario"))
                        Try
                            .Altura = tbAltura2.Text
                        Catch ex As Exception
                            .Altura = 160
                        End Try

                        .Genero = cbGenero2.SelectedItem.Value
                        .TipoSangre = cbTipoSangre2.SelectedItem.Value
                        .TelefonoDomicilio = tbConvencional2.Text
                        .TelefonoCelular = tbCelular2.Text
                        .DireccionDomicilio = tbDireccion2.Text.ToUpper
                        .Email = tbCorreo2.Text
                        .FechaExpiracionLicencia = fchExpiracionL2.Text
                        .RestriccionLicencia = tbRestriccion2.Text
                        .NumeroSeguroSocial = tbSeguroSocial2.Text
                        .PolizaSeguro = tbPoliza2.Text
                        .FechaExpiracionSeguro = fchExpiracionS2.Text
                        .DriverID = tbDriverId2.Text
                        .ContactoEmergencia = tbContactoE2.Text
                        .NumeroEmergencia = tbNumeroE2.Text
                        .UsuarioIngreso = Session("Usuario")
                        .TipoLicencia = cbLicencia2.SelectedItem.Value
                        .AlertarVencimiento = (cbAlertar2.SelectedItem.Value)

                        If tbSeguroSocial2.Text = "" Then
                            tbSeguroSocial2.Text = tbIdentificacion2.Text
                        End If
                        .NumeroSeguroSocial = tbSeguroSocial2.Text

                        Try
                            .IdCiaSeguro = cbAseguradora2.SelectedItem.Value
                        Catch ex As Exception
                            .IdCiaSeguro = 0
                        End Try

                        Try
                            .DadoBaja = IIf(chkBaja2.Checked, 1, 0)
                        Catch ex As Exception
                            .DadoBaja = 0
                        End Try

                        .MotivoBaja = tbMotivoBaja2.Text.ToUpper

                        .Ejecutar("MOD")
                        .Ejecutar("BAJ")
                        'If .DadoBaja Then
                        '    .Ejecutar("BAJ")
                        'End If

                        Dim startUpScript As String = String.Format("window.parent.HidePopup2();")
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

                        texto = "Conductor modificado con éxito"
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S');")
                    End With

                    'objConductor = Nothing
                End If
            End If

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
            texto = "Hubo un error al realizar la actualizazión."
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
        End Try
    End Sub

End Class
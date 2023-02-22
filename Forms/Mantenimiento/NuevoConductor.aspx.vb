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

Public Class NuevoConductor
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private objConductor As Conductor
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarCombo()
        End If
    End Sub

    Private Sub LlenarCombo()
        Try
            'cbAseguradora.DataBind()
            Dim Aseguradora As New Object()
            Aseguradora = BD.spLlenarCombo("ASEGURADORA").ToList

            cbAseguradora.Items.Clear()

            For Each aseg In Aseguradora
                cbAseguradora.Items.Add(New ListEditItem(aseg.Descripcion.ToString, aseg.Codigo.ToString))
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
            .tbIdentificacion.Text = ""
            .tbDriverId.Text = ""
            .tbNombres.Text = ""
            .tbApellidos.Text = ""
            .tbAltura.Text = "0"
            '.cbGenero.SelectedIndex = -1
            '.cbTipoSangre.SelectedIndex = -1
            .tbConvencional.Text = ""
            .tbCelular.Text = ""
            .tbDireccion.Value = ""
            .tbCorreo.Text = ""
            .fchExpiracionL.Date = New Date()
            .tbRestriccion.Value = ""
            '.cbAlertar.SelectedIndex = -1
            '.cbLicencia.SelectedIndex = -1
            .tbSeguroSocial.Text = ""
            .cbAseguradora.SelectedIndex = 0
            .tbPoliza.Text = ""
            .fchExpiracionS.Date = New Date()
            .tbContactoE.Text = ""
            .tbNumeroE.Text = ""
            .chkBaja.Checked = False
            .tbMotivoBaja.Value = ""

            Try
                tbIdentificacion.MaxLength = BD.getMaxCI("EC")
            Catch ex As Exception
                tbIdentificacion.MaxLength = 10
            End Try
        End With
    End Sub

    Protected Sub btGrabar_Click(sender As Object, e As EventArgs)
        Try
            If (Session("PaisLogin").ToString.Equals("EC")) Then
                If tbNombres.Text = "" Or tbApellidos.Text = "" Or tbIdentificacion.Text = "" Or tbDireccion.Text = "" Or tbCelular.Text = "" Then
                    texto = "Los Datos de Nombre, Apellido, Identificacion, Direccion y Telefonos Celular son Obligatorios."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    Return
                Else
                    objConductor = Nothing
                    objConductor = New Conductor(Me, Session("PaisLogin"))

                    If tbIdentificacion.Text = "" Or tbIdentificacion.Text.Length < tbIdentificacion.MaxLength Then
                        texto = "Cédula o Identificación (es menor a " & tbIdentificacion.MaxLength.ToString() & " números)."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    If Funciones.VerificaCedula(tbIdentificacion.Text) = False Then
                        texto = "Cédula o Identificación tiene un formato no Válido."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    If BD.ExisteConductor(tbIdentificacion.Text, CInt(Session("idUsuario"))) <> 0 Then
                        texto = "La identificación del conductor Ya Existe con el Usuario, favor ingresar una nueva."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    If tbAltura.Text = "" Or CInt(tbAltura.Text) <= 0 Then
                        texto = "Debe de Ingresar la Altura del Conductor para Continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    If fchExpiracionL.Text = "" Then
                        texto = "Debe de Ingresar una Fecha de Expiración de Licencia para Continuar."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    If Not IsDate(fchExpiracionL.Text) And fchExpiracionL.Text <> "" Then
                        texto = "Formato de Fecha No Válido, para Fecha Expiración Licencia."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    ElseIf DateDiff(DateInterval.Day, Now.Date, CDate(fchExpiracionL.Text)) <= 0 Then
                        texto = "Fecha No Válida, la Fecha debe de ser mayor a la fecha de hoy, para Fecha Expiración Licencia."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    Try
                        If Not IsDate(fchExpiracionS.Text) And fchExpiracionS.Text <> "" Then
                            texto = "Formato de Fecha No Válido, para Fecha Expiración Seguro."
                            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                            Return
                            fchExpiracionL.Date = New Date()
                            fchExpiracionL.Focus()
                        ElseIf DateDiff(DateInterval.Day, Now.Date, CDate(fchExpiracionS.Text)) <= 0 Then
                            texto = "Fecha No Válida, la Fecha debe de ser mayor a la fecha de hoy, para Fecha Expiración Seguro."
                            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                            Return
                            fchExpiracionS.Date = New Date()
                            fchExpiracionS.Focus()
                        End If
                    Catch ex As Exception
                        fchExpiracionS.Date = New Date()
                    End Try

                    With objConductor

                        .NuevaIdentificacion = tbCodigo.Value
                        .Identificacion = tbIdentificacion.Text
                        .Nombres = tbNombres.Text.ToUpper()
                        .Apellidos = tbApellidos.Text.ToUpper()
                        .IdUsuario = CInt(Session("IdUsuario"))
                        Try
                            .Altura = tbAltura.Text
                        Catch ex As Exception
                            .Altura = 160
                        End Try

                        .Genero = cbGenero.SelectedItem.Value
                        .TipoSangre = cbTipoSangre.SelectedItem.Value
                        .TelefonoDomicilio = tbConvencional.Text
                        .TelefonoCelular = tbCelular.Text
                        .DireccionDomicilio = tbDireccion.Text.ToUpper
                        .Email = tbCorreo.Text
                        .FechaExpiracionLicencia = fchExpiracionL.Text
                        .RestriccionLicencia = tbRestriccion.Text
                        .NumeroSeguroSocial = tbSeguroSocial.Text
                        .PolizaSeguro = tbPoliza.Text
                        .FechaExpiracionSeguro = fchExpiracionS.Text
                        .DriverID = tbDriverId.Text
                        .ContactoEmergencia = tbContactoE.Text
                        .NumeroEmergencia = tbNumeroE.Text
                        .UsuarioIngreso = Session("Usuario")
                        .TipoLicencia = cbLicencia.SelectedItem.Value
                        .AlertarVencimiento = (cbAlertar.SelectedItem.Value)

                        If tbSeguroSocial.Text = "" Then
                            tbSeguroSocial.Text = tbIdentificacion.Text
                        End If

                        Try
                            .IdCiaSeguro = cbAseguradora.SelectedItem.Value
                        Catch ex As Exception
                            .IdCiaSeguro = 0
                        End Try

                        Try
                            .DadoBaja = IIf(chkBaja.Checked, 1, 0)
                        Catch ex As Exception
                            .DadoBaja = 0
                        End Try

                        .MotivoBaja = tbMotivoBaja.Text.ToUpper

                        .Ejecutar("ING")

                        If .DadoBaja Then
                            .Ejecutar("BAJ")
                        End If

                        Dim startUpScript As String = String.Format("window.parent.HidePopup();")
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

                        texto = "Conductor ingresado con éxito"
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S');")
                    End With

                    objConductor = Nothing
                End If
            Else
                If tbNombres.Text = "" Or tbApellidos.Text = "" Or tbIdentificacion.Text = "" Then
                    texto = "Los Datos de Nombre, Apellido, e Identificacion son Obligatorios."
                    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    Return
                Else
                    objConductor = Nothing
                    objConductor = New Conductor(Me, Session("PaisLogin"))

                    'If tbIdentificacion.Text = "" Or tbIdentificacion.Text.Length < tbIdentificacion.MaxLength Then
                    '    texto = "Cédula o Identificación (es menor a " & tbIdentificacion.MaxLength.ToString() & " números)."
                    '    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    '    Return
                    'End If

                    'If Funciones.VerificaCedula(tbIdentificacion.Text) = False Then
                    '    texto = "Cédula o Identificación tiene un formato no Válido."
                    '    Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                    '    Return
                    'End If

                    If BD.ExisteConductor(tbIdentificacion.Text, CInt(Session("idUsuario"))) <> 0 Then
                        texto = "La identificación del conductor Ya Existe con el Usuario, favor ingresar una nueva."
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
                        Return
                    End If

                    With objConductor

                        .NuevaIdentificacion = tbCodigo.Value
                        .Identificacion = tbIdentificacion.Text
                        .Nombres = tbNombres.Text.ToUpper()
                        .Apellidos = tbApellidos.Text.ToUpper()
                        .IdUsuario = CInt(Session("IdUsuario"))
                        Try
                            .Altura = tbAltura.Text
                        Catch ex As Exception
                            .Altura = 160
                        End Try

                        .Genero = cbGenero.SelectedItem.Value
                        .TipoSangre = cbTipoSangre.SelectedItem.Value
                        .TelefonoDomicilio = tbConvencional.Text
                        .TelefonoCelular = tbCelular.Text
                        .DireccionDomicilio = tbDireccion.Text.ToUpper
                        .Email = tbCorreo.Text
                        .FechaExpiracionLicencia = fchExpiracionL.Text
                        .RestriccionLicencia = tbRestriccion.Text
                        .NumeroSeguroSocial = tbSeguroSocial.Text
                        .PolizaSeguro = tbPoliza.Text
                        .FechaExpiracionSeguro = fchExpiracionS.Text
                        .DriverID = tbDriverId.Text
                        .ContactoEmergencia = tbContactoE.Text
                        .NumeroEmergencia = tbNumeroE.Text
                        .UsuarioIngreso = Session("Usuario")
                        .TipoLicencia = cbLicencia.SelectedItem.Value
                        .AlertarVencimiento = (cbAlertar.SelectedItem.Value)

                        If tbSeguroSocial.Text = "" Then
                            tbSeguroSocial.Text = tbIdentificacion.Text
                        End If

                        Try
                            .IdCiaSeguro = cbAseguradora.SelectedItem.Value
                        Catch ex As Exception
                            .IdCiaSeguro = 0
                        End Try

                        Try
                            .DadoBaja = IIf(chkBaja.Checked, 1, 0)
                        Catch ex As Exception
                            .DadoBaja = 0
                        End Try

                        .MotivoBaja = tbMotivoBaja.Text.ToUpper

                        .Ejecutar("ING")

                        If .DadoBaja Then
                            .Ejecutar("BAJ")
                        End If

                        Dim startUpScript As String = String.Format("window.parent.HidePopup();")
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)

                        texto = "Conductor ingresado con éxito"
                        Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','S');")
                    End With

                    objConductor = Nothing
                End If
            End If

            LimpiarDatos()
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
            If (ex.Message.Contains("PK")) Then
                texto = "Hubo un error al realizar el ingreso, Identificación ID ya existente."
            Else
                texto = "Hubo un error al realizar el ingreso."
            End If

            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification('" + texto + "','A');")
        End Try
    End Sub

End Class
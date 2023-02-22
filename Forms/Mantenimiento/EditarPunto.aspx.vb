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

Public Class EditarPunto
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private punto As String = ""
    Private color As String = ""
    Private latitud As String = ""
    Private longitud As String = ""
    Private PuntosDet As New Object()
    Private datasetAux As DataSet
    Public oPunto As Punto
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BD = New OdinDataContext(Globales.getCadenaConexionPais(Session("PaisLogin"), False))

        If Not IsPostBack Then
            LlenarCombo()
            LlenarWindow()
        End If
    End Sub

    Protected Sub btnOK_Click(sender As Object, e As EventArgs)
        Try
            oPunto = New Punto(Me, Session("PaisLogin"))
            punto = Request.QueryString("myPid").ToString
            color = selectColor2(cbColor.Text)
            latitud = tbLatitud.Text
            longitud = tbLongitud.Text
            oPunto.UpdateDatosSecundarios(CInt(punto), CDbl(latitud), CDbl(longitud), CInt(color), CInt(cbCategoria.Value), CInt(cbSubcategoria.Value), tbCelular.Text, tbEmail.Text, Session("Usuario"))

            Dim startUpScript As String = String.Format("window.parent.HidePopup7();")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ANY_KEY", startUpScript, True)
            texto = "Punto :: Datos Actualizados con Exito"
            Funciones.EjecutarFuncionJavascript(Me, "window.parent.showNotification2('" + texto + "','S');")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub LlenarCombo()
        Try
            oPunto = New Punto(Me, Session("PaisLogin"))
            Dim categoria As DataSet
            Dim subcategoria As DataSet
            categoria = oPunto.LlenarCombo("CATEGORIAPUNTO")
            subcategoria = oPunto.LlenarCombo("SUBCATEGORIAPUNTO")

            cbCategoria.Items.Clear()
            cbSubcategoria.Items.Clear()

            For Each categ As DataRow In categoria.Tables(0).Rows()
                cbCategoria.Items.Add(New ListEditItem(categ(1).ToString, categ(0).ToString))
            Next
            For Each scateg As DataRow In subcategoria.Tables(0).Rows()
                cbSubcategoria.Items.Add(New ListEditItem(scateg(1).ToString, scateg(0).ToString))
            Next

            categoria = Nothing
            subcategoria = Nothing
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Private Sub LlenarWindow()
        Try
            punto = Request.QueryString("myPid").ToString
            oPunto = New Punto(Me, Session("PaisLogin"))
            datasetAux = oPunto.ConsultarParciales(CInt(Session("IdUsuario")), CInt(punto))
            For Each fila As DataRow In datasetAux.Tables(0).Rows()
                tbLatitud.Text = fila(1)
                tbLongitud.Text = fila(2)
                If (Not fila(3).Equals("")) Then
                    cbColor.Value = obtenerColor(fila(3).ToString)
                    cbColor.Text = obtenerColor2(fila(3).ToString)
                End If
                If (Not fila(4).Equals("")) Then
                    cbCategoria.Value = fila(4).ToString
                End If
                If (Not fila(5).Equals("")) Then
                    cbSubcategoria.Value = fila(5).ToString
                End If
                tbCelular.Text = fila(6).ToString
                tbEmail.Text = fila(7).ToString
            Next
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Public Function selectColor(ByVal color As String) As String
        Dim id As String = ""
        Try
            If (color.Equals("#00457E")) Then
                id = "0"
            ElseIf (color.Equals("#ff0000")) Then
                id = "1"
            ElseIf (color.Equals("#000000")) Then
                id = "2"
            ElseIf (color.Equals("#77FF00")) Then
                id = "3"
            ElseIf (color.Equals("#FF9900")) Then
                id = "4"
            ElseIf (color.Equals("#FFDA44")) Then
                id = "5"
            ElseIf (color.Equals("#933EC5")) Then
                id = "6"
            End If
            Return id
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    Public Function selectColor2(ByVal color As String) As String
        Dim id As String = ""
        Try
            If (color.Equals("AZUL")) Then
                id = "0"
            ElseIf (color.Equals("ROJO")) Then
                id = "1"
            ElseIf (color.Equals("NEGRO")) Then
                id = "2"
            ElseIf (color.Equals("VERDE")) Then
                id = "3"
            ElseIf (color.Equals("NARANJA")) Then
                id = "4"
            ElseIf (color.Equals("AMARILLO")) Then
                id = "5"
            ElseIf (color.Equals("MORADO")) Then
                id = "6"
            End If
            Return id
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    Public Function obtenerColor(ByVal id As String) As String
        Dim color As String = ""
        Try
            If (id.Equals("0")) Then
                color = "#00457E"
            ElseIf (id.Equals("1")) Then
                color = "#ff0000"
            ElseIf (id.Equals("2")) Then
                color = "#000000"
            ElseIf (id.Equals("3")) Then
                color = "#77FF00"
            ElseIf (id.Equals("4")) Then
                color = "#FF9900"
            ElseIf (id.Equals("5")) Then
                color = "#FFDA44"
            ElseIf (id.Equals("6")) Then
                color = "#933EC5"
            End If
            Return color
        Catch ex As Exception
            Return "#00457E"
        End Try
    End Function

    Public Function obtenerColor2(ByVal id As String) As String
        Dim color As String = ""
        Try
            If (id.Equals("0")) Then
                color = "AZUL"
            ElseIf (id.Equals("1")) Then
                color = "ROJO"
            ElseIf (id.Equals("2")) Then
                color = "NEGRO"
            ElseIf (id.Equals("3")) Then
                color = "VERDE"
            ElseIf (id.Equals("4")) Then
                color = "NARANJA"
            ElseIf (id.Equals("5")) Then
                color = "AMARILLO"
            ElseIf (id.Equals("6")) Then
                color = "MORADO"
            End If
            Return color
        Catch ex As Exception
            Return "AZUL"
        End Try
    End Function

End Class
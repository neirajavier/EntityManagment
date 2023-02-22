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

Public Class MantenimientoPassKey
    Inherits System.Web.UI.Page

    Private texto As String = ""
    Private BD As New OdinDataContext()
    Private oActivo As Activo
    Private datasetAux As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ''Codigo para pruebas internas''
            Dim valor As New Object()
            If Request.QueryString("o") <> "" Then
                Try
                    Dim Keys = BD.spUsuarioConsultarKey(Request.QueryString("O"))
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

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            If Not IsPostBack Then
                LlenarCombo()
            End If
        End Try

    End Sub

    Private Sub LlenarCombo()
        Try
            Dim Unidades As New Object()
            Dim Geocercas As New Object()

            Unidades = BD.spListarActivosEntidad(Session("Entidades"), 0).ToList
            Geocercas = BD.spGeocercaConsultar(CInt(Session("IdUsuario"))).ToList

            cbActivos.Items.Clear()
            cbGeocercas.Items.Clear()

            For Each unidad In Unidades
                cbActivos.Items.Add(New ListEditItem(unidad.Alias.ToString & " :: " & unidad.Etiqueta.ToString, unidad.Vid.ToString + "_" + unidad.IdActivo.ToString))
            Next

            For Each geo In Geocercas
                cbGeocercas.Items.Add(New ListEditItem(geo.Nombre.ToString & " :: " & geo.Tipo_Geocerca.ToString, geo.IdGeocerca.ToString))
            Next

        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
        End Try
    End Sub

    Protected Sub cbComandos_Callback(sender As Object, e As CallbackEventArgsBase)
        Try
            Dim Comandos As New Object()
            oActivo = New Activo(Me)
            Dim miActivo As String() = cbActivos.Value.ToString.Split("_")

            'Comandos = BD.spComandosPorIdVehiculoMOBILEDEVICEv2(CStr(Session("Usuario")), CStr(Session("Clave")), CInt(miActivo(1)), miActivo(0).ToString, 0, "").ToList
            'For Each comando In Comandos
            '    cbComandos.Items.Add(New ListEditItem(comando.NOMBRECOMANDO.ToString, comando.IDCOMANDO))
            'Next

            datasetAux = oActivo.ComandosConsultar(CStr(Session("Usuario")), CStr(Session("Clave")), miActivo(1), miActivo(0))
            For Each fila As DataRow In datasetAux.Tables(0).Rows()
                cbComandos.Items.Add(New ListEditItem(fila(5).ToString, fila(4).ToString))
            Next
        Catch ex As Exception
            Console.WriteLine(ex.StackTrace)
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Protected Sub btGrabar_Click(sender As Object, e As EventArgs)

    End Sub
End Class
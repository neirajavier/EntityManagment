Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System
Imports DevExpress.Web
Imports DevExpress.Utils.Menu
Imports System.Configuration.ConfigurationSettings
Imports Microsoft.ApplicationBlocks.Data

Partial Class AdminMain
    Inherits System.Web.UI.Page
    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("o") <> "" Then
            Try
                'txuid.Value = "o=" & Request.QueryString("o") & "&INI=" & Request.QueryString("INI") & "&FIN=" & Request.QueryString("FIN") & "&Grupo=" & Request.QueryString("GRP")
                Dim Keys = BD.spUsuarioConsultarKey(Request.QueryString("O"))
                For Each Key In Keys
                    Try
                        hidusuario.Value = Key.IdUsuario
                    Catch ex As Exception
                        hidusuario.Value = -1
                        Console.WriteLine(ex.Message)
                    End Try
                    Try
                        spusuario.InnerText = "Usuario: " & Key.Nombre & " (" & Key.Usuario & ")"
                    Catch ex As Exception
                        spusuario.InnerText = "Usuario: " & Key.Usuario & ""
                    End Try
                Next

                If hidusuario.Value = 0 Then
                    Dim KeysS = BD.spSubUsuarioConsultarKey(Request.QueryString("O"))
                    For Each Key In KeysS
                        Try
                            hidusuario.Value = Key.IdUsuario
                            hidsubusuario.Value = Key.IdSubUsuario
                        Catch ex As Exception
                            hidusuario.Value = -1
                            Console.WriteLine(ex.Message)
                        End Try
                        Try
                            spusuario.InnerText = "Usuario: " & Key.Nombre
                        Catch ex As Exception
                            Console.Write(ex.Message)
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
            hdregs.Value = 0
            Exit Sub
        End If
    End Sub

End Class
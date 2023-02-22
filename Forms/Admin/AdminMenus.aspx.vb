Imports System.Configuration
Imports System.Configuration.ConfigurationSettings
Partial Class AdminMenus
    Inherits System.Web.UI.Page

    Public urlGrupoVehiculos As String
    Public urlGrupoGeocercas As String
    Public urlGrupoPuntos As String
    Public urlGrupoConductores As String

    Private BD As New OdinDataContext()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim valor As New Object()
            Dim user As String = ""

            'If Request.QueryString("o") <> "" Then
            Try
                'txuid.Value = "o=" & Request.QueryString("o") & "&INI=" & Request.QueryString("INI") & "&FIN=" & Request.QueryString("FIN") & "&Grupo=" & Request.QueryString("GRP")
                'Dim Keys = BD.spUsuarioConsultarKey(Request.QueryString("O"))

                'USERS
                'CFFF36E8-8DB6-41C2-92CF-2AC5E72A55F5 'JAVIER
                '7E88DEF0-BDA1-4275-B3C3-32FD4EFA76E0 'ERWIN
                '15E608D8-38FD-4611-83EC-5BA19F2301A0 'DINADEC
                '5739EE93-40D9-4B94-97A9-E2F3F262423D 'CSCG
                '97B28E02-1931-4562-B746-C4F9D1C9C56F 'URVASEO
                'B605C1D1-BC76-4676-B83C-7652D21FAFA6 'SIN DATOS
                '3FBBEE1E-0A92-4A60-9D5B-C87CAD4AF371 'TIA
                '1ABD5A0C-1FFB-49EF-BAC8-E9424BCE4AC9 'GFCARSEGSA
                '5FE71F4B-32F1-4D51-BE70-4BA8D6DD6EA5 'VENTASCARSEG
                '84A4052F-C58F-4590-A41B-4EFBCC080F54 'VENTASSTODGO
                '7A6CFD49-FDB7-4741-A083-3CA48D613765 'PORTRANSTOTAL
                '139528C5-63CC-47C2-85F6-FE3DBD8AB20B 'ICHIBAN

                'SUBUSERS
                '521527C6-BC64-4B38-93A0-82A7DC3F1FF4 'ECUAHIELO subuser EXPALSA
                'B219148E-B01C-46D5-8860-777298080690 'MINORISTA GPS 1
                'D6E8AF95-1D02-4D4F-8417-54F19D4E4EA6 'ALAPOGPS
                '9A76B635-11ED-4437-9FA0-196C64BD315B 'PORTRANSSUPGA

                'PERU
                'D6E8AF95-1D02-4D4F-8417-54F19D4E4EA6 'HIRAOKA
                'FC361C00-C5AD-437A-9170-B869892F8FB8 'DEMOCORPORATIVOFULL
                '91515AE4-650C-4AF6-95CD-B1092EF61E4E 'DEMOBASICOPERSONAL
                '371B6FCB-5AD1-4B5A-9ECD-D72F268474C2 'GHCOIN STATKRAFT

                BD = New OdinDataContext(Globales.getCadenaConexionPais("PE", False))

                user = "91515AE4-650C-4AF6-95CD-B1092EF61E4E"
                Dim Keys = BD.spUsuarioConsultarKey(user).ToList
                For Each Key In Keys
                    Try
                        hidusuario.Value = Key.IdUsuario
                        Session("Usuario") = Key.Usuario
                        Session("Clave") = Key.Clave
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
                hidusuario.Value = IIf(Keys.Count = 0, 0, hidusuario.Value)
                If (hidusuario.Value.Equals("0") Or hidusuario.Value.Equals("")) Then
                    Dim KeysS = BD.spSubUsuarioConsultarKey(user).ToList
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
                        Try
                            spusuario.InnerText = "Usuario: " & Key.Nombre & " : " & Key.SubUsuario
                        Catch ex As Exception
                            Console.Write(ex.Message)
                        End Try
                    Next
                    Session("idSubUsuario") = hidsubusuario.Value
                    valor = BD.spUsuarioEntidadConsultarSubusuario(CInt(hidusuario.Value), "127001", CInt(hidsubusuario.Value)).ToList
                    For Each entidad In valor
                        Session("Entidades") = CStr(entidad.IdEntidad)
                    Next
                    Session("esUsuario") = False
                Else
                    valor = BD.spUsuarioEntidadConsultar(CInt(hidusuario.Value), "127001").ToList
                    For Each entidad In valor
                        Session("Entidades") = CStr(entidad.IdEntidad)
                    Next
                    Session("esUsuario") = True
                End If
                Session("IdUsuario") = hidusuario.Value

                Keys = Nothing

            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.StackTrace)
            End Try

            urlGrupoGeocercas = "/Forms/Mantenimiento/MantenimientoVehiculo.aspx"
            urlGrupoVehiculos = "/Forms/Mantenimiento/MantenimientoGrupo.aspx"
            urlGrupoConductores = "/Forms/Mantenimiento/MantenimientoConductor.aspx"
            urlGrupoPuntos = "/Forms/Mantenimiento/MantenimientoPuntos.aspx"

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

End Class
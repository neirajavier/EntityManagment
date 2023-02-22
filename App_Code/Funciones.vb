Imports System.Math
Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager
Imports System
Imports System.Web.UI
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Module Globales
    Public MensajeJavascript As String
    Public LatitudInicial As String
    Public LongitudInicial As String
    Public KeyRecorrido As String = AppSettings("KeyGMRecorrido")

    Public Empresa As String
    Public FondoSuperior As String
    Public FondoSuperior2 As String
    Public ColorEmpresa As String
    Public ColorCheckPoint As String

    <Serializable>
    Public Structure clsInfoUnidad
        Dim Vid As String
        Dim IdActivo As Integer
        Dim Alias_ As String
        Dim Etiqueta As String
        Dim Entidad As String
        Dim Grupo As String
        Dim Despacho As Integer
    End Structure

    <Serializable>
    Public Structure ConfigUsuario
        Dim EnvioComandos As Boolean
        Dim VerIndicadores As Boolean
        Dim UnidadVelocidad As String
        Dim UnidadDistancia As String
        Dim HusoHorario As Integer
        Dim CapasAdicionales As String
        Dim MostrarCoordenadas As Boolean
        Dim UnidadCoordenadas As String
        Dim MostrarTodos As Boolean
        Dim AdministracionRutas As Boolean
        Dim MotorMapa As String
        Dim MaxUsuarioInterrogacion As Integer
        Dim MaxUsuarioEmail As Integer
        Dim GenerarNombrePuntos As Boolean
        Dim SubUsuarios As Boolean
        Dim CaducidadClave As Boolean
        Dim IntervaloCaducidad As Integer
        Dim ReintentosLogin As Integer
        Dim FechaCambioContraseña As String
        Dim EstadoFlotaInicial As Boolean
        Dim AdministracionGrupos As Boolean
        Dim EditarEtiquetas As Boolean
        Dim AgruparPor As String
        Dim AdmConductores As Boolean
        Dim AdmMantenimientos As Boolean
        Dim MostrarConsumoPromedio As Boolean
        Dim ReportesProgramados As Boolean
        Dim EnvioKMS As Boolean
        Dim EnvioTemperatura As Boolean
        Dim VisorAlertas As Boolean
        Dim PermitirCambioMapas As Boolean
        Dim IdPais As String
        Dim MostrarOTT As Boolean
        Dim MostrarDASH As Boolean
        Dim ModoLogin As String
        Dim MostrarAuditoriaEXT As Boolean
        Dim FormatoEstadoFlota As String
        Dim TiempoSinReportar As Integer
        Dim AbrirSeguimientosFlota As Boolean
        Dim MostrarAlertasMapa As Boolean
        Dim ClusterMapa As Boolean
        Dim ClusterZoom As Integer
        Dim MostrarSubIconoSR As Boolean
        Dim MostrarUnidadesCercanas As Boolean
        Dim EstiloMostrarAlertas As String
        Dim PicoPlaca As Boolean
        Dim PicoPlacaLocalidades As String
        Dim PaisEstadoFlota As String
        Dim PaisVisorAlertas As String
        Dim PaisDashBoard As String
        Dim AutoFit As Boolean
        Dim TiempoAlertarON As Integer
        Dim TiempoAlertarOFF As Integer
        Dim MaxDiasReportes As Integer
        Dim ImagenRecorrido As Boolean
        Dim AutoCodigoPunto As Boolean
    End Structure

    <Serializable>
    Public Structure ConfigSubUsuario
        Dim EnvioComandos As Boolean
        Dim VerIndicadores As Boolean
        Dim UnidadVelocidad As String
        Dim UnidadDistancia As String
        Dim HusoHorario As Integer
        Dim FechaCaducidad As String
        Dim VerRecorridos As Boolean
        Dim VerKilometraje As Boolean
        Dim VerAlertas As Boolean
        Dim AdmAlertas As Boolean
        Dim AdmPuntosReferencia As Boolean
        Dim AdmGeocercas As Boolean
        Dim EstadoFlotaInicial As Boolean
        Dim AdministracionGrupos As Boolean
        Dim EditarEtiquetas As Boolean
        Dim AgruparPor As String
        Dim AdmConductores As Boolean
        Dim AdmMantenimientos As Boolean
        Dim MostrarConsumoPromedio As Boolean
        Dim ReportesProgramados As Boolean
        Dim EnvioKMS As Boolean
        Dim EnvioTemperatura As Boolean
        Dim VisorAlertas As Boolean
        Dim MotorMapa As String
        Dim PermitirCambioMapas As Boolean
        Dim IdPais As String
        Dim MostrarOTT As Boolean
        Dim MostrarDASH As Boolean
        Dim verSeguimiento As Boolean
        Dim verDashboard As Boolean
        Dim MostrarAuditoriaEXT As Boolean
        Dim CrearLinkSeguimiento As Boolean
        Dim AbrirSeguimientoFlota As Boolean
        Dim MostrarAlertasMapa As Boolean
        Dim FormatoEstadoFlota As String
        Dim TiempoSinReportar As Integer
        Dim ClusterMapa As Boolean
        Dim ClusterZoom As Integer
        Dim MostrarSubIconoSR As Boolean
        Dim MostrarUnidadesCercanas As Boolean
        Dim EstiloMostrarAlertas As String
        Dim PicoPlaca As Boolean
        Dim PicoPlacaLocalidades As String
        Dim PaisEstadoFlota As String
        Dim PaisVisorAlertas As String
        Dim PaisDashBoard As String
        Dim AutoFit As Boolean
        Dim TiempoAlertarON As Integer
        Dim TiempoAlertarOFF As Integer
        Dim MaxDiasReportes As Integer
        Dim EditarEtiquetasSub As Boolean
        Dim ConfigActivoSub As Boolean
        Dim ImagenRecorrido As Boolean
        Dim AutoCodigoPunto As Boolean
    End Structure

    <Serializable>
    Public Structure ConfigEA
        Dim EA1_Etiqueta As String
        Dim EA1_Unidad As String
        Dim EA1_EscalaMIN As Double
        Dim EA1_EscalaMAX As Double
        Dim EA1_RangoMin As Double
        Dim EA1_RangoMax As Double

        Dim EA2_Etiqueta As String
        Dim EA2_Unidad As String
        Dim EA2_EscalaMIN As Double
        Dim EA2_EscalaMAX As Double
        Dim EA2_RangoMin As Double
        Dim EA2_RangoMax As Double

        Dim EA3_Etiqueta As String
        Dim EA3_Unidad As String
        Dim EA3_EscalaMIN As Double
        Dim EA3_EscalaMAX As Double
        Dim EA3_RangoMin As Double
        Dim EA3_RangoMax As Double

        Dim SA1_Etiqueta As String
        Dim SA1_Unidad As String
        Dim SA1_EscalaMIN As Double
        Dim SA1_EscalaMAX As Double
        Dim SA1_RangoMin As Double
        Dim SA1_RangoMax As Double

        Dim SA2_Etiqueta As String
        Dim SA2_Unidad As String
        Dim SA2_EscalaMIN As Double
        Dim SA2_EscalaMAX As Double
        Dim SA2_RangoMin As Double
        Dim SA2_RangoMax As Double

        Dim SA3_Etiqueta As String
        Dim SA3_Unidad As String
        Dim SA3_EscalaMIN As Double
        Dim SA3_EscalaMAX As Double
        Dim SA3_RangoMin As Double
        Dim SA3_RangoMax As Double
    End Structure

    Public DatosConfigUsuario As ConfigUsuario
    Public DatosConfigSubUsuario As ConfigSubUsuario
    Public DatosEAActivo As ConfigEA

    Function FormatoHora(Hora As String, Formato As String, Optional ByVal Separador As String = ":") As String
        Try
            Dim tmpHora As String()
            Dim nHora As String = ""

            tmpHora = Hora.Split(Separador)

            If Formato = "H M" Then
                If tmpHora("0") = "00" Then
                    nHora = tmpHora(1) & "m"
                Else
                    nHora = tmpHora(0) & "h " & tmpHora(1) & "m"
                End If
            End If

            Return nHora
        Catch ex As Exception
            Return Hora
        End Try
    End Function

    Private Function Desencriptar(cipherText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return cipherText
    End Function

    Private Function Encriptar(clearText As String) As String
        Dim EncryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using

        Return clearText
    End Function

    Public Function ConsultarSMSOperadora(ByVal Numero As String) As String
        Dim Operador As String = ""
        Dim oCon As SqlConnection
        Dim SQL As String = String.Empty

        Try
            oCon = Nothing
            oCon = New SqlConnection(AppSettings("CadenaConexionSMS"))
            oCon.Open()

            SQL = "spsmsgateway '" & Numero & "'"

            Operador = New SqlCommand(SQL, oCon).ExecuteScalar()

            If Operador = "0" Then
                Operador = "Claro"
            ElseIf Operador = "1" Then
                Operador = "Movistar"
            Else
                Operador = "Otro"
            End If

            Return Operador

            oCon.Close()
            oCon.Dispose()

            oCon = Nothing
        Catch ex As Exception

        End Try
    End Function

    Public Function getValorESV(idusuario As Integer, idactivo As Integer, NombreEtiqueta As String, Valor As Double) As String
        Try


            Dim Resultado As Double = 0.0
            Dim ValorFinal As String = ""
            Dim Unidad As String = ""
            Dim Etiqueta As String = ""

            ValorFinal = ""
            Unidad = ""
            Etiqueta = ""

            If NombreEtiqueta = "EA1" Then
                Resultado = 0
            End If

        Catch ex As Exception

        End Try
    End Function

    Public Sub EnviarMensajeJavascript(ByVal Pagina As Page,
                                           ByVal Msj As String)

        ScriptManager.RegisterStartupScript(Pagina, Pagina.GetType(), Guid.NewGuid().ToString(), "javascript:alert('" & Msj & "');", True)
    End Sub

    Public Sub CrearEjecutarFuncionesJavascript(ByVal Pagina As Page,
                                                  ByVal CodigoJavascript As String,
                                                  ByVal EjecutarFuncion As String)

        Dim scriptString As String = vbCrLf & "<script language=JavaScript>"
        scriptString += vbCrLf & CodigoJavascript
        scriptString += vbCrLf & "</script>"
        scriptString += vbCrLf & EjecutarFuncion

        If (Not Pagina.IsClientScriptBlockRegistered("clientScript")) Then
            Pagina.RegisterStartupScript("clientScript", scriptString)
        End If
    End Sub

    Public Sub EjecutarFuncionJavascript(ByVal Pagina As Page, ByVal Funcion As String)

        Dim scriptString As String = vbCrLf & "<script language=JavaScript>"
        scriptString += vbCrLf & Funcion
        scriptString += vbCrLf & "</script>"

        If (Not Pagina.IsClientScriptBlockRegistered("clientScript")) Then
            Pagina.RegisterStartupScript("clientScript", scriptString)
        End If
    End Sub

    Public Function ANSI2Fecha(ByVal Fecha As String) As DateTime
        Dim tmpFecha() As String = Split(Fecha, " ")
        Dim nFecha As DateTime

        Dim Año As String = tmpFecha(0).Substring(0, 4)
        Dim mes As String = tmpFecha(0).Substring(4, 2)
        Dim Dia As String = tmpFecha(0).Substring(6, 2)

        nFecha = New DateTime(Año,
                     mes,
                     Dia,
                     tmpFecha(1).Split(":")(0),
                     tmpFecha(1).Split(":")(1),
                     tmpFecha(1).Split(":")(2))

        Año = Nothing
        mes = Nothing
        Dia = Nothing

        tmpFecha = Nothing

        Return nFecha
    End Function

    Public Function Fecha2ANSI(ByVal Fecha As DateTime,
                          Optional ByVal SoloFecha As Boolean = True) As String
        Dim tmpFecha As String = ""

        Try
            With Fecha
                tmpFecha = .Year.ToString()

                If .Month.ToString.Length = 1 Then
                    tmpFecha &= "0" & .Month.ToString()
                Else
                    tmpFecha &= .Month.ToString()
                End If

                If .Day.ToString.Length = 1 Then
                    tmpFecha &= String.Format("0{0} ",
                                              .Day)
                Else
                    tmpFecha &= .Day.ToString() & " "
                End If

                If Not SoloFecha Then
                    If .Hour.ToString.Length = 1 Then
                        tmpFecha &= String.Format("0{0}:",
                                                  .Hour)
                    Else
                        tmpFecha &= .Hour.ToString() & ":"
                    End If

                    If .Minute.ToString.Length = 1 Then
                        tmpFecha &= String.Format("0{0}:",
                                                  .Minute)
                    Else
                        tmpFecha &= .Minute.ToString() & ":"
                    End If

                    If .Second.ToString.Length = 1 Then
                        tmpFecha &= "0" & .Second.ToString()
                    Else
                        tmpFecha &= .Second.ToString()
                    End If
                End If

                If SoloFecha Then
                    tmpFecha = tmpFecha.Trim()
                End If

                Return tmpFecha
            End With
        Catch ex As Exception
            Return "null"
        End Try
    End Function

    Public Function GetKeyGoogle(ByVal Host As String) As String
        If Host Like "*10.100.89.244*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRR_Q1YdF2AGiQCOdlLtasSyrjLqKBRLL6MXUlE9zhZyXH6m1dA-bd-xcw"
        ElseIf Host Like "*172.16.1.27*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRRriTS1ilKshTTL4QzgOatK1NbNTRTC9tAXuCLIGZpTFw5IGo8U5TbolA"
        ElseIf Host Like "*200.25.203.32*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRQUZ4b_IBHxxlqV8Q9FpAO4-bG0kBR0CdScuytBjWhoydVDIMqaNzNIJQ"
        ElseIf Host Like "*10.100.89.208*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRQ0z0Q3PZtlkvmqhgJBVNGvNiYCMxTfdzH9y5-jzjPzFvpuweOw2bMhOg"
        ElseIf Host Like "*172.16.1.26*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRSE1dpV4jVVMPrYRzLagobfhW6yohRXtbor_p_jsx3FD6H1g-gXj4V82g"
        ElseIf Host Like "*www.hunterlojack.com:7777*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRTD9pQNufIRFfeDeeUnaZ3vTbX_IRRYlAbzmHFcv65D0R4vv2kS6eVYbg"
        ElseIf Host Like "*www.huntermonitoreoperu.com*" Then
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRTM8Tg2c1Ub4_l3yKpWtHfYoqUbnhQwt6DOWl5n0rfjtNqFo0tS7vKBBg"
        ElseIf Host Like "*www.huntermonitoreo.com*" Then
            'Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRQiP-N_j_OupzS524U91V1_5lPgBhRk2OEQM2DJmKZqg87arBTL-aTz2g"
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRRO3F5RFnyhyBvrJOUrDm9VBT2VbhTNxEYBnqhEcfXV9AzvS-JuL9GENg"
        Else
            Return "ABQIAAAAo12cG_mtBNlOF2j51KsMaRT3sMLVnSxrcBPem0z8j92mKoX68BTM_0BRAK3wDueZIud5UKcS_drW3w"
        End If
    End Function

    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
            Return input 'If encryption fails, return the unaltered input.
        End Try
    End Function

    'Decrypt a string with AES
    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return input 'If decryption fails, return the unaltered input.
        End Try
    End Function

    Public Function getCadenaConexionPais(IdPais As String, RS As Boolean) As String
        Try
            Select Case IdPais.ToUpper()
                Case "EC"
                    If Not RS Then
                        Return AppSettings("CadenaConexion")
                    Else
                        Return AppSettings("CadenaConexionRS")
                    End If
                Case "PE"
                    If Not RS Then
                        Return AppSettings("CadenaConexionPE")
                    Else
                        Return AppSettings("CadenaConexionRSPE")
                    End If
                Case "CO"
                    If Not RS Then
                        Return AppSettings("CadenaConexionCO")
                    Else
                        Return AppSettings("CadenaConexionRSCO")
                    End If
                Case "PA"
                    If Not RS Then
                        Return AppSettings("CadenaConexionPA")
                    Else
                        Return AppSettings("CadenaConexionRSPA")
                    End If
                Case "MX"
                    If Not RS Then
                        Return AppSettings("CadenaConexionMX")
                    Else
                        Return AppSettings("CadenaConexionRSMX")
                    End If
                Case "CH"
                    If Not RS Then
                        Return AppSettings("CadenaConexionCH")
                    Else
                        Return AppSettings("CadenaConexionRSCH")
                    End If
                Case Else
                    If Not RS Then
                        Return AppSettings("CadenaConexion")
                    Else
                        Return AppSettings("CadenaConexionRS")
                    End If
            End Select
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            If RS Then
                Return AppSettings("CadenaConexion")
            Else
                Return AppSettings("CadenaConexionRS")
            End If
        End Try
    End Function

End Module

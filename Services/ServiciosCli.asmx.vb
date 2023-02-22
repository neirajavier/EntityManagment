Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Math
Imports System.Configuration.ConfigurationSettings
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports System.IO
Imports DevExpress.Office.Utils


' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class ServiciosCli
    Inherits System.Web.Services.WebService

    Private oGeocercasDetalle As New Object
    Private oGeocerca As New Object
    Private oGeocercaL As New Object
    Private oGeocercaC As New Object
    Private oGeocercaP As New Object
    Private oGeocercaR As New Object

    Private oPuntos As New Object
    Private oPuntoInfo As New Object
    Private oPuntoInfoUbicacion As New Object

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetGeocercaDetalle(ByVal IdGeocerca As Integer, ByVal Tipo As Integer, ByVal Nombre As String, ByVal Parametro As String, ByVal Pais As String) As String()
        SyncLock oGeocercasDetalle
            Dim ogeocercas As Geocerca = New Geocerca(Pais) With {
                .FuenteUsuario = "GeoSyS",
                .idGeocerca = IdGeocerca
            }
            Dim Lista As New List(Of String)
            Dim dsGeocercas As New DataSet
            dsGeocercas = ogeocercas.PuntosConsultar()
            dsGeocercas.AcceptChanges()

            Try
                Lista.Add(IdGeocerca.ToString() & ";" & Tipo & ";" & Nombre & ";" & Parametro)
                For Each drow As DataRow In dsGeocercas.Tables(0).Rows
                    Try
                        Lista.Add(drow("Secuencia").ToString() & "_" & drow("Lat").ToString() & "_" & drow("Lon").ToString())
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                Next
                dsGeocercas = Nothing

                Return Lista.ToArray()
            Catch ex As Exception
                Return Nothing
            End Try

            oPuntos = Nothing
        End SyncLock
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setGeoInfo(ByVal IdGeocerca As String,
                               ByVal Nombre As String,
                               ByVal Tipo As String,
                               ByVal Ancho As String,
                               ByVal Clasificacion As String,
                               ByVal IdUsuario As String,
                               ByVal IdSubUsuario As String,
                               ByVal Accion As String,
                               ByVal Indice As String,
                               ByVal Pais As String) As String
        SyncLock oGeocerca
            Dim result As String = ""
            Dim oGeo As New Geocerca(Pais)
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = AES_Decrypt(IdSubUsuario, AppSettings("sk"))
            End If

            If Tipo = "null" Then
                Tipo = "1"
            End If

            If Ancho = "null" Or Ancho = "" Then
                Ancho = "0"
            End If

            Try
                With oGeo
                    .IdUsuario = IdUsuario
                    .FuenteUsuario = "GeoSyS"
                    .IdTipoGeocerca = Tipo
                    .Nombre = Nombre.ToUpper()
                    .Parametro1 = Ancho
                    .Clasificacion = Clasificacion

                    If Accion = "ING" Then
                        .idGeocerca = BD.getLastGeocercaUsuario(CInt(IdUsuario))
                        IdGeocerca = .idGeocerca
                    Else
                        .idGeocerca = IdGeocerca
                    End If

                    If Tipo = 3 Then
                        Try
                            .ActualizarPuntosGeocerca(.idGeocerca, 1, "C")
                        Catch ex As Exception
                            .ActualizarPuntosGeocerca(.idGeocerca, 0, "C")
                        End Try
                    End If

                    .Ejecutar(Accion)
                End With

                result = "OK_" & IdGeocerca.ToString()

                Try
                    oGeo.RepararGeocercasUsuario(CInt(IdUsuario))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Dim oUsuario = New Usuario(Pais) With {
                   .IdUsuario = IdUsuario,
                   .IdSubUsuario = 0
               }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEO",
                                                 Accion & " GEO: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MGEOS",
                                                 Accion & " GEO: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                result = "ERR_-1"
            Finally
                oGeo = Nothing
            End Try
            Return result
        End SyncLock
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setGeoCInfo(ByVal IdGeocerca As String,
                                    ByVal IdUsuario As String,
                                    ByVal IdSubUsuario As String,
                                    ByVal Latitud As String,
                                    ByVal Longitud As String,
                                    ByVal Nombre As String,
                                    ByVal Radio As String,
                                    ByVal Accion As String,
                                    ByVal Pais As String) As String
        SyncLock oGeocercaC
            Dim result As String = ""
            Dim oGeo As New Geocerca(Pais)
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = AES_Decrypt(IdSubUsuario, AppSettings("sk"))

                If IdSubUsuario = "" Then
                    IdSubUsuario = "0"
                End If
            End If

            ConfigurarRegion()
            Try
                'Dim oGeo As New Geocerca()

                With oGeo
                    If BD.ObtenerTipoGeocerca(IdGeocerca) <> 3 Then
                        Try
                            BD.spGeocercaTipoActualizar(IdGeocerca, 3)
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If

                    .IdUsuario = IdUsuario
                    .FuenteUsuario = "GeoSyS"
                    .IdTipoGeocerca = 3
                    .Nombre = Nombre.ToUpper()
                    .Parametro1 = Radio
                    .Latitud = CDbl(Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")))
                    .Longitud = CDbl(Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")))
                    .Radio = CDbl(Radio.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")))

                    .Ejecutar(Accion)

                    If Accion = "ING" Then
                        .idGeocerca = BD.getLastGeocercaUsuario(CInt(IdUsuario))
                        IdGeocerca = .idGeocerca
                    Else
                        .idGeocerca = IdGeocerca
                    End If

                    .EjecutarPuntosGeocercaCircular(0, 0, 0)
                    .EjecutarPuntosGeocercaCircular(Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")),
                                                    Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")),
                                                    Radio.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")))
                End With

                'oGeo = Nothing

                If Accion = "ING" Then
                    Dim Geos = BD.spGeocercaConsultarCli(IdGeocerca)
                    For Each Geo In Geos
                        Try
                            result = Geo.Nombre & "_" & Geo.Dato & "_OK"
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    Geos = Nothing

                    If IdSubUsuario <> "0" Then
                        Try
                            BD.spGeocercaSubUsuarioIngresar(CInt(IdGeocerca), CInt(IdUsuario), CInt(IdSubUsuario))
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If
                Else
                    result = "OK_" &
                    IdGeocerca.ToString() &
                    "_" & Nombre &
                    "_" & Radio.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) &
                    "_" & Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) &
                    "_" & Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                End If

                Try
                    oGeo.RepararGeocercasUsuario(CInt(IdUsuario))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Dim oUsuario = New Usuario(Pais) With {
                   .IdUsuario = IdUsuario,
                   .IdSubUsuario = IdSubUsuario
               }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEO",
                                                 Accion & " Geo: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MGEOS",
                                                 Accion & " Geo: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            Catch ex As Exception
                result = "Error " & ex.Message & " " & Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) & " " & Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) & " " & Radio.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
            End Try

            Return result
        End SyncLock
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setGeoRInfo(ByVal IdGeo As String,
                                    ByVal Latitud1 As String,
                                    ByVal Longitud1 As String,
                                    ByVal Latitud2 As String,
                                    ByVal Longitud2 As String,
                                    ByVal Latitud3 As String,
                                    ByVal Longitud3 As String,
                                    ByVal Latitud4 As String,
                                    ByVal Longitud4 As String,
                                    ByVal Nombre As String,
                                    ByVal IdUsuario As String,
                                    ByVal Accion As String,
                                    ByVal Pais As String) As String
        Dim result As String = ""
        Try
            Dim oGeo As New Geocerca(Pais)
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            With oGeo
                .IdUsuario = IdUsuario
                .FuenteUsuario = "GeoSyS"
                .IdTipoGeocerca = 1
                .Nombre = Nombre.ToUpper()
                .Parametro1 = 0
                .Ejecutar(Accion)

                If Accion = "ING" Then
                    '.idGeocerca = New OdinDataContext().getLastGeocercaUsuario(CInt(IdUsuario))
                    .idGeocerca = BD.getLastGeocercaUsuario(CInt(IdUsuario))
                Else
                    .idGeocerca = IdGeo
                    .EjecutarPuntosGeocerca("ELM",
                                            0)
                End If

                'Punto1
                .Secuencia = 1
                .Latitud = Latitud1
                .Longitud = Longitud1
                .EjecutarPuntosGeocerca("ING", 0)

                'Punto2
                .Secuencia = 2
                .Latitud = Latitud2
                .Longitud = Longitud2
                .EjecutarPuntosGeocerca("ING", 0)

                'Punto3
                .Secuencia = 3
                .Latitud = Latitud3
                .Longitud = Longitud3
                .EjecutarPuntosGeocerca("ING", 0)

                'Punto4
                .Secuencia = 4
                .Latitud = Latitud4
                .Longitud = Longitud4
                .EjecutarPuntosGeocerca("ING", 0)

                'PuntoF
                .Secuencia = 5
                .Latitud = Latitud1
                .Longitud = Longitud1
                .EjecutarPuntosGeocerca("ING", 0)

                Try
                    .ActualizarPuntosGeocerca(.idGeocerca, 1)
                Catch ex As Exception
                    .ActualizarPuntosGeocerca(.idGeocerca, 0)
                End Try

                Try
                    oGeo.RepararGeocercasUsuario(CInt(IdUsuario))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Dim oUsuario = New Usuario(Pais) With {
                  .IdUsuario = IdUsuario,
                  .IdSubUsuario = 0
                }

                If True Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEO",
                                                 Accion & " Geo: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEOS",
                                                 Accion & " Geo: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), 0),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            End With

            oGeo = Nothing

            result = "OK"
        Catch ex As Exception
            result = "Error"
        End Try

        Return result
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setGeoLInfo(ByVal IdGeocerca As String,
                                ByVal Nombre As String,
                                ByVal Tipo As String,
                                ByVal Ancho As String,
                                ByVal Clasificacion As String,
                                ByVal IdUsuario As String,
                                ByVal IdSubUsuario As String,
                                ByVal Puntos As String,
                                ByVal Accion As String,
                                ByVal Pais As String) As String
        SyncLock oGeocercaL
            Dim result As String = ""
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = AES_Decrypt(IdSubUsuario, AppSettings("sk"))

                If IdSubUsuario = "" Then
                    IdSubUsuario = "0"
                End If
            End If

            Try

                Dim oGeo As New Geocerca(Pais)
                Dim tmpPuntos As New JArray()
                Dim hPuntos As New Hashtable
                tmpPuntos = JsonConvert.DeserializeObject(Puntos)

                If Ancho = "0" Then
                    Ancho = "50"
                End If

                With oGeo
                    If BD.ObtenerTipoGeocerca(.idGeocerca) <> Tipo Then
                        Try
                            BD.spGeocercaTipoActualizar(IdGeocerca, Tipo)
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If

                    .IdUsuario = IdUsuario
                    .FuenteUsuario = "GeoSyS"
                    .IdTipoGeocerca = 2
                    .Nombre = Nombre.ToUpper()
                    .Parametro1 = Ancho
                    .Ejecutar(Accion)

                    If Accion = "ING" Then
                        .idGeocerca = BD.getLastGeocercaUsuario(CInt(IdUsuario))
                        IdGeocerca = .idGeocerca
                    Else
                        .idGeocerca = IdGeocerca
                        .EjecutarPuntosGeocerca("ELM", .Parametro1)
                    End If

                    Dim Ind As Integer = 1
                    Dim dspunto As New DataSet()
                    hPuntos.Clear()
                    For Each tPunto In tmpPuntos
                        Try
                            .Latitud = tPunto.Item("lat").ToString().Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                            .Longitud = tPunto.Item("lng").ToString().Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                            If Not hPuntos.ContainsKey(.Latitud.ToString() & " " & .Longitud.ToString()) Then
                                .Secuencia = Ind
                                hPuntos.Add(.Latitud.ToString() & " " & .Longitud.ToString(), Ind)
                                .EjecutarPuntosGeocerca("ING", .Parametro1)
                                Ind += 1
                            End If
                            Console.WriteLine(tPunto)

                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    Ind = Nothing

                    Try
                        .ActualizarPuntosGeocerca(.idGeocerca,
                                                        1,
                                                        "L")
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        .ActualizarPuntosGeocerca(.idGeocerca,
                                                        0,
                                                        "L")
                    End Try
                End With

                If Accion = "ING" Then
                    Dim Geos = BD.spGeocercaConsultarCli(IdGeocerca)
                    For Each Geo In Geos
                        Try
                            result = Geo.Nombre & "_" & Geo.Dato & "_OK"
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    Geos = Nothing

                    If IdSubUsuario <> "0" Then
                        Try
                            BD.spGeocercaSubUsuarioIngresar(CInt(IdGeocerca), CInt(IdUsuario), CInt(IdSubUsuario))
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If
                Else
                    result = "OK_" &
                        IdGeocerca.ToString() &
                        "_" & Nombre &
                        "_" & Ancho.ToString() &
                        "_" & "0".Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) &
                        "_" & "0".Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                End If

                Try
                    oGeo.RepararGeocercasUsuario(CInt(IdUsuario))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Dim oUsuario = New Usuario(Pais) With {
                   .IdUsuario = IdUsuario,
                   .IdSubUsuario = IdSubUsuario
               }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEO",
                                                 Accion & " Geo: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MGEOS",
                                                 Accion & " Geo: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                result = "ERR_-1"
            End Try
            Return result
        End SyncLock
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setGeoPInfo(ByVal IdGeocerca As String,
                                ByVal Nombre As String,
                                ByVal Tipo As String,
                                ByVal Ancho As String,
                                ByVal Clasificacion As String,
                                ByVal IdUsuario As String,
                                ByVal IdSubUsuario As String,
                                ByVal Puntos As String,
                                ByVal Accion As String,
                                ByVal Pais As String) As String
        SyncLock oGeocercaP
            Dim result As String = ""
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = AES_Decrypt(IdSubUsuario, AppSettings("sk"))

                If IdSubUsuario = "" Then
                    IdSubUsuario = "0"
                End If
            End If

            Try
                Dim oGeo As New Geocerca(Pais)
                Dim tmpPuntos As New JArray()
                Dim hPuntos As New Hashtable
                tmpPuntos = JsonConvert.DeserializeObject(Puntos)

                'If Ancho = "0" Then
                '    Ancho = "50"
                'End If
                Ancho = "0"
                With oGeo
                    If BD.ObtenerTipoGeocerca(.idGeocerca) <> Tipo Then
                        Try
                            BD.spGeocercaTipoActualizar(IdGeocerca, Tipo)
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If

                    .IdUsuario = IdUsuario
                    .FuenteUsuario = "GeoSyS"
                    .IdTipoGeocerca = 1
                    .Nombre = Nombre.ToUpper()
                    .Parametro1 = Ancho
                    .Ejecutar(Accion)

                    If Accion = "ING" Then
                        .idGeocerca = BD.getLastGeocercaUsuario(CInt(IdUsuario))
                        IdGeocerca = .idGeocerca
                    Else
                        .idGeocerca = IdGeocerca
                        .EjecutarPuntosGeocerca("ELM", .Parametro1)
                    End If

                    Dim Ind As Integer = 1
                    Dim dspunto As New DataSet()
                    hPuntos.Clear()
                    For Each tPunto In tmpPuntos
                        Try
                            .Latitud = tPunto.Item("lat").ToString().Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                            .Longitud = tPunto.Item("lng").ToString().Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                            If Not hPuntos.ContainsKey(.Latitud.ToString() & " " & .Longitud.ToString()) Then
                                .Secuencia = Ind
                                hPuntos.Add(.Latitud.ToString() & " " & .Longitud.ToString(), Ind)
                                .EjecutarPuntosGeocerca("ING", .Parametro1)
                                Ind += 1
                            End If
                            Console.WriteLine(tPunto)

                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    Ind = Nothing

                    Try
                        .ActualizarPuntosGeocerca(.idGeocerca,
                                                        1)
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                        .ActualizarPuntosGeocerca(.idGeocerca,
                                                        0)
                    End Try
                End With

                If Accion = "ING" Then
                    Dim Geos = BD.spGeocercaConsultarCli(IdGeocerca)
                    For Each Geo In Geos
                        Try
                            result = Geo.Nombre & "_" & Geo.Dato & "_OK"
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    Next
                    Geos = Nothing

                    If IdSubUsuario <> "0" Then
                        Try
                            BD.spGeocercaSubUsuarioIngresar(CInt(IdGeocerca), CInt(IdUsuario), CInt(IdSubUsuario))
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If
                Else
                    result = "OK_" &
                        IdGeocerca.ToString() &
                        "_" & Nombre &
                        "_" & Ancho.ToString() &
                        "_" & "0".Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC")) &
                        "_" & "0".Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))
                End If

                Try
                    oGeo.RepararGeocercasUsuario(CInt(IdUsuario))
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

                Dim oUsuario = New Usuario(Pais) With {
                  .IdUsuario = IdUsuario,
                  .IdSubUsuario = IdSubUsuario
                }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(CInt(IdUsuario)),
                                                 "MGEO",
                                                 Accion & " Geo: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MGEOS",
                                                 Accion & " Gep: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                result = "ERR_-1"
            End Try
            Return result
        End SyncLock
    End Function

    Private Sub ConfigurarRegion()
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("Es-EC", False)
            Dim FormatoNumeros As New NumberFormatInfo
            Dim FormatoFecha As New DateTimeFormatInfo

            With FormatoNumeros
                .CurrencyDecimalDigits = 2
                .CurrencyDecimalSeparator = "."
                .CurrencyGroupSeparator = ","
            End With

            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat = FormatoNumeros
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setPointInfo(ByVal IdPunto As String,
                                 ByVal Latitud As String,
                                 ByVal Longitud As String,
                                 ByVal Nombre As String,
                                 ByVal Descripcion As String,
                                 ByVal Codigo As String,
                                 ByVal IdUsuario As String,
                                 ByVal IdSubUsuario As String,
                                 ByVal IdColor As String,
                                 ByVal IdCategoria As String,
                                 ByVal IdSubCategoria As String,
                                 ByVal Celular As String,
                                 ByVal Email As String,
                                 ByVal Accion As String,
                                 ByVal Indice As String,
                                 ByVal Pais As String) As String
        SyncLock oPuntoInfo
            Dim result As String = ""
            Dim oPunto As Punto
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = Funciones.AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = Funciones.AES_Decrypt(IdSubUsuario, AppSettings("sk"))
            End If

            If IdColor = "" Then
                IdColor = "0"
            End If

            If IdSubUsuario = "" Then
                IdSubUsuario = "0"
            End If

            Try
                oPunto = New Punto(Pais) With {
                    .IdPunto = IdPunto,
                    .FuenteUsuario = "GeoSyS",
                    .Nombre = Nombre,
                    .Latitud = CDbl(Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))),
                    .Longitud = CDbl(Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))),
                    .IdUsuario = IdUsuario,
                    .Descripcion = Descripcion,
                    .IdPuntoCliente = Codigo,
                    .idColor = IdColor,
                    .idCategoria = IdCategoria,
                    .idSubCategoria = IdSubCategoria,
                    .Email = Email,
                    .SMS = Celular
                }

                Dim oUsuario = New Usuario(Pais) With {
                    .IdUsuario = IdUsuario,
                    .IdSubUsuario = IdSubUsuario
                }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(IdUsuario),
                                                 "MPTO",
                                                 Accion & " PTO: " & Nombre & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MPTOS",
                                                 Accion & " PTO: " & Nombre & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing

                oPunto.Ejecutar(Accion)
                oPunto = Nothing

                If Accion = "ING" Then
                    'IdPunto = New OdinDataContext().getLastPuntoUsuario(IdUsuario)
                    IdPunto = BD.getLastPuntoUsuario(IdUsuario)
                    result = "OK_" & IdPunto.ToString() & "_" & Nombre & "_" & Latitud.ToString() & "_" & Longitud.ToString()

                    If IdSubUsuario <> "0" Then
                        Try
                            BD.spPuntoSubUsuarioIngresar(CInt(IdPunto), CInt(IdUsuario), CInt(IdSubUsuario))


                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                        End Try
                    End If
                Else
                    result = "OK_" & IdPunto.ToString()
                End If
            Catch ex As Exception
                result = "Error_-1_" & ex.Message & "_" & Latitud.ToString() & "_" & Longitud.ToString()
            Finally
                oPunto = Nothing
            End Try

            Return result
        End SyncLock
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function setPointInfoUbicacion(ByVal IdPunto As String,
                               ByVal Latitud As String,
                               ByVal Longitud As String,
                               ByVal IdUsuario As String,
                               ByVal IdSubUsuario As String,
                               ByVal Indice As String,
                               ByVal Pais As String) As String
        SyncLock oPuntoInfoUbicacion
            Dim result As String = ""
            Dim oPunto As Punto
            Dim BD As New OdinDataContext(Globales.getCadenaConexionPais(Pais, False))

            If AppSettings("Protect") = "1" Then
                IdUsuario = Funciones.AES_Decrypt(IdUsuario, AppSettings("sk"))
                IdSubUsuario = Funciones.AES_Decrypt(IdSubUsuario, AppSettings("sk"))
            End If

            Try
                oPunto = New Punto(Pais) With {
                    .IdPunto = IdPunto,
                    .FuenteUsuario = "GeoSyS",
                    .Latitud = CDbl(Latitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))),
                    .Longitud = CDbl(Longitud.Replace(AppSettings("SEPARADOR_MIL"), AppSettings("SEPARADOR_DEC"))),
                    .IdUsuario = IdUsuario
                }
                oPunto.Ejecutar("UBI")
                oPunto = Nothing

                result = "OK_" & IdPunto.ToString()

                If IdSubUsuario = "" Then
                    IdSubUsuario = "0"
                End If

                Dim oUsuario = New Usuario(Pais) With {
                    .IdUsuario = IdUsuario,
                    .IdSubUsuario = IdSubUsuario
                }

                If IdSubUsuario = "0" Then
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(IdUsuario),
                                                 "MPTO",
                                                 "MOD UBI Pto: " & IdPunto.ToString() & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008")
                Else
                    oUsuario.BitacoraIngresar(BD.getUsuarioxID(IdUsuario),
                                                 "MPTOS",
                                                 "MOD UBI Pto: " & IdPunto.ToString() & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008")
                End If
                oUsuario = Nothing
            Catch ex As Exception
                result = "Error_-1_" & ex.Message & "_" & Latitud.ToString() & "_" & Longitud.ToString()
            Finally
                oPunto = Nothing
            End Try

            Return result
        End SyncLock
    End Function

End Class
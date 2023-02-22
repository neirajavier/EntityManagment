Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports System.Configuration.ConfigurationSettings
Imports System.Globalization


' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Info
    Inherits System.Web.Services.WebService

    Public Result As String = ""
    Public Records As New Object
    Public Message As String = ""

    Public oGrupo As New Object

#Region "Clases"
    Public Class Grupo
        Public idgrupo As Integer
        Public grupo As String
        Public descripcion As String
        Public idusuario As Integer
        Public unidades As Integer
        Public fechaingreso As String
    End Class
#End Region

#Region "Estructuras"
    Public Structure sResultado
        Public Result As String
        Public Records As Object
        Public Record As Object
        Public TotalRecordCount As Integer
    End Structure

    Public Structure sError
        Public Result As String
        Public Message As Object
    End Structure
#End Region

#Region "Grupos"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function EliminarGrupo(idgrupo As Integer) As Object
        SyncLock oGrupo
            Dim Lista As New List(Of Grupo)
            Dim BD As New OdinDataContext()
            Dim Contador As Integer = 0
            Dim resGrupo As sResultado
            Dim resError As sError

            Dim IdUsuario As Integer = Session("IdUsuario")
            Dim IdSubUsuario As Integer = Session("IdSubUsuario")

            Try
                BD.spGrupoEliminar(idgrupo)

                If IdSubUsuario = "0" Then
                    BD.spLogUsuarioGeoIngresar(BD.getUsuarioxID(IdUsuario),
                                                 "MGRP",
                                                 "ELM GRP: " & idgrupo.ToString & ",USR: " & BD.getUsuarioxID(IdUsuario),
                                                 "Host",
                                                 "127008",
                                                 My.MySettings.Default.IdAplicacion)
                Else
                    BD.spLogUsuarioGeoIngresar(BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "MGRPS",
                                                 "ELM GRP: " & idgrupo.ToString() & ",SUSR: " & BD.getSubUsuarioXID(CInt(IdUsuario), CInt(IdSubUsuario)),
                                                 "Host",
                                                 "127008",
                                                 My.MySettings.Default.IdAplicacion)
                End If

                With resGrupo
                    .Result = "OK"
                End With
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                With resError
                    .Result = "ERROR"
                    .Message = ex.Message
                End With
            End Try

            Return resGrupo
        End SyncLock
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ListarGrupo(jtStartIndex As Integer, jtPageSize As Integer, jtSorting As String, grupo As String) As Object
        SyncLock oGrupo
            Dim Lista As New List(Of Grupo)
            Dim BD As New OdinDataContext()
            Dim Contador As Integer = 0
            Dim resGrupo As sResultado
            Dim resError As sError

            Dim IdUsuario As Integer = Session("IdUsuario")
            Dim IdSubUsuario As Integer = Session("IdSubUsuario")

            If IdSubUsuario = 0 Then
                Dim lGrupos = BD.spGrupoConsultar(IdUsuario, grupo)

                If jtSorting Like "*DESC*" Then
                    For Each lGrupo In lGrupos.Reverse()
                        Try
                            Lista.Add(New Grupo() With {
                            .idgrupo = lGrupo.IdGrupo,
                            .grupo = lGrupo.Grupo.ToUpper(),
                            .descripcion = lGrupo.Descripcion.ToUpper(),
                            .idusuario = lGrupo.IdUsuario,
                            .unidades = lGrupo.Cantidad,
                            .fechaingreso = lGrupo.FechaIngreso
                        })

                            Contador += 1
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)

                            With resError
                                .Result = "ERROR"
                                .Message = ex.Message
                            End With
                        End Try
                    Next
                Else
                    For Each lGrupo In lGrupos
                        Try
                            Lista.Add(New Grupo() With {
                            .idgrupo = lGrupo.IdGrupo,
                            .grupo = lGrupo.Grupo.ToUpper(),
                            .descripcion = lGrupo.Descripcion.ToUpper(),
                            .idusuario = lGrupo.IdUsuario,
                            .unidades = lGrupo.Cantidad,
                            .fechaingreso = lGrupo.FechaIngreso
                        })

                            Contador += 1
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)

                            With resError
                                .Result = "ERROR"
                                .Message = ex.Message
                            End With
                        End Try
                    Next
                End If

                With resGrupo
                    .Result = "OK"
                    .Records = Lista
                    .TotalRecordCount = Lista.Count
                End With
            End If

            Return resGrupo
        End SyncLock
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ActualizarGrupo(record As Grupo) As Object
        SyncLock oGrupo
            Dim Lista As New List(Of Grupo)
            Dim BD As New OdinDataContext()
            Dim Contador As Integer = 0
            Dim resGrupo As sResultado
            Dim resError As sError

            Dim IdUsuario As Integer = Session("IdUsuario")
            Dim IdSubUsuario As Integer = Session("IdSubUsuario")

            Try
                With record
                    BD.spGrupoActualizar(.idgrupo, .grupo, .descripcion, IdUsuario, Session("Usuario"))

                    Try
                        .unidades = BD.getCountUnidadesGrupo(.idgrupo)
                    Catch ex As Exception
                        .unidades = 0
                    End Try
                End With

                Lista.Add(record)
                With resGrupo
                    .Result = "OK"
                    .Records = Lista
                End With
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                With resError
                    .Result = "ERROR"
                    .Message = ex.Message
                End With
            End Try

            Return resGrupo
        End SyncLock
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function IngresarGrupo(record As Grupo) As Object
        SyncLock oGrupo
            Dim Lista As New List(Of Grupo)
            Dim BD As New OdinDataContext()
            Dim Contador As Integer = 0
            Dim resGrupo As sResultado
            Dim resError As sError
            Dim nRecord As Grupo = New Grupo()

            Dim IdUsuario As Integer = Session("IdUsuario")
            Dim IdSubUsuario As Integer = Session("IdSubUsuario")

            Try
                With record
                    BD.spGrupoIngresar(.grupo, .descripcion, IdUsuario, Session("Usuario"))
                End With

                With nRecord
                    .idgrupo = BD.maxGrupoUsuario(IdUsuario)
                    .unidades = 0
                    .grupo = record.grupo
                    .descripcion = record.descripcion
                    .idusuario = .idusuario
                    .fechaingreso = Now.ToShortDateString() & " " & Now.ToShortTimeString()
                End With

                With resGrupo
                    .Result = "OK"
                    .Record = nRecord
                End With
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                With resError
                    .Result = "ERROR"
                    .Message = ex.Message
                End With
            End Try

            Return resGrupo
        End SyncLock
    End Function
#End Region

#Region "Categoria Puntos"

#End Region

#Region "SubCategoria Puntos"

#End Region

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
End Class
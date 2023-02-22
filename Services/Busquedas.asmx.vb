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
Public Class Busquedas
    Inherits System.Web.Services.WebService

    Private oGeocercasDetalle As New Object
    Private oPuntos As New Object

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function GetGeocercaDetalle(ByVal IdGeocerca As Integer, ByVal Tipo As Integer, ByVal Nombre As String, ByVal Parametro As String) As String()
        SyncLock oGeocercasDetalle
            Dim ogeocercas As Geocerca = New Geocerca() With {
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

End Class
Imports System
Imports System.Collections.Generic

Public Class PuntoRef
    Public Property IdPunto As Integer
    Public Property Nombre As String
    Public Property Descripcion As String
    Public Property Latitud As Double
    Public Property Longitud As Double
    Public Property IdIcono As Nullable(Of Integer)
    Public Property IdUsuario As Nullable(Of Integer)
    Public Property GeoPoint As System.Data.Entity.Spatial.DbGeography
    Public Property Estado As String
    Public Property UsuarioIngreso As String
    Public Property FechaIngreso As Nullable(Of Date)
    Public Property UsuarioModificacion As String
    Public Property FechaModificacion As Nullable(Of Date)
    Public Property IdPuntoUsadoPorCliente As String
    Public Property idColor As Nullable(Of Integer)
    Public Property idCategoria As Nullable(Of Integer)
    Public Property idSubCategoria As Nullable(Of Integer)
    Public Property CategoriaGeneral As Nullable(Of Boolean)
    Public Property Foto As Byte()
    Public Property Celular As String
    Public Property Email As String
End Class

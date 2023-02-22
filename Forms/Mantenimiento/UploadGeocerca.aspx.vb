Imports System.IO
Imports System.Xml
Imports DevExpress.Web
Imports Kml2Sql.Mapping
Imports SharpKml.Base
Imports SharpKml.Engine

Public Class UploadedFilesStorage
    Public Property Path() As String
    Public Property Key() As String
    Public Property LastUsageTime() As DateTime
    Public Property Files() As IList(Of UploadedFileInfo)
End Class

Public Class UploadedFileInfo
    Public Property UniqueFileName() As String
    Public Property OriginalFileName() As String
    Public Property FilePath() As String
    Public Property FileSize() As String
End Class

Public Class UploadGeocerca
    Inherits System.Web.UI.Page

    Private BD As New OdinDataContext()

    Protected Property SubmissionID() As String
        Get
            Return HiddenField.Get("SubmissionID").ToString()
        End Get
        Set(ByVal value As String)
            HiddenField.Set("SubmissionID", value)
        End Set
    End Property

    Private ReadOnly Property UploadedFilesStorage() As UploadedFilesStorage
        Get
            Return UploadControlHelper.GetUploadedFilesStorageByKey(SubmissionID)
        End Get
    End Property

    Protected Sub ProcessSubmit(ByVal fileInfos As List(Of UploadedFileInfo))
        Try
            For Each fileInfo As UploadedFileInfo In fileInfos
                ' process uploaded files here
                'Dim fileContent() As Byte = File.ReadAllBytes(fileInfo.FilePath)
                Dim path As String = MapPath("~/Upload/Temp/" + fileInfo.OriginalFileName)
                Dim fileStream = File.Open(path, FileMode.Open)

                Dim filePath As KmlFile = KmlFile.Load(fileStream)


                'Dim mapper = New Kml2SqlMapper(fileStream)

                'Dim xml_Doc As XmlDocument = New XmlDocument()
                'xml_Doc.Load(MapPath("~/Upload/Temp/" + fileInfo.OriginalFileName))

                'Dim nodeList As XmlNodeList = xml_Doc.ChildNodes
                'Dim node As XmlNode
                'For Each node In nodeList
                '    If node.Name = "coordinates" Or node.Name = "alititudeMode" Then
                '        Console.WriteLine(node.Name + " = " + node.FirstChild.Value + "\r\n")
                '    Else
                '        Console.WriteLine(node.ChildNodes)
                '    End If
                'Next

                Dim Existe As Boolean = False
                Dim IdGeocerca As Integer
                Dim Ind As Integer

                'For Each mapFeature In mapper.GetMapFeatures()
                '    Try
                '        IdGeocerca = 0
                '        'Try
                '        '    Console.WriteLine(mapFeature.Name & "(" & mapFeature.Coordinates.Length & ")")
                '        'Catch ex As Exception
                '        '    Console.WriteLine(ex.Message)
                '        'End Try

                '        If BD.ExisteGeocerca(mapFeature.Name.ToUpper(), Session("IdUsuario")) Then
                '            Console.ForegroundColor = ConsoleColor.Green
                '            Console.WriteLine("Ya Existe " & mapFeature.Name & " " & IdGeocerca)
                '            Console.ForegroundColor = ConsoleColor.Gray
                '        Else
                '            Console.ForegroundColor = ConsoleColor.Red
                '            Console.WriteLine("No Existe " & mapFeature.Name & " " & IdGeocerca)
                '            Console.ForegroundColor = ConsoleColor.Gray
                '            IdGeocerca = 0
                '            BD.spGeocercaIngresar(mapFeature.Name.ToUpper, Session("IdUsuario"), 1, "0", "Admin", "U", IdGeocerca)
                '            Console.WriteLine(IdGeocerca.ToString() & " (Nueva) para " & mapFeature.Name)
                '            Ind = 1
                '            If IdGeocerca > 0 Then
                '                BD.spPuntos_GeocercaLimpiar(IdGeocerca)
                '                'For Each cfeature In mapFeature.Coordinates
                '                '    Try
                '                '        BD.spPuntosGeocercaIngresar(IdGeocerca, Ind, cfeature.Latitude.ToString(), cfeature.Longitude.ToString())
                '                '    Catch ex As Exception
                '                '        Console.WriteLine(ex.Message)
                '                '    End Try
                '                '    Ind += 1
                '                'Next
                '                Try
                '                    BD.spPuntosGeocercaGeo(IdGeocerca, 1, "P")
                '                Catch ex As Exception
                '                    Try
                '                        BD.spPuntosGeocercaGeo(IdGeocerca, 0, "P")
                '                    Catch ex1 As Exception
                '                        Console.ForegroundColor = ConsoleColor.Red
                '                        Console.WriteLine("No se Puede Crear GeoPoint para " & mapFeature.Name & " " & Ind.ToString())
                '                        Console.ForegroundColor = ConsoleColor.Gray
                '                    End Try
                '                End Try
                '                Console.WriteLine("Puntos Ingresados para " & mapFeature.Name & " " & Ind.ToString())
                '            End If
                '        End If
                '        Console.WriteLine(Chr(13))
                '    Catch ex As Exception
                '        Console.WriteLine(ex.Message)
                '    End Try
                'Next

            Next fileInfo
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.WriteLine(ex.StackTrace)
        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            SubmissionID = UploadControlHelper.GenerateUploadedFilesStorageKey()
            UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
        End If
    End Sub

    Protected Sub DocumentsUploadControl_FileUploadComplete(sender As Object, e As FileUploadCompleteEventArgs)
        Dim isSubmissionExpired As Boolean = False
        If UploadedFilesStorage Is Nothing Then
            isSubmissionExpired = True
            UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
        End If
        'UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
        Dim tempFileInfo As UploadedFileInfo = UploadControlHelper.AddUploadedFileInfo(SubmissionID, e.UploadedFile.FileName)

        e.UploadedFile.SaveAs(tempFileInfo.FilePath)

        If e.IsValid Then
            e.CallbackData = tempFileInfo.UniqueFileName & "|" & isSubmissionExpired
        End If

        If e.IsValid Then
            e.UploadedFile.SaveAs(MapPath("~/Upload/Temp/" + e.UploadedFile.FileName))
        End If
    End Sub

    Protected Sub SubmitButton_Click(sender As Object, e As EventArgs)
        Dim resultFileInfos As New List(Of UploadedFileInfo)()
        Dim allFilesExist As Boolean = True

        If UploadedFilesStorage Is Nothing Then
            UploadedFilesTokenBox.Tokens = New TokenCollection()
        End If

        For Each fileName As String In UploadedFilesTokenBox.Tokens
            Dim demoFileInfo As UploadedFileInfo = UploadControlHelper.GetDemoFileInfo(SubmissionID, fileName)
            Dim fileInfo As New FileInfo(demoFileInfo.FilePath)

            If fileInfo.Exists Then
                demoFileInfo.FileSize = UploadControlHelper.FormatSize(fileInfo.Length)
                resultFileInfos.Add(demoFileInfo)
            Else
                allFilesExist = False
            End If
        Next fileName

        If allFilesExist AndAlso resultFileInfos.Count > 0 Then
            ProcessSubmit(resultFileInfos)

            UploadControlHelper.RemoveUploadedFilesStorage(SubmissionID)

            ASPxEdit.ClearEditorsInContainer(FormLayout, True)
        Else
            UploadedFilesTokenBox.ErrorText = "Submit failed because files have been removed from the server due to the 5 minute timeout."
            UploadedFilesTokenBox.IsValid = False
        End If
    End Sub

End Class

Public NotInheritable Class UploadControlHelper
    Private Const DisposeTimeout As Integer = 5
    Private Const TempDirectory As String = "~/Upload/Temp/"
    Private Shared ReadOnly storageListLocker As Object = New Object()

    Private Sub New()
    End Sub
    Private Shared ReadOnly Property Context() As HttpContext
        Get
            Return HttpContext.Current
        End Get
    End Property
    Private Shared ReadOnly Property RootDirectory() As String
        Get
            Return Context.Request.MapPath(TempDirectory)
        End Get
    End Property

    Private Shared uploadedFilesStorageList_Renamed As IList(Of UploadedFilesStorage)
    Private Shared ReadOnly Property UploadedFilesStorageList() As IList(Of UploadedFilesStorage)
        Get
            Return uploadedFilesStorageList_Renamed
        End Get
    End Property

    Shared Sub New()
        uploadedFilesStorageList_Renamed = New List(Of UploadedFilesStorage)()
    End Sub

    Private Shared Function CreateTempDirectoryCore() As String
        Dim uploadDirectory As String = Path.Combine(RootDirectory, Path.GetRandomFileName())
        Directory.CreateDirectory(uploadDirectory)

        Return uploadDirectory
    End Function
    Public Shared Function GetUploadedFilesStorageByKey(ByVal key As String) As UploadedFilesStorage
        SyncLock storageListLocker
            Return GetUploadedFilesStorageByKeyUnsafe(key)
        End SyncLock
    End Function
    Private Shared Function GetUploadedFilesStorageByKeyUnsafe(ByVal key As String) As UploadedFilesStorage
        Dim storage As UploadedFilesStorage = UploadedFilesStorageList.Where(Function(i) i.Key = key).SingleOrDefault()
        If storage IsNot Nothing Then
            storage.LastUsageTime = DateTime.Now
        End If
        Return storage
    End Function
    Public Shared Function GenerateUploadedFilesStorageKey() As String
        Return Guid.NewGuid().ToString("N")
    End Function
    Public Shared Sub AddUploadedFilesStorage(ByVal key As String)
        SyncLock storageListLocker
            Dim storage As UploadedFilesStorage = New UploadedFilesStorage With {.Key = key, .Path = CreateTempDirectoryCore(), .LastUsageTime = DateTime.Now, .Files = New List(Of UploadedFileInfo)()}
            UploadedFilesStorageList.Add(storage)
        End SyncLock
    End Sub
    Public Shared Sub RemoveUploadedFilesStorage(ByVal key As String)
        SyncLock storageListLocker
            Dim storage As UploadedFilesStorage = GetUploadedFilesStorageByKeyUnsafe(key)
            If storage IsNot Nothing Then
                Directory.Delete(storage.Path, True)
                UploadedFilesStorageList.Remove(storage)
            End If
        End SyncLock
    End Sub
    Public Shared Sub RemoveOldStorages()
        If (Not Directory.Exists(RootDirectory)) Then
            Directory.CreateDirectory(RootDirectory)
        End If

        SyncLock storageListLocker
            Dim existingDirectories() As String = Directory.GetDirectories(RootDirectory)
            For Each directoryPath As String In existingDirectories
                Dim storage As UploadedFilesStorage = UploadedFilesStorageList.Where(Function(i) i.Path = directoryPath).SingleOrDefault()
                If storage Is Nothing OrElse (DateTime.Now - storage.LastUsageTime).TotalMinutes > DisposeTimeout Then
                    Directory.Delete(directoryPath, True)
                    If storage IsNot Nothing Then
                        UploadedFilesStorageList.Remove(storage)
                    End If
                End If
            Next directoryPath
        End SyncLock
    End Sub
    Public Shared Function AddUploadedFileInfo(ByVal key As String, ByVal originalFileName As String) As UploadedFileInfo
        Dim currentStorage As UploadedFilesStorage = GetUploadedFilesStorageByKey(key)
        Dim fileInfo As UploadedFileInfo = New UploadedFileInfo With {.FilePath = Path.Combine(currentStorage.Path, Path.GetRandomFileName()), .OriginalFileName = originalFileName, .UniqueFileName = GetUniqueFileName(currentStorage, originalFileName)}
        currentStorage.Files.Add(fileInfo)

        Return fileInfo
    End Function
    Public Shared Function GetDemoFileInfo(ByVal key As String, ByVal fileName As String) As UploadedFileInfo
        Dim currentStorage As UploadedFilesStorage = GetUploadedFilesStorageByKey(key)
        Return currentStorage.Files.Where(Function(i) i.UniqueFileName = fileName).SingleOrDefault()
    End Function
    Public Shared Function GetUniqueFileName(ByVal currentStorage As UploadedFilesStorage, ByVal fileName As String) As String
        Dim baseName As String = Path.GetFileNameWithoutExtension(fileName)
        Dim ext As String = Path.GetExtension(fileName)
        Dim index As Integer = 1

        Do While currentStorage.Files.Any(Function(i) i.UniqueFileName = fileName)
            fileName = String.Format("{0} ({1}){2}", baseName, index, ext)
            index += 1
        Loop

        Return fileName
    End Function

    Public Shared Function FormatSize(ByVal value As Object) As String
        Dim amount As Double = Convert.ToDouble(value)
        Dim unit As String = "KB"
        If amount <> 0 Then
            If amount <= 1024 Then
                amount = 1
            Else
                amount /= 1024
            End If
            If amount > 1024 Then
                amount /= 1024
                unit = "MB"
            End If
            If amount > 1024 Then
                amount /= 1024
                unit = "GB"
            End If
        End If
        Return String.Format("{0:#,0} {1}", Math.Round(amount, MidpointRounding.AwayFromZero), unit)
    End Function

End Class

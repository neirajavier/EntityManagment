Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Web
Imports System.Web.Configuration

Namespace QueryModifier
    Public Class CustomCountQueryProvider
        Implements IQueryProvider

        'Private _internalQueryProvider As IQueryProvider

        'Public Sub New(ByVal internalQueryProvider As IQueryProvider)
        '    _internalQueryProvider = internalQueryProvider
        'End Sub

        'Public Function CreateQuery(expression As Expression) As IQueryable Implements IQueryProvider.CreateQuery
        '    Dim elementType As Type = TypeSystem.GetElementType(expression.Type)
        '    Return CType(Activator.CreateInstance(GetType(QueryUpdater(Of)).MakeGenericType(elementType), New Object() {Me, expression}), IQueryable)
        'End Function

        'Public Function CreateQuery(Of TElement)(expression As Expression) As IQueryable(Of TElement) Implements IQueryProvider.CreateQuery
        '    Return New QueryUpdater(Of TResult)(Me, expression)
        'End Function

        'Public Function Execute(expression As Expression) As Object Implements IQueryProvider.Execute
        '    Dim expressionChecker As CountExpressionChecker = New CountExpressionChecker(expression)

        '    If expressionChecker.IsCountExpression Then
        '        Return GetRowCount()
        '    Else
        '        Return _internalQueryProvider.Execute(expression)
        '    End If
        'End Function

        'Public Function Execute(Of TResult)(expression As Expression) As TResult Implements IQueryProvider.Execute
        '    Dim IsEnumerable As Boolean = (GetType(TResult).Name = "IEnumerable")

        '    If IsEnumerable Then
        '        Return CType(_internalQueryProvider.CreateQuery(expression), TResult)
        '    Else
        '        Return CType(_internalQueryProvider.Execute(expression), TResult)
        '    End If
        'End Function

        'Private Function GetRowCount() As Object
        '    Using connection As SqlConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("PX_DBConnectionString").ConnectionString)

        '        Using command As SqlCommand = New SqlCommand("SELECT max(ROWS) from sysindexes WHERE id = object_id('Puntos_Referencia')", connection)
        '            connection.Open()
        '            Return command.ExecuteScalar()
        '        End Using
        '    End Using
        'End Function

        Private _internalQueryProvider As IQueryProvider

        Public Sub New(ByVal internalQueryProvider As IQueryProvider)
            _internalQueryProvider = internalQueryProvider
        End Sub
        Public Function CreateQuery(ByVal expression As Expression) As IQueryable Implements IQueryProvider.CreateQuery
            Dim elementType As Type = TypeSystem.GetElementType(expression.Type)
            Return CType(Activator.CreateInstance(GetType(QueryUpdater(Of)).MakeGenericType(elementType), New Object() {Me, expression}), IQueryable)
        End Function

        Public Function CreateQuery(Of TResult)(ByVal expression As Expression) As IQueryable(Of TResult) Implements IQueryProvider.CreateQuery
            Return New QueryUpdater(Of TResult)(Me, expression)
        End Function

        Public Function Execute(ByVal expression As Expression) As Object Implements IQueryProvider.Execute
            Dim expressionChecker As CountExpressionChecker = New CountExpressionChecker(expression)
            If expressionChecker.IsCountExpression Then
                Return GetRowCount()
            Else
                Return _internalQueryProvider.Execute(expression)
            End If
        End Function

        Public Function Execute(Of TResult)(ByVal expression As Expression) As TResult Implements IQueryProvider.Execute
            Dim IsEnumerable = Equals(GetType(TResult).Name, "IEnumerable")
            If IsEnumerable Then
                Return _internalQueryProvider.CreateQuery(expression)
            Else
                Return _internalQueryProvider.Execute(expression)
            End If
        End Function

        Private Function GetRowCount() As Object
            Using connection As SqlConnection = New SqlConnection(WebConfigurationManager.ConnectionStrings("PX_DBConnectionString").ConnectionString)
                Using command As SqlCommand = New SqlCommand("SELECT max(ROWS) from sysindexes WHERE id = object_id('Puntos_Referencia')", connection)
                    connection.Open()
                    Return command.ExecuteScalar()
                End Using
            End Using
        End Function


    End Class
End Namespace

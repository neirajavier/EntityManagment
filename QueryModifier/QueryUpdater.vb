Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Web
Imports ArtemisAdmin.QueryModifier

Namespace QueryModifier
    Public Class QueryUpdater(Of TData)
        Implements IOrderedQueryable(Of TData)

        'Public Sub New(ByVal dataSet As IQueryable(Of TData))
        '    Dim query As IQueryable = dataSet.AsQueryable()
        '    Provider = New CustomCountQueryProvider(query.Provider)
        '    Expression = query.Expression
        'End Sub

        'Public Sub New(ByVal provider As IQueryProvider, ByVal expression As Expression)
        '    If provider Is Nothing Then
        '        Throw New ArgumentNullException("provider")
        '    End If

        '    If expression Is Nothing Then
        '        Throw New ArgumentNullException("expression")
        '    End If

        '    If Not GetType(IQueryable(Of TData)).IsAssignableFrom(expression.Type) Then
        '        Throw New ArgumentOutOfRangeException("expression")
        '    End If

        '    provider = provider
        '    expression = expression
        'End Sub

        'Public Property Provider As IQueryProvider
        'Public Property Expression As Expression

        'Public ReadOnly Property ElementType As Type
        '    Get
        '        Return GetType(TData)
        '    End Get
        'End Property

        'Private ReadOnly Property IQueryable_Expression As Expression Implements IQueryable.Expression
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Private ReadOnly Property IQueryable_ElementType As Type Implements IQueryable.ElementType
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Private ReadOnly Property IQueryable_Provider As IQueryProvider Implements IQueryable.Provider
        '    Get
        '        Throw New NotImplementedException()
        '    End Get
        'End Property

        'Public Function GetEnumerator() As IEnumerator(Of TData) Implements IEnumerable(Of TData).GetEnumerator
        '    Return (Provider.Execute(Of IEnumerable(Of TData))(Expression)).GetEnumerator()
        'End Function

        'Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        '    Return (Provider.Execute(Of IEnumerable)(Expression)).GetEnumerator()
        'End Function

        Private _Provider As System.Linq.IQueryProvider, _Expression As System.Linq.Expressions.Expression
        Public Sub New(ByVal dataSet As IQueryable(Of TData))
            Dim query As IQueryable = dataSet.AsQueryable()
            Provider = New CustomCountQueryProvider(query.Provider)
            Expression = query.Expression
        End Sub

        Public Sub New(ByVal provider As IQueryProvider, ByVal expression As Expression)
            If provider Is Nothing Then
                Throw New ArgumentNullException("provider")
            End If

            If expression Is Nothing Then
                Throw New ArgumentNullException("expression")
            End If

            If Not GetType(IQueryable(Of TData)).IsAssignableFrom(expression.Type) Then
                Throw New ArgumentOutOfRangeException("expression")
            End If

            Me.Provider = provider
            Me.Expression = expression
        End Sub

        Public Property Provider As IQueryProvider Implements IQueryable.Provider
            Get
                Return _Provider
            End Get
            Private Set(ByVal value As IQueryProvider)
                _Provider = value
            End Set
        End Property

        Public Property Expression As Expression Implements IQueryable.Expression
            Get
                Return _Expression
            End Get
            Private Set(ByVal value As Expression)
                _Expression = value
            End Set
        End Property

        Public ReadOnly Property ElementType As Type Implements IQueryable.ElementType
            Get
                Return GetType(TData)
            End Get
        End Property

        Public Function GetEnumerator() As IEnumerator(Of TData) Implements IEnumerable(Of TData).GetEnumerator
            Return Provider.Execute(Of IEnumerable(Of TData))(Expression).GetEnumerator()
        End Function

        Private Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Provider.Execute(Of IEnumerable)(Expression).GetEnumerator()
        End Function


    End Class
End Namespace

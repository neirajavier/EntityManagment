Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Web

Namespace QueryModifier
    Public Class CountExpressionChecker
        Inherits ExpressionVisitor

        Private _expression As Expression
        Private _isCountExpression As Boolean

        Public Sub New(ByVal expression As Expression)
            MyBase.New()
            _expression = expression
        End Sub

        Public ReadOnly Property IsCountExpression As Boolean
            Get
                Visit(_expression)
                Return _isCountExpression
            End Get
        End Property

        Protected Overrides Function VisitMethodCall(ByVal node As MethodCallExpression) As Expression
            If node.Method.Name = "Count" Then _isCountExpression = True
            Return MyBase.VisitMethodCall(node)
        End Function
    End Class
End Namespace

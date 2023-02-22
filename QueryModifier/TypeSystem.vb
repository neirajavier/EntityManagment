﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace QueryModifier
    Friend Module TypeSystem
        Friend Function GetElementType(ByVal seqType As Type) As Type
            Dim ienum As Type = FindIEnumerable(seqType)
            If ienum Is Nothing Then Return seqType
            Return ienum.GetGenericArguments()(0)
        End Function

        Private Function FindIEnumerable(ByVal seqType As Type) As Type
            If seqType Is Nothing OrElse seqType = GetType(String) Then Return Nothing
            If seqType.IsArray Then Return GetType(IEnumerable(Of)).MakeGenericType(seqType.GetElementType())

            If seqType.IsGenericType Then
                For Each arg As Type In seqType.GetGenericArguments()
                    Dim ienum As Type = GetType(IEnumerable(Of)).MakeGenericType(arg)

                    If ienum.IsAssignableFrom(seqType) Then
                        Return ienum
                    End If
                Next
            End If

            Dim ifaces As Type() = seqType.GetInterfaces()

            If ifaces IsNot Nothing AndAlso ifaces.Length > 0 Then

                For Each iface As Type In ifaces
                    Dim ienum As Type = FindIEnumerable(iface)
                    If ienum IsNot Nothing Then Return ienum
                Next
            End If

            If seqType.BaseType IsNot Nothing AndAlso seqType.BaseType <> GetType(Object) Then
                Return FindIEnumerable(seqType.BaseType)
            End If

            Return Nothing
        End Function
    End Module
End Namespace
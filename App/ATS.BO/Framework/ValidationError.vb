#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Transactions
Imports System.Reflection
Imports ATS.BO.Framework
Imports ATS.DA.Framework
Imports System.ComponentModel
#End Region

Namespace Framework

    ''' <summary>
    ''' This class contains the error message when a validation rule is broken
    ''' A validation error object should be created when validating input from the user and 
    ''' we want to display a message back to the user.
    ''' </summary>
    Public Class ValidationError

#Region " Constructor "
        Public Sub New()

        End Sub
        Public Sub New(ByVal ErrorMessage As String)
            mstrErrorMessage = ErrorMessage
        End Sub
#End Region

#Region " properties "
        Private mstrErrorMessage As String
        Public Property ErrorMessage() As String
            Get
                Return mstrErrorMessage
            End Get
            Set(ByVal value As String)
                mstrErrorMessage = value
            End Set
        End Property
#End Region


    End Class

#Region " ValidationErrors "
    ''' <summary>
    ''' This class contains a list of validation errors.  This allows you to report back multiple errors.
    ''' </summary>
    Public Class ValidationErrors
        Inherits List(Of ValidationError)
        Public Sub AddError(ByVal ErrorMessage As String)
            MyBase.Add(New ValidationError() With {.ErrorMessage = ErrorMessage})
        End Sub
    End Class

#End Region


End Namespace


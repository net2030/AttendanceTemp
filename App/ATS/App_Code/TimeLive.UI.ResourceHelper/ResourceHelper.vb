Imports Microsoft.VisualBasic
Imports System.Resources

Public Class ResourceHelper
    Public Shared Function GetDeleteMessageJavascript() As String
        Return "return confirm('" & "Â·  —Ìœ »«· √ﬂÌœ Õ–› Â–« «·”Ã· ?" & "');"
    End Function
    Public Shared Function GetSubmitMessageJavascript() As String
        Return "return confirm('" & "Are you sure you want to submit your timesheet for approval?" & " ');"
    End Function
    Public Shared Function GetSaveMessageJavascript() As String
        Return "return alert('" & Resources.TimeLive.Web.Save_cofirmation & "');"
    End Function

    Public Shared Function GetDefaultDataLocalValue(ByVal Value As String) As String
        Return System.Web.HttpContext.GetGlobalResourceObject("TimeLive.Web", Value)
    End Function




    Public Shared Function IsGetFromSystemTerminology(ByVal Term As String) As Boolean
        If Term.Contains("Cost Center") Or Term.Contains("Location") Then
            Return True
        End If
    End Function
    Public Shared Function GetFromResource(ByVal Value As String, Optional ByVal AccountId As Integer = 0) As String
        Return ""
        ' Return ReadResourceValue("MulResource.resx", Value)
    End Function

    Public Shared Function ReadResourceValue(ByVal file As String, ByVal key As String) As String
        Dim resourceValue As String = String.Empty

        Try
            Dim resourceFile As String = file
            Dim filePath As String = System.AppDomain.CurrentDomain.BaseDirectory.ToString()
            Dim resourceManager As ResourceManager = resourceManager.CreateFileBasedResourceManager(resourceFile, filePath, Nothing)
            resourceValue = resourceManager.GetString(key)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            resourceValue = String.Empty
        End Try

        Return resourceValue
    End Function

    Public Shared Function FindAndReplace(ByVal Value As String) As String
        Dim App_Name As String = "TimeLive"
        Dim Copyright_Text As String = "Copyright 2007 - 2013 MAS Technology. All rights reserved."
        If Value.Contains(App_Name) Then
            If System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME") Is Nothing Then
                Return Value
            Else
                Return Replace(Value, App_Name, System.Configuration.ConfigurationManager.AppSettings("APPLICATION_NAME"))
            End If
        ElseIf Value.Contains(Copyright_Text) Then
            If System.Configuration.ConfigurationManager.AppSettings("COPYRIGHT_TEXT") Is Nothing Then
                Return Value
            Else
                Return System.Configuration.ConfigurationManager.AppSettings("COPYRIGHT_TEXT")
            End If
        End If
        If System.Configuration.ConfigurationManager.AppSettings("BugTracking") = "Yes" Then
            Return Value.Replace("Task", "Bug")
        Else
            Return Value
        End If
        Return Value
    End Function
    Public Shared Function SetResourceValueInDataTable(ByVal dt As DataTable, ByVal ColumnName As String) As DataTable
        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows.Item(i).Item(ColumnName) = ResourceHelper.GetFromResource(dt.Rows.Item(i).Item(ColumnName).ToString)
        Next
        Return dt
    End Function
    Public Shared Function GetCustomFieldMessageJavascript() As String
        Return "return confirm('" & "Deleting a Custom Field is a permanent action, and will delete all data entered against this field as well. " & Resources.TimeLive.Web.Delete_cofirmation & " ');"
    End Function
    Public Shared Function GetResetPolicyMessageJavascript() As String
        Return "return confirm('This will reset the policy as if it starts from today. Do you want to continue?');"
    End Function

End Class

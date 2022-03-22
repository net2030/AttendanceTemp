#Region " Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Microsoft.Office.Interop
Imports Microsoft.Win32
Imports System.Configuration

Imports System.Security.Principal
Imports System.DirectoryServices

#End Region

Public Class Notifications
#Region " Form Archiving "
    Public Shared WordMessage As String

    Public Shared Function OpenFormFileDocument() As Boolean

        'Try
        Dim oWordApplication As New Word.Application
        Dim oWordDocument As New Word.Document
        Dim myStreamWriter As StreamWriter
        If System.IO.File.Exists(Environment.CurrentDirectory & "\" & "Source.txt") Then
            System.IO.File.Delete(Environment.CurrentDirectory & "\" & "Source.txt")
        End If
        ' Open Word document
        oWordDocument = oWordApplication.Documents.Open("C:\Users\adnan\Desktop\إنذارات.docx")
        Dim sbHeader As New System.Text.StringBuilder
        Dim sbvalues As New System.Text.StringBuilder

        sbHeader.Append("EmployeeName;DepartmentName;EmployeeCode;Title;StartDate;EndDate;DirectorName;DirectorTitle")
        sbvalues.Append("فواز النجراني;قسم المتابعة;1411;مسؤول متابعة;1/1/2014;5/1/2014;علي بن أحمد الشريده;مدير إدارة الموارد البشرية")
        myStreamWriter = File.CreateText(Environment.CurrentDirectory & "\" & "Source.txt")

        myStreamWriter.WriteLine(sbHeader.ToString)
        myStreamWriter.WriteLine(sbvalues.ToString)
        myStreamWriter.Flush()

        myStreamWriter.Dispose()

        oWordDocument.MailMerge.OpenDataSource(Environment.CurrentDirectory & "\" & "Source.txt")
        oWordDocument.MailMerge.Destination = Word.WdMailMergeDestination.wdSendToNewDocument
        oWordDocument.MailMerge.Execute()
        oWordDocument.Close(False)

        ' Show Word document
        oWordApplication.Visible = True

        'Catch ex As System.IO.IOException

        '    'MsgBox("ملف قالب الوثيقة مفتوح.. فضلا اغلق ملف قالب الوثيقة")
        'Catch ex As System.Runtime.InteropServices.COMException

        '    'MsgBox("ميكروسوفت ورد غير محمل على جهازك")
        'Catch ex As System.MissingMemberException
        'Catch ex As System.FormatException
        'Catch ex As Exception
        'End Try
        Return True
    End Function


    
#End Region
End Class

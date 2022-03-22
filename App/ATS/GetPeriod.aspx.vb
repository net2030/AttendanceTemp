Imports System.Web.Services
Imports System.Web.Script.Services
Imports ATS.BO.Framework
Imports System.Data.SqlClient
Imports ATS.DA.Framework
Imports System.Globalization

Imports System
Imports System.Web
Imports System.Diagnostics

Imports System.Data
Imports System.Collections

Partial Class WebService_GetPeriod
    Inherits System.Web.UI.Page



    <System.Web.Services.WebMethod()> _
    Public Shared Function GetPeriodBetweenTwoDates(ByVal StartDate As String, ByVal EndDate As String) As String
        Dim allFormats As String() = {"yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy"}

        Dim DateObj As Dates = New Dates

        If DateObj.IsGreg(StartDate) Then
            Dim SDate As Date = Date.ParseExact(StartDate.Trim, "yyyy/MM/dd", New CultureInfo("en-US").DateTimeFormat)
            Dim EDate As Date = Date.ParseExact(EndDate.Trim, "yyyy/MM/dd", New CultureInfo("en-US").DateTimeFormat)
            Return DateDiff(DateInterval.Day, SDate, EDate) + 1

        End If

        Dim query1 As String = "dbo.GetActualGeorgDate"
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Dim com As New SqlCommand(query1, oCon)
            com.CommandType = CommandType.StoredProcedure

            com.Parameters.Add("@FirstDate", SqlDbType.Date).Value = StartDate
            com.Parameters.Add("@EndDate", SqlDbType.Date).Value = EndDate

            com.Parameters.Add("@FirstDateOut", SqlDbType.Char, 10)
            com.Parameters.Item("@FirstDateOut").Direction = ParameterDirection.Output

            com.Parameters.Add("@EndDateOut", SqlDbType.Char, 10)
            com.Parameters.Item("@EndDateOut").Direction = ParameterDirection.Output

            oCon.Open()
            com.ExecuteNonQuery()

            Dim SDate As DateTime = DateTime.ParseExact(com.Parameters("@FirstDateOut").Value, allFormats, New CultureInfo("en-US").DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Dim EDate As DateTime = DateTime.ParseExact(com.Parameters("@EndDateOut").Value, allFormats, New CultureInfo("en-US").DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)

            'Dim SDate As Date = Date.ParseExact(com.Parameters("@FirstDateOut").Value, "yyyy-MM-dd", New CultureInfo("en-US").DateTimeFormat)
            'Dim EDate As Date = Date.ParseExact(com.Parameters("@EndDateOut").Value, "yyyy-MM-dd", New CultureInfo("en-US").DateTimeFormat)

            Return DateDiff(DateInterval.Day, SDate, EDate) + 1
            oCon.Close()
        Catch ex As Exception
            Return 0
        End Try


    End Function

End Class




Public Class Dates
    Dim cur As HttpContext
    Const startGreg As Integer = 1900
    Const endGreg As Integer = 2100
    Dim allFormats As String() = {"yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy", "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy"}
    Dim arCul As CultureInfo
    Dim enCul As CultureInfo
    Dim h As HijriCalendar
    Dim g As GregorianCalendar

    Public Sub New()
        cur = HttpContext.Current
        arCul = New CultureInfo("ar-SA")
        enCul = New CultureInfo("en-US")
        h = New HijriCalendar()
        g = New GregorianCalendar(GregorianCalendarTypes.USEnglish)
        arCul.DateTimeFormat.Calendar = h
    End Sub

    Public Function IsHijri(ByVal hijri As String) As Boolean
        If hijri.Length <= 0 Then
            cur.Trace.Warn("IsHijri Error: Date String is Empty")
            Return False
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)

            If tempDate.Year >= startGreg AndAlso tempDate.Year <= endGreg Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            cur.Trace.Warn("IsHijri Error :" & hijri.ToString() & vbLf & ex.Message)
            Return False
        End Try
    End Function

    Public Function IsGreg(ByVal greg As String) As Boolean

       

        If greg.Length <= 0 Then
            cur.Trace.Warn("IsGreg :Date String is Empty")
            Return False
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)

            If tempDate.Year >= startGreg AndAlso tempDate.Year <= endGreg Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            cur.Trace.Warn("IsGreg Error :" & greg.ToString() & vbLf & ex.Message)
            Return False
        End Try
    End Function

    Public Function FormatHijri(ByVal dat As String, ByVal format As String) As String


        If dat.Length <= 0 Then
            cur.Trace.Warn("Format :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(dat, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces) ' DateTime.ParseExact(dat, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)()
            Return tempDate.ToString(format, arCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("Date :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function FormatGreg(ByVal dat As String, ByVal format As String) As String
        If dat.Length <= 0 Then
            cur.Trace.Warn("Format :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(dat, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return tempDate.ToString(format, enCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("Date :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function GDateNow() As String
        Try
            Return DateTime.Now.ToString("yyyy/MM/dd", enCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("GDateNow :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function GDateNow(ByVal format As String) As String
        Try
            Return DateTime.Now.ToString(format, enCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("GDateNow :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function HDateNow() As String
        Try
            Return DateTime.Now.ToString("yyyy/MM/dd", arCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("HDateNow :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function HDateNow(ByVal format As String) As String
        Try
            Return DateTime.Now.ToString(format, arCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("HDateNow :" & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function HijriToGreg(ByVal hijri As String) As String
        If hijri.Length <= 0 Then
            cur.Trace.Warn("HijriToGreg :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return tempDate.ToString("yyyy/MM/dd", enCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("HijriToGreg :" & hijri.ToString() & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function HijriToGreg(ByVal hijri As String, ByVal format As String) As String
        If hijri.Length <= 0 Then
            cur.Trace.Warn("HijriToGreg :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(hijri, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return tempDate.ToString(format, enCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("HijriToGreg :" & hijri.ToString() & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function GregToHijri(ByVal greg As String) As String
        If greg.Length <= 0 Then
            cur.Trace.Warn("GregToHijri :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return tempDate.ToString("yyyy/MM/dd", arCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("GregToHijri :" & greg.ToString() & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function GregToHijri(ByVal greg As String, ByVal format As String) As String
        If greg.Length <= 0 Then
            cur.Trace.Warn("GregToHijri :Date String is Empty")
            Return ""
        End If

        Try
            Dim tempDate As DateTime = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return tempDate.ToString(format, arCul.DateTimeFormat)
        Catch ex As Exception
            cur.Trace.Warn("GregToHijri :" & greg.ToString() & vbLf & ex.Message)
            Return ""
        End Try
    End Function

    Public Function GTimeStamp() As String
        Return GDateNow("yyyyMMddHHmmss")
    End Function

    Public Function HTimeStamp() As String
        Return HDateNow("yyyyMMddHHmmss")
    End Function

    Public Function Compare(ByVal d1 As String, ByVal d2 As String) As Integer
        Try
            Dim date1 As DateTime = DateTime.ParseExact(d1, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Dim date2 As DateTime = DateTime.ParseExact(d2, allFormats, arCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces)
            Return DateTime.Compare(date1, date2)
        Catch ex As Exception
            cur.Trace.Warn("Compare :" & vbLf & ex.Message)
            Return -1
        End Try
    End Function
End Class


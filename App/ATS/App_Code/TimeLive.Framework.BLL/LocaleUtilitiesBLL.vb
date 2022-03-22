Imports Microsoft.VisualBasic
Imports System.Globalization
Imports SMInformatik.Util
Public Class LocaleUtilitiesBLL

    Public Shared Function GetDateFormatForGridView() As String
        Return "{0:dd/MM/yyyy}"
    End Function

    Public Shared Function GetCurrentCulture() As String
        If System.Web.HttpContext.Current.Session("CultureInfoName") Is Nothing Then
            Return "auto"
        ElseIf IsDBNull(System.Web.HttpContext.Current.Session("CultureInfoName")) Then
            Return "auto"
        Else
            Return System.Web.HttpContext.Current.Session("CultureInfoName")
        End If

    End Function
    Public Shared Function GetCurrencySymbol() As String
        If System.Web.HttpContext.Current.Session("CurrencySymbol") Is Nothing Then
            Return "$"
        ElseIf IsDBNull(System.Web.HttpContext.Current.Session("CurrencySymbol")) Then
            Return "$"
        ElseIf System.Web.HttpContext.Current.Session("CurrencySymbol") = "auto" Then
            Return "$"
        Else
            Return System.Web.HttpContext.Current.Session("CurrencySymbol")
        End If

    End Function
    Public Shared Function GetCurrentUICulture() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return System.Threading.Thread.CurrentThread.CurrentUICulture.ToString()
        ElseIf System.Web.HttpContext.Current.Session("UserInterfaceLanguage") Is Nothing Then
            Return System.Threading.Thread.CurrentThread.CurrentUICulture.ToString
        ElseIf IsDBNull(System.Web.HttpContext.Current.Session("UserInterfaceLanguage")) Then
            Return System.Threading.Thread.CurrentThread.CurrentUICulture.ToString
        ElseIf System.Web.HttpContext.Current.Session("UserInterfaceLanguage") = "auto" Then
            Return System.Threading.Thread.CurrentThread.CurrentUICulture.ToString
        Else
            Return System.Web.HttpContext.Current.Session("UserInterfaceLanguage")
        End If

    End Function
    Public Shared Function GetSavedCurrentUICulture() As String
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return "auto"
        ElseIf System.Web.HttpContext.Current.Session("UserInterfaceLanguage") Is Nothing Then
            Return "auto"
        ElseIf IsDBNull(System.Web.HttpContext.Current.Session("UserInterfaceLanguage")) Then
            Return "auto"
        Else
            Return System.Web.HttpContext.Current.Session("UserInterfaceLanguage")
        End If

    End Function
    Public Shared Function GetCurrentUICultureInfo() As CultureInfo
        Return New System.Globalization.CultureInfo(GetCurrentUICulture)
    End Function
    Public Shared Function GetBaseCulture() As String
        Return "en-us"
    End Function
    Public Shared Function GetCurrentCultureInfo() As System.Globalization.CultureInfo
        Dim myCItrad As New CultureInfo(GetCurrentCulture, False)
        Return myCItrad
    End Function
    Public Shared Function GetCurrentCultureInfoWithThreadCulture() As System.Globalization.CultureInfo
        Dim CultureString As String
        CultureString = GetCurrentCulture()
        If CultureString = "auto" Then
            CultureString = LocaleUtilitiesBLL.GetCurrentThreadCulture.ToString
        End If
        Dim myCItrad As New CultureInfo(CultureString, False)
        Return myCItrad
    End Function
    Public Shared Function GetBaseCultureInfo() As System.Globalization.CultureInfo
        Dim myCItrad As New CultureInfo(GetBaseCulture, False)
        Return myCItrad
    End Function
    Public Shared Function GetCultureInfoByCulture(ByVal CultureName As String) As System.Globalization.CultureInfo
        Dim myCItrad As New CultureInfo(CultureName, False)
        Return myCItrad
    End Function
    Public Shared Function GetCurrentThreadCulture() As System.Globalization.CultureInfo
        Return System.Threading.Thread.CurrentThread.CurrentCulture
    End Function
    Public Shared Sub SetPageCultureSetting(ByVal objPage As Page)
        objPage.Culture = LocaleUtilitiesBLL.GetCurrentCulture
        Dim cult As System.Globalization.CultureInfo = LocaleUtilitiesBLL.GetCurrentThreadCulture.Clone
        cult.NumberFormat.CurrencySymbol = GetCurrencySymbol()
        System.Threading.Thread.CurrentThread.CurrentCulture = cult
        SetCalenderPopByCultureAndWorkingDays()
        LocaleUtilitiesBLL.ChangePageTitleOfCurrentLocale(objPage)
    End Sub
    Public Shared Sub SetCurrentThreadCulture(ByVal CultureString As String)
        Dim cult As New CultureInfo(CultureString)
        System.Threading.Thread.CurrentThread.CurrentCulture = cult
        System.Threading.Thread.CurrentThread.CurrentUICulture = cult
    End Sub
    Public Shared Sub ChangePageTitleOfCurrentLocale(ByVal objPage As Page)
        Dim title As String = objPage.Title.Replace("TimeLive - ", "")

        objPage.Title = ResourceHelper.GetFromResource(title)
    End Sub
    Public Shared Function IsCurrentCultureIsEnglish() As Boolean
        If LocaleUtilitiesBLL.IsEnglishCultureString(LocaleUtilitiesBLL.GetCurrentThreadCulture.ToString) Then
            Return True
        End If
    End Function
    Public Shared Function IsEnglishCultureString(ByVal CultureString As String) As Boolean
        If CultureString = "en-US" Then
            Return True
        End If
    End Function
    Public Shared Sub SetCalenderPopByCultureAndWorkingDays()
        Dim WeekStartDate As Integer = DBUtilities.GetSessionEmployeeWeekStartDay
        If WeekStartDate = 1 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday
        ElseIf WeekStartDate = 2 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Tuesday
        ElseIf WeekStartDate = 3 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Wednesday
        ElseIf WeekStartDate = 4 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Thursday
        ElseIf WeekStartDate = 5 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Friday
        ElseIf WeekStartDate = 6 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday
        ElseIf WeekStartDate = 7 Then
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday
        End If
    End Sub
    Public Shared Function GetCurrentThreadCultureCurrencySymbol() As System.Globalization.CultureInfo
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = System.Web.HttpContext.Current.Session("CurrencySymbol")
    End Function

    Public Shared Sub SetGridColumnDateFormat(ByVal objGridView As GridView, ByVal ColumnNumber As Short)
        If objGridView.Rows.Count > 0 Then
            If CType(objGridView.Columns(ColumnNumber), BoundField).DataFormatString <> "" Then
                CType(objGridView.Columns(ColumnNumber), BoundField).DataFormatString = LocaleUtilitiesBLL.GetDateFormatForGridView
            Else
                Throw New Exception("Invalid date column")
            End If
        End If

    End Sub

    Public Shared Sub FixDatePickerInternationalDate(ByVal objDatePicker As eWorld.UI.CalendarPopup, ByVal strInputName As String, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        If Not objDatePicker.PostedDate Is Nothing Then
            e.InputParameters(strInputName) = Convert.ToDateTime(objDatePicker.PostedDate)
        End If
    End Sub

    Public Shared Function ConvertDateToBaseLocaleAsString(ByVal strDate As String) As String
        Return Convert.ToDateTime(strDate).ToString(GetBaseCultureInfo.DateTimeFormat.ShortDatePattern, GetBaseCultureInfo.DateTimeFormat)
    End Function
    Public Shared Function ConvertBaseDateStringToDate(ByVal strDate As String) As Date
        Return Convert.ToDateTime(strDate, LocaleUtilitiesBLL.GetBaseCultureInfo.DateTimeFormat)
    End Function
    Public Shared Function ConvertCurrentDateStringToDate(ByVal strDate As String) As Date
        Return Convert.ToDateTime(strDate, LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat)
    End Function
    Public Shared Function ConvertDateBaseToStringForAllCulture(ByVal strDate As String) As String
        Return Convert.ToDateTime(strDate).ToString("yyyy-MM-dd")
    End Function
    Public Shared Function ConvertDateBaseIntoSQLQurey(ByVal strDate As String) As String
        Return "Convert(DateTime, Convert(VARCHAR(10), '" & ConvertDateBaseToStringForAllCulture(strDate) & "', 120), 120)"
    End Function
    Public Shared Function ConvertDateToBaseLocaleAsDate(ByVal strDate As String) As DateTime
        Return Convert.ToDateTime(strDate)
    End Function
    Public Shared Function ConvertDateBaseToCurrentLocaleAsString(ByVal strDate As String) As String
        Return Convert.ToDateTime(strDate, GetBaseCultureInfo).ToShortDateString
    End Function
    Public Shared Function ConvertDateBaseToUserLocaleAsString(ByVal strDate As String, ByVal CultureName As String) As String
        Return Convert.ToDateTime(strDate).ToString(GetCultureInfoByCulture(CultureName).DateTimeFormat.ShortDatePattern, GetCultureInfoByCulture(CultureName).DateTimeFormat)
    End Function
    Public Shared Function ConvertDateToDateBaseCulture(ByVal strDate As Date) As String
        Return strDate.ToString(LocaleUtilitiesBLL.GetBaseCultureInfo.DateTimeFormat.ShortDatePattern, LocaleUtilitiesBLL.GetBaseCultureInfo)
    End Function
    Public Shared Function ConvertDateToDateBaseCultureOfUser(ByVal strDate As Date) As String
        Return strDate.ToString(LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.ShortDatePattern, LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture)
    End Function
    Public Shared Function GetDayDateAndMonthInCurrentLocale(ByVal dtDate As Date) As String
        Dim DateFormat As String = "M/dd"
        If LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.MonthDayPattern.StartsWith("d") Then
            DateFormat = "dd" & LocaleUtilitiesBLL.GetCurrentThreadCulture.DateTimeFormat.DateSeparator & "M"
        ElseIf LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.MonthDayPattern.StartsWith("M") Then
            DateFormat = "M" & LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.DateSeparator & "dd"
        End If
        Return dtDate.ToString(DateFormat)
    End Function
    Public Shared Function GetDayDateAndMonthInCurrentLocaleForEmail(ByVal dtDate As Date, ByVal CultureName As String) As String
        Dim DateFormat As String = "M/dd"
        If LocaleUtilitiesBLL.GetCultureInfoByCulture(CultureName).DateTimeFormat.MonthDayPattern.StartsWith("d") Then
            DateFormat = "dd" & LocaleUtilitiesBLL.GetCultureInfoByCulture(CultureName).DateTimeFormat.DateSeparator & "M"
        ElseIf LocaleUtilitiesBLL.GetCultureInfoByCulture(CultureName).DateTimeFormat.MonthDayPattern.StartsWith("M") Then
            DateFormat = "M" & LocaleUtilitiesBLL.GetCultureInfoByCulture(CultureName).DateTimeFormat.DateSeparator & "dd"
        End If
        Return dtDate.ToString(DateFormat)
    End Function
    Public Shared Function GetDayInCurrentLocale(ByVal dtDate As Date) As String
        Return Left([Enum].Format(GetType(DayOfWeek), dtDate.DayOfWeek, "G"), 3)
    End Function
    Public Function GetAllCultureInfo() As ArrayList
        Dim list As New SortedList
        Dim AllCultures() As System.Globalization.CultureInfo = CultureInfo.GetCultures(CultureTypes.FrameworkCultures)
        For n As Integer = 0 To AllCultures.Length - 1
            If AllCultures(n).IsNeutralCulture = False And AllCultures(n).Name <> "" Then
                list.Add(AllCultures(n).EnglishName, AllCultures(n))
            End If
        Next

        Dim arrlist As New ArrayList
        Dim objCultureInfo As CultureInfo
        For Each objCultureInfo In list.Values
            arrlist.Add(objCultureInfo)
        Next

        Return arrlist

    End Function
    Public Function GetAllCultureCurrencySymbolInfo() As ArrayList
        Dim list As New SortedList
        Dim AllCultures() As System.Globalization.CultureInfo = CultureInfo.GetCultures(CultureTypes.FrameworkCultures)
        For n As Integer = 0 To AllCultures.Length - 1
            If AllCultures(n).IsNeutralCulture = False And AllCultures(n).Name <> "" Then
                If list.ContainsKey(AllCultures(n).NumberFormat.CurrencySymbol) = False Then
                    list.Add(AllCultures(n).NumberFormat.CurrencySymbol, AllCultures(n))
                End If
            End If
        Next

        Dim arrlist As New ArrayList
        Dim objCultureInfo As CultureInfo
        For Each objCultureInfo In list.Values
            arrlist.Add(objCultureInfo.NumberFormat)
        Next

        Return arrlist

    End Function
    Public Shared Function GetCurrentTimeEntryFormat() As String

        If System.Web.HttpContext.Current.Session("TimeEntryFormat") = "" Then
            Return "HH:mm"
        Else
            Return System.Web.HttpContext.Current.Session("TimeEntryFormat")

        End If

    End Function

    Public Shared Function GetCurrentTimeEntryFormatForTotalTime() As String

        If System.Web.HttpContext.Current.Session("TimeEntryFormat") = "" Then
            Return "HH:mm"
        Else
            GetCurrentTimeEntryFormatForTotalTime = Replace(System.Web.HttpContext.Current.Session("TimeEntryFormat"), " tt", "")
            If System.Web.HttpContext.Current.Session("TimeEntryFormat").ToString.IndexOf("tt") > 0 Then
                GetCurrentTimeEntryFormatForTotalTime = GetCurrentTimeEntryFormatForTotalTime.Replace("hh", "HH")
            End If
            Return GetCurrentTimeEntryFormatForTotalTime
        End If

    End Function
    Public Shared Function ConvertStringToTimeFormatAsString(ByVal strTime As String) As String
        If strTime.IndexOf(":") = -1 Then
            If strTime.IndexOf("AM") > 0 Then
                strTime = strTime.Substring(0, 2) & ":00 AM"
            ElseIf strTime.IndexOf("PM") > 0 Then
                strTime = strTime.Substring(0, 2) & ":00 PM"
            Else
                strTime = strTime & ":00"

            End If
        End If
        Return strTime
    End Function

    Public Shared Function ConvertStringToTimeFormatWithDate(ByVal strTime As String, ByVal dtDate As Date) As DateTime

        strTime = LocaleUtilitiesBLL.ConvertStringToTimeFormatAsString(strTime)
        Return Convert.ToDateTime(dtDate & " " & strTime)
    End Function
    Public Shared Function ConvertStringToTimeFormatWithoutDate(ByVal strTime As String) As DateTime
        strTime = LocaleUtilitiesBLL.ConvertStringToTimeFormatAsString(strTime)
        Return Convert.ToDateTime(strTime)
    End Function

    Public Shared Function IsAcceptAMPMForTimeentry() As Boolean
        If GetCurrentTimeEntryFormat.IndexOf("tt") > 0 Then
            Return True
        End If
    End Function
    Public Shared Function GetCurrentSessionTimeout() As Integer

        Return System.Web.HttpContext.Current.Session.Timeout

    End Function
    Public Shared Function IsShowCompletedTasksInTimeSheet() As Boolean

        Return System.Web.HttpContext.Current.Session("ShowCompletedTasksInTimeSheet")

    End Function

    Public Shared Function IsShowCompanyOwnLogo() As Boolean

        Return System.Web.HttpContext.Current.Session("IsCompanyOwnLogo")

    End Function
    Public Shared Function ShowLastScheduledEmailSentTime() As DateTime

        Return System.Web.HttpContext.Current.Session("LastScheduledEmailSentTime")

    End Function
    Public Shared Function ShowScheduledEmailSendTime() As DateTime

        Return System.Web.HttpContext.Current.Session("ScheduledEmailSendTime")

    End Function
    Public Shared Function IsShowLockSubmittedRecords() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("LockSubmittedRecords") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockSubmittedRecords")
        Else
            Return True
        End If
    End Function
    Public Shared Function IsShowLockApprovedRecords() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("LockApprovedRecords") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("LockApprovedRecords")
        Else
            Return True
        End If
    End Function
    Public Shared Function ConvertStringToTimeEntryTotalTimeFormat(ByVal strTime As String) As String
        strTime = Convert.ToDateTime(strTime).ToString(LocaleUtilitiesBLL.GetCurrentTimeEntryFormatForTotalTime)
        Return strTime
    End Function

    Public Shared Function ConvertStringToTimeEntryStartEndTimeFormat(ByVal strTime As Object) As String
        If IsDBNull(strTime) Then
            strTime = ""
        Else
            strTime = Convert.ToDateTime(strTime).ToString(LocaleUtilitiesBLL.GetCurrentTimeEntryFormat)
        End If
        Return strTime
    End Function

    Public Shared Function IsBothTimeAreDiffer(ByVal Time1 As Object, ByVal Time2 As Object)

        If IsDate(Time1) Then
            Time1 = CDate(Time1).ToShortTimeString()
        Else
            Time1 = ""
        End If

        If IsDate(Time2) Then
            Time2 = LocaleUtilitiesBLL.ConvertStringToTimeFormatWithoutDate(Time2).ToShortTimeString()
        Else
            Time2 = ""
        End If

        Return Time1 <> Time2
    End Function

    Public Shared Function GetString0IfNull(ByVal strValue As Object) As String
        If IsDBNull(strValue) Then
            Return "0"
        Else
            Return strValue
        End If
    End Function

    Public Shared Function GetInteger0IfNull(ByVal intValue As Object) As Integer
        If IsDBNull(intValue) Then
            Return 0
        Else
            Return intValue
        End If
    End Function
    
    
    Public Shared Function ConvertNumberToBaseCultureNumberFormat(ByVal value As String) As Double
        Return Convert.ToDouble(value, LocaleUtilitiesBLL.GetBaseCultureInfo.NumberFormat)
    End Function
    
    
    
    Public Shared Function IsLogoFileExist() As Boolean
        Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")
        Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
        If Not System.IO.Directory.Exists(strRoot) Then
            Return False
        End If
        Dim strAccountPath As String = strRoot & DBUtilities.GetInstalledAccountId & "\"
        If Not System.IO.Directory.Exists(strAccountPath) Then
            Return False
        End If
        Dim strLogoPath As String = strAccountPath & "Logo" & "\"
        If Not System.IO.Directory.Exists(strLogoPath) Then
            Return False
        End If
        Dim strFilePath As String = strLogoPath & "CompanyLogo.gif"
        If Not System.IO.File.Exists(strFilePath) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function IsLogoFileExistInSessionAccount() As Boolean
        Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")
        Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
        If Not System.IO.Directory.Exists(strRoot) Then
            Return False
        End If
        Dim strAccountPath As String = strRoot & DBUtilities.GetSessionAccountId & "\"
        If Not System.IO.Directory.Exists(strAccountPath) Then
            Return False
        End If
        Dim strLogoPath As String = strAccountPath & "Logo" & "\"
        If Not System.IO.Directory.Exists(strLogoPath) Then
            Return False
        End If
        Dim strFilePath As String = strLogoPath & "CompanyLogo.gif"
        If Not System.IO.File.Exists(strFilePath) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function IsShowCompletedProjectsInTimeSheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowCompletedProjectsInTimeSheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowCompletedProjectsInTimeSheet")
        Else
            Return False
        End If
    End Function
    Public Shared Function ConvertHoursToTimeFormat(ByVal Hours As String, ByVal Minute As String) As String
        Return Hours & ":" & Math.Round(Minute * 60 / 100, 0)
    End Function
    Public Shared Function ConvertTimeIntoHoursDecimalFormat(ByVal Hours As String, ByVal Minute As String) As Decimal
        Return Convert.ToDecimal(Hours & "." & Right("00" & LTrim(RTrim(Str(Convert.ToDecimal(Math.Round(Minute / 60 * 100, 0))))), 2), LocaleUtilitiesBLL.GetBaseCultureInfo)
    End Function
    Public Shared Function IsShowProjectForTimeOff() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowProjectForTimeOff") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowProjectForTimeOff")
        Else
            Return False
        End If
    End Function
    Public Shared Function GetDayDateInCurrentLocale(ByVal dtDate As Date) As String
        Dim DateFormat As String = "M/dd/YY"
        If LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.MonthDayPattern.StartsWith("d") Then
            DateFormat = "dd" & LocaleUtilitiesBLL.GetCurrentThreadCulture.DateTimeFormat.DateSeparator & "M"
        ElseIf LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.MonthDayPattern.StartsWith("M") Then
            DateFormat = "M" & LocaleUtilitiesBLL.GetCurrentCultureInfoWithThreadCulture.DateTimeFormat.DateSeparator & "dd"
        End If
        Return dtDate.Date
    End Function
    Public Shared Function IsShowTimeOffInTimesheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowTimeOffInTimesheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowTimeOffInTimesheet")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowAllInTimesheetReadOnly() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowAllInTimesheetReadOnly") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowAllInTimesheetReadOnly")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowTaskInExpenseSheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowTaskInExpenseSheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowTaskInExpenseSheet")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowBillingRateInInvoiceReport() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowBillingRateInInvoiceReport") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowBillingRateInInvoiceReport")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowProjectNameInInvoiceReport() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowProjectNameInInvoiceReport") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowProjectNameInInvoiceReport")
        Else
            Return False
        End If
    End Function
    Public Shared Function AutoGenerateProjectCode() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("AutoGenerateProjectCode") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("AutoGenerateProjectCode")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowPercentageInTimesheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowPercentageInTimesheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowPercentageInTimesheet")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowProjectNameInTask() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowProjectNameInTask") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowProjectNameInTask")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowClientNameInTask() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowClientNameInTask") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowClientNameInTask")
        Else
            Return False
        End If
    End Function
    Public Shared Function IsShowElectronicSign() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("IsShowElectronicSign") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("IsShowElectronicSign")
        Else
            Return False
        End If
    End Function
    Public Shared Function IsElectronicSignExistInSessionAccount() As Boolean
        Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")
        Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
        If Not System.IO.Directory.Exists(strRoot) Then
            Return False
        End If
        Dim strAccountPath As String = strRoot & DBUtilities.GetSessionAccountId & "\" & DBUtilities.GetSessionAccountEmployeeId & "\"
        If Not System.IO.Directory.Exists(strAccountPath) Then
            Return False
        End If
        Dim strLogoPath As String = strAccountPath & "Sign" & "\"
        If Not System.IO.Directory.Exists(strLogoPath) Then
            Return False
        End If
        Dim strFilePath As String = strLogoPath & "E-Sign.gif"
        If Not System.IO.File.Exists(strFilePath) Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowCompletedProjectInProjectGrid() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("ShowCompletedProjectInProjectGrid") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowCompletedProjectInProjectGrid")
        Else
            Return True
        End If
    End Function
    Public Shared Function ShowEmployeeNameInInvoiceReport() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowEmployeeNameInInvoiceReport") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowEmployeeNameInInvoiceReport")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowWorkTypeInInvoiceDescription() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowWorkTypeInInvoiceDescription") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowWorkTypeInInvoiceDescription")
        Else
            Return False
        End If
    End Function
    Public Shared Function EnableCalculateTaskPercentageAutomatically() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return True
        ElseIf Not System.Web.HttpContext.Current.Session("CalculateTaskPercentageAutomatically") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("CalculateTaskPercentageAutomatically")
        Else
            Return True
        End If
    End Function
    Public Shared Function GetSessionEmployeeCreatedOnDate() As Date
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return now.date
        ElseIf Not System.Web.HttpContext.Current.Session("CreatedOn") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("CreatedOn")
        Else
            Return Now.Date
        End If
    End Function
    Public Shared Function IsShowEmployeeProfilePicture() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("IsShowEmployeeProfilePicture") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("IsShowEmployeeProfilePicture")
        Else
            Return False
        End If
    End Function
    Public Shared Function ShowClientInExpenseSheet() As Boolean
        If System.Web.HttpContext.Current.Session Is Nothing Then
            Return False
        ElseIf Not System.Web.HttpContext.Current.Session("ShowClientInExpenseSheet") Is Nothing Then
            Return System.Web.HttpContext.Current.Session("ShowClientInExpenseSheet")
        Else
            Return False
        End If
    End Function
    Public Shared Function IsNotSupportedCultureFormatsForDecimal() As Boolean
        If LocaleUtilitiesBLL.GetCurrentCulture = "ru-RU" Or System.Threading.Thread.CurrentThread.CurrentCulture.Name = "ru-RU" Then
            Return True
        ElseIf LocaleUtilitiesBLL.GetCurrentCulture = "fr-CA" Or System.Threading.Thread.CurrentThread.CurrentCulture.Name = "fr-CA" Then
            Return True
        End If
        Return False
    End Function
End Class

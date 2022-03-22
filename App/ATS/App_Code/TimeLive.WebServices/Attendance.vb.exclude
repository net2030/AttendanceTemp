Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Attendance
    Inherits System.Web.Services.WebService
    Public SoapHeader As SecuredWebServiceHeader
    <WebMethod()> _
    <System.Web.Services.Protocols.SoapHeader("SoapHeader")> _
    Public Function AddAttendance(ByVal objAttendance As AttendanceObject) As String
        If Not TimeLiveServices.IsUserValid(SoapHeader) Then
            Return "Please call AuthenitcateUser() first."
        End If
        Return "Hello World"
    End Function
    <WebMethod()> _
    <System.Web.Services.Protocols.SoapHeader("SoapHeader")> _
    Public Function GetAttendanceByEmployeeIdAndDate(ByVal AccountEmployeeId As Integer, ByVal AttendanceDate As Date) As ArrayList
        If Not TimeLiveServices.IsUserValid(SoapHeader) Then
            Throw New Exception("You are not authorized to access this.")
        End If
        Dim AttendanceArrayList As New ArrayList
        Dim objAttendance As New AttendanceObject
        Dim AttendanceBLL As New AccountEmployeeAttendanceBLL
        Dim AccountId As Integer = TimeLiveServices.GetSecurityToken(SoapHeader.AuthenticatedToken).AccountID
        Dim dtAttendance As TimeLiveDataSet.vueAccountEmployeeAttendanceDataTable = AttendanceBLL.GetvueEmployeeAttendanceByAttendanceDate(AccountId, AccountEmployeeId, AttendanceDate)
        Dim drAttendance As TimeLiveDataSet.vueAccountEmployeeAttendanceRow
        For Each drAttendance In dtAttendance.Rows
            objAttendance = New AttendanceObject
            With objAttendance
                .EmployeeName = drAttendance.EmployeeName
                .AttendanceDate = drAttendance.AttendanceDate
                .AttendanceTime = FormatDateTime(drAttendance.AttendanceTime, DateFormat.ShortTime)
                .AbsenceType = IIf(IsDBNull(drAttendance.Item("AbsenceDescription")), "Present", drAttendance.Item("AbsenceDescription"))
                .InOut = drAttendance.InOut
            End With
            AttendanceArrayList.Add(objAttendance)
        Next
        Return AttendanceArrayList
    End Function
    <WebMethod()> _
    <System.Web.Services.Protocols.SoapHeader("SoapHeader")> _
    Public Sub InsertAttendance(ByVal AccountEmployeeId As Integer, _
                    ByVal AttendanceDate As Date, _
                    ByVal AbsenceTypeId As Integer, _
                    ByVal AttendanceTime As DateTime, _
                    ByVal InOut As String)
        If Not TimeLiveServices.IsUserValid(SoapHeader) Then
            Throw New Exception("You are not authorized to access this.")
        End If
        Dim AttendanceBLL As New AccountEmployeeAttendanceBLL
        Dim AccountId As Integer = TimeLiveServices.GetSecurityToken(SoapHeader.AuthenticatedToken).AccountID
        AttendanceBLL.AddAccountEmployeeAttendance(AccountId, AccountEmployeeId, AttendanceDate, AttendanceTime, InOut, AbsenceTypeId, Now.Date, AccountEmployeeId, Now.Date, AccountEmployeeId)
    End Sub
    <WebMethod()> _
    <System.Web.Services.Protocols.SoapHeader("SoapHeader")> _
    Public Function GetAbsenceTypeId(ByVal AbsenceTypeName As String) As Integer
        If Not TimeLiveServices.IsUserValid(SoapHeader) Then
            Throw New Exception("You are not authorized to access this.")
        End If
        Dim objAbsesnceType As New AccountAbsenceTypeBLL
        Dim AccountId As Integer = TimeLiveServices.GetSecurityToken(SoapHeader.AuthenticatedToken).AccountID
        Dim nAbsenceTypeId As Integer = objAbsesnceType.GetAbsenceTypeByAccountIdAndAbsenceTypeName(AccountId, AbsenceTypeName).Rows(0).Item(6)
        Return nAbsenceTypeId
    End Function
End Class
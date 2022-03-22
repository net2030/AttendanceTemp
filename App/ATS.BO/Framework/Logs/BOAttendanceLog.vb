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

#Region " BOAttendanceLog "
    Public Class BOAttendanceLog
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Return Nothing
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boAttendanceLog As New BOEAttendanceLog

            If Not IsNothing(Entity) Then
                Dim eAttendanceLog As AttendanceLog = CType(Entity, AttendanceLog)
                With boAttendanceLog
                    .AddedDate = eAttendanceLog.AddedDate
                    .AddedUserAccountId = eAttendanceLog.AddedUserAccountId
                    .UpdatedDate = eAttendanceLog.UpdatedDate
                    .UpdatedUserAccountId = eAttendanceLog.UpdatedUserAccountId
                    .VersionNo = eAttendanceLog.VersionNo

                    .AddedUserName = eAttendanceLog.AddedUserName
                    .UpdatedUserName = eAttendanceLog.UpdatedUserName
                End With
            End If

            Return boAttendanceLog
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eAttendanceLog As New AttendanceLog

            If Not IsNothing(Entity) Then
                Dim BoAttendanceLog As BOEAttendanceLog = CType(Entity, BOEAttendanceLog)
                With eAttendanceLog
                    .AddedDate = BoAttendanceLog.AddedDate
                    .AddedUserAccountId = BoAttendanceLog.AddedUserAccountId
                    .UpdatedDate = BoAttendanceLog.UpdatedDate
                    .UpdatedUserAccountId = BoAttendanceLog.UpdatedUserAccountId
                    .VersionNo = BoAttendanceLog.VersionNo

                End With
            End If

            Return eAttendanceLog
        End Function
#End Region

#Region " Public Methods "

        Public Function Add( _
        ByVal AttendanceLogName As String, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Return boolSeccessed
        End Function
        Public Function UpdateAttendance(
        ByVal OptionNo As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        ByVal EmployeeId As Integer,
        ByVal departmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oAttendanceLog As New DAAttedanceLog
            If oAttendanceLog.UpdateAttendance(OptionNo, BegDate, EndDate, EmployeeId, departmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function ChangeAttendanceStatus(
        ByVal LogId As Integer,
        ByVal StatusId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oAttendanceLog As New DAAttedanceLog
            If oAttendanceLog.ChangeAttendanceStatus(LogId, StatusId, UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function UpdateDepartmentAttendance(ByVal optionNo As Integer, ByVal begDate As Date, ByVal endDate As Date, ByVal departmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oAttendanceLog As New DAAttedanceLog
            If oAttendanceLog.UpdateDepartmentAttendance(optionNo, begDate, endDate, departmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function UpdateEmployeeAttendance(ByVal OptionNo As Integer, ByVal BegDate As Date, ByVal EndDate As Date, ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oAttendanceLog As New DAAttedanceLog
            If oAttendanceLog.UpdateEmployeeAttendance(OptionNo, BegDate, EndDate, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oAttendanceLog.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetDepartmentAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        ByVal Employer As Integer,
        ByVal Contracttype As Integer,
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetDepartmentAttendanceDataset(DepartmentId, AttendanceDate, Employer, Contracttype, PageNo, PageSize, Lang)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function
        Public Function GetDepartmentAttendanceDatasetRange( _
                                                            ByVal DepartmentId As Integer, _
                                                            ByVal StartDate As Date, _
                                                            ByVal EndDate As Date, _
                                                            ByVal Employer As Integer,
                                                            ByVal Contracttype As Integer,
                                                            ByVal WorkScheduleId As Integer,
                                                            Optional ByVal PageNo As Integer = 1, _
                                                            Optional ByVal PageSize As Integer = 50,
                                                            Optional ByVal Lang As String = "ar") As DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetDepartmentAttendanceDatasetRange(DepartmentId, StartDate, EndDate, Employer, Contracttype, WorkScheduleId, PageNo, PageSize, Lang)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function
        Public Function GetAttendanceByUserIdDataset( _
        ByVal UserAccountId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetAttendanceByUserIdDataset(UserAccountId, PageNo, PageSize)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function
        Public Function GetEmployeeCiviliansAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetEmployeeCiviliansAttendanceDataset(DepartmentId, AttendanceDate, PageNo, PageSize)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function
        Public Function GetEmployeeAttendanceByDate(
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50, Optional ByVal Lang As String = "ar", Optional ByVal Op As Integer = 0) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetEmployeeAttendanceByDate(EmployeeId, BegDate, EndDate, PageNo, PageSize, Lang, Op)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function

        Public Function GetEmployeeAttendanceDatesByDate(
     ByVal EmployeeId As Integer,
     ByVal BegDate As Date,
     ByVal EndDate As Date,
     Optional ByVal PageNo As Integer = 1,
     Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetEmployeeAttendanceDatesByDate(EmployeeId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function

        Public Function GetEmployeeMilitaryAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As DataSet
            Dim ds As DataSet = Nothing

            Dim oAttedanceLog As New DAAttedanceLog
            ds = oAttedanceLog.GetEmployeeMilitaryAttendanceDataset(DepartmentId, AttendanceDate, PageNo, PageSize)
            MyBase.PageTotal = oAttedanceLog.PageTotal

            Return ds
        End Function
        Public Function GetCiviliansAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date) As DataSet
            Return New DAAttedanceLog().GetCiviliansAttendanceDataset(DepartmentId, AttendanceDate)
        End Function
        Public Function GetMilitaryAttendanceDataset( _
        ByVal DepartmentId As Integer, _
        ByVal AttendanceDate As Date) As DataSet
            Return New DAAttedanceLog().GetMilitaryAttendanceDataset(DepartmentId, AttendanceDate)
        End Function
        Public Function GetUncompleteStatusDataset(
        ByVal UserAccountId As Integer,
        ByVal FilterOption As enumFilterOption,
        ByVal SortOption As enumSortOption,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oAttendanceLog As New BOAttendanceLog
            ds = oAttendanceLog.GetUncompleteStatusDataset(UserAccountId,
                                                  CType(FilterOption, BOAttendanceLog.enumFilterOption),
                                                  CType(SortOption, BOAttendanceLog.enumSortOption),
                                                  PageNo, PageSize)
            MyBase.PageTotal = oAttendanceLog.PageTotal

            Return ds
        End Function
#End Region

#Region "Data Retrive For Charts"
        Public Function GetAttendanceChartData(EmployeeId As Integer, BegDate As Date, EndDate As Date, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Return New DAAttedanceLog().GetAttendanceChartData(EmployeeId, BegDate, EndDate, Lang)
        End Function

        Public Function GetAttendanceSummuryChartDataForReport(DeptId As Integer, BegDate As Date, EndDate As Date) As DataSet
            Return New DAAttedanceLog().GetAttendanceSummuryChartDataForReport(DeptId, BegDate, EndDate)
        End Function

        Public Function GetEmployeeAttendanceSummuryChartDataForReport(EmployeeId As Integer, BegDate As Date, EndDate As Date) As DataSet
            Return New DAAttedanceLog().GetEmployeeAttendanceSummuryChartDataForReport(EmployeeId, BegDate, EndDate)
        End Function

        Public Function GetAttendanceChartDataByDepartment(DepartmentId As Integer, BegDate As Date, EndDate As Date, Optional ByVal Lang As String = "ar") As DataSet
            Return New DAAttedanceLog().GetAttendanceChartDataByDepartment(DepartmentId, BegDate, EndDate, Lang)
        End Function

        Public Function GetAttendanceChartDataForReportComparssion(DepartmentId As Integer, BegDate As Date, EndDate As Date, ByVal StatusId As Integer, Optional ByVal Lang As String = "ar") As DataSet

            Return New DAAttedanceLog().GetAttendanceChartDataForReportComparssion(DepartmentId, BegDate, EndDate, StatusId, Lang)
        End Function

        Public Function GetAttendanceStatusList() As System.Data.DataSet
            Return New DAAttedanceLog().GetAttendanceStatusList()

        End Function

        Public Function GetAttendanceStatusList(Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Return New DAAttedanceLog().GetAttendanceStatusList(Lang)

        End Function

#End Region

#Region " Reports Data "
        Public Function GetLateComersByDateDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Return New DAAttedanceLog().GetLateComersByDateDataset(DepartmentId, BegDate, EndDate, OptionNo)
        End Function
        Public Function GetLateComersByEmployeeIdDataset(EmployeeId As Integer, BegDate As Date, EndDate As Date) As System.Data.DataSet
            Return New DAAttedanceLog().GetLateComersByEmployeeIdDataset(EmployeeId, BegDate, EndDate)
        End Function

        Public Function GetAbsentsByDateDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Return New DAAttedanceLog().GetAbsentsByDateDataset(DepartmentId, BegDate, EndDate, OptionNo)
        End Function
        Public Function GetEmployeeAttendanceByIdDataset(ByVal EmployeeId As Integer, ByVal BegDate As Date, ByVal EndDate As Date) As System.Data.DataSet
            Return New DAAttedanceLog().GetEmployeeAttendanceByIdDataset(EmployeeId, BegDate, EndDate)
        End Function
        Public Function GetAbsentsByEmployeeIdDataset(ByVal EmployeeId As Integer, ByVal BegDate As Date, ByVal EndDate As Date) As System.Data.DataSet
            Return New DAAttedanceLog().GetAbsentsByEmployeeIdDataset(EmployeeId, BegDate, EndDate)
        End Function
        Public Function GetEmployeesStatisticsDataset(
        ByVal DepartmentId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        ByVal SelectOptionNo As Integer,
        ByVal SortOptionNo As Integer,
        ByVal FilterOptionNo As Integer) As System.Data.DataSet
            Return New DAAttedanceLog().GetEmployeesStatisticsDataset(DepartmentId, BegDate, EndDate, SelectOptionNo, SortOptionNo, FilterOptionNo)
        End Function
        

        Public Function GetUncompleteTransDataset(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Return New DAAttedanceLog().GetUncompleteTransDataset(DepartmentId, BegDate, EndDate, OptionNo)
        End Function

        Public Function GetSanctionNotification(DepartmentId As Integer, BegDate As Date, EndDate As Date, OptionNo As Integer) As System.Data.DataSet
            Return New DAAttedanceLog().GetSanctionNotification(DepartmentId, BegDate, EndDate, OptionNo)
        End Function

#End Region

#Region " Miscellaneous "
        Private intFilterOption As enumFilterOption
        Public Enum enumFilterOption As Integer
            AllEmployees = 1
            ALLCivilians = 2
            AllMilitaryEmployees = 3
            MilitaryOfficersOnly = 4
            MilitaryNoneOfficersOnly = 5
        End Enum
        Private intSortOption As enumSortOption
        Public Enum enumSortOption As Integer
            OrderByEmployeeName = 1
            OrderByRankPostion = 2
            OrderByEmployeeId = 3
        End Enum
        Public Property FilterOption() As enumFilterOption
            Get
                Return intFilterOption
            End Get
            Set(ByVal value As enumFilterOption)
                intFilterOption = value
            End Set
        End Property
        Public Property SortOption() As enumSortOption
            Get
                Return intSortOption
            End Get
            Set(ByVal value As enumSortOption)
                intSortOption = value
            End Set
        End Property
#End Region

    End Class
#End Region

#Region " BOEAttendanceLog "
    Public Class BOEAttendanceLog
        Inherits BOEntityBase

    End Class
#End Region

End Namespace


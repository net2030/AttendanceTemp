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

#Region " BOMachineLog "
    Public Class BOMachineLog
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineLog As New DAMachineLog
            If oMachineLog.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oMachineLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineLog.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oMachineLog As New DAMachineLog
            Return MapEntityToProperties(oMachineLog.Find(Id))
        End Function
        Public Function GetLogByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        ByVal LogBegDate As Date, _
        ByVal LogEndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAMachineLog
            ds = oEmployee.GetLogByEmployeeIdDataset(EmployeeId, LogBegDate, LogEndDate, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function

        Public Function GetLogByAttendanceLogIdDataset(ByVal LogId As Integer,
                                                    Optional ByVal PageNo As Integer = 1, _
                                                    Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAMachineLog
            ds = oEmployee.GetLogByAttendanceLogIdDataset(LogId, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function

Public Function GetManualLogsFromMachineLog(DepartmentID As Integer, BegDate As Date, EndDate As Date) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAMachineLog
            ds = oEmployee.GetManualLogsFromMachineLog(DepartmentID, BegDate, EndDate)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bMachineLog As New BOEMachineLog

            If Not IsNothing(Entity) Then
                Dim eMachineLog As MachineLog = CType(Entity, MachineLog)
                With bMachineLog
                    .AddedDate = eMachineLog.AddedDate
                    .AddedUserAccountId = eMachineLog.AddedUserAccountId
                    .UpdatedDate = eMachineLog.UpdatedDate
                    .UpdatedUserAccountId = eMachineLog.UpdatedUserAccountId
                    .VersionNo = eMachineLog.VersionNo
                    .AddedUserName = eMachineLog.AddedUserName
                    .UpdatedUserName = eMachineLog.UpdatedUserName

                    .LogId = eMachineLog.LogId
                    .EmployeeId = eMachineLog.EmployeeId
                    .LogDate = eMachineLog.LogDate
                    .LogTime = eMachineLog.LogTime
                    .LogTypeId = eMachineLog.LogTypeId
                    .IPAddress = eMachineLog.IPAddress
                    .MachineId = eMachineLog.MachineId
                    .IsValidRecord = eMachineLog.IsValidRecord
                    .IsManualRecord = eMachineLog.IsManualRecord
                End With
            End If

            Return bMachineLog
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eMachineLog As New MachineLog

            If Not IsNothing(Entity) Then
                Dim bAttendanceLog As BOEMachineLog = CType(Entity, BOEMachineLog)
                With eMachineLog
                    .AddedDate = bAttendanceLog.AddedDate
                    .AddedUserAccountId = bAttendanceLog.AddedUserAccountId
                    .UpdatedDate = bAttendanceLog.UpdatedDate
                    .UpdatedUserAccountId = bAttendanceLog.UpdatedUserAccountId
                    .VersionNo = bAttendanceLog.VersionNo

                    .LogId = bAttendanceLog.LogId
                    .EmployeeId = bAttendanceLog.EmployeeId
                    .LogDate = bAttendanceLog.LogDate
                    .LogTime = bAttendanceLog.LogTime
                    .LogTypeId = bAttendanceLog.LogTypeId
                    .IPAddress = bAttendanceLog.IPAddress
                    .MachineId = bAttendanceLog.MachineId
                    .IsValidRecord = bAttendanceLog.IsValidRecord
                    .IsManualRecord = bAttendanceLog.IsManualRecord
                End With
            End If

            Return eMachineLog
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal EmployeeId As Integer, _
        ByVal LogDate As Date, _
        ByVal LogTime As Date, _
        ByVal LogTypeId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineLog As New DAMachineLog
            If oMachineLog.Add( _
                             EmployeeId,
                             LogDate,
                             LogTime,
                             LogTypeId,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oMachineLog.Identity
                MyBase.InfoMessage = oMachineLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineLog.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal LogId As Integer, _
        ByVal EmployeeId As Integer, _
        ByVal LogDate As Date, _
        ByVal LogTime As Date, _
        ByVal LogTypeId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineLog As New DAMachineLog
            If oMachineLog.Update( _
                             LogId,
                             EmployeeId,
                             LogDate,
                             LogTime,
                             LogTypeId,
                             UserAccountId,
                             VersionNo) Then
                boolSeccessed = True
                MyBase.Identity = oMachineLog.Identity
                MyBase.InfoMessage = oMachineLog.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineLog.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function ChangeLogType(ByVal LogId As Integer) As Boolean
            Return New DAMachineLog().ChangeLogType(LogId)
        End Function

        Public Function GetLogTypesList() As DataSet
            Return New DAMachineLog().GetLogTypesList
        End Function

        Public Function GetDevicesChartData() As System.Data.DataSet
            Return New DAMachineLog().GetDevicesChartData

        End Function

#End Region

    End Class
#End Region

#Region " BOEMachineLog "
    Public Class BOEMachineLog
        Inherits BOEntityBase


#Region " Constructor "
        Public Sub New()

        End Sub
#End Region

#Region " Private Variables  "
        Private intLogId As Integer
        Private intEmployeeId As Integer
        Private datLogDate As Date
        Private datLogTime As Date
        Private intLogTypeId As Integer
        Private strIPAddress As String
        Private intMachineId As Integer
        Private bolIsValidRecord As Boolean
        Private bolIsManualRecord As Boolean
#End Region

#Region " Public Properties "
        Public Property LogId() As Integer
            Get
                Return intLogId
            End Get
            Set(ByVal value As Integer)
                intLogId = value
            End Set
        End Property
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Public Property LogDate() As Date
            Get
                Return datLogDate
            End Get
            Set(ByVal value As Date)
                datLogDate = value
            End Set
        End Property
        Public Property LogTime() As Date
            Get
                Return datLogTime
            End Get
            Set(ByVal value As Date)
                datLogTime = value
            End Set
        End Property
        Public Property LogTypeId() As Integer
            Get
                Return intLogTypeId
            End Get
            Set(ByVal value As Integer)
                intLogTypeId = value
            End Set
        End Property
        Public Property IPAddress() As String
            Get
                Return strIPAddress
            End Get
            Set(ByVal value As String)
                strIPAddress = value
            End Set
        End Property
        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
            End Set
        End Property
        Public Property IsValidRecord() As Boolean
            Get
                Return bolIsValidRecord
            End Get
            Set(ByVal value As Boolean)
                bolIsValidRecord = value
            End Set
        End Property
        Public Property IsManualRecord() As Boolean
            Get
                Return bolIsManualRecord
            End Get
            Set(ByVal value As Boolean)
                bolIsManualRecord = value
            End Set
        End Property
#End Region

#Region " Mapping "
        Public Sub MapEntityToProperties(ByVal entity As ICommonAttributes)
            If Not IsNothing(entity) Then
                MyBase.AddedDate = entity.AddedDate
                MyBase.AddedUserAccountId = entity.AddedUserAccountId
                MyBase.UpdatedDate = entity.UpdatedDate
                MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
                MyBase.VersionNo = entity.VersionNo
                MyBase.AddedUserName = entity.AddedUserName
                MyBase.UpdatedUserName = entity.UpdatedUserName
                Me.MapEntityToCustomProperties(entity)
            End If
        End Sub
        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
            Dim eMachineLog As MachineLog = CType(entity, MachineLog)
            With eMachineLog
                intLogId = .LogId
                intEmployeeId = .EmployeeId
                datLogDate = .LogDate
                datLogTime = .LogTime
                intLogTypeId = .LogTypeId
                strIPAddress = .IPAddress
                intMachineId = .MachineId
                bolIsValidRecord = .IsValidRecord
                bolIsManualRecord = .IsManualRecord
            End With
        End Sub
#End Region


    End Class
#End Region

End Namespace


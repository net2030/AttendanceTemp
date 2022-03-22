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

#Region " BOWorkSchedule "
    Public Class BOWorkSchedule
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkSchedule As New DAWorkSchedule
            If oWorkSchedule.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oWorkSchedule As New DAWorkSchedule
            Return MapEntityToProperties(oWorkSchedule.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
           Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bWorkSchedule As New BOEWorkSchedule

            If Not IsNothing(Entity) Then
                Dim eWorkSchedule As WorkSchedule = CType(Entity, WorkSchedule)
                With bWorkSchedule
                    .AddedDate = eWorkSchedule.AddedDate
                    .AddedUserAccountId = eWorkSchedule.AddedUserAccountId
                    .UpdatedDate = eWorkSchedule.UpdatedDate
                    .UpdatedUserAccountId = eWorkSchedule.UpdatedUserAccountId
                    .VersionNo = eWorkSchedule.VersionNo

                    .WorkScheduleId = eWorkSchedule.WorkScheduleId
                    .WorkScheduleTypeId = eWorkSchedule.WorkScheduleTypeId
                    .DepartmentId = eWorkSchedule.DepartmentId
                    .WorkScheduleName = eWorkSchedule.WorkScheduleName
                    .ScheduleBegDate = eWorkSchedule.ScheduleBegDate
                    .ScheduleEndDate = eWorkSchedule.ScheduleEndDate
                    .IsEffectiveDuringHoliday = eWorkSchedule.IsEffectiveDuringHoliday
                    .IsPolicyApplied = eWorkSchedule.IsPolicyApplied
                    .PolicyId = eWorkSchedule.PolicyId

                    .AddedUserName = eWorkSchedule.AddedUserName
                    .UpdatedUserName = eWorkSchedule.UpdatedUserName
                End With
            End If

            Return bWorkSchedule
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkSchedule As New WorkSchedule

            If Not IsNothing(Entity) Then
                Dim bWorkSchedule As BOEWorkSchedule = CType(Entity, BOEWorkSchedule)
                With eWorkSchedule
                    .AddedDate = bWorkSchedule.AddedDate
                    .AddedUserAccountId = bWorkSchedule.AddedUserAccountId
                    .UpdatedDate = bWorkSchedule.UpdatedDate
                    .UpdatedUserAccountId = bWorkSchedule.UpdatedUserAccountId
                    .VersionNo = bWorkSchedule.VersionNo

                    .WorkScheduleId = bWorkSchedule.WorkScheduleId
                    .WorkScheduleTypeId = bWorkSchedule.WorkScheduleTypeId
                    .DepartmentId = bWorkSchedule.DepartmentId
                    .WorkScheduleName = bWorkSchedule.WorkScheduleName
                    .ScheduleBegDate = bWorkSchedule.ScheduleBegDate
                    .ScheduleEndDate = bWorkSchedule.ScheduleEndDate
                    .IsEffectiveDuringHoliday = bWorkSchedule.IsEffectiveDuringHoliday
                    .IsPolicyApplied = bWorkSchedule.IsPolicyApplied
                    .PolicyId = bWorkSchedule.PolicyId
                End With
            End If

            Return eWorkSchedule
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal WorkScheduleName As String,
        ByVal WorkScheduleTypeId As Integer,
        ByVal DepartmentId As Integer,
        ByVal ScheduleBegDate As Date,
        ByVal ScheduleEndDate As Date,
        ByVal IsEffectiveDuringHoliday As Boolean,
        ByVal IsPolicyApplied As Boolean,
        ByVal PolicyId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkSchedule As New DAWorkSchedule
            If oWorkSchedule.Add( _
                             WorkScheduleName,
                             WorkScheduleTypeId,
                             DepartmentId,
                             ScheduleBegDate,
                             ScheduleEndDate,
                             IsEffectiveDuringHoliday,
                             IsPolicyApplied,
                             PolicyId,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oWorkSchedule.Identity
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal WorkScheduleId As Integer, _
        ByVal WorkScheduleName As String,
        ByVal WorkScheduleTypeId As Integer,
        ByVal DepartmentId As Integer,
        ByVal ScheduleBegDate As Date,
        ByVal ScheduleEndDate As Date,
        ByVal IsEffectiveDuringHoliday As Boolean,
        ByVal IsPolicyApplied As Boolean,
        ByVal PolicyId As Integer,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkSchedule As New DAWorkSchedule
            If oWorkSchedule.Update(WorkScheduleId,
                                    WorkScheduleName,
                                    WorkScheduleTypeId,
                                    DepartmentId,
                                    ScheduleBegDate,
                                    ScheduleEndDate,
                                    IsEffectiveDuringHoliday,
                                    IsPolicyApplied,
                                    PolicyId,
                                    UserAccountId,
                                    VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkSchedule.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetWorkSchedulesDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkSchedule As New DAWorkSchedule
            ds = oWorkSchedule.GetWorkSchedulesDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oWorkSchedule.PageTotal

            Return ds
        End Function
        Public Function GetWorkScheduleTypesList() As DataSet
            Return New DAWorkSchedule().GetWorkScheduleTypesList
        End Function
#End Region

#Region " Save "
        Public Function Save(ByVal WorkSchedule As BOEWorkSchedule, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            If WorkSchedule.Days.Count < 7 Then
                MyBase.InfoMessage = "يجب إضافة وتحديد جميع أيام الأسبوع لجدول العمل"
                Return False
            End If

            Using TS As New TransactionScope()

                Select Case WorkSchedule.DBAction
                    Case BOEntityBase.DBActionEnum.Add
                        If Not Me.Add(
                                  WorkSchedule.WorkScheduleName,
                                  WorkSchedule.WorkScheduleTypeId,
                                  WorkSchedule.DepartmentId,
                                  WorkSchedule.ScheduleBegDate,
                                  WorkSchedule.ScheduleEndDate,
                                  WorkSchedule.IsEffectiveDuringHoliday,
                                  WorkSchedule.IsPolicyApplied,
                                  WorkSchedule.PolicyId,
                                  UserAccountId) Then
                            Return False
                        Else
                            ' Add Work Schedule days
                            Dim oDay As New BOWorkScheduleDay
                            For Each eDay As BOEWorkScheduleDay In WorkSchedule.Days
                                If Not oDay.Add(Me.Identity,
                                                eDay.WeekDayNo,
                                                eDay.IsWeekendDay,
                                                eDay.WorkBegTime,
                                                eDay.WorkEndTime,
                                                UserAccountId) Then
                                    MyBase.InfoMessage = oDay.InfoMessage
                                    Return False
                                End If
                            Next
                        End If

                    Case BOEntityBase.DBActionEnum.Update
                        If Not Me.Update(
                                  WorkSchedule.WorkScheduleId,
                                  WorkSchedule.WorkScheduleName,
                                  WorkSchedule.WorkScheduleTypeId,
                                  WorkSchedule.DepartmentId,
                                  WorkSchedule.ScheduleBegDate,
                                  WorkSchedule.ScheduleEndDate,
                                  WorkSchedule.IsEffectiveDuringHoliday,
                                  WorkSchedule.IsPolicyApplied,
                                  WorkSchedule.PolicyId,
                                  UserAccountId,
                                  WorkSchedule.VersionNo) Then
                            Return False
                        End If

                    Case BOEntityBase.DBActionEnum.Delete
                        If Not Me.Delete(WorkSchedule.WorkScheduleId) Then
                            Return False
                        End If
                End Select

                TS.Complete()
                boolSeccessed = True
            End Using


            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region

#Region " BOWorkScheduleDay "
    Public Class BOWorkScheduleDay
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Throw New NotImplementedException
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bWorkScheduleDay As New BOEWorkScheduleDay

            If Not IsNothing(Entity) Then
                Dim eWorkSchedule As WorkScheduleDay = CType(Entity, WorkScheduleDay)
                With bWorkScheduleDay
                    .AddedDate = eWorkSchedule.AddedDate
                    .AddedUserAccountId = eWorkSchedule.AddedUserAccountId
                    .UpdatedDate = eWorkSchedule.UpdatedDate
                    .UpdatedUserAccountId = eWorkSchedule.UpdatedUserAccountId
                    .VersionNo = eWorkSchedule.VersionNo

                    .WorkScheduleId = eWorkSchedule.WorkScheduleId
                    .WeekDayNo = eWorkSchedule.WeekDayNo
                    .IsWeekendDay = eWorkSchedule.IsWeekendDay
                    .WorkBegTime = eWorkSchedule.WorkBegTime
                    .WorkEndTime = eWorkSchedule.WorkEndTime

                    .AddedUserName = eWorkSchedule.AddedUserName
                    .UpdatedUserName = eWorkSchedule.UpdatedUserName
                End With
            End If

            Return bWorkScheduleDay
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkScheduleDay As New WorkScheduleDay

            If Not IsNothing(Entity) Then
                Dim bWorkScheduleDay As BOEWorkScheduleDay = CType(Entity, BOEWorkScheduleDay)
                With eWorkScheduleDay
                    .AddedDate = bWorkScheduleDay.AddedDate
                    .AddedUserAccountId = bWorkScheduleDay.AddedUserAccountId
                    .UpdatedDate = bWorkScheduleDay.UpdatedDate
                    .UpdatedUserAccountId = bWorkScheduleDay.UpdatedUserAccountId
                    .VersionNo = bWorkScheduleDay.VersionNo

                    .WorkScheduleId = bWorkScheduleDay.WorkScheduleId
                    .WeekDayNo = bWorkScheduleDay.WeekDayNo
                    .IsWeekendDay = bWorkScheduleDay.IsWeekendDay
                    .WorkBegTime = bWorkScheduleDay.WorkBegTime
                    .WorkEndTime = bWorkScheduleDay.WorkEndTime
                End With
            End If

            Return eWorkScheduleDay
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal WorkScheduleId As Integer,
        ByVal WeekDayNo As Integer,
        ByVal IsWeekendDay As Boolean,
        ByVal WorkBegTime As DateTime,
        ByVal WorkEndTime As DateTime,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkScheduleDay As New DAWorkScheduleDay
            If oWorkScheduleDay.Add( _
                             WorkScheduleId,
                             WeekDayNo,
                             IsWeekendDay,
                             WorkBegTime,
                             WorkEndTime,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oWorkScheduleDay.Identity
                MyBase.InfoMessage = oWorkScheduleDay.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkScheduleDay.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal WorkScheduleId As Integer,
        ByVal WeekDayNo As Integer,
        ByVal IsWeekendDay As Boolean,
        ByVal WorkBegTime As DateTime,
        ByVal WorkEndTime As DateTime,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkScheduleDay As New DAWorkScheduleDay
            If oWorkScheduleDay.Update(WorkScheduleId,
                                       WeekDayNo,
                                       IsWeekendDay,
                                       WorkBegTime,
                                       WorkEndTime,
                                       UserAccountId,
                                       VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkScheduleDay.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkScheduleDay.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function FindDay(ByVal WorkScheduleId As Integer, ByVal WeekDayNo As Integer) As BOEntityBase
            Dim oWorkScheduleDay As New DAWorkScheduleDay
            Return MapEntityToProperties(oWorkScheduleDay.FindDay(WorkScheduleId, WeekDayNo))
        End Function
        Public Function GetWorkScheduleDaysDataset(ByVal WorkScheduleId As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oWorkSchedule As New DAWorkScheduleDay
            ds = oWorkSchedule.GetWorkScheduleDaysDataset(WorkScheduleId)
            MyBase.PageTotal = oWorkSchedule.PageTotal
            Return ds
        End Function
#End Region

    End Class
#End Region
#Region " BOWorkScheduleEmployee "
    Public Class BOWorkScheduleEmployee
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oWorkScheduleEmployee As New DAWorkScheduleEmployee
            Return MapEntityToProperties(oWorkScheduleEmployee.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bWorkScheduleEmployee As New BOEWorkScheduleEmployee

            If Not IsNothing(Entity) Then
                Dim eWorkScheduleEmployee As WorkScheduleEmployee = CType(Entity, WorkScheduleEmployee)
                With bWorkScheduleEmployee
                    .AddedDate = eWorkScheduleEmployee.AddedDate
                    .AddedUserAccountId = eWorkScheduleEmployee.AddedUserAccountId
                    .UpdatedDate = eWorkScheduleEmployee.UpdatedDate
                    .UpdatedUserAccountId = eWorkScheduleEmployee.UpdatedUserAccountId
                    .VersionNo = eWorkScheduleEmployee.VersionNo

                    .WorkScheduleId = eWorkScheduleEmployee.WorkScheduleId
                    .EmployeeId = eWorkScheduleEmployee.EmployeeId
                End With
            End If

            Return bWorkScheduleEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkScheduleEmployee As New WorkScheduleEmployee

            If Not IsNothing(Entity) Then
                Dim bWorkScheduleEmployee As BOEWorkScheduleEmployee = CType(Entity, BOEWorkScheduleEmployee)
                With eWorkScheduleEmployee
                    .AddedDate = bWorkScheduleEmployee.AddedDate
                    .AddedUserAccountId = bWorkScheduleEmployee.AddedUserAccountId
                    .UpdatedDate = bWorkScheduleEmployee.UpdatedDate
                    .UpdatedUserAccountId = bWorkScheduleEmployee.UpdatedUserAccountId
                    .VersionNo = bWorkScheduleEmployee.VersionNo

                    .WorkScheduleId = bWorkScheduleEmployee.WorkScheduleId
                    .EmployeeId = bWorkScheduleEmployee.EmployeeId
                End With
            End If

            Return eWorkScheduleEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee(
        ByVal WorkScheduleId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkScheduleEmployee As New DAWorkScheduleEmployee
            If oWorkScheduleEmployee.AddEmployee(WorkScheduleId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function AddAllEmployees(
        ByVal workScheduleId As Integer,
        ByVal departmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkScheduleEmployee As New DAWorkScheduleEmployee
            If oWorkScheduleEmployee.AddAllEmployees(WorkScheduleId, departmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteEmployee(
        ByVal WorkScheduleId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkScheduleEmployee As New DAWorkScheduleEmployee
            If oWorkScheduleEmployee.DeleteEmployee(WorkScheduleId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkScheduleEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetWorkScheduleEmployeesDataset(
        ByVal WorkScheduleId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkScheduleEmployee As New DAWorkScheduleEmployee
            ds = oWorkScheduleEmployee.GetWorkScheduleEmployeesDataset(WorkScheduleId, PageNo, PageSize)
            MyBase.PageTotal = oWorkScheduleEmployee.PageTotal

            Return ds
        End Function
        Public Function IsExistsInWorkSchedule(ByVal WorkScheduleId As Integer, ByVal EmployeeId As Integer) As Boolean
            Return New DAWorkScheduleEmployee().IsExistsInWorkSchedule(WorkScheduleId, EmployeeId)
        End Function
#End Region

    End Class
#End Region


#Region " BOEWorkSchedule "
    Public Class BOEWorkSchedule
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
            colDays = New BOWorkScheduleDaysList
            colEmployees = New BOWorkScheduleEmployeesList
        End Sub
#End Region

#Region " Private Variables  "
        Private intWorkScheduleId As Integer
        Private intWorkScheduleTypeId As Integer
        Private strWorkScheduleName As String
        Private datScheduleBegDate As Date
        Private datScheduleEndDate As Date
        Private bolIsEffectiveDuringHoliday As Boolean
        Private bolIsPolicyApplied As Boolean
        Private intPolicyId As Integer
        Private intDepartmentId As Integer

        Private colDays As BOWorkScheduleDaysList
        Private colEmployees As BOWorkScheduleEmployeesList
#End Region
#Region " Public Properties "
        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property
        Public Property WorkScheduleTypeId() As Integer
            Get
                Return intWorkScheduleTypeId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleTypeId = value
            End Set
        End Property
        Public Property WorkScheduleName() As String
            Get
                Return strWorkScheduleName
            End Get
            Set(ByVal value As String)
                strWorkScheduleName = value
            End Set
        End Property
        Public Property ScheduleBegDate() As Date
            Get
                Return datScheduleBegDate
            End Get
            Set(ByVal value As Date)
                datScheduleBegDate = value
            End Set
        End Property
        Public Property ScheduleEndDate() As Date
            Get
                Return datScheduleEndDate
            End Get
            Set(ByVal value As Date)
                datScheduleEndDate = value
            End Set
        End Property
        Public Property IsEffectiveDuringHoliday() As Boolean
            Get
                Return bolIsEffectiveDuringHoliday
            End Get
            Set(ByVal value As Boolean)
                bolIsEffectiveDuringHoliday = value
            End Set
        End Property
        Public Property IsPolicyApplied() As Boolean
            Get
                Return bolIsPolicyApplied
            End Get
            Set(ByVal value As Boolean)
                bolIsPolicyApplied = value
            End Set
        End Property
        Public Property PolicyId() As Integer
            Get
                Return intPolicyId
            End Get
            Set(ByVal value As Integer)
                intPolicyId = value
            End Set
        End Property
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property



        Public Property Days() As BOWorkScheduleDaysList
            Get
                Return colDays
            End Get
            Set(ByVal value As BOWorkScheduleDaysList)
                colDays = value
            End Set
        End Property
        Public Property Employees() As BOWorkScheduleEmployeesList
            Get
                Return colEmployees
            End Get
            Set(ByVal value As BOWorkScheduleEmployeesList)
                colEmployees = value
            End Set
        End Property
#End Region

#Region " Properties Mapping "
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
            Dim eWorkSchedule As WorkSchedule = CType(entity, WorkSchedule)
            With eWorkSchedule
                intWorkScheduleId = .WorkScheduleId
                intWorkScheduleTypeId = .WorkScheduleTypeId
                strWorkScheduleName = .WorkScheduleName
                datScheduleBegDate = .ScheduleBegDate
                datScheduleEndDate = .ScheduleEndDate
                bolIsEffectiveDuringHoliday = .IsEffectiveDuringHoliday
                bolIsPolicyApplied = .IsPolicyApplied
                intPolicyId = .PolicyId
                intDepartmentId = .DepartmentId
            End With
        End Sub
#End Region

    End Class
#End Region
#Region " BOEWorkScheduleDay "
    Public Class BOEWorkScheduleDay
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intWorkScheduleId As Integer
        Private intWeekDayNo As Integer
        Private bolIsWeekendDay As Boolean
        Private datWorkBegTime As DateTime
        Private datWorkEndTime As DateTime
#End Region
#Region " Public Properties "
        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property
        Public Property WeekDayNo() As Integer
            Get
                Return intWeekDayNo
            End Get
            Set(ByVal value As Integer)
                intWeekDayNo = value
            End Set
        End Property
        Public Property IsWeekendDay() As Boolean
            Get
                Return bolIsWeekendDay
            End Get
            Set(ByVal value As Boolean)
                bolIsWeekendDay = value
            End Set
        End Property
        Public Property WorkBegTime() As DateTime
            Get
                Return datWorkBegTime
            End Get
            Set(ByVal value As DateTime)
                datWorkBegTime = value
            End Set
        End Property
        Public Property WorkEndTime() As DateTime
            Get
                Return datWorkEndTime
            End Get
            Set(ByVal value As DateTime)
                datWorkEndTime = value
            End Set
        End Property
#End Region

#Region " Properties Mapping "
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
            Dim eWorkScheduleDay As WorkScheduleDay = CType(entity, WorkScheduleDay)
            With eWorkScheduleDay
                intWorkScheduleId = .WorkScheduleId
                intWeekDayNo = .WeekDayNo
                bolIsWeekendDay = .IsWeekendDay
                datWorkBegTime = .WorkBegTime
                datWorkEndTime = .WorkEndTime
            End With
        End Sub
#End Region

    End Class
#End Region
#Region " BOEWorkScheduleEmployee "
    Public Class BOEWorkScheduleEmployee
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intWorkScheduleId As Integer
        Private intEmployeeId As Integer
#End Region
#Region " Public Properties "
        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
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
#End Region

#Region " Properties Mapping "
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
            Dim eWorkScheduleEmployee As WorkScheduleEmployee = CType(entity, WorkScheduleEmployee)
            With eWorkScheduleEmployee
                intWorkScheduleId = .WorkScheduleId
                intEmployeeId = .EmployeeId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOWorkScheduleDaysList "
    Public Class BOWorkScheduleDaysList
        Inherits BOListBase(Of BOEWorkScheduleDay)

        Public Sub Load(ByVal WorkScheduleId As Integer)
            LoadFromList(New DAWorkScheduleDay().LoadDays(WorkScheduleId))
        End Sub
        Private Sub LoadFromList(ByVal WorkScheduleDays As List(Of WorkScheduleDay))
            If WorkScheduleDays.Count > 0 Then
                For Each eWorkScheduleDay As WorkScheduleDay In WorkScheduleDays
                    Dim bWorkScheduleDay As New BOEWorkScheduleDay
                    bWorkScheduleDay.MapEntityToProperties(eWorkScheduleDay)
                    Me.Add(bWorkScheduleDay)
                Next
            End If
        End Sub
    End Class
#End Region
#Region " BOWorkScheduleEmployeesList "
    Public Class BOWorkScheduleEmployeesList
        Inherits BOListBase(Of BOEWorkScheduleEmployee)

        Public Sub Load(ByVal WorkScheduleId As Integer)
            LoadFromList(New DAWorkScheduleEmployee().LoadEmployees(WorkScheduleId))
        End Sub
        Private Sub LoadFromList(ByVal WorkScheduleEmployees As List(Of WorkScheduleEmployee))
            If WorkScheduleEmployees.Count > 0 Then
                For Each eWorkScheduleEmployee As WorkScheduleEmployee In WorkScheduleEmployees
                    Dim bWorkScheduleEmployee As New BOEWorkScheduleEmployee
                    bWorkScheduleEmployee.MapEntityToProperties(eWorkScheduleEmployee)
                    Me.Add(bWorkScheduleEmployee)
                Next
            End If
        End Sub
    End Class
#End Region


End Namespace


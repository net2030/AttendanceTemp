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

#Region " BOEWorkException "
    Public Class BOEWorkException
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
            oWorkExceptionEmployees = New BOWorkExceptionEmployeesList
        End Sub
#End Region

#Region " Private Variables  "

        Private intWorkExceptionId As Integer
        Private intEmployeeId As Integer
        Private intWorkExceptionTypeId As Integer
        Private datWorkExceptionBegDate As Date
        Private datWorkExceptionEndDate As Date
        Private intDepartmentId As Integer
        Private strNotes As String

        Private oWorkExceptionEmployees As BOWorkExceptionEmployeesList
#End Region
#Region " Public Properties "
        Public Property WorkExceptionId() As Integer
            Get
                Return intWorkExceptionId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionId = value
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
        Public Property WorkExceptionTypeId() As Integer
            Get
                Return intWorkExceptionTypeId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionTypeId = value
            End Set
        End Property
        Public Property WorkExceptionBegDate() As Date
            Get
                Return datWorkExceptionBegDate
            End Get
            Set(ByVal value As Date)
                datWorkExceptionBegDate = value
            End Set
        End Property
        Public Property WorkExceptionEndDate() As Date
            Get
                Return datWorkExceptionEndDate
            End Get
            Set(ByVal value As Date)
                datWorkExceptionEndDate = value
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
        Public Property Notes() As String
            Get
                Return strNotes
            End Get
            Set(ByVal value As String)
                strNotes = value
            End Set
        End Property

        Public Property WorkExceptionEmployees() As BOWorkExceptionEmployeesList
            Get
                Return oWorkExceptionEmployees
            End Get
            Set(ByVal value As BOWorkExceptionEmployeesList)
                oWorkExceptionEmployees = value
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
            Dim eWorkException As WorkException = CType(entity, WorkException)
            With eWorkException

                intWorkExceptionId = .WorkExceptionId
                intEmployeeId = .EmployeeId
                intWorkExceptionTypeId = .WorkExceptionTypeId
                datWorkExceptionBegDate = .WorkExceptionBegDate
                datWorkExceptionEndDate = .WorkExceptionEndDate
                intDepartmentId = .DepartmentId
                strNotes = .Notes
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOWorkException "
    Public Class BOWorkException
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkException As New DAWorkException
            If oWorkException.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkException.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkException.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oWorkException As New DAWorkException
            Return MapEntityToProperties(oWorkException.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bWorkException As New BOEWorkException

            If Not IsNothing(Entity) Then
                Dim eWorkException As WorkException = CType(Entity, WorkException)
                With bWorkException
                    .AddedDate = eWorkException.AddedDate
                    .AddedUserAccountId = eWorkException.AddedUserAccountId
                    .UpdatedDate = eWorkException.UpdatedDate
                    .UpdatedUserAccountId = eWorkException.UpdatedUserAccountId
                    .VersionNo = eWorkException.VersionNo

                    .WorkExceptionId = eWorkException.WorkExceptionId
                    .EmployeeId = eWorkException.EmployeeId
                    .WorkExceptionTypeId = eWorkException.WorkExceptionTypeId
                    .WorkExceptionBegDate = eWorkException.WorkExceptionBegDate
                    .WorkExceptionEndDate = eWorkException.WorkExceptionEndDate
                    .DepartmentId = eWorkException.DepartmentId
                    .Notes = eWorkException.Notes

                    .AddedUserName = eWorkException.AddedUserName
                    .UpdatedUserName = eWorkException.UpdatedUserName
                End With
            End If

            Return bWorkException
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkException As New WorkException

            If Not IsNothing(Entity) Then
                Dim bWorkException As BOEWorkException = CType(Entity, BOEWorkException)
                With eWorkException
                    .AddedDate = bWorkException.AddedDate
                    .AddedUserAccountId = bWorkException.AddedUserAccountId
                    .UpdatedDate = bWorkException.UpdatedDate
                    .UpdatedUserAccountId = bWorkException.UpdatedUserAccountId
                    .VersionNo = bWorkException.VersionNo

                    .WorkExceptionId = bWorkException.WorkExceptionId
                    .WorkExceptionTypeId = bWorkException.WorkExceptionTypeId
                    .WorkExceptionBegDate = bWorkException.WorkExceptionBegDate
                    .WorkExceptionEndDate = bWorkException.WorkExceptionEndDate
                    .DepartmentId = bWorkException.DepartmentId
                    .Notes = bWorkException.Notes
                End With
            End If

            Return eWorkException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal EmployeeId As Integer,
        ByVal WorkExceptionTypeId As Integer,
        ByVal WorkExceptionBegDate As Date,
        ByVal WorkExceptionEndDate As Date,
        ByVal DepartmentId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkException As New DAWorkException
            If oWorkException.Add(
                             EmployeeId,
                             WorkExceptionTypeId,
                             WorkExceptionBegDate,
                             WorkExceptionEndDate,
                             DepartmentId,
                             Notes,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oWorkException.Identity
                MyBase.InfoMessage = oWorkException.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkException.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal WorkExceptionId As Integer,
        ByVal EmployeeId As Integer,
        ByVal WorkExceptionTypeId As Integer,
        ByVal WorkExceptionBegDate As Date,
        ByVal WorkExceptionEndDate As Date,
        ByVal DepartmentId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkException As New DAWorkException
            If oWorkException.Update(WorkExceptionId,
                                     EmployeeId,
                                     WorkExceptionTypeId,
                                     WorkExceptionBegDate,
                                     WorkExceptionEndDate,
                                     DepartmentId,
                                     Notes,
                                     UserAccountId,
                                     VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkException.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkException.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetWorkExceptionsDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionsDataset(UserAccountId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function
        Public Function GetWorkExceptionsByDepartmentDataset( _
        ByVal DepartmentId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionsByDepartmentDataset(DepartmentId, BegDate, EndDate, PageNo, PageSize, Lang)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function
        Public Function GetWorkExceptionsByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionsByEmployeeIdDataset(EmployeeId, BegDate, EndDate, PageNo, PageSize, Lang)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function
        Public Function GetWorkExceptionsByByTypeDataset(
        ByVal UserAccountId As Integer,
        ByVal ExceptionTypeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionsByByTypeDataset(UserAccountId, ExceptionTypeId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function

        Public Function GetWorkExceptionByEmployeeManagerForApprovalDataset( _
       ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionByEmployeeManagerForApprovalDataset(ManagerID)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function

        Public Function GetWorkExceptionByEmployeeManagerForApprovalDataset1( _
       ByVal ManagerID As Integer,
       Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionByEmployeeManagerForApprovalDataset1(ManagerID, Lang)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function

        Public Function GetWorkExceptionBySpecificRoleForApprovalDataset(ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionBySpecificRoleForApprovalDataset(ManagerID)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function

        Public Function AddWorkExceptionApproval(ByVal WorkExceptionId As Integer,
                                            ByVal ApprovalPolicyId As Integer,
                                            ByVal ApprovalPathId As Integer,
                                            ByVal IsApproved As Boolean,
                                            ByVal IsRejected As Boolean,
                                            ByVal Comments As String,
                                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkException As New DAWorkException

            If oWorkException.AddWorkExceptionApproval(WorkExceptionId,
                           ApprovalPolicyId,
                           ApprovalPathId,
                           IsApproved,
                           IsRejected,
                           Comments,
                           UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oWorkException.Identity
                MyBase.InfoMessage = oWorkException.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkException.InfoMessage
            End If

            Return boolSeccessed

        End Function


        Public Function UpdateWorkExceptionApprovalStatus(ByVal WorkExceptionId As Integer,
                                                     ByVal IsApproved As Boolean,
                                                     ByVal IsRejected As Boolean,
                                                     ByVal UserId As Integer,
                                                     ByVal Notes As String) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkException As New DAWorkException
            If oWorkException.UpdateWorkExceptionApprovalStatus(WorkExceptionId,
                                IsApproved,
                                IsRejected,
                                UserId,
                                Notes) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkException.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkException.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function GetWorkExceptionApprovalLogByWorkExceptionId(ByVal WorkExceptionId As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkException As New DAWorkException
            ds = oWorkException.GetWorkExceptionApprovalLogByWorkExceptionId(WorkExceptionId)
            MyBase.PageTotal = oWorkException.PageTotal

            Return ds
        End Function


#End Region

#Region " Save "
        Public Function Save(ByVal WorkException As BOEWorkException, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Using TS As New TransactionScope()

                Select Case WorkException.DBAction
                    Case BOEntityBase.DBActionEnum.Add
                        'If WorkException.WorkExceptionEmployees.Count = 0 Then
                        '    MyBase.InfoMessage = "يجب إضافة على الأقل موظف واحد للإستثناء من الدوام"
                        '    Return False
                        'End If

                        If Not Me.Add(
                                  WorkException.EmployeeId,
                                  WorkException.WorkExceptionTypeId,
                                  WorkException.WorkExceptionBegDate,
                                  WorkException.WorkExceptionEndDate,
                                  WorkException.DepartmentId,
                                  WorkException.Notes,
                                  UserAccountId) Then
                            Return False
                        Else
                            Dim oWorkExceptionEmployee As New BOWorkExceptionEmployee
                            For Each eEmployee As BOEWorkExceptionEmployee In WorkException.WorkExceptionEmployees
                                If Not oWorkExceptionEmployee.AddEmployee(Me.Identity, eEmployee.EmployeeId, UserAccountId) Then
                                    MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
                                    Return False
                                End If
                            Next
                        End If

                    Case BOEntityBase.DBActionEnum.Update
                        'If WorkException.WorkExceptionEmployees.Count = 0 Then
                        '    MyBase.InfoMessage = "يجب إضافة على الأقل موظف واحد للإستثناء من الدوام"
                        '    Return False
                        'End If

                        If Not Me.Update(
                                  WorkException.WorkExceptionId,
                                  WorkException.EmployeeId,
                                  WorkException.WorkExceptionTypeId,
                                  WorkException.WorkExceptionBegDate,
                                  WorkException.WorkExceptionEndDate,
                                  WorkException.DepartmentId,
                                  WorkException.Notes,
                                  UserAccountId,
                                  WorkException.VersionNo) Then
                            Return False
                        Else
                            Dim oWorkExceptionEmployee As New BOWorkExceptionEmployee
                            For Each eEmployee As BOEWorkExceptionEmployee In WorkException.WorkExceptionEmployees
                                Select Case eEmployee.DBAction
                                    Case BOEntityBase.DBActionEnum.Add
                                        If Not oWorkExceptionEmployee.AddEmployee(WorkException.WorkExceptionId, eEmployee.EmployeeId, UserAccountId) Then
                                            MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
                                            Return False
                                        End If
                                    Case BOEntityBase.DBActionEnum.Delete
                                        If Not oWorkExceptionEmployee.DeleteEmployee(WorkException.WorkExceptionId, eEmployee.EmployeeId) Then
                                            MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
                                            Return False
                                        End If
                                End Select
                            Next
                        End If

                    Case BOEntityBase.DBActionEnum.Delete
                        If Not Me.Delete(WorkException.WorkExceptionId) Then
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

    


#Region " BOEWorkExceptionType "
    Public Class BOEWorkExceptionType
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intWorkExceptionTypeId As Integer
        Private strWorkExceptionTypeName As String
        Private strWorkExceptionTypeNameEN As String
#End Region
#Region " Public Properties "
        Public Property WorkExceptionTypeId() As Integer
            Get
                Return intWorkExceptionTypeId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionTypeId = value
            End Set
        End Property

        Public Property WorkExceptionTypeName() As String
            Get
                Return strWorkExceptionTypeName
            End Get
            Set(ByVal value As String)
                strWorkExceptionTypeName = value
            End Set
        End Property

        Public Property WorkExceptionTypeNameEN() As String
            Get
                Return strWorkExceptionTypeNameEN
            End Get
            Set(ByVal value As String)
                strWorkExceptionTypeNameEN = value
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
            Dim eWorkExceptionType As WorkExceptionType = CType(entity, WorkExceptionType)
            With eWorkExceptionType
                intWorkExceptionTypeId = .WorkExceptionTypeId
                strWorkExceptionTypeName = .WorkExceptionTypeName
                strWorkExceptionTypeNameEN = .WorkExceptionTypeNameEN
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOWorkExceptionType "
    Public Class BOWorkExceptionType
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkExceptionType As New DAWorkExceptionType
            If oWorkExceptionType.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oWorkExceptionType As New DAWorkExceptionType
            Return MapEntityToProperties(oWorkExceptionType.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bWorkExceptionType As New BOEWorkExceptionType

            If Not IsNothing(Entity) Then
                Dim eWorkExceptionType As WorkExceptionType = CType(Entity, WorkExceptionType)
                With bWorkExceptionType
                    .AddedDate = eWorkExceptionType.AddedDate
                    .AddedUserAccountId = eWorkExceptionType.AddedUserAccountId
                    .UpdatedDate = eWorkExceptionType.UpdatedDate
                    .UpdatedUserAccountId = eWorkExceptionType.UpdatedUserAccountId
                    .VersionNo = eWorkExceptionType.VersionNo

                    .WorkExceptionTypeId = eWorkExceptionType.WorkExceptionTypeId
                    .WorkExceptionTypeName = eWorkExceptionType.WorkExceptionTypeName
                    .WorkExceptionTypeNameEN = eWorkExceptionType.WorkExceptionTypeNameEN

                    .AddedUserName = eWorkExceptionType.AddedUserName
                    .UpdatedUserName = eWorkExceptionType.UpdatedUserName
                End With
            End If

            Return bWorkExceptionType
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkExceptionType As New WorkExceptionType

            If Not IsNothing(Entity) Then
                Dim bWorkException As BOEWorkExceptionType = CType(Entity, BOEWorkExceptionType)
                With eWorkExceptionType
                    .AddedDate = bWorkException.AddedDate
                    .AddedUserAccountId = bWorkException.AddedUserAccountId
                    .UpdatedDate = bWorkException.UpdatedDate
                    .UpdatedUserAccountId = bWorkException.UpdatedUserAccountId
                    .VersionNo = bWorkException.VersionNo

                    .WorkExceptionTypeId = bWorkException.WorkExceptionTypeId
                    .WorkExceptionTypeName = bWorkException.WorkExceptionTypeName
                    .WorkExceptionTypeNameEN = bWorkException.WorkExceptionTypeNameEN
                End With
            End If

            Return eWorkExceptionType
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal WorkExceptionTypeName As String,
                            ByVal UserAccountId As Integer, Optional ByVal WorkExceptionTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkExceptionType As New DAWorkExceptionType
            If oWorkExceptionType.Add(WorkExceptionTypeName, UserAccountId, WorkExceptionTypeNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oWorkExceptionType.Identity
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal WorkExceptionTypeId As Integer,
        ByVal WorkExceptionTypeName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal WorkExceptionTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkExceptionType As New DAWorkExceptionType
            If oWorkExceptionType.Update(WorkExceptionTypeId, WorkExceptionTypeName, UserAccountId, VersionNo, WorkExceptionTypeNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkExceptionType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetWorkExceptionTypesDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkExceptionType As New DAWorkExceptionType
            ds = oWorkExceptionType.GetWorkExceptionTypesDataset(PageNo, PageSize)
            MyBase.PageTotal = oWorkExceptionType.PageTotal

            Return ds
        End Function
    

        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAWorkExceptionType().GetWorkExceptionTypesListDataset(Lang)
        End Function

#End Region

    End Class
#End Region

#Region " BOWorkExceptionEmployee "
    Public Class BOWorkExceptionEmployee
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
            Dim bWorkExceptionEmployee As New BOEWorkExceptionEmployee

            If Not IsNothing(Entity) Then
                Dim eWorkExceptionEmployee As WorkExceptionEmployee = CType(Entity, WorkExceptionEmployee)
                With bWorkExceptionEmployee
                    .WorkExceptionId = eWorkExceptionEmployee.WorkExceptionId
                    .EmployeeId = eWorkExceptionEmployee.EmployeeId
                End With
            End If

            Return bWorkExceptionEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eWorkExceptionEmployee As New WorkExceptionEmployee

            If Not IsNothing(Entity) Then
                Dim bWorkExceptionEmployee As BOEWorkExceptionEmployee = CType(Entity, BOEWorkExceptionEmployee)
                With eWorkExceptionEmployee
                    .WorkExceptionId = bWorkExceptionEmployee.WorkExceptionId
                    .EmployeeId = bWorkExceptionEmployee.EmployeeId
                End With
            End If

            Return eWorkExceptionEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee( _
        ByVal WorkExceptionId As Integer, _
        ByVal EmployeeId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkExceptionEmployee As New DAWorkExceptionEmployee
            If oWorkExceptionEmployee.AddEmployee(WorkExceptionId, EmployeeId, UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oWorkExceptionEmployee.Identity
                MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteEmployee(ByVal WorkExceptionId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oWorkExceptionEmployee As New DAWorkExceptionEmployee
            If oWorkExceptionEmployee.DeleteEmployee(WorkExceptionId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oWorkExceptionEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetWorkExceptionEmployeesDataset( _
        ByVal WorkExceptionId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oWorkExceptionEmployee As New DAWorkExceptionEmployee
            ds = oWorkExceptionEmployee.GetWorkExceptionEmployeesDataset(WorkExceptionId, PageNo, PageSize)
            MyBase.PageTotal = oWorkExceptionEmployee.PageTotal

            Return ds
        End Function
#End Region


    End Class
#End Region

#Region " BOEWorkExceptionEmployee "
    Public Class BOEWorkExceptionEmployee
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intWorkExceptionId As Integer
        Private intEmployeeId As Integer
#End Region
#Region " Public Properties  "
        Public Property WorkExceptionId() As Integer
            Get
                Return intWorkExceptionId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionId = value
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
                Try
                    MyBase.AddedDate = entity.AddedDate
                    MyBase.AddedUserAccountId = entity.AddedUserAccountId
                    MyBase.UpdatedDate = entity.UpdatedDate
                    MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
                    MyBase.VersionNo = entity.VersionNo
                    MyBase.AddedUserName = entity.AddedUserName
                    MyBase.UpdatedUserName = entity.UpdatedUserName
                Catch ex As Exception
                End Try

                Me.MapEntityToCustomProperties(entity)
            End If
        End Sub
        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
            Dim eWorkExceptionEmployee As WorkExceptionEmployee = CType(entity, WorkExceptionEmployee)
            With eWorkExceptionEmployee
                intWorkExceptionId = .WorkExceptionId
                intEmployeeId = .EmployeeId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOWorkExceptionEmployeesList "
    Public Class BOWorkExceptionEmployeesList
        Inherits BOListBase(Of BOEWorkExceptionEmployee)

        Public Sub Load(ByVal Id As Integer)
            LoadFromList(New DAWorkExceptionEmployee().LoadEmployees(Id))
        End Sub
        Private Sub LoadFromList(ByVal WorkExceptionEmployees As List(Of WorkExceptionEmployee))
            If WorkExceptionEmployees.Count > 0 Then
                For Each eWorkExceptionEmployee As WorkExceptionEmployee In WorkExceptionEmployees
                    Dim bWorkExceptionEmployee As New BOEWorkExceptionEmployee
                    bWorkExceptionEmployee.MapEntityToProperties(eWorkExceptionEmployee)
                    Me.Add(bWorkExceptionEmployee)
                Next
            End If
        End Sub
    End Class


#End Region

End Namespace


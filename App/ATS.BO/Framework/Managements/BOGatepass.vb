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

#Region " BOGatepass "
    Public Class BOGatepass
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass
            If oGatepass.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oGatepass As New DAGatepass
            Return MapEntityToProperties(oGatepass.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Public Function GetGatepasssByIdDataset(ByVal GatepassId As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepasssByIdDateDataset(GatepassId)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function GetGatepasssDataset( _
        ByVal UserAccountId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepasssDataset(UserAccountId, PageNo, PageSize)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function GetGatepasssByDateDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepasssByDateDataset(UserAccountId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function GetGatepasssByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepasssByEmployeeIdDataset(EmployeeId, PageNo, PageSize, Lang)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bGatepass As New BOEGatepass

            If Not IsNothing(Entity) Then
                Dim eGatepass As Gatepass = CType(Entity, Gatepass)
                With bGatepass
                    .AddedDate = eGatepass.AddedDate
                    .AddedUserAccountId = eGatepass.AddedUserAccountId
                    .UpdatedDate = eGatepass.UpdatedDate
                    .UpdatedUserAccountId = eGatepass.UpdatedUserAccountId
                    .VersionNo = eGatepass.VersionNo

                    .GatepassId = eGatepass.GatepassId
                    .EmployeeId = eGatepass.EmployeeId
                    .GatepassBegDate = eGatepass.GatepassBegDate
                    .GatepassEndDate = eGatepass.GatepassEndDate
                    .GatepassBegTime = eGatepass.GatepassBegTime
                    .GatepassEndTime = eGatepass.GatepassEndTime
                    .Notes = eGatepass.Notes
                    .IsApproved = eGatepass.IsApproved
                    .RequestedDate = eGatepass.RequestedDate
                    If eGatepass.IsApproved Then
                        .ApprovedDate = eGatepass.ApprovedDate
                    End If
                    .GatepassTypeId = eGatepass.GatepassTypeId
                    .ApprovedEmployeeId = eGatepass.ApprovedEmployeeId

                    .AddedUserName = eGatepass.AddedUserName
                    .UpdatedUserName = eGatepass.UpdatedUserName
                End With
            End If

            Return bGatepass
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eGatepass As New Gatepass

            If Not IsNothing(Entity) Then
                Dim bGatepass As BOEGatepass = CType(Entity, BOEGatepass)
                With eGatepass
                    .AddedDate = bGatepass.AddedDate
                    .AddedUserAccountId = bGatepass.AddedUserAccountId
                    .UpdatedDate = bGatepass.UpdatedDate
                    .UpdatedUserAccountId = bGatepass.UpdatedUserAccountId
                    .VersionNo = bGatepass.VersionNo

                    .GatepassId = bGatepass.GatepassId
                    .EmployeeId = bGatepass.EmployeeId
                    .GatepassBegDate = bGatepass.GatepassBegDate
                    .GatepassEndDate = bGatepass.GatepassEndDate
                    .GatepassBegTime = bGatepass.GatepassBegTime
                    .GatepassEndTime = bGatepass.GatepassEndTime
                    .Notes = bGatepass.Notes
                    .IsApproved = bGatepass.IsApproved
                    .RequestedDate = bGatepass.RequestedDate
                    If bGatepass.IsApproved Then
                        .ApprovedDate = bGatepass.ApprovedDate
                    End If
                    .GatepassTypeId = bGatepass.GatepassTypeId
                    .ApprovedEmployeeId = bGatepass.ApprovedEmployeeId
                End With
            End If

            Return eGatepass
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal EmployeeId As Integer,
        ByVal GatepassTypeId As Integer,
        ByVal GatepassBegDate As Date,
        ByVal GatepassEndDate As Date,
        ByVal GatepassBegTime As Date,
        ByVal GatepassEndTime As Date,
        ByVal ApprovedEmployeeId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass
            If oGatepass.Add(EmployeeId,
                             GatepassTypeId,
                             GatepassBegDate,
                             GatepassEndDate,
                             GatepassBegTime,
                             GatepassEndTime,
                             ApprovedEmployeeId,
                             Notes,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oGatepass.Identity
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function Update(
        ByVal GatepassId As Integer,
        ByVal EmployeeId As Integer,
        ByVal GatepassTypeId As Integer,
        ByVal GatepassBegDate As Date,
        ByVal GatepassEndDate As Date,
        ByVal GatepassBegTime As Date,
        ByVal GatepassEndTime As Date,
        ByVal ApprovedEmployeeId As Integer,
        ByVal Notes As String,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass
            If oGatepass.Update(GatepassId,
                                EmployeeId,
                                GatepassTypeId,
                                GatepassBegDate,
                                GatepassEndDate,
                                GatepassBegTime,
                                GatepassEndTime,
                                ApprovedEmployeeId,
                                Notes,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Approve( _
        ByVal GatepassId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass
            If oGatepass.Approve(GatepassId,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetEmployeeGatepasssDateDataset( _
        ByVal EmployeeId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetEmployeeGatepasssDateDataset(EmployeeId, BegDate, EndDate)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function
        Public Function GetDepartmentGatepassDateDataset( _
        ByVal DepartmentId As Integer, _
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal OptionNo As Integer = 1,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetDepartmentGatepassDateDataset(DepartmentId, BegDate, EndDate, OptionNo, Lang)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function
        Public Function GetGatepassTypesList() As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepassTypesList()
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function


        Public Function GetGatepassByEmployeeManagerForApprovalDataset( _
ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepassByEmployeeManagerForApprovalDataset(ManagerID)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function GetGatepassByEmployeeManagerForApprovalDataset1(ByVal ManagerID As Integer, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepassByEmployeeManagerForApprovalDataset1(ManagerID, Lang)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function GetGatepassBySpecificRoleForApprovalDataset(ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepassBySpecificRoleForApprovalDataset(ManagerID)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function

        Public Function AddGatePassApproval(ByVal GatepassId As Integer,
                                            ByVal ApprovalPolicyId As Integer,
                                            ByVal ApprovalPathId As Integer,
                                            ByVal IsApproved As Boolean,
                                            ByVal IsRejected As Boolean,
                                            ByVal Comments As String,
                                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass

            If oGatepass.AddGatePassApproval(GatepassId,
                           ApprovalPolicyId,
                           ApprovalPathId,
                           IsApproved,
                           IsRejected,
                           Comments,
                           UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oGatepass.Identity
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed

        End Function

        Public Function UpdateGatepassApprovalStatus(ByVal GatepassId As Integer,
                                                     ByVal IsApproved As Boolean,
                                                     ByVal IsRejected As Boolean,
                                                     ByVal UserId As Integer,
                                                     ByVal Notes As String) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepass As New DAGatepass
            If oGatepass.UpdateGatepassApprovalStatus(GatepassId,
                                IsApproved,
                                IsRejected,
                                UserId,
                                Notes) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepass.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepass.InfoMessage
            End If

            Return boolSeccessed
        End Function


        Public Function GetGatepassApprovalLogByGatepassId(ByVal GatepassId As Integer) As System.Data.DataSet

            Dim ds As DataSet = Nothing

            Dim oGatepass As New DAGatepass
            ds = oGatepass.GetGatepassApprovalLogByGatepassId(GatepassId)
            MyBase.PageTotal = oGatepass.PageTotal

            Return ds
        End Function
#End Region

    End Class
#End Region

#Region " BOEGatepass "
    Public Class BOEGatepass
        Inherits BOEntityBase


#Region " Private Variables  "
        Private intGatepassId As Integer
        Private intEmployeeId As Integer
        Private intGatepassTypeId As Integer
        Private datGatepassBegDate As Date
        Private datGatepassEndDate As Date
        Private datGatepassBegTime As Date
        Private datGatepassEndTime As Date
        Private datRequestedDate As Date
        Private datApprovedDate As Date
        Private intApprovedEmployeeId As Integer
        Private strNotes As String
        Private bolIsApproved As Boolean
#End Region

#Region " Public Properties "
        Public Property GatepassId() As Integer
            Get
                Return intGatepassId
            End Get
            Set(ByVal value As Integer)
                intGatepassId = value
            End Set
        End Property
        Public Property GatepassTypeId() As Integer
            Get
                Return intGatepassTypeId
            End Get
            Set(ByVal value As Integer)
                intGatepassTypeId = value
            End Set
        End Property
        Public Property ApprovedEmployeeId() As Integer
            Get
                Return intApprovedEmployeeId
            End Get
            Set(ByVal value As Integer)
                intApprovedEmployeeId = value
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

        Public Property IsApproved() As Boolean
            Get
                Return bolIsApproved
            End Get
            Set(ByVal value As Boolean)
                bolIsApproved = value
            End Set
        End Property
        Public Property GatepassBegDate() As Date
            Get
                Return datGatepassBegDate
            End Get
            Set(ByVal value As Date)
                datGatepassBegDate = value
            End Set
        End Property
        Public Property GatepassEndDate() As Date
            Get
                Return datGatepassEndDate
            End Get
            Set(ByVal value As Date)
                datGatepassEndDate = value
            End Set
        End Property
        Public Property GatepassBegTime() As Date
            Get
                Return datGatepassBegTime
            End Get
            Set(ByVal value As Date)
                datGatepassBegTime = value
            End Set
        End Property
        Public Property GatepassEndTime() As Date
            Get
                Return datGatepassEndTime
            End Get
            Set(ByVal value As Date)
                datGatepassEndTime = value
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
        Public Property RequestedDate() As Date
            Get
                Return datRequestedDate
            End Get
            Set(ByVal value As Date)
                datRequestedDate = value
            End Set
        End Property
        Public Property ApprovedDate() As Date
            Get
                Return datApprovedDate
            End Get
            Set(ByVal value As Date)
                datApprovedDate = value
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
            Dim eGatepass As Gatepass = CType(entity, Gatepass)
            With eGatepass
                intGatepassId = .GatepassId
                intEmployeeId = .EmployeeId
                datGatepassBegDate = .GatepassBegDate
                datGatepassEndDate = .GatepassEndDate
                datGatepassBegTime = .GatepassBegTime
                datGatepassEndTime = .GatepassEndTime
                intGatepassTypeId = .GatepassTypeId
                intApprovedEmployeeId = .ApprovedEmployeeId
                strNotes = .Notes
                bolIsApproved = .IsApproved
                datRequestedDate = .RequestedDate
                If bolIsApproved Then
                    datApprovedDate = .ApprovedDate
                End If
            End With
        End Sub
#End Region

    End Class
#End Region


#Region " BOEGatepassType "
    Public Class BOEGatepassType
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intGatepassTypeId As Integer
        Private strGatepassTypeName As String
        Private strGatepassTypeNameEN As String
#End Region
#Region " Public Properties "
        Public Property GatepassTypeId() As Integer
            Get
                Return intGatepassTypeId
            End Get
            Set(ByVal value As Integer)
                intGatepassTypeId = value
            End Set
        End Property

        Public Property GatepassTypeName() As String
            Get
                Return strGatepassTypeName
            End Get
            Set(ByVal value As String)
                strGatepassTypeName = value
            End Set
        End Property

        Public Property GatepassTypeNameEN() As String
            Get
                Return strGatepassTypeNameEN
            End Get
            Set(ByVal value As String)
                strGatepassTypeNameEN = value
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
            Dim eGatepassType As GatepassType = CType(entity, GatepassType)
            With eGatepassType
                intGatepassTypeId = .GatepassTypeId
                strGatepassTypeName = .GatepassTypeName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOGatepassType "
    Public Class BOGatepassType
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepassType As New DAGatepassType
            If oGatepassType.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepassType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepassType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oGatepassType As New DAGatepassType
            Return MapEntityToProperties(oGatepassType.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bGatepassType As New BOEGatepassType

            If Not IsNothing(Entity) Then
                Dim eGatepassType As GatepassType = CType(Entity, GatepassType)
                With bGatepassType
                    .AddedDate = eGatepassType.AddedDate
                    .AddedUserAccountId = eGatepassType.AddedUserAccountId
                    .UpdatedDate = eGatepassType.UpdatedDate
                    .UpdatedUserAccountId = eGatepassType.UpdatedUserAccountId
                    .VersionNo = eGatepassType.VersionNo

                    .GatepassTypeId = eGatepassType.GatepassTypeId
                    .GatepassTypeName = eGatepassType.GatepassTypeName
                    .GatepassTypeNameEN = eGatepassType.GatepassTypeNameEN

                    .AddedUserName = eGatepassType.AddedUserName
                    .UpdatedUserName = eGatepassType.UpdatedUserName
                End With
            End If

            Return bGatepassType
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eGatepassType As New GatepassType

            If Not IsNothing(Entity) Then
                Dim bGatepass As BOEGatepassType = CType(Entity, BOEGatepassType)
                With eGatepassType
                    .AddedDate = bGatepass.AddedDate
                    .AddedUserAccountId = bGatepass.AddedUserAccountId
                    .UpdatedDate = bGatepass.UpdatedDate
                    .UpdatedUserAccountId = bGatepass.UpdatedUserAccountId
                    .VersionNo = bGatepass.VersionNo

                    .GatepassTypeId = bGatepass.GatepassTypeId
                    .GatepassTypeName = bGatepass.GatepassTypeName
                    .GatepassTypeNameEN = bGatepass.GatepassTypeNameEN
                End With
            End If

            Return eGatepassType
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal GatepassTypeName As String,
                            ByVal UserAccountId As Integer, Optional ByVal GatepassTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepassType As New DAGatepassType
            If oGatepassType.Add(GatepassTypeName, UserAccountId, GatepassTypeNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oGatepassType.Identity
                MyBase.InfoMessage = oGatepassType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepassType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal GatepassTypeId As Integer,
        ByVal GatepassTypeName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal GatepassTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oGatepassType As New DAGatepassType
            If oGatepassType.Update(GatepassTypeId, GatepassTypeName, UserAccountId, VersionNo, GatepassTypeNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oGatepassType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oGatepassType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetGatepassTypesDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oGatepassType As New DAGatepassType
            ds = oGatepassType.GetGatepassTypesDataset(PageNo, PageSize)
            MyBase.PageTotal = oGatepassType.PageTotal

            Return ds
        End Function

        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAGatepassType().GetGatepassTypesListDataset(Lang)
        End Function

        Public Function GetAllGatepassTypes() As DataSet
            Return New DAGatepassType().GetGatepassTypesDataset(1, 1000)
        End Function
#End Region

    End Class
#End Region
End Namespace


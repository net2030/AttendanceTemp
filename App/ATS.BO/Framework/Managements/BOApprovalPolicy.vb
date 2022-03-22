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


#Region " BOApprovalPolicy "
    Public Class BOApprovalPolicy
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Dim oApprovalPolicy As New DAApprovalPolicy
            If oApprovalPolicy.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            End If
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oApprovalPolicy As New DAApprovalPolicy
            Return MapEntityToProperties(oApprovalPolicy.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Public Function GetApprovalPolicysDataset(Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oApprovalPolicy As New DAApprovalPolicy
            ds = oApprovalPolicy.GetApprovalPolicysDataset(PageNo, PageSize)
            MyBase.PageTotal = oApprovalPolicy.PageTotal
            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bApprovalPolicy As New BOEApprovalPolicy

            If Not IsNothing(Entity) Then
                Dim eApprovalPolicy As ApprovalPolicy = CType(Entity, ApprovalPolicy)
                With bApprovalPolicy
                    .AddedDate = eApprovalPolicy.AddedDate
                    .AddedUserAccountId = eApprovalPolicy.AddedUserAccountId
                    .UpdatedDate = eApprovalPolicy.UpdatedDate
                    .UpdatedUserAccountId = eApprovalPolicy.UpdatedUserAccountId
                    .VersionNo = eApprovalPolicy.VersionNo

                    .ApprovalPolicyId = eApprovalPolicy.ApprovalPolicyId
                    .ApprovalPolicyName = eApprovalPolicy.ApprovalPolicyName
                    .TimeOffApprovalType = eApprovalPolicy.TimeOffApprovalType

                    .AddedUserName = eApprovalPolicy.AddedUserName
                    .UpdatedUserName = eApprovalPolicy.UpdatedUserName
                End With
            End If

            Return bApprovalPolicy
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eApprovalPolicy As New ApprovalPolicy

            If Not IsNothing(Entity) Then
                Dim bApprovalPolicy As BOEApprovalPolicy = CType(Entity, BOEApprovalPolicy)
                With eApprovalPolicy
                    .AddedDate = bApprovalPolicy.AddedDate
                    .AddedUserAccountId = bApprovalPolicy.AddedUserAccountId
                    .UpdatedDate = bApprovalPolicy.UpdatedDate
                    .UpdatedUserAccountId = bApprovalPolicy.UpdatedUserAccountId
                    .VersionNo = bApprovalPolicy.VersionNo

                    .ApprovalPolicyId = bApprovalPolicy.ApprovalPolicyId
                    .ApprovalPolicyName = bApprovalPolicy.ApprovalPolicyName
                    .TimeOffApprovalType = bApprovalPolicy.TimeOffApprovalType


                End With
            End If

            Return eApprovalPolicy
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal ApprovalPolicyName As String,
        ByVal TimeOffApprovalType As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPolicy As New DAApprovalPolicy
            If oApprovalPolicy.Add(ApprovalPolicyName,
                             TimeOffApprovalType,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oApprovalPolicy.Identity
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal ApprovalPolicyId As Integer,
        ByVal ApprovalPolicyName As String,
        ByVal TimeOffApprovalType As Integer,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPolicy As New DAApprovalPolicy
            If oApprovalPolicy.Update(ApprovalPolicyId,
                                ApprovalPolicyName,
                                TimeOffApprovalType,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function GetApprovalPolicyEmployees(ByVal PolicyId As Integer, Optional ByVal PageNo As Integer = 1,
                                                 Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return New DAApprovalPolicy().GetApprovalPolicyEmployees(PolicyId, PageNo, PageSize)
        End Function

        Public Function GetApprovalBalanceByEmployeeID(ByVal EmployeeId As Integer, ByVal TimeOffApprovalType As Integer) As System.Data.DataSet
            Return New DAApprovalPolicy().GetApprovalBalanceByEmployeeID(EmployeeId, TimeOffApprovalType)
        End Function

        Public Function AddEmployee(
        ByVal ApprovalPolicyId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPolicyEmployee As New DAApprovalPolicy
            If oApprovalPolicyEmployee.AddEmployee(ApprovalPolicyId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPolicyEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPolicyEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function DeleteEmployee(
        ByVal ApprovalPolicyId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPolicyEmployee As New DAApprovalPolicy
            If oApprovalPolicyEmployee.DeleteEmployee(ApprovalPolicyId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPolicyEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPolicyEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region

#Region " BOEApprovalPolicy "
    Public Class BOEApprovalPolicy
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intApprovalPolicyId As Integer
        Private strApprovalPolicyName As String
        Private intTimeOffApprovalType As Integer
        Private datEffectiveDate As Date

#End Region

#Region " Public Properties "
        Public Property ApprovalPolicyId() As Integer
            Get
                Return intApprovalPolicyId
            End Get
            Set(ByVal value As Integer)
                intApprovalPolicyId = value
            End Set
        End Property
        Public Property ApprovalPolicyName() As String
            Get
                Return strApprovalPolicyName
            End Get
            Set(ByVal value As String)
                strApprovalPolicyName = value
            End Set
        End Property
        Public Property TimeOffApprovalType() As Integer
            Get
                Return intTimeOffApprovalType
            End Get
            Set(ByVal value As Integer)
                intTimeOffApprovalType = value
            End Set
        End Property
        Public Property EffectiveDate() As Date
            Get
                Return datEffectiveDate
            End Get
            Set(ByVal value As Date)
                datEffectiveDate = value
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
            Dim eApprovalPolicy As ApprovalPolicy = CType(entity, ApprovalPolicy)
            With eApprovalPolicy
                intApprovalPolicyId = .ApprovalPolicyId
                strApprovalPolicyName = .ApprovalPolicyName
                intTimeOffApprovalType = .TimeOffApprovalType

            End With
        End Sub
#End Region

    End Class
#End Region


#Region " BOEApproverType "
    Public Class BOEApproverType
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intApproverTypeId As Integer
        Private strApproverTypeName As String
#End Region
#Region " Public Properties "
        Public Property ApproverTypeId() As Integer
            Get
                Return intApproverTypeId
            End Get
            Set(ByVal value As Integer)
                intApproverTypeId = value
            End Set
        End Property

        Public Property ApproverTypeName() As String
            Get
                Return strApproverTypeName
            End Get
            Set(ByVal value As String)
                strApproverTypeName = value
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
            Dim eApproverType As ApproverType = CType(entity, ApproverType)
            With eApproverType
                intApproverTypeId = .ApproverTypeId
                strApproverTypeName = .ApproverTypeName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOApproverType "
    Public Class BOApproverType
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApproverType As New DAApproverType
            If oApproverType.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApproverType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApproverType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oApproverType As New DAApproverType
            Return MapEntityToProperties(oApproverType.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bApproverType As New BOEApproverType

            If Not IsNothing(Entity) Then
                Dim eApproverType As ApproverType = CType(Entity, ApproverType)
                With bApproverType
                    .AddedDate = eApproverType.AddedDate
                    .AddedUserAccountId = eApproverType.AddedUserAccountId
                    .UpdatedDate = eApproverType.UpdatedDate
                    .UpdatedUserAccountId = eApproverType.UpdatedUserAccountId
                    .VersionNo = eApproverType.VersionNo

                    .ApproverTypeId = eApproverType.ApproverTypeId
                    .ApproverTypeName = eApproverType.ApproverTypeName

                    .AddedUserName = eApproverType.AddedUserName
                    .UpdatedUserName = eApproverType.UpdatedUserName
                End With
            End If

            Return bApproverType
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eApproverType As New ApproverType

            If Not IsNothing(Entity) Then
                Dim bApprover As BOEApproverType = CType(Entity, BOEApproverType)
                With eApproverType
                    .AddedDate = bApprover.AddedDate
                    .AddedUserAccountId = bApprover.AddedUserAccountId
                    .UpdatedDate = bApprover.UpdatedDate
                    .UpdatedUserAccountId = bApprover.UpdatedUserAccountId
                    .VersionNo = bApprover.VersionNo

                    .ApproverTypeId = bApprover.ApproverTypeId
                    .ApproverTypeName = bApprover.ApproverTypeName
                End With
            End If

            Return eApproverType
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal ApproverTypeName As String, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApproverType As New DAApproverType
            If oApproverType.Add(ApproverTypeName, UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oApproverType.Identity
                MyBase.InfoMessage = oApproverType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApproverType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal ApproverTypeId As Integer,
        ByVal ApproverTypeName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApproverType As New DAApproverType
            If oApproverType.Update(ApproverTypeId, ApproverTypeName, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApproverType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApproverType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetApproverTypesDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oApproverType As New DAApproverType
            ds = oApproverType.GetApproverTypesDataset(PageNo, PageSize)
            MyBase.PageTotal = oApproverType.PageTotal

            Return ds
        End Function
        Public Function GetList() As DataSet
            Return New DAApproverType().GetApproverTypesListDataset
        End Function
#End Region

    End Class
#End Region


#Region " BOEApprovalPath"
    Public Class BOEApprovalPath
        Inherits BOEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local variables  "
        Private intApprovalPathId As Integer
        Private intApprovalPolicyId As Integer
        Private intApproverTypeId As Integer
        Private intEmployeeId As Integer
        Private intApprovalPathSequence As Integer
#End Region

#Region " Properties To Match the Table Definition  "
        Public Property ApprovalPathId() As Integer
            Get
                Return intApprovalPathId
            End Get
            Set(ByVal value As Integer)
                intApprovalPathId = value
            End Set
        End Property
        Public Property ApprovalPolicyId() As Integer
            Get
                Return intApprovalPolicyId
            End Get
            Set(ByVal value As Integer)
                intApprovalPolicyId = value
            End Set
        End Property
        Public Property ApproverTypeId() As Integer
            Get
                Return intApproverTypeId
            End Get
            Set(ByVal value As Integer)
                intApproverTypeId = value
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
        Public Property ApprovalPathSequence() As Integer
            Get
                Return intApprovalPathSequence
            End Get
            Set(ByVal value As Integer)
                intApprovalPathSequence = value
            End Set
        End Property
#End Region

    End Class
#End Region

#Region " BOApprovalPath "
    Public Class BOApprovalPath
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Dim oApprovalPath As New DAApprovalPath
            If oApprovalPath.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            End If
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oApprovalPath As New DAApprovalPath
            Return MapEntityToProperties(oApprovalPath.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bApprovalPath As New BOEApprovalPath

            If Not IsNothing(Entity) Then
                Dim eApprovalPath As ApprovalPath = CType(Entity, ApprovalPath)
                With bApprovalPath
                    .AddedDate = eApprovalPath.AddedDate
                    .AddedUserAccountId = eApprovalPath.AddedUserAccountId
                    .UpdatedDate = eApprovalPath.UpdatedDate
                    .UpdatedUserAccountId = eApprovalPath.UpdatedUserAccountId
                    .VersionNo = eApprovalPath.VersionNo

                    .ApprovalPathId = eApprovalPath.ApprovalPathId
                    .ApprovalPolicyId = eApprovalPath.ApprovalPolicyId
                    .ApproverTypeId = eApprovalPath.ApproverTypeId
                    .EmployeeId = eApprovalPath.EmployeeId
                    .ApprovalPathSequence = eApprovalPath.ApprovalPathSequence
                   
                    .AddedUserName = eApprovalPath.AddedUserName
                    .UpdatedUserName = eApprovalPath.UpdatedUserName
                End With
            End If

            Return bApprovalPath
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eApprovalPath As New ApprovalPath

            If Not IsNothing(Entity) Then
                Dim bApprovalPath As BOEApprovalPath = CType(Entity, BOEApprovalPath)
                With eApprovalPath
                    .AddedDate = bApprovalPath.AddedDate
                    .AddedUserAccountId = bApprovalPath.AddedUserAccountId
                    .UpdatedDate = bApprovalPath.UpdatedDate
                    .UpdatedUserAccountId = bApprovalPath.UpdatedUserAccountId
                    .VersionNo = bApprovalPath.VersionNo

                    .ApprovalPathId = bApprovalPath.ApprovalPathId
                    .ApprovalPolicyId = bApprovalPath.ApprovalPolicyId
                    .ApproverTypeId = bApprovalPath.ApproverTypeId
                    .EmployeeId = bApprovalPath.EmployeeId
                    .ApprovalPathSequence = bApprovalPath.ApprovalPathSequence
                End With
            End If

            Return eApprovalPath
        End Function
#End Region

#Region " Public Methods "

        Public Function Add(ByVal ApprovalPolicyId As Integer,
                            ByVal ApproverTypeId As Integer,
                            ByVal EmployeeId As Integer,
                            ByVal ApprovalPathSequence As Integer,
                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPath As New DAApprovalPath
            If oApprovalPath.Add(ApprovalPolicyId,
                             ApproverTypeId,
                             EmployeeId,
                             ApprovalPathSequence,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oApprovalPath.Identity
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function Update(ByVal ApprovalPathId As Integer,
                            ByVal ApprovalPolicyId As Integer,
                            ByVal ApproverTypeId As Integer,
                            ByVal EmployeeId As Integer,
                            ByVal ApprovalPathSequence As Integer,
                            ByVal UserAccountId As Integer,
                            ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oApprovalPath As New DAApprovalPath
            If oApprovalPath.Update(ApprovalPathId,
                                 ApprovalPolicyId,
                                 ApproverTypeId,
                                 EmployeeId,
                                 ApprovalPathSequence,
                                 UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oApprovalPath.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function GetApprovalPathsByPolicyId(ByVal TimeOffPolicyId As Integer) As System.Data.DataSet
            Return New DAApprovalPath().GetApprovalPathsByPolicyId(TimeOffPolicyId)
        End Function

#End Region

    End Class
#End Region

End Namespace


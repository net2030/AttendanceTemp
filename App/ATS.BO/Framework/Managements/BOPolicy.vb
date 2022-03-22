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

#Region " BOEPolicy "
    Public Class BOEPolicy
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intPolicyId As Integer
        Private strPolicyName As String
        Private strPolicyNameEN As String
        Private intMarkObsentDuration As Integer
        Private intLateInMinutes As Integer
        Private intLateOutMinutes As Integer
        Private intEarlyInMinutes As Integer
        Private intEarlyOutMinutes As Integer
        Private intLateLimitPerMonthMinutes As Integer
        Private intDepartmentId As Integer
#End Region
#Region " Public Properties "
        Public Property PolicyId() As Integer
            Get
                Return intPolicyId
            End Get
            Set(ByVal value As Integer)
                intPolicyId = value
            End Set
        End Property
        Public Property PolicyName() As String
            Get
                Return strPolicyName
            End Get
            Set(ByVal value As String)
                strPolicyName = value
            End Set
        End Property

        Public Property PolicyNameEN() As String
            Get
                Return strPolicyNameEN
            End Get
            Set(ByVal value As String)
                strPolicyNameEN = value
            End Set
        End Property

        Public Property MarkObsentDuration() As Integer
            Get
                Return intMarkObsentDuration
            End Get
            Set(ByVal value As Integer)
                intMarkObsentDuration = value
            End Set
        End Property
        Public Property LateInMinutes() As Integer
            Get
                Return intLateInMinutes
            End Get
            Set(ByVal value As Integer)
                intLateInMinutes = value
            End Set
        End Property
        Public Property LateOutMinutes() As Integer
            Get
                Return intLateOutMinutes
            End Get
            Set(ByVal value As Integer)
                intLateOutMinutes = value
            End Set
        End Property
        Public Property EarlyInMinutes() As Integer
            Get
                Return intEarlyInMinutes
            End Get
            Set(ByVal value As Integer)
                intEarlyInMinutes = value
            End Set
        End Property
        Public Property EarlyOutMinutes() As Integer
            Get
                Return intEarlyOutMinutes
            End Get
            Set(ByVal value As Integer)
                intEarlyOutMinutes = value
            End Set
        End Property
        Public Property LateLimitPerMonthMinutes() As Integer
            Get
                Return intLateLimitPerMonthMinutes
            End Get
            Set(ByVal value As Integer)
                intLateLimitPerMonthMinutes = value
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
            Dim ePolicy As Policy = CType(entity, Policy)
            With ePolicy
                intPolicyId = .PolicyId
                strPolicyName = .PolicyName
                strPolicyNameEN = .PolicyNameEN
                intMarkObsentDuration = .MarkObsentDuration
                intLateInMinutes = .LateInMinutes
                intLateOutMinutes = .LateOutMinutes
                intEarlyInMinutes = .EarlyInMinutes
                intEarlyOutMinutes = .EarlyOutMinutes
                intLateLimitPerMonthMinutes = .LateLimitPerMonthMinutes
                intDepartmentId = .DepartmentId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOPolicy "
    Public Class BOPolicy
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPolicy As New DAPolicy
            If oPolicy.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oPolicy As New DAPolicy
            Return MapEntityToProperties(oPolicy.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bPolicy As New BOEPolicy

            If Not IsNothing(Entity) Then
                Dim ePolicy As Policy = CType(Entity, Policy)
                With bPolicy
                    .AddedDate = ePolicy.AddedDate
                    .AddedUserAccountId = ePolicy.AddedUserAccountId
                    .UpdatedDate = ePolicy.UpdatedDate
                    .UpdatedUserAccountId = ePolicy.UpdatedUserAccountId
                    .VersionNo = ePolicy.VersionNo

                    .PolicyId = ePolicy.PolicyId
                    .PolicyName = ePolicy.PolicyName
                    .PolicyNameEN = ePolicy.PolicyNameEN
                    .MarkObsentDuration = ePolicy.MarkObsentDuration
                    .LateInMinutes = ePolicy.LateInMinutes
                    .LateOutMinutes = ePolicy.LateOutMinutes
                    .EarlyInMinutes = ePolicy.EarlyInMinutes
                    .EarlyOutMinutes = ePolicy.EarlyOutMinutes
                    .LateLimitPerMonthMinutes = ePolicy.LateLimitPerMonthMinutes
                    .DepartmentId = ePolicy.DepartmentId

                    .AddedUserName = ePolicy.AddedUserName
                    .UpdatedUserName = ePolicy.UpdatedUserName
                End With
            End If

            Return bPolicy
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim ePolicy As New Policy

            If Not IsNothing(Entity) Then
                Dim bPolicy As BOEPolicy = CType(Entity, BOEPolicy)
                With ePolicy
                    .AddedDate = bPolicy.AddedDate
                    .AddedUserAccountId = bPolicy.AddedUserAccountId
                    .UpdatedDate = bPolicy.UpdatedDate
                    .UpdatedUserAccountId = bPolicy.UpdatedUserAccountId
                    .VersionNo = bPolicy.VersionNo

                    .PolicyId = bPolicy.PolicyId
                    .PolicyName = bPolicy.PolicyName
                    .MarkObsentDuration = bPolicy.MarkObsentDuration
                    .LateInMinutes = bPolicy.LateInMinutes
                    .LateOutMinutes = bPolicy.LateOutMinutes
                    .EarlyInMinutes = bPolicy.EarlyInMinutes
                    .EarlyOutMinutes = bPolicy.EarlyOutMinutes
                    .LateLimitPerMonthMinutes = bPolicy.LateLimitPerMonthMinutes
                    .DepartmentId = bPolicy.DepartmentId
                End With
            End If

            Return ePolicy
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal PolicyName As String,
        ByVal PolicyNameEN As String,
        ByVal MarkObsentDuration As Integer,
        ByVal LateInMinutes As Integer,
        ByVal LateOutMinutes As Integer,
        ByVal EarlyInMinutes As Integer,
        ByVal EarlyOutMinutes As Integer,
        ByVal LateLimitPerMonthMinutes As Integer,
        ByVal DepartmentId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPolicy As New DAPolicy
            If oPolicy.Add(PolicyName,
                           MarkObsentDuration,
                           LateInMinutes,
                           LateOutMinutes,
                           EarlyInMinutes,
                           EarlyOutMinutes,
                           LateLimitPerMonthMinutes,
                           DepartmentId,
                           UserAccountId, PolicyNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oPolicy.Identity
                MyBase.InfoMessage = oPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal PolicyId As Integer, _
        ByVal PolicyName As String,
        ByVal PolicyNameEN As String,
        ByVal MarkObsentDuration As Integer,
        ByVal LateInMinutes As Integer,
        ByVal LateOutMinutes As Integer,
        ByVal EarlyInMinutes As Integer,
        ByVal EarlyOutMinutes As Integer,
        ByVal LateLimitPerMonthMinutes As Integer,
        ByVal DepartmentId As Integer,
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPolicy As New DAPolicy
            If oPolicy.Update(PolicyId,
                              PolicyName,
                              MarkObsentDuration,
                              LateInMinutes,
                              LateOutMinutes,
                              EarlyInMinutes,
                              EarlyOutMinutes,
                              LateLimitPerMonthMinutes,
                              DepartmentId,
                              UserAccountId,
                              VersionNo, PolicyNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetAllPoliciesDataset(Optional ByVal PageNo As Integer = 1, _
                                             Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oPolicy As New DAPolicy
            ds = oPolicy.GetAllPoliciesDataset(PageNo, PageSize)
            MyBase.PageTotal = oPolicy.PageTotal

            Return ds
        End Function

        Public Function GetPoliciesDataset( _
ByVal DepartmentId As Integer, _
Optional ByVal PageNo As Integer = 1, _
Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oPolicy As New DAPolicy
            ds = oPolicy.GetPoliciesDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oPolicy.PageTotal

            Return ds
        End Function

        Public Function GetPolicyWorkSchedulesDataset( _
        ByVal PolicyId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oPolicy As New DAPolicy
            ds = oPolicy.GetPolicyWorkSchedulesDataset(PolicyId, PageNo, PageSize)
            MyBase.PageTotal = oPolicy.PageTotal

            Return ds
        End Function
        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAPolicy().GetList(Lang)
        End Function


#End Region

    End Class
#End Region

End Namespace


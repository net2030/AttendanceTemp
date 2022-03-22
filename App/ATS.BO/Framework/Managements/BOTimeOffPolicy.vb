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

#Region " BOTimeOffPolicy "
    Public Class BOTimeOffPolicy
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Dim oTimeOffPolicy As New DATimeOffPolicy
            If oTimeOffPolicy.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            End If
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oTimeOffPolicy As New DATimeOffPolicy
            Return MapEntityToProperties(oTimeOffPolicy.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Public Function GetTimeOffPolicysDataset(Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oTimeOffPolicy As New DATimeOffPolicy
            ds = oTimeOffPolicy.GetTimeOffPolicysDataset(PageNo, PageSize)
            MyBase.PageTotal = oTimeOffPolicy.PageTotal
            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bTimeOffPolicy As New BOETimeOffPolicy

            If Not IsNothing(Entity) Then
                Dim eTimeOffPolicy As TimeOffPolicy = CType(Entity, TimeOffPolicy)
                With bTimeOffPolicy
                    .AddedDate = eTimeOffPolicy.AddedDate
                    .AddedUserAccountId = eTimeOffPolicy.AddedUserAccountId
                    .UpdatedDate = eTimeOffPolicy.UpdatedDate
                    .UpdatedUserAccountId = eTimeOffPolicy.UpdatedUserAccountId
                    .VersionNo = eTimeOffPolicy.VersionNo

                    .TimeOffPolicyId = eTimeOffPolicy.TimeOffPolicyId
                    .TimeOffPolicyName = eTimeOffPolicy.TimeOffPolicyName
                    .TypeId = eTimeOffPolicy.TypeId
                    .EffectiveDate = eTimeOffPolicy.EffectiveDate
                    

                    .AddedUserName = eTimeOffPolicy.AddedUserName
                    .UpdatedUserName = eTimeOffPolicy.UpdatedUserName
                End With
            End If

            Return bTimeOffPolicy
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eTimeOffPolicy As New TimeOffPolicy

            If Not IsNothing(Entity) Then
                Dim bTimeOffPolicy As BOETimeOffPolicy = CType(Entity, BOETimeOffPolicy)
                With eTimeOffPolicy
                    .AddedDate = bTimeOffPolicy.AddedDate
                    .AddedUserAccountId = bTimeOffPolicy.AddedUserAccountId
                    .UpdatedDate = bTimeOffPolicy.UpdatedDate
                    .UpdatedUserAccountId = bTimeOffPolicy.UpdatedUserAccountId
                    .VersionNo = bTimeOffPolicy.VersionNo

                    .TimeOffPolicyId = bTimeOffPolicy.TimeOffPolicyId
                    .TimeOffPolicyName = bTimeOffPolicy.TimeOffPolicyName
                    .TypeId = bTimeOffPolicy.TypeId
                    .EffectiveDate = bTimeOffPolicy.EffectiveDate
                   
                End With
            End If

            Return eTimeOffPolicy
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal TimeOffPolicyName As String,
        ByVal TypeId As Integer,
        ByVal EffectiveDate As Date,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicy As New DATimeOffPolicy
            If oTimeOffPolicy.Add(TimeOffPolicyName,
                             TypeId,
                             EffectiveDate,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oTimeOffPolicy.Identity
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal TimeOffPolicyId As Integer,
        ByVal TimeOffPolicyName As String,
        ByVal TypeId As Integer,
        ByVal EffectiveDate As Date,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicy As New DATimeOffPolicy
            If oTimeOffPolicy.Update(TimeOffPolicyId,
                                TimeOffPolicyName,
                                TypeId,
                                EffectiveDate,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicy.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function GetTimeOffPolicyEmployees(ByVal PolicyId As Integer, Optional ByVal PageNo As Integer = 1,
                                                 Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Return New DATimeOffPolicy().GetTimeOffPolicyEmployees(PolicyId, PageNo, PageSize)
        End Function

        Public Function GetTimeOffBalanceByEmployeeID(ByVal EmployeeId As Integer, ByVal TypeId As Integer, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Return New DATimeOffPolicy().GetTimeOffBalanceByEmployeeID(EmployeeId, TypeId, Lang)
        End Function

        Public Function AddEmployee(
        ByVal TimeOffPolicyId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicyEmployee As New DATimeOffPolicy
            If oTimeOffPolicyEmployee.AddEmployee(TimeOffPolicyId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicyEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function DeleteEmployee(
        ByVal TimeOffPolicyId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTimeOffPolicyEmployee As New DATimeOffPolicy
            If oTimeOffPolicyEmployee.DeleteEmployee(TimeOffPolicyId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTimeOffPolicyEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTimeOffPolicyEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region

#Region " BOETimeOffPolicy "
    Public Class BOETimeOffPolicy
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intTimeOffPolicyId As Integer
        Private strTimeOffPolicyName As String
        Private intTypeId As Integer
        Private datEffectiveDate As Date
  
#End Region

#Region " Public Properties "
        Public Property TimeOffPolicyId() As Integer
            Get
                Return intTimeOffPolicyId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyId = value
            End Set
        End Property
        Public Property TimeOffPolicyName() As String
            Get
                Return strTimeOffPolicyName
            End Get
            Set(ByVal value As String)
                strTimeOffPolicyName = value
            End Set
        End Property
        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
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
            Dim eTimeOffPolicy As TimeOffPolicy = CType(entity, TimeOffPolicy)
            With eTimeOffPolicy
                intTimeOffPolicyId = .TimeOffPolicyId
                strTimeOffPolicyName = .TimeOffPolicyName
                intTypeId = .TypeId
                datEffectiveDate = .EffectiveDate
            End With
        End Sub
#End Region

    End Class
#End Region



End Namespace


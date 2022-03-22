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

#Region " BORole "
    Public Class BORole1
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole1
            If oRole.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
                MyBase.FieldInError = oRole.FieldInError
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oRole As New DARole1
            Return MapEntityToProperties(oRole.Find(Id))
        End Function

        Public Function GetUserRolesDataset(ByVal UserAccountId As Integer) As System.Data.DataSet
            Return New DARole1().GetUserRolesDataset(UserAccountId)
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eRole As Role1 = DirectCast(MapPropertiesToEntity(BOEntity), Role1)
            Dim oRole As New DARole1
            ds = oRole.GetDataset(eRole, PageNo, PageSize)
            MyBase.PageTotal = oRole.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bRole As New BOERole1

            If Not IsNothing(Entity) Then
                Dim eRole As Role1 = CType(Entity, Role1)
                With bRole
                    .AddedDate = eRole.AddedDate
                    .AddedUserAccountId = eRole.AddedUserAccountId
                    .UpdatedDate = eRole.UpdatedDate
                    .UpdatedUserAccountId = eRole.UpdatedUserAccountId
                    .VersionNo = eRole.VersionNo

                    .RoleId = eRole.RoleId
                    .RoleName = eRole.RoleName
                    .LDAPRole = eRole.LDAPRole
                    .IsApprover = eRole.IsApprover
                    .IsMasterApprover = eRole.IsMasterApprover

                    .AddedUserName = eRole.AddedUserName
                    .UpdatedUserName = eRole.UpdatedUserName
                End With
            End If

            Return bRole
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eRole As New Role1

            If Not IsNothing(Entity) Then
                Dim bRole As BOERole1 = CType(Entity, BOERole1)
                With eRole
                    .AddedDate = bRole.AddedDate
                    .AddedUserAccountId = bRole.AddedUserAccountId
                    .UpdatedDate = bRole.UpdatedDate
                    .UpdatedUserAccountId = bRole.UpdatedUserAccountId
                    .VersionNo = bRole.VersionNo
                    .RoleId = bRole.RoleId
                    .RoleName = bRole.RoleName
                    .LDAPRole = bRole.LDAPRole
                    .IsApprover = bRole.IsApprover
                    .IsMasterApprover = bRole.IsMasterApprover
                    .SelectFilter = CType(bRole.SelectFilter, Role1.FilterSelect)
                    .WhereFilter = CType(bRole.WhereFilter, Role1.FilterWhere)
                    .OrderByFilter = CType(bRole.OrderByFilter, Role1.FilterOrderBy)
                End With
            End If

            Return eRole
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal RoleName As String,
                            ByVal LDAPRole As String,
                            ByVal IsApprover As Boolean,
                            ByVal IsMasterApprover As Boolean,
                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole1
            If oRole.Add(RoleName, LDAPRole, IsApprover, IsMasterApprover, UserAccountId) Then
                MyBase.Identity = oRole.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(ByVal RoleId As Integer,
                                ByVal RoleName As String,
                                ByVal LDAPRole As String,
                                ByVal IsApprover As Boolean,
                                ByVal IsMasterApprover As Boolean,
                                ByVal UserAccountId As Integer,
                                ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole1
            If oRole.Update(RoleId, RoleName, LDAPRole, IsApprover, IsMasterApprover, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function


        Public Function GetList() As DataSet
            Return New DARole1().GetList
        End Function

        Public Function GetApprovalRoles() As System.Data.DataSet
            Return New DARole1().GetApprovalRoles
        End Function

        Public Function GetDefaultPage(ByVal RoleId As Integer) As DataSet
            Return New DARole1().GetDefaultPage(RoleId)
        End Function

        Public Function SetDefaultPage(ByVal RoleId As Integer, ByVal PageId As Integer) As DataSet
            Return New DARole1().GetDefaultPage(RoleId)
        End Function

#End Region



    End Class
#End Region

#Region " BOERole "
    Public Class BOERole1
        Inherits BOEntityBase

#Region " Enumerations "

#Region " Filter Varibales "
        Private intSelectFilter As FilterSelect
        Private intWhereFilter As FilterWhere
        Private intOrderByFilter As FilterOrderBy
#End Region
#Region " Class Enumerations (will change for each class)  "
        Public Enum FilterSelect As Integer
            None = 0
            All = 1
        End Enum
        Public Enum FilterWhere As Integer
            None = 0
            PrimaryKey = 1
            UserAccountId = 2
        End Enum
        Public Enum FilterOrderBy As Integer
            None = 0
        End Enum
#End Region
#Region " Filter Properties  "
        Public Property SelectFilter() As FilterSelect
            Get
                Return intSelectFilter
            End Get
            Set(ByVal Value As FilterSelect)
                intSelectFilter = Value
            End Set
        End Property
        Public Property WhereFilter() As FilterWhere
            Get
                Return intWhereFilter
            End Get
            Set(ByVal Value As FilterWhere)
                intWhereFilter = Value
            End Set
        End Property
        Public Property OrderByFilter() As FilterOrderBy
            Get
                Return intOrderByFilter
            End Get
            Set(ByVal Value As FilterOrderBy)
                intOrderByFilter = Value
            End Set
        End Property
#End Region

#End Region

#Region " Constructors "
        Public Sub New()

        End Sub
#End Region

#Region " Local Variables  "
        Private intRoleId As Integer
        Private strRoleName As String
        Private strLDAPRole As String
        Private bolIsApprover As Boolean
        Private bolIsmasterApprover As Boolean
    
#End Region

#Region "  Properties "
        Public Property RoleId() As Integer
            Get
                Return intRoleId
            End Get
            Set(ByVal value As Integer)
                intRoleId = value
            End Set
        End Property
        Public Property RoleName() As String
            Get
                Return strRoleName
            End Get
            Set(ByVal value As String)
                strRoleName = value
            End Set
        End Property
        Public Property LDAPRole() As String
            Get
                Return strLDAPRole
            End Get
            Set(ByVal value As String)
                strLDAPRole = value
            End Set
        End Property

        Public Property IsApprover() As Boolean
            Get
                Return bolIsApprover
            End Get
            Set(ByVal value As Boolean)
                bolIsApprover = value
            End Set
        End Property

        Public Property IsMasterApprover() As Boolean
            Get
                Return bolIsmasterApprover
            End Get
            Set(ByVal value As Boolean)
                bolIsmasterApprover = value
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
            Dim eRole As Role1 = CType(entity, Role1)
            With eRole
                intRoleId = .RoleId
                strRoleName = .RoleName
                strLDAPRole = .LDAPRole
                bolIsApprover = .IsApprover
                bolIsmasterApprover = .IsMasterApprover
            End With
        End Sub
#End Region

#Region " Public "

#End Region

    End Class
#End Region



End Namespace


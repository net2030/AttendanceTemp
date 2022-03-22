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
    Public Class BORole
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole
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
            Dim oRole As New DARole
            Return MapEntityToProperties(oRole.Find(Id))
        End Function

        Public Function GetUserRolesDataset(ByVal UserAccountId As Integer) As System.Data.DataSet
            Return New DARole().GetUserRolesDataset(UserAccountId)
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eRole As Role = DirectCast(MapPropertiesToEntity(BOEntity), Role)
            Dim oRole As New DARole
            ds = oRole.GetDataset(eRole, PageNo, PageSize)
            MyBase.PageTotal = oRole.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bRole As New BOERole

            If Not IsNothing(Entity) Then
                Dim eRole As Role = CType(Entity, Role)
                With bRole
                    .AddedDate = eRole.AddedDate
                    .AddedUserAccountId = eRole.AddedUserAccountId
                    .UpdatedDate = eRole.UpdatedDate
                    .UpdatedUserAccountId = eRole.UpdatedUserAccountId
                    .VersionNo = eRole.VersionNo

                    .RoleId = eRole.RoleId
                    .RoleName = eRole.RoleName
                    .DepartmentId = eRole.DepartmentId

                    .AddedUserName = eRole.AddedUserName
                    .UpdatedUserName = eRole.UpdatedUserName
                End With
            End If

            Return bRole
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eRole As New Role

            If Not IsNothing(Entity) Then
                Dim bRole As BOERole = CType(Entity, BOERole)
                With eRole
                    .AddedDate = bRole.AddedDate
                    .AddedUserAccountId = bRole.AddedUserAccountId
                    .UpdatedDate = bRole.UpdatedDate
                    .UpdatedUserAccountId = bRole.UpdatedUserAccountId
                    .VersionNo = bRole.VersionNo
                    .RoleId = bRole.RoleId
                    .RoleName = bRole.RoleName
                    .DepartmentId = bRole.DepartmentId

                    .SelectFilter = CType(bRole.SelectFilter, Role.FilterSelect)
                    .WhereFilter = CType(bRole.WhereFilter, Role.FilterWhere)
                    .OrderByFilter = CType(bRole.OrderByFilter, Role.FilterOrderBy)
                End With
            End If

            Return eRole
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal RoleName As String, _
        ByVal DepartmentId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole
            If oRole.Add(RoleName, DepartmentId, UserAccountId) Then
                MyBase.Identity = oRole.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal RoleId As Integer, _
        ByVal RoleName As String, _
        ByVal DepartmentId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARole
            If oRole.Update(RoleId, RoleName, DepartmentId, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetCapabilities(ByVal RoleId As Integer) As DataSet
            Return New DARole().GetCapabilities(RoleId)
        End Function
        Public Function GetRoleCapabilities(ByVal RoleId As Integer) As DataSet
            Return New DARoleCapability().GetRoleCapabilitiesDataset(RoleId)
        End Function
        Public Function GetList() As DataSet
            Return New DARole().GetList
        End Function
        Public Function GetRoleByDepartmentIdDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oRole As New DARole
            ds = oRole.GetRoleByDepartmentIdDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oRole.PageTotal

            Return ds
        End Function
#End Region

#Region " Save "
        Public Function Save(ByVal eRole As BOERole, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim intIdentity As Integer = 0
            Dim intUserIdentity As Integer = 0

            If eRole.RoleUserAccounts.Count = 0 AndAlso eRole.DBAction = BOEntityBase.DBActionEnum.Add Then
                MyBase.InfoMessage = "يجب إضافة مستخدم واحد على الأقل للمجموعه"
                Return False
            End If

            Using TS As New TransactionScope

                Select Case eRole.DBAction
                    Case BOEntityBase.DBActionEnum.Add
                        If Not Me.Add(
                              eRole.RoleName,
                              eRole.DepartmentId,
                              UserAccountId) Then
                            Return False
                        Else
                            intIdentity = Me.Identity
                            If eRole.RoleUserAccounts.Count > 0 Then
                                For Each eUserAccount As BOERoleUserAccount In eRole.RoleUserAccounts
                                    Dim oRoleUserAccount As New BORoleUserAccount
                                    If Not oRoleUserAccount.Add(intIdentity, eUserAccount.UserAccountId, UserAccountId) Then
                                        MyBase.InfoMessage = oRoleUserAccount.InfoMessage
                                        Return False
                                    End If
                                Next
                            End If
                        End If

                    Case BOEntityBase.DBActionEnum.Update
                        If Not Me.Update(eRole.RoleId, eRole.RoleName, eRole.DepartmentId, UserAccountId, eRole.VersionNo) Then
                            Return False
                        Else
                            If eRole.RoleUserAccounts.Count > 0 Then
                                For Each eUserAccount As BOERoleUserAccount In eRole.RoleUserAccounts
                                    Select Case eUserAccount.DBAction
                                        Case BOEntityBase.DBActionEnum.Add
                                            Dim oRoleUserAccount As New BORoleUserAccount
                                            If Not oRoleUserAccount.Add(eRole.RoleId, eUserAccount.UserAccountId, UserAccountId) Then
                                                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
                                                Return False
                                            End If
                                        Case BOEntityBase.DBActionEnum.Delete
                                            Dim oRoleUserAccount As New BORoleUserAccount
                                            If Not oRoleUserAccount.Delete(eUserAccount.RoleUserAccountId) Then
                                                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
                                                Return False
                                            End If
                                    End Select
                                Next
                            End If
                        End If

                    Case BOEntityBase.DBActionEnum.Delete
                        If Not Me.Delete(eRole.RoleId) Then
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

#Region " BOERole "
    Public Class BOERole
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
            oRoleCapabilities = New BORoleCapabilities
            oRoleUserAccounts = New BORoleUserAccounts
        End Sub
#End Region

#Region " Local Variables  "
        Private intRoleId As Integer
        Private strRoleName As String
        Private intDepartmentId As Integer
        Private oRoleCapabilities As BORoleCapabilities
        Private oRoleUserAccounts As BORoleUserAccounts
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
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Public Property RoleCapabilities() As BORoleCapabilities
            Get
                Return oRoleCapabilities
            End Get
            Set(ByVal value As BORoleCapabilities)
                oRoleCapabilities = value
            End Set
        End Property
        Public Property RoleUserAccounts() As BORoleUserAccounts
            Get
                Return oRoleUserAccounts
            End Get
            Set(ByVal value As BORoleUserAccounts)
                oRoleUserAccounts = value
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
            Dim eRole As Role = CType(entity, Role)
            With eRole
                intRoleId = .RoleId
                strRoleName = .RoleName
                intDepartmentId = .DepartmentId
                oRoleCapabilities.Load(.RoleId)
                oRoleUserAccounts.Load(.RoleId)
            End With
        End Sub
#End Region

#Region " Public "

#End Region

    End Class
#End Region

#Region " BORoles "
    Public Class BORoles
        Inherits BOListBase(Of BOERole)

        Public Sub Load()
            LoadFromList(New DARole().[Load]())
        End Sub
        Public Sub Load(ByVal UserId As Integer)
            LoadFromList(New DARole().[LoadRoleByUserAccountId](UserId))
        End Sub
        Private Sub LoadFromList(ByVal Roles As List(Of Role))
            If Roles.Count > 0 Then
                For Each eRole As Role In Roles
                    Dim bRole As New BOERole
                    bRole.MapEntityToProperties(eRole)
                    Me.Add(bRole)
                Next
            End If
        End Sub
        Friend Function GetByRoleId(ByVal roleId As Integer) As BOERole
            Return Me.SingleOrDefault(Function(r) r.RoleId = roleId)
        End Function
        Friend Sub LoadByUserAccountId(ByVal UserAccountId As Integer)
            LoadFromList(New DARole().LoadByUserAccountId(UserAccountId))
        End Sub
    End Class

#End Region

End Namespace


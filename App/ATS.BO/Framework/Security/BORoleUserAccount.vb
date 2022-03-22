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

#Region " BORoleUserAccount "
    Public Class BORoleUserAccount
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRoleUserAccount As New DARoleUserAccount
            If oRoleUserAccount.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
                MyBase.FieldInError = oRoleUserAccount.FieldInError
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oRoleUserAccount As New DARoleUserAccount
            Return MapEntityToProperties(oRoleUserAccount.Find(Id))
        End Function

        Public Overrides Function GetDataset(
        ByVal BOEntity As BOEntityBase,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
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
        ByVal RoleId As Integer,
        ByVal UserAccountId As Integer,
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRoleUserAccount As New DARoleUserAccount
            If oRoleUserAccount.Add(RoleId, UserAccountId, AddedUserAccountId) Then
                MyBase.Identity = oRoleUserAccount.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRoleUserAccount.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetRoleUsersDataset(
        ByVal RoleId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oRoleUserAccount As New DARoleUserAccount
            ds = oRoleUserAccount.GetRoleUsersDataset(RoleId, PageNo, PageSize)
            MyBase.PageTotal = oRoleUserAccount.PageTotal
            Return ds
        End Function
        Public Function AddRoleUser(ByVal RoleId As Integer, ByVal UserAccountId As Integer, ByVal AccountId As Integer) As Boolean
            Return New DARoleUserAccount().Add(RoleId, UserAccountId, AccountId)
        End Function
        Public Function DeleteRoleUser(ByVal RoleUserAccountId As Integer) As Boolean
            Return New DARoleUserAccount().Delete(RoleUserAccountId)
        End Function
        Public Function DeleteALLRoleUser(ByVal RoleId As Integer) As Boolean
            Return New DARoleUserAccount().DeleteALL(RoleId)
        End Function
        Public Function AddALLUsersRole(ByVal RoleId As Integer, ByVal UserAccountId As Integer) As Boolean
            Return New DARoleUserAccount().AddAllUserAccountsToRole(RoleId, UserAccountId)
        End Function
#End Region

    End Class
#End Region

#Region " BOERoleUserAccount "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BOERoleUserAccount
        Inherits BOEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intRoleUserAccountId As Integer
        Private intRoleId As Integer
        Private intUserAccountId As Integer
        Private strUserName As String
#End Region
#Region "  Properties "
        Public Property RoleUserAccountId() As Integer
            Get
                Return intRoleUserAccountId
            End Get
            Set(ByVal value As Integer)
                intRoleUserAccountId = value
            End Set
        End Property
        Public Property RoleId() As Integer
            Get
                Return intRoleId
            End Get
            Set(ByVal value As Integer)
                intRoleId = value
            End Set
        End Property
        Public Property UserAccountId() As Integer
            Get
                Return intUserAccountId
            End Get
            Set(ByVal value As Integer)
                intUserAccountId = value
            End Set
        End Property
        Public Property UserName() As String
            Get
                Return strUserName
            End Get
            Set(ByVal value As String)
                strUserName = value
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
            Dim eRoleUserAccount As RoleUserAccount = CType(entity, RoleUserAccount)
            With eRoleUserAccount
                intRoleUserAccountId = .RoleUserAccountId
                intRoleId = .RoleId
                intUserAccountId = .UserAccountId
                strUserName = .UserName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BORoleUserAccounts "
    Public Class BORoleUserAccounts
        Inherits BOListBase(Of BOERoleUserAccount)

        Public Sub Load(ByVal Id As Integer)
            LoadFromList(New DARoleUserAccount().LoadRoleUsers(Id))
        End Sub

        Private Sub LoadFromList(ByVal RoleUserAccounts As List(Of RoleUserAccount))
            If RoleUserAccounts.Count > 0 Then
                For Each eRoleUserAccount As RoleUserAccount In RoleUserAccounts
                    Dim bRole As New BOERoleUserAccount
                    bRole.MapEntityToProperties(eRoleUserAccount)
                    Me.Add(bRole)
                Next
            End If
        End Sub

#Region " Public Methods "
        Public Function IsUserInRole(ByVal UserAccountId As Integer) As Boolean
            Return (GetByUserAccountId(UserAccountId) IsNot Nothing)
        End Function
        ''' <summary>
        ''' Return the object from this instance with the specified userAccountId
        ''' </summary>
        ''' <param name="userAccountId"></param>
        ''' <returns></returns>
        Public Function GetByUserAccountId(ByVal UserAccountId As Integer) As BOERoleUserAccount
            Return Me.SingleOrDefault(Function(u) u.UserAccountId = UserAccountId)
        End Function
#End Region

    End Class
#End Region

End Namespace


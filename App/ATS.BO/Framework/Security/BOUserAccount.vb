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

#Region " BOUserAccount "
    Public Class BOUserAccount
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oUserAccount As New DAUserAccount
            If oUserAccount.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oUserAccount.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oUserAccount.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oUserAccount As New DAUserAccount
            Return MapEntityToProperties(oUserAccount.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eUserAccount As UserAccount = DirectCast(MapPropertiesToEntity(BOEntity), UserAccount)
            Dim oUserAccount As New DAUserAccount
            ds = oUserAccount.GetDataset(eUserAccount, PageNo, PageSize)
            MyBase.PageTotal = oUserAccount.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bUserAccount As New BOEUserAccount

            If Not IsNothing(Entity) Then
                Dim eUserAccount As UserAccount = CType(Entity, UserAccount)
                With bUserAccount
                    .AddedDate = eUserAccount.AddedDate
                    .AddedUserAccountId = eUserAccount.AddedUserAccountId
                    .UpdatedDate = eUserAccount.UpdatedDate
                    .UpdatedUserAccountId = eUserAccount.UpdatedUserAccountId
                    .VersionNo = eUserAccount.VersionNo

                    .UserAccountId = eUserAccount.UserAccountId
                    .windowsAccountName = eUserAccount.windowsAccountName
                    .UserName = eUserAccount.UserName
                    .Email = eUserAccount.Email
                    .IsActive = eUserAccount.IsActive
                    .IsConnectionSucceeded = eUserAccount.IsConnectionSucceeded
                    .IsFound = eUserAccount.IsFound
                    .EmployeeId = eUserAccount.EmployeeId

                    .AddedUserName = eUserAccount.AddedUserName
                    .UpdatedUserName = eUserAccount.UpdatedUserName
                End With
            End If

            Return bUserAccount
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eUserAccount As New UserAccount

            If Not IsNothing(Entity) Then
                Dim bUserAccount As BOEUserAccount = CType(Entity, BOEUserAccount)
                With eUserAccount
                    .AddedDate = bUserAccount.AddedDate
                    .AddedUserAccountId = bUserAccount.AddedUserAccountId
                    .UpdatedDate = bUserAccount.UpdatedDate
                    .UpdatedUserAccountId = bUserAccount.UpdatedUserAccountId
                    .VersionNo = bUserAccount.VersionNo
                    .UserAccountId = bUserAccount.UserAccountId
                    .windowsAccountName = bUserAccount.windowsAccountName
                    .UserName = bUserAccount.UserName
                    .Email = bUserAccount.Email
                    .IsActive = bUserAccount.IsActive
                    .EmployeeId = bUserAccount.EmployeeId

                    .SelectFilter = CType(bUserAccount.SelectFilter, UserAccount.FilterSelect)
                    .WhereFilter = CType(bUserAccount.WhereFilter, UserAccount.FilterWhere)
                    .OrderByFilter = CType(bUserAccount.OrderByFilter, UserAccount.FilterOrderBy)
                End With
            End If

            Return eUserAccount
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal WindowsAccountName As String, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean, _
        ByVal EmployeeId As Integer, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oUserAccount As New DAUserAccount
            If oUserAccount.Add( _
                WindowsAccountName, _
                UserName, _
                Email, _
                IsActive, _
                EmployeeId, _
                AddedUserAccountId) Then

                MyBase.InfoMessage = oUserAccount.InfoMessage
                MyBase.Identity = oUserAccount.Identity
                boolSeccessed = True
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oUserAccount.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal UserAccountId As Integer, _
        ByVal WindowsAccountName As String, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal version As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oUserAccount As New DAUserAccount
            If oUserAccount.Update( _
                UserAccountId, _
                WindowsAccountName, _
                UserName, _
                Email, _
                IsActive, _
                UpdatedUserAccountId, _
                version) Then

                MyBase.InfoMessage = oUserAccount.InfoMessage
                boolSeccessed = True
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oUserAccount.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function FindByWindowsAccountName(ByVal WindowsAccountName As String) As BOEntityBase
            Dim oUserAccount As New DAUserAccount
            Return MapEntityToProperties(oUserAccount.FindByWindowsAccountName(WindowsAccountName))
        End Function
        Public Function GetUsersByDepartmentIdDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oUserAccount As New DAUserAccount
            ds = oUserAccount.GetUsersByDepartmentIdDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oUserAccount.PageTotal
            Return ds
        End Function
#End Region

    End Class
#End Region

#Region " BOEUserAccount "
    <Serializable()> _
    Public Class BOEUserAccount
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
            UserName = 2
            EMail = 3
            IsActive = 4
        End Enum
        Public Enum FilterOrderBy As Integer
            None = 0
            UserName = 1
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
            oRoles = New BORoles
        End Sub
#End Region

#Region " Local Variables  "
        Private intUserAccountId As Integer
        Private strWindowsAccountName As String
        Private strUserName As String
        Private strEmail As String
        Private bolIsActive As Boolean
        Private oRoles As BORoles
        Private intEmployeeId As Integer
        Private bolIsConnectionSucceeded As Boolean = False
#End Region

#Region "  Properties  "
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
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
        Public Property windowsAccountName() As String
            Get
                Return strWindowsAccountName
            End Get
            Set(ByVal value As String)
                strWindowsAccountName = value
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
        Public Property Email() As String
            Get
                Return strEmail
            End Get
            Set(ByVal value As String)
                strEmail = value
            End Set
        End Property
        Public Property IsActive() As Boolean
            Get
                Return bolIsActive
            End Get
            Set(ByVal value As Boolean)
                bolIsActive = value
            End Set
        End Property
        Public Property Roles() As BORoles
            Get
                Return oRoles
            End Get
            Set(ByVal value As BORoles)
                oRoles = value
            End Set
        End Property
        Public Property IsConnectionSucceeded() As Boolean
            Get
                Return bolIsConnectionSucceeded
            End Get
            Set(ByVal value As Boolean)
                bolIsConnectionSucceeded = value
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
            Dim eUserAccount As UserAccount = CType(entity, UserAccount)
            With eUserAccount
                intUserAccountId = .UserAccountId
                strWindowsAccountName = .windowsAccountName
                strUserName = .UserName
                strEmail = .Email
                bolIsActive = .IsActive
                bolIsConnectionSucceeded = .IsConnectionSucceeded
                intEmployeeId = .EmployeeId
                MyBase.IsFound = .IsFound

                intSelectFilter = CType(.SelectFilter, BOEUserAccount.FilterSelect)
                intWhereFilter = CType(.WhereFilter, BOEUserAccount.FilterWhere)
                intOrderByFilter = CType(.OrderByFilter, BOEUserAccount.FilterOrderBy)
            End With
        End Sub
#End Region

#Region " Public "
        Public Function Load(ByVal id As Integer) As Boolean
            Dim eUserAccount As UserAccount = New DAUserAccount().[Find](id)
            If eUserAccount IsNot Nothing Then
                MapEntityToProperties(eUserAccount)
                Return True
            Else
                Return False
            End If
        End Function
        ''' <summary>
        ''' The capabilities are least restrictive.  If a user is in more then one role 
        ''' and one has edit and the other is read only then edit is returned.
        ''' </summary>
        ''' <param name="capabilityId"></param>
        ''' <param name="rolesWithCapabilities"></param>
        ''' <returns></returns>
        Public Function GetCapabilityAccess( _
        ByVal CapabilityId As Integer, _
        ByVal RolesWithCapabilities As BORoles) _
        As BOERoleCapability.AccessFlagEnum

            Dim retVal As BOERoleCapability.AccessFlagEnum = BOERoleCapability.AccessFlagEnum.None

            'The roles in the user object do not include the capabilities.
            For Each boRole As BOERole In Roles
                Dim roleWithCapabilities As BOERole = RolesWithCapabilities.GetByRoleId(boRole.RoleId)

                For Each boRoleCapability As BOERoleCapability In roleWithCapabilities.RoleCapabilities
                    If boRoleCapability.Capability.CapabilityId = CapabilityId Then
                        If boRoleCapability.AccessFlag = BOERoleCapability.AccessFlagEnum.Edit Then
                            Return BOERoleCapability.AccessFlagEnum.Edit
                        ElseIf boRoleCapability.AccessFlag = BOERoleCapability.AccessFlagEnum.Read_Only Then
                            'Since this is least restrictive temporarirly set the return value to read only.
                            retVal = BOERoleCapability.AccessFlagEnum.Read_Only
                        End If
                    End If
                Next
            Next

            Return retVal
        End Function
#End Region

    End Class
#End Region

#Region " BOUsers "
    Public Class BOUsers
        Inherits BOListBase(Of BOEUserAccount)

#Region " Public "
        Public Sub Load()
            LoadFromList(New DAUserAccount().[Load]())
        End Sub
        Public Sub LoadDepartmentUsers(ByVal UserAccountId As Integer)
            LoadFromList(New DAUserAccount().[LoadDepartmentUsers](UserAccountId))
        End Sub
        Public Sub LoadWithRoles()
            Load()
            For Each bUser As BOEUserAccount In Me
                bUser.Roles.LoadByUserAccountId(bUser.UserAccountId)
            Next
        End Sub
        Public Function GetByWindowAccountName(ByVal WindowsAccountName As String) As BOEUserAccount
            Return Me.SingleOrDefault(Function(u) u.windowsAccountName.ToUpper() = windowsAccountName.ToUpper())
        End Function
        Public Function GetByID(ByVal id As Integer) As BOEUserAccount
            Return Me.SingleOrDefault(Function(u) u.UserAccountId = id)
        End Function
#End Region

#Region " Private "
        Private Sub LoadFromList(ByVal UserAccounts As List(Of UserAccount))
            If UserAccounts.Count > 0 Then
                For Each eUserAccount As UserAccount In UserAccounts
                    Dim bUserAccount As New BOEUserAccount
                    bUserAccount.MapEntityToProperties(eUserAccount)
                    Me.Add(bUserAccount)
                Next
            End If
        End Sub
#End Region

    End Class

#End Region

End Namespace


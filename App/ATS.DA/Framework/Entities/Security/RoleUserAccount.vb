Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RoleUserAccount
        Inherits DataEntityBase

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
        Public Sub New( _
        ByVal RoleUserAccountId As Integer, _
        ByVal RoleId As Integer, _
        ByVal UserAccountId As Integer)
            intRoleUserAccountId = RoleUserAccountId
            intRoleId = RoleId
            intUserAccountId = UserAccountId
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intRoleUserAccountId As Integer
        Private intRoleId As Integer
        Private intUserAccountId As Integer
        Private strUserName As String
#End Region

#Region " Public Properties To Match the Table Definition "
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

    End Class
End Namespace


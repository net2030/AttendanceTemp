Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Role
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
        Public Sub New( _
        ByVal RoleName As String)
            strRoleName = RoleName
        End Sub
        Public Sub New( _
        ByVal RoleId As Integer, _
        ByVal RoleName As String)
            intRoleId = RoleId
            strRoleName = RoleName
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intRoleId As Integer
        Private strRoleName As String
        Private intDepartmentId As Integer
#End Region

#Region " Public Properties To Match the Table Definition "
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
#End Region

    End Class
End Namespace

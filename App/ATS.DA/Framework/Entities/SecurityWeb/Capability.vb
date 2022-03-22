Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PagePermession
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

        Public Enum AccessTypeEnum As Integer
            ReadOnlyEdit = 0
            Read_Only = 1
            Edit = 2
        End Enum

#End Region

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intAccountPagePermissionId As Integer
        Private intAccountRoleId As Integer
        Private intSystemSecurityCategoryPageId As Integer
        Private intSystemPermissionId As Integer
#End Region

#Region " Public Properties To Match the Table Definition "
        Public Property AccountPagePermissionId() As Integer
            Get
                Return intAccountPagePermissionId
            End Get
            Set(ByVal value As Integer)
                intAccountPagePermissionId = value
            End Set
        End Property
        Public Property AccountRoleId() As Integer
            Get
                Return intAccountRoleId
            End Get
            Set(ByVal value As Integer)
                intAccountRoleId = value
            End Set
        End Property
        Public Property SystemSecurityCategoryPageId() As Integer
            Get
                Return intSystemSecurityCategoryPageId
            End Get
            Set(ByVal value As Integer)
                intSystemSecurityCategoryPageId = value
            End Set
        End Property
        ''' <summary>
        ''' what types of access should be allowed for this capability
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SystemPermissionId() As Integer
            Get
                Return intSystemPermissionId
            End Get
            Set(ByVal value As Integer)
                intSystemPermissionId = value
            End Set
        End Property
#End Region

    End Class
End Namespace

Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RoleCapability
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

        Public Enum AccessFlagEnum As Integer
            None = 0
            Read_Only = 1
            Edit = 2
        End Enum

#End Region

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intRoleCapabilityId As Integer
        Private intRoleId As Integer
        Private intCapabilityId As Integer
        Private intAccessFlag As AccessFlagEnum
#End Region
#Region " Public Properties To Match the Table Definition "
        Public Property RoleCapabilityId() As Integer
            Get
                Return intRoleCapabilityId
            End Get
            Set(ByVal value As Integer)
                intRoleCapabilityId = value
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
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CapabilityId() As Integer
            Get
                Return intCapabilityId
            End Get
            Set(ByVal value As Integer)
                intCapabilityId = value
            End Set
        End Property
        ''' <summary>
        ''' The AccessFlag field specifies the type of access 
        ''' the user has for this capability        
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AccessFlag() As AccessFlagEnum
            Get
                Return intAccessFlag
            End Get
            Set(ByVal value As AccessFlagEnum)
                intAccessFlag = value
            End Set
        End Property
#End Region

    End Class
End Namespace

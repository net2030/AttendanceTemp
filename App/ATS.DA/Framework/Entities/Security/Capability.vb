Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Capability
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
        Private intCapabilityId As Integer
        Private strCapabilityName As String
        Private intMenuItemId As Integer
        Private intAccessType As AccessTypeEnum
#End Region

#Region " Public Properties To Match the Table Definition "
        Public Property CapabilityId() As Integer
            Get
                Return intCapabilityId
            End Get
            Set(ByVal value As Integer)
                intCapabilityId = value
            End Set
        End Property
        Public Property CapabilityName() As String
            Get
                Return strCapabilityName
            End Get
            Set(ByVal value As String)
                strCapabilityName = value
            End Set
        End Property
        Public Property MenuItemId() As Integer
            Get
                Return intMenuItemId
            End Get
            Set(ByVal value As Integer)
                intMenuItemId = value
            End Set
        End Property
        ''' <summary>
        ''' what types of access should be allowed for this capability
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AccessType() As AccessTypeEnum
            Get
                Return intAccessType
            End Get
            Set(ByVal value As AccessTypeEnum)
                intAccessType = value
            End Set
        End Property
#End Region

    End Class
End Namespace

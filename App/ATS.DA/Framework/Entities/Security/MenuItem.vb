Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MenuItem
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
        ByVal MenuItemName As String, _
        ByVal Description As String, _
        ByVal Url As String, _
        ByVal ParentMenuItemId As Integer, _
        ByVal DisplaySequence As Integer, _
        ByVal IsAlwaysEnabled As Boolean)
            strMenuItemName = MenuItemName
            strDescription = Description
            strUrl = Url
            intParentMenuItemId = ParentMenuItemId
            intDisplaySequence = DisplaySequence
            bolIsAlwaysEnabled = IsAlwaysEnabled
        End Sub
        Public Sub New( _
        ByVal MenuItemId As Integer, _
        ByVal MenuItemName As String, _
        ByVal Description As String, _
        ByVal Url As String, _
        ByVal ParentMenuItemId As Integer, _
        ByVal DisplaySequence As Integer, _
        ByVal IsAlwaysEnabled As Boolean)
            intMenuItemId = MenuItemId
            strMenuItemName = MenuItemName
            strDescription = Description
            strUrl = Url
            intParentMenuItemId = ParentMenuItemId
            intDisplaySequence = DisplaySequence
            bolIsAlwaysEnabled = IsAlwaysEnabled
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intMenuItemId As Integer
        Private strMenuItemName As String
        Private strDescription As String
        Private strUrl As String
        Private intParentMenuItemId As Integer
        Private intDisplaySequence As Integer
        Private bolIsAlwaysEnabled As Boolean
        Private strMenuItemTag As String
#End Region

#Region " Public Properties To Match the Table Definition "
        Public Property MenuItemId() As Integer
            Get
                Return intMenuItemId
            End Get
            Set(ByVal value As Integer)
                intMenuItemId = value
            End Set
        End Property
        Public Property MenuItemName() As String
            Get
                Return strMenuItemName
            End Get
            Set(ByVal value As String)
                strMenuItemName = value
            End Set
        End Property
        Public Property Description() As String
            Get
                Return strDescription
            End Get
            Set(ByVal value As String)
                strDescription = value
            End Set
        End Property
        Public Property Url() As String
            Get
                Return strUrl
            End Get
            Set(ByVal value As String)
                strUrl = value
            End Set
        End Property
        Public Property ParentMenuItemId() As Integer
            Get
                Return intParentMenuItemId
            End Get
            Set(ByVal value As Integer)
                intParentMenuItemId = value
            End Set
        End Property
        Public Property DisplaySequence() As Integer
            Get
                Return intDisplaySequence
            End Get
            Set(ByVal value As Integer)
                intDisplaySequence = value
            End Set
        End Property
        Public Property IsAlwaysEnabled() As Boolean
            Get
                Return bolIsAlwaysEnabled
            End Get
            Set(ByVal value As Boolean)
                bolIsAlwaysEnabled = value
            End Set
        End Property
        Public Property MenuItemTag() As String
            Get
                Return strMenuItemTag
            End Get
            Set(ByVal value As String)
                strMenuItemTag = value
            End Set
        End Property
#End Region

    End Class
End Namespace


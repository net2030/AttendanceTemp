Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UserAccount
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
        End Sub
        Public Sub New( _
        ByVal WindowsAccountName As String, _
        ByVal JobTitleId As Integer, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean)
            strWindowsAccountName = WindowsAccountName
            strUserName = UserName
            strEmail = Email
            bolIsActive = IsActive
        End Sub
        Public Sub New( _
        ByVal UserAccountId As Integer, _
        ByVal WindowsAccountName As String, _
        ByVal JobTitleId As Integer, _
        ByVal UserName As String, _
        ByVal Email As String, _
        ByVal IsActive As Boolean)
            intUserAccountId = UserAccountId
            strWindowsAccountName = WindowsAccountName
            strUserName = UserName
            strEmail = Email
            bolIsActive = IsActive
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intUserAccountId As Integer
        Private strWindowsAccountName As String
        Private strUserName As String
        Private strEmail As String
        Private bolIsActive As Boolean
        Private intEmployeeId As Integer

        Private bolIsConnectionSucceeded As Boolean = False
#End Region

#Region " Public Properties To Match the Table Definition "

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

        Public Property IsConnectionSucceeded() As Boolean
            Get
                Return bolIsConnectionSucceeded
            End Get
            Set(ByVal value As Boolean)
                bolIsConnectionSucceeded = value
            End Set
        End Property
#End Region

    End Class
End Namespace


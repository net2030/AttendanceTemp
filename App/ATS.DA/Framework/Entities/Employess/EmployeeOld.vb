Namespace Framework
    Public Class Employee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
            oUserAccount = New UserAccount
        End Sub
#End Region

#Region " Private Data To Match the Table Definition "
        Private intEmployeeId As Integer
        Private strEmployeeName As String
        Private strEmployeeNameEnglish As String
        Private intManagerId As Integer
        Private strJobTitle As String
        Private intPositionTypeId As Integer
        Private intPositionId As Integer
        Private strBadgeNo As String
        Private intDepartmentId As Integer
        Private intNationalityId As Integer
        Private datHireDate As Date
        Private bolIsActive As Boolean
        Private bytPicture As Byte()
        Private intActionId As Integer
        Private strImageFileName As String
        Private bolIsFingerRegistered As Boolean
        Private bolIsGatepassApproval As Boolean

        Private strFirstName As String
        Private strMiddleName As String
        Private strLastName As String

        Private intRoleId As Integer
        Private intLocationId As Integer
        Private strUserName As String
        Private strPassword As String
        Private strAllowedAccessFromIP As String
        Private bolIsForcePasswordChange As Boolean
        Private intWorkScheduleId As Integer

        Private strEmailAddress As String



        Private oUserAccount As UserAccount
#End Region

#Region " Public Properties To Match the Table Definition  "

        Public Property FirstName() As String
            Get
                Return strFirstName
            End Get
            Set(ByVal value As String)
                strFirstName = value
            End Set
        End Property

        Public Property MiddleName() As String
            Get
                Return strMiddleName
            End Get
            Set(ByVal value As String)
                strMiddleName = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return strLastName
            End Get
            Set(ByVal value As String)
                strLastName = value
            End Set
        End Property



        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Public Property EmployeeName() As String
            Get
                Return strEmployeeName
            End Get
            Set(ByVal value As String)
                strEmployeeName = value
            End Set
        End Property
        Public Property EmployeeNameEnglish() As String
            Get
                Return strEmployeeNameEnglish
            End Get
            Set(ByVal value As String)
                strEmployeeNameEnglish = value
            End Set
        End Property
        Public Property ManagerId() As Integer
            Get
                Return intManagerId
            End Get
            Set(ByVal value As Integer)
                intManagerId = value
            End Set
        End Property
        Public Property JobTitle() As String
            Get
                Return strJobTitle
            End Get
            Set(ByVal value As String)
                strJobTitle = value
            End Set
        End Property
        Public Property PositionTypeId() As Integer
            Get
                Return intPositionTypeId
            End Get
            Set(ByVal value As Integer)
                intPositionTypeId = value
            End Set
        End Property
        Public Property PositionId() As Integer
            Get
                Return intPositionId
            End Get
            Set(ByVal value As Integer)
                intPositionId = value
            End Set
        End Property
        Public Property BadgeNo() As String
            Get
                Return strBadgeNo
            End Get
            Set(ByVal value As String)
                strBadgeNo = value
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
        Public Property NationalityId() As Integer
            Get
                Return intNationalityId
            End Get
            Set(ByVal value As Integer)
                intNationalityId = value
            End Set
        End Property
        Public Property HireDate() As Date
            Get
                Return datHireDate
            End Get
            Set(ByVal value As Date)
                datHireDate = value
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
        Public Property Picture() As Byte()
            Get
                Return bytPicture
            End Get
            Set(ByVal value As Byte())
                bytPicture = value
            End Set
        End Property
        Public Property ActionId() As Integer
            Get
                Return intActionId
            End Get
            Set(ByVal value As Integer)
                intActionId = value
            End Set
        End Property
        Public Property UserAccount As UserAccount
            Get
                Return oUserAccount
            End Get
            Set(ByVal value As UserAccount)
                oUserAccount = value
            End Set
        End Property
        Public Property ImageFileName() As String
            Get
                Return strImageFileName
            End Get
            Set(ByVal value As String)
                strImageFileName = value
            End Set
        End Property
        Public Property IsFingerRegistered() As Boolean
            Get
                Return bolIsFingerRegistered
            End Get
            Set(ByVal value As Boolean)
                bolIsFingerRegistered = value
            End Set
        End Property
        Public Property IsGatepassApproval() As Boolean
            Get
                Return bolIsGatepassApproval
            End Get
            Set(ByVal value As Boolean)
                bolIsGatepassApproval = value
            End Set
        End Property

        Public Property IsForcePasswordChange() As Boolean
            Get
                Return bolIsForcePasswordChange
            End Get
            Set(ByVal value As Boolean)
                bolIsForcePasswordChange = value
            End Set
        End Property

        Public Property AllowedAccessFromIP() As String
            Get
                Return strAllowedAccessFromIP
            End Get
            Set(ByVal value As String)
                strAllowedAccessFromIP = value
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

        Public Property Password() As String
            Get
                Return strPassword
            End Get
            Set(ByVal value As String)
                strPassword = value
            End Set
        End Property

        Public Property EmailAddress() As String
            Get
                Return strEmailAddress
            End Get
            Set(ByVal value As String)
                strEmailAddress = value
            End Set
        End Property

        Public Property LocationId() As Integer
            Get
                Return intLocationId
            End Get
            Set(ByVal value As Integer)
                intLocationId = value
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

        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property
#End Region

    End Class
End Namespace


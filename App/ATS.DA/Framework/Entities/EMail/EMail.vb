Namespace Framework
    Public Class EMail
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intEmailId As Integer
        Private strToEmailAddress As String
        Private strCCEmailAddress As String
        Private strBCCEmailAddress As String
        Private strFromEmailAddress As String
        Private strEmailSubject As String
        Private strEmailBody As String
        Private bolIsEmailSent As Boolean
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property EmailId() As Integer
            Get
                Return intEmailId
            End Get
            Set(ByVal value As Integer)
                intEmailId = value
            End Set
        End Property
        Public Property ToEmailAddress() As String
            Get
                Return strToEmailAddress
            End Get
            Set(ByVal value As String)
                strToEmailAddress = value
            End Set
        End Property

        Public Property CCEmailAddress() As String
            Get
                Return strCCEmailAddress
            End Get
            Set(ByVal value As String)
                strCCEmailAddress = value
            End Set
        End Property
        Public Property BCCEmailAddress() As String
            Get
                Return strBCCEmailAddress
            End Get
            Set(ByVal value As String)
                strBCCEmailAddress = value
            End Set
        End Property
        Public Property FromEmailAddress() As String
            Get
                Return strFromEmailAddress
            End Get
            Set(ByVal value As String)
                strFromEmailAddress = value
            End Set
        End Property
        Public Property EmailSubject() As String
            Get
                Return strEmailSubject
            End Get
            Set(ByVal value As String)
                strEmailSubject = value
            End Set
        End Property
        Public Property EmailBody() As String
            Get
                Return strEmailBody
            End Get
            Set(ByVal value As String)
                strEmailBody = value
            End Set
        End Property
        Public Property IsEmailSent() As Boolean
            Get
                Return bolIsEmailSent
            End Get
            Set(ByVal value As Boolean)
                bolIsEmailSent = value
            End Set
        End Property
#End Region


    End Class
End Namespace


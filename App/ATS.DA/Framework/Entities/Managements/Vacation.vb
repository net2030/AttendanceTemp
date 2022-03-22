Namespace Framework

    Public Class Vacation
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intVacationId As Integer
        Public Property VacationId() As Integer
            Get
                Return intVacationId
            End Get
            Set(ByVal value As Integer)
                intVacationId = value
            End Set
        End Property
        Private intEmployeeId As Integer
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Private intTypeId As Integer
        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
            End Set
        End Property

        Private intAltEmployeeId As Integer
        Public Property AltEmployeeId() As Integer
            Get
                Return intAltEmployeeId
            End Get
            Set(ByVal value As Integer)
                intAltEmployeeId = value
            End Set
        End Property

        Private datEffectiveDate As Date
        Public Property EffectiveDate() As Date
            Get
                Return datEffectiveDate
            End Get
            Set(ByVal value As Date)
                datEffectiveDate = value
            End Set
        End Property
        Private datDateExpire As Date
        Public Property DateExpire() As Date
            Get
                Return datDateExpire
            End Get
            Set(ByVal value As Date)
                datDateExpire = value
            End Set
        End Property
        Private datDateOfReturn As Date
        Public Property DateOfReturn() As Date
            Get
                Return datDateOfReturn
            End Get
            Set(ByVal value As Date)
                datDateOfReturn = value
            End Set
        End Property

        Private strNote As String
        Public Property Note() As String
            Get
                Return strNote
            End Get
            Set(ByVal value As String)
                strNote = value
            End Set
        End Property

        Private strAddress As String
        Public Property Address() As String
            Get
                Return strAddress
            End Get
            Set(ByVal value As String)
                strAddress = value
            End Set
        End Property

        Private strContact As String
        Public Property Contact() As String
            Get
                Return strContact
            End Get
            Set(ByVal value As String)
                strContact = value
            End Set
        End Property

#End Region

    End Class

End Namespace


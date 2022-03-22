Namespace Framework

    Public Class Holiday
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intHolidayId As Integer
        Private strHolidayName As String
        Private datEffectiveDate As Date
        Private datDateExpire As Date
        Private strNote As String
#End Region

#Region " prperties To Match the Table Definition  "
        Public Property HolidayId() As Integer
            Get
                Return intHolidayId
            End Get
            Set(ByVal value As Integer)
                intHolidayId = value
            End Set
        End Property
        Public Property HolidayName() As String
            Get
                Return strHolidayName
            End Get
            Set(ByVal value As String)
                strHolidayName = value
            End Set
        End Property
        Public Property EffectiveDate() As Date
            Get
                Return datEffectiveDate
            End Get
            Set(ByVal value As Date)
                datEffectiveDate = value
            End Set
        End Property
        Public Property DateExpire() As Date
            Get
                Return datDateExpire
            End Get
            Set(ByVal value As Date)
                datDateExpire = value
            End Set
        End Property
        Public Property Note() As String
            Get
                Return strNote
            End Get
            Set(ByVal value As String)
                strNote = value
            End Set
        End Property
#End Region

    End Class

End Namespace


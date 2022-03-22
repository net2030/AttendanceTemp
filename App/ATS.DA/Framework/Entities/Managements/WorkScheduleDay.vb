Namespace Framework

    Public Class WorkScheduleDay
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local variables  "
        Private intWorkScheduleId As Integer
        Private intShiftNo As Integer
        Private intWeekDayNo As Integer
        Private bolIsWeekendDay As Boolean
        Private datWorkBegTime As DateTime
        Private datWorkEndTime As DateTime
#End Region

#Region " Properties To Match the Table Definition  "
        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property
        Public Property ShiftNo() As Integer
            Get
                Return intShiftNo
            End Get
            Set(ByVal value As Integer)
                intShiftNo = value
            End Set
        End Property
        Public Property WeekDayNo() As Integer
            Get
                Return intWeekDayNo
            End Get
            Set(ByVal value As Integer)
                intWeekDayNo = value
            End Set
        End Property
        Public Property IsWeekendDay() As Boolean
            Get
                Return bolIsWeekendDay
            End Get
            Set(ByVal value As Boolean)
                bolIsWeekendDay = value
            End Set
        End Property
        Public Property WorkBegTime() As DateTime
            Get
                Return datWorkBegTime
            End Get
            Set(ByVal value As DateTime)
                datWorkBegTime = value
            End Set
        End Property
        Public Property WorkEndTime() As DateTime
            Get
                Return datWorkEndTime
            End Get
            Set(ByVal value As DateTime)
                datWorkEndTime = value
            End Set
        End Property
#End Region

    End Class

End Namespace


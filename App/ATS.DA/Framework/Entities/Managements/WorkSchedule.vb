Namespace Framework

    Public Class WorkSchedule
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intWorkScheduleId As Integer
        Private intWorkScheduleTypeId As Integer
        Private strWorkScheduleName As String
        Private strWorkScheduleNameEN As String
        Private datScheduleBegDate As Date
        Private datScheduleEndDate As Date
        Private bolIsEffectiveDuringHoliday As Boolean
        Private bolIsPolicyApplied As Boolean
        Private intPolicyId As Integer
        Private intDepartmentId As Integer
        Private intShiftsCount As Integer
        Private bolIsPeriodic As Boolean
        Private intPeriodLength As Integer
        Private datFirstPeriodStartDate As Date
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
        Public Property WorkScheduleTypeId() As Integer
            Get
                Return intWorkScheduleTypeId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleTypeId = value
            End Set
        End Property
        Public Property WorkScheduleName() As String
            Get
                Return strWorkScheduleName
            End Get
            Set(ByVal value As String)
                strWorkScheduleName = value
            End Set
        End Property

        Public Property WorkScheduleNameEN() As String
            Get
                Return strWorkScheduleNameEN
            End Get
            Set(ByVal value As String)
                strWorkScheduleNameEN = value
            End Set
        End Property

        Public Property ScheduleBegDate() As Date
            Get
                Return datScheduleBegDate
            End Get
            Set(ByVal value As Date)
                datScheduleBegDate = value
            End Set
        End Property
        Public Property ScheduleEndDate() As Date
            Get
                Return datScheduleEndDate
            End Get
            Set(ByVal value As Date)
                datScheduleEndDate = value
            End Set
        End Property
        Public Property IsEffectiveDuringHoliday() As Boolean
            Get
                Return bolIsEffectiveDuringHoliday
            End Get
            Set(ByVal value As Boolean)
                bolIsEffectiveDuringHoliday = value
            End Set
        End Property
        Public Property IsPolicyApplied() As Boolean
            Get
                Return bolIsPolicyApplied
            End Get
            Set(ByVal value As Boolean)
                bolIsPolicyApplied = value
            End Set
        End Property
        Public Property PolicyId() As Integer
            Get
                Return intPolicyId
            End Get
            Set(ByVal value As Integer)
                intPolicyId = value
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

        Public Property ShiftsCount() As Integer
            Get
                Return intShiftsCount
            End Get
            Set(ByVal value As Integer)
                intShiftsCount = value
            End Set
        End Property

        Public Property IsPeriodic() As Boolean
            Get
                Return bolIsPeriodic
            End Get
            Set(ByVal value As Boolean)
                bolIsPeriodic = value
            End Set
        End Property

        Public Property PeriodLength() As Integer
            Get
                Return intPeriodLength
            End Get
            Set(ByVal value As Integer)
                intPeriodLength = value
            End Set
        End Property

        Public Property FirstPeriodStartDate() As Date
            Get
                Return datFirstPeriodStartDate
            End Get
            Set(ByVal value As Date)
                datFirstPeriodStartDate = value
            End Set
        End Property
#End Region

    End Class

End Namespace


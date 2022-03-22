Namespace Framework
    Public Class AttendanceLog
        Inherits DataEntityBase

#Region " Private Data To Match the Table Definition  "
        Private intLogId As Integer
        Public Property LogId() As Integer
            Get
                Return intLogId
            End Get
            Set(ByVal value As Integer)
                intLogId = value
            End Set
        End Property
        Private datLogDate As Date
        Public Property LogDate() As Date
            Get
                Return datLogDate
            End Get
            Set(ByVal value As Date)
                datLogDate = value
            End Set
        End Property
        Private intStatusId As Integer
        Public Property StatusId() As Integer
            Get
                Return intStatusId
            End Get
            Set(ByVal value As Integer)
                intStatusId = value
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
        Private intInLogId As Integer
        Public Property InLogId() As Integer
            Get
                Return intInLogId
            End Get
            Set(ByVal value As Integer)
                intInLogId = value
            End Set
        End Property
        Private datInTime As Date
        Public Property InTime() As Date
            Get
                Return datInTime
            End Get
            Set(ByVal value As Date)
                datInTime = value
            End Set
        End Property
        Private intOutLogId As Integer
        Public Property OutLogId() As Integer
            Get
                Return intOutLogId
            End Get
            Set(ByVal value As Integer)
                intOutLogId = value
            End Set
        End Property
        Private datOutTime As Date
        Public Property OutTime() As Date
            Get
                Return datOutTime
            End Get
            Set(ByVal value As Date)
                datOutTime = value
            End Set
        End Property
        Private bolIsInPunchManual As Boolean
        Public Property IsInPunchManual() As Boolean
            Get
                Return bolIsInPunchManual
            End Get
            Set(ByVal value As Boolean)
                bolIsInPunchManual = value
            End Set
        End Property
        Private bolIsOutPunchManual As Boolean
        Public Property IsOutPunchManual() As Boolean
            Get
                Return bolIsOutPunchManual
            End Get
            Set(ByVal value As Boolean)
                bolIsOutPunchManual = value
            End Set
        End Property

        Private intWorkingMinutes As Integer
        Public Property WorkingMinutes() As Integer
            Get
                Return intWorkingMinutes
            End Get
            Set(ByVal value As Integer)
                intWorkingMinutes = value
            End Set
        End Property
        Private intLateMinutes As Integer
        Public Property LateMinutes() As Integer
            Get
                Return intLateMinutes
            End Get
            Set(ByVal value As Integer)
                intLateMinutes = value
            End Set
        End Property
        Private intOvertimeMinutes As Integer
        Public Property OvertimeMinutes() As Integer
            Get
                Return intOvertimeMinutes
            End Get
            Set(ByVal value As Integer)
                intOvertimeMinutes = value
            End Set
        End Property
        Private intActualWorkMinutes As Integer
        Public Property ActualWorkMinutes() As Integer
            Get
                Return intActualWorkMinutes
            End Get
            Set(ByVal value As Integer)
                intActualWorkMinutes = value
            End Set
        End Property
        Private intWorkScheduleId As Integer
        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property

        Private datWorkStartTime As Date
        Public Property WorkStartTime() As Date
            Get
                Return datWorkStartTime
            End Get
            Set(ByVal value As Date)
                datWorkStartTime = value
            End Set
        End Property
        Private datWorkEndTime As Date
        Public Property WorkEndTime() As Date
            Get
                Return datWorkEndTime
            End Get
            Set(ByVal value As Date)
                datWorkEndTime = value
            End Set
        End Property
        Private intPolicyId As Integer
        Public Property PolicyId() As Integer
            Get
                Return intPolicyId
            End Get
            Set(ByVal value As Integer)
                intPolicyId = value
            End Set
        End Property
        Private intLateInMinutes As Integer
        Public Property LateInMinutes() As Integer
            Get
                Return intLateInMinutes
            End Get
            Set(ByVal value As Integer)
                intLateInMinutes = value
            End Set
        End Property
        Private intLateOutMinutes As Integer
        Public Property LateOutMinutes() As Integer
            Get
                Return intLateOutMinutes
            End Get
            Set(ByVal value As Integer)
                intLateOutMinutes = value
            End Set
        End Property
        Private intMarkObsentDuration As Integer
        Public Property MarkObsentDuration() As Integer
            Get
                Return intMarkObsentDuration
            End Get
            Set(ByVal value As Integer)
                intMarkObsentDuration = value
            End Set
        End Property

        Private bolIsProcessed As Boolean
        Public Property IsProcessed() As Boolean
            Get
                Return bolIsProcessed
            End Get
            Set(ByVal value As Boolean)
                bolIsProcessed = value
            End Set
        End Property
        Private bolIsReapplyPolicy As Boolean
        Public Property IsReapplyPolicy() As Boolean
            Get
                Return bolIsReapplyPolicy
            End Get
            Set(ByVal value As Boolean)
                bolIsReapplyPolicy = value
            End Set
        End Property
        Private intActionId As Integer
        Public Property ActionId() As Integer
            Get
                Return intActionId
            End Get
            Set(ByVal value As Integer)
                intActionId = value
            End Set
        End Property
        Private bolIsManualUpdated As Boolean
        Public Property IsManualUpdated() As Boolean
            Get
                Return bolIsManualUpdated
            End Get
            Set(ByVal value As Boolean)
                bolIsManualUpdated = value
            End Set
        End Property
        Private intUpdateVersion As Integer
        Public Property UpdateVersion() As Integer
            Get
                Return intUpdateVersion
            End Get
            Set(ByVal value As Integer)
                intUpdateVersion = value
            End Set
        End Property
#End Region

    End Class
End Namespace


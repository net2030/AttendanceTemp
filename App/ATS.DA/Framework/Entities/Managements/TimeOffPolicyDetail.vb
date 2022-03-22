Namespace Framework

    Public Class TimeOffPolicyDetail
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "

        Private intTimeOffPolicyDetailId As Integer
        Private intTimeOffPolicyId As Integer
        Private intGatepassTypeId As Integer
        Private intEarnPeriodId As Integer
        Private intResetToZeroPeriodId As Integer
        Private decInitialSetToMinutes As Decimal
        Private decResetToMinutes As Decimal
        Private decEarnMinutes As Decimal
        Private datEffectiveDate As Date
        Private bolIsCarryForward As Boolean
        Private decMinValue As Decimal
        Private decMaxValue As Decimal

#End Region

#Region " Properties To Match the Table Definition  "

        Public Property TimeOffPolicyDetailId() As Integer
            Get
                Return intTimeOffPolicyDetailId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyDetailId = value
            End Set
        End Property

        Public Property TimeOffPolicyId() As Integer
            Get
                Return intTimeOffPolicyId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyId = value
            End Set
        End Property

        Public Property GatepassTypeId() As Integer
            Get
                Return intGatepassTypeId
            End Get
            Set(ByVal value As Integer)
                intGatepassTypeId = value
            End Set
        End Property
        Public Property EarnPeriodId() As Integer
            Get
                Return intEarnPeriodId
            End Get
            Set(ByVal value As Integer)
                intEarnPeriodId = value
            End Set
        End Property


        Public Property ResetToZeroPeriodId() As Integer
            Get
                Return intResetToZeroPeriodId
            End Get
            Set(ByVal value As Integer)
                intResetToZeroPeriodId = value
            End Set
        End Property

        Public Property InitialSetToMinutes() As Decimal
            Get
                Return decInitialSetToMinutes
            End Get
            Set(ByVal value As Decimal)
                decInitialSetToMinutes = value
            End Set
        End Property

        Public Property ResetToMinutes() As Decimal
            Get
                Return decResetToMinutes
            End Get
            Set(ByVal value As Decimal)
                decResetToMinutes = value
            End Set
        End Property

        Public Property EarnMinutes() As Decimal
            Get
                Return decEarnMinutes
            End Get
            Set(ByVal value As Decimal)
                decEarnMinutes = value
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

        Public Property IsCarryForward() As Boolean
            Get
                Return bolIsCarryForward
            End Get
            Set(ByVal value As Boolean)
                bolIsCarryForward = value
            End Set
        End Property

        Public Property MinValue() As Decimal
            Get
                Return decMinValue
            End Get
            Set(ByVal value As Decimal)
                decMinValue = value
            End Set
        End Property

        Public Property MaxValue() As Decimal
            Get
                Return decMaxValue
            End Get
            Set(ByVal value As Decimal)
                decMaxValue = value
            End Set
        End Property

#End Region

    End Class

End Namespace


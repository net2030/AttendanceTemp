Namespace Framework

    Public Class Policy
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intPolicyId As Integer
        Public Property PolicyId() As Integer
            Get
                Return intPolicyId
            End Get
            Set(ByVal value As Integer)
                intPolicyId = value
            End Set
        End Property
        Private strPolicyName As String
        Public Property PolicyName() As String
            Get
                Return strPolicyName
            End Get
            Set(ByVal value As String)
                strPolicyName = value
            End Set
        End Property

        Private strPolicyNameEN As String
        Public Property PolicyNameEN() As String
            Get
                Return strPolicyNameEN
            End Get
            Set(ByVal value As String)
                strPolicyNameEN = value
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
        Private intEarlyInMinutes As Integer
        Public Property EarlyInMinutes() As Integer
            Get
                Return intEarlyInMinutes
            End Get
            Set(ByVal value As Integer)
                intEarlyInMinutes = value
            End Set
        End Property
        Private intEarlyOutMinutes As Integer
        Public Property EarlyOutMinutes() As Integer
            Get
                Return intEarlyOutMinutes
            End Get
            Set(ByVal value As Integer)
                intEarlyOutMinutes = value
            End Set
        End Property
        Private intLateLimitPerMonthMinutes As Integer
        Public Property LateLimitPerMonthMinutes() As Integer
            Get
                Return intLateLimitPerMonthMinutes
            End Get
            Set(ByVal value As Integer)
                intLateLimitPerMonthMinutes = value
            End Set
        End Property
        Private intDepartmentId As Integer
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
#End Region

    End Class

End Namespace


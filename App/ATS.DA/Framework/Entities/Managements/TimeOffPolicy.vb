Namespace Framework

    Public Class TimeOffPolicy
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intTimeOffPolicyId As Integer
        Private strTimeOffPolicyName As String
        Private intTypeId As Integer
        Private datEffectiveDate As Date
#End Region

#Region " Properties To Match the Table Definition  "
        Public Property TimeOffPolicyId() As Integer
            Get
                Return intTimeOffPolicyId
            End Get
            Set(ByVal value As Integer)
                intTimeOffPolicyId = value
            End Set
        End Property
        
        Public Property TimeOffPolicyName() As String
            Get
                Return strTimeOffPolicyName
            End Get
            Set(ByVal value As String)
                strTimeOffPolicyName = value
            End Set
        End Property

        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
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
       
#End Region

    End Class

End Namespace


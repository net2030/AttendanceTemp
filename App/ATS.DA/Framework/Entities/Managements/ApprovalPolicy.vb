Namespace Framework

    Public Class ApprovalPolicy
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables  "
        Private intApprovalPolicyId As Integer
        Private strApprovalPolicyName As String
        Private intTimeOffApprovalType As Integer

#End Region

#Region " Properties To Match the Table Definition  "
        Public Property ApprovalPolicyId() As Integer
            Get
                Return intApprovalPolicyId
            End Get
            Set(ByVal value As Integer)
                intApprovalPolicyId = value
            End Set
        End Property

        Public Property ApprovalPolicyName() As String
            Get
                Return strApprovalPolicyName
            End Get
            Set(ByVal value As String)
                strApprovalPolicyName = value
            End Set
        End Property

        Public Property TimeOffApprovalType() As Integer
            Get
                Return intTimeOffApprovalType
            End Get
            Set(ByVal value As Integer)
                intTimeOffApprovalType = value
            End Set
        End Property

#End Region

    End Class

End Namespace


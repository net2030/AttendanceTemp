Namespace Framework

    Public Class ApprovalPath
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local variables  "
        Private intApprovalPathId As Integer
        Private intApprovalPolicyId As Integer
        Private intApproverTypeId As Integer
        Private intEmployeeId As Integer
        Private intApprovalPathSequence As Integer
#End Region

#Region " Properties To Match the Table Definition  "
        Public Property ApprovalPathId() As Integer
            Get
                Return intApprovalPathId
            End Get
            Set(ByVal value As Integer)
                intApprovalPathId = value
            End Set
        End Property
        Public Property ApprovalPolicyId() As Integer
            Get
                Return intApprovalPolicyId
            End Get
            Set(ByVal value As Integer)
                intApprovalPolicyId = value
            End Set
        End Property
        Public Property ApproverTypeId() As Integer
            Get
                Return intApproverTypeId
            End Get
            Set(ByVal value As Integer)
                intApproverTypeId = value
            End Set
        End Property
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Public Property ApprovalPathSequence() As Integer
            Get
                Return intApprovalPathSequence
            End Get
            Set(ByVal value As Integer)
                intApprovalPathSequence = value
            End Set
        End Property
#End Region

    End Class

End Namespace


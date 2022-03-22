Namespace Framework
    Public Class ApproverType
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local variables  "
        Private intApproverTypeId As Integer
        Private strApproverTypeName As String
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property ApproverTypeId() As Integer
            Get
                Return intApproverTypeId
            End Get
            Set(ByVal value As Integer)
                intApproverTypeId = value
            End Set
        End Property

        Public Property ApproverTypeName() As String
            Get
                Return strApproverTypeName
            End Get
            Set(ByVal value As String)
                strApproverTypeName = value
            End Set
        End Property
#End Region

    End Class
End Namespace


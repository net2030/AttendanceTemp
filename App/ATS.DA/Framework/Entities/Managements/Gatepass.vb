Namespace Framework

    Public Class Gatepass
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intGatepassId As Integer
        Public Property GatepassId() As Integer
            Get
                Return intGatepassId
            End Get
            Set(ByVal value As Integer)
                intGatepassId = value
            End Set
        End Property
        Private bolIsApproved As Boolean
        Public Property IsApproved() As Boolean
            Get
                Return bolIsApproved
            End Get
            Set(ByVal value As Boolean)
                bolIsApproved = value
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
        Private datGatepassBegDate As Date
        Public Property GatepassBegDate() As Date
            Get
                Return datGatepassBegDate
            End Get
            Set(ByVal value As Date)
                datGatepassBegDate = value
            End Set
        End Property
        Private datGatepassEndDate As Date
        Public Property GatepassEndDate() As Date
            Get
                Return datGatepassEndDate
            End Get
            Set(ByVal value As Date)
                datGatepassEndDate = value
            End Set
        End Property
        Private datGatepassBegTime As Date
        Public Property GatepassBegTime() As Date
            Get
                Return datGatepassBegTime
            End Get
            Set(ByVal value As Date)
                datGatepassBegTime = value
            End Set
        End Property
        Private datGatepassEndTime As Date
        Public Property GatepassEndTime() As Date
            Get
                Return datGatepassEndTime
            End Get
            Set(ByVal value As Date)
                datGatepassEndTime = value
            End Set
        End Property
        Private strNotes As String
        Public Property Notes() As String
            Get
                Return strNotes
            End Get
            Set(ByVal value As String)
                strNotes = value
            End Set
        End Property

        Private datRequestedDate As Date
        Public Property RequestedDate() As Date
            Get
                Return datRequestedDate
            End Get
            Set(ByVal value As Date)
                datRequestedDate = value
            End Set
        End Property
        Private datApprovedDate As Date
        Public Property ApprovedDate() As Date
            Get
                Return datApprovedDate
            End Get
            Set(ByVal value As Date)
                datApprovedDate = value
            End Set
        End Property

        Private intGatepassTypeId As Integer
        Public Property GatepassTypeId() As Integer
            Get
                Return intGatepassTypeId
            End Get
            Set(ByVal value As Integer)
                intGatepassTypeId = value
            End Set
        End Property
        Private intApprovedEmployeeId As Integer
        Public Property ApprovedEmployeeId() As Integer
            Get
                Return intApprovedEmployeeId
            End Get
            Set(ByVal value As Integer)
                intApprovedEmployeeId = value
            End Set
        End Property
#End Region

    End Class

End Namespace


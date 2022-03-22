Namespace Framework

    Public Class GatepassDetails
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
       
       
        Private intGatepassDetailId As Integer
        Public Property GatepassDetailId() As Integer
            Get
                Return intGatepassDetailId
            End Get
            Set(ByVal value As Integer)
                intGatepassDetailId = value
            End Set
        End Property

        Private intGatepassId As Integer
        Public Property GatepassId() As Integer
            Get
                Return intGatepassId
            End Get
            Set(ByVal value As Integer)
                intGatepassId = value
            End Set
        End Property
       

        Private intTermNo As Integer
        Public Property TermNo() As Integer
            Get
                Return intTermNo
            End Get
            Set(ByVal value As Integer)
                intTermNo = value
            End Set
        End Property
        Private intAltEmployeeId As Integer
        Public Property AltEmployeeId() As Integer
            Get
                Return intAltEmployeeId
            End Get
            Set(ByVal value As Integer)
                intAltEmployeeId = value
            End Set
        End Property
#End Region

    End Class

End Namespace


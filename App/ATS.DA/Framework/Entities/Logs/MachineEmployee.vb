Namespace Framework

    Public Class MachineEmployee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New(ByVal MachineId As Integer, ByVal EmployeeId As Integer)
            intMachineId = MachineId
            intEmployeeId = EmployeeId
        End Sub
#End Region

#Region " Local Variables  "
        Private intMachineId As Integer
        Private intEmployeeId As Integer
#End Region

#Region " Private Data To Match the Table Definition  "

        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
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
#End Region

    End Class

End Namespace


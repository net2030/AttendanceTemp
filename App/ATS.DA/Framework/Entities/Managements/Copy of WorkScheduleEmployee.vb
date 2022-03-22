Namespace Framework

    Public Class WorkScheduleEmployee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New(ByVal WorkScheduleId As Integer, ByVal EmployeeId As Integer)
            intWorkScheduleId = WorkScheduleId
            intEmployeeId = EmployeeId
        End Sub
#End Region

#Region " Local Variables  "
        Private intWorkScheduleId As Integer
        Private intEmployeeId As Integer
#End Region

#Region " Private Data To Match the Table Definition  "

        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
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


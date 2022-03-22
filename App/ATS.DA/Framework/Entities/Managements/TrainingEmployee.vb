Namespace Framework

    Public Class TrainingEmployee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New(ByVal TrainingId As Integer, ByVal EmployeeId As Integer)
            intTrainingId = TrainingId
            intEmployeeId = EmployeeId
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intTrainingId As Integer
        Private intEmployeeId As Integer
#End Region

#Region " Public Properties  "
        Public Property TrainingId() As Integer
            Get
                Return intTrainingId
            End Get
            Set(ByVal value As Integer)
                intTrainingId = value
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


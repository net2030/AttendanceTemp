Namespace Framework

    Public Class WorkExceptionEmployee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New(ByVal WorkExceptionId As Integer, ByVal EmployeeId As Integer)
            intWorkExceptionId = WorkExceptionId
            intEmployeeId = EmployeeId
        End Sub
#End Region

#Region "  Local variables  "
        Private intWorkExceptionId As Integer
        Private intEmployeeId As Integer
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property WorkExceptionId() As Integer
            Get
                Return intWorkExceptionId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionId = value
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


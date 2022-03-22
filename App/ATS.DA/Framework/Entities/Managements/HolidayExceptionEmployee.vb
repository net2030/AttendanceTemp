Namespace Framework

    Public Class HolidayExceptionEmployee
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New(ByVal HolidayId As Integer, ByVal EmployeeId As Integer)
            intHolidayId = HolidayId
            intEmployeeId = EmployeeId
        End Sub
#End Region

#Region "  Local variables  "
        Private intHolidayId As Integer
        Private intEmployeeId As Integer
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property HolidayId() As Integer
            Get
                Return intHolidayId
            End Get
            Set(ByVal value As Integer)
                intHolidayId = value
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


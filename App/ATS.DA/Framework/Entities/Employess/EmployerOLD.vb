Namespace Framework
    Public Class Employer
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local variables  "
        Private intEmployerId As Integer
        Private strEmployerName As String
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property EmployerId() As Integer
            Get
                Return intEmployerId
            End Get
            Set(ByVal value As Integer)
                intEmployerId = value
            End Set
        End Property

        Public Property EmployerName() As String
            Get
                Return strEmployerName
            End Get
            Set(ByVal value As String)
                strEmployerName = value
            End Set
        End Property
#End Region

    End Class
End Namespace


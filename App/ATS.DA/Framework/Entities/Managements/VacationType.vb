Namespace Framework

    Public Class VacationType
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intTypeId As Integer
        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
            End Set
        End Property
        Private strTypeName As String
        Public Property TypeName() As String
            Get
                Return strTypeName
            End Get
            Set(ByVal value As String)
                strTypeName = value
            End Set
        End Property

        Private strTypeNameEN As String
        Public Property TypeNameEN() As String
            Get
                Return strTypeNameEN
            End Get
            Set(ByVal value As String)
                strTypeNameEN = value
            End Set
        End Property
#End Region

    End Class

End Namespace


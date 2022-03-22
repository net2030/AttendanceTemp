Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Location
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New( _
        ByVal LocationName As String)
            strLocationName = LocationName
        End Sub
        Public Sub New( _
        ByVal LocationId As Integer, _
        ByVal LocationName As String)
            intLocationId = LocationId
            strLocationName = LocationName
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intLocationId As Integer
        Public Property LocationId() As Integer
            Get
                Return intLocationId
            End Get
            Set(ByVal value As Integer)
                intLocationId = value
            End Set
        End Property
        Private strLocationName As String
        Public Property LocationName() As String
            Get
                Return strLocationName
            End Get
            Set(ByVal value As String)
                strLocationName = value
            End Set
        End Property
#End Region



    End Class
End Namespace


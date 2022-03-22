Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Position
        Inherits DataEntityBase

#Region " Enumerations "
       
#End Region

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intPositionId As Integer
        Private strPositionName As String
        Private intPositionTypeId As Integer
        Private intSortIndex As Integer
        Private bolIsOfficer As Boolean

#End Region

#Region " Public Properties To Match the Table Definition "
        Public Property PositionId() As Integer
            Get
                Return intPositionId
            End Get
            Set(ByVal value As Integer)
                intPositionId = value
            End Set
        End Property
        Public Property PositionName() As String
            Get
                Return strPositionName
            End Get
            Set(ByVal value As String)
                strPositionName = value
            End Set
        End Property
        Public Property PositionTypeId() As Integer
            Get
                Return intPositionTypeId
            End Get
            Set(ByVal value As Integer)
                intPositionTypeId = value
            End Set
        End Property
        Public Property SortIndex() As Integer
            Get
                Return intSortIndex
            End Get
            Set(ByVal value As Integer)
                intSortIndex = value
            End Set
        End Property
        Public Property IsOfficer() As Boolean
            Get
                Return bolIsOfficer
            End Get
            Set(ByVal value As Boolean)
                bolIsOfficer = value
            End Set
        End Property
#End Region

    End Class
End Namespace


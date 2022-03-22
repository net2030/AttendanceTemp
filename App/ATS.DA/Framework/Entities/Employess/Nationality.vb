Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Nationality
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New( _
        ByVal NationalityName As String)
            strNationalityName = NationalityName
        End Sub
        Public Sub New( _
        ByVal NationalityId As Integer, _
        ByVal NationalityName As String)
            intNationalityId = NationalityId
            strNationalityName = NationalityName
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intNationalityId As Integer
        Public Property NationalityId() As Integer
            Get
                Return intNationalityId
            End Get
            Set(ByVal value As Integer)
                intNationalityId = value
            End Set
        End Property
        Private strNationalityName As String
        Public Property NationalityName() As String
            Get
                Return strNationalityName
            End Get
            Set(ByVal value As String)
                strNationalityName = value
            End Set
        End Property
#End Region



    End Class
End Namespace

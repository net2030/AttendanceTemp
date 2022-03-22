Namespace Framework
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JobTitle
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
        Public Sub New( _
        ByVal JobTitleName As String)
            strJobTitleName = JobTitleName
        End Sub
        Public Sub New( _
        ByVal JobTitleId As Integer, _
        ByVal JobTitleName As String)
            intJobTitleId = JobTitleId
            strJobTitleName = JobTitleName
        End Sub
#End Region

#Region " Private Data To Match the Table Definition  "
        Private intJobTitleId As Integer
        Public Property JobTitleId() As Integer
            Get
                Return intJobTitleId
            End Get
            Set(ByVal value As Integer)
                intJobTitleId = value
            End Set
        End Property
        Private strJobTitleName As String
        Public Property JobTitleName() As String
            Get
                Return strJobTitleName
            End Get
            Set(ByVal value As String)
                strJobTitleName = value
            End Set
        End Property
#End Region



    End Class
End Namespace


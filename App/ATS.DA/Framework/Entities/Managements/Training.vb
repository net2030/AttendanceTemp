Namespace Framework

    Public Class Training
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region " Local Variables "
        Private intTrainingId As Integer
        Private intDepartmentId As Integer
        Private strCourseName As String
        Private strInstituteName As String
        Private datTrainingBegDate As Date
        Private datTrainingEndDate As Date
        Private strNotes As String
#End Region

#Region " Private Data To Match the Table Definition  "
        Public Property TrainingId() As Integer
            Get
                Return intTrainingId
            End Get
            Set(ByVal value As Integer)
                intTrainingId = value
            End Set
        End Property
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Public Property CourseName() As String
            Get
                Return strCourseName
            End Get
            Set(ByVal value As String)
                strCourseName = value
            End Set
        End Property
        Public Property InstituteName() As String
            Get
                Return strInstituteName
            End Get
            Set(ByVal value As String)
                strInstituteName = value
            End Set
        End Property
        Public Property TrainingBegDate() As Date
            Get
                Return datTrainingBegDate
            End Get
            Set(ByVal value As Date)
                datTrainingBegDate = value
            End Set
        End Property
        Public Property TrainingEndDate() As Date
            Get
                Return datTrainingEndDate
            End Get
            Set(ByVal value As Date)
                datTrainingEndDate = value
            End Set
        End Property

        Public Property Notes() As String
            Get
                Return strNotes
            End Get
            Set(ByVal value As String)
                strNotes = value
            End Set
        End Property
#End Region

    End Class

End Namespace


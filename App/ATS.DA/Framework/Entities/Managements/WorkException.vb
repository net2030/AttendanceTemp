Namespace Framework
    Public Class WorkException
        Inherits DataEntityBase

#Region " Constructors "
        Public Sub New()
        End Sub
#End Region

#Region "  Local variables  "
        Private intWorkExceptionId As Integer
        Private intEmployeeId As Integer
        Private intWorkExceptionTypeId As Integer
        Private datWorkExceptionBegDate As Date
        Private datWorkExceptionEndDate As Date
        Private intDepartmentId As Integer
        Private strNotes As String
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

        Public Property WorkExceptionTypeId() As Integer
            Get
                Return intWorkExceptionTypeId
            End Get
            Set(ByVal value As Integer)
                intWorkExceptionTypeId = value
            End Set
        End Property
        Public Property WorkExceptionBegDate() As Date
            Get
                Return datWorkExceptionBegDate
            End Get
            Set(ByVal value As Date)
                datWorkExceptionBegDate = value
            End Set
        End Property
        Public Property WorkExceptionEndDate() As Date
            Get
                Return datWorkExceptionEndDate
            End Get
            Set(ByVal value As Date)
                datWorkExceptionEndDate = value
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


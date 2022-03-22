Namespace Framework

    Public Class Department
        Inherits DataEntityBase

#Region " Private Data To Match the Table Definition  "
        Private intDepartmentId As Integer
        Private strDepartmentName As String
        Private strDepartmentNameEN As String
        Private intParentId As Integer
#End Region
#Region " Public Properties "
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Public Property DepartmentName() As String
            Get
                Return strDepartmentName
            End Get
            Set(ByVal value As String)
                strDepartmentName = value
            End Set
        End Property

        Public Property DepartmentNameEN() As String
            Get
                Return strDepartmentNameEN
            End Get
            Set(ByVal value As String)
                strDepartmentNameEN = value
            End Set
        End Property

        Public Property ParentId() As Integer
            Get
                Return intParentId
            End Get
            Set(ByVal value As Integer)
                intParentId = value
            End Set
        End Property
#End Region

    End Class

End Namespace


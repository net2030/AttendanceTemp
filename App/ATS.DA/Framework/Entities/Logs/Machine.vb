Namespace Framework
    Public Class Machine
        Inherits DataEntityBase

#Region " Private Data To Match the Table Definition  "
        Private intMachineId As Integer
        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
            End Set
        End Property
        Private strMachineName As String
        Public Property MachineName() As String
            Get
                Return strMachineName
            End Get
            Set(ByVal value As String)
                strMachineName = value
            End Set
        End Property
        Private strLocation As String
        Public Property Location() As String
            Get
                Return strLocation
            End Get
            Set(ByVal value As String)
                strLocation = value
            End Set
        End Property
        Private intDepartmentId As Integer
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Private strIPAddress As String
        Public Property IPAddress() As String
            Get
                Return strIPAddress
            End Get
            Set(ByVal value As String)
                strIPAddress = value
            End Set
        End Property
        Private intMachineTypeId As Integer
        Public Property MachineTypeId() As Integer
            Get
                Return intMachineTypeId
            End Get
            Set(ByVal value As Integer)
                intMachineTypeId = value
            End Set
        End Property
#End Region

    End Class
End Namespace


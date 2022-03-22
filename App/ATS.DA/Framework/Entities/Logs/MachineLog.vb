Namespace Framework
    Public Class MachineLog
        Inherits DataEntityBase

#Region " Private Data To Match the Table Definition  "
        Private intLogId As Integer
        Public Property LogId() As Integer
            Get
                Return intLogId
            End Get
            Set(ByVal value As Integer)
                intLogId = value
            End Set
        End Property
        Private intEmployeeId As Integer
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Private datLogDate As Date
        Public Property LogDate() As Date
            Get
                Return datLogDate
            End Get
            Set(ByVal value As Date)
                datLogDate = value
            End Set
        End Property
        Private datLogTime As Date
        Public Property LogTime() As Date
            Get
                Return datLogTime
            End Get
            Set(ByVal value As Date)
                datLogTime = value
            End Set
        End Property
        Private intLogTypeId As Integer
        Public Property LogTypeId() As Integer
            Get
                Return intLogTypeId
            End Get
            Set(ByVal value As Integer)
                intLogTypeId = value
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
        Private intMachineId As Integer
        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
            End Set
        End Property
        Private bolIsValidRecord As Boolean
        Public Property IsValidRecord() As Boolean
            Get
                Return bolIsValidRecord
            End Get
            Set(ByVal value As Boolean)
                bolIsValidRecord = value
            End Set
        End Property
        Private bolIsManualRecord As Boolean
        Public Property IsManualRecord() As Boolean
            Get
                Return bolIsManualRecord
            End Get
            Set(ByVal value As Boolean)
                bolIsManualRecord = value
            End Set
        End Property
#End Region

    End Class
End Namespace


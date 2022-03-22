Namespace Framework
    ''' <summary>
    ''' Common Data Entity Common Attrbutes Interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ICommonAttributes
        Property AddedUserAccountId() As Integer
        Property UpdatedUserAccountId() As Integer
        Property AddedDate() As Date
        Property UpdatedDate() As Date
        Property VersionNo() As Byte()
        Property AddedUserName() As String
        Property UpdatedUserName() As String
    End Interface

End Namespace


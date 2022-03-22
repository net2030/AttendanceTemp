'***********************************************************
'* Author      : Sami Aljafar
'* Notice      : Copyright 2010
'* Date Created: 17-01-2010
'* Description : 
'*
'* Data Entity Base Class
'* 
'***********************************************************
Namespace Framework

    Public MustInherit Class DataEntityBase
        Implements Framework.ICommonAttributes


#Region " Private Variable "
        Private intAddedUserAccountId As Integer
        Private intUpdatedUserAccountId As Integer
        Private datAddedDate As Date
        Private datUpdatedDate As Date
        Private bytVersion As Byte()
        Private strAddedUserName As String
        Private strUpdatedUserName As String
        Private dtBegDate As Date
        Private dtEndDate As Date
        Private strSearchFor As String
        Private bolIsFound As Boolean
#End Region

#Region " Standard Properties "
        Public Property IsFound() As Boolean
            Get
                Return bolIsFound
            End Get
            Set(ByVal value As Boolean)
                bolIsFound = value
            End Set
        End Property
        Public Property BegDate() As Date
            Get
                Return dtBegDate
            End Get
            Set(ByVal value As Date)
                dtBegDate = value
            End Set
        End Property
        Public Property EndDate() As Date
            Get
                Return dtEndDate
            End Get
            Set(ByVal value As Date)
                dtEndDate = value
            End Set
        End Property
        Public Property SearchFor() As String
            Get
                Return strSearchFor
            End Get
            Set(ByVal value As String)
                strSearchFor = value
            End Set
        End Property
        Public Property AddedDate() As Date Implements ICommonAttributes.AddedDate
            Get
                Return datAddedDate
            End Get
            Set(ByVal value As Date)
                datAddedDate = value
            End Set
        End Property

        Public Property AddedUserAccountId() As Integer Implements ICommonAttributes.AddedUserAccountId
            Get
                Return intAddedUserAccountId
            End Get
            Set(ByVal value As Integer)
                intAddedUserAccountId = value
            End Set
        End Property

        Public Property UpdatedDate() As Date Implements ICommonAttributes.UpdatedDate
            Get
                Return datUpdatedDate
            End Get
            Set(ByVal value As Date)
                datUpdatedDate = value
            End Set
        End Property

        Public Property UpdatedUserAccountId() As Integer Implements ICommonAttributes.UpdatedUserAccountId
            Get
                Return intUpdatedUserAccountId
            End Get
            Set(ByVal value As Integer)
                intUpdatedUserAccountId = value
            End Set
        End Property

        Public Property VersionNo() As Byte() Implements ICommonAttributes.VersionNo
            Get
                Return bytVersion
            End Get
            Set(ByVal value As Byte())
                bytVersion = value
            End Set
        End Property
        Public Property AddedUserName() As String Implements ICommonAttributes.AddedUserName
            Get
                Return strAddedUserName
            End Get
            Set(ByVal value As String)
                strAddedUserName = value
            End Set
        End Property

        Public Property UpdatedUserName() As String Implements ICommonAttributes.UpdatedUserName
            Get
                Return strUpdatedUserName
            End Get
            Set(ByVal value As String)
                strUpdatedUserName = value
            End Set
        End Property
#End Region


    End Class
End Namespace


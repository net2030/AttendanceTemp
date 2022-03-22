#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Transactions
Imports System.Reflection
Imports ATS.BO.Framework
Imports ATS.DA.Framework
#End Region

Namespace Framework

#Region " BOEntityBase "
    <Serializable()> _
    Public Class BOEntityBase

#Region " Constructors "
        Public Sub New()
            MyBase.New()
            'Default the action to save.
            intDBAction = DBActionEnum.NoAction
        End Sub
#End Region

#Region " Enumerations "
        Private intDBAction As DBActionEnum
        Public Enum DBActionEnum As Integer
            NoAction = 0
            Add = 1
            Update = 2
            Delete = 3
        End Enum
#End Region

#Region " Private Variables "
        ' Common Attributes
        Private intAddedUserAccountId As Integer
        Private intUpdatedUserAccountId As Integer
        Private datAddedDate As Date
        Private datUpdatedDate As Date
        Private bytVersionNo As Byte()
        Private strAddedUserName As String
        Private strUpdatedUserName As String
        Private dtBegDate As Date
        Private dtEndDate As Date
        Private strSearchFor As String

#End Region

#Region " Standard properties "
        Private bolIsFound As Boolean
        Public Property IsFound() As Boolean
            Get
                Return bolIsFound
            End Get
            Set(ByVal value As Boolean)
                bolIsFound = value
            End Set
        End Property
        Public Property DBAction() As DBActionEnum
            Get
                Return intDBAction
            End Get
            Set(ByVal value As DBActionEnum)
                intDBAction = value
            End Set
        End Property
        Public Property AddedDate() As Date
            Get
                Return datAddedDate
            End Get
            Set(ByVal value As Date)
                datAddedDate = value
            End Set
        End Property

        Public Property AddedUserAccountId() As Integer
            Get
                Return intAddedUserAccountId
            End Get
            Set(ByVal value As Integer)
                intAddedUserAccountId = value
            End Set
        End Property

        Public Property UpdatedDate() As Date
            Get
                Return datUpdatedDate
            End Get
            Set(ByVal value As Date)
                datUpdatedDate = value
            End Set
        End Property

        Public Property UpdatedUserAccountId() As Integer
            Get
                Return intUpdatedUserAccountId
            End Get
            Set(ByVal value As Integer)
                intUpdatedUserAccountId = value
            End Set
        End Property

        Public Property VersionNo() As Byte()
            Get
                Return bytVersionNo
            End Get
            Set(ByVal value As Byte())
                bytVersionNo = value
            End Set
        End Property

        Public Property AddedUserName() As String
            Get
                Return strAddedUserName
            End Get
            Set(ByVal value As String)
                strAddedUserName = value
            End Set
        End Property
        Public Property UpdatedUserName() As String
            Get
                Return strUpdatedUserName
            End Get
            Set(ByVal value As String)
                strUpdatedUserName = value
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
#End Region

    End Class
#End Region

#Region " BOBase "
    Public MustInherit Class BOBase

#Region " Private Variables "
        Private strInfoMessage As String
        Private intMessageId As Integer
        Private intIdentity As Integer
        Private intPageTotal As Integer
        Private strFieldInError As String
        Private strSearchFor As String
#End Region

#Region " Standard properties "
        Public Property Identity() As Integer
            Get
                Return intIdentity
            End Get
            Set(ByVal value As Integer)
                intIdentity = value
            End Set
        End Property
        Public Property MessageId() As Integer
            Get
                Return intMessageId
            End Get
            Set(ByVal value As Integer)
                intMessageId = value
            End Set
        End Property
        Public Property FieldInError() As String
            Get
                Return strFieldInError
            End Get
            Set(ByVal value As String)
                strFieldInError = value
            End Set
        End Property
        Public Property InfoMessage() As String
            Get
                Return strInfoMessage
            End Get
            Set(ByVal value As String)
                strInfoMessage = value
            End Set
        End Property
        Public Property PageTotal() As Integer
            Get
                Return intPageTotal
            End Get
            Set(ByVal value As Integer)
                intPageTotal = value
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
#End Region

#Region " Constructors "
        Public Sub New()
            MyBase.New()
        End Sub
#End Region

#Region " Protected Methods "
        ''' <summary>
        ''' This method will load all the properties of the object from the entity.
        ''' </summary>        
        Protected MustOverride Function MapEntityToProperties(ByVal Entity As ICommonAttributes) As BOEntityBase
        Protected MustOverride Function MapPropertiesToEntity(ByVal Entity As BOEntityBase) As DataEntityBase
#End Region

#Region " Public Methods "
        Public MustOverride Function Delete(ByVal Id As Integer) As Boolean

        Public MustOverride Function Find(ByVal Id As Integer) As BOEntityBase

        Public MustOverride Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As DataSet
#End Region

    End Class
#End Region

#Region " BOListBase "
    <Serializable()> _
    Public MustInherit Class BOListBase(Of T As BOEntityBase)
        Inherits List(Of T)

    End Class
#End Region

End Namespace

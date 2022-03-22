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
Imports System.ComponentModel
#End Region

Namespace Framework

#Region " BOEEMail "
    Public Class BOEEMail
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intEmailId As Integer
        Private strToEmailAddress As String
        Private strCCEmailAddress As String
        Private strBCCEmailAddress As String
        Private strFromEmailAddress As String
        Private strEmailSubject As String
        Private strEmailBody As String
        Private bolIsEmailSent As Boolean
#End Region

#Region " Public Properties "
        Public Property EmailId() As Integer
            Get
                Return intEmailId
            End Get
            Set(ByVal value As Integer)
                intEmailId = value
            End Set
        End Property
        Public Property ToEmailAddress() As String
            Get
                Return strToEmailAddress
            End Get
            Set(ByVal value As String)
                strToEmailAddress = value
            End Set
        End Property

        Public Property CCEmailAddress() As String
            Get
                Return strCCEmailAddress
            End Get
            Set(ByVal value As String)
                strCCEmailAddress = value
            End Set
        End Property
        Public Property BCCEmailAddress() As String
            Get
                Return strBCCEmailAddress
            End Get
            Set(ByVal value As String)
                strBCCEmailAddress = value
            End Set
        End Property
        Public Property FromEmailAddress() As String
            Get
                Return strFromEmailAddress
            End Get
            Set(ByVal value As String)
                strFromEmailAddress = value
            End Set
        End Property
        Public Property EmailSubject() As String
            Get
                Return strEmailSubject
            End Get
            Set(ByVal value As String)
                strEmailSubject = value
            End Set
        End Property
        Public Property EmailBody() As String
            Get
                Return strEmailBody
            End Get
            Set(ByVal value As String)
                strEmailBody = value
            End Set
        End Property
        Public Property IsEmailSent() As Boolean
            Get
                Return bolIsEmailSent
            End Get
            Set(ByVal value As Boolean)
                bolIsEmailSent = value
            End Set
        End Property
#End Region

#Region " Properties Mapping "
        Public Sub MapEntityToProperties(ByVal entity As ICommonAttributes)
            If Not IsNothing(entity) Then
                MyBase.AddedDate = entity.AddedDate
                MyBase.AddedUserAccountId = entity.AddedUserAccountId
                MyBase.UpdatedDate = entity.UpdatedDate
                MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
                MyBase.VersionNo = entity.VersionNo
                MyBase.AddedUserName = entity.AddedUserName
                MyBase.UpdatedUserName = entity.UpdatedUserName
                Me.MapEntityToCustomProperties(entity)
            End If
        End Sub
        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
            Dim eEMail As EMail = CType(entity, EMail)
            With eEMail
                intEmailId = .EmailId
                strToEmailAddress = .ToEmailAddress
                strCCEmailAddress = .CCEmailAddress
                strBCCEmailAddress = .BCCEmailAddress
                strFromEmailAddress = .FromEmailAddress
                strEmailSubject = .EmailSubject
                strEmailBody = .EmailBody
                bolIsEmailSent = .IsEmailSent
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOEMail "
    Public Class BOEMail
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEMail As New DAEMail
            If oEMail.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEMail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEMail.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oEMail As New DAEMail
            Return MapEntityToProperties(oEMail.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
           Throw New NotImplementedException
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boEMail As New BOEEMail

            If Not IsNothing(Entity) Then
                Dim eEMail As EMail = CType(Entity, EMail)
                With boEMail
                    .AddedDate = eEMail.AddedDate
                    .AddedUserAccountId = eEMail.AddedUserAccountId
                    .UpdatedDate = eEMail.UpdatedDate
                    .UpdatedUserAccountId = eEMail.UpdatedUserAccountId
                    .VersionNo = eEMail.VersionNo
                    .EMailId = eEMail.EMailId
                    .ToEmailAddress = eEMail.ToEmailAddress
                    .CCEmailAddress = eEMail.CCEmailAddress
                    .BCCEmailAddress = eEMail.BCCEmailAddress
                    .FromEmailAddress = eEMail.FromEmailAddress
                    .EmailSubject = eEMail.EmailSubject
                    .EmailBody = eEMail.EmailBody
                    .IsEmailSent = eEMail.IsEmailSent
                End With
            End If

            Return boEMail
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eEMail As New EMail

            If Not IsNothing(Entity) Then
                Dim BoEMail As BOEEMail = CType(Entity, BOEEMail)
                With eEMail
                    .AddedDate = BoEMail.AddedDate
                    .AddedUserAccountId = BoEMail.AddedUserAccountId
                    .UpdatedDate = BoEMail.UpdatedDate
                    .UpdatedUserAccountId = BoEMail.UpdatedUserAccountId
                    .VersionNo = BoEMail.VersionNo
                    .EmailId = BoEMail.EmailId
                    .ToEmailAddress = BoEMail.ToEmailAddress
                    .CCEmailAddress = BoEMail.CCEmailAddress
                    .BCCEmailAddress = BoEMail.BCCEmailAddress
                    .FromEmailAddress = BoEMail.FromEmailAddress
                    .EmailSubject = BoEMail.EmailSubject
                    .EmailBody = BoEMail.EmailBody
                    .IsEmailSent = BoEMail.IsEmailSent
                End With
            End If

            Return eEMail
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal ToEmailAddress As String,
        ByVal CCEmailAddress As String,
        ByVal BCCEmailAddress As String,
        ByVal FromEmailAddress As String,
        ByVal EmailSubject As String,
        ByVal EmailBody As String,
        ByVal IsEmailSent As Boolean,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEMail As New DAEMail
            If oEMail.Add(ToEmailAddress,
                          CCEmailAddress,
                          BCCEmailAddress,
                          FromEmailAddress,
                          EmailSubject,
                          EmailBody,
                          IsEmailSent,
                          UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEMail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEMail.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal EmailId As Integer,
        ByVal ToEmailAddress As String,
        ByVal CCEmailAddress As String,
        ByVal BCCEmailAddress As String,
        ByVal FromEmailAddress As String,
        ByVal EmailSubject As String,
        ByVal EmailBody As String,
        ByVal IsEmailSent As Boolean,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEMail As New DAEMail
            If oEMail.Update(EmailId,
                             ToEmailAddress,
                             CCEmailAddress,
                             BCCEmailAddress,
                             FromEmailAddress,
                             EmailSubject,
                             EmailBody,
                             IsEmailSent,
                             UserAccountId,
                             VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEMail.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEMail.InfoMessage
            End If

            Return boolSeccessed
        End Function
       
#End Region

    End Class
#End Region

#Region " BOEMails "
    Public Class BOEMails
        Inherits BOListBase(Of BOEEMail)
        Public Sub LoadUnsent(Optional ByVal IsEmailSent As Boolean = False)
            LoadFromList(New DAEMail().[LoadEmailsBySentStatus](IsEmailSent))
        End Sub
        Private Sub LoadFromList(ByVal EMails As List(Of EMail))
            If EMails.Count > 0 Then
                For Each eEMail As EMail In EMails
                    Dim bEMail As New BOEEMail
                    bEMail.MapEntityToProperties(eEMail)
                    Me.Add(bEMail)
                Next
            End If
        End Sub
        Friend Function GetByEmailId(ByVal EmailId As Integer) As BOEEMail
            Return Me.SingleOrDefault(Function(r) r.EmailId = EmailId)
        End Function
    End Class
#End Region

End Namespace


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

#Region " BONationality "
    Public Class BONationality
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oNationality As New DANationality
            If oNationality.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oNationality.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oNationality.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oNationality As New DANationality
            Return MapEntityToProperties(oNationality.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eNationality As Nationality = DirectCast(MapPropertiesToEntity(BOEntity), Nationality)
            Dim oNationality As New DANationality

            ds = oNationality.GetDataset(eNationality, PageNo, PageSize)
            MyBase.PageTotal = oNationality.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boNationality As New BOENationality

            If Not IsNothing(Entity) Then
                Dim eNationality As Nationality = CType(Entity, Nationality)
                With boNationality
                    .AddedDate = eNationality.AddedDate
                    .AddedUserAccountId = eNationality.AddedUserAccountId
                    .UpdatedDate = eNationality.UpdatedDate
                    .UpdatedUserAccountId = eNationality.UpdatedUserAccountId
                    .VersionNo = eNationality.VersionNo
                    .NationalityId = eNationality.NationalityId
                    .NationalityName = eNationality.NationalityName
                    .AddedUserName = eNationality.AddedUserName
                    .UpdatedUserName = eNationality.UpdatedUserName
                End With
            End If

            Return boNationality
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eNationality As New Nationality

            If Not IsNothing(Entity) Then
                Dim BoNationality As BOENationality = CType(Entity, BOENationality)
                With eNationality
                    .AddedDate = BoNationality.AddedDate
                    .AddedUserAccountId = BoNationality.AddedUserAccountId
                    .UpdatedDate = BoNationality.UpdatedDate
                    .UpdatedUserAccountId = BoNationality.UpdatedUserAccountId
                    .VersionNo = BoNationality.VersionNo
                    .NationalityId = BoNationality.NationalityId
                    .NationalityName = BoNationality.NationalityName
                End With
            End If

            Return eNationality
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal NationalityName As String, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oNationality As New DANationality
            If oNationality.Add(NationalityName, UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oNationality.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oNationality.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal NationalityId As Integer, _
        ByVal NationalityName As String, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oNationality As New DANationality
            If oNationality.Update(NationalityId, NationalityName, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oNationality.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oNationality.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetList() As DataSet
            Return New DANationality().GetList
        End Function
#End Region


    End Class
#End Region

#Region " BOENationality "
    Public Class BOENationality
        Inherits BOEntityBase


#Region " Public Properties "
        Private intNationalityId As Integer
        Public Property NationalityId() As Integer
            Get
                Return intNationalityId
            End Get
            Set(ByVal value As Integer)
                intNationalityId = value
            End Set
        End Property
        Private strNationalityName As String
        Public Property NationalityName() As String
            Get
                Return strNationalityName
            End Get
            Set(ByVal value As String)
                strNationalityName = value
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
            Dim eNationality As Nationality = CType(entity, Nationality)
            With eNationality
                intNationalityId = .NationalityId
                strNationalityName = .NationalityName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BONationalities "
    Public Class BONationalities
        Inherits BOListBase(Of BOENationality)

        Public Sub Load()
            LoadFromList(New DANationality().[Load]())
        End Sub

        Private Sub LoadFromList(ByVal Nationalities As List(Of Nationality))
            If Nationalities.Count > 0 Then
                For Each eNationality As Nationality In Nationalities
                    Dim bNationality As New BOENationality
                    bNationality.MapEntityToProperties(eNationality)
                    Me.Add(bNationality)
                Next
            End If
        End Sub
        Public Function GetByNationalityId(ByVal Id As Integer) As BOENationality
            Return Me.SingleOrDefault(Function(r) r.NationalityId = Id)
        End Function
    End Class
#End Region


End Namespace


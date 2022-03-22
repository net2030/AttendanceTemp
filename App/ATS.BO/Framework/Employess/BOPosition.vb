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

#Region " BOEPosition "
    <Serializable()> _
    Public Class BOEPosition
        Inherits BOEntityBase

#Region " Properties  "
        Private intPositionId As Integer
        Public Property PositionId As Integer
            Get
                Return intPositionId
            End Get
            Set(ByVal value As Integer)
                intPositionId = value
            End Set
        End Property
        Private strPositionName As String
        Public Property PositionName As String
            Get
                Return strPositionName
            End Get
            Set(ByVal value As String)
                strPositionName = value
            End Set
        End Property
        Private intMCodeId As Integer
        Public Property PositionTypeId() As Integer
            Get
                Return intMCodeId
            End Get
            Set(ByVal value As Integer)
                intMCodeId = value
            End Set
        End Property
        Private intSortIndex As Integer
        Public Property SortIndex() As Integer
            Get
                Return intSortIndex
            End Get
            Set(ByVal value As Integer)
                intSortIndex = value
            End Set
        End Property
        Private bolIsOfficer As Boolean
        Public Property IsOfficer() As Boolean
            Get
                Return bolIsOfficer
            End Get
            Set(ByVal value As Boolean)
                bolIsOfficer = value
            End Set
        End Property
#End Region

#Region " Mapping "
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
            Dim ePosition As Position = CType(entity, Position)
            With ePosition
                intPositionId = .PositionId
                strPositionName = .PositionName
                intMCodeId = .PositionTypeId
                intSortIndex = .SortIndex
                bolIsOfficer = .IsOfficer

            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOPosition "
    Public Class BOPosition
        Inherits BOBase

#Region " Overrides "

        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPosition As New DAPosition
            If oPosition.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oPosition.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPosition.InfoMessage
                MyBase.FieldInError = oPosition.FieldInError
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oPosition As New DAPosition
            Return MapEntityToProperties(oPosition.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim ePosition As Position = DirectCast(MapPropertiesToEntity(BOEntity), Position)
            Dim oPosition As New DAPosition
            ds = oPosition.GetDataset(ePosition, PageNo, PageSize)
            MyBase.PageTotal = oPosition.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ICommonAttributes) As BOEntityBase
            Dim boPosition As New BOEPosition

            If Not IsNothing(Entity) Then
                Dim ePosition As Position = CType(Entity, Position)
                With boPosition
                    .AddedDate = ePosition.AddedDate
                    .AddedUserAccountId = ePosition.AddedUserAccountId
                    .UpdatedDate = ePosition.UpdatedDate
                    .UpdatedUserAccountId = ePosition.UpdatedUserAccountId
                    .VersionNo = ePosition.VersionNo
                    .PositionId = ePosition.PositionId
                    .PositionName = ePosition.PositionName
                    .PositionTypeId = ePosition.PositionTypeId
                    .SortIndex = ePosition.SortIndex
                    .IsOfficer = ePosition.IsOfficer

                    .AddedUserName = ePosition.AddedUserName
                    .UpdatedUserName = ePosition.UpdatedUserName
                End With
            End If

            Return boPosition
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As DataEntityBase
            Dim ePosition As New Position

            If Not IsNothing(Entity) Then
                Dim bPosition As BOEPosition = CType(Entity, BOEPosition)
                With ePosition
                    .AddedDate = bPosition.AddedDate
                    .AddedUserAccountId = bPosition.AddedUserAccountId
                    .UpdatedDate = bPosition.UpdatedDate
                    .UpdatedUserAccountId = bPosition.UpdatedUserAccountId
                    .VersionNo = bPosition.VersionNo
                    .PositionId = bPosition.PositionId
                    .PositionName = bPosition.PositionName
                    .PositionTypeId = bPosition.PositionTypeId
                    .SortIndex = bPosition.SortIndex
                    .IsOfficer = bPosition.IsOfficer
                End With
            End If

            Return ePosition
        End Function

#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal PositionName As String,
        ByVal PositionTypeId As Integer,
        ByVal SortIndex As Integer,
        ByVal IsOfficer As Boolean,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPosition As New DAPosition
            If oPosition.Add(PositionName, PositionTypeId, SortIndex, IsOfficer, UserAccountId) Then
                MyBase.Identity = oPosition.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oPosition.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPosition.InfoMessage
                MyBase.FieldInError = oPosition.FieldInError
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal PositionId As Integer,
        ByVal PositionName As String,
        ByVal PositionTypeId As Integer,
        ByVal SortIndex As Integer,
        ByVal IsOfficer As Boolean,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oPosition As New DAPosition
            If oPosition.Update(PositionId, PositionName, PositionTypeId, SortIndex, IsOfficer, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oPosition.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oPosition.InfoMessage
                MyBase.FieldInError = oPosition.FieldInError
            End If

            Return boolSeccessed
        End Function
        Public Function GetList(ByVal PositionTypeId As Integer) As DataSet
            Return New DAPosition().GetList(PositionTypeId)
        End Function
        Public Function GetPositionTypesList() As DataSet
            Return New DAPosition().GetPositionTypesList()
        End Function
#End Region

    End Class
#End Region

#Region " BOPositionesList "
    <Serializable()> _
    Public Class BOPositionesList
        Inherits BOListBase(Of BOEPosition)
        Public Sub Load()
            LoadFromList(New DAPosition().[Load]())
        End Sub
        Public Sub LoadByPositionType(ByVal PositionTypeId As Integer)
            LoadFromList(New DAPosition().[LoadByPositionType](PositionTypeId))
        End Sub
        Private Sub LoadFromList(ByVal Positiones As List(Of Position))
            If Positiones.Count > 0 Then
                For Each ePosition As Position In Positiones
                    Dim bPosition As New BOEPosition
                    bPosition.MapEntityToProperties(ePosition)
                    Me.Add(bPosition)
                Next
            End If
        End Sub
        Friend Function GetByPositionId(ByVal Id As Integer) As BOEPosition
            Return Me.SingleOrDefault(Function(r) r.PositionId = Id)
        End Function
    End Class
#End Region

End Namespace


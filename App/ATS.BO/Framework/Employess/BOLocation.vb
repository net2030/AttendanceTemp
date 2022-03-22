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

#Region " BOLocation "
    Public Class BOLocation
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oLocation As New DALocation
            If oLocation.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oLocation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oLocation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oLocation As New DALocation
            Return MapEntityToProperties(oLocation.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eLocation As Location = DirectCast(MapPropertiesToEntity(BOEntity), Location)
            Dim oLocation As New DALocation

            ds = oLocation.GetDataset(eLocation, PageNo, PageSize)
            MyBase.PageTotal = oLocation.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boLocation As New BOELocation

            If Not IsNothing(Entity) Then
                Dim eLocation As Location = CType(Entity, Location)
                With boLocation
                    .AddedDate = eLocation.AddedDate
                    .AddedUserAccountId = eLocation.AddedUserAccountId
                    .UpdatedDate = eLocation.UpdatedDate
                    .UpdatedUserAccountId = eLocation.UpdatedUserAccountId
                    .VersionNo = eLocation.VersionNo
                    .LocationId = eLocation.LocationId
                    .LocationName = eLocation.LocationName
                    .AddedUserName = eLocation.AddedUserName
                    .UpdatedUserName = eLocation.UpdatedUserName
                End With
            End If

            Return boLocation
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eLocation As New Location

            If Not IsNothing(Entity) Then
                Dim BoLocation As BOELocation = CType(Entity, BOELocation)
                With eLocation
                    .AddedDate = BoLocation.AddedDate
                    .AddedUserAccountId = BoLocation.AddedUserAccountId
                    .UpdatedDate = BoLocation.UpdatedDate
                    .UpdatedUserAccountId = BoLocation.UpdatedUserAccountId
                    .VersionNo = BoLocation.VersionNo
                    .LocationId = BoLocation.LocationId
                    .LocationName = BoLocation.LocationName
                End With
            End If

            Return eLocation
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal LocationName As String, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oLocation As New DALocation
            If oLocation.Add(LocationName, UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oLocation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oLocation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal LocationId As Integer, _
        ByVal LocationName As String, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oLocation As New DALocation
            If oLocation.Update(LocationId, LocationName, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oLocation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oLocation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetList() As DataSet
            Return New DALocation().GetList
        End Function
#End Region


    End Class
#End Region

#Region " BOELocation "
    Public Class BOELocation
        Inherits BOEntityBase

#Region " Enumerations "

#Region " Filter Varibales "
        Private intSelectFilter As FilterSelect
        Private intWhereFilter As FilterWhere
        Private intOrderByFilter As FilterOrderBy
#End Region
#Region " Class Enumerations (will change for each class)  "
        Public Enum FilterSelect As Integer
            None = 0
            All = 1
        End Enum
        Public Enum FilterWhere As Integer
            None = 0
            PrimaryKey = 1
        End Enum
        Public Enum FilterOrderBy As Integer
            None = 0
        End Enum
#End Region
#Region " Filter Properties  "
        Public Property SelectFilter() As FilterSelect
            Get
                Return intSelectFilter
            End Get
            Set(ByVal Value As FilterSelect)
                intSelectFilter = Value
            End Set
        End Property
        Public Property WhereFilter() As FilterWhere
            Get
                Return intWhereFilter
            End Get
            Set(ByVal Value As FilterWhere)
                intWhereFilter = Value
            End Set
        End Property
        Public Property OrderByFilter() As FilterOrderBy
            Get
                Return intOrderByFilter
            End Get
            Set(ByVal Value As FilterOrderBy)
                intOrderByFilter = Value
            End Set
        End Property
#End Region

#End Region

#Region " Private Variables  "
        Private intLocationId As Integer
        Private strLocationName As String
#End Region
#Region " Public Properties "
        Public Property LocationId() As Integer
            Get
                Return intLocationId
            End Get
            Set(ByVal value As Integer)
                intLocationId = value
            End Set
        End Property
        Public Property LocationName() As String
            Get
                Return strLocationName
            End Get
            Set(ByVal value As String)
                strLocationName = value
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
            Dim eLocation As Location = CType(entity, Location)
            With eLocation
                intLocationId = .LocationId
                strLocationName = .LocationName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOLocations "
    Public Class BOLocations
        Inherits BOListBase(Of BOELocation)

        Public Sub Load()
            LoadFromList(New DALocation().[Load]())
        End Sub

        Private Sub LoadFromList(ByVal Locations As List(Of Location))
            If Locations.Count > 0 Then
                For Each eLocation As Location In Locations
                    Dim bLocation As New BOELocation
                    bLocation.MapEntityToProperties(eLocation)
                    Me.Add(bLocation)
                Next
            End If
        End Sub
        Friend Function GetByLocationId(ByVal Id As Integer) As BOELocation
            Return Me.SingleOrDefault(Function(r) r.LocationId = Id)
        End Function
    End Class
#End Region


End Namespace


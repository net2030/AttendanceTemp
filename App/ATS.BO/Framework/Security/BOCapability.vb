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

#Region " BOCapability "

    Public Class BOCapability
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oCapability As New DACapability
            If oCapability.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oCapability.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oCapability.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oCapability As New DACapability
            Return MapEntityToProperties(oCapability.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eCapability As Capability = DirectCast(MapPropertiesToEntity(BOEntity), Capability)
            Dim oCapability As New DACapability
            ds = oCapability.GetDataset(eCapability, PageNo, PageSize)
            MyBase.PageTotal = oCapability.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boCapability As New BOECapability

            If Not IsNothing(Entity) Then
                Dim eCapability As Capability = CType(Entity, Capability)
                With BOCapability
                    .AddedDate = eCapability.AddedDate
                    .AddedUserAccountId = eCapability.AddedUserAccountId
                    .UpdatedDate = eCapability.UpdatedDate
                    .UpdatedUserAccountId = eCapability.UpdatedUserAccountId
                    .VersionNo = eCapability.VersionNo
                    .CapabilityId = eCapability.CapabilityId
                    .CapabilityName = eCapability.CapabilityName
                    .MenuItemId = eCapability.MenuItemId
                    .AccessType = CType(eCapability.AccessType, BOECapability.AccessTypeEnum)
                End With
            End If

            Return boCapability
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eCapability As New Capability

            If Not IsNothing(Entity) Then
                Dim BoCapability As BOECapability = CType(Entity, BOECapability)
                With eCapability
                    .AddedDate = BoCapability.AddedDate
                    .AddedUserAccountId = BoCapability.AddedUserAccountId
                    .UpdatedDate = BoCapability.UpdatedDate
                    .UpdatedUserAccountId = BoCapability.UpdatedUserAccountId
                    .VersionNo = BoCapability.VersionNo
                    .CapabilityId = BoCapability.CapabilityId
                    .CapabilityName = BoCapability.CapabilityName
                    .MenuItemId = BoCapability.MenuItemId

                    .AccessType = CType(BoCapability.AccessType, Capability.AccessTypeEnum)
                    .SelectFilter = CType(BoCapability.SelectFilter, Capability.FilterSelect)
                    .WhereFilter = CType(BoCapability.WhereFilter, Capability.FilterWhere)
                    .OrderByFilter = CType(BoCapability.OrderByFilter, Capability.FilterOrderBy)
                End With
            End If

            Return eCapability
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal CapabilityName As String, _
        ByVal MenuItemId As Integer, _
        ByVal AccessType As Integer, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oCapability As New DACapability
            If oCapability.Add(CapabilityName, MenuItemId, AccessType, AddedUserAccountId) Then
                MyBase.Identity = oCapability.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oCapability.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oCapability.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal CapabilityId As Integer, _
        ByVal CapabilityName As String, _
        ByVal MenuItemId As Integer, _
        ByVal AccessType As Integer, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oCapability As New DACapability
            If oCapability.Update(CapabilityId, CapabilityName, MenuItemId, AccessType, UpdatedUserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oCapability.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oCapability.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class

#End Region

#Region " BOECapability "
    <Serializable()> _
    Public Class BOECapability
        Inherits BOEntityBase

        Public Function Load(ByVal Id As Integer) As Boolean
            Dim Capability As Capability = New DACapability().[Find](Id)
            MapEntityToProperties(Capability)
            Return True
        End Function


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

        Public Enum AccessTypeEnum As Integer
            ReadOnlyEdit = 0
            Read_Only = 1
            Edit = 2
        End Enum

#End Region

#Region " Local Variables  "
        Private intCapabilityId As Integer
        Private strCapabilityName As String
        Private intMenuItemId As Integer
        Private intAccessType As AccessTypeEnum
#End Region

#Region "  Properties  "
        Public Property CapabilityId() As Integer
            Get
                Return intCapabilityId
            End Get
            Set(ByVal value As Integer)
                intCapabilityId = value
            End Set
        End Property
        Public Property CapabilityName() As String
            Get
                Return strCapabilityName
            End Get
            Set(ByVal value As String)
                strCapabilityName = value
            End Set
        End Property
        Public Property MenuItemId() As Integer
            Get
                Return intMenuItemId
            End Get
            Set(ByVal value As Integer)
                intMenuItemId = value
            End Set
        End Property
        ''' <summary>
        ''' what types of access should be allowed for this capability
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AccessType() As AccessTypeEnum
            Get
                Return intAccessType
            End Get
            Set(ByVal value As AccessTypeEnum)
                intAccessType = value
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
            Dim eCapability As Capability = CType(entity, Capability)
            With eCapability
                intCapabilityId = .CapabilityId
                strCapabilityName = .CapabilityName
                intMenuItemId = .MenuItemId
                intAccessType = CType(.AccessType, BOECapability.AccessTypeEnum)
                'oRoleUserAccounts.Load(.eCapability)
            End With
        End Sub
#End Region

    End Class

#End Region

#Region " BOCapabilities "
    Public Class BOCapabilities
        Inherits BOListBase(Of BOECapability)

#Region "Overrides"
        Public Sub Load()
            LoadFromList(New DACapability().[Load]())
        End Sub
#End Region

#Region "Private Methods"
        Private Sub LoadFromList(ByVal Capabilities As List(Of Capability))
            If Capabilities.Count > 0 Then
                For Each eCapability As Capability In Capabilities
                    Dim newCapability As New BOECapability()
                    newCapability.MapEntityToProperties(eCapability)
                    Me.Add(newCapability)
                Next
            End If
        End Sub
#End Region

#Region "Public Methods"

        ''' <summary>
        ''' Returns a Capability with the matching name.  If it is not found then null is returned.
        ''' </summary>
        ''' <param name="capabilityName"></param>
        ''' <returns></returns>
        Public Function GetByName(ByVal capabilityName As String) As BOECapability
            Return Me.SingleOrDefault(Function(c) c.CapabilityName = capabilityName)
        End Function
        Public Function GetByMenuItemId(ByVal MenuItemId As Integer) As IEnumerable(Of BOECapability)
            Return From c In Me Where c.MenuItemId = MenuItemId Order By c.CapabilityName Select c



        End Function
#End Region

    End Class

#End Region

End Namespace


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

#Region " BORoleCapability "
    Public Class BORoleCapability
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRoleCapability As New DARoleCapability
            If oRoleCapability.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRoleCapability.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRoleCapability.InfoMessage
                MyBase.FieldInError = oRoleCapability.FieldInError
            End If

            Return boolSeccessed
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oRoleCapability As New DARoleCapability
            Return MapEntityToProperties(oRoleCapability.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eRoleCapability As RoleCapability = DirectCast(MapPropertiesToEntity(BOEntity), RoleCapability)
            Dim oRole As New DARoleCapability
            ds = oRole.GetDataset(eRoleCapability, PageNo, PageSize)
            MyBase.PageTotal = oRole.PageTotal

            Return ds
        End Function


        Protected Overrides Function MapEntityToProperties( _
        ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boRoleCapability As New BOERoleCapability

            If Not IsNothing(Entity) Then
                Dim eRoleCapability As RoleCapability = CType(Entity, RoleCapability)
                With boRoleCapability
                    .AddedDate = eRoleCapability.AddedDate
                    .AddedUserAccountId = eRoleCapability.AddedUserAccountId
                    .UpdatedDate = eRoleCapability.UpdatedDate
                    .UpdatedUserAccountId = eRoleCapability.UpdatedUserAccountId
                    .VersionNo = eRoleCapability.VersionNo
                    .RoleCapabilityId = eRoleCapability.RoleCapabilityId
                    .RoleId = eRoleCapability.RoleId
                    .CapabilityId = eRoleCapability.CapabilityId
                    .AccessFlag = CType(eRoleCapability.AccessFlag, BOERoleCapability.AccessFlagEnum)
                    .AddedUserName = eRoleCapability.AddedUserName
                    .UpdatedUserName = eRoleCapability.UpdatedUserName
                End With
            End If

            Return boRoleCapability
        End Function

        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eRoleCapability As New RoleCapability

            If Not IsNothing(Entity) Then
                Dim BoRoleCapability As BOERoleCapability = CType(Entity, BOERoleCapability)
                With eRoleCapability
                    .AddedDate = BoRoleCapability.AddedDate
                    .AddedUserAccountId = BoRoleCapability.AddedUserAccountId
                    .UpdatedDate = BoRoleCapability.UpdatedDate
                    .UpdatedUserAccountId = BoRoleCapability.UpdatedUserAccountId
                    .VersionNo = BoRoleCapability.VersionNo
                    .RoleCapabilityId = BoRoleCapability.RoleCapabilityId
                    .RoleId = BoRoleCapability.RoleId
                    .CapabilityId = BoRoleCapability.CapabilityId

                    .AccessFlag = CType(BoRoleCapability.AccessFlag, RoleCapability.AccessFlagEnum)
                    .SelectFilter = CType(BoRoleCapability.SelectFilter, RoleCapability.FilterSelect)
                    .WhereFilter = CType(BoRoleCapability.WhereFilter, RoleCapability.FilterWhere)
                    .OrderByFilter = CType(BoRoleCapability.OrderByFilter, RoleCapability.FilterOrderBy)
                End With
            End If

            Return eRoleCapability
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal RoleId As Integer, _
        ByVal CapabilityId As Integer, _
        ByVal AccessFlag As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARoleCapability
            If oRole.Add(RoleId, CapabilityId, AccessFlag, UserAccountId) Then
                MyBase.Identity = oRole.Identity
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal RoleCapabilityId As Integer, _
        ByVal RoleId As Integer, _
        ByVal CapabilityId As Integer, _
        ByVal AccessFlag As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oRole As New DARoleCapability
            If oRole.Update(RoleCapabilityId, RoleId, CapabilityId, AccessFlag, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oRole.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oRole.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetRoleCapabilitiesDataset(ByVal RoleId As Integer) As DataSet
            Return New DARoleCapability().GetRoleCapabilitiesDataset(RoleId)
        End Function
        Public Function GetRoleCapabilities(ByVal RoleId As Integer) As DataSet
            Return New DARoleCapability().GetRoleCapabilities(RoleId)
        End Function
#End Region


    End Class
#End Region

#Region " BOERoleCapability "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BOERoleCapability
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
            UserAccountId = 2
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

        Public Enum AccessFlagEnum
            None = 0
            Read_Only = 1
            Edit = 2
        End Enum
#End Region

#Region " Constructors "
        Public Sub New()
            oCapability = New BOECapability
        End Sub
#End Region

#Region " Local Variables  "
        Private intRoleCapabilityId As Integer
        Private intRoleId As Integer
        Private intCapabilityId As Integer
        Private intAccessFlag As AccessFlagEnum
        Private oCapability As BOECapability
#End Region
#Region "  Properties "
        Public Property Capability() As BOECapability
            Get
                Return oCapability
            End Get
            Set(ByVal value As BOECapability)
                oCapability = value
            End Set
        End Property
        Public Property RoleCapabilityId() As Integer
            Get
                Return intRoleCapabilityId
            End Get
            Set(ByVal value As Integer)
                intRoleCapabilityId = value
            End Set
        End Property
        Public Property RoleId() As Integer
            Get
                Return intRoleId
            End Get
            Set(ByVal value As Integer)
                intRoleId = value
            End Set
        End Property
        Public Property CapabilityId() As Integer
            Get
                Return intCapabilityId
            End Get
            Set(ByVal value As Integer)
                intCapabilityId = value
            End Set
        End Property
        Public Property AccessFlag() As AccessFlagEnum
            Get
                Return intAccessFlag
            End Get
            Set(ByVal value As AccessFlagEnum)
                intAccessFlag = value
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
            Dim eRoleCapability As RoleCapability = CType(entity, RoleCapability)
            With eRoleCapability
                intRoleCapabilityId = .RoleCapabilityId
                intRoleId = .RoleId
                intCapabilityId = .CapabilityId
                intAccessFlag = CType(.AccessFlag, AccessFlagEnum)

                oCapability.Load(.CapabilityId)
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BORoleCapabilities "
    Public Class BORoleCapabilities
        Inherits BOListBase(Of BOERoleCapability)

        Public Sub Load(ByVal RoleId As Integer)
            LoadFromList(New DARoleCapability().[LoadByRoleId](RoleId))
        End Sub

        Private Sub LoadFromList(ByVal RoleCapabilities As List(Of RoleCapability))
            If RoleCapabilities.Count > 0 Then
                For Each eRoleCapability As RoleCapability In RoleCapabilities
                    Dim bRoleCapability As New BOERoleCapability
                    bRoleCapability.MapEntityToProperties(eRoleCapability)
                    Me.Add(bRoleCapability)
                Next
            End If
        End Sub

        ''' <summary>
        ''' Returns all the Role\Capabilitis for the specificed menuItemId.
        ''' </summary>
        ''' <param name="menuItemId"></param>
        ''' <returns></returns>
        Friend Function GetByMenuItemId(ByVal menuItemId As Integer) As IEnumerable(Of BOERoleCapability)
            Return From rc In Me Where rc.Capability.MenuItemId = menuItemId Select rc
        End Function

        ''' <summary>
        ''' Get from this instance the object with the specified capability.
        ''' </summary>
        ''' <param name="capabilityId"></param>
        ''' <returns></returns>
        Public Function GetByCapabilityID(ByVal CapabilityId As Integer) As BOERoleCapability
            Return Me.SingleOrDefault(Function(rc) rc.Capability.CapabilityId = CapabilityId)
        End Function
        Public Function GetRoleCapability(ByVal RoleCapabilityId As Integer) As BOERoleCapability
            Return Me.SingleOrDefault(Function(u) u.RoleCapabilityId = RoleCapabilityId)
        End Function
    End Class
#End Region

End Namespace


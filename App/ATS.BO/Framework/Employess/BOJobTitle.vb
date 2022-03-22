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

#Region " BOJobTitle "
    Public Class BOJobTitle
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oJobTitle As New DAJobTitle
            If oJobTitle.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oJobTitle.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oJobTitle.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oJobTitle As New DAJobTitle
            Return MapEntityToProperties(oJobTitle.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eJobTitle As JobTitle = DirectCast(MapPropertiesToEntity(BOEntity), JobTitle)
            Dim oJobTitle As New DAJobTitle

            ds = oJobTitle.GetDataset(eJobTitle, PageNo, PageSize)
            MyBase.PageTotal = oJobTitle.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boJobTitle As New BOEJobTitle

            If Not IsNothing(Entity) Then
                Dim eJobTitle As JobTitle = CType(Entity, JobTitle)
                With boJobTitle
                    .AddedDate = eJobTitle.AddedDate
                    .AddedUserAccountId = eJobTitle.AddedUserAccountId
                    .UpdatedDate = eJobTitle.UpdatedDate
                    .UpdatedUserAccountId = eJobTitle.UpdatedUserAccountId
                    .VersionNo = eJobTitle.VersionNo
                    .JobTitleId = eJobTitle.JobTitleId
                    .JobTitleName = eJobTitle.JobTitleName
                    .AddedUserName = eJobTitle.AddedUserName
                    .UpdatedUserName = eJobTitle.UpdatedUserName
                End With
            End If

            Return boJobTitle
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eJobTitle As New JobTitle

            If Not IsNothing(Entity) Then
                Dim BoJobTitle As BOEJobTitle = CType(Entity, BOEJobTitle)
                With eJobTitle
                    .AddedDate = BoJobTitle.AddedDate
                    .AddedUserAccountId = BoJobTitle.AddedUserAccountId
                    .UpdatedDate = BoJobTitle.UpdatedDate
                    .UpdatedUserAccountId = BoJobTitle.UpdatedUserAccountId
                    .VersionNo = BoJobTitle.VersionNo
                    .JobTitleId = BoJobTitle.JobTitleId
                    .JobTitleName = BoJobTitle.JobTitleName
                End With
            End If

            Return eJobTitle
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal JobTitleName As String, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oJobTitle As New DAJobTitle
            If oJobTitle.Add(JobTitleName, UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oJobTitle.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oJobTitle.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal JobTitleId As Integer, _
        ByVal JobTitleName As String, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oJobTitle As New DAJobTitle
            If oJobTitle.Update(JobTitleId, JobTitleName, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oJobTitle.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oJobTitle.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetList() As DataSet
            Return New DAJobTitle().GetList
        End Function
#End Region


    End Class
#End Region

#Region " BOEJobTitle "
    Public Class BOEJobTitle
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
        Private intJobTitleId As Integer
        Private strJobTitleName As String
#End Region
#Region " Public Properties "
        Public Property JobTitleId() As Integer
            Get
                Return intJobTitleId
            End Get
            Set(ByVal value As Integer)
                intJobTitleId = value
            End Set
        End Property
        Public Property JobTitleName() As String
            Get
                Return strJobTitleName
            End Get
            Set(ByVal value As String)
                strJobTitleName = value
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
            Dim eJobTitle As JobTitle = CType(entity, JobTitle)
            With eJobTitle
                intJobTitleId = .JobTitleId
                strJobTitleName = .JobTitleName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOJobTitles "
    Public Class BOJobTitles
        Inherits BOListBase(Of BOEJobTitle)

        Public Sub Load()
            LoadFromList(New DAJobTitle().[Load]())
        End Sub

        Private Sub LoadFromList(ByVal JobTitles As List(Of JobTitle))
            If JobTitles.Count > 0 Then
                For Each eJobTitle As JobTitle In JobTitles
                    Dim bJobTitle As New BOEJobTitle
                    bJobTitle.MapEntityToProperties(eJobTitle)
                    Me.Add(bJobTitle)
                Next
            End If
        End Sub
        Friend Function GetByJobTitleId(ByVal Id As Integer) As BOEJobTitle
            Return Me.SingleOrDefault(Function(r) r.JobTitleId = Id)
        End Function
    End Class
#End Region


End Namespace


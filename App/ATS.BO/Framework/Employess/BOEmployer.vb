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

    '#Region " BOEEmployer "
    '    Public Class BOEEmployer
    '        Inherits BOEntityBase

    '#Region " Constructor "
    '        Public Sub New()
    '        End Sub
    '#End Region

    '#Region " Private Variables  "
    '        Private intEmployerId As Integer
    '        Private strEmployerName As String
    '#End Region
    '#Region " Public Properties "
    '        Public Property EmployerId() As Integer
    '            Get
    '                Return intEmployerId
    '            End Get
    '            Set(ByVal value As Integer)
    '                intEmployerId = value
    '            End Set
    '        End Property

    '        Public Property EmployerName() As String
    '            Get
    '                Return strEmployerName
    '            End Get
    '            Set(ByVal value As String)
    '                strEmployerName = value
    '            End Set
    '        End Property
    '#End Region

    '#Region " Properties Mapping "
    '        Public Sub MapEntityToProperties(ByVal entity As ICommonAttributes)
    '            If Not IsNothing(entity) Then
    '                MyBase.AddedDate = entity.AddedDate
    '                MyBase.AddedUserAccountId = entity.AddedUserAccountId
    '                MyBase.UpdatedDate = entity.UpdatedDate
    '                MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
    '                MyBase.VersionNo = entity.VersionNo
    '                MyBase.AddedUserName = entity.AddedUserName
    '                MyBase.UpdatedUserName = entity.UpdatedUserName
    '                Me.MapEntityToCustomProperties(entity)
    '            End If
    '        End Sub
    '        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
    '            Dim eEmployer As Employer = CType(entity, Employer)
    '            With eEmployer
    '                intEmployerId = .EmployerId
    '                strEmployerName = .EmployerName
    '            End With
    '        End Sub
    '#End Region

    '    End Class
    '#End Region

    '#Region " BOEmployer "
    '    Public Class BOEmployer
    '        Inherits BOBase

    '#Region " Overrides "
    '        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
    '            Dim boolSeccessed As Boolean = False

    '            Dim oEmployer As New DAEmployer
    '            If oEmployer.Delete(Id) Then
    '                boolSeccessed = True
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            Else
    '                boolSeccessed = False
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            End If

    '            Return boolSeccessed
    '        End Function
    '        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
    '            Dim oEmployer As New DAEmployer
    '            Return MapEntityToProperties(oEmployer.Find(Id))
    '        End Function

    '        Public Overrides Function GetDataset( _
    '        ByVal BOEntity As BOEntityBase, _
    '        Optional ByVal PageNo As Integer = 1, _
    '        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
    '            Throw New NotImplementedException
    '        End Function
    '        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
    '            Dim bEmployer As New BOEEmployer

    '            If Not IsNothing(Entity) Then
    '                Dim eEmployer As Employer = CType(Entity, Employer)
    '                With bEmployer
    '                    .AddedDate = eEmployer.AddedDate
    '                    .AddedUserAccountId = eEmployer.AddedUserAccountId
    '                    .UpdatedDate = eEmployer.UpdatedDate
    '                    .UpdatedUserAccountId = eEmployer.UpdatedUserAccountId
    '                    .VersionNo = eEmployer.VersionNo

    '                    .EmployerId = eEmployer.EmployerId
    '                    .EmployerName = eEmployer.EmployerName

    '                    .AddedUserName = eEmployer.AddedUserName
    '                    .UpdatedUserName = eEmployer.UpdatedUserName
    '                End With
    '            End If

    '            Return bEmployer
    '        End Function
    '        Protected Overrides Function MapPropertiesToEntity( _
    '        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
    '            Dim eEmployer As New Employer

    '            If Not IsNothing(Entity) Then
    '                Dim bContract As BOEEmployer = CType(Entity, BOEEmployer)
    '                With eEmployer
    '                    .AddedDate = bContract.AddedDate
    '                    .AddedUserAccountId = bContract.AddedUserAccountId
    '                    .UpdatedDate = bContract.UpdatedDate
    '                    .UpdatedUserAccountId = bContract.UpdatedUserAccountId
    '                    .VersionNo = bContract.VersionNo

    '                    .EmployerId = bContract.EmployerId
    '                    .EmployerName = bContract.EmployerName
    '                End With
    '            End If

    '            Return eEmployer
    '        End Function
    '#End Region

    '#Region " Public Methods "
    '        Public Function Add(ByVal EmployerName As String, ByVal UserAccountId As Integer) As Boolean
    '            Dim boolSeccessed As Boolean = False

    '            Dim oEmployer As New DAEmployer
    '            If oEmployer.Add(EmployerName, UserAccountId) Then
    '                boolSeccessed = True
    '                MyBase.Identity = oEmployer.Identity
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            Else
    '                boolSeccessed = False
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            End If

    '            Return boolSeccessed
    '        End Function
    '        Public Function Update(
    '        ByVal EmployerId As Integer,
    '        ByVal EmployerName As String,
    '        ByVal UserAccountId As Integer,
    '        ByVal VersionNo As Byte()) As Boolean
    '            Dim boolSeccessed As Boolean = False

    '            Dim oEmployer As New DAEmployer
    '            If oEmployer.Update(EmployerId, EmployerName, UserAccountId, VersionNo) Then
    '                boolSeccessed = True
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            Else
    '                boolSeccessed = False
    '                MyBase.InfoMessage = oEmployer.InfoMessage
    '            End If

    '            Return boolSeccessed
    '        End Function
    '        Public Function GetEmployerDataset(Optional ByVal PageNo As Integer = 1,
    '        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
    '            Dim ds As DataSet = Nothing

    '            Dim oEmployer As New DAEmployer
    '            ds = oEmployer.GetEmployerDataset(PageNo, PageSize)
    '            MyBase.PageTotal = oEmployer.PageTotal

    '            Return ds
    '        End Function
    '        Public Function GetList() As DataSet
    '            Return New DAEmployer().GetEmployerListDataset
    '        End Function
    '#End Region

    '    End Class
    '#End Region


#Region " BOEEmployer "
    Public Class BOEEmployer
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intEmployerId As Integer
        Private strEmployerName As String
        Private strEmployerNameEN As String
#End Region
#Region " Public Properties "
        Public Property EmployerId() As Integer
            Get
                Return intEmployerId
            End Get
            Set(ByVal value As Integer)
                intEmployerId = value
            End Set
        End Property

        Public Property EmployerName() As String
            Get
                Return strEmployerName
            End Get
            Set(ByVal value As String)
                strEmployerName = value
            End Set
        End Property

        Public Property EmployerNameEN() As String
            Get
                Return strEmployerNameEN
            End Get
            Set(ByVal value As String)
                strEmployerNameEN = value
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
            Dim eEmployer As Employer = CType(entity, Employer)
            With eEmployer
                intEmployerId = .EmployerId
                strEmployerName = .EmployerName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOEmployer "
    Public Class BOEmployer
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEmployer As New DAEmployer
            If oEmployer.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEmployer.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployer.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oEmployer As New DAEmployer
            Return MapEntityToProperties(oEmployer.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bEmployer As New BOEEmployer

            If Not IsNothing(Entity) Then
                Dim eEmployer As Employer = CType(Entity, Employer)
                With bEmployer
                    .AddedDate = eEmployer.AddedDate
                    .AddedUserAccountId = eEmployer.AddedUserAccountId
                    .UpdatedDate = eEmployer.UpdatedDate
                    .UpdatedUserAccountId = eEmployer.UpdatedUserAccountId
                    .VersionNo = eEmployer.VersionNo

                    .EmployerId = eEmployer.EmployerId
                    .EmployerName = eEmployer.EmployerName
                    .EmployerNameEN = eEmployer.EmployerNameEN

                    .AddedUserName = eEmployer.AddedUserName
                    .UpdatedUserName = eEmployer.UpdatedUserName
                End With
            End If

            Return bEmployer
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eEmployer As New Employer

            If Not IsNothing(Entity) Then
                Dim bGatepass As BOEEmployer = CType(Entity, BOEEmployer)
                With eEmployer
                    .AddedDate = bGatepass.AddedDate
                    .AddedUserAccountId = bGatepass.AddedUserAccountId
                    .UpdatedDate = bGatepass.UpdatedDate
                    .UpdatedUserAccountId = bGatepass.UpdatedUserAccountId
                    .VersionNo = bGatepass.VersionNo

                    .EmployerId = bGatepass.EmployerId
                    .EmployerName = bGatepass.EmployerName
                    .EmployerNameEN = bGatepass.EmployerNameEN
                End With
            End If

            Return eEmployer
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal EmployerName As String,
                            ByVal UserAccountId As Integer, Optional ByVal EmployerNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEmployer As New DAEmployer
            If oEmployer.Add(EmployerName, UserAccountId, EmployerNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oEmployer.Identity
                MyBase.InfoMessage = oEmployer.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployer.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal EmployerId As Integer,
        ByVal EmployerName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal EmployerNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEmployer As New DAEmployer
            If oEmployer.Update(EmployerId, EmployerName, UserAccountId, VersionNo, EmployerNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEmployer.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployer.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetEmployersDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployer As New DAEmployer
            ds = oEmployer.GetEmployersDataset(PageNo, PageSize)
            MyBase.PageTotal = oEmployer.PageTotal

            Return ds
        End Function
        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAEmployer().GetEmployersListDataset(Lang)
        End Function
#End Region

    End Class
#End Region

End Namespace


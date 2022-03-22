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

    


#Region " BOEContractType "
    Public Class BOEContractType
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intContractTypeId As Integer
        Private strContractTypeName As String
        Private strContractTypeNameEN As String
#End Region
#Region " Public Properties "
        Public Property ContractTypeId() As Integer
            Get
                Return intContractTypeId
            End Get
            Set(ByVal value As Integer)
                intContractTypeId = value
            End Set
        End Property

        Public Property ContractTypeName() As String
            Get
                Return strContractTypeName
            End Get
            Set(ByVal value As String)
                strContractTypeName = value
            End Set
        End Property

        Public Property ContractTypeNameEN() As String
            Get
                Return strContractTypeNameEN
            End Get
            Set(ByVal value As String)
                strContractTypeNameEN = value
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
            Dim eContractType As ContractType = CType(entity, ContractType)
            With eContractType
                intContractTypeId = .ContractTypeId
                strContractTypeName = .ContractTypeName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOContractType "
    Public Class BOContractType
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oContractType As New DAContractType
            If oContractType.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oContractType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oContractType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oContractType As New DAContractType
            Return MapEntityToProperties(oContractType.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bContractType As New BOEContractType

            If Not IsNothing(Entity) Then
                Dim eContractType As ContractType = CType(Entity, ContractType)
                With bContractType
                    .AddedDate = eContractType.AddedDate
                    .AddedUserAccountId = eContractType.AddedUserAccountId
                    .UpdatedDate = eContractType.UpdatedDate
                    .UpdatedUserAccountId = eContractType.UpdatedUserAccountId
                    .VersionNo = eContractType.VersionNo

                    .ContractTypeId = eContractType.ContractTypeId
                    .ContractTypeName = eContractType.ContractTypeName
                    .ContractTypeNameEN = eContractType.ContractTypeNameEN

                    .AddedUserName = eContractType.AddedUserName
                    .UpdatedUserName = eContractType.UpdatedUserName
                End With
            End If

            Return bContractType
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eContractType As New ContractType

            If Not IsNothing(Entity) Then
                Dim bGatepass As BOEContractType = CType(Entity, BOEContractType)
                With eContractType
                    .AddedDate = bGatepass.AddedDate
                    .AddedUserAccountId = bGatepass.AddedUserAccountId
                    .UpdatedDate = bGatepass.UpdatedDate
                    .UpdatedUserAccountId = bGatepass.UpdatedUserAccountId
                    .VersionNo = bGatepass.VersionNo

                    .ContractTypeId = bGatepass.ContractTypeId
                    .ContractTypeName = bGatepass.ContractTypeName
                    .ContractTypeNameEN = bGatepass.ContractTypeNameEN
                End With
            End If

            Return eContractType
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal ContractTypeName As String,
                            ByVal UserAccountId As Integer, Optional ByVal ContractTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oContractType As New DAContractType
            If oContractType.Add(ContractTypeName, UserAccountId, ContractTypeNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oContractType.Identity
                MyBase.InfoMessage = oContractType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oContractType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal ContractTypeId As Integer,
        ByVal ContractTypeName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal ContractTypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oContractType As New DAContractType
            If oContractType.Update(ContractTypeId, ContractTypeName, UserAccountId, VersionNo, ContractTypeNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oContractType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oContractType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetContractTypesDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oContractType As New DAContractType
            ds = oContractType.GetContractTypesDataset(PageNo, PageSize)
            MyBase.PageTotal = oContractType.PageTotal

            Return ds
        End Function
        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAContractType().GetContractTypesListDataset(Lang)
        End Function
#End Region

    End Class
#End Region

End Namespace


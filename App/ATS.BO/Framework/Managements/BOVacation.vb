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


#Region " BOVacation "
    Public Class BOVacation
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation
            If oVacation.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oVacation As New DAVacation
            Return MapEntityToProperties(oVacation.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Public Function GetVacationsDataset( _
        ByVal UserAccountId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationsDataset(UserAccountId, PageNo, PageSize)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function
        Public Function GetVacationsByDepartmentIdDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationsByDepartmentIdDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function
        Public Function GetVacationsByEffectiveDateDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationsByEffectiveDateDataset(UserAccountId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function
        Public Function GetVacationsByDateExpireDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationsByDateExpireDataset(UserAccountId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function
        Public Function GetVacationsByEmployeeIdDataset(
        ByVal UserAccountId As Integer,
        ByVal EmployeeId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationsByEmployeeIdDataset(UserAccountId, EmployeeId, PageNo, PageSize, Lang)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bVacation As New BOEVacation

            If Not IsNothing(Entity) Then
                Dim eVacation As Vacation = CType(Entity, Vacation)
                With bVacation
                    .AddedDate = eVacation.AddedDate
                    .AddedUserAccountId = eVacation.AddedUserAccountId
                    .UpdatedDate = eVacation.UpdatedDate
                    .UpdatedUserAccountId = eVacation.UpdatedUserAccountId
                    .VersionNo = eVacation.VersionNo

                    .VacationId = eVacation.VacationId
                    .EmployeeId = eVacation.EmployeeId
                    .TypeId = eVacation.TypeId
                    .AltEmployeeId = eVacation.AltEmployeeId
                    .EffectiveDate = eVacation.EffectiveDate
                    .DateExpire = eVacation.DateExpire
                    .DateOfReturn = eVacation.DateOfReturn
                    .Note = eVacation.Note
                    .Address = eVacation.Address
                    .Contact = eVacation.Contact

                    .AddedUserName = eVacation.AddedUserName
                    .UpdatedUserName = eVacation.UpdatedUserName
                End With
            End If

            Return bVacation
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eVacation As New Vacation

            If Not IsNothing(Entity) Then
                Dim bVacation As BOEVacation = CType(Entity, BOEVacation)
                With eVacation
                    .AddedDate = bVacation.AddedDate
                    .AddedUserAccountId = bVacation.AddedUserAccountId
                    .UpdatedDate = bVacation.UpdatedDate
                    .UpdatedUserAccountId = bVacation.UpdatedUserAccountId
                    .VersionNo = bVacation.VersionNo

                    .VacationId = bVacation.VacationId
                    .EmployeeId = bVacation.EmployeeId
                    .TypeId = bVacation.TypeId
                    .AltEmployeeId = bVacation.AltEmployeeId
                    .EffectiveDate = bVacation.EffectiveDate
                    .DateExpire = bVacation.DateExpire
                    .DateOfReturn = bVacation.DateOfReturn
                    .Note = bVacation.Note

                    .Address = bVacation.Address
                    .Contact = bVacation.Contact
                End With
            End If

            Return eVacation
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal EmployeeId As Integer, _
        ByVal TypeId As Integer, _
        ByVal AltEmployeeId As Integer, _
        ByVal EffectiveDate As Date, _
        ByVal DateExpire As Date, _
        ByVal DateOfReturn As Date, _
        ByVal Note As String, _
        ByVal Address As String, _
        ByVal Contact As String, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation
            If oVacation.Add(EmployeeId,
                             TypeId,
                             AltEmployeeId,
                             EffectiveDate,
                             DateExpire,
                             DateOfReturn,
                             Note,
                             Address,
                             Contact,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oVacation.Identity
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal VacationId As Integer, _
        ByVal EmployeeId As Integer, _
        ByVal TypeId As Integer, _
        ByVal AltEmployeeId As Integer, _
        ByVal EffectiveDate As Date, _
        ByVal DateExpire As Date, _
        ByVal DateOfReturn As Date, _
        ByVal Note As String, _
        ByVal Address As String, _
        ByVal Contact As String, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation
            If oVacation.Update(VacationId,
                                EmployeeId,
                                TypeId,
                                AltEmployeeId,
                                EffectiveDate,
                                DateExpire,
                                DateOfReturn,
                                Note,
                                Address,
                                Contact,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetDepartmentVacationsDataset(
        ByVal DepartmentId As Integer,
        ByVal BegDate As Date,
        ByVal EndDate As Date,
        Optional ByVal SelectOptionNo As Integer = 0,
        Optional ByVal FilterOptionNo As Integer = 0,
        Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Return New DAVacation().GetDepartmentVacationsDataset(DepartmentId, BegDate, EndDate, SelectOptionNo, FilterOptionNo, Lang)
        End Function
        Public Function GetVacationTypesList() As DataSet
            Return New DAVacation().GetVacationTypesList
        End Function


        Public Function GetVacationByEmployeeManagerForApprovalDataset( _
ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationByEmployeeManagerForApprovalDataset(ManagerID)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function

        Public Function GetVacationByEmployeeManagerForApprovalDataset1(ByVal ManagerID As Integer, Optional ByVal Lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationByEmployeeManagerForApprovalDataset1(ManagerID, Lang)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function

        Public Function GetVacationBySpecificRoleForApprovalDataset(ByVal ManagerID As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationBySpecificRoleForApprovalDataset(ManagerID)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function

        Public Function GetVacationByAltEmployeeForApprovalDataset(ByVal AltEmployeeId As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacation As New DAVacation
            ds = oVacation.GetVacationByAltEmployeeForApprovalDataset(AltEmployeeId)
            MyBase.PageTotal = oVacation.PageTotal

            Return ds
        End Function

        Public Function GetVacationApprovalLogByVacationId(ByVal VacationId As Integer) As System.Data.DataSet
            Return New DAVacation().GetVacationApprovalLogByVacationId(VacationId)
        End Function

        Public Function AddVacationApproval(ByVal VacationId As Integer,
                                            ByVal ApprovalPolicyId As Integer,
                                            ByVal ApprovalPathId As Integer,
                                            ByVal IsApproved As Boolean,
                                            ByVal IsRejected As Boolean,
                                            ByVal Comments As String,
                                            ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation

            If oVacation.AddVacationApproval(VacationId,
                           ApprovalPolicyId,
                           ApprovalPathId,
                           IsApproved,
                           IsRejected,
                           Comments,
                           UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oVacation.Identity
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed

        End Function

        Public Function UpdateVacationApprovalStatus(ByVal VacationId As Integer,
                                                     ByVal IsApproved As Boolean,
                                                     ByVal IsRejected As Boolean,
                                                     ByVal UserId As Integer,
                                                     ByVal Notes As String) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation
            If oVacation.UpdateVacationApprovalStatus(VacationId,
                                IsApproved,
                                IsRejected,
                                UserId,
                                Notes) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function UpdateVacationAltEmployeeApprovalStatus(ByVal VacationId As Integer,
                                                                 ByVal IsApproved As Boolean,
                                                                 ByVal IsRejected As Boolean,
                                                                 ByVal UserId As Integer,
                                                                 ByVal Notes As String) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacation As New DAVacation
            If oVacation.UpdateVacationAltEmployeeApprovalStatus(VacationId,
                                IsApproved,
                                IsRejected,
                                UserId,
                                Notes) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacation.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacation.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region

#Region " BOEVacation "
    Public Class BOEVacation
        Inherits BOEntityBase


#Region " Private Variables  "
        Private intVacationId As Integer
        Private intEmployeeId As Integer
        Private intTypeId As Integer
        Private intAltEmployeeId As Integer
        Private datEffectiveDate As Date
        Private datDateExpire As Date
        Private datDateOfReturn As Date
        Private strNote As String
        Private strAddress As String
        Private strContact As String
#End Region
#Region " Public Properties "
        Public Property VacationId() As Integer
            Get
                Return intVacationId
            End Get
            Set(ByVal value As Integer)
                intVacationId = value
            End Set
        End Property
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property
        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
            End Set
        End Property

        Public Property AltEmployeeId() As Integer
            Get
                Return intAltEmployeeId
            End Get
            Set(ByVal value As Integer)
                intAltEmployeeId = value
            End Set
        End Property
        Public Property EffectiveDate() As Date
            Get
                Return datEffectiveDate
            End Get
            Set(ByVal value As Date)
                datEffectiveDate = value
            End Set
        End Property

        Public Property DateExpire() As Date
            Get
                Return datDateExpire
            End Get
            Set(ByVal value As Date)
                datDateExpire = value
            End Set
        End Property

        Public Property DateOfReturn() As Date
            Get
                Return datDateOfReturn
            End Get
            Set(ByVal value As Date)
                datDateOfReturn = value
            End Set
        End Property


        Public Property Note() As String
            Get
                Return strNote
            End Get
            Set(ByVal value As String)
                strNote = value
            End Set
        End Property

       
        Public Property Address() As String
            Get
                Return strAddress
            End Get
            Set(ByVal value As String)
                strAddress = value
            End Set
        End Property


        Public Property Contact() As String
            Get
                Return strContact
            End Get
            Set(ByVal value As String)
                strContact = value
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
            Dim eVacation As Vacation = CType(entity, Vacation)
            With eVacation
                intVacationId = .VacationId
                intEmployeeId = .EmployeeId
                intTypeId = .TypeId
                intAltEmployeeId = .AltEmployeeId
                datEffectiveDate = .EffectiveDate
                datDateExpire = .DateExpire
                datDateOfReturn = .DateOfReturn
                strNote = .Note
                strAddress = .Address
                strContact = .Contact
            End With
        End Sub
#End Region

    End Class
#End Region

    



#Region " BOEVacationType "
    Public Class BOEVacationType
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intTypeId As Integer
        Private strTypeName As String
        Private strTypeNameEN As String
#End Region
#Region " Public Properties "
        Public Property TypeId() As Integer
            Get
                Return intTypeId
            End Get
            Set(ByVal value As Integer)
                intTypeId = value
            End Set
        End Property

        Public Property TypeName() As String
            Get
                Return strTypeName
            End Get
            Set(ByVal value As String)
                strTypeName = value
            End Set
        End Property

        Public Property TypeNameEN() As String
            Get
                Return strTypeNameEN
            End Get
            Set(ByVal value As String)
                strTypeNameEN = value
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
            Dim eVacationType As VacationType = CType(entity, VacationType)
            With eVacationType
                intTypeId = .TypeId
                strTypeName = .TypeName
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOVacationType "
    Public Class BOVacationType
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacationType As New DAVacationType
            If oVacationType.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacationType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacationType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oVacationType As New DAVacationType
            Return MapEntityToProperties(oVacationType.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bVacationType As New BOEVacationType

            If Not IsNothing(Entity) Then
                Dim eVacationType As VacationType = CType(Entity, VacationType)
                With bVacationType
                    .AddedDate = eVacationType.AddedDate
                    .AddedUserAccountId = eVacationType.AddedUserAccountId
                    .UpdatedDate = eVacationType.UpdatedDate
                    .UpdatedUserAccountId = eVacationType.UpdatedUserAccountId
                    .VersionNo = eVacationType.VersionNo

                    .TypeId = eVacationType.TypeId
                    .TypeName = eVacationType.TypeName
                    .TypeNameEN = eVacationType.TypeNameEN

                    .AddedUserName = eVacationType.AddedUserName
                    .UpdatedUserName = eVacationType.UpdatedUserName
                End With
            End If

            Return bVacationType
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eVacationType As New VacationType

            If Not IsNothing(Entity) Then
                Dim bVacation As BOEVacationType = CType(Entity, BOEVacationType)
                With eVacationType
                    .AddedDate = bVacation.AddedDate
                    .AddedUserAccountId = bVacation.AddedUserAccountId
                    .UpdatedDate = bVacation.UpdatedDate
                    .UpdatedUserAccountId = bVacation.UpdatedUserAccountId
                    .VersionNo = bVacation.VersionNo

                    .TypeId = bVacation.TypeId
                    .TypeName = bVacation.TypeName
                    .TypeNameEN = bVacation.TypeNameEN
                End With
            End If

            Return eVacationType
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(ByVal TypeName As String,
                            ByVal UserAccountId As Integer, Optional ByVal TypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacationType As New DAVacationType
            If oVacationType.Add(TypeName, UserAccountId, TypeNameEN) Then
                boolSeccessed = True
                MyBase.Identity = oVacationType.Identity
                MyBase.InfoMessage = oVacationType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacationType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal TypeId As Integer,
        ByVal TypeName As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte(),
        Optional ByVal TypeNameEN As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oVacationType As New DAVacationType
            If oVacationType.Update(TypeId, TypeName, UserAccountId, VersionNo, TypeNameEN) Then
                boolSeccessed = True
                MyBase.InfoMessage = oVacationType.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oVacationType.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetVacationTypesDataset(Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oVacationType As New DAVacationType
            ds = oVacationType.GetVacationTypesDataset(PageNo, PageSize)
            MyBase.PageTotal = oVacationType.PageTotal

            Return ds
        End Function


        Public Function GetList(Optional ByVal Lang As String = "ar") As DataSet
            Return New DAVacationType().GetVacationTypesListDataset(Lang)
        End Function
#End Region

    End Class
#End Region
End Namespace


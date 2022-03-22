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

#Region " BODepartment "
    Public Class BODepartment
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oDepartment As New DADepartment
            If oDepartment.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oDepartment.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oDepartment.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oDepartment As New DADepartment
            Return MapEntityToProperties(oDepartment.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim eDepartment As Department = DirectCast(MapPropertiesToEntity(BOEntity), Department)
            Dim oDepartment As New DADepartment

            ds = oDepartment.GetDataset(eDepartment, PageNo, PageSize)
            MyBase.PageTotal = oDepartment.PageTotal

            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim boDepartment As New BOEDepartment

            If Not IsNothing(Entity) Then
                Dim eDepartment As Department = CType(Entity, Department)
                With boDepartment
                    .AddedDate = eDepartment.AddedDate
                    .AddedUserAccountId = eDepartment.AddedUserAccountId
                    .UpdatedDate = eDepartment.UpdatedDate
                    .UpdatedUserAccountId = eDepartment.UpdatedUserAccountId
                    .VersionNo = eDepartment.VersionNo
                    .DepartmentId = eDepartment.DepartmentId
                    .DepartmentName = eDepartment.DepartmentName
                    .DepartmentNameEN = eDepartment.DepartmentNameEN
                    .ParentId = eDepartment.ParentId
                    .AddedUserName = eDepartment.AddedUserName
                    .UpdatedUserName = eDepartment.UpdatedUserName
                End With
            End If

            Return boDepartment
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eDepartment As New Department

            If Not IsNothing(Entity) Then
                Dim BoDepartment As BOEDepartment = CType(Entity, BOEDepartment)
                With eDepartment
                    .AddedDate = BoDepartment.AddedDate
                    .AddedUserAccountId = BoDepartment.AddedUserAccountId
                    .UpdatedDate = BoDepartment.UpdatedDate
                    .UpdatedUserAccountId = BoDepartment.UpdatedUserAccountId
                    .VersionNo = BoDepartment.VersionNo
                    .DepartmentId = BoDepartment.DepartmentId
                    .DepartmentName = BoDepartment.DepartmentName
                    .ParentId = eDepartment.ParentId
                End With
            End If

            Return eDepartment
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal DepartmentName As String, _
        ByVal DepartmentNameEN As String, _
        ByVal ParentId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oDepartment As New DADepartment
            If oDepartment.Add(DepartmentName, DepartmentNameEN, ParentId, UserAccountId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oDepartment.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oDepartment.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal DepartmentId As Integer, _
        ByVal DepartmentName As String, _
        ByVal DepartmentNameEN As String, _
        ByVal ParentId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oDepartment As New DADepartment
            If oDepartment.Update(DepartmentId, DepartmentName, DepartmentNameEN, ParentId, UserAccountId, VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oDepartment.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oDepartment.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetList() As DataSet
            Return New DADepartment().GetList
        End Function

        Public Function GetAllDepartment() As System.Data.DataSet
            Return New DADepartment().GetAllDepartment()
        End Function
        Public Function GetChildsDataset(ByVal Id As Integer) As DataSet
            Return New DADepartment().GetChildsDataset(Id)
        End Function
        Public Function GetDepartmentsDataset() As DataSet
            Return New DADepartment().GetDepartmentsDataset
        End Function
        Public Function GetEmployeeDepartmentsListForDropdownTree(ByVal UserAccountId As Integer) As DataSet
            Dim intEmployeeId As Integer = New DAEmployee().GetEmployeeId(UserAccountId)
            Return New DADepartment().GetEmployeeDepartmentsListForDropdownTree(intEmployeeId)
        End Function

        Public Function GetEmployeeDepartmentsList(ByVal UserAccountId As Integer) As DataSet
            Dim intEmployeeId As Integer = New DAEmployee().GetEmployeeId(UserAccountId)
            Return New DADepartment().GetEmployeeDepartmentsList(intEmployeeId)
        End Function
#End Region

#Region " Miscellaneous "
        Public Function AddScope(ByVal EmployeeId As Integer, ByVal DepartmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oDepartment As New DADepartment
            If oDepartment.AddScope(EmployeeId, DepartmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oDepartment.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oDepartment.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteScope(ByVal EmployeeId As Integer, ByVal DepartmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oDepartment As New DADepartment
            If oDepartment.DeleteScope(EmployeeId, DepartmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oDepartment.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oDepartment.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetScope(ByVal EmployeeId As Integer) As Integer
            Return New DADepartment().GetScope(EmployeeId)
        End Function

        Public Function GetScopes(ByVal EmployeeId As Integer) As System.Data.DataSet
            Return New DADepartment().GetScopes(EmployeeId)
        End Function

#End Region

    End Class
#End Region

#Region " BOEDepartment "
    Public Class BOEDepartment
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intDepartmentId As Integer
        Private strDepartmentName As String
        Private strDepartmentNameEN As String
        Private intParentId As Integer
#End Region
#Region " Public Properties "
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Public Property DepartmentName() As String
            Get
                Return strDepartmentName
            End Get
            Set(ByVal value As String)
                strDepartmentName = value
            End Set
        End Property

        Public Property DepartmentNameEN() As String
            Get
                Return strDepartmentNameEN
            End Get
            Set(ByVal value As String)
                strDepartmentNameEN = value
            End Set
        End Property

        Public Property ParentId() As Integer
            Get
                Return intParentId
            End Get
            Set(ByVal value As Integer)
                intParentId = value
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
            Dim eDepartment As Department = CType(entity, Department)
            With eDepartment
                intDepartmentId = .DepartmentId
                strDepartmentName = .DepartmentName
                strDepartmentNameEN = .DepartmentNameEN
                intParentId = .ParentId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BODepartments "
    Public Class BODepartmentsList
        Inherits BOListBase(Of BOEDepartment)

        Public Sub Load()
            LoadFromList(New DADepartment().[Load]())
        End Sub

        Private Sub LoadFromList(ByVal Departments As List(Of Department))
            If Departments.Count > 0 Then
                For Each eDepartment As Department In Departments
                    Dim bDepartment As New BOEDepartment
                    bDepartment.MapEntityToProperties(eDepartment)
                    Me.Add(bDepartment)
                Next
            End If
        End Sub
        Friend Function GetByDepartmentId(ByVal Id As Integer) As BOEDepartment
            Return Me.SingleOrDefault(Function(r) r.DepartmentId = Id)
        End Function
    End Class
#End Region

End Namespace


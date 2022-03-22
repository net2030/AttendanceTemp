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

#Region " BOMachine "
    Public Class BOMachine
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachine As New DAMachine
            If oMachine.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oMachine.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachine.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oMachine As New DAMachine
            Return MapEntityToProperties(oMachine.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bMachine As New BOEMachine

            If Not IsNothing(Entity) Then
                Dim eMachine As Machine = CType(Entity, Machine)
                With bMachine
                    .AddedDate = eMachine.AddedDate
                    .AddedUserAccountId = eMachine.AddedUserAccountId
                    .UpdatedDate = eMachine.UpdatedDate
                    .UpdatedUserAccountId = eMachine.UpdatedUserAccountId
                    .VersionNo = eMachine.VersionNo
                    .AddedUserName = eMachine.AddedUserName
                    .UpdatedUserName = eMachine.UpdatedUserName

                    .MachineId = eMachine.MachineId
                    .MachineName = eMachine.MachineName
                    .Location = eMachine.Location
                    .DepartmentId = eMachine.DepartmentId
                    .IPAddress = eMachine.IPAddress
                    .MachineTypeId = eMachine.MachineTypeId
                End With
            End If

            Return bMachine
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eMachine As New Machine

            If Not IsNothing(Entity) Then
                Dim bMachine As BOEMachine = CType(Entity, BOEMachine)
                With eMachine
                    .AddedDate = bMachine.AddedDate
                    .AddedUserAccountId = bMachine.AddedUserAccountId
                    .UpdatedDate = bMachine.UpdatedDate
                    .UpdatedUserAccountId = bMachine.UpdatedUserAccountId
                    .VersionNo = bMachine.VersionNo

                    .MachineId = bMachine.MachineId
                    .MachineName = bMachine.MachineName
                    .Location = bMachine.Location
                    .DepartmentId = bMachine.DepartmentId
                    .IPAddress = bMachine.IPAddress
                    .MachineTypeId = bMachine.MachineTypeId
                End With
            End If

            Return eMachine
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal MachineName As String, _
        ByVal Location As String, _
        ByVal IPAdress As String, _
        ByVal TypeId As Integer, _
        ByVal DepartmentId As Integer, _
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachine As New DAMachine
            If oMachine.Add( _
                              MachineName, _
                             Location, _
                             IPAdress, _
                             TypeId, _
                             DepartmentId, _
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oMachine.Identity
                MyBase.InfoMessage = oMachine.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachine.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal MachineId As Integer, _
         ByVal MachineName As String, _
        ByVal Location As String, _
        ByVal IPAdress As String, _
        ByVal TypeId As Integer, _
        ByVal DepartmentId As Integer, _
        ByVal UserAccountId As Integer, _
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachine As New DAMachine
            If oMachine.Update( _
                             MachineId,
                             MachineName, _
                             Location, _
                             IPAdress, _
                             TypeId, _
                             DepartmentId, _
                             UserAccountId,
                             VersionNo) Then
                boolSeccessed = True
                MyBase.Identity = oMachine.Identity
                MyBase.InfoMessage = oMachine.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachine.InfoMessage
            End If

            Return boolSeccessed
        End Function

       

        Public Function GetList() As DataSet
            Return New DAMachine().GetList
        End Function
#End Region

    End Class
#End Region

#Region " BOMachineEmployee "
    Public Class BOMachineEmployee
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oMachineEmployee As New DAMachineEmployee
            Return MapEntityToProperties(oMachineEmployee.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bMachineEmployee As New BOEMachineEmployee

            If Not IsNothing(Entity) Then
                Dim eMachineEmployee As MachineEmployee = CType(Entity, MachineEmployee)
                With bMachineEmployee
                    .AddedDate = eMachineEmployee.AddedDate
                    .AddedUserAccountId = eMachineEmployee.AddedUserAccountId
                    .UpdatedDate = eMachineEmployee.UpdatedDate
                    .UpdatedUserAccountId = eMachineEmployee.UpdatedUserAccountId
                    .VersionNo = eMachineEmployee.VersionNo

                    .MachineId = eMachineEmployee.MachineId
                    .EmployeeId = eMachineEmployee.EmployeeId
                End With
            End If

            Return bMachineEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eMachineEmployee As New MachineEmployee

            If Not IsNothing(Entity) Then
                Dim bMachineEmployee As BOEMachineEmployee = CType(Entity, BOEMachineEmployee)
                With eMachineEmployee
                    .AddedDate = bMachineEmployee.AddedDate
                    .AddedUserAccountId = bMachineEmployee.AddedUserAccountId
                    .UpdatedDate = bMachineEmployee.UpdatedDate
                    .UpdatedUserAccountId = bMachineEmployee.UpdatedUserAccountId
                    .VersionNo = bMachineEmployee.VersionNo

                    .MachineId = bMachineEmployee.MachineId
                    .EmployeeId = bMachineEmployee.EmployeeId
                End With
            End If

            Return eMachineEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee(
        ByVal MachineId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineEmployee As New DAMachineEmployee
            If oMachineEmployee.AddEmployee(MachineId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function AddAllEmployees(
        ByVal MachineId As Integer,
        ByVal departmentId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineEmployee As New DAMachineEmployee
            If oMachineEmployee.AddAllEmployees(MachineId, departmentId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteEmployee(
        ByVal MachineId As Integer,
        ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oMachineEmployee As New DAMachineEmployee
            If oMachineEmployee.DeleteEmployee(MachineId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oMachineEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetMachineEmployeesDataset(
        ByVal MachineId As Integer,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oMachineEmployee As New DAMachineEmployee
            ds = oMachineEmployee.GetMachineEmployeesDataset(MachineId, PageNo, PageSize)
            MyBase.PageTotal = oMachineEmployee.PageTotal

            Return ds
        End Function
        Public Function GetMachinesByEmployee(ByVal EmployeeId As Integer) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oMachineEmployee As New DAMachineEmployee
            ds = oMachineEmployee.GetMachinesByEmployee(EmployeeId)
            MyBase.PageTotal = oMachineEmployee.PageTotal

            Return ds
        End Function
        Public Function IsExistsInMachine(ByVal MachineId As Integer, ByVal EmployeeId As Integer) As Boolean
            Return New DAMachineEmployee().IsExistsInMachine(MachineId, EmployeeId)
        End Function
#End Region

    End Class
#End Region

#Region " BOEMachine "
    Public Class BOEMachine
        Inherits BOEntityBase


#Region " Constructor "
        Public Sub New()

        End Sub
#End Region

#Region " Private Variables  "
        Private intMachineId As Integer
        Private strMachineName As String
        Private strLocation As String
        Private intDepartmentId As Integer
        Private strIPAddress As String
        Private intMachineTypeId As Integer
#End Region

#Region " Public Properties "
        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
            End Set
        End Property
        Public Property MachineName() As String
            Get
                Return strMachineName
            End Get
            Set(ByVal value As String)
                strMachineName = value
            End Set
        End Property
        Public Property Location() As String
            Get
                Return strLocation
            End Get
            Set(ByVal value As String)
                strLocation = value
            End Set
        End Property
        Public Property DepartmentId() As Integer
            Get
                Return intDepartmentId
            End Get
            Set(ByVal value As Integer)
                intDepartmentId = value
            End Set
        End Property
        Public Property IPAddress() As String
            Get
                Return strIPAddress
            End Get
            Set(ByVal value As String)
                strIPAddress = value
            End Set
        End Property
        Public Property MachineTypeId() As Integer
            Get
                Return intMachineTypeId
            End Get
            Set(ByVal value As Integer)
                intMachineTypeId = value
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
            Dim eMachine As Machine = CType(entity, Machine)
            With eMachine
                intMachineId = .MachineId
                strMachineName = .MachineName
                strLocation = .Location
                intDepartmentId = .DepartmentId
                strIPAddress = .IPAddress
                intMachineTypeId = .MachineTypeId
            End With
        End Sub
#End Region


    End Class
#End Region

#Region " BOEMachineEmployee "
    Public Class BOEMachineEmployee
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
        End Sub
#End Region

#Region " Private Variables  "
        Private intMachineId As Integer
        Private intEmployeeId As Integer
#End Region
#Region " Public Properties "
        Public Property MachineId() As Integer
            Get
                Return intMachineId
            End Get
            Set(ByVal value As Integer)
                intMachineId = value
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
            Dim eMachineEmployee As MachineEmployee = CType(entity, MachineEmployee)
            With eMachineEmployee
                intMachineId = .MachineId
                intEmployeeId = .EmployeeId
            End With
        End Sub
#End Region

    End Class
#End Region
End Namespace


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

#Region " BOHoliday "
    Public Class BOHoliday
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False
            Dim oHoliday As New DAHoliday
            If oHoliday.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oHoliday.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oHoliday.InfoMessage
            End If
            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oHoliday As New DAHoliday
            Return MapEntityToProperties(oHoliday.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Public Function GetHolidaysDataset(Optional ByVal PageNo As Integer = 1, Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Dim oHoliday As New DAHoliday
            ds = oHoliday.GetHolidaysDataset(PageNo, PageSize)
            MyBase.PageTotal = oHoliday.PageTotal
            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bHoliday As New BOEHoliday

            If Not IsNothing(Entity) Then
                Dim eHoliday As Holiday = CType(Entity, Holiday)
                With bHoliday
                    .AddedDate = eHoliday.AddedDate
                    .AddedUserAccountId = eHoliday.AddedUserAccountId
                    .UpdatedDate = eHoliday.UpdatedDate
                    .UpdatedUserAccountId = eHoliday.UpdatedUserAccountId
                    .VersionNo = eHoliday.VersionNo

                    .HolidayId = eHoliday.HolidayId
                    .HolidayName = eHoliday.HolidayName
                    .EffectiveDate = eHoliday.EffectiveDate
                    .DateExpire = eHoliday.DateExpire
                    .Note = eHoliday.Note

                    .AddedUserName = eHoliday.AddedUserName
                    .UpdatedUserName = eHoliday.UpdatedUserName
                End With
            End If

            Return bHoliday
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eHoliday As New Holiday

            If Not IsNothing(Entity) Then
                Dim bHoliday As BOEHoliday = CType(Entity, BOEHoliday)
                With eHoliday
                    .AddedDate = bHoliday.AddedDate
                    .AddedUserAccountId = bHoliday.AddedUserAccountId
                    .UpdatedDate = bHoliday.UpdatedDate
                    .UpdatedUserAccountId = bHoliday.UpdatedUserAccountId
                    .VersionNo = bHoliday.VersionNo

                    .HolidayId = bHoliday.HolidayId
                    .HolidayName = bHoliday.HolidayName
                    .EffectiveDate = bHoliday.EffectiveDate
                    .DateExpire = bHoliday.DateExpire
                    .Note = bHoliday.Note
                End With
            End If

            Return eHoliday
        End Function
#End Region

#Region " Public Methods "
        Public Function Add( _
        ByVal HolidayName As String,
        ByVal EffectiveDate As Date,
        ByVal DateExpire As Date,
        ByVal Note As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oHoliday As New DAHoliday
            If oHoliday.Add(HolidayName,
                             EffectiveDate,
                             DateExpire,
                             Note,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oHoliday.Identity
                MyBase.InfoMessage = oHoliday.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oHoliday.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update( _
        ByVal HolidayId As Integer,
        ByVal HolidayName As String,
        ByVal EffectiveDate As Date,
        ByVal DateExpire As Date,
        ByVal Note As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oHoliday As New DAHoliday
            If oHoliday.Update(HolidayId,
                                HolidayName,
                                EffectiveDate,
                                DateExpire,
                                Note,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oHoliday.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oHoliday.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region

#Region " BOEHoliday "
    Public Class BOEHoliday
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intHolidayId As Integer
        Private strHolidayName As String
        Private datEffectiveDate As Date
        Private datDateExpire As Date
        Private strNote As String
#End Region

#Region " Public Properties "
        Public Property HolidayId() As Integer
            Get
                Return intHolidayId
            End Get
            Set(ByVal value As Integer)
                intHolidayId = value
            End Set
        End Property
        Public Property HolidayName() As String
            Get
                Return strHolidayName
            End Get
            Set(ByVal value As String)
                strHolidayName = value
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
        Public Property Note() As String
            Get
                Return strNote
            End Get
            Set(ByVal value As String)
                strNote = value
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
            Dim eHoliday As Holiday = CType(entity, Holiday)
            With eHoliday
                intHolidayId = .HolidayId
                strHolidayName = .HolidayName
                datEffectiveDate = .EffectiveDate
                datDateExpire = .DateExpire
                strNote = .Note
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOHolidayExceptionEmployee "
    Public Class BOHolidayExceptionEmployee
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Throw New NotImplementedException
        End Function

        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Throw New NotImplementedException
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bHolidayExceptionEmployee As New BOEHolidayExceptionEmployee

            If Not IsNothing(Entity) Then
                Dim eHolidayExceptionEmployee As HolidayExceptionEmployee = CType(Entity, HolidayExceptionEmployee)
                With bHolidayExceptionEmployee
                    .HolidayId = eHolidayExceptionEmployee.HolidayId
                    .EmployeeId = eHolidayExceptionEmployee.EmployeeId
                End With
            End If

            Return bHolidayExceptionEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eHolidayExceptionEmployee As New HolidayExceptionEmployee

            If Not IsNothing(Entity) Then
                Dim bHolidayExceptionEmployee As BOEHolidayExceptionEmployee = CType(Entity, BOEHolidayExceptionEmployee)
                With eHolidayExceptionEmployee
                    .HolidayId = bHolidayExceptionEmployee.HolidayId
                    .EmployeeId = bHolidayExceptionEmployee.EmployeeId
                End With
            End If

            Return eHolidayExceptionEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee( _
        ByVal HolidayId As Integer, _
        ByVal EmployeeId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oHolidayExceptionEmployee As New DAHolidayExceptionEmployee
            If oHolidayExceptionEmployee.AddEmployee(HolidayId, EmployeeId, UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oHolidayExceptionEmployee.Identity
                MyBase.InfoMessage = oHolidayExceptionEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oHolidayExceptionEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteEmployee(ByVal HolidayId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oHolidayExceptionEmployee As New DAHolidayExceptionEmployee
            If oHolidayExceptionEmployee.DeleteEmployee(HolidayId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oHolidayExceptionEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oHolidayExceptionEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetHolidayExceptionEmployeesDataset( _
        ByVal HolidayId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oHolidayExceptionEmployee As New DAHolidayExceptionEmployee
            ds = oHolidayExceptionEmployee.GetHolidayExceptionEmployeesDataset(HolidayId, PageNo, PageSize)
            MyBase.PageTotal = oHolidayExceptionEmployee.PageTotal

            Return ds
        End Function
#End Region


    End Class
#End Region

#Region " BOEHolidayExceptionEmployee "
    Public Class BOEHolidayExceptionEmployee
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intHolidayId As Integer
        Private intEmployeeId As Integer
#End Region
#Region " Public Properties  "
        Public Property HolidayId() As Integer
            Get
                Return intHolidayId
            End Get
            Set(ByVal value As Integer)
                intHolidayId = value
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
                Try
                    MyBase.AddedDate = entity.AddedDate
                    MyBase.AddedUserAccountId = entity.AddedUserAccountId
                    MyBase.UpdatedDate = entity.UpdatedDate
                    MyBase.UpdatedUserAccountId = entity.UpdatedUserAccountId
                    MyBase.VersionNo = entity.VersionNo
                    MyBase.AddedUserName = entity.AddedUserName
                    MyBase.UpdatedUserName = entity.UpdatedUserName
                Catch ex As Exception
                End Try

                Me.MapEntityToCustomProperties(entity)
            End If
        End Sub
        Private Sub MapEntityToCustomProperties(ByVal entity As ICommonAttributes)
            Dim eHolidayExceptionEmployee As HolidayExceptionEmployee = CType(entity, HolidayExceptionEmployee)
            With eHolidayExceptionEmployee
                intHolidayId = .HolidayId
                intEmployeeId = .EmployeeId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOHolidayExceptionEmployeesList "
    Public Class BOHolidayExceptionEmployeesList
        Inherits BOListBase(Of BOEHolidayExceptionEmployee)

        Public Sub Load(ByVal Id As Integer)
            LoadFromList(New DAHolidayExceptionEmployee().LoadEmployees(Id))
        End Sub
        Private Sub LoadFromList(ByVal HolidayExceptionEmployees As List(Of HolidayExceptionEmployee))
            If HolidayExceptionEmployees.Count > 0 Then
                For Each eHolidayExceptionEmployee As HolidayExceptionEmployee In HolidayExceptionEmployees
                    Dim bHolidayExceptionEmployee As New BOEHolidayExceptionEmployee
                    bHolidayExceptionEmployee.MapEntityToProperties(eHolidayExceptionEmployee)
                    Me.Add(bHolidayExceptionEmployee)
                Next
            End If
        End Sub
    End Class


#End Region

End Namespace


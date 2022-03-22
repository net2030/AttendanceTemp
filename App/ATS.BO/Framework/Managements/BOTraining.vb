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

#Region " BOTraining "
    Public Class BOTraining
        Inherits BOBase

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTraining As New DATraining
            If oTraining.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTraining.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTraining.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oTraining As New DATraining
            Return MapEntityToProperties(oTraining.Find(Id))
        End Function

        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function

        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bTraining As New BOETraining

            If Not IsNothing(Entity) Then
                Dim eTraining As Training = CType(Entity, Training)
                With bTraining
                    .AddedDate = eTraining.AddedDate
                    .AddedUserAccountId = eTraining.AddedUserAccountId
                    .UpdatedDate = eTraining.UpdatedDate
                    .UpdatedUserAccountId = eTraining.UpdatedUserAccountId
                    .VersionNo = eTraining.VersionNo

                    .TrainingId = eTraining.TrainingId
                    .DepartmentId = eTraining.DepartmentId
                    .CourseName = eTraining.CourseName
                    .InstituteName = eTraining.InstituteName
                    .TrainingBegDate = eTraining.TrainingBegDate
                    .TrainingEndDate = eTraining.TrainingEndDate
                    .Notes = eTraining.Notes

                    .AddedUserName = eTraining.AddedUserName
                    .UpdatedUserName = eTraining.UpdatedUserName
                End With
            End If

            Return bTraining
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eTraining As New Training

            If Not IsNothing(Entity) Then
                Dim bTraining As BOETraining = CType(Entity, BOETraining)
                With eTraining
                    .AddedDate = bTraining.AddedDate
                    .AddedUserAccountId = bTraining.AddedUserAccountId
                    .UpdatedDate = bTraining.UpdatedDate
                    .UpdatedUserAccountId = bTraining.UpdatedUserAccountId
                    .VersionNo = bTraining.VersionNo

                    .TrainingId = bTraining.TrainingId
                    .DepartmentId = bTraining.DepartmentId
                    .CourseName = bTraining.CourseName
                    .InstituteName = bTraining.InstituteName
                    .TrainingBegDate = bTraining.TrainingBegDate
                    .TrainingEndDate = bTraining.TrainingEndDate
                    .Notes = bTraining.Notes
                End With
            End If

            Return eTraining
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal DepartmentId As Integer,
        ByVal CourseName As String,
        ByVal InstituteName As String,
        ByVal TrainingBegDate As Date,
        ByVal TrainingEndDate As Date,
        ByVal Notes As String,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTraining As New DATraining
            If oTraining.Add( _
                             DepartmentId,
                             CourseName,
                             InstituteName,
                             TrainingBegDate,
                             TrainingEndDate,
                             Notes,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oTraining.Identity
                MyBase.InfoMessage = oTraining.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTraining.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal TrainingId As Integer,
        ByVal DepartmentId As Integer,
        ByVal CourseName As String,
        ByVal InstituteName As String,
        ByVal TrainingBegDate As Date,
        ByVal TrainingEndDate As Date,
        ByVal Notes As String,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTraining As New DATraining
            If oTraining.Update(TrainingId,
                                DepartmentId,
                                CourseName,
                                InstituteName,
                                TrainingBegDate,
                                TrainingEndDate,
                                Notes,
                                UserAccountId,
                                VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTraining.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTraining.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function GetTrainingsDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oTraining As New DATraining
            ds = oTraining.GetTrainingDataset(UserAccountId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oTraining.PageTotal

            Return ds
        End Function
        Public Function GetTrainingByDepartmentIdDataset( _
        ByVal DepartmentId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oTraining As New DATraining
            ds = oTraining.GetTrainingByDepartmentIdDataset(DepartmentId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oTraining.PageTotal

            Return ds
        End Function
        Public Function GetTrainingsByEmployeeIdDataset( _
        ByVal EmployeeId As Integer, _
        ByVal BegDate As Date, _
        ByVal EndDate As Date, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oTraining As New DATraining
            ds = oTraining.GetTrainingEmployeeIdDataset(EmployeeId, BegDate, EndDate, PageNo, PageSize)
            MyBase.PageTotal = oTraining.PageTotal

            Return ds
        End Function
#End Region

#Region " Save "
        Public Function Save(ByVal Training As BOETraining, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False


            Using TS As New TransactionScope()

                Select Case Training.DBAction
                    Case BOEntityBase.DBActionEnum.Add
                        If Training.Employees.Count = 0 Then
                            MyBase.InfoMessage = "يجب إضافة على الأقل موظف واحد للدورة التدريبية"
                            Return False
                        End If

                        If Not Me.Add(
                                  Training.DepartmentId,
                                  Training.CourseName,
                                  Training.InstituteName,
                                  Training.TrainingBegDate,
                                  Training.TrainingEndDate,
                                  Training.Notes,
                                  UserAccountId) Then
                            Return False
                        Else
                            Dim oEmployee As New BOTrainingEmployee
                            For Each eEmployee As BOETrainingEmployee In Training.Employees
                                If Not oEmployee.AddEmployee(Me.Identity, eEmployee.EmployeeId, UserAccountId) Then
                                    MyBase.InfoMessage = oEmployee.InfoMessage
                                    Return False
                                End If
                            Next
                        End If

                    Case BOEntityBase.DBActionEnum.Update
                        If Training.Employees.Count = 0 Then
                            MyBase.InfoMessage = "يجب إضافة على الأقل موظف واحد للدورة التدريبية"
                            Return False
                        End If

                        If Not Me.Update(
                                  Training.TrainingId,
                                  Training.DepartmentId,
                                  Training.CourseName,
                                  Training.InstituteName,
                                  Training.TrainingBegDate,
                                  Training.TrainingEndDate,
                                  Training.Notes,
                                  UserAccountId,
                                  Training.VersionNo) Then
                            Return False
                        Else
                            Dim oEmployee As New BOTrainingEmployee
                            For Each eEmployee As BOETrainingEmployee In Training.Employees
                                Select Case eEmployee.DBAction
                                    Case BOEntityBase.DBActionEnum.Add
                                        If Not oEmployee.AddEmployee(Training.TrainingId, eEmployee.EmployeeId, UserAccountId) Then
                                            MyBase.InfoMessage = oEmployee.InfoMessage
                                            Return False
                                        End If
                                    Case BOEntityBase.DBActionEnum.Delete
                                        If Not oEmployee.DeleteEmployee(Training.TrainingId, eEmployee.EmployeeId) Then
                                            MyBase.InfoMessage = oEmployee.InfoMessage
                                            Return False
                                        End If
                                End Select
                            Next
                        End If

                    Case BOEntityBase.DBActionEnum.Delete
                        If Not Me.Delete(Training.TrainingId) Then
                            Return False
                        End If
                End Select

                TS.Complete()
                boolSeccessed = True
            End Using


            Return boolSeccessed
        End Function
#End Region

    End Class
#End Region
#Region " BOETraining "
    Public Class BOETraining
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
            oEmployees = New BOTrainingEmployeesList
        End Sub
#End Region

#Region " Private Variables  "
        Private intTrainingId As Integer
        Private intDepartmentId As Integer
        Private strCourseName As String
        Private strInstituteName As String
        Private datTrainingBegDate As Date
        Private datTrainingEndDate As Date
        Private strNotes As String

        Private oEmployees As BOTrainingEmployeesList
#End Region
#Region " Public Properties "
        Public Property TrainingId() As Integer
            Get
                Return intTrainingId
            End Get
            Set(ByVal value As Integer)
                intTrainingId = value
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
        Public Property CourseName() As String
            Get
                Return strCourseName
            End Get
            Set(ByVal value As String)
                strCourseName = value
            End Set
        End Property
        Public Property InstituteName() As String
            Get
                Return strInstituteName
            End Get
            Set(ByVal value As String)
                strInstituteName = value
            End Set
        End Property
        Public Property TrainingBegDate() As Date
            Get
                Return datTrainingBegDate
            End Get
            Set(ByVal value As Date)
                datTrainingBegDate = value
            End Set
        End Property
        Public Property TrainingEndDate() As Date
            Get
                Return datTrainingEndDate
            End Get
            Set(ByVal value As Date)
                datTrainingEndDate = value
            End Set
        End Property
        
        Public Property Notes() As String
            Get
                Return strNotes
            End Get
            Set(ByVal value As String)
                strNotes = value
            End Set
        End Property

        Public Property Employees() As BOTrainingEmployeesList
            Get
                Return oEmployees
            End Get
            Set(ByVal value As BOTrainingEmployeesList)
                oEmployees = value
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
            Dim eTraining As Training = CType(entity, Training)
            With eTraining
                intTrainingId = .TrainingId
                intDepartmentId = .DepartmentId
                strCourseName = .CourseName
                strInstituteName = .InstituteName
                datTrainingBegDate = .TrainingBegDate
                datTrainingEndDate = .TrainingEndDate
                strNotes = .Notes
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOTrainingEmployee "
    Public Class BOTrainingEmployee
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
        Public Function GetTrainingEmployeesDataset( _
        ByVal TrainingId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oTrainingEmployee As New DATrainingEmployee
            ds = oTrainingEmployee.GetTrainingEmployeesDataset(TrainingId, PageNo, PageSize)
            MyBase.PageTotal = oTrainingEmployee.PageTotal

            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bTrainingEmployee As New BOETrainingEmployee

            If Not IsNothing(Entity) Then
                Dim eTrainingEmployee As TrainingEmployee = CType(Entity, TrainingEmployee)
                With bTrainingEmployee
                    .TrainingId = eTrainingEmployee.TrainingId
                    .EmployeeId = eTrainingEmployee.EmployeeId
                End With
            End If

            Return bTrainingEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eTrainingEmployee As New TrainingEmployee

            If Not IsNothing(Entity) Then
                Dim bTrainingEmployee As BOETrainingEmployee = CType(Entity, BOETrainingEmployee)
                With eTrainingEmployee
                    .TrainingId = bTrainingEmployee.TrainingId
                    .EmployeeId = bTrainingEmployee.EmployeeId
                End With
            End If

            Return eTrainingEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function AddEmployee(
        ByVal TrainingId As Integer,
        ByVal EmployeeId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTrainingEmployee As New DATrainingEmployee
            If oTrainingEmployee.AddEmployee(TrainingId, EmployeeId, UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oTrainingEmployee.Identity
                MyBase.InfoMessage = oTrainingEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTrainingEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function DeleteEmployee(ByVal TrainingId As Integer, ByVal EmployeeId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oTrainingEmployee As New DATrainingEmployee
            If oTrainingEmployee.DeleteEmployee(TrainingId, EmployeeId) Then
                boolSeccessed = True
                MyBase.InfoMessage = oTrainingEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oTrainingEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region


    End Class
#End Region
#Region " BOETrainingEmployee "
    Public Class BOETrainingEmployee
        Inherits BOEntityBase

#Region " Private Variables  "
        Private intTrainingId As Integer
        Private intEmployeeId As Integer
#End Region
#Region " Public Properties  "
        Public Property TrainingId() As Integer
            Get
                Return intTrainingId
            End Get
            Set(ByVal value As Integer)
                intTrainingId = value
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
            Dim eTrainingEmployee As TrainingEmployee = CType(entity, TrainingEmployee)
            With eTrainingEmployee
                intTrainingId = .TrainingId
                intEmployeeId = .EmployeeId
            End With
        End Sub
#End Region

    End Class
#End Region

#Region " BOTrainingEmployeesList "
    Public Class BOTrainingEmployeesList
        Inherits BOListBase(Of BOETrainingEmployee)

        Public Sub Load(ByVal Id As Integer)
            LoadFromList(New DATrainingEmployee().LoadEmployees(Id))
        End Sub
        Private Sub LoadFromList(ByVal TrainingEmployees As List(Of TrainingEmployee))
            If TrainingEmployees.Count > 0 Then
                For Each eTrainingEmployee As TrainingEmployee In TrainingEmployees
                    Dim bTrainingEmployee As New BOETrainingEmployee
                    bTrainingEmployee.MapEntityToProperties(eTrainingEmployee)
                    Me.Add(bTrainingEmployee)
                Next
            End If
        End Sub
    End Class


#End Region

End Namespace


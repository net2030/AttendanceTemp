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
Imports System.Web
Imports System.Web.UI.WebControls

#End Region

Namespace Framework

#Region " BOEmployee "

    Public Class BOEmployee
        Inherits BOBase

#Region " Propreties "
        Private bolIsUpdateScopeRequired As Boolean = False
        Public WriteOnly Property IsNeedToBeUpdated As Boolean
            Set(ByVal value As Boolean)
                bolIsUpdateScopeRequired = value
            End Set
        End Property
#End Region

#Region " Overrides "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim oEmployee As New DAEmployee
            If oEmployee.Delete(Id) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As BOEntityBase
            Dim oEmployee As New DAEmployee
            Return MapEntityToProperties(oEmployee.Find(Id))
        End Function
        Public Overrides Function GetDataset( _
        ByVal BOEntity As BOEntityBase, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing
            Return ds
        End Function
        Public Function GetAllEmployeesDataset(
        ByVal UserAccountId As Integer,
        ByVal FilterOption As enumFilterOption,
        ByVal SortOption As enumSortOption,
        Optional ByVal PageNo As Integer = 1,
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetAllEmployeesDataset(UserAccountId,
                                                  CType(FilterOption, DAEmployee.enumFilterOption),
                                                  CType(SortOption, DAEmployee.enumSortOption),
                                                  PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function
        Public Function GetMilitaryEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Return New DAEmployee().GetMilitaryEmployeesByDepartmentDataset(DepartmentId)
        End Function
        Public Function GetCivilianEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Return New DAEmployee().GetCivilianEmployeesByDepartmentDataset(DepartmentId)
        End Function
        Public Function GetEmployeesByDepartmentDataset(ByVal DepartmentId As Integer) As System.Data.DataSet
            Return New DAEmployee().GetEmployeesByDepartmentDataset(DepartmentId)
        End Function
        Public Function GetEmployeesByNameDataset( _
        ByVal UserAccountId As Integer, _
        ByVal EmployeeName As String, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetEmployeesByNameDataset(UserAccountId, EmployeeName, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function
        Public Function GetEmployeesByBadgeDataset( _
        ByVal UserAccountId As Integer, _
        ByVal BadgeNo As String, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetEmployeesByBadgeDataset(UserAccountId, BadgeNo, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function
        Public Function GetEmployeesByIdDataset( _
        ByVal UserAccountId As Integer, _
        ByVal EmployeeId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetEmployeesByIdDataset(UserAccountId, EmployeeId, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function
        Protected Overrides Function MapEntityToProperties(ByVal Entity As ATS.DA.Framework.ICommonAttributes) As BOEntityBase
            Dim bEmployee As New BOEEmployee

            If Not IsNothing(Entity) Then
                Dim eEmployee As Employee = CType(Entity, Employee)
                With bEmployee
                    .AddedDate = eEmployee.AddedDate
                    .AddedUserAccountId = eEmployee.AddedUserAccountId
                    .UpdatedDate = eEmployee.UpdatedDate
                    .UpdatedUserAccountId = eEmployee.UpdatedUserAccountId
                    .VersionNo = eEmployee.VersionNo

                    .EmployeeId = eEmployee.EmployeeId
                    .FirstName = eEmployee.FirstName
                    .MiddleName = eEmployee.MiddleName
                    .LastName = eEmployee.LastName
                    .EmployeeName = .FirstName + " " + .MiddleName + " " + .LastName
                    .EmployeeNameEnglish = eEmployee.EmployeeNameEnglish

                    .GovId = eEmployee.GovId
                    .EmployeeNo = eEmployee.EmployeeNo
                    .MobileNo = eEmployee.MobileNo
                    .Sex = eEmployee.Sex
                    .Employer = eEmployee.Employer
                    .ContractType = eEmployee.ContractType

                    .ManagerId = eEmployee.ManagerId
                    .JobTitle = eEmployee.JobTitle
                    .PositionTypeId = eEmployee.PositionTypeId
                    .PositionId = eEmployee.PositionId
                    .BadgeNo = eEmployee.BadgeNo
                    .DepartmentId = eEmployee.DepartmentId
                    .NationalityId = eEmployee.NationalityId
                    .HireDate = eEmployee.HireDate
                    .IsActive = eEmployee.IsActive
                    .Picture = eEmployee.Picture
                    .ImageFileName = eEmployee.ImageFileName
                    .IsFingerRegistered = eEmployee.IsFingerRegistered
                    .IsAllowOvertime = eEmployee.IsAllowOvertime
                    .IsExempt = eEmployee.IsExempt
                    .ActionId = eEmployee.ActionId
                    .RoleId = eEmployee.RoleId
                    .LocationId = eEmployee.LocationId
                    .UserName = eEmployee.UserName
                    .EmailAddress = eEmployee.EmailAddress
                    .IsForcePasswordChange = eEmployee.IsForcePasswordChange
                    .WorkScheduleId = eEmployee.WorkScheduleId
                    .AddedUserName = eEmployee.AddedUserName
                    .UpdatedUserName = eEmployee.UpdatedUserName

                    Dim oUser As New DAUserAccount
                    Dim eUser As UserAccount
                    eUser = oUser.FindByEmployeeId(.EmployeeId)
                    Dim bUser As New BOEUserAccount
                    With bUser
                        .AddedDate = eUser.AddedDate
                        .AddedUserAccountId = eUser.AddedUserAccountId
                        .UpdatedDate = eUser.UpdatedDate
                        .UpdatedUserAccountId = eUser.UpdatedUserAccountId
                        .VersionNo = eUser.VersionNo

                        .UserAccountId = eUser.UserAccountId
                        .windowsAccountName = eUser.windowsAccountName
                        .UserName = eUser.UserName
                        .Email = eUser.Email
                        .IsActive = eUser.IsActive
                        .IsFound = eUser.IsFound
                        .EmployeeId = eUser.EmployeeId
                    End With
                    .UserAccount = bUser
                End With
            End If

            Return bEmployee
        End Function
        Protected Overrides Function MapPropertiesToEntity( _
        ByVal Entity As BOEntityBase) As ATS.DA.Framework.DataEntityBase
            Dim eEmployee As New Employee

            If Not IsNothing(Entity) Then
                Dim bEmployee As BOEEmployee = CType(Entity, BOEEmployee)
                With eEmployee
                    .AddedDate = bEmployee.AddedDate
                    .AddedUserAccountId = bEmployee.AddedUserAccountId
                    .UpdatedDate = bEmployee.UpdatedDate
                    .UpdatedUserAccountId = bEmployee.UpdatedUserAccountId
                    .VersionNo = bEmployee.VersionNo

                    .EmployeeId = bEmployee.DepartmentId
                    .FirstName = bEmployee.FirstName
                    .MiddleName = bEmployee.MiddleName
                    .LastName = bEmployee.LastName
                    .EmployeeNameEnglish = bEmployee.EmployeeNameEnglish
                    .GovId = bEmployee.GovId
                    .EmployeeNo = bEmployee.EmployeeNo
                    .MobileNo = bEmployee.MobileNo
                    .Sex = bEmployee.Sex
                    .Employer = bEmployee.Employer
                    .ContractType = bEmployee.ContractType

                    .ManagerId = bEmployee.ManagerId
                    .JobTitle = bEmployee.JobTitle
                    .PositionTypeId = bEmployee.PositionTypeId
                    .PositionId = bEmployee.PositionId
                    .BadgeNo = bEmployee.BadgeNo
                    .DepartmentId = bEmployee.DepartmentId
                    .NationalityId = bEmployee.NationalityId
                    .HireDate = bEmployee.HireDate
                    .IsActive = bEmployee.IsActive
                    .Picture = bEmployee.Picture
                    .IsFingerRegistered = bEmployee.IsFingerRegistered
                    .ActionId = bEmployee.ActionId
                    .RoleId = bEmployee.RoleId
                    .LocationId = bEmployee.LocationId
                    .UserName = bEmployee.UserName
                    .EmailAddress = bEmployee.EmailAddress
                    .IsForcePasswordChange = bEmployee.IsForcePasswordChange
                    .WorkScheduleId = bEmployee.WorkScheduleId
                    .ImageFileName = bEmployee.ImageFileName
                    .IsAllowOvertime = bEmployee.IsAllowOvertime
                    .IsExempt = bEmployee.IsExempt
                End With
            End If

            Return eEmployee
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal FirstName As String,
        ByVal MiddleName As String,
        ByVal LastName As String,
        ByVal EmployeeNameEnglish As String,
         ByVal GovId As String,
        ByVal EmployeeNo As String,
        ByVal MobileNo As String,
        ByVal Sex As Integer,
        ByVal ContractType As Integer,
        ByVal Employer As Integer,
        ByVal ManagerId As Integer,
        ByVal JobTitle As String,
        ByVal PositionTypeId As Integer,
        ByVal PositionId As Integer,
        ByVal BadgeNo As String,
        ByVal DepartmentId As Integer,
        ByVal NationalityId As Integer,
        ByVal HireDate As Date,
        ByVal IsActive As Boolean,
        ByVal IsFingerRegistered As Boolean,
        ByVal Picture As Byte(),
        ByVal ImageFileName As String,
        ByVal ActionId As Integer,
        ByVal IsAllowOvertime As Boolean,
        ByVal IsExempt As Boolean,
        ByVal LocationId As Integer,
        ByVal RoleId As Integer,
        ByVal UserName As String,
        ByVal Password As String,
        ByVal EmailAddress As String,
        ByVal IsForcePasswordChange As Boolean,
        ByVal WorkScheduleId As Integer,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            If Password <> "" Then
                Password = UIUtilities.EncryptPasswordInHash(Password)
            End If

            Dim oEmployee As New DAEmployee
            If oEmployee.Add(
                             FirstName,
                             MiddleName,
                             LastName,
                             EmployeeNameEnglish,
                             GovId,
                             EmployeeNo,
                             MobileNo,
                             Sex,
                             ContractType,
                             Employer,
                             ManagerId,
                             JobTitle,
                             PositionTypeId,
                             PositionId,
                             BadgeNo,
                             DepartmentId,
                             NationalityId,
                             HireDate,
                             IsActive,
                             IsFingerRegistered,
                             Picture,
                             ImageFileName,
                             ActionId,
                             IsAllowOvertime,
                             IsExempt,
                             LocationId,
                             RoleId,
                             UserName,
                             Password,
                             EmailAddress,
                             IsForcePasswordChange,
                             WorkScheduleId,
                             UserAccountId) Then
                boolSeccessed = True
                MyBase.Identity = oEmployee.Identity
                MyBase.InfoMessage = oEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
        Public Function Update(
        ByVal EmployeeId As Integer,
        ByVal FirstName As String,
        ByVal MiddleName As String,
        ByVal LastName As String,
         ByVal EmployeeNameEnglish As String,
         ByVal GovId As String,
        ByVal EmployeeNo As String,
        ByVal MobileNo As String,
        ByVal Sex As Integer,
        ByVal ContractType As Integer,
        ByVal Employer As Integer,
        ByVal ManagerId As Integer,
        ByVal JobTitle As String,
        ByVal PositionTypeId As Integer,
        ByVal PositionId As Integer,
        ByVal BadgeNo As String,
        ByVal DepartmentId As Integer,
        ByVal NationalityId As Integer,
        ByVal HireDate As Date,
        ByVal IsActive As Boolean,
        ByVal IsFingerRegistered As Boolean,
        ByVal Picture As Byte(),
        ByVal ImageFileName As String,
        ByVal ActionId As Integer,
        ByVal IsAllowOvertime As Boolean,
         ByVal IsExempt As Boolean,
        ByVal LocationId As Integer,
        ByVal RoleId As Integer,
        ByVal UserName As String,
        ByVal Password As String,
        ByVal EmailAddress As String,
        ByVal IsForcePasswordChange As Boolean,
        ByVal WorkScheduleId As Integer,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            If Password <> "" Then
                Password = UIUtilities.EncryptPasswordInHash(Password)
            End If

            Dim oEmployee As New DAEmployee
            If oEmployee.Update(
                             EmployeeId,
                             FirstName,
                             MiddleName,
                             LastName,
                             EmployeeNameEnglish,
                             GovId,
                             EmployeeNo,
                             MobileNo,
                             Sex,
                             ContractType,
                             Employer,
                             ManagerId,
                             JobTitle,
                             PositionTypeId,
                             PositionId,
                             BadgeNo,
                             DepartmentId,
                             NationalityId,
                             HireDate,
                             IsActive,
                             IsFingerRegistered,
                             Picture,
                             ImageFileName,
                             ActionId,
                             IsAllowOvertime,
                             IsExempt,
                             LocationId,
                             RoleId,
                             UserName,
                             Password,
                             EmailAddress,
                             IsForcePasswordChange,
                             WorkScheduleId,
                             UserAccountId,
                             VersionNo) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function

        Public Function UpdateEmployeeManager(ByVal EmployeeId As Integer,
                                             ByVal ManagerId As Integer,
                                             ByVal UserId As Integer) As Boolean
            Dim oEmployee As New DAEmployee
            Return oEmployee.UpdateEmployeeManager(EmployeeId, ManagerId, UserId)
        End Function

        Public Function UpdatePasswordVerificationCodeByEmployeeId(ByVal EmployeeId As Integer, ByVal PasswordVerificationCode As Guid) As Boolean
            Dim oEmployee As New DAEmployee
            Return oEmployee.UpdatePasswordVerificationCodeByEmployeeId(EmployeeId, PasswordVerificationCode)
        End Function

        Public Function UpdatePasswordReset(ByVal EmailAddress As String, ByVal NewPassword As String) As Boolean
            Dim oEmployee As New DAEmployee
            Dim EncryptedNewPass As String = UIUtilities.EncryptPasswordInHash(NewPassword)
            Return oEmployee.UpdatePasswordReset(EmailAddress, EncryptedNewPass)
        End Function

        Public Function GetDepartmentEmployeesDataset( _
        ByVal DepartmentId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetDepartmentEmployeesDataset(DepartmentId, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function

        Public Function GetEmployeesByDepartmentAndContractTypeAndEmployerDataset(ByVal DepartmentId As Integer, ByVal Employer As Integer, ByVal ContractType As Integer, Optional ByVal lang As String = "ar") As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetEmployeesByDepartmentAndContractTypeAndEmployerDataset(DepartmentId, Employer, ContractType, lang)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function


        Public Function GetEmployeeManager(ByVal ManagerId As Integer) As String
            Return New DAEmployee().GetEmployeeManager(ManagerId)
        End Function
        Public Function GetAllBehaviorActionsDataset() As System.Data.DataSet
            Return New DAEmployee().GetAllBehaviorActionsDataset
        End Function
        Public Function GetUnregisteredEmployees(ByVal DepartmentId As Integer, ByVal Op As Integer, Optional ByVal lang As String = "ar") As System.Data.DataSet
            Return New DAEmployee().GetUnregisteredEmployees(DepartmentId, Op, lang)
        End Function
        Public Function GetGatepassApprovals(ByVal UserAccountId As Integer) As System.Data.DataSet
            Return New DAEmployee().GetGatepassApprovals(UserAccountId)
        End Function

        Public Function GetEmployeeByUserName(ByVal UserName As String) As System.Data.DataSet
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetEmployeeByUserName(UserName)
        End Function

        Public Function GetEmployeeByUserNameAndPassword(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Pass As String = UIUtilities.EncryptPasswordInHash(Password)
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetEmployeeByUserNameAndPassword(UserName, Pass)
        End Function

        Public Function GetEmployeeByUserNameAndPassword1(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Pass As String = UIUtilities.EncryptPasswordInHash(Password)
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetEmployeeByUserNameAndPassword1(UserName, Pass)
        End Function

        Public Function GetEmployeeByEmail(ByVal EmailAddress As String) As System.Data.DataSet
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetEmployeeByEmail(EmailAddress)
        End Function

        Public Function GetEmployeeByPasswordVerificationCode(ByVal PasswordVerificationCode As Guid) As System.Data.DataSet
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetEmployeeByPasswordVerificationCode(PasswordVerificationCode)
        End Function

        Public Function GetEmployeesByManagerDataset( _
        ByVal ManagerId As Integer, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Dim ds As DataSet = Nothing

            Dim oEmployee As New DAEmployee
            ds = oEmployee.GetEmployeesByManagerDataset(ManagerId, PageNo, PageSize)
            MyBase.PageTotal = oEmployee.PageTotal

            Return ds
        End Function

        Public Function GetAuthorizedEmployeeById(ByVal EmployeeId As Integer) As System.Data.DataSet
            Dim oEmployee As New DAEmployee
            Return oEmployee.GetAuthorizedEmployeeById(EmployeeId)
        End Function

        Public Function UnAuthorizeEmployee(ByVal AuthorizorEmployeeId As Integer, ByVal AuthorizedEmployeeId As Integer) As Boolean
            Dim oEmployee As New DAEmployee
            Return oEmployee.UnAuthorizeEmployee(AuthorizorEmployeeId, AuthorizedEmployeeId)
        End Function

        Public Function AuthorizeEmployee(ByVal AuthorizorEmployeeId As Integer, ByVal AuthorizedEmployeeId As Integer) As Boolean
            Dim oEmployee As New DAEmployee
            Return oEmployee.AuthorizeEmployee(AuthorizorEmployeeId, AuthorizedEmployeeId)
        End Function

        Public Function FileUploadForProfile(ByVal objFileUpload As FileUpload, ByVal AccountEmployeeId As Integer) As Boolean
            Try

           
            If objFileUpload.FileName = "" Then
                Return True
            End If

            Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")

            Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
            If Not System.IO.Directory.Exists(strRoot) Then
                System.IO.Directory.CreateDirectory(strRoot)
            End If
            Dim strAccountPath As String = strRoot & DBUtilities.GetSessionAccountId & "\" & AccountEmployeeId & "\"
            If Not System.IO.Directory.Exists(strAccountPath) Then
                System.IO.Directory.CreateDirectory(strAccountPath)
            End If
            Dim strLogoPath As String = strAccountPath & "Profile" & "\"
            If Not System.IO.Directory.Exists(strLogoPath) Then
                System.IO.Directory.CreateDirectory(strLogoPath)
            End If
            Dim strpath As String = strLogoPath & "E-Profile.gif"
            'If System.IO.Directory.Exists(strpath) Then
            '    System.IO.File.Delete(strpath)
            'End If
            Dim FileToSave As String = strLogoPath & "E-Profile.gif" 'objFileUpload.FileName
                objFileUpload.SaveAs(FileToSave)

            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function

        Public Function CheckIsthereNotificationsNotReceipt(ByVal EmployeeId As Integer) As Boolean
            Dim oEmployee As New DAEmployee
            Return oEmployee.CheckIsthereNotificationsNotReceipt(EmployeeId)
        End Function


#End Region

#Region " Save "
        Public Function Save(ByVal eEmpoyee As BOEEmployee, ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Dim intIdentity As Integer = 0
            Dim intUserIdentity As Integer = 0

            Using TS As New TransactionScope

                Select Case eEmpoyee.DBAction
                    Case BOEntityBase.DBActionEnum.Add
                        If Not Me.Add(
                               eEmpoyee.FirstName,
                              eEmpoyee.MiddleName,
                              eEmpoyee.LastName,
                              eEmpoyee.EmployeeNameEnglish,
                              eEmpoyee.GovId,
                              eEmpoyee.EmployeeNo,
                              eEmpoyee.MobileNo,
                              eEmpoyee.Sex,
                              eEmpoyee.ContractType,
                              eEmpoyee.Employer,
                              eEmpoyee.ManagerId,
                              eEmpoyee.JobTitle,
                              eEmpoyee.PositionTypeId,
                              eEmpoyee.PositionId,
                              eEmpoyee.BadgeNo,
                              eEmpoyee.DepartmentId,
                              eEmpoyee.NationalityId,
                              eEmpoyee.HireDate,
                              eEmpoyee.IsActive,
                              eEmpoyee.IsFingerRegistered,
                              eEmpoyee.Picture,
                              eEmpoyee.ImageFileName,
                              eEmpoyee.ActionId,
                              eEmpoyee.IsAllowOvertime,
                               eEmpoyee.IsExempt,
                              eEmpoyee.LocationId,
                              eEmpoyee.RoleId,
                              eEmpoyee.UserName,
                               eEmpoyee.Password,
                              eEmpoyee.EmailAddress,
                              eEmpoyee.IsForcePasswordChange,
                              eEmpoyee.WorkScheduleId,
                              UserAccountId) Then
                            Return False
                        Else
                            intIdentity = Me.Identity
                            Dim oUserAccount As New DAUserAccount
                            If Not oUserAccount.Add(
                                   eEmpoyee.UserAccount.windowsAccountName,
                                   eEmpoyee.EmployeeName,
                                   eEmpoyee.UserAccount.Email,
                                   eEmpoyee.UserAccount.IsActive,
                                   intIdentity, _
                                   UserAccountId) Then
                                MyBase.InfoMessage = oUserAccount.InfoMessage
                                Return False
                            End If
                        End If

                    Case BOEntityBase.DBActionEnum.Update
                        If Not Me.Update(
                              eEmpoyee.EmployeeId,
                              eEmpoyee.FirstName,
                              eEmpoyee.MiddleName,
                              eEmpoyee.LastName,
                              eEmpoyee.EmployeeNameEnglish,
                               eEmpoyee.GovId,
                              eEmpoyee.EmployeeNo,
                              eEmpoyee.MobileNo,
                              eEmpoyee.Sex,
                              eEmpoyee.ContractType,
                              eEmpoyee.Employer,
                              eEmpoyee.ManagerId,
                              eEmpoyee.JobTitle,
                              eEmpoyee.PositionTypeId,
                              eEmpoyee.PositionId,
                              eEmpoyee.BadgeNo,
                              eEmpoyee.DepartmentId,
                              eEmpoyee.NationalityId,
                              eEmpoyee.HireDate,
                              eEmpoyee.IsActive,
                              eEmpoyee.IsFingerRegistered,
                              eEmpoyee.Picture,
                              eEmpoyee.ImageFileName,
                              eEmpoyee.ActionId,
                              eEmpoyee.IsAllowOvertime,
                               eEmpoyee.IsExempt,
                              eEmpoyee.LocationId,
                              eEmpoyee.RoleId,
                              eEmpoyee.UserName,
                              eEmpoyee.Password,
                              eEmpoyee.EmailAddress,
                              eEmpoyee.IsForcePasswordChange,
                              eEmpoyee.WorkScheduleId,
                              UserAccountId,
                              eEmpoyee.VersionNo) Then
                            Return False
                        Else
                            Dim oUserAccount As New DAUserAccount
                            If Not oUserAccount.Update( _
                                   eEmpoyee.UserAccount.UserAccountId, _
                                   eEmpoyee.UserAccount.windowsAccountName, _
                                   eEmpoyee.EmployeeName, _
                                   eEmpoyee.UserAccount.Email, _
                                   eEmpoyee.UserAccount.IsActive, _
                                   UserAccountId, _
                                   eEmpoyee.UserAccount.VersionNo) Then
                                MyBase.InfoMessage = oUserAccount.InfoMessage
                                Return False
                            End If
                        End If

                    Case BOEntityBase.DBActionEnum.Delete
                        If Not Me.Delete(eEmpoyee.EmployeeId) Then
                            Return False
                        End If
                End Select

                TS.Complete()
                boolSeccessed = True
            End Using

            Return boolSeccessed
        End Function
#End Region

#Region " Miscellaneous "
        Private intFilterOption As enumFilterOption
        Public Enum enumFilterOption As Integer
            AllEmployees = 1
            ALLCivilians = 2
            AllMilitaryEmployees = 3
            MilitaryOfficersOnly = 4
            MilitaryNoneOfficersOnly = 5
        End Enum
        Private intSortOption As enumSortOption
        Public Enum enumSortOption As Integer
            OrderByEmployeeName = 1
            OrderByRankPostion = 2
            OrderByEmployeeId = 3
        End Enum
        Public Property FilterOption() As enumFilterOption
            Get
                Return intFilterOption
            End Get
            Set(ByVal value As enumFilterOption)
                intFilterOption = value
            End Set
        End Property
        Public Property SortOption() As enumSortOption
            Get
                Return intSortOption
            End Get
            Set(ByVal value As enumSortOption)
                intSortOption = value
            End Set
        End Property


#End Region

#Region "Security"
        Public Function ValidateLogin(ByVal Username As String, ByVal Password As String) As Boolean

            'If Now.Date > #4/20/2020# Then
            '    Return False
            'End If

            Dim dsEmployee As New System.Data.DataSet

            If Username.ToLower <> "systemadmin" Then
                If New LDAPUtilitiesBLL().IsAspNetActiveDirectoryMembershipProvider = True Then
                    dsEmployee = GetEmployeeByUserName(Username)
                Else
                    dsEmployee = GetEmployeeByUserNameAndPassword(Username, Password)
                End If

                If Not IsNothing(dsEmployee) AndAlso dsEmployee.Tables.Count > 0 AndAlso dsEmployee.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = dsEmployee.Tables(0).Rows(0)
                    'System.Web.HttpContext.Current.Session.Remove("RootNode")
                    'System.Web.HttpContext.Current.Session.Remove("PermissionTable")

                    'Call New AccountPagePermissionBLL().ResetPageSecurity()


                    'If CheckPasswordChangePolicy(AccountEmployeeRow) Then
                    '    Return False
                    '  End If

                    System.Web.HttpContext.Current.Session.Add("AccountId", 1)

                    'If Not LicensingBLL.CheckLicenseAndRedirectToLicensePage(AccountEmployeeRow.AccountEmployeeId, AccountEmployeeRow.AccountId, AccountEmployeeRow.Item("AccountExpiryActivation"), AccountEmployeeRow.Item("LicenseActivation")) Then
                    '    Return False
                    'End If

                    System.Web.HttpContext.Current.Response.Cookies("AccountEmployeeId").Value = CStr(dr("EmployeeId"))
                    System.Web.HttpContext.Current.Session.Add("AccountEmployeeId", dr("EmployeeId"))

                    System.Web.HttpContext.Current.Session.Add("CountryId", dr("NationalityId"))


                    System.Web.HttpContext.Current.Session.Add("AccountDepartmentId", dr("DepartmentId"))

                    Me.SetSessionValues(dr)

                    If CBool(dr("IsForcePasswordChange")) Then
                        System.Web.HttpContext.Current.Response.Redirect("~/Authenticate/PasswordChange.aspx?Username=" & CStr(dr("UserName")), False)
                        Return False
                    End If

                    Dim str As String
                    str = System.Web.HttpContext.Current.Request.UserHostAddress()
                    If CStr(dr("AllowedAccessFromIP")) = str Or CStr(dr("AllowedAccessFromIP")) = "" Then
                        Return True
                    End If

                End If
            ElseIf Username.ToLower = "systemadmin" Then
                If System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Is Nothing OrElse System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") = "" Then
                    Return True
                ElseIf UIUtilities.EncryptPasswordInHash(Password) = System.Configuration.ConfigurationManager.AppSettings("SystemSettingPassword") Then
                    Return True
                Else
                    Return False
                End If
            Else

                Return False
            End If
        End Function

        Public Sub SetSessionValues(ByVal drEm As DataRow)


            System.Web.HttpContext.Current.Session.Add("AccountDepartmentId1", 1)
            System.Web.HttpContext.Current.Session.Add("EmployeeManagerId", drEm("ManagerId"))
            'System.Web.HttpContext.Current.Session.Add("CultureInfoName", AccountEmployeeRow.CultureInfoName)


            System.Web.HttpContext.Current.Session.Add("AccountEmployeeFullName", drEm("FirstName").ToString & " " & drEm("LastName").ToString)


            System.Web.HttpContext.Current.Session.Add("CountryId", drEm("NationalityId"))
            System.Web.HttpContext.Current.Session.Add("AccountRoleId", drEm("RoleId"))
            System.Web.HttpContext.Current.Session.Add("Role", drEm("RoleName"))

            System.Web.HttpContext.Current.Session.Add("EmailAddress", drEm("EmailAddress"))

            System.Web.HttpContext.Current.Session.Timeout = 120

            System.Web.HttpContext.Current.Session.Add("PageSize", 10)

            System.Web.HttpContext.Current.Session.Add("EmployeeNameWithCode", drEm("FirstName").ToString & " " & drEm("LastName").ToString & " - " & drEm("BadgeNo").ToString)

            System.Web.HttpContext.Current.Session.Add("CreatedOn", drEm("AddedDate"))


            ' Update Features of hosted
            'LicensingBLL.UpdateFeaturesOfAccounts()
        End Sub

        Public Function ChangePassword(ByVal EmployeeId As Integer,
                                       ByVal Password As String,
                                       Optional ByVal EmailAddress As String = "") As Boolean
            Dim boolSeccessed As Boolean = False

            Dim Pass As String = UIUtilities.EncryptPasswordInHash(Password)

            Dim oEmployee As New DAEmployee
            If oEmployee.ChangePassword(EmployeeId,
                                         Pass,
                                         EmailAddress) Then
                boolSeccessed = True
                MyBase.InfoMessage = oEmployee.InfoMessage
            Else
                boolSeccessed = False
                MyBase.InfoMessage = oEmployee.InfoMessage
            End If

            Return boolSeccessed
        End Function
#End Region

    End Class

#End Region

#Region " BOEEmployee "

    Public Class BOEEmployee
        Inherits BOEntityBase

#Region " Constructor "
        Public Sub New()
            oUserAccount = New BOEUserAccount
        End Sub
#End Region

#Region " Private Variables  "
        Private intEmployeeId As Integer
        Private strEmployeeName As String
        Private strEmployeeNameEnglish As String
        Private intManagerId As Integer
        Private strJobTitle As String
        Private intPositionTypeId As Integer
        Private intPositionId As Integer
        Private strBadgeNo As String
        Private intDepartmentId As Integer
        Private intNationalityId As Integer
        Private datHireDate As Date
        Private bolIsActive As Boolean
        Private bytPicture As Byte()
        Private strImageFileName As String
        Private intActionId As Integer
        Private bolIsFingerRegistered As Boolean
        Private bolIsAllowOvertime As Boolean
        Private bolIsExempt As Boolean
        Private strFirstName As String
        Private strMiddleName As String
        Private strLastName As String
        Private strGovId As String
        Private strEmployeeNo As String
        Private strMobileNo As String
        Private intSex As Integer
        Private intEmployer As Integer
        Private intContractType As Integer
        Private intRoleId As Integer
        Private intLocationId As Integer
        Private strUserName As String
        Private strPassword As String
        Private strAllowedAccessFromIP As String
        Private bolIsForcePasswordChange As Boolean
        Private strEmailAddress As String
        Private intWorkScheduleId As Integer

#End Region

#Region " Public Properties "
        Public Property EmployeeId() As Integer
            Get
                Return intEmployeeId
            End Get
            Set(ByVal value As Integer)
                intEmployeeId = value
            End Set
        End Property

        Public Property ActionId() As Integer
            Get
                Return intActionId
            End Get
            Set(ByVal value As Integer)
                intActionId = value
            End Set
        End Property
        Public Property IsFingerRegistered() As Boolean
            Get
                Return bolIsFingerRegistered
            End Get
            Set(ByVal value As Boolean)
                bolIsFingerRegistered = value
            End Set
        End Property
        Public Property EmployeeName() As String
            Get
                Return strEmployeeName
            End Get
            Set(ByVal value As String)
                strEmployeeName = value
            End Set
        End Property
        Public Property EmployeeNameEnglish() As String
            Get
                Return strEmployeeNameEnglish
            End Get
            Set(ByVal value As String)
                strEmployeeNameEnglish = value
            End Set
        End Property
        Public Property ManagerId() As Integer
            Get
                Return intManagerId
            End Get
            Set(ByVal value As Integer)
                intManagerId = value
            End Set
        End Property
        Public Property JobTitle() As String
            Get
                Return strJobTitle
            End Get
            Set(ByVal value As String)
                strJobTitle = value
            End Set
        End Property
        Public Property PositionTypeId() As Integer
            Get
                Return intPositionTypeId
            End Get
            Set(ByVal value As Integer)
                intPositionTypeId = value
            End Set
        End Property
        Public Property PositionId() As Integer
            Get
                Return intPositionId
            End Get
            Set(ByVal value As Integer)
                intPositionId = value
            End Set
        End Property
        Public Property BadgeNo() As String
            Get
                Return strBadgeNo
            End Get
            Set(ByVal value As String)
                strBadgeNo = value
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
        Public Property NationalityId() As Integer
            Get
                Return intNationalityId
            End Get
            Set(ByVal value As Integer)
                intNationalityId = value
            End Set
        End Property
        Public Property HireDate() As Date
            Get
                Return datHireDate
            End Get
            Set(ByVal value As Date)
                datHireDate = value
            End Set
        End Property
        Public Property IsActive() As Boolean
            Get
                Return bolIsActive
            End Get
            Set(ByVal value As Boolean)
                bolIsActive = value
            End Set
        End Property
        Public Property Picture() As Byte()
            Get
                Return bytPicture
            End Get
            Set(ByVal value As Byte())
                bytPicture = value
            End Set
        End Property
        Public Property ImageFileName() As String
            Get
                Return strImageFileName
            End Get
            Set(ByVal value As String)
                strImageFileName = value
            End Set
        End Property
        Public Property IsAllowOvertime() As Boolean
            Get
                Return bolIsAllowOvertime
            End Get
            Set(ByVal value As Boolean)
                bolIsAllowOvertime = value
            End Set
        End Property

        Public Property IsExempt() As Boolean
            Get
                Return bolIsExempt
            End Get
            Set(ByVal value As Boolean)
                bolIsExempt = value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return strFirstName
            End Get
            Set(ByVal value As String)
                strFirstName = value
            End Set
        End Property

        Public Property MiddleName() As String
            Get
                Return strMiddleName
            End Get
            Set(ByVal value As String)
                strMiddleName = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return strLastName
            End Get
            Set(ByVal value As String)
                strLastName = value
            End Set
        End Property

        Public Property GovId() As String
            Get
                Return strGovId
            End Get
            Set(ByVal value As String)
                strGovId = value
            End Set
        End Property

        Public Property EmployeeNo() As String
            Get
                Return strEmployeeNo
            End Get
            Set(ByVal value As String)
                strEmployeeNo = value
            End Set
        End Property

        Public Property MobileNo() As String
            Get
                Return strMobileNo
            End Get
            Set(ByVal value As String)
                strMobileNo = value
            End Set
        End Property


        Public Property Sex() As Integer
            Get
                Return intSex
            End Get
            Set(ByVal value As Integer)
                intSex = value
            End Set
        End Property

        Public Property ContractType() As Integer
            Get
                Return intContractType
            End Get
            Set(ByVal value As Integer)
                intContractType = value
            End Set
        End Property

        Public Property Employer() As Integer
            Get
                Return intEmployer
            End Get
            Set(ByVal value As Integer)
                intEmployer = value
            End Set
        End Property

        Public Property IsForcePasswordChange() As Boolean
            Get
                Return bolIsForcePasswordChange
            End Get
            Set(ByVal value As Boolean)
                bolIsForcePasswordChange = value
            End Set
        End Property

        Public Property AllowedAccessFromIP() As String
            Get
                Return strAllowedAccessFromIP
            End Get
            Set(ByVal value As String)
                strAllowedAccessFromIP = value
            End Set
        End Property

        Public Property EmailAddress() As String
            Get
                Return strEmailAddress
            End Get
            Set(ByVal value As String)
                strEmailAddress = value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return strUserName
            End Get
            Set(ByVal value As String)
                strUserName = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return strPassword
            End Get
            Set(ByVal value As String)
                strPassword = value
            End Set
        End Property

        Public Property LocationId() As Integer
            Get
                Return intLocationId
            End Get
            Set(ByVal value As Integer)
                intLocationId = value
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

        Public Property WorkScheduleId() As Integer
            Get
                Return intWorkScheduleId
            End Get
            Set(ByVal value As Integer)
                intWorkScheduleId = value
            End Set
        End Property

        Dim oUserAccount As BOEUserAccount
        Public Property UserAccount() As BOEUserAccount
            Get
                Return oUserAccount
            End Get
            Set(ByVal value As BOEUserAccount)
                oUserAccount = value
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
            Dim eEmployee As Employee = CType(entity, Employee)
            With eEmployee
                intEmployeeId = .EmployeeId
                strEmployeeName = .EmployeeName
                strEmployeeNameEnglish = .EmployeeNameEnglish
                intManagerId = .ManagerId
                strJobTitle = .JobTitle
                intPositionTypeId = .PositionTypeId
                intPositionId = .PositionId
                strBadgeNo = .BadgeNo
                intDepartmentId = .DepartmentId
                intNationalityId = .NationalityId
                datHireDate = .HireDate
                bolIsActive = .IsActive
                bytPicture = .Picture
                strImageFileName = .ImageFileName
                bolIsFingerRegistered = .IsFingerRegistered
                intActionId = .ActionId
                bolIsAllowOvertime = .IsAllowOvertime
                bolIsExempt = .IsExempt

                strFirstName = .FirstName
                strMiddleName = .MiddleName
                strLastName = .LastName
                strGovId = .GovId
                strEmployeeNo = .EmployeeNo
                strMobileNo = .MobileNo
                intSex = .Sex
                intEmployer = .Employer
                intContractType = .ContractType

                intRoleId = .RoleId
                intLocationId = .LocationId
                strUserName = .UserName
                strPassword = .Password
                strEmailAddress = .EmailAddress
                strAllowedAccessFromIP = .AllowedAccessFromIP
                bolIsForcePasswordChange = .IsForcePasswordChange
                intWorkScheduleId = .WorkScheduleId
            End With
        End Sub
#End Region

    End Class

#End Region

End Namespace


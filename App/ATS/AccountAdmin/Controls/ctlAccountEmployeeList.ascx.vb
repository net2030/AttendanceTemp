Imports System.DirectoryServices
Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class Controls_ctlAccountEmployeeList
    Inherits System.Web.UI.UserControl

#Region "General declaration"
    Dim moVersionNo As Byte()
    Dim moEmployee As BOEmployee = New BOEmployee
    Dim eEmployee As New BOEEmployee

    Public EmployeeId As String

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Try
                GridViewDataBinding()
                GridView1.DataBind()
            Catch ex As Exception

            End Try
            
            '   Me.ShowbtnGetUserData()
        Else
            ' Me.ShowbtnGetUserData()
        End If

    End Sub
#End Region

#Region "Data Binding"
    Protected Sub GridViewDataBinding()
        'If Session("Role") = "Administrator" Then
        '    'dsAccountEmployeeList.SelectParameters.Item("EmployeeID").DefaultValue = "0"
        'Else
        '    'dsAccountEmployeeList.SelectParameters.Item("EmployeeID").DefaultValue = Session("AccountEmployeeId")
        'End If
    End Sub
#End Region

#Region "Form Events"
    Protected Sub FormView1_DataBound(sender As Object, e As EventArgs) Handles FormView1.DataBound
        Try
            If FormView1.CurrentMode = FormViewMode.Insert Then
                CType(Me.FormView1.FindControl("ddlAccountRoleId"), DropDownList).SelectedValue = 2
                CType(Me.FormView1.FindControl("ctlDepartmentTree1"), ctlDepartmentTree).SelectedValue = Session("AccountDepartmentId")

                If Session("AccountRoleId") <> 1 Then
                    CType(Me.FormView1.FindControl("ctlDepartmentTree1"), ctlDepartmentTree).Enabled = False
                End If



            End If
            'ATS.BO.BOPagePermission.SetPagePermission(4, Me.GridView1, Me.FormView1, "Add", 6, 7)
            If FormView1.CurrentMode = FormViewMode.Edit Then

                If Not IsNothing(eEmployee.VersionNo) Then
                    System.Web.HttpContext.Current.Session.Add("VersionNo", eEmployee.VersionNo)
                End If

                If Not IsNothing(eEmployee.DepartmentId) Then
                    CType(Me.FormView1.FindControl("ctlDepartmentTree1"), ctlDepartmentTree).SelectedValue = eEmployee.DepartmentId
                End If

                If Not IsNothing(eEmployee.WorkScheduleId) AndAlso eEmployee.WorkScheduleId > 0 Then
                    CType(Me.FormView1.FindControl("ddlWorkSchedules"), DropDownList).SelectedValue = eEmployee.WorkScheduleId
                End If

                If Not IsNothing(eEmployee.ManagerId) Then
                    Dim EmployeesList As New List(Of BOEEmployee)()
                    Dim eEmployee1 As New BOEEmployee
                    eEmployee1 = moEmployee.Find(eEmployee.ManagerId)
                    EmployeesList.Add(eEmployee1)

                    CType(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList).DataSource = EmployeesList
                    CType(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList).DataBind()
                    CType(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList).SelectedValue = eEmployee.ManagerId
                End If

                EmployeeId = eEmployee.EmployeeId
                System.Web.HttpContext.Current.Session.Add("EmployeeId1", eEmployee.EmployeeId)
            End If

        Catch ex As Exception

        End Try
    End Sub



#End Region

#Region "Others Controls Event"

    Protected Sub btnAddEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEmployee.Click
        Me.FormView1.Visible = True

        'Me.FormView1.DataBind()
        Me.GridView1.Visible = False
        Me.btnAddEmployee.Visible = False
        Me.btnDeleteSelectedItem.Visible = False
        'ddlDepartment = TryCast(FormView1.FindControl("ddlAccountDepartmentId"), DropDownList)
        'ddlDepartment1 = TryCast(FormView1.FindControl("ddlAccountDepartmentId1"), DropDownList)
        'ShowTreeNodes(0)
    End Sub

    Public Sub NewEmployee()
        Me.FormView1.ChangeMode(FormViewMode.Insert)
        Me.FormView1.DataBind()
    End Sub

    Public Sub ShowbtnGetUserData()
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
        Try
            If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
                CType(Me.FormView1.FindControl("btnGetUserData"), Button).Visible = True
            Else
                If Not CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox) Is Nothing Then
                    CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).ReadOnly = True
                    CType(Me.FormView1.FindControl("btnGetUserData"), Button).Visible = False
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnGetUserData_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.GetUserData()
    End Sub

    Protected Sub EMailAddressTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.FillUsername()
    End Sub

    Protected Sub Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.FormView1.Visible = False
        Me.GridView1.Visible = True
        Me.btnAddEmployee.Visible = True
        ' Me.btnDeleteSelectedItem.Visible = True
    End Sub

    Protected Sub btnDeleteSelectedItem_Click(sender As Object, e As System.EventArgs) Handles btnDeleteSelectedItem.Click
        'Dim row As GridViewRow
        'Dim Original_AccountEmployeeId As Integer
        'For Each row In Me.GridView1.Rows
        '    If Me.GridView1.DataKeys(row.RowIndex)(0) <> 0 Then
        '        If CType(row.FindControl("chkSelect"), CheckBox).Checked = True Then
        '            Original_AccountEmployeeId = Me.GridView1.DataKeys(row.RowIndex)(0)
        '            Dim BLL As New AccountEmployeeBLL
        '            If Original_AccountEmployeeId <> DBUtilities.GetSessionAccountEmployeeId Then
        '                Original_AccountEmployeeId = New AccountEmployeeBLL().DeleteAccountEmployee(Original_AccountEmployeeId)
        '            End If
        '        End If
        '    End If
        'Next
        'Me.GridView1.DataBind()
    End Sub

#End Region

#Region "common Methods"

    Private Function FillObject() As BOEEmployee
        Dim eEmployee As New BOEEmployee
        Try

            With eEmployee
                If DirectCast(FormView1.FindControl("txtAccountEmployeeId"), TextBox).Text.Trim <> "" Then
                    .EmployeeId = DirectCast(FormView1.FindControl("txtAccountEmployeeId"), TextBox).Text
                End If

                .BadgeNo = CType(Me.FormView1.FindControl("EmployeeCodeTextBox"), TextBox).Text
                .UserName = CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).Text
                .FirstName = CType(Me.FormView1.FindControl("FirstNameTextBox"), TextBox).Text
                .MiddleName = CType(Me.FormView1.FindControl("MiddleNameTextBox"), TextBox).Text
                .LastName = CType(Me.FormView1.FindControl("LastNameTextBox"), TextBox).Text
                .EmployeeNameEnglish = CType(Me.FormView1.FindControl("EmployeeNameEnglishTextBox"), TextBox).Text
                .GovId = CType(Me.FormView1.FindControl("txtGovId"), TextBox).Text
                .EmployeeNo = CType(Me.FormView1.FindControl("txtEmployeeNo"), TextBox).Text
                .MobileNo = CType(Me.FormView1.FindControl("txtMobileNo"), TextBox).Text
                .Sex = CType(Me.FormView1.FindControl("ddlSex"), DropDownList).SelectedValue
                .ContractType = CType(Me.FormView1.FindControl("ddlContractType"), DropDownList).SelectedValue
                .Employer = CType(Me.FormView1.FindControl("ddlEmployer"), DropDownList).SelectedValue

                .NationalityId = CType(Me.FormView1.FindControl("ddlCountryId"), DropDownList).SelectedValue
                .DepartmentId = CType(Me.FormView1.FindControl("ctlDepartmentTree1"), ctlDepartmentTree).SelectedValue
                .ManagerId = CType(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList).SelectedValue
                .HireDate = CType(Me.FormView1.FindControl("HiredDateCalendarPopup"), GeneralControls_MyDate).SelectedDate
                .EmailAddress = CType(Me.FormView1.FindControl("EMailAddressTextBox"), TextBox).Text
                .Password = CType(Me.FormView1.FindControl("PasswordTextBox"), TextBox).Text
                .JobTitle = CType(Me.FormView1.FindControl("JobTitleTextBox"), TextBox).Text
                .RoleId = CType(Me.FormView1.FindControl("ddlAccountRoleId"), DropDownList).SelectedValue
                .ManagerId = CType(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList).SelectedValue
                .LocationId = CType(Me.FormView1.FindControl("ddlAccountLocationId"), DropDownList).SelectedValue
                .IsActive = CType(Me.FormView1.FindControl("chkIsDisabled"), CheckBox).Checked
                .IsForcePasswordChange = CType(Me.FormView1.FindControl("chkForcePasswordChange"), CheckBox).Checked
                .AllowedAccessFromIP = CType(Me.FormView1.FindControl("AccessAllowedFromIPTextBox"), TextBox).Text
                .WorkScheduleId = CType(Me.FormView1.FindControl("ddlWorkSchedules"), DropDownList).SelectedValue
                .IsAllowOvertime = CType(Me.FormView1.FindControl("chkIsAllowOvertime"), CheckBox).Checked
                .IsExempt = CType(Me.FormView1.FindControl("chkIsExempt"), CheckBox).Checked

                '.PositionTypeId = CType(Me.FormView1.FindControl("ddlPositionType"), DropDownList).SelectedValue
                '.PositionId = CType(Me.FormView1.FindControl("ddlPosition"), DropDownList).SelectedValue

                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eEmployee
    End Function

    Protected Sub FindByEmployeeCode()
        Try


            Dim txtEmbloyeeCode As TextBox = DirectCast(Me.FormView1.FindControl("txtEmployeeCode"), TextBox)
            Dim ddlEmps As DropDownList = DirectCast(Me.FormView1.FindControl("ddlEmployeeManagerId"), DropDownList)



            Dim dt As DataTable = New DataTable
            Dim EmployeeCode As String = txtEmbloyeeCode.Text
            'Dim DepartmentId As Integer = Session("AccountDepartmentId1")
            dt = moEmployee.GetEmployeesByBadgeDataset(0, EmployeeCode, 1, 1).Tables(0)
            'dt = EmployeeDAL.GetEmployeeByEmployeeCode(EmployeeCode, DepartmentId)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then


                ddlEmps.DataSource = dt
                ddlEmps.DataBind()
                ddlEmps.SelectedValue = dt.Rows(0)("EmployeeId")
            Else

                ddlEmps.SelectedValue = 0

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

    Public Function Validate() As Boolean
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
        Dim username As String = CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).Text
        Dim password As String = CType(Me.FormView1.FindControl("PasswordTextBox"), TextBox).Text

        If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
            If LDAP.IsValidUserNameAndPassword(username, password) = True Then
                Return True
            Else
                Return False
            End If

        Else
            Return True
        End If
    End Function

    Public Function GetUserData() As Boolean
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL

        If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
            Dim Username As String = CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).Text
            Dim User As DirectoryEntry = LDAP.GetUserByName(Username)
            If Not User Is Nothing Then
                If Not User.Properties("givenname").Value Is Nothing Then
                    CType(Me.FormView1.FindControl("FirstNameTextBox"), TextBox).Text = User.Properties("givenname").Item(0)
                Else
                    CType(Me.FormView1.FindControl("FirstNameTextBox"), TextBox).Text = Username
                End If
                If Not User.Properties("sn").Value Is Nothing Then
                    CType(Me.FormView1.FindControl("LastNameTextBox"), TextBox).Text = User.Properties("sn").Item(0)
                Else
                    CType(Me.FormView1.FindControl("LastNameTextBox"), TextBox).Text = Username
                End If
                If Not User.Properties("mail").Value Is Nothing Then
                    CType(Me.FormView1.FindControl("EMailAddressTextBox"), TextBox).Text = User.Properties("mail").Item(0)
                Else
                    CType(Me.FormView1.FindControl("EMailAddressTextBox"), TextBox).Text = Username & "@" & Username & ".com"
                End If
                If Me.FormView1.CurrentMode = FormViewMode.Insert Then
                    CType(Me.FormView1.FindControl("PasswordTextBox"), TextBox).Text = Username
                    CType(Me.FormView1.FindControl("VerifyPasswordTextbox"), TextBox).Text = Username
                End If
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs)
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
        If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
            args.IsValid = Me.GetUserData = True
        End If
    End Sub

    Public Sub FillUsername()
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL
        If LDAP.IsAspNetActiveDirectoryMembershipProvider <> True Then
            'CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).Text = CType(Me.FormView1.FindControl("EMailAddressTextBox"), TextBox).Text
        End If
    End Sub

    Public Function CheckUserOnUpdate() As Boolean
        Dim LDAP As New ATS.BO.LDAPUtilitiesBLL

        If LDAP.IsAspNetActiveDirectoryMembershipProvider = True Then
            Dim Username As String = CType(Me.FormView1.FindControl("UsernameTextBox"), TextBox).Text
            Dim User As DirectoryEntry = LDAP.GetUserByName(Username)
            If Not User Is Nothing Then
                If Me.FormView1.CurrentMode = FormViewMode.Insert Then
                    CType(Me.FormView1.FindControl("PasswordTextBox"), TextBox).Text = Username
                    CType(Me.FormView1.FindControl("VerifyPasswordTextbox"), TextBox).Text = Username
                End If
                Return True
            Else
                Return False
            End If
        End If
    End Function
#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eEmployee As BOEEmployee = FillObject()

        moVersionNo = Session("VersionNo")

        With moEmployee
            If .Update(eEmployee.EmployeeId,
                       eEmployee.FirstName,
                            eEmployee.MiddleName,
                            eEmployee.LastName,
                             eEmployee.EmployeeNameEnglish,
                            eEmployee.GovId,
                            eEmployee.EmployeeNo,
                            eEmployee.MobileNo,
                            eEmployee.Sex,
                            eEmployee.ContractType,
                            eEmployee.Employer,
                            eEmployee.ManagerId,
                            eEmployee.JobTitle,
                            eEmployee.PositionTypeId,
                            eEmployee.PositionId,
                            eEmployee.BadgeNo,
                            eEmployee.DepartmentId,
                            eEmployee.NationalityId,
                            eEmployee.HireDate,
                            eEmployee.IsActive,
                            eEmployee.IsFingerRegistered,
                            eEmployee.Picture,
                            eEmployee.ImageFileName,
                            eEmployee.ActionId,
                            eEmployee.IsAllowOvertime,
                            eEmployee.IsExempt,
                            eEmployee.LocationId,
                            eEmployee.RoleId,
                            eEmployee.UserName,
                            eEmployee.Password,
                            eEmployee.EmailAddress,
                            eEmployee.IsForcePasswordChange,
                            eEmployee.WorkScheduleId,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEEmployee
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtEmployeeId.Text)), BOEEmployee)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                ' CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        Dim eEmployee As BOEEmployee = FillObject()



        With moEmployee
            If .Add(eEmployee.FirstName,
                            eEmployee.MiddleName,
                            eEmployee.LastName,
                             eEmployee.EmployeeNameEnglish,
                            eEmployee.GovId,
                            eEmployee.EmployeeNo,
                            eEmployee.MobileNo,
                            eEmployee.Sex,
                            eEmployee.ContractType,
                            eEmployee.Employer,
                            eEmployee.ManagerId,
                            eEmployee.JobTitle,
                            eEmployee.PositionTypeId,
                            eEmployee.PositionId,
                            eEmployee.BadgeNo,
                            eEmployee.DepartmentId,
                            eEmployee.NationalityId,
                            eEmployee.HireDate,
                            eEmployee.IsActive,
                            eEmployee.IsFingerRegistered,
                            eEmployee.Picture,
                            eEmployee.ImageFileName,
                            eEmployee.ActionId,
                            eEmployee.IsAllowOvertime,
                              eEmployee.IsExempt,
                            eEmployee.LocationId,
                            eEmployee.RoleId,
                            eEmployee.UserName,
                            eEmployee.Password,
                            eEmployee.EmailAddress,
                            eEmployee.IsForcePasswordChange,
                            eEmployee.WorkScheduleId,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEEmployee
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtEmployeeId.Text)), BOEEmployee)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
                System.Web.HttpContext.Current.Session.Add("EmployeeId1", .Identity)
                DirectCast(Me.FormView1.FindControl("BtnScope"), Button).Visible = True
                DirectCast(Me.FormView1.FindControl("btnManagerScope"), Button).Visible = True

            Else
                boolSeccessed = False 'BtnScope
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataDelete(ByVal id As Integer) As String
        moEmployee.Delete(id)
        Return moEmployee.InfoMessage
    End Function
#End Region

#Region "GridView Events"
    'Protected Sub GridView1_DataBound(sender As Object, e As System.EventArgs) Handles GridView1.DataBound


    '    'Get the header CheckBox
    '    Dim row As GridViewRow
    '    For Each row In Me.GridView1.Rows
    '        If Me.GridView1.DataKeys(row.RowIndex).Item(0) <> 0 Then
    '            ' Me.btnDeleteSelectedItem.Visible = True
    '            Dim cbHeader As CheckBox = CType(GridView1.HeaderRow.FindControl("chkCheckAll"), CheckBox)

    '            'Run the ChangeCheckBoxState client-side function whenever the
    '            'header checkbox is checked/unchecked
    '            cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked);"

    '            For Each gvr As GridViewRow In GridView1.Rows
    '                'Get a programmatic reference to the CheckBox control
    '                Dim cb As CheckBox = CType(gvr.FindControl("chkselect"), CheckBox)

    '                'Add the CheckBox's ID to the client-side CheckBoxIDs array
    '                ScriptManager.RegisterArrayDeclaration(Me, "CheckBoxIDs", String.Concat("'", cb.ClientID, "'"))
    '            Next
    '        End If
    '    Next


    'End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            Dim x As Integer = e.NewEditIndex
            Dim row As GridViewRow = GridView1.Rows(x)
            Dim EmployeesId As String = row.Cells(0).Text
            Dim EmployeesList As New List(Of BOEEmployee)()
            eEmployee = moEmployee.Find(EmployeesId)
            EmployeesList.Add(eEmployee)
            FormView1.DataSource = EmployeesList
            EmployeeId = eEmployee.EmployeeId
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()

            Me.FormView1.Visible = True
            Me.GridView1.Visible = False
            Me.btnAddEmployee.Visible = False
            Me.btnDeleteSelectedItem.Visible = False


        Catch ex As Exception
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(ex.Message), True)
            lblErr.Text = ex.Message
            GridView1.DataBind()
            'Dim x As String = ex.InnerException.Message
        End Try
    End Sub

    Protected Sub gvTreeNodes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView1.DataKeys(index)("EmployeeId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            'GridViewBinding()
        End If
    End Sub
#End Region


End Class




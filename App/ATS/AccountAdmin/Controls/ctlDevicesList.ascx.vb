Imports System.Data.SqlClient
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlDevicesList
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moMachine As New ATS.BO.Framework.BOMachine

    Dim MachineId As Integer
    Dim eMachine As New BOEMachine

    Dim moVersionNo As Byte()
#End Region

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            GetDevices()
            FormView1.Visible = False
        End If

    End Sub
#End Region

#Region "Data Binding"
    Protected Sub GetDevices()
        Try

            Me.GridView2.DataSource = moMachine.GetList().Tables(0)
            Me.GridView2.DataBind()
            GridView2.AllowPaging = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub FormView1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FormView1.DataBound
        'BOPagePermission.SetPagePermission(9, Me.GridView1, Me.FormView1, "btnAdd", 2, 3)
        If FormView1.CurrentMode = FormViewMode.Edit Then
            If Not IsNothing(eMachine.VersionNo) Then
                System.Web.HttpContext.Current.Session.Add("VersionNo", eMachine.VersionNo)
            End If
        End If
    End Sub

#End Region

#Region "Controls Events"


    Protected Sub GridView2_RowEditing(sender As Object, e As GridViewEditEventArgs)
        Try

            Dim x As Integer = e.NewEditIndex

            Dim row As GridViewRow = GridView2.Rows(x)
            Dim MachineId As String = row.Cells(0).Text
            Dim MachinesList As New List(Of BOEMachine)()
            eMachine = moMachine.Find(MachineId)
            MachinesList.Add(eMachine)
            FormView1.DataSource = MachinesList
            Me.FormView1.ChangeMode(FormViewMode.Edit)
            Me.FormView1.DataBind()
            Me.GridView2.Visible = False
            FormView1.Visible = True
            ctlDeviceEmployees.ListBoxDataBinding(MachineId)

            btnAddMachine.Visible = False

            Session("MachineId") = MachineId

        Catch ex As Exception
            lblErr.Text = ex.Message
        End Try
       
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        If (e.CommandName = "show") Then
            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim MachineId As Integer = GridView2.DataKeys(index)("MachineId").ToString()
            'Session("MachineId") = MachineId
            'ctlDeviceEmployees.ListBoxDataBinding(MachineId)
            'ctlDeviceEmployees.Visible = True
            'GridView2.Visible = False
            'lblDeviceName.Text = GridView2.DataKeys(index)("MachineName").ToString()
            'lblLocation.Text = GridView2.DataKeys(index)("Location").ToString()
            'javaScript()
        End If

        If (e.CommandName = "DataDelete") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim id As Integer = GridView2.DataKeys(index)("MachineId").ToString()
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(DataDelete(id)), True)
            GridView2.DataSource = Nothing
            GetDevices()
            ' GridView2.DataBind()
        End If
    End Sub

    Protected Sub btnAddMachine_Click(sender As Object, e As EventArgs) Handles btnAddMachine.Click
        Me.GridView2.Visible = False
        FormView1.Visible = True

        btnAddMachine.Visible = False
    End Sub

    Protected Sub btnAddNew_Click(sender As Object, e As EventArgs)
        DataAdd()
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        DataUpdate()
    End Sub

#End Region

#Region "Data Manipulation"

    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eMachine = FillObject()

        moVersionNo = Session("VersionNo")

        With moMachine
            If .Update(eMachine.MachineId,
                       eMachine.MachineName,
                       eMachine.Location,
                       eMachine.IPAddress,
                       eMachine.MachineTypeId,
                       eMachine.DepartmentId,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"

            Else
                boolSeccessed = False
                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Red
                lbl.Visible = True
                lbl.Text = .InfoMessage
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eMachine = FillObject()



        With moMachine
            If .Add(eMachine.MachineName,
                       eMachine.Location,
                       eMachine.IPAddress,
                       eMachine.MachineTypeId,
                       eMachine.DepartmentId,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True

              


                Dim lbl As Label = DirectCast(Me.FormView1.FindControl("Label2"), Label)
                lbl.ForeColor = Color.Green
                lbl.Visible = True
                lbl.Text = .InfoMessage
                CType(Me.FormView1.FindControl("Button2"), Button).Text = "ÑÌæÚ"

                CType(Me.FormView1.FindControl("btnAddNew"), Button).Enabled = False
                System.Web.HttpContext.Current.Session.Add("MachineId", .Identity)



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

    Public Function DataDelete(ByVal id As Integer) As String
        moMachine.Delete(id)
        Return moMachine.InfoMessage
    End Function


#End Region

#Region "common methods"

    Protected Function IsConnected(ByVal IPAddress As String) As Boolean
        Return True
        Dim connected As Boolean
        If My.Computer.Network.Ping(IPAddress, 1480) Then
            connected = True
        Else
            connected = False
        End If
        Return connected
    End Function

    Public Function PortScan(ByVal IP As String, ByVal port As Integer) As Boolean
        ' Return True
        Dim connected As Boolean
        Try

            If ScanPort(System.Net.IPAddress.Parse(IP), port) = True Then
                connected = True
            Else
                connected = False
            End If

        Catch

        End Try
        Return connected
    End Function

    Public Function ScanPort(ByVal IP As System.Net.IPAddress, ByVal Port As Integer) As Boolean

        Dim functionReturnValue As Boolean = False
        Dim TCP As New System.Net.Sockets.TcpClient()


        Dim result = TCP.BeginConnect(IP, Port, Nothing, Nothing)


        Dim success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1))

        If success Then
            functionReturnValue = True
            TCP.EndConnect(result)
            TCP.Close()
        Else
            functionReturnValue = False
            TCP.Close()
        End If

        Return functionReturnValue

    End Function

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

    Private Function FillObject() As BOEMachine


        Try
            With eMachine
                If Not IsNothing(FormView1.FindControl("txtMachineId")) AndAlso DirectCast(FormView1.FindControl("txtMachineId"), TextBox).Text.Trim <> "" Then
                    .MachineId = DirectCast(FormView1.FindControl("txtMachineId"), TextBox).Text
                End If
                .MachineName = DirectCast(FormView1.FindControl("txtMachineName"), TextBox).Text
                .Location = DirectCast(FormView1.FindControl("txtLocation"), TextBox).Text
                .IPAddress = DirectCast(FormView1.FindControl("txtIPAddress"), TextBox).Text
                .MachineTypeId = DirectCast(FormView1.FindControl("ddlType"), DropDownList).SelectedValue
                .DepartmentId = 1



                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try




        Return eMachine
    End Function

    Public Sub javaScript()
        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append("')};")
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "test", "<script>alert('')</script> ", True)
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString(), True)
    End Sub

#End Region
    
    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Response.Redirect("DevicesList.aspx")
    End Sub
End Class

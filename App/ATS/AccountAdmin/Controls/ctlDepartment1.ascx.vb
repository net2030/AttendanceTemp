Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Web.Security
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Telerik.Web.UI
Imports ATS.BO.Framework

Partial Class AccountAdmin_Controls_ctlDepartment1
    Inherits System.Web.UI.UserControl

#Region "General Declaration"
    Dim moDepartment As New ATS.BO.Framework.BODepartment
    Dim eDepartment As New ATS.BO.Framework.BOEDepartment
    Dim moVersionNo As Byte()
#End Region


#Region "Load"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not (Me.IsPostBack) Then
            GridViewDataBinding()

        End If

    End Sub
#End Region

#Region "Data Binding"
    Private Sub GridViewDataBinding()
        RadTreeView1.DataBind()
        Me.RadTreeView1.Nodes(0).Expanded = True
    End Sub
#End Region

#Region "Common Methods"
    Private Function FillObject() As BOEDepartment
        Try
            With eDepartment
                If txtDepartmentId.Text.Trim <> "" Then
                    .DepartmentId = txtDepartmentId.Text
                End If

                .DepartmentName = txtNode.Text
                .ParentId = ctlDepartmentTree1.SelectedValue

                '.VersionNo = moVersionNo
            End With
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return eDepartment
    End Function

    Public Function javaMsg(ByVal message As String) As String

        Dim sb As New System.Text.StringBuilder()
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")

        Return sb.ToString()

    End Function

#End Region

#Region "Controls Events"

    

    Protected Sub RadTreeView1_NodeClick(sender As Object, e As RadTreeNodeEventArgs)

        Label2.Visible = False
        Label2.Text = ""

        Dim DepartmentId As Integer = RadTreeView1.SelectedValue
        eDepartment = moDepartment.Find(DepartmentId)

        txtDepartmentId.Text = eDepartment.DepartmentId
        ctlDepartmentTree1.SelectedValue = eDepartment.ParentId
        txtNode.Text = eDepartment.DepartmentName

        System.Web.HttpContext.Current.Session.Add("VersionNo", eDepartment.VersionNo)

        btnSave.Text = "تحديث"
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim x As String = ctlDepartmentTree1.SelectedValue
        If txtNode.Text = "" Then
            Exit Sub
        End If
        If btnSave.Text = "تحديث" Then
            DataUpdate()
        Else
            DataAdd()
        End If

        GridViewDataBinding()
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ClearForm()
    End Sub


#End Region

#Region "Data Manipulation"
    Public Function DataUpdate() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eDepartment = FillObject()

        moVersionNo = Session("VersionNo")

        With moDepartment
            If .Update(eDepartment.DepartmentId,
                       eDepartment.DepartmentName,
                       eDepartment.DepartmentName,
                       eDepartment.ParentId,
                       Session("AccountEmployeeId"),
                       moVersionNo) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)


                Label2.ForeColor = Color.Green
                Label2.Visible = True
                Label2.Text = .InfoMessage


            Else
                boolSeccessed = False

                Label2.ForeColor = Color.Red
                Label2.Visible = True
                Label2.Text = .InfoMessage
                'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", javaMsg(.InfoMessage), True)
            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataAdd() As Boolean
        Dim boolSeccessed As Boolean = False

        'Fill object
        eDepartment = FillObject()



        With moDepartment
            If .Add(eDepartment.DepartmentName,
                       eDepartment.DepartmentName,
                       eDepartment.ParentId,
                       Session("AccountEmployeeId")) Then

                boolSeccessed = True
                'Dim UpdatedObject As BOEGatepass
                'UpdatedObject = DirectCast(.Find(StringToInteger(Me.txtGatepassId.Text)), BOEGatepass)
                'Call FormShow(UpdatedObject)

                Label2.ForeColor = Color.Green
                Label2.Visible = True
                Label2.Text = .InfoMessage

            Else
                boolSeccessed = False
                Label2.ForeColor = Color.Red
                Label2.Visible = True
                Label2.Text = .InfoMessage

            End If
        End With

        Return boolSeccessed
    End Function

    Public Function DataDelete(ByVal id As Integer) As String
        moDepartment.Delete(id)
        Return moDepartment.InfoMessage
    End Function
#End Region

    Private Sub ClearForm()
        txtNode.Text = ""
        btnSave.Text = "حفظ"
        txtDepartmentId.Text = ""
        ctlDepartmentTree1.SelectedValue = 0
    End Sub



    
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If txtDepartmentId.Text <> "" Then
            Dim deptID As Integer = CInt(txtDepartmentId.Text)
            DataDelete(deptID)
            RadTreeView1.DataBind()
        End If
       
    End Sub
End Class



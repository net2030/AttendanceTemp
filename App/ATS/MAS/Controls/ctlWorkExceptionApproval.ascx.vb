Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports System
Imports System.Data.SqlClient

Partial Class Attendance_Controls_ctlWorkExceptionFinancialApproval
    Inherits System.Web.UI.UserControl

    Dim moWorkException As New ATS.BO.Framework.BOWorkException



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub



    Protected Sub btnAddGatePass_Click()

        Dim WorkExceptionId As Integer
        Dim UserId As Integer = Session("AccountEmployeeId")
        Dim Notes As String

        For Each row1 As GridViewRow In GridView2.Rows
            '   Dim UserId1 As Integer = DirectCast(row1.FindControl("txtDueAmount"), RadNumericTextBox).Value
            WorkExceptionId = Me.GridView2.DataKeys(row1.RowIndex)("WorkExceptionId")
            Notes = CType(row1.FindControl("Comments"), TextBox).Text
            UpdateWorkExceptionApprovalStatus(WorkExceptionId,
                                              DirectCast(row1.FindControl("datWorkExceptionBegDate"), GeneralControls_MyDate).SelectedDate,
                                              DirectCast(row1.FindControl("datWorkExceptionEndDate"), GeneralControls_MyDate).SelectedDate,
                                              DirectCast(row1.FindControl("rdSpecificEmployee"), CheckBox).Checked,
                                              DirectCast(row1.FindControl("rdSpecificEmployeeRejected"), CheckBox).Checked,
                                              CBool(DirectCast(row1.FindControl("isPaid"), CheckBox).Checked),
                                              CDec(DirectCast(row1.FindControl("txtDueAmount"), RadNumericTextBox).Value),
                                              CDec(DirectCast(row1.FindControl("txtAmount"), RadNumericTextBox).Value),
                                              UserId,
                                              Notes)
        Next

        GridView2.DataBind()
    End Sub

    Protected Function UpdateWorkExceptionApprovalStatus(ByVal WorkExceptionId As Integer,
                                                         ByVal WorkExceptionBegDate As Date,
                                                         ByVal WorkExceptionEndDate As Date,
                                                         ByVal IsApproved As Boolean,
                                                         ByVal IsRejected As Boolean,
                                                         ByVal IsPaid As Boolean,
                                                         ByVal DueAmount As Decimal,
                                                         ByVal PaidAmount As Decimal,
                                                         ByVal UserId As Integer,
                                                         ByVal Notes As String) As Boolean

        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("ATSConnectionString").ConnectionString
        Dim query As String = "Managements.SpWorkException1_ApproveStatus"
        Try
            Dim con As New SqlConnection(constr)
            Dim com As New SqlCommand(query, con)
            com.CommandType = CommandType.StoredProcedure

            com.Parameters.Add("@WorkExceptionId", SqlDbType.Int).Value = WorkExceptionId
            com.Parameters.Add("@WorkExceptionBegDate", SqlDbType.Date).Value = WorkExceptionBegDate
            com.Parameters.Add("@WorkExceptionEndDate", SqlDbType.Date).Value = WorkExceptionEndDate
            com.Parameters.Add("@IsApproved", SqlDbType.Bit).Value = IsApproved
            com.Parameters.Add("@IsRejected", SqlDbType.Bit).Value = IsRejected
            com.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = IsPaid
            com.Parameters.Add("@DueAmount", SqlDbType.Decimal).Value = DueAmount
            com.Parameters.Add("@PaidAmount", SqlDbType.Decimal).Value = PaidAmount

            com.Parameters.Add("@UserAccountId", SqlDbType.Int).Value = UserId
            com.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = Notes


            con.Open()
            com.ExecuteNonQuery()

            con.Close()
        Catch ex As Exception
        End Try

        Return True
    End Function
End Class

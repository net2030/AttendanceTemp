Imports System.Web.Services
Imports System.Web.Script.Services
Imports ATS.BO.Framework
Imports System.Data.SqlClient
Imports ATS.DA.Framework

Partial Class WebService_Count
    Inherits System.Web.UI.Page

    Public Shared moVacation As New BOVacation
    Public Shared moGatepass As New BOGatepass
    Public Shared moWorkException As New BOWorkException

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountOfAllPendingRequest(ByVal EmployeeId As Integer) As String

        Return moGatepass.GetGatepassByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count +
               moVacation.GetVacationByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count +
               moWorkException.GetWorkExceptionByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count +
               moVacation.GetVacationByAltEmployeeForApprovalDataset(EmployeeId).Tables(0).Rows.Count

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountOfVacationPendingRequest(ByVal EmployeeId As Integer) As String

        Return moVacation.GetVacationByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count
        'moVacation.GetVacationBySpecificRoleForApprovalDataset(EmployeeId).Tables(0).Rows.Count

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountOfWorkExceptionPendingRequest(ByVal EmployeeId As Integer) As String

        Return New BOWorkException().GetWorkExceptionByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count

    End Function


    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountOfGatepassPendingRequest(ByVal EmployeeId As Integer) As String

        Return moGatepass.GetGatepassByEmployeeManagerForApprovalDataset1(EmployeeId).Tables(0).Rows.Count
        'moGatepass.GetGatepassBySpecificRoleForApprovalDataset(EmployeeId).Tables(0).Rows.Count

    End Function

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetCountOfAltEmployeePendingRequest(ByVal EmployeeId As Integer) As String

        Return moVacation.GetVacationByAltEmployeeForApprovalDataset(EmployeeId).Tables(0).Rows.Count
        'moGatepass.GetGatepassBySpecificRoleForApprovalDataset(EmployeeId).Tables(0).Rows.Count

    End Function

    Public Sub GetCountOfPendingApproval(ByVal EmployeeId As Integer,
                                         ByRef VacationsCount As Integer,
                                         ByRef WorkExceptionsEmployeeCount As Integer,
                                         ByRef GatepassCount As Integer,
                                         ByRef VacationAltEmployeeCount As Integer)
        Try
            Dim oCon As SqlConnection = New SqlConnection(DataHelper.GetConnectionString())
            oCon.Open()
            Dim cmd As New SqlCommand("Managements.SpApproval_GetPendingsCount")
            Using cmd
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = oCon
                With cmd.Parameters
                    .Add("@EmployeeId", SqlDbType.Int).Value = EmployeeId

                    .Add("@VacationsCount", SqlDbType.Int)
                    .Item("@VacationsCount").Direction = ParameterDirection.Output

                    .Add("@WorkExceptionsEmployeeCount", SqlDbType.Int)
                    .Item("@WorkExceptionsEmployeeCount").Direction = ParameterDirection.Output

                    .Add("@GatepassCount", SqlDbType.Int)
                    .Item("@GatepassCount").Direction = ParameterDirection.Output

                    .Add("@VacationAltEmployeeCount", SqlDbType.Int)
                    .Item("@VacationAltEmployeeCount").Direction = ParameterDirection.Output

                End With
                cmd.ExecuteNonQuery()
                VacationsCount = cmd.Parameters.Item("@VacationsCount").Value
                WorkExceptionsEmployeeCount = cmd.Parameters.Item("@WorkExceptionsEmployeeCount").Value
                GatepassCount = cmd.Parameters.Item("@GatepassCount").Value
                VacationAltEmployeeCount = cmd.Parameters.Item("@VacationAltEmployeeCount").Value

            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try

    End Sub
End Class

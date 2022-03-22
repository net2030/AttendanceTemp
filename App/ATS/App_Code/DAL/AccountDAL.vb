Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports ATS.DA.Framework

Public Class AccountDAL

    Public Function GetDataByAccountId(ByVal AccountId As Integer) As System.Data.DataSet
        Dim ds As DataSet = New DataSet
        Dim dt As DataTable = New DataTable

        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Using oCon
                oCon.Open()
                Dim cmd As New SqlCommand("SELECT * FROM Account Where AccountId=@AccountId")
                Using cmd
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = oCon
                    With cmd.Parameters
                        .Add("@AccountId", SqlDbType.Int).Value = AccountId
                    End With

                    Dim Adapter As New SqlDataAdapter

                    Adapter.SelectCommand = cmd
                    Adapter.Fill(ds)

                End Using
            End Using
        Catch ex As Exception
            Dim strEx As String = ex.Message
        End Try
        Return ds
    End Function

    
    Public Function UpdateLicense(ByVal NoOfReader As String,
                                  ByVal ExpireDate As String,
                                  ByVal DeviceId As String,
                                  ByVal ActivationType As String) As Boolean
        Dim id As Integer = 0
        Dim query As String = "Update Account SET LicenseId=@Readers,AccountExpiryActivation=@AccountExpiryActivation,ActivationType=@ActivationType WHERE MachineId=@MachineId"
        Try
            Dim Con As New SqlConnection(DataHelper.GetConnectionString())
            Dim com As New SqlCommand(query, Con)
            'com.CommandType = CommandType.StoredProcedure

            com.Parameters.Add("@MachineId", SqlDbType.NVarChar, 50).Value = DeviceId
            com.Parameters.Add("@Readers", SqlDbType.NVarChar, 150).Value = NoOfReader
            com.Parameters.Add("@AccountExpiryActivation", SqlDbType.NVarChar, 150).Value = ExpireDate
            com.Parameters.Add("@ActivationType", SqlDbType.NVarChar, 150).Value = ActivationType
            Con.Open()
            com.ExecuteNonQuery()

            'id = com.Parameters.Item("@AccountEmployeeId_out").Value
            Con.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function
    

    Public Function UpdateAccount(ByVal AccountName As String,
                                    ByVal Address1 As String,
                                    ByVal EMailAddress As String,
                                    ByVal Telephone As String,
                                    ByVal Fax As String,
                                    ByVal Culture As String,
                                    ByVal PageSize As Integer,
                                    ByVal NoOfSepratedAbsenceDays As Integer,
                                    ByVal NoOfConsecutiveAbsenceDays As Integer,
                                    ByVal VerificationLogStartTime As Date,
                                    ByVal VerificationLogEndTime As Date,
                                    ByVal ModifiedByEmployeeId As Integer,
                                    ByVal original_AccountId As Integer
                                    ) As Boolean

        Dim MachineId As String = programLic.SystemID()
        MachineId = programLic.AES_encrypt(MachineId)


        Dim query As String = "Update Account set AccountName=@AccountName,Address1=@Address1,EMailAddress=@EMailAddress,Telephone=@Telephone,Fax=@Fax,culture=@Culture,PageSize=@PageSize,NoOfSepratedAbsenceDays=@NoOfSepratedAbsenceDays,NoOfConsecutiveAbsenceDays=@NoOfConsecutiveAbsenceDays,VerificationLogStartTime=@VerificationLogStartTime,VerificationLogEndTime=@VerificationLogEndTime,ModifiedByEmployeeId=@ModifiedByEmployeeId Where AccountId=@AccountId "
        Try
            Dim oCon As New SqlConnection(DataHelper.GetConnectionString())
            Dim com As New SqlCommand(query, oCon)
            com.CommandType = CommandType.Text

            com.Parameters.Add("@AccountDepartmentId", SqlDbType.Int).Value = original_AccountId
            com.Parameters.Add("@AccountName", SqlDbType.NVarChar, 100).Value = AccountName
            com.Parameters.Add("@Address1", SqlDbType.NVarChar, 100).Value = Address1
            com.Parameters.Add("@EMailAddress", SqlDbType.NVarChar, 100).Value = EMailAddress
            com.Parameters.Add("@Telephone", SqlDbType.NVarChar, 20).Value = Telephone
            com.Parameters.Add("@Fax", SqlDbType.NVarChar, 20).Value = Fax
            com.Parameters.Add("@Culture", SqlDbType.NVarChar, 5).Value = Culture
            com.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize
            com.Parameters.Add("@NoOfSepratedAbsenceDays", SqlDbType.Int).Value = NoOfSepratedAbsenceDays
            com.Parameters.Add("@NoOfConsecutiveAbsenceDays", SqlDbType.Int).Value = NoOfConsecutiveAbsenceDays
            com.Parameters.Add("@VerificationLogStartTime", SqlDbType.DateTime).Value = VerificationLogStartTime
            com.Parameters.Add("@VerificationLogEndTime", SqlDbType.DateTime).Value = VerificationLogEndTime

            com.Parameters.Add("@ModifiedByEmployeeId", SqlDbType.Int).Value = ModifiedByEmployeeId
            com.Parameters.Add("@AccountId", SqlDbType.Int).Value = original_AccountId


            oCon.Open()
            com.ExecuteNonQuery()

            System.Web.HttpContext.Current.Session.Add("CultureInfo", Culture)

            oCon.Close()
        Catch ex As Exception
        End Try

    End Function

    Public Function FileUpload(ByVal objFileUpload As FileUpload) As Boolean


        If objFileUpload.FileName = "" Then
            Return True
        End If

        Dim strUploadPath As String = System.Configuration.ConfigurationManager.AppSettings("UploadFilesPath")

        Dim strRoot As String = System.Web.HttpContext.Current.Server.MapPath(strUploadPath)
        If Not System.IO.Directory.Exists(strRoot) Then
            System.IO.Directory.CreateDirectory(strRoot)
        End If
        Dim strAccountPath As String = strRoot & DBUtilities.GetSessionAccountId & "\"
        If Not System.IO.Directory.Exists(strAccountPath) Then
            System.IO.Directory.CreateDirectory(strAccountPath)
        End If
        Dim strLogoPath As String = strAccountPath & "Logo" & "\"
        If Not System.IO.Directory.Exists(strLogoPath) Then
            System.IO.Directory.CreateDirectory(strLogoPath)
        End If
        Dim FileToSave As String = strLogoPath & "CompanyLogo.gif" 'objFileUpload.FileName
        objFileUpload.SaveAs(FileToSave)

    End Function
End Class

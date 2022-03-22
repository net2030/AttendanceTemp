Imports System.Management
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Partial Class AccountAdmin_Controls_ctlLicensing
    Inherits System.Web.UI.UserControl
    Dim moAccount As New AccountDAL
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Init

        If Not Me.IsPostBack Then
            Try

                Dim MachineId As String = programLic.SystemID()
                MachineId = programLic.AES_encrypt(MachineId)

                If Not Request.QueryString("DeviceId") Is Nothing AndAlso Request.QueryString("DeviceId").Replace(" ", "+") = MachineId Then
                    Dim DeviceId As String = Request.QueryString("DeviceId").Replace(" ", "+")
                    Dim AccountExpiryActivation As String
                    Dim LicenseType As String = Request.QueryString("LicenseType").Replace(" ", "+")
                    LicenseType = programLic.AES_decrypt(LicenseType)

                    Dim Readers As String = Request.QueryString("Readers").Replace(" ", "+")
                    Dim ExpireDate As String = Request.QueryString("ExpireDate").Replace(" ", "+")
                    ExpireDate = programLic.AES_decrypt(ExpireDate)
                    'ExpireDate = ExpireDate
                    If IsDate(ExpireDate) Then
                        AccountExpiryActivation = programLic.AES_encrypt(ExpireDate)

                        If moAccount.UpdateLicense(Readers, AccountExpiryActivation, DeviceId, LicenseType) Then
                            lbl1.ForeColor = Color.Green
                            lbl1.Text = "تم تفعيل الرخصة بنجاح"
                        Else
                            lbl1.ForeColor = Color.Red
                            lbl1.Text = "لم يتم تفعيل الرخصة"
                        End If

                    End If
                End If
            Catch ex As Exception
                lbl1.ForeColor = Color.Red
                lbl1.Text = "لم يتم تفعيل الرخصة"
            End Try
        End If

    End Sub

    

End Class

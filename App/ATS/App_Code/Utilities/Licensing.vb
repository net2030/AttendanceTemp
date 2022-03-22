Imports Microsoft.VisualBasic
Imports System.Management
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System
Imports ATS.DA.Framework
Imports System.Globalization




Public Class Licensing

    Public Function IsAllowedToUseApplication(ByVal SystemId As String, ByVal Expire As String, ByVal Readers As String) As Boolean
        Dim Result As Boolean = False
        If IsSameSystemID(SystemId) AndAlso IsExpired(Expire) = False AndAlso IsReadersCountWithinRange(Readers) Then
            Result = True
        Else
            Result = False
        End If

        Return Result
    End Function

    Public Shared Function IsSameSystemID(ByVal SystemId As String) As Boolean
        Dim IsSameMachine As Boolean = False
        Try
            SystemId = AES_decrypt(SystemId)


            'Dim cpuInfo As String = String.Empty
            'Dim mc As New ManagementClass("win32_processor")
            'Dim moc As ManagementObjectCollection = mc.GetInstances()

            'For Each mo As ManagementObject In moc
            '    cpuInfo = mo.Properties("processorID").Value.ToString()
            '    Exit For
            'Next

            Dim drive As String = "C"
            Dim dsk As New ManagementObject("win32_logicaldisk.deviceid=""" & drive & ":""")
            dsk.[Get]()
            Dim volumeSerial As String = dsk("VolumeSerialNumber").ToString()

            If volumeSerial = SystemId Then
                IsSameMachine = True
            End If

        Catch ex As Exception
            IsSameMachine = False
        End Try
        Return True
    End Function

    Public Shared Function IsReadersCountWithinRange(ByVal Readers As String) As Boolean
        Dim Result As Boolean = False
        Try
            Dim ReadersCount As Integer
            Dim AllowedReders As String
            AllowedReders = AES_decrypt(Readers)
            If AllowedReders = "Unlimited" Then
                AllowedReders = "9999"
            End If

            ReadersCount = GetDevices()
            If ReadersCount <= AllowedReders And AllowedReders <> 99999 Then
                Result = True
            End If
        Catch ex As Exception
            Result = False
        End Try
        Return Result
    End Function

    Public Shared Function GetDevices() As Integer
        Dim moMachine As New ATS.BO.Framework.BOMachine
        Dim Count As Integer = 0

        Try

            Dim table As New DataTable()
            table = moMachine.GetList().Tables(0)


            Count = table.Rows.Count
        Catch ex As Exception
            Count = 99999
        End Try
        Return Count
    End Function

    Public Shared Function IsExpired(ByVal Expire As String) As Boolean
        Dim Expired As Boolean = True
        Try
            Dim ExpireEncrypted As String = Expire
            Dim ExpireDecrypted As String = ""

            ExpireDecrypted = AES_decrypt(ExpireEncrypted)

            Dim Today As Date = Now.Date
            Dim ExpiredDate As Date

            ExpiredDate = Date.ParseExact(ExpireDecrypted, "MM/dd/yyyy", New CultureInfo("en-US").DateTimeFormat)

            If Not IsDate(ExpiredDate) Then
                ExpiredDate = Date.ParseExact(ExpireDecrypted, "dd/MM/yyyy", New CultureInfo("en-US").DateTimeFormat)
            End If

            If Today < ExpiredDate Then
                Expired = False
            End If
        Catch ex As Exception
            Expired = True
        End Try
        Return Expired
    End Function

    Public Shared Function GetHost() As String
        Dim strHostName As String
        strHostName = System.Net.Dns.GetHostName()
        Return strHostName
    End Function

    Public Shared Function SystemID() As String
        Dim id As String = ""
        Try


            'Dim cpuInfo As String = String.Empty
            'Dim mc As New ManagementClass("win32_processor")
            'Dim moc As ManagementObjectCollection = mc.GetInstances()

            'For Each mo As ManagementObject In moc
            '    cpuInfo = mo.Properties("processorID").Value.ToString()
            '    Exit For
            'Next

            Dim drive As String = "C"
            Dim dsk As New ManagementObject("win32_logicaldisk.deviceid=""" & drive & ":""")
            dsk.[Get]()

            id = dsk("VolumeSerialNumber").ToString()




        Catch ex As Exception

        End Try
        Return id
    End Function

    Public Shared Sub UpdateLicense(ByVal NoOfReader As String, ByVal ExpireDate As String, ByVal DeviceId As String)

        Dim id As Integer = 0
        Dim constr As String = System.Configuration.ConfigurationManager.ConnectionStrings("LiveConnectionString").ConnectionString
        Dim query As String = "Update Account SET LicenseId=@Readers,AccountExpiryActivation=@AccountExpiryActivation WHERE MachineId=@MachineId"
        Try
            Dim con As New SqlConnection(constr)
            Dim com As New SqlCommand(query, con)
            'com.CommandType = CommandType.StoredProcedure

            com.Parameters.Add("@MachineId", SqlDbType.NVarChar, 100).Value = DeviceId
            com.Parameters.Add("@Readers", SqlDbType.NVarChar, 100).Value = NoOfReader
            com.Parameters.Add("@AccountExpiryActivation", SqlDbType.NVarChar, 100).Value = ExpireDate
            con.Open()
            com.ExecuteNonQuery()

            'id = com.Parameters.Item("@AccountEmployeeId_out").Value
            con.Close()
        Catch ex As Exception
        End Try


    End Sub

    Public Shared Function SendActivationRequest(ByVal reqparm As NameValueCollection) As Boolean
        Dim Result As Boolean = False
        Try
            Dim url = "http://mastechnology.net/Activation.php"
            Using client As New System.Net.WebClient
                Dim responsebytes = client.UploadValues(url, "POST", reqparm)
                Dim responsebody = (New UTF8Encoding).GetString(responsebytes)
                If Not IsNothing(responsebody) Then
                    Result = True
                Else
                    Result = False
                End If
            End Using

        Catch ex As Exception
            Result = False
        End Try

        Return Result
    End Function

#Region "Encrypt/Decrypt Methodes"

    Private Shared AES_Key As String = "PSVJQRk9QTEpNVU1DWUZCRVFGV1VVT0ZOV1RRU1NaWQ="
    Private Shared AES_IV As String = "YWlFLVEZZUFNaWlhPQ01ZT0lLWU5HTFJQVFNCRUJZVA="

    Public Shared Function AES_encrypt(Input As String) As String
        Dim aes As RijndaelManaged = New RijndaelManaged()
        aes.KeySize = 256
        aes.BlockSize = 256
        aes.Padding = PaddingMode.PKCS7
        aes.Key = Convert.FromBase64String(AES_Key)
        aes.IV = Convert.FromBase64String(AES_IV)

        Dim encrypt As System.Security.Cryptography.ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)
        Dim xBuff As Byte() = Nothing
        Using ms As MemoryStream = New MemoryStream()
            Using cs As CryptoStream = New CryptoStream(ms, encrypt, CryptoStreamMode.Write)
                Dim xXml As Byte() = Encoding.UTF8.GetBytes(Input)
                cs.Write(xXml, 0, xXml.Length)
            End Using

            xBuff = ms.ToArray()
        End Using

        Dim Output As [String] = Convert.ToBase64String(xBuff)
        Return Output
    End Function

    Public Shared Function AES_decrypt(Input As String) As String
        Try


            Dim aes As New RijndaelManaged()
            aes.KeySize = 256
            aes.BlockSize = 256
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            aes.Key = Convert.FromBase64String(AES_Key)
            aes.IV = Convert.FromBase64String(AES_IV)

            Dim decrypt As System.Security.Cryptography.ICryptoTransform = aes.CreateDecryptor()
            Dim xBuff As Byte() = Nothing
            Using ms As MemoryStream = New MemoryStream()
                Using cs As CryptoStream = New CryptoStream(ms, decrypt, CryptoStreamMode.Write)
                    Dim xXml As Byte() = Convert.FromBase64String(Input)
                    cs.Write(xXml, 0, xXml.Length)
                End Using

                xBuff = ms.ToArray()
            End Using

            Dim Output As [String] = Encoding.UTF8.GetString(xBuff)
            Return Output
        Catch ex As Exception

        End Try

    End Function
#End Region
End Class

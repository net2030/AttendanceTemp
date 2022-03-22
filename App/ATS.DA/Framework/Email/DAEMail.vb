#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports ATS.DA.Framework
Imports ATS.DA.Framework.DataHelper
#End Region

Namespace Framework

    Public Class DAEMail
        Inherits DataAccessBase(Of EMail)

#Region " Constructors "
        Public Sub New()
            MyBase.new()
        End Sub
#End Region

#Region " Overrides Methods "
        Public Overrides Function Delete(ByVal Id As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Notifications.SpEmail_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmailId", SqlDbType.Int).Value = FieldToint32(Id)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As EMail, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load() As System.Collections.Generic.List(Of EMail)
            Throw New NotImplementedException
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As EMail, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of EMail)
            Throw New NotImplementedException
        End Function
        Public Overrides Function Find(ByVal Id As Integer) As EMail
            Dim eEmail As New Email

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Notifications.SpEmail_GetById")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmailId", SqlDbType.Int).Value = Int32ToField(Id)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With eEmail
                                .EmailId = FieldToint32(oReader, 0)
                                .ToEmailAddress = FieldToString(oReader, 1)
                                .CCEmailAddress = FieldToString(oReader, 2)
                                .BCCEmailAddress = FieldToString(oReader, 3)
                                .FromEmailAddress = FieldToString(oReader, 4)
                                .EmailSubject = FieldToString(oReader, 5)
                                .EmailBody = FieldToString(oReader, 6)
                                .IsEmailSent = FieldToBoolean(oReader, 7)

                                .AddedUserAccountId = FieldToint32(oReader, 8)
                                .UpdatedUserAccountId = FieldToint32(oReader, 9)
                                .AddedDate = FieldToDate(oReader, 10)
                                .UpdatedDate = FieldToDate(oReader, 11)
                                .VersionNo = FieldToByte(oReader, 12)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return eEmail
        End Function
        Protected Overrides Function SqlSelect(ByVal Entity As EMail) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As EMail) As String
            Throw New NotImplementedException
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As EMail) As String
           Throw New NotImplementedException
        End Function
#End Region

#Region " Public Methods "
        Public Function Add(
        ByVal ToEmailAddress As String,
        ByVal CCEmailAddress As String,
        ByVal BCCEmailAddress As String,
        ByVal FromEmailAddress As String,
        ByVal EmailSubject As String,
        ByVal EmailBody As String,
        ByVal IsEmailSent As Boolean,
        ByVal UserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Notifications.SpEmail_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@ToEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(ToEmailAddress)
                            .Add("@CCEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(CCEmailAddress)
                            .Add("@BCCEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(BCCEmailAddress)
                            .Add("@FromEmailAddress", SqlDbType.NVarChar, 255).Value = StringToField(FromEmailAddress)
                            .Add("@EmailSubject", SqlDbType.NVarChar, -1).Value = StringToField(EmailSubject)
                            .Add("@EmailBody", SqlDbType.NVarChar, -1).Value = StringToField(EmailBody)
                            .Add("@IsEmailSent", SqlDbType.Bit).Value = BooleanToField(IsEmailSent)
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@EmailId", SqlDbType.Int))
                            .Item("@EmailId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.Identity = FieldToint32(cmd.Parameters("@EmailId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function


        Public Function Update(
        ByVal EmailId As Integer,
        ByVal ToEmailAddress As String,
        ByVal CCEmailAddress As String,
        ByVal BCCEmailAddress As String,
        ByVal FromEmailAddress As String,
        ByVal EmailSubject As String,
        ByVal EmailBody As String,
        ByVal IsEmailSent As Boolean,
        ByVal UserAccountId As Integer,
        ByVal VersionNo As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Notifications.SpEmail_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@EmailId", SqlDbType.Int).Value = Int32ToField(EmailId)
                            .Add("@ToEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(ToEmailAddress)
                            .Add("@CCEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(CCEmailAddress)
                            .Add("@BCCEmailAddress", SqlDbType.NVarChar, -1).Value = StringToField(BCCEmailAddress)
                            .Add("@FromEmailAddress", SqlDbType.NVarChar, 255).Value = StringToField(FromEmailAddress)
                            .Add("@EmailSubject", SqlDbType.NVarChar, -1).Value = StringToField(EmailSubject)
                            .Add("@EmailBody", SqlDbType.NVarChar, -1).Value = StringToField(EmailBody)
                            .Add("@IsEmailSent", SqlDbType.Bit).Value = BooleanToField(IsEmailSent)
                            .Add("@VersionNo", SqlDbType.Timestamp).Value = VersionNo
                            .Add("@UserAccountId", SqlDbType.Int).Value = Int32ToField(UserAccountId)

                            .Add(New SqlParameter("@RC", SqlDbType.Int))
                            .Item("@RC").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMsgId", SqlDbType.Int))
                            .Item("@RMsgId").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@RMessage", SqlDbType.VarChar, 200))
                            .Item("@RMessage").Direction = ParameterDirection.Output
                            .Add(New SqlParameter("@FieldInError", SqlDbType.VarChar, 50))
                            .Item("@FieldInError").Direction = ParameterDirection.Output
                        End With

                        MyBase.RowsEffected = cmd.ExecuteNonQuery()

                        If CInt(cmd.Parameters("@RC").Value) = 0 Then
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            MyBase.FieldInError = FieldToString(cmd.Parameters("@FieldInError").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
                MyBase.InfoMessage = "فشلت عملية التحديث"
            End Try

            Return boolSeccessed
        End Function
        Public Function LoadEmailsBySentStatus(Optional ByVal IsEmailSent As Boolean = False) As System.Collections.Generic.List(Of EMail)
            Dim List As New List(Of EMail)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Notifications.SpEmail_SelectBySentFlag")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@IsEmailSent", SqlDbType.Bit).Value = BooleanToField(IsEmailSent)
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eEMail As EMail
                        While oReader.Read
                            eEMail = New EMail
                            With eEMail
                                .EmailId = FieldToint32(oReader, 0)
                                .ToEmailAddress = FieldToString(oReader, 1)
                                .CCEmailAddress = FieldToString(oReader, 2)
                                .BCCEmailAddress = FieldToString(oReader, 3)
                                .FromEmailAddress = FieldToString(oReader, 4)
                                .EmailSubject = FieldToString(oReader, 5)
                                .EmailBody = FieldToString(oReader, 6)
                                .IsEmailSent = FieldToBoolean(oReader, 7)

                                .AddedUserAccountId = FieldToint32(oReader, 8)
                                .UpdatedUserAccountId = FieldToint32(oReader, 9)
                                .AddedDate = FieldToDate(oReader, 10)
                                .UpdatedDate = FieldToDate(oReader, 11)
                                .VersionNo = FieldToByte(oReader, 12)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eEMail)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
#End Region

#Region " Miscellaneous "

#End Region

    End Class

End Namespace


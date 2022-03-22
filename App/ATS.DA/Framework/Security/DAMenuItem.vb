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
    ''' <summary>
    '''  
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DAMenuItem
        Inherits DataAccessBase(Of MenuItem)

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
                    Dim cmd As New SqlCommand("Users.SpMenuItem_Delete")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@MenuItemId", SqlDbType.Int).Value = FieldToint32(Id)

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
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
        Public Overrides Function GetDataset( _
        ByVal DataEntity As MenuItem, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Data.DataSet
            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.MenuItem"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim Adapter As New SqlDataAdapter
                        MyBase.Dataset = New DataSet
                        Adapter.SelectCommand = cmd
                        MyBase.RowsEffected = Adapter.Fill(MyBase.Dataset, "MenuItem")

                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If MyBase.RowsEffected > 0 Then
                MyBase.GetPagesTotal("Users.MenuItem", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return MyBase.Dataset
        End Function
        Public Overloads Overrides Function Load() _
        As System.Collections.Generic.List(Of MenuItem)
            Dim List As New List(Of MenuItem)

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "SELECT ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Description, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Url, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ParentMenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "DisplaySequence, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsAlwaysEnabled, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Tag, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.MenuItem ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ORDER BY MenuItemId", Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = oCon

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim eMenuItem As MenuItem
                        While oReader.Read
                            eMenuItem = New MenuItem
                            With eMenuItem
                                .MenuItemId = FieldToint32(oReader, 0)
                                .MenuItemName = FieldToString(oReader, 1)
                                .Description = FieldToString(oReader, 2)
                                .Url = FieldToString(oReader, 3)
                                .ParentMenuItemId = FieldToint32(oReader, 4)
                                .DisplaySequence = FieldToint32(oReader, 5)
                                .IsAlwaysEnabled = FieldToBoolean(oReader, 6)
                                .MenuItemTag = FieldToString(oReader, 7)

                                .AddedUserAccountId = FieldToint32(oReader, 8)
                                .UpdatedUserAccountId = FieldToint32(oReader, 9)
                                .AddedDate = FieldToDateTime(oReader, 10)
                                .UpdatedDate = FieldToDateTime(oReader, 11)
                                .VersionNo = FieldToByte(oReader, 12)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(eMenuItem)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return List
        End Function
        Public Overloads Overrides Function Load( _
        ByVal DataEntity As MenuItem, _
        Optional ByVal PageNo As Integer = 1, _
        Optional ByVal PageSize As Integer = 50) As System.Collections.Generic.List(Of MenuItem)
            Dim List As New List(Of MenuItem)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Common.SpCommon_GetDataPage")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@Tables", SqlDbType.NVarChar, -1).Value = "Users.MenuItem"
                            .Add("@Fields", SqlDbType.NVarChar, -1).Value = SqlSelect(DataEntity)
                            .Add("@Sort", SqlDbType.NVarChar, 500).Value = SqlOrderBy(DataEntity)
                            .Add("@Filter", SqlDbType.NVarChar, -1).Value = SqlWhere(DataEntity)
                            .Add("@Group", SqlDbType.NVarChar, -1).Value = String.Empty
                            .Add("@PageNumber", SqlDbType.Int).Value = PageNo
                            .Add("@PageSize", SqlDbType.Int).Value = PageSize
                        End With

                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        Dim oMenuItem As MenuItem
                        While oReader.Read
                            oMenuItem = New MenuItem
                            With oMenuItem
                                .MenuItemId = FieldToint32(oReader, 1)
                                .MenuItemName = FieldToString(oReader, 2)
                                .Description = FieldToString(oReader, 3)
                                .Url = FieldToString(oReader, 4)
                                .ParentMenuItemId = FieldToint32(oReader, 5)
                                .DisplaySequence = FieldToint16(oReader, 6)
                                .IsAlwaysEnabled = FieldToBoolean(oReader, 7)
                                .MenuItemTag = FieldToString(oReader, 8)

                                .AddedUserAccountId = FieldToint32(oReader, 9)
                                .UpdatedUserAccountId = FieldToint32(oReader, 10)
                                .AddedDate = FieldToDateTime(oReader, 11)
                                .UpdatedDate = FieldToDateTime(oReader, 12)
                                .VersionNo = FieldToByte(oReader, 13)
                                .AddedUserName = GetUserName(.AddedUserAccountId)
                                .UpdatedUserName = GetUserName(.UpdatedUserAccountId)
                            End With
                            List.Add(oMenuItem)
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            If List.Count > 0 Then
                MyBase.GetPagesTotal("Users.MenuItem", SqlWhere(DataEntity), PageSize)
            Else
                MyBase.PageTotal = 0
            End If

            Return List
        End Function


        Public Overrides Function Find(ByVal Id As Integer) As MenuItem
            Dim oMenuItem As New MenuItem

            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Description, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Url, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ParentMenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "DisplaySequence, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsAlwaysEnabled, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Tag, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "FROM Users.MenuItem ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "WHERE MenuItemId = " & Id.ToString, Environment.NewLine)

            Try
                Dim oCon As SqlConnection = New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand(sb.ToString, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        Dim oReader As SqlDataReader
                        oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                        If oReader.Read Then
                            With oMenuItem
                                .MenuItemId = FieldToint32(oReader, 0)
                                .MenuItemName = FieldToString(oReader, 1)
                                .Description = FieldToString(oReader, 2)
                                .Url = FieldToString(oReader, 3)
                                .ParentMenuItemId = FieldToint32(oReader, 4)
                                .DisplaySequence = FieldToint16(oReader, 5)
                                .IsAlwaysEnabled = FieldToBoolean(oReader, 6)
                                .MenuItemTag = FieldToString(oReader, 7)

                                .AddedUserAccountId = FieldToint32(oReader, 8)
                                .UpdatedUserAccountId = FieldToint32(oReader, 9)
                                .AddedDate = FieldToDateTime(oReader, 10)
                                .UpdatedDate = FieldToDateTime(oReader, 11)
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

            Return oMenuItem
        End Function

        Protected Overrides Function SqlSelect(ByVal Entity As MenuItem) As String
            Dim sb As New System.Text.StringBuilder
            sb.AppendFormat("{0}{1}", "MenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "MenuItemName, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Description, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Url, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "ParentMenuItemId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "DisplaySequence, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "IsAlwaysEnabled, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "Tag, ", Environment.NewLine)

            sb.AppendFormat("{0}{1}", "AddedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedUserAccountId, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "AddedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "UpdatedDate, ", Environment.NewLine)
            sb.AppendFormat("{0}{1}", "VersionNo ", Environment.NewLine)
            Return sb.ToString
        End Function
        Protected Overrides Function SqlWhere(ByVal Entity As MenuItem) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.WhereFilter
                Case MenuItem.FilterWhere.PrimaryKey
                    sb.AppendFormat("{0}{1}", "MenuItemId = " & Entity.MenuItemId.ToString, Environment.NewLine)
            End Select

            Return sb.ToString.Trim
        End Function
        Protected Overrides Function SqlOrderBy(ByVal Entity As MenuItem) As String
            Dim sb As New System.Text.StringBuilder

            Select Case Entity.OrderByFilter
                Case MenuItem.FilterOrderBy.None
            End Select

            Return sb.ToString.Trim
        End Function
#End Region

#Region " Public Methods "
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="MenuItemName"></param>
        ''' <param name="Description"></param>
        ''' <param name="Url"></param>
        ''' <param name="ParentMenuItemId"></param>
        ''' <param name="DisplaySequence"></param>
        ''' <param name="IsAlwaysEnabled"></param>
        ''' <param name="AddedUserAccountId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Add( _
        ByVal MenuItemName As String, _
        ByVal Description As String, _
        ByVal Url As String, _
        ByVal ParentMenuItemId As Integer, _
        ByVal DisplaySequence As Integer, _
        ByVal IsAlwaysEnabled As Boolean, _
        ByVal AddedUserAccountId As Integer) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpMenuItem_Insert")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@MenuItemName", SqlDbType.VarChar, 50).Value = StringToField(MenuItemName)
                            .Add("@Description", SqlDbType.VarChar, -1).Value = StringToField(Description)
                            .Add("@Url", SqlDbType.VarChar, -1).Value = StringToField(Url)
                            .Add("@ParentMenuItemId", SqlDbType.Int).Value = Int32ToField(ParentMenuItemId)
                            .Add("@DisplaySequence", SqlDbType.Int).Value = Int32ToField(DisplaySequence)
                            .Add("@IsAlwaysEnabled", SqlDbType.Bit).Value = BooleanToField(IsAlwaysEnabled)
                            .Add("@AddedUserAccountId", SqlDbType.Int).Value = Int32ToField(AddedUserAccountId)

                            .Add(New SqlParameter("@MenuItemId", SqlDbType.Int))
                            .Item("@MenuItemId").Direction = ParameterDirection.Output
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
                            MyBase.Identity = FieldToint32(cmd.Parameters("@MenuItemId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                            boolSeccessed = True
                        Else
                            MyBase.IfnoMessageId = FieldToint32(cmd.Parameters("@RMsgId").Value)
                            MyBase.InfoMessage = FieldToString(cmd.Parameters("@RMessage").Value)
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="MenuItemId"></param>
        ''' <param name="MenuItemName"></param>
        ''' <param name="Description"></param>
        ''' <param name="Url"></param>
        ''' <param name="ParentMenuItemId"></param>
        ''' <param name="DisplaySequence"></param>
        ''' <param name="IsAlwaysEnabled"></param>
        ''' <param name="UpdatedUserAccountId"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Update( _
        ByVal MenuItemId As Integer, _
        ByVal MenuItemName As String, _
        ByVal Description As String, _
        ByVal Url As String, _
        ByVal ParentMenuItemId As Integer, _
        ByVal DisplaySequence As Integer, _
        ByVal IsAlwaysEnabled As Boolean, _
        ByVal UpdatedUserAccountId As Integer, _
        ByVal version As Byte()) As Boolean
            Dim boolSeccessed As Boolean = False

            Try
                Dim oCon As New SqlConnection(MyBase.ConnectionString)
                Using oCon
                    oCon.Open()
                    Dim cmd As New SqlCommand("Users.SpMenuItem_Update")
                    Using cmd
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = oCon
                        With cmd.Parameters
                            .Add("@MenuItemId", SqlDbType.Int).Value = FieldToint32(MenuItemId)
                            .Add("@MenuItemName", SqlDbType.VarChar, 50).Value = StringToField(MenuItemName)
                            .Add("@Description", SqlDbType.VarChar, -1).Value = StringToField(Description)
                            .Add("@Url", SqlDbType.VarChar, -1).Value = StringToField(Url)
                            .Add("@ParentMenuItemId", SqlDbType.Int).Value = Int32ToField(ParentMenuItemId)
                            .Add("@DisplaySequence", SqlDbType.Int).Value = Int32ToField(DisplaySequence)
                            .Add("@IsAlwaysEnabled", SqlDbType.Bit).Value = BooleanToField(IsAlwaysEnabled)
                            .Add("@UpdatedUserAccountId", SqlDbType.Int).Value = Int32ToField(UpdatedUserAccountId)
                            .Add("@VersionNo", SqlDbType.Timestamp).Value = version

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
                        End If
                    End Using
                End Using

            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return boolSeccessed
        End Function
#End Region



    End Class
End Namespace


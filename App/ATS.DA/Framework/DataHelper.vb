#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports System.Web
Imports System.Diagnostics
Imports System.Configuration
Imports System.Net

#End Region

Namespace Framework
    ''' <summary>
    ''' Data Helper Class
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DataHelper

        Private Const Attendance_CONNSTRING_KEY As String = "ATSConnectionString"

#Region " Standard Variables "
        Public Shared conLikeOperator As String = "%"
        Private Shared vbChecked As Integer = 1
        Private Shared vbUnChecked As Integer = 0
#End Region

#Region " Numeric Values "
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToint16( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Integer

            Dim intReturnValue As Int16 = 0
            Try
                If anyReader.IsDBNull(intIndx) Then
                    intReturnValue = 0
                Else
                    intReturnValue = anyReader.GetInt16(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return intReturnValue
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToint32( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Integer

            Dim intReturnValue As Int32 = 0
            Try
                If anyReader.IsDBNull(intIndx) Then
                    intReturnValue = 0
                Else
                    intReturnValue = anyReader.GetInt32(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return intReturnValue
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToint64( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Decimal

            Dim intReturnValue As Int64 = 0
            Try
                If anyReader.IsDBNull(intIndx) Then
                    intReturnValue = 0
                Else
                    intReturnValue = anyReader.GetInt64(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return intReturnValue
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToLong( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Long

            Dim lngReturnValue As Long = 0
            Try
                If anyReader.IsDBNull(intIndx) Then
                    lngReturnValue = 0
                Else
                    lngReturnValue = anyReader.GetInt64(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return lngReturnValue
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="intValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToint32(ByVal intValue As Integer) As Integer
            Dim ReturnedValue As Integer = 0

            Try
                If (IsDBNull(intValue) Or IsNothing(intValue)) Then
                    intValue = 0
                Else
                    ReturnedValue = CInt(intValue)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function FieldToint32(ByVal objValue As Object) As Integer
            Dim ReturnedValue As Integer = 0

            Try
                If IsNothing(objValue) Then
                    objValue = 0
                Else
                    ReturnedValue = Integer.Parse(objValue.ToString)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function Int32ToField(ByVal intValue As Integer) As Integer
            Dim ReturnedValue As Integer = 0

            Try
                If (IsDBNull(intValue) Or IsNothing(intValue)) Then
                    ReturnedValue = 0
                Else
                    ReturnedValue = CInt(intValue)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function FieldToDecimal( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Decimal

            Dim ReturnedValue As Decimal = 0
            Try
                If anyReader.IsDBNull(intIndx) Then
                    ReturnedValue = 0
                Else
                    ReturnedValue = anyReader.GetDecimal(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function DecimalToField(ByVal decValue As Decimal) As Decimal
            Dim ReturnedValue As Decimal = 0

            Try
                If (IsNothing(decValue) Or IsDBNull(decValue)) Then
                    ReturnedValue = 0
                Else
                    ReturnedValue = CDec(decValue)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function DecimalToField(ByVal objValue As Object) As Decimal
            Dim ReturnedValue As Decimal = 0

            Try
                If IsNothing(objValue) Then
                    ReturnedValue = 0
                Else
                    ReturnedValue = Decimal.Parse(objValue.ToString)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
#End Region

#Region " String Values "
        ''' <summary>
        ''' To return a sting value between a single quotes 
        ''' </summary>
        ''' <param name="strValue">a String Value</param>
        Public Shared Function Str2Field(ByVal strValue As String) As String
            Dim ReturnValue As String = String.Empty
            If strValue = "" Then
                ReturnValue = String.Empty
            Else
                ReturnValue = "'" & QuoteConvert(strValue.Trim) & "'"
            End If
            Return ReturnValue
        End Function
        Public Shared Function LikeStr2Field(ByVal strValue As String) As String
            Dim ReturnValue As String = String.Empty
            If strValue = "" Then
                ReturnValue = String.Empty
            Else
                ReturnValue = "'" & conLikeOperator & QuoteConvert(strValue.Trim) & conLikeOperator & "'"
            End If
            Return ReturnValue
        End Function
        Public Shared Function LikeLeftStr2Field(ByVal strValue As String) As String
            Dim ReturnValue As String = String.Empty
            If strValue = "" Then
                ReturnValue = String.Empty
            Else
                ReturnValue = "'" & conLikeOperator & QuoteConvert(strValue.Trim) & "'"
            End If
            Return ReturnValue
        End Function
        Public Shared Function LikeRightStr2Field(ByVal strValue As String) As String
            Dim ReturnValue As String = String.Empty
            If strValue = "" Then
                ReturnValue = String.Empty
            Else
                ReturnValue = "'" & QuoteConvert(strValue.Trim) & conLikeOperator & "'"
            End If
            Return ReturnValue
        End Function
        ''' <summary>
        ''' To Clean up, Massage a string value to be send to a Database column
        ''' </summary>
        ''' <param name="strValue">a String Value</param>
        Public Shared Function StringToField(ByVal strValue As String) As String
            Dim ReurnValue As String = String.Empty
            Try
                If IsNothing(strValue) Then
                    ReurnValue = String.Empty
                Else
                    ReurnValue = strValue.Trim
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return ReurnValue
        End Function
        ''' <summary>
        ''' To Clean up, Massage a string value coming from a Database column
        ''' </summary>
        ''' <param name="anyReader">an SqlDataReader Object</param>
        ''' <param name="Index">an Integer Value</param>
        Public Shared Function FieldToString( _
        ByVal anyReader As SqlClient.SqlDataReader, ByVal Index As Integer) As String
            Dim ReurnValue As String = String.Empty
            Try
                If anyReader.IsDBNull(Index) Then
                    ReurnValue = String.Empty
                Else
                    ReurnValue = anyReader.GetString(Index).Trim
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return ReurnValue
        End Function
        ''' <summary>
        ''' To Clean up, Massage a string value 
        ''' </summary>
        ''' <param name="strValue">a String Value</param>
        Public Shared Function FieldToString(ByVal strValue As String) As String
            Dim ReurnValue As String = String.Empty
            Try
                If strValue.ToString = String.Empty Then
                    ReurnValue = String.Empty
                Else
                    ReurnValue = strValue.ToString.Trim
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return ReurnValue
        End Function
        Public Shared Function FieldToString(ByVal objValue As Object) As String
            Dim ReurnValue As String = String.Empty
            Try
                If objValue Is Nothing Then
                    ReurnValue = String.Empty
                Else
                    ReurnValue = CStr(objValue).Trim
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
            Return ReurnValue
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="strValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function QuoteConvert(ByVal strValue As String) As String
            QuoteConvert = Replace(strValue.Trim, "'", "''")
            Return QuoteConvert
        End Function
#End Region

#Region " Boolen Values "
        Public Shared Function FieldToBoolean( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Boolean
            Dim bolReturnValue As Boolean = False

            Try
                If anyReader.IsDBNull(intIndx) Then
                    bolReturnValue = False
                Else
                    bolReturnValue = anyReader.GetBoolean(intIndx)
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return bolReturnValue
        End Function
        Public Shared Function FieldToBoolean(ByVal bolValue As Boolean) As Boolean
            Dim ReturnedValue As Boolean = False

            Try
                If (IsNothing(bolValue) Or IsDBNull(bolValue)) Then
                    ReturnedValue = False
                Else
                    If bolValue Then
                        ReturnedValue = True
                    Else
                        ReturnedValue = False
                    End If
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return ReturnedValue
        End Function
        Public Shared Function BooleanToField(ByVal bolValue As Boolean) As Integer
            Dim bolReturnValue As Integer = 0

            Try
                If (IsNothing(bolValue) Or IsDBNull(bolValue)) Then
                    bolReturnValue = 0
                Else
                    If bolValue Then
                        bolReturnValue = 1
                    Else
                        bolReturnValue = 0
                    End If
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return bolReturnValue
        End Function
#End Region

#Region " Date Values "
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToDateTime( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As DateTime
            Dim anyDateTime As DateTime

            Try
                If anyReader.IsDBNull(intIndx) Then
                    anyDateTime = #12:00:00 AM#
                Else
                    anyDateTime = anyReader.GetDateTime(intIndx)
                End If
            Catch ex As Exception
                anyDateTime = #12:00:00 AM#
            End Try

            Return anyDateTime
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToDate( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Date
            Dim datReturnValue As Date = #1/1/1900#
            Try
                If anyReader.IsDBNull(intIndx) Then
                    datReturnValue = #1/1/1900#
                Else
                    datReturnValue = anyReader.GetDateTime(intIndx)
                End If
            Catch ex As Exception
                datReturnValue = #1/1/1900#
            End Try
            Return datReturnValue
        End Function
        Public Shared Function DateToField(ByVal objValue As Object) As Date
            Dim datReturnValue As Date = #1/1/1900#

            Try
                If (IsNothing(objValue) Or IsDBNull(objValue)) Then
                    datReturnValue = #1/1/1900#
                Else
                    If IsDate(objValue) Then
                        datReturnValue = CDate(objValue)
                    End If
                End If
            Catch ex As Exception
                datReturnValue = #1/1/1900#
            End Try

            Return datReturnValue
        End Function
#End Region

#Region " Miscellanies "
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="anyReader"></param>
        ''' <param name="intIndx"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FieldToByte( _
        ByVal anyReader As SqlClient.SqlDataReader, _
        ByVal intIndx As Integer) As Byte()
            Dim BytValue As Byte() = Nothing

            Try
                If Not anyReader.IsDBNull(intIndx) Then
                    BytValue = CType(anyReader(intIndx), Byte())
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return BytValue
        End Function
        Public Shared Function ByteToField(ByVal anyByte As Byte()) As Byte()
            Dim BytValue As Byte() = Nothing

            Try
                BytValue = CType(anyByte, Byte())
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try

            Return BytValue
        End Function
        Public Shared Function GetConnectionString() As String
            Dim strConn As String = String.Empty
            strConn = ConfigurationManager.ConnectionStrings(Attendance_CONNSTRING_KEY).ConnectionString
            Return strConn
        End Function
        Public Shared Function GetUserName(ByVal UserAccountId As Integer) As String
            Return New DAUserAccount().GetUserName(UserAccountId)
        End Function
#End Region

#Region " Exception "
        Public Shared Sub SQLExceptionHandler(ByVal ex As SqlException)
            Dim sb As New StringBuilder

            Dim i As Integer
            For i = 0 To ex.Errors.Count - 1
                sb.AppendFormat("Error: {0}{1}", "Index #: " & i.ToString, Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Source: " & ex.Errors(i).Source, Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Number: " & ex.Errors(i).Number.ToString(), Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "State: " & ex.Errors(i).State.ToString(), Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Class: " & ex.Errors(i).Class.ToString(), Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Server: " & ex.Errors(i).Server, Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Message: " & ex.Errors(i).Message, Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "Procedure : " & ex.Errors(i).Procedure, Environment.NewLine)
                sb.AppendFormat("Error: {0}{1}", "LineNumber : " & ex.Errors(i).LineNumber.ToString(), Environment.NewLine)
            Next

            Dim strEx As String = sb.ToString
        End Sub
#End Region
    End Class

End Namespace


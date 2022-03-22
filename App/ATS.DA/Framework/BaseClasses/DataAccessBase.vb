#Region " Imports "
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Configuration
Imports ATS.DA.Framework.DataHelper
#End Region

Namespace Framework
    ''' <summary>
    ''' Data Access Layer Base Class
    ''' </summary>
    ''' <typeparam name="DataEntityBase"></typeparam>
    ''' <remarks></remarks>
    Public MustInherit Class DataAccessBase(Of DataEntityBase)

#Region " Private Variables "
        Private strConnectionString As String
        Private intIdentity As Integer
        Private intUserAccountId As Integer
        Private dsDataset As DataSet
        Private intRowsEffected As Integer
        Private strMessage As String
        Private intMessageId As Integer
        Private strFieldInError As String
        Private intPageTotal As Integer
#End Region

#Region " Standard Properties"
        ''' <summary>
        ''' a database connection string
        ''' </summary>
        Public Property ConnectionString() As String
            Get
                Return strConnectionString
            End Get
            Set(ByVal value As String)
                strConnectionString = value
            End Set
        End Property
        ''' <summary>
        ''' the new inserted identiy value
        ''' </summary>
        Public Property Identity() As Integer
            Get
                Return intIdentity
            End Get
            Set(ByVal value As Integer)
                intIdentity = value
            End Set
        End Property
        ''' <summary>
        ''' Application Identification Code 
        ''' </summary>
        Public Property FieldInError() As String
            Get
                Return strFieldInError
            End Get
            Set(ByVal value As String)
                strFieldInError = value
            End Set
        End Property
        ''' <summary>
        ''' User Account Identification Code
        ''' </summary>
        Public Property UserAccountId() As Integer
            Get
                Return intUserAccountId
            End Get
            Set(ByVal value As Integer)
                intUserAccountId = value
            End Set
        End Property
        ''' <summary>
        ''' Result Dataset Object
        ''' </summary>
        Public Property Dataset() As DataSet
            Get
                Return dsDataset
            End Get
            Set(ByVal value As DataSet)
                dsDataset = value
            End Set
        End Property
        ''' <summary>
        ''' Data Rows Effected 
        ''' </summary>
        Public Property RowsEffected() As Integer
            Get
                Return intRowsEffected
            End Get
            Set(ByVal value As Integer)
                intRowsEffected = value
            End Set
        End Property
        ''' <summary>
        ''' Custom Message
        ''' </summary>
        Public Property InfoMessage() As String
            Get
                Return strMessage
            End Get
            Set(ByVal value As String)
                strMessage = value
            End Set
        End Property
        ''' <summary>
        ''' Custom Message Id
        ''' </summary>
        Public Property IfnoMessageId() As Integer
            Get
                Return intMessageId
            End Get
            Set(ByVal value As Integer)
                intMessageId = value
            End Set
        End Property
        ''' <summary>
        ''' Total of Data Pages
        ''' </summary>
        Public Property PageTotal() As Integer
            Get
                Return intPageTotal
            End Get
            Set(ByVal value As Integer)
                intPageTotal = value
            End Set
        End Property
#End Region

#Region " Constructors "
        Public Sub New()
            strConnectionString = GetConnectionString()
        End Sub
        Public Sub New(ByVal ConnectionString As String)
            strConnectionString = ConnectionString
        End Sub
#End Region

#Region " MustOverride Methods "
        ''' <summary>
        ''' To return a list Entity objects
        ''' </summary>
        ''' <returns name="Returns">A list of entity objects</returns>
        Public MustOverride Function Load( _
        ByVal DataEntity As DataEntityBase, _
              Optional ByVal PageNo As Integer = 1, _
              Optional ByVal PageSize As Integer = 50) As List(Of DataEntityBase)
        Public MustOverride Function Load() As List(Of DataEntityBase)
        ''' <summary>
        ''' To retrieve a single record  
        ''' </summary>
        ''' <param name="Id">An integer value represents a key value</param>
        ''' <returns name="Returns">a data entity object</returns>
        Public MustOverride Function Find(ByVal Id As Integer) As DataEntityBase
        ''' <summary>
        ''' To Delete an existing record
        ''' </summary>
        ''' <param name="Id">An integer value represents a key value</param>
        ''' <returns name="Returns">a True/False To indicate the success of the execution</returns>
        Public MustOverride Function Delete(ByVal Id As Integer) As Boolean
        ''' <summary>
        ''' Return a result as Dataset  
        ''' </summary>        
        ''' <returns name="Returns">Return a result as a Dataset</returns>
        Public MustOverride Function GetDataset( _
        ByVal DataEntity As DataEntityBase, _
              Optional ByVal PageNo As Integer = 1, _
              Optional ByVal PageSize As Integer = 50) As DataSet


#End Region

#Region " Protected Overridable Methods "
        ''' <summary>
        ''' Generate a SQL Select Statment
        ''' </summary>
        ''' <param name="Entity"></param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Protected Overridable Function SqlSelect(ByVal Entity As DataEntityBase) As String
            Return String.Empty
        End Function
        ''' <summary>
        ''' Generate a SQL Where Statment 
        ''' </summary>
        ''' <param name="Entity"></param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>            
        Protected Overridable Function SqlWhere(ByVal Entity As DataEntityBase) As String
            Return String.Empty
        End Function
        ''' <summary>
        ''' Generate a SQL Order By Statment 
        ''' </summary>
        ''' <param name="Entity"></param>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Protected Overridable Function SqlOrderBy(ByVal Entity As DataEntityBase) As String
            Return String.Empty
        End Function

        Protected Overridable Sub GetPagesTotal( _
        ByVal TableName As String, _
        ByVal SqlWhere As String, _
        Optional ByVal PageSize As Integer = 50)
            Dim intRows As Integer = 0
            Dim strSQL As String = String.Empty
            Dim douReminder As Double

            ' Generate SQL Statments
            strSQL = strSQL & "Select Count(*) From " & TableName
            If Not (SqlWhere = String.Empty) Then
                strSQL = strSQL & " Where " & SqlWhere & " "
            End If

            Try
                Dim oCon As New SqlConnection(strConnectionString)
                Using oCon
                    oCon.Open()

                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(strSQL, oCon)
                    Using cmd
                        cmd.CommandType = CommandType.Text
                        intRows = CType(cmd.ExecuteScalar, Integer)
                    End Using
                End Using

                If intRows > 0 Then
                    If intRows < PageSize Then
                        intPageTotal = 1
                    Else
                        intPageTotal = CInt((intRows / CLng(PageSize)))
                        douReminder = Math.IEEERemainder(CDbl(intRows), CDbl(PageSize))
                        If douReminder > 0 Then
                            intPageTotal += 1
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
        End Sub
        Protected Overridable Sub GetPagesTotal(ByVal RowsCount As Integer, Optional ByVal PageSize As Integer = 50)
            Dim strSQL As String = String.Empty
            Dim douReminder As Double

            Try
                If RowsCount > 0 Then
                    If RowsCount < PageSize Then
                        intPageTotal = 1
                    Else
                        intPageTotal = CInt((RowsCount / CLng(PageSize)))
                        douReminder = Math.IEEERemainder(CDbl(RowsCount), CDbl(PageSize))
                        If douReminder > 0 Then
                            intPageTotal += 1
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim strEx As String = ex.Message
            End Try
        End Sub
#End Region

    End Class
End Namespace


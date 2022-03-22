Imports Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Threading
Imports System.Net
Public Class TimeOutSocket
    Public Shared IsConnectionSuccessful As Boolean = False
    Private Shared socketexception As Exception
    Private Shared TimeoutObject As New ManualResetEvent(False)

    Public Shared Function Connect(remoteEndPoint As IPEndPoint, timeoutMSec As Integer) As TcpClient
        TimeoutObject.Reset()
        socketexception = Nothing

        Dim serverip As String = Convert.ToString(remoteEndPoint.Address)
        Dim serverport As Integer = remoteEndPoint.Port
        Dim tcpclient As New TcpClient()

        tcpclient.BeginConnect(serverip, serverport, New AsyncCallback(AddressOf CallBackMethod), tcpclient)

        If TimeoutObject.WaitOne(timeoutMSec, False) Then
            If IsConnectionSuccessful Then
                Return tcpclient
            Else
                Throw socketexception
            End If
        Else

            tcpclient.Close()

            'Throw New TimeoutException("TimeOut Exception")

        End If
    End Function
    Private Shared Sub CallBackMethod(asyncresult As IAsyncResult)
        Try
            IsConnectionSuccessful = False
            Dim tcpclient As TcpClient = TryCast(asyncresult.AsyncState, TcpClient)

            If tcpclient.Client IsNot Nothing Then
                tcpclient.EndConnect(asyncresult)
                IsConnectionSuccessful = True
            End If
        Catch ex As Exception
            IsConnectionSuccessful = False
            socketexception = ex
        Finally
            TimeoutObject.[Set]()
        End Try
    End Sub
End Class
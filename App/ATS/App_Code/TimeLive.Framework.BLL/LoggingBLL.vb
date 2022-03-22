Imports Microsoft.VisualBasic

Public Class LoggingBLL

    Public Shared Sub WriteExceptionToLog(ByVal Message As String, ByVal ex As Exception)
        'Dim logger As log4net.ILog = log4net.LogManager.GetLogger("File")
        'logger.Error(Message, ex)
        'Throw ex
    End Sub
    Public Shared Sub WriteExceptionNoRaiseToLog(ByVal Message As String, ByVal ex As Exception)
        'Dim logger As log4net.ILog = log4net.LogManager.GetLogger("File")
        'logger.Error(Message, ex)
    End Sub
    Public Shared Sub WriteToLog(ByVal Message As String)
        'Dim logger As log4net.ILog = log4net.LogManager.GetLogger("File")
        'logger.Info(Message)
    End Sub
    Public Shared Sub WriteTracingToLog(ByVal Message As String)
        'If LoggingBLL.IsTracingEnabled Then
        '    Dim logger As log4net.ILog = log4net.LogManager.GetLogger("File")
        '    logger.Info(Message)
        'End If
    End Sub
    Public Shared Function IsTracingEnabled() As Boolean
        'If System.Configuration.ConfigurationManager.AppSettings("TracingEnabled") Is Nothing Then
        '    Return False
        'ElseIf System.Configuration.ConfigurationManager.AppSettings("TracingEnabled") = "Yes" Then
        '    Return True
        'Else
        '    Return False
        'End If
        Return False
    End Function

End Class

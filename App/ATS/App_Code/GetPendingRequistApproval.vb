Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

'' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
'' <System.Web.Script.Services.ScriptService()> _
'<WebService(Namespace:="http://tempuri.org/")> _
'<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GetPendingRequistApproval
    '     Inherits System.Web.Services.WebService

    '    <WebMethod()> _
    '    Public Function HelloWorld() As String
    '        Return "Hello World"
    '    End Function





    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function masternotification() As Object
        Dim curpage As New System.Web.UI.Page()
        Dim em As New Employee()

        em.frontoffice = True
        If em.frontoffice = True Then
            em.name = "arun"

            If em.name = "0" Then
                em.Successful = False
            Else
                em.Successful = True
            End If
            em.name = em.name + " coming"
        End If
        Return em
    End Function

End Class

Public Class Employee
    Public Property name() As String
        Get
            Return m_name
        End Get
        Set(value As String)
            m_name = value
        End Set
    End Property
    Private m_name As String
    Public Property frontoffice() As Boolean
        Get
            Return m_frontoffice
        End Get
        Set(value As Boolean)
            m_frontoffice = value
        End Set
    End Property
    Private m_frontoffice As Boolean
    Public Property Successful() As Boolean
        Get
            Return m_Successful
        End Get
        Set(value As Boolean)
            m_Successful = value
        End Set
    End Property
    Private m_Successful As Boolean
End Class

